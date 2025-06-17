using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using DataAccess;
using BP;
using System.Data;
using Formatting;

namespace BP
{

    // tidak ada insert karena data diambil pada saat
    public class TKegiatanAPBDLogic:BP 
    {
        int  _profile;
        public TKegiatanAPBDLogic(int _pTahun, int profile=1 )
            : base(_pTahun,0,profile )
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tKegiatan_A";
            
            _profile = profile;
        }


        public bool Simpan(TKegiatanAPBD t , int _tahap)
        {
            try
            {
                TKegiatanAPBD o = GetKegiatan((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.Jenis, _tahap);

                if (o == null)
                {


                    SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sWaktu," +
                    "dtPembahasan,sKeterangan,cAnggaranTahunDepan," +
                    "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                    "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                     " IDProgram,IDkegiatan,iTahun ,btJenis,sNama) values (" +
                     " @psLokasi,@psKondisi,@psWaktu,@pdtPembahasan,@psKeterangan,@pcAnggaranTahunDepan," +
                    "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan " +
                     " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama)";


                    //SSQL = "INSERT INTO " + m_sNamaTabel + " (IDDinas,IDUrusan," +
                    // " IDProgram,IDkegiatan,iTahun ,btJenis,sNama) values (" +
                    // "@pIDDinas ,@pIDUrusan " +
                    // " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama)";


                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                    paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                    paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                    paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                    paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu));
                    paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                    paramCollection.Add(new DBParameter("@piSumberDana", 0));
                    paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                    paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }
                else
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET sLokasi=@psLokasi,sKondisi=@psKondisi,sWaktu=@psWaktu," +
                       "dtPembahasan=@pdtPembahasan,sKeterangan=@psKeterangan,cAnggaranTahunDepan=@pcAnggaranTahunDepan," +
                       "cAnggaranTahunLalu=@pcAnggaranTahunLalu,sKelompokSasaran=@psKelompokSasaran,iSumberDana=@piSumberDana," +
                       "sSumberDana=@psSumberDana,sAlasanPerubahan =@psAlasanPerubahan  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                        " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis";

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi, DbType.String));
                    paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi, DbType.String));
                    paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu, DbType.String));
                    paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                    paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan, DbType.String));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran, DbType.String));
                    paramCollection.Add(new DBParameter("@piSumberDana", 0));
                    paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                    paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan, DbType.String));
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int32));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }

                IndikatorLogic oIndikatorLogic = new IndikatorLogic(Tahun,_profile);
                oIndikatorLogic.Simpan(t.ListIndikator, (int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, _tahap);
                CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun, _profile);

                oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);


                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
            

        }
        public bool Simpanoutcome(TKegiatanAPBD t)
        {
          //  try{
          //  IndikatorLogic oIndikatorLogic = new IndikatorLogic(Tahun);
     //       oIndikatorLogic.Simpan(t.ListIndikator, (int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan,(int)t.Tahap );// _tahap);
           //)

            try
            {
                SSQL = "UPDATE " + m_sNamaTabel + " SET Outcome=@pOutconme,Keluaran=@pSasaran WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                           " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@pOutconme", t.Outcome, DbType.String));
                paramCollection.Add(new DBParameter("@pSasaran", t.Keluaran == null ? "" : t.Keluaran, DbType.String));
                paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int64));
                paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                // paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int32));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
               return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public bool SimpanFormKUA(int pIDDInas)
        {
            try
            {
                List<KUA> _lst = new List<KUA>();
                SSQL = "Select tKUA.iTahun, tKUA.IDDInas,tKUA.IDUrusan, tKUA.IDProgram, tKUA.IDKegiatan,tKUA.IDUrusanMaster, " +
                    "tKUA.IDProgramMaster, tKUA.IDKegiatanMaster,mKegiatan.sNamaKegiatan,SUM(JumlahMurni) as PaguMurni, SUm(JumlahPerubahan) as PaguPerubahan from tKUA " +
                    "INNER JOIN mKegiatan ON mKegiatan.ID= tKUA.IDKegiatanMaster WHERE IDDInas = " + pIDDInas.ToString() + " AND iTahun= " + Tahun.ToString() +
                   " GROUP BY tKUA.iTahun, tKUA.IDDInas,tKUA.IDUrusan, tKUA.IDProgram, " +
                    "tKUA.IDKegiatan,tKUA.IDUrusanMaster, tKUA.IDProgramMaster, tKUA.IDKegiatanMaster,mKegiatan.sNamaKegiatan";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KUA()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    JumlahMurni = dr["PaguMurni"] == null ? 0 : DataFormat.GetDecimal(dr["PaguMurni"]),
                                    JumlahPerubahan = dr["PaguPerubahan"] == null ? 0 : DataFormat.GetDecimal(dr["PaguPerubahan"]),
                                    Jenis = 3,// DataFormat.GetInteger(dr["btJenis"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"]),
                                    NamaKegiatan = DataFormat.GetString(dr["sNamaKegiatan"]),
                                    

                                }).ToList();
                    }
                }
                if (_lst != null)
                {
                    foreach (KUA oKUA in _lst)
                    {
                        TKegiatanAPBD oKegiatan = new TKegiatanAPBD();
                        oKegiatan.Tahun = oKUA.Tahun;
                        oKegiatan.IDDinas = oKUA.IDDinas;
                        oKegiatan.IDUrusan = oKUA.IDUrusan;
                        oKegiatan.IDProgram = oKUA.IDProgram;
                        oKegiatan.IDKegiatan = oKUA.IDKegiatan;
                        oKegiatan.IDUrusanMaster = oKUA.IDUrusanMaster;
                        oKegiatan.IDProgramMaster = oKUA.IDProgramMaster;
                        oKegiatan.IDKegiatanMaster = oKUA.IDKegiatanMaster;
                        oKegiatan.Nama = oKUA.NamaKegiatan;
                        oKegiatan.Plafon = oKUA.JumlahMurni;
                        oKegiatan.PlafonABT = oKUA.JumlahPerubahan;
                        oKegiatan.Jenis = oKUA.Jenis;
                        oKegiatan.Lokasi = "";
                        oKegiatan.Kondisi = "";
                        oKegiatan.Waktu = "";
                        oKegiatan.Keterangan = "";
                        oKegiatan.AnggaranTahunDepan = 0;
                        Simpan(oKegiatan, true);


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

        public bool SimpanImport (TKegiatanAPBD t)
        {
            try
            {
                

                    SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sWaktu," +
                    "dtPembahasan,sKeterangan,cAnggaranTahunDepan," +
                    "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                    "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                     " IDProgram,IDkegiatan,iTahun ,btJenis,sNama,btKodekategoriPelaksana,btKodeUrusanPelaksana, btKodekategori,btKodeUrusan,btKodeSKPD, btKodeUK, btIDProgram, btIDKegiatan,cPlafon) values (" +
                     " @psLokasi,@psKondisi,@psWaktu,@pdtPembahasan,@psKeterangan,@pcAnggaranTahunDepan," +
                    "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan " +
                     " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama,@pbtKodekategoriPelaksana,@pbtKodeUrusanPelaksana, @pbtKodekategori,@pbtKodeUrusan,@pbtKodeSKPD, @pbtKodeUK, @pbtIDProgram,@pbtIDKegiatan,@pcPlafon)";


                    
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                    paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                    paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                    paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                    paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan",  t.AnggaranTahunDepan));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu));
                    paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                    paramCollection.Add(new DBParameter("@piSumberDana", 0));
                    paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                    paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "": t.Nama));
                    paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", t.KodeKategoriPelaksana));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", t.KodeUrusanPelaksana)); 
                    paramCollection.Add(new DBParameter("@pbtKodekategori", t.KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", t.KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", t.KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", t.KodeUK));
                    paramCollection.Add(new DBParameter("@pbtIDProgram", t.KodeProgram));
                    paramCollection.Add(new DBParameter("@pbtIDKegiatan", t.KodeKegiatan ));
                    paramCollection.Add(new DBParameter("@pcPlafon", t.Plafon));


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

               // IndikatorLogic oIndikatorLogic = new IndikatorLogic();
                //oIndikatorLogic.Simpan(t.ListIndikator,(int)t.Tahun, t.IDDinas,t.IDUrusan, t.IDProgram,t.IDKegiatan );
                //CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic();

                //oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);

                

                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
            

        }
        public bool SetDPA(TKegiatanAPBD t)
        {
            try
            {


                SSQL = "UPDATE tAnggaranRekening_A set cDPA = cJumlahMurni where IDDInas = @pIDDinas  and IDUrusan = @pIDUrusan " +
                 " and IDProgram = @pIDProgram and IDKegiatan = @pIDkegiatan and iTahun = @piTahun and btJenis= @pbtJenis";



                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));
               

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

        public bool Simpan2(TKegiatanAPBD t)
        {
            try
            {
                //TKegiatanAPBD o = GetKegiatan((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.Jenis);
                int j = CekAda((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);


                if (j == 0)
                {

                    SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sWaktu," +
                    "dtPembahasan,sKeterangan,cAnggaranTahunDepan," +
                    "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                    "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                     " IDProgram,IDkegiatan,iTahun ,btJenis,sNama) values (" +
                     " @psLokasi,@psKondisi,@psWaktu,@pdtPembahasan,@psKeterangan,@pcAnggaranTahunDepan," +
                    "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan " +
                     " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama)";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                    paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                    paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                    paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                    paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan));
                    paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu));
                    paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                    paramCollection.Add(new DBParameter("@piSumberDana", 0));
                    paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                    paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                    paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));

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
        public bool SimpanImport(List<TKegiatanAPBD> _lst, int pIDDInas, int _itahun)
        {

            try
            {
                
                //SSQL = "UPDATE tKegiatan_A SET sNama2='' WHERE iTahun = " + _itahun.ToString() + " AND IDDInas=" + pIDDInas.ToString();
                SSQL = "DELETE tKegiatan_A  WHERE iTahun = " + _itahun.ToString() + " AND IDDInas=" + pIDDInas.ToString();
                
                _dbHelper.ExecuteNonQuery(SSQL);

                foreach (TKegiatanAPBD t in _lst)
                {

                    //TKegiatanAPBD o = GetKegiatan((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, t.Jenis);
                    int jml = CekAda((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);
                    if (jml == 0)
                    {

                        SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sWaktu," +
                        "sKeterangan,cAnggaranTahunDepan," +
                        "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                        "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                         " IDProgram,IDkegiatan,iTahun ,btJenis,sNama,sNama2,cPlafon) values (" +
                         " @psLokasi,@psKondisi,@psWaktu,@psKeterangan,@pcAnggaranTahunDepan," +
                        "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan " +
                         " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama,@psNama2,@pcPlafon)";


                        //SSQL = "INSERT INTO " + m_sNamaTabel + " (IDDinas,IDUrusan," +
                        // " IDProgram,IDkegiatan,iTahun ,btJenis,sNama) values (" +
                        // "@pIDDinas ,@pIDUrusan " +
                        // " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama)";


                        DBParameterCollection paramCollection = new DBParameterCollection();

                        paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                        paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                        paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                        //paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                        paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                        paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan));
                        paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu));
                        paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                        paramCollection.Add(new DBParameter("@piSumberDana", 0));
                        paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                        paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                        paramCollection.Add(new DBParameter("@psNama", t.Nama2 == null ? "" : t.Nama2));
                        paramCollection.Add(new DBParameter("@psNama2", t.Nama2 == null ? "" : t.Nama2));
                        paramCollection.Add(new DBParameter("@pcPlafon", t.Pagu == null ? 0 : t.Pagu));
                        
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    }
                    else
                    {



                        SSQL = "UPDATE " + m_sNamaTabel + " SET sNama=@psNama , sLokasi =@psLokasi WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                             " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis";

                        DBParameterCollection paramCollection = new DBParameterCollection();                        
                        paramCollection.Add(new DBParameter("@psNama", t.Nama2 ));                        
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                        paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public bool SimpanUmum(TKegiatanAPBD t)
        {
            try
            {
                //APBDPTahunLalu
                //
              
                  SSQL = "UPDATE " + m_sNamaTabel + " SET sLokasi=@psLokasi,sKondisi=@psKondisi,sWaktu=@psWaktu," +
                        "dtPembahasan=@pdtPembahasan,sKeterangan=@psKeterangan,cAnggaranTahunDepan=@pcAnggaranTahunDepan," +
                        "cAnggaranTahunLalu=@pcAnggaranTahunLalu,sKelompokSasaran=@psKelompokSasaran,iSumberDana=@piSumberDana," +
                        "sSumberDana=@psSumberDana,sAlasanPerubahan =@psAlasanPerubahan,bLanjutan=@pLanjutan ,APBDPLAlu=@pAPBDPLAlu  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                         " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun ";

                  DBParameterCollection paramCollection = new DBParameterCollection();

                  paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi, DbType.String));
                  paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi, DbType.String));
                  paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu, DbType.String));
                  paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                  paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan, DbType.String));
                  paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Decimal));
                  paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Decimal));
                  paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran, DbType.String));
                  paramCollection.Add(new DBParameter("@piSumberDana", 0));
                  paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                  paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan, DbType.String));
                  paramCollection.Add(new DBParameter("@pLanjutan", t.ApakahLanjutan, DbType.Int16));
                  paramCollection.Add(new DBParameter("@pAPBDPLAlu", t.APBDPTahunLalu == null ? 0 : t.APBDPTahunLalu, DbType.Decimal));
                  paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                  paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                  paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                  paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                  paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                  paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                  CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun, _profile);
              oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);



                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public bool SimpanUntukGabungan(TKegiatanAPBD t)
        {
            return SimpanUntukGabungan(t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);

        }
        public bool SimpanUntukGabungan(  int pIDDinas, int pIDUrusan, int  pIDProgram, long pIDKegiatan, int pJenis, TKegiatanAPBD t=null )
        {
            try
            {
                //APBDPTahunLalu
                //
                int jml = CekAda(Tahun,pIDDinas, pIDUrusan, pIDProgram, pIDKegiatan, pJenis );
                if (jml == 0)
                {


                    SKPDLogic oLogic = new SKPDLogic(Tahun);
                    List<SKPD> lstSKPD = new List<SKPD>();
                    if (t != null) 
                         lstSKPD = oLogic.GetByParent(t.IDDinas);
                    else
                        lstSKPD = oLogic.GetByParent(pIDDinas);

                    
                    
                    
                        foreach (SKPD s in lstSKPD)
                    {
                        //if (t != null)
                        //{
                        //    jml = CekAda(t);

                        //} else 

                        if (t != null)
                        {
                            jml = CekAda(Tahun, s.ID, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);
                            pIDKegiatan = t.IDKegiatan;
                            pIDDinas = t.IDDinas;


                        }
                        else
                        {
                            jml = CekAda(Tahun, s.ID, pIDUrusan, pIDProgram, pIDKegiatan, pJenis );//t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);
                        }
                        if (jml > 0)
                        {
                            SSQL = "SELECT * into tempKegiatanGabung from tKegiatan_A where IDkegiatan = " + pIDKegiatan.ToString() + " AND IDDInas = " + s.ID.ToString();
                            _dbHelper.ExecuteNonQuery(SSQL);
                            SSQL = "UPDATE tempKegiatanGabung SET IDDInas = " + pIDDinas.ToString() + ", sLokasi='" + s.Nama + "'";
                            _dbHelper.ExecuteNonQuery(SSQL);
                            SSQL = "INSERT INTO tKegiatan_A SELECT * from tempKegiatanGabung ";
                            _dbHelper.ExecuteNonQuery(SSQL);
                            SSQL = "DROP TABLE tempKegiatanGabung ";
                            _dbHelper.ExecuteNonQuery(SSQL);
                            break;
                        }

                    }
                }
                    
                //    SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sNama,sWaktu," +
                //    "sKeterangan,cAnggaranTahunDepan," +
                //    "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                //    "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                //     " IDProgram,IDkegiatan,iTahun ,btJenis,sNama2,cPlafon) values (" +
                //     " @psLokasi,@psKondisi,@psNama,@psWaktu,@psKeterangan,@pcAnggaranTahunDepan," +
                //    "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan " +
                //     " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama2,@pcPlafon)";


                //    //SSQL = "INSERT INTO " + m_sNamaTabel + " (IDDinas,IDUrusan," +
                //    // " IDProgram,IDkegiatan,iTahun ,btJenis,sNama) values (" +
                //    // "@pIDDinas ,@pIDUrusan " +
                //    // " ,@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama)";


                //    DBParameterCollection paramCollection = new DBParameterCollection();

                //    paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                //    paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                //    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                    
                //    paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                //    //paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                //    paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                //    paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan));
                //    paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu));
                //    paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                //    paramCollection.Add(new DBParameter("@piSumberDana", 0));
                //    paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                //    paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                //    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                //    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                //    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                //    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                //    paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                //    paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                ////    paramCollection.Add(new DBParameter("@psNama", t.Nama2 == null ? "" : t.Nama2));
                //    paramCollection.Add(new DBParameter("@psNama2", t.Nama2 == null ? "" : t.Nama2));
                //    paramCollection.Add(new DBParameter("@pcPlafon", t.Pagu == null ? 0 : t.Pagu));

                //    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                //}
                //else
                //{

                //    SSQL = "UPDATE " + m_sNamaTabel + " SET sNama=@psNama,sLokasi=@psLokasi,sKondisi=@psKondisi,sWaktu=@psWaktu," +
                //          "dtPembahasan=@pdtPembahasan,sKeterangan=@psKeterangan,cAnggaranTahunDepan=@pcAnggaranTahunDepan," +
                //          "cAnggaranTahunLalu=@pcAnggaranTahunLalu,sKelompokSasaran=@psKelompokSasaran,iSumberDana=@piSumberDana," +
                //          "sSumberDana=@psSumberDana,sAlasanPerubahan =@psAlasanPerubahan,bLanjutan=@pLanjutan ,APBDPLAlu=@pAPBDPLAlu  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                //           " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis";

                //    DBParameterCollection paramCollection = new DBParameterCollection();

                //    paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                    
                //    paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi, DbType.String));
                //    paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi, DbType.String));
                //    paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu, DbType.String));
                //    paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                //    paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan, DbType.String));
                //    paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Decimal));
                //    paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Decimal));
                //    paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran, DbType.String));
                //    paramCollection.Add(new DBParameter("@piSumberDana", 0));
                //    paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                //    paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan, DbType.String));
                //    paramCollection.Add(new DBParameter("@pLanjutan", t.ApakahLanjutan, DbType.Int16));
                //    paramCollection.Add(new DBParameter("@pAPBDPLAlu", t.APBDPTahunLalu == null ? 0 : t.APBDPTahunLalu, DbType.Decimal));
                //    paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                //    paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                //    paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                //    paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                //    paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                //    paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));
                //    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                //}
                //CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun);
                //oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);



                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }


        public bool Simpan(TKegiatanAPBD t, bool frmKUA = false )
        {
            try
            {
                //APBDPTahunLalu
                //
                int jml = CekAda(Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int) t.Jenis,t.IDUnit);
                if (jml == 0)
                {
                    if (Tahun >= 2021)
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

                        _KodeProgram = DataFormat.GetInteger(t.IDProgram.ToString().Substring(3, 2));
                        _KodeKegiatan = DataFormat.GetInteger(t.IDKegiatan.ToString().Substring(5, 3));
                    
                        _KodeKategoriPelaksana = DataFormat.GetInteger(t.IDUrusan.ToString().Substring(0, 1));
                        _kodeUrusanPelaksana = DataFormat.GetInteger(t.IDUrusan.ToString().Substring(1, 2));
                        _KodeKategori = DataFormat.GetInteger(t.IDDinas.ToString().Substring(0, 1));
                        _KodeUrusan = DataFormat.GetInteger(t.IDDinas.ToString().Substring(1, 2));
                        _KodeSKPD = DataFormat.GetInteger(t.IDDinas.ToString().Substring(3, 2));
                        _KodeUK = t.KodeUK;

                        SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sWaktu," +
                            "sKeterangan,cAnggaranTahunDepan," +
                            "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                            "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                            " btkodekategori,btkodeurusan, btkodeSKPD, btkodeUK, btkodekategoriPelaksana,btkodeurusanPelaksana,btidprogram, btidkegiatan" + 
                              ", IDProgram,IDkegiatan,iTahun ,btJenis,sNama,sNama2,cPlafon, cPlafonABT,Outcome,Keluaran,idUnit) values (" +
                             " @psLokasi,@psKondisi,@psWaktu,@psKeterangan,@pcAnggaranTahunDepan," +
                            "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan, " +
                            "@pbtkodekategori,@pbtkodeurusan, @pbtkodeSKPD, @pKodeUK, @pbtkodekategoriPelaksana,@pbtkodeurusanPelaksana,@pbtidprogram, @pbtidkegiatan"+
                            ",@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama,@psNama2,@pcPlafon,@pcPlafonABT,@pOutcome,@pKeluaran,@pidUnit)";

                        DBParameterCollection paramCollection = new DBParameterCollection();

                        paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                        paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                        paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                        //paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                        paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                        paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan));
                        paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu));
                        paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                        paramCollection.Add(new DBParameter("@piSumberDana", 0));
                        paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                        paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pbtkodekategori", _KodeKategori));
                        paramCollection.Add(new DBParameter("@pbtkodeurusan", _KodeUrusan));
                        paramCollection.Add(new DBParameter("@pbtkodeSKPD", _KodeSKPD));
                        paramCollection.Add(new DBParameter("@pKodeUK", t.KodeUK));

                        paramCollection.Add(new DBParameter("@pbtkodekategoriPelaksana", _KodeKategoriPelaksana));
                        paramCollection.Add(new DBParameter("@pbtkodeurusanPelaksana", _kodeUrusanPelaksana));
                        paramCollection.Add(new DBParameter("@pbtidprogram", _KodeProgram));
                        paramCollection.Add(new DBParameter("@pbtidkegiatan", _KodeKegiatan));

                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                        paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                        paramCollection.Add(new DBParameter("@psNama2", t.Nama == null ? "" : t.Nama, DbType.String));
                        paramCollection.Add(new DBParameter("@pcPlafon", t.Pagu == null ? 0 : t.Pagu, DbType.Currency));
                        paramCollection.Add(new DBParameter("@pcPlafonABT", t.PaguABT == null ? 0 : t.PaguABT, DbType.Currency));
                        paramCollection.Add(new DBParameter("@pOutcome", t.Outcome == null ? "" : t.Outcome, DbType.String));
                        paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran == null ? "" : t.Keluaran, DbType.String));
                        paramCollection.Add(new DBParameter("@pidUnit", t.IDUnit , DbType.Int32));
                        
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
                    else
                    {
                        SSQL = "INSERT INTO " + m_sNamaTabel + " (sLokasi,sKondisi,sWaktu," +
                        "sKeterangan,cAnggaranTahunDepan," +
                        "cAnggaranTahunLalu,sKelompokSasaran,iSumberDana," +
                        "sSumberDana,sAlasanPerubahan ,IDDinas,IDUrusan," +
                         " IDProgram,IDkegiatan,iTahun ,btJenis,sNama,sNama2,cPlafon, cPlafonABT,IDUnit) values (" +
                         " @psLokasi,@psKondisi,@psWaktu,@psKeterangan,@pcAnggaranTahunDepan," +
                        "@pcAnggaranTahunLalu,@psKelompokSasaran,@piSumberDana,@psSumberDana,@psAlasanPerubahan,@pIDDinas ,@pIDUrusan " +
                        ",@pIDProgram ,@pIDkegiatan ,@piTahun ,@pbtJenis,@psNama,@psNama2,@pcPlafon,@pcPlafonABT, @pidUnit)";

                        DBParameterCollection paramCollection = new DBParameterCollection();

                        paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi == null ? "" : t.Lokasi, DbType.String));
                        paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi));
                        paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu));
                        //paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                        paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan));
                        paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan));
                        paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu));
                        paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran));
                        paramCollection.Add(new DBParameter("@piSumberDana", 0));
                        paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                        paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan));
                        paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas));
                        paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan));
                        paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram));
                        paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan));
                        paramCollection.Add(new DBParameter("@piTahun", t.Tahun));
                        paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis));
                        paramCollection.Add(new DBParameter("@psNama", t.Nama == null ? "" : t.Nama));
                        paramCollection.Add(new DBParameter("@psNama2", t.Nama == null ? "" : t.Nama, DbType.String));
                        paramCollection.Add(new DBParameter("@pcPlafon", t.Pagu == null ? 0 : t.Pagu, DbType.Currency));
                        paramCollection.Add(new DBParameter("@pcPlafonABT", t.PaguABT == null ? 0 : t.PaguABT, DbType.Currency));
                        paramCollection.Add(new DBParameter("@pidUnit", t.IDUnit, DbType.Int32));
                        

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }

                }
                else
                {
                    if (frmKUA == true)
                    {

                        if (Tahun >= 2021)
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET  cPlafon=@pcPlafon,cPlafonABT=@pcPlafonABT,cAnggaranTahunLalu=@pcAnggaranTahunLalu, cAnggaranTahunDepan=@pcAnggaranTahunDepan, Outcome=@pOutcome,Keluaran=@pKeluaran WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                                   " AND IDProgram=@pIDProgram AND IDUnit = @pUnit AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis ";

                            DBParameterCollection paramCollection = new DBParameterCollection();
                            paramCollection.Add(new DBParameter("@pcPlafon", t.Pagu == null ? 0 : t.Pagu, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pcPlafonABT", t.PaguABT == null ? 0 : t.PaguABT, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));
                            paramCollection.Add(new DBParameter("@pOutcome",t.Outcome));
                            paramCollection.Add(new DBParameter("@pKeluaran", t.Keluaran));
                            paramCollection.Add(new DBParameter("@punit", t.IDUnit, DbType.Int32));

                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                        }
                        else
                        {
                            SSQL = "UPDATE " + m_sNamaTabel + " SET  cPlafon=@pcPlafon,cPlafonABT=@pcPlafonABT,cAnggaranTahunLalu=@pcAnggaranTahunLalu, cAnggaranTahunDepan=@pcAnggaranTahunDepan WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                              " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis ";

                            DBParameterCollection paramCollection = new DBParameterCollection();
                            paramCollection.Add(new DBParameter("@pcPlafon", t.Pagu == null ? 0 : t.Pagu, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pcPlafonABT", t.PaguABT == null ? 0 : t.PaguABT, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Currency));
                            paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));

                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                        }

                    }
                    else
                    {

                        try
                        {
                            //APBDPTahunLalu
                            //

                            SSQL = "UPDATE " + m_sNamaTabel + " SET sLokasi=@psLokasi,sKondisi=@psKondisi,sWaktu=@psWaktu," +
                                  "dtPembahasan=@pdtPembahasan,sKeterangan=@psKeterangan,cAnggaranTahunDepan=@pcAnggaranTahunDepan," +
                                  "cAnggaranTahunLalu=@pcAnggaranTahunLalu,sKelompokSasaran=@psKelompokSasaran,iSumberDana=@piSumberDana," +
                                  "sSumberDana=@psSumberDana,sAlasanPerubahan =@psAlasanPerubahan,bLanjutan=@pLanjutan ,APBDPLAlu=@pAPBDPLAlu  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                                   " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis";

                            DBParameterCollection paramCollection = new DBParameterCollection();

                            paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi, DbType.String));
                            paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi, DbType.String));
                            paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu, DbType.String));
                            paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                            paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan, DbType.String));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran, DbType.String));
                            paramCollection.Add(new DBParameter("@piSumberDana", 0));
                            paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                            paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan, DbType.String));
                            paramCollection.Add(new DBParameter("@pLanjutan", t.ApakahLanjutan, DbType.Int16));
                            paramCollection.Add(new DBParameter("@pAPBDPLAlu", t.APBDPTahunLalu == null ? 0 : t.APBDPTahunLalu, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));

                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                            //CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun, _profile);
                            //oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);



                            return true;
                        }
                        catch (Exception ex)
                        {
                            _isError = true;
                            _lastError = ex.Message;
                            return false;
                        }
                    }
                }
                //CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun);
                //oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);



                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }

        public bool SimpanUmum(TKegiatanAPBD t, bool frmKUA = false)
        {
            try
            {
                //APBDPTahunLalu
                //
                int jml = CekAda(Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);
                if (jml > 0)
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET sLokasi=@psLokasi,sKondisi=@psKondisi,sWaktu=@psWaktu," +
                                  "dtPembahasan=@pdtPembahasan,sKeterangan=@psKeterangan,cAnggaranTahunDepan=@pcAnggaranTahunDepan," +
                                  "cAnggaranTahunLalu=@pcAnggaranTahunLalu,sKelompokSasaran=@psKelompokSasaran,iSumberDana=@piSumberDana," +
                                  "sSumberDana=@psSumberDana,sAlasanPerubahan =@psAlasanPerubahan,bLanjutan=@pLanjutan ,APBDPLAlu=@pAPBDPLAlu  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                                   " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis";

                            DBParameterCollection paramCollection = new DBParameterCollection();

                            paramCollection.Add(new DBParameter("@psLokasi", t.Lokasi, DbType.String));
                            paramCollection.Add(new DBParameter("@psKondisi", t.Kondisi == null ? "" : t.Kondisi, DbType.String));
                            paramCollection.Add(new DBParameter("@psWaktu", t.Waktu == null ? "" : t.Waktu, DbType.String));
                            paramCollection.Add(new DBParameter("@pdtPembahasan", t.TanggalPembahasan, DbType.DateTime));
                            paramCollection.Add(new DBParameter("@psKeterangan", t.Keterangan == null ? "" : t.Keterangan, DbType.String));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunDepan", t.AnggaranTahunDepan == null ? 0 : t.AnggaranTahunDepan, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pcAnggaranTahunLalu", t.AnggaranTahunLalu == null ? 0 : t.AnggaranTahunLalu, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@psKelompokSasaran", t.KelompokSasaran == null ? "" : t.KelompokSasaran, DbType.String));
                            paramCollection.Add(new DBParameter("@piSumberDana", 0));
                            paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                            paramCollection.Add(new DBParameter("@psAlasanPerubahan", t.AlasanPerubahan == null ? "" : t.AlasanPerubahan, DbType.String));
                            paramCollection.Add(new DBParameter("@pLanjutan", t.ApakahLanjutan, DbType.Int16));
                            paramCollection.Add(new DBParameter("@pAPBDPLAlu", t.APBDPTahunLalu == null ? 0 : t.APBDPTahunLalu, DbType.Decimal));
                            paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                            paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                            paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));
                            


                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                            CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun, _profile);
                            oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);
                            
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
        public bool SimpanIndikator(TKegiatanAPBD t, int _tahap=1)
        {
            try
            {


                IndikatorLogic oIndikatorLogic = new IndikatorLogic(Tahun, _profile);
                oIndikatorLogic.Simpan(t.ListIndikator, (int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, _tahap);

                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }


        }

        public bool SimpanKolomSumberDana(TKegiatanAPBD t)
        {
            try
            {
                //APBDPTahunLalu
                //

                SSQL = "UPDATE " + m_sNamaTabel + " SET iSumberDana=@piSumberDana,sSumberDana=@psSumberDana  WHERE IDDinas=@pIDDinas AND IDUrusan=@pIDUrusan " +
                       " AND IDProgram=@pIDProgram AND IDkegiatan=@pIDkegiatan AND iTahun =@piTahun AND btJenis=@pbtJenis";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@piSumberDana", 0));
                paramCollection.Add(new DBParameter("@psSumberDana", t.SumberDana == null ? "" : t.SumberDana, DbType.String));
                paramCollection.Add(new DBParameter("@pIDDinas", t.IDDinas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", t.IDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", t.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDkegiatan", t.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@piTahun", t.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtJenis", t.Jenis, DbType.Int16));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                CatatanKegiatanLogic oCatatanKegiatanLogic = new CatatanKegiatanLogic(Tahun, _profile);
                oCatatanKegiatanLogic.Simpan(t.ListCatatan, (int)t.Tahun, t.IDUrusan, t.IDDinas, t.IDProgram, t.IDKegiatan, t.Jenis);



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
        public bool CekNAmaKegiatan(TKegiatan oKegiatan) //int pTahun, int pIDDinas,int pIDUrusan, int pIDProgram, int pIDKegiatan, Single _pJenis)
        {
            
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + oKegiatan.Tahun.ToString() + " AND IDDInas =" + oKegiatan.IDDinas.ToString() +
                       " AND IDUrusan =" + oKegiatan.IDUrusan.ToString() + " AND IDProgram = " + oKegiatan.IDProgram.ToString() +
                       " AND IDKegiatan=" + oKegiatan.IDKegiatan.ToString();// +" AND btJenis= " + oKegiatan.Jenis.ToString();

                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        if (DataFormat.GetString(dr["sNama"]).Length==0){

                            SSQL = "Update " + m_sNamaTabel + " SET sNama ='" + oKegiatan.Nama + "' WHERE iTAhun =" + oKegiatan.Tahun.ToString() + " AND IDDInas =" + oKegiatan.IDDinas.ToString() +
                                " AND IDUrusan =" + oKegiatan.IDUrusan.ToString() + " AND IDProgram = " + oKegiatan.IDProgram.ToString() +
                                " AND IDKegiatan=" + oKegiatan.IDKegiatan.ToString();// +" AND btJenis= " + oKegiatan.Jenis.ToString();
                            _dbHelper.ExecuteNonQuery(SSQL);
                        }
                    }
                }
                return true;
            } catch(Exception ex){
                _lastError= ex.Message;
                return false;
            }
        }

        public bool CekKUAdanKegiatan(int _iTahun, int _pDinas)
        {
            try
            {

                SSQL = " Select tKUA.iTahun, tKUA.IDurusan,tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, mKegiatan.sNamaKegiatan as sNama,btJenis,tKUA.JumlahOlah as cPlafon  " +
                         " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                         " where tKUA.iTahun= " + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + " AND isnull(tKUA.Status,0)<9 " +
                        "  ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";

                DataTable dtkua = new DataTable();
                dtkua = _dbHelper.ExecuteDataTable(SSQL);
                if (dtkua != null)
                {
                    if (dtkua.Rows.Count > 0)
                    {
                        foreach (DataRow drKUA in dtkua.Rows)
                        {
                            //TKegiatanAPBD o = new TKegiatanAPBD();
                            //o.IDDinas = 
                            //if (DataFormat.GetInteger(drKUA["IDKEgiatan"]) == 1211612)
                            //{
                            //    _lastError = DataFormat.GetInteger(drKUA["IDKEgiatan"]).ToKodeKegiatan();
                            //}
                            SSQL = "Select * FROM tKegiatan_A where iTahun=" + _iTahun.ToString() + " and IDDInas= " + _pDinas.ToString() + "  and IDUrusan= " +
                                DataFormat.GetString(drKUA["IDUrusan"]) + " AND IDProgram=" + DataFormat.GetString(drKUA["IDProgram"]) + " AND IDKegiatan=" + DataFormat.GetString(drKUA["IDKEgiatan"]);

                            DataTable dtx = new DataTable();
                            dtx = _dbHelper.ExecuteDataTable(SSQL);
                            if (dtx != null)
                            {
                                if (dtx.Rows.Count == 0)
                                {//todo
                                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDinas,IDUrusan," +
                                            " IDProgram,IDkegiatan ,btJenis,sNama,cPlafon) Select tKUA.iTahun, tKUA.IDDInas,tKUA.IDurusan, tKUA.IDProgram,tKUA.IDkegiatan, 3 as btJenis, mKegiatan.sNamaKegiatan as sNama,tKUA.JumlahOlah as cPagu " +
                                            " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                                             " where tKUA.iTahun= " + _iTahun.ToString() + " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + " AND tKUA.IDUrusan = " + DataFormat.GetString(drKUA["IDUrusan"]) +
                                             " AND tKUA.IDProgram = " + DataFormat.GetString(drKUA["IDProgram"]) + " AND tKUA.IDkegiatan = " + DataFormat.GetString(drKUA["IDKEgiatan"]) +
                                             " AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + " AND isnull(tKUA.Status,0)<9 ";
                                    _dbHelper.ExecuteNonQuery(SSQL);
                                } else {
                                    //SSQL = " Select tKUA.iTahun, tKUA.IDurusan,tKUA.IDDInas, tKUA.IDProgram,tKUA.IDkegiatan, mKegiatan.sNamaKegiatan as sNama,btJenis,tKUA.JumlahOlah as cPlafon  " +
                                    //    " FROM tKUA INNER JOIN mKegiatan ON tKUA.IDUrusanMaster= mKegiatan.IDUrusan and tKUA.IDProgramMaster= mKegiatan.IDProgram and tKUA.IDkegiatanMaster= mKegiatan.ID " +
                                    //    " where tKUA.iTahun= " + _iTahun.ToString() + " AND tKUA.btJenis=3 AND tKUA.IDlokasi=0  and tKUA.IDDInas= " + _pDinas.ToString() + "  and tKUA.IDUrusan= " + DataFormat.GetString(drKUA["IDUrusan"]) + " AND tKUA.IDProgram=" + DataFormat.GetString(drKUA["IDProgram"]) + " AND IDKegiatan=" + DataFormat.GetString(drKUA["IDKEgiatan"])  +"  AND isnull(tKUA.Status,0)<9 " +
                                    //    " ORDER BY tKUA.IDUrusan,tKUA.IDProgram, tKUA.IDkegiatan, tKUA.btIDprogram, tKUA.btIDkegiatan,  mKegiatan.sNamaKEgiatan ";                                    
                                    //DataTable dt = new DataTable();
                                    //dt = _dbHelper.ExecuteDataTable(SSQL);
                                    //if (dt != null)
                                    //{
                                    //    if (dt.Rows.Count > 0)
                                    //    {
                                    //        //DataRow dr = dt.Rows[0];

                                    //        //SSQL = "Update tKegiatan_A SET sNama ='" + DataFormat.GetString(dr["sNama"]) + "',cPlafon = " + DataFormat.GetDecimal(dr["cPlafon"]) + " WHERE iTAhun =" + _iTahun.ToString() + " AND IDDInas =" + _pDinas.ToString() +
                                    //        //        " AND IDUrusan =" + DataFormat.GetString(dr["IDUrusan"]) + " AND IDProgram = " + DataFormat.GetString(dr["IDProgram"])  +
                                    //        //        " AND IDKegiatan=" + DataFormat.GetString(dr["IDkegiatan"]) + " AND isnull(sNama,'')=''";// +" AND btJenis= " + oKegiatan.Jenis.ToString();

                                    //        //_dbHelper.ExecuteNonQuery(SSQL);

                                    //    }
                                    //    else
                                    //    {

                                    //    }
                                    //} 
                                }
                            }
                        }
                    }
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

        //public TKegiatanAPBD GetKegiatan22(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan, Single _pJenis, int _tahap, bool bGabungan =false)
        //{
        //    //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

        //    //}
        //    TKegiatanAPBD tKAPBD = new TKegiatanAPBD();
        //    try
        //    {
               
        //            if (pIDProgram == 0)
        //            {
        //                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
        //                       " AND IDProgram = " + pIDProgram.ToString() +
        //                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();
        //            }
        //            else
        //            {
        //                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
        //                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
        //                       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();
        //            }
              



        //        _dbHelper.ExecuteNonQuery(SSQL);
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);

        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];
        //                tKAPBD = new TKegiatanAPBD()
        //                {
        //                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
        //                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
        //                    Nama = DataFormat.GetString(dr["sNama"]),
        //                    Pagu = DataFormat.GetDecimal(dr["cPlafon"]), //GetPlafon(DataFormat.GetInteger(dr["iTahun"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDurusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"])),//DataFormat.GetDecimal(dr["cPlafon"]),
        //                    PaguABT = DataFormat.GetDecimal(dr["cPlafonABT"]),
        //                    Lokasi = DataFormat.GetString(dr["sLokasi"]),
        //                    Kondisi = DataFormat.GetString(dr["sKondisi"]),
        //                    Waktu = DataFormat.GetString(dr["sWaktu"]),
        //                    TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
        //                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
        //                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
        //                    AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
        //                    AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
        //                    APBDPTahunLalu = DataFormat.GetDecimal(dr["APBDPLAlu"]),
        //                    KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
        //                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
        //                    KodeKegiatan = DataFormat.GetInteger(dr["IDKegiatan"])==0?0: Convert.ToInt32(DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5)),
        //                    KodeProgram =DataFormat.GetInteger(dr["IDProgram"])==0?0:  Convert.ToInt32(DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram)),
        //                    ApakahLanjutan = DataFormat.GetInteger(dr["bLanjutan"]),
        //                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length >5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
        //                    AlasanPerubahan = dr["sAlasanPerubahan"]!= null?dr["sAlasanPerubahan"].ToString():"", //DataFormat.GetString(dr["sAlasanPerubahan"]),
        //                    SumberDana = GetSumberDana (DataFormat.GetInteger(dr["iTahun"]),
        //                                                 DataFormat.GetInteger(dr["IDDInas"]),
        //                                                 DataFormat.GetInteger(dr["IDurusan"]),
        //                                                 DataFormat.GetInteger(dr["IDProgram"]),
        //                                                 DataFormat.GetInteger(dr["IDkegiatan"])),
        //                    ListIndikator = GetIndikator(DataFormat.GetInteger(dr["iTahun"]),
        //                                                 DataFormat.GetInteger(dr["IDDInas"]),
        //                                                 DataFormat.GetInteger(dr["IDurusan"]),
        //                                                 DataFormat.GetInteger(dr["IDProgram"]),
        //                                                 DataFormat.GetInteger(dr["IDkegiatan"]), _tahap), //,bGabungan
        //                    ListCatatan = GetCatatanKegiatan(DataFormat.GetInteger(dr["iTahun"]),
        //                                                DataFormat.GetInteger(dr["IDDInas"]),
        //                                                DataFormat.GetInteger(dr["IDurusan"]),
        //                                                DataFormat.GetInteger(dr["IDProgram"]),
        //                                                DataFormat.GetInteger(dr["IDkegiatan"]), _pJenis),
        //                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"])==0?
        //                                    DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
        //                                   "00.000":DataFormat.GetInteger(dr["IDProgram"]) > 0 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram) + "." +
        //                                   DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5) : ""
        //                };
        //                return tKAPBD;
        //            }
        //            else
        //            { // jikatidak ada..

        //                TKegiatan oKegiatan = new TKegiatan();
        //                TKegiatanLogic oKUALogic = new TKegiatanLogic(Tahun, _profile);
        //                if (oKUALogic.ExportToAPBD(pTahun, pIDDinas, pIDUrusan, pIDProgram, pIDKegiatan, (int)_pJenis) == true)
        //                {
        //                    // *********************************************

        //                    if (pIDProgram == 0)
        //                    {
        //                        SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
        //                            " AND IDProgram = " + pIDProgram.ToString() +
        //                            " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();

        //                    }
        //                    else
        //                    {
        //                        SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
        //                            " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
        //                            " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();
        //                    }
        //                    _dbHelper.ExecuteNonQuery(SSQL);
        //                    DataTable dtx = new DataTable();
        //                    dtx = _dbHelper.ExecuteDataTable(SSQL);

        //                    if (dtx != null)
        //                    {
        //                        if (dtx.Rows.Count > 0)
        //                        {
        //                            DataRow dr = dtx.Rows[0];
        //                            tKAPBD = new TKegiatanAPBD()
        //                            {
        //                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                                IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
        //                                IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                                IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
        //                                Nama = DataFormat.GetString(dr["sNama"]),
        //                                Pagu = DataFormat.GetDecimal(dr["cPlafon"]),
        //                                Lokasi = DataFormat.GetString(dr["sLokasi"]),
        //                                Kondisi = DataFormat.GetString(dr["sKondisi"]),
        //                                Waktu = DataFormat.GetString(dr["sWaktu"]),
        //                                TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
        //                                Keterangan = DataFormat.GetString(dr["sKeterangan"]),
        //                                TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
        //                                AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
        //                                AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
        //                                KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
        //                                AlasanPerubahan = DataFormat.GetString(dr["sAlasanPerubahan"]),
        //                                KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
                         
        //                                ListIndikator = GetIndikator(DataFormat.GetInteger(dr["iTahun"]),
        //                                                             DataFormat.GetInteger(dr["IDDInas"]),
        //                                                             DataFormat.GetInteger(dr["IDurusan"]),
        //                                                             DataFormat.GetInteger(dr["IDProgram"]),
        //                                                             DataFormat.GetInteger(dr["IDkegiatan"]), _tahap),
        //                                ListCatatan = GetCatatanKegiatan(DataFormat.GetInteger(dr["iTahun"]),
        //                                                            DataFormat.GetInteger(dr["IDDInas"]),
        //                                                            DataFormat.GetInteger(dr["IDurusan"]),
        //                                                            DataFormat.GetInteger(dr["IDProgram"]),
        //                                                            DataFormat.GetInteger(dr["IDkegiatan"]), _pJenis),
        //                                TampilanKode =  DataFormat.GetInteger(dr["IDProgram"])==0?
        //                                    DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
        //                                   "00.000"
        //                                    : DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
        //                                   DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram) + "." +
        //                                   DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan)
        //                            };
        //                            return tKAPBD;
        //                        }
        //                    }
        //                    //*********

        //                }
        //                else
        //                    return null;

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        _isError = true;
        //        _lastError = ex.Message;

        //        return null;
        //    }
        //    return tKAPBD;

        //}
        public TKegiatanAPBD GetKegiatan(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int pIDKegiatan, Single _pJenis,int _tahap, List<SKPD> lst=null,int idunit = 0 )
        {


            TKegiatanAPBD tKAPBD = new TKegiatanAPBD();
            try
            {

                string strSKPD = "(";

                if (lst != null)
                {
                    foreach(SKPD s in lst){
                        strSKPD = strSKPD + s.ID.ToString() + ",";
                    }
                    strSKPD = strSKPD + "99)";

                    SimpanUntukGabungan( pIDDinas, pIDUrusan, pIDProgram, pIDKegiatan, (int)_pJenis);


                }

                if (pIDProgram == 0 && lst ==null)
                {
                    SSQL = "SELECT tKegiatan_A.*, tKUA.JumlahMurni, tKUA.JumlahPerubahan FROM " + m_sNamaTabel + " LEFT JOIN tKUA On tKegiatan_A.IDDInas = tKUA.IDDInas and tKegiatan_A.IDKEgiatan =tKUA.IDKegiatan and tKegiatan_A.iTahun = tKUA.iTahun and tKegiatan_A.profile = tKUA.profile  WHERE tKegiatan_A.iTAhun =" + pTahun.ToString() + " AND tKegiatan_A.IDDInas =" + pIDDinas.ToString() +
                           " AND tKegiatan_A.IDProgram = " + pIDProgram.ToString() +
                           " AND tKegiatan_A.IDKegiatan=" + pIDKegiatan.ToString() + " AND tKegiatan_A.btJenis=" + _pJenis.ToString() + " AND tKUA.IDLokasi=0" ;
                }
                else
                {

                    if (lst == null)
                    {
                       

                        SSQL = "SELECT tKegiatan_A.iTahun,tKegiatan_A.IDDInas,tKegiatan_A.IDurusan,tKegiatan_A.IDProgram,tKegiatan_A.IDkegiatan,sNama,cPlafon,cPlafonABT,sAlasanPerubahan,sLokasi,bLanjutan,sKondisi,sWaktu,dtPembahasan,sKeterangan,btTahapInput,sKelompokSasaran,tKegiatan_A.btJenis, cAnggaranTahunDepan," +
                             " cAnggaranTahunLalu ,0  as JumlahMurni, 0 as JumlahPerubahan, Outcome,Keluaran,ssumberdana FROM " + m_sNamaTabel + " WHERE tKegiatan_A.iTAhun =" + pTahun.ToString() + " AND tKegiatan_A.IDDInas =" + pIDDinas.ToString() +
                            " AND tKegiatan_A.IDUrusan =" + pIDUrusan.ToString() + " AND tKegiatan_A.IDProgram = " + pIDProgram.ToString() +
                            " AND tKegiatan_A.IDKegiatan=" + pIDKegiatan.ToString() + " AND tKegiatan_A.btJenis=" + _pJenis.ToString() + " ";
                    
                    }
                    else
                    {
                        SSQL = "SELECT tKegiatan_A.iTahun,tKegiatan_A.IDDInas,tKegiatan_A.IDurusan,tKegiatan_A.IDProgram,tKegiatan_A.IDkegiatan,sNama,ssumberdana,cPlafon,cPlafonABT,sLokasi,sAlasanPerubahan,bLanjutan,sKondisi,sWaktu,dtPembahasan,sKeterangan,btTahapInput,sKelompokSasaran,btJenis, " +
                            "(select sum (cAnggaranTahunDepan) From tKegiatan_A   where IDDInas in " + strSKPD + " and IDKegiatan = " + pIDKegiatan.ToString() + " ) AS cAnggaranTahunDepan," +
                                "(select sum (cAnggaranTahunLalu) From tKegiatan_A  where tKegiatan_A.IDDInas in " + strSKPD + " and tKegiatan_A.IDKegiatan = " + pIDKegiatan.ToString() + " ) AS cAnggaranTahunLalu , Outcome,Keluaran FROM " + m_sNamaTabel + " WHERE tKegiatan_A.iTAhun =" + pTahun.ToString() + " AND tKegiatan_A.IDDInas = " + pIDDinas.ToString() +
                               " AND tKegiatan_A.IDUrusan =" + pIDUrusan.ToString() + " AND tKegiatan_A.IDProgram = " + pIDProgram.ToString() +
                               " AND tKegiatan_A.IDKegiatan=" + pIDKegiatan.ToString() + " AND tKegiatan_A.btJenis=" + _pJenis.ToString() + " AND sNama <>''" ;
                        // RKA gabungan 

                    }
                }

                


                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        tKAPBD = new TKegiatanAPBD()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                            Nama = DataFormat.GetString(dr["sNama"]),
                            Pagu = DataFormat.GetDecimal(dr["cPlafon"]), //GetPlafon(DataFormat.GetInteger(dr["iTahun"]), DataFormat.GetInteger(dr["IDDInas"]), DataFormat.GetInteger(dr["IDurusan"]), DataFormat.GetInteger(dr["IDProgram"]), DataFormat.GetInteger(dr["IDkegiatan"])),//DataFormat.GetDecimal(dr["cPlafon"]),
                            PaguABT = DataFormat.GetDecimal(dr["cPlafonABT"]),
                            Lokasi = DataFormat.GetString(dr["sLokasi"]),
                            Kondisi = DataFormat.GetString(dr["sKondisi"]),
                            Waktu = DataFormat.GetString(dr["sWaktu"]),
                            TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
                            Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                            TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                            AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
                            AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
                            APBDPTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
                            KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
                            Outcome = DataFormat.GetString(dr["Outcome"]),
                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"]),
                            KodeKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]) == 0 ? 0 : Convert.ToInt32(DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5)),
                            KodeProgram = DataFormat.GetInteger(dr["IDProgram"]) == 0 ? 0 : Convert.ToInt32(DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram)),
                            ApakahLanjutan = DataFormat.GetInteger(dr["bLanjutan"]),
                            KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length>5? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5,3):"000",//.Length
                            AlasanPerubahan = dr["sAlasanPerubahan"] != null ? dr["sAlasanPerubahan"].ToString() : "", //DataFormat.GetString(dr["sAlasanPerubahan"]),
                            SumberDana = DataFormat.GetString(dr["ssumberdana"]),
                                                      ListIndikator = GetIndikator(DataFormat.GetInteger(dr["iTahun"]),
                                                         DataFormat.GetInteger(dr["IDDInas"]),
                                                         DataFormat.GetInteger(dr["IDurusan"]),
                                                         DataFormat.GetInteger(dr["IDProgram"]),
                                                         DataFormat.GetInteger(dr["IDkegiatan"]), _tahap,lst), //,bGabungan
                            ListCatatan = GetCatatanKegiatan(DataFormat.GetInteger(dr["iTahun"]),
                                                        DataFormat.GetInteger(dr["IDDInas"]),
                                                        DataFormat.GetInteger(dr["IDurusan"]),
                                                        DataFormat.GetInteger(dr["IDProgram"]),
                                                        DataFormat.GetInteger(dr["IDkegiatan"]), _pJenis),
                            TampilanKode = DataFormat.GetInteger(dr["IDProgram"]) == 0 ?
                                            DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
                                           "00.000" : DataFormat.GetInteger(dr["IDProgram"]) > 0 ? DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
                                           DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram) + "." +
                                           DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5) : ""
                        };
                        return tKAPBD;
                    }
                    else
                    { // jikatidak ada..

                        TKegiatan oKegiatan = new TKegiatan();
                        TKegiatanLogic oKUALogic = new TKegiatanLogic(Tahun, _profile);
                        if (oKUALogic.ExportToAPBD(pTahun, pIDDinas, pIDUrusan, pIDProgram, pIDKegiatan, (int)_pJenis) == true)
                        {
                            // *********************************************

                            if (pIDProgram == 0)
                            {
                                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                                    " AND IDProgram = " + pIDProgram.ToString() + 
                                    " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();

                            }
                            else
                            {
                                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                                    " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() + 
                                    " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();
                            }
                            _dbHelper.ExecuteNonQuery(SSQL);
                            DataTable dtx = new DataTable();
                            dtx = _dbHelper.ExecuteDataTable(SSQL);

                            if (dtx != null)
                            {
                                if (dtx.Rows.Count > 0)
                                {
                                    DataRow dr = dtx.Rows[0];
                                    tKAPBD = new TKegiatanAPBD()
                                    {
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                        Nama = DataFormat.GetString(dr["sNama"]),
                                        Pagu = DataFormat.GetDecimal(dr["cPlafon"]),
                                        Lokasi = DataFormat.GetString(dr["sLokasi"]),
                                        Kondisi = DataFormat.GetString(dr["sKondisi"]),
                                        Waktu = DataFormat.GetString(dr["sWaktu"]),
                                        TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
                                        Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                        TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                                        AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
                                        AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
                                        KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
                                        AlasanPerubahan = DataFormat.GetString(dr["sAlasanPerubahan"]),
                                        ListIndikator = GetIndikator(DataFormat.GetInteger(dr["iTahun"]),
                                                                     DataFormat.GetInteger(dr["IDDInas"]),
                                                                     DataFormat.GetInteger(dr["IDurusan"]),
                                                                     DataFormat.GetInteger(dr["IDProgram"]),
                                                                     DataFormat.GetInteger(dr["IDkegiatan"]), _tahap),
                                        ListCatatan = GetCatatanKegiatan(DataFormat.GetInteger(dr["iTahun"]),
                                                                    DataFormat.GetInteger(dr["IDDInas"]),
                                                                    DataFormat.GetInteger(dr["IDurusan"]),
                                                                    DataFormat.GetInteger(dr["IDProgram"]),
                                                                    DataFormat.GetInteger(dr["IDkegiatan"]), _pJenis),
                                        TampilanKode = DataFormat.GetInteger(dr["IDProgram"]) == 0 ?
                                            DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDDInas"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
                                           "00.000"
                                            : DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
                                           DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
                                           DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram) + "." +
                                           DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan)
                                    };
                                    return tKAPBD;
                                }
                            }
                            //*********

                        }
                        else
                            return null;

                    }
                }

            }
            catch (Exception ex)
            {

                _isError = true;
                _lastError = ex.Message;

                return null;
            }
            return tKAPBD;

        }
        private string GetSumberDana(int pTahun, int IDDInas, int idUrusan, int idProgram, int IdKegiatan)
        {

            TSumberDanaLogic oLogic = new TSumberDanaLogic(pTahun, _profile);
            List<TSumberDana> _lst = new List<TSumberDana>();
            _lst = oLogic.Get(pTahun, IDDInas, idUrusan, idProgram, IdKegiatan,0 );
            if (_lst == null)
                return "";
            if (_lst.Count == 0)
                return "";

            string sSumberDana;
            sSumberDana = "";

            foreach (TSumberDana s in _lst)
            {

                sSumberDana = sSumberDana + s.Nama;
                sSumberDana = sSumberDana + ", ";

                    
                
            }
            if (sSumberDana.Length > 3)
            {
                return sSumberDana.Substring(0, sSumberDana.Trim().Length - 1);
            }
            else
            {
                return sSumberDana;
            }
        }
        private decimal GetPlafon(int _tahun, int _pIDDInas, int pIDurusan, int pIDProgram, int pIDkegiatan)
        {
            KUALogic oKUALogic = new KUALogic(Tahun,(int)_profile);
            KUA oKUA = new KUA();
            oKUA = oKUALogic.GetByIDKegiatanInduk(_pIDDInas, pIDurusan, pIDProgram, pIDkegiatan, _tahun);
            return oKUA.JumlahOlah;



        }
        //public TKegiatanAPBD GetKegiatanLama( int pTahun, int pIDDinas,int pIDUrusan, int pIDProgram, int pIDKegiatan, Single _pJenis)
        //{
        //    //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

        //    //}
        //    TKegiatanAPBD tKAPBD= new TKegiatanAPBD();
        //    try
        //    {
        //        SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
        //               " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
        //               " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" +_pJenis.ToString();

        //        _dbHelper.ExecuteNonQuery(SSQL);
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);

        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];
        //                tKAPBD = new TKegiatanAPBD()
        //                 {
        //                     Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                     IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
        //                     IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                     IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                     IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
        //                     Nama = DataFormat.GetString(dr["sNama"]),
        //                     Pagu = DataFormat.GetDecimal(dr["cPlafon"]),
        //                     Lokasi = DataFormat.GetString(dr["sLokasi"]),
        //                     Kondisi = DataFormat.GetString(dr["sKondisi"]),
        //                     Waktu = DataFormat.GetString(dr["sWaktu"]),
        //                     TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
        //                     Keterangan = DataFormat.GetString(dr["sKeterangan"]),
        //                     TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
        //                     AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
        //                     AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
        //                     KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
        //                     Jenis = DataFormat.GetSingle(dr["btJenis"]),
        //                     KodeKegiatan = Convert.ToInt32(DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan )),
        //                     KodeProgram = Convert.ToInt32(DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram )),
        //                     KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
        //                     //public int SumberDana  { set; get; }//iSumberDana
        //                     //public int TahunAwal  { set; get; }//iTahunAwal
        //                     //public Single ApakahLanjutan { set; get; }//bLanjutan
        //                     //public string NamaSumberDana  { set; get; }//       sSumberDana
        //                     AlasanPerubahan = DataFormat.GetString(dr["sAlasanPerubahan"]),
        //                     ListIndikator = GetIndikator(DataFormat.GetInteger(dr["iTahun"]),
        //                                                  DataFormat.GetInteger(dr["IDDInas"]),
        //                                                  DataFormat.GetInteger(dr["IDurusan"]),
        //                                                  DataFormat.GetInteger(dr["IDProgram"]),
        //                                                  DataFormat.GetInteger(dr["IDkegiatan"]),1),
        //                     ListCatatan = GetCatatanKegiatan(DataFormat.GetInteger(dr["iTahun"]),
        //                                                 DataFormat.GetInteger(dr["IDDInas"]),
        //                                                 DataFormat.GetInteger(dr["IDurusan"]),
        //                                                 DataFormat.GetInteger(dr["IDProgram"]),
        //                                                 DataFormat.GetInteger(dr["IDkegiatan"]), _pJenis),
        //                     TampilanKode = DataFormat.GetInteger(dr["IDProgram"]) >0 ?DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0, 1) + "." +
        //                DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1, 2) + "." +
        //                DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
        //                DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
        //                DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
        //                DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram) + "." +
        //                DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan):""
        //                 };
        //                return tKAPBD;
        //            }
        //            else
        //            { // jikatidak ada..

        //                TKegiatan oKegiatan = new TKegiatan();
        //                TKegiatanLogic oKUALogic = new TKegiatanLogic(Tahun, _profile);
        //                if (oKUALogic.ExportToAPBD(pTahun, pIDDinas, pIDUrusan, pIDProgram, pIDKegiatan, (int)_pJenis) == true)
        //                {
        //                    // *********************************************
        //                    SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
        //                        " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() +
        //                        " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" +_pJenis.ToString();

        //                        _dbHelper.ExecuteNonQuery(SSQL);
        //                        DataTable dtx = new DataTable();
        //                        dtx = _dbHelper.ExecuteDataTable(SSQL);

        //                        if (dtx != null)
        //                        {
        //                            if (dtx.Rows.Count > 0)
        //                            {
        //                                DataRow dr = dtx.Rows[0];
        //                                tKAPBD = new TKegiatanAPBD()
        //                                {
        //                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
        //                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
        //                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
        //                                    Nama = DataFormat.GetString(dr["sNama"]),
        //                                    Pagu = DataFormat.GetDecimal(dr["cPlafon"]),
        //                                    Lokasi = DataFormat.GetString(dr["sLokasi"]),
        //                                    Kondisi = DataFormat.GetString(dr["sKondisi"]),
        //                                    Waktu = DataFormat.GetString(dr["sWaktu"]),
        //                                    TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
        //                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
        //                                    TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
        //                                    AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
        //                                    AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
        //                                    KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),

        //                                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
        //                                    AlasanPerubahan = DataFormat.GetString(dr["sAlasanPerubahan"]),
        //                                    ListIndikator = GetIndikator(DataFormat.GetInteger(dr["iTahun"]),
        //                                                                 DataFormat.GetInteger(dr["IDDInas"]),
        //                                                                 DataFormat.GetInteger(dr["IDurusan"]),
        //                                                                 DataFormat.GetInteger(dr["IDProgram"]),
        //                                                                 DataFormat.GetInteger(dr["IDkegiatan"]),1),
        //                                    ListCatatan = GetCatatanKegiatan(DataFormat.GetInteger(dr["iTahun"]),
        //                                                                DataFormat.GetInteger(dr["IDDInas"]),
        //                                                                DataFormat.GetInteger(dr["IDurusan"]),
        //                                                                DataFormat.GetInteger(dr["IDProgram"]),
        //                                                                DataFormat.GetInteger(dr["IDkegiatan"]), _pJenis),
        //                                    TampilanKode = DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(0, 1) + "." +
        //                                       DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(1, 2) + "." +
        //                                       DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(0, 1) + "." +
        //                                       DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(1, 2) + "." +
        //                                       DataFormat.GetInteger(dr["IDDinas"]).ToString().Substring(3, 2) + "." +
        //                                       DataFormat.GetInteger(dr["IDProgram"]).ToString().Substring(3, m_ProfileProgKegiatan.KodeProgram ) + "." +
        //                                       DataFormat.GetInteger(dr["IDKegiatan"]).ToString().Substring(5, m_ProfileProgKegiatan.KodeKegiatan)
        //                                };
        //                                return tKAPBD;
        //                            }
        //                    }
        //                    //*********

        //                }
        //                else
        //                    return null;

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        _isError = true;
        //        _lastError = ex.Message;

        //        return null;
        //    }
        //    return tKAPBD;

        //}
        public bool CekAda(TKegiatanAPBD k)
        {
            if (CekAda(Tahun, k.IDDinas, k.IDUrusan, k.IDProgram, k.IDKegiatan, (int)k.Jenis) > 0)
                return true;
            else
                return false;

        }
        private int CekAda(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, long  pIDKegiatan, int _pJenis, int idUnit=0)
        {
            
            TKegiatanAPBD tKAPBD = new TKegiatanAPBD();
            int jumlahRecord = 0;
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTAhun =2025  AND IDDInas =" + pIDDinas.ToString() +
                       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() + 
                       " and idunit = "+ idUnit.ToString () + " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString() ;

                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        jumlahRecord= dt.Rows.Count;
                    }                    
                    
                }
                //if (jumlahRecord > 1)
                //{
                //    SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE iTAhun =" + pTahun.ToString() + " AND IDDInas =" + pIDDinas.ToString() +
                //       " AND IDUrusan =" + pIDUrusan.ToString() + " AND IDProgram = " + pIDProgram.ToString() + 
                //       " AND IDKegiatan=" + pIDKegiatan.ToString() + " AND btJenis=" + _pJenis.ToString();

                //    _dbHelper.ExecuteNonQuery(SSQL);
                //    jumlahRecord = 0;
                //}

                return jumlahRecord;
            }
            catch (Exception ex)
            {

                _isError = true;
                _lastError = ex.Message;

                return 0;
            }
            

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


                    SSQL = "SELECT IDDInas, idurusan,IDProgram,IDKEGIATAN,sNama, SUM(Jumlah) as Jumlah from ( ";
                    SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram,A.IDKEGIATAN, A.sNama, SUM(C.cJumlah) as Jumlah FROM tKegiatan_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
                         " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram And a.idkegiatan=b.idkegiatan  INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
                          " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                          " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
                          " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama " +
                          " UNION ";
                    SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama, " +
                         " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tKegiatan_A A  INNER JOIN tKoreksi B " +
                         "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
                         " And a.idkegiatan=C.idkegiatan  AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
                          " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
                          " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.IDKEGIATAN,A.sNama";
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


        public List<TKegiatanAPBD> GetKegiatanByProgram(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, List<SKPD>lst = null)
        {


            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                if (lst ==null)
                SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A " + // LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    //SSQL = "SELECT distinct A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, 0 as JumlahOlah, A.btJenis  " +
                    //" FROM " + m_sNamaTabel + " A  " +//LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    //" WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    //" AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    //" GROUP BY A.iTahun,  A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    //" ORDER BY A.IDKegiatan ";

                    SSQL = "SELECT distinct A.iTahun, "+ pIDDinas.ToString() + " as IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, 0 as JumlahOlah, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A  " +//LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    " GROUP BY A.iTahun,   A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

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
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama"]),
                                    JumlahDiInput = "0",//DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = "0",//DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode= DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = (DataFormat.GetInteger(dr["IDkegiatan"]) % 100).ToString(),//.Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
                                }).ToList() ;
                        
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
        public List<TKegiatanAPBD> Get(int pTahun)
        {


            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,A.btJenis, btKodeuk  " +
                    " FROM " + m_sNamaTabel + " A " + 
                    " WHERE A.iTAhun =" + pTahun.ToString() +
                    //" GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    " ORDER BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram,A.IDKegiatan ";

                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatanAPBD()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDUnit =DataFormat.GetInteger(dr["IDDInas"])+DataFormat.GetInteger(dr["btKodeUK"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),

                                    KodePendek = (DataFormat.GetInteger(dr["IDkegiatan"]) % 100).ToString(),//.Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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

        public List<TKegiatanAPBD> GetKegiatanByDInasUkProgram(int pTahun, int pIDDinas,int KodeUK, int pIDProgram)
        {


            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {

                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,A.btJenis  " +
                        " FROM tKegiatan_A A " + // LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =@TAHUN  AND A.IDDInas =@DINAS" +
                        " AND A.IDProgram = @IDPROGRAM " +
                        " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                        " ORDER BY A.IDKegiatan ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@TAHUN", pTahun));
              
                    paramCollection.Add(new DBParameter("@DINAS", pIDDinas));
                    paramCollection.Add(new DBParameter("@IDPROGRAM", pIDProgram));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatanAPBD()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama"]),
                                    JumlahDiInput = "0",//DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = "0",//DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = (DataFormat.GetInteger(dr["IDkegiatan"]) % 100).ToString(),//.Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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
        public List<TKegiatanAPBD> GetKegiatanByProgramEx(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, int kodeuk,List<SKPD> lst = null)
        {

            //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

            //}

            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                if (lst == null)
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,A.btJenis  " +
                        " FROM " + m_sNamaTabel + " A " + // LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                        " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        " AND A.btKOdeuk= " + kodeuk.ToString() + " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                        " ORDER BY A.IDKegiatan ";

                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    //SSQL = "SELECT distinct A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, 0 as JumlahOlah, A.btJenis  " +
                    //" FROM " + m_sNamaTabel + " A  " +//LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    //" WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    //" AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    //" GROUP BY A.iTahun,  A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    //" ORDER BY A.IDKegiatan ";
                    if (pIDDinas == 1020100)
                    {
                        SSQL = "SELECT distinct A.iTahun, " + pIDDinas.ToString() + " as IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, 0 as JumlahOlah, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A  " +//LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND btKodeuk = " + kodeuk.ToString() + " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    " GROUP BY A.iTahun,   A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

                    }
                    else
                    {
                        SSQL = "SELECT distinct A.iTahun, " + pIDDinas.ToString() + " as IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, 0 as JumlahOlah, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A  " +//LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND btKodeuk = " + kodeuk.ToString() + " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    " GROUP BY A.iTahun,   A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

                    }
                    
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
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama"]),
                                    JumlahDiInput = "0",//DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = "0",//DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = (DataFormat.GetInteger(dr["IDkegiatan"]) % 100).ToString(),//.Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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
        private List<Indikator> GetIndikator(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, int _tahap, List<SKPD>lst = null)
        {
            IndikatorLogic oLogic = new IndikatorLogic(Tahun,_profile);
            return oLogic.Get(pTahun, pIDDInas, pIDUrusan, pIDProgram, pIDkegiatan, _tahap, lst);

        }
         private List<CatatanKegiatan> GetCatatanKegiatan(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan,Single pJenis)
        {
            CatatanKegiatanLogic oLogic = new CatatanKegiatanLogic(Tahun,_profile);
            return oLogic.Get(pTahun, pIDDInas, pIDUrusan, pIDProgram, pIDkegiatan, pJenis);
        }
        public bool CopyRKA(TKegiatanAPBD oSumber, TKegiatanAPBD oTujuan)
        {
            string _nameTableRekening;
            string _nameTableUraian;
            string _nameIndikator;

            _nameTableRekening="tempRekening" + oSumber.IDKegiatan.ToString();
            _nameTableUraian="tempUraian" + oSumber.IDKegiatan.ToString();
            _nameIndikator="tempIndikator" + oSumber.IDKegiatan.ToString();
            try
            {


                SSQL = "Select * into " + _nameTableRekening + " from tAnggaranRekening_A WHERE iTahun =" + oSumber.Tahun.ToString() +
                    " AND IDDINAS=" + oSumber.IDDinas.ToString() + " AND IDUrusan =" + oSumber.IDUrusan + " AND IDProgram =" + oSumber.IDProgram.ToString() +
                    " AND IDKegiatan =" + oSumber.IDKegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "Select * into " + _nameTableUraian + " from tAnggaranUraian_A WHERE iTahun =" + oSumber.Tahun.ToString() +
                    " AND IDDINAS=" + oSumber.IDDinas.ToString() + " AND IDUrusan =" + oSumber.IDUrusan + " AND IDProgram =" + oSumber.IDProgram.ToString() +
                    " AND IDKegiatan =" + oSumber.IDKegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE " + _nameTableRekening + " SET iTahun =" + oTujuan.Tahun.ToString() +
                    " , IDDINAS=" + oTujuan.IDDinas.ToString() + " ,IDUrusan =" + oTujuan.IDUrusan + " ,IDProgram =" + oTujuan.IDProgram.ToString() +
                    " ,IDKegiatan =" + oTujuan.IDKegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE " + _nameTableUraian + " SET iTahun =" + oTujuan.Tahun.ToString() +
                    " ,IDDINAS=" + oTujuan.IDDinas.ToString() + " ,IDUrusan =" + oTujuan.IDUrusan + " ,IDProgram =" + oTujuan.IDProgram.ToString() +
                    " ,IDKegiatan =" + oTujuan.IDKegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "INSERT INTO tAnggaranRekening_A  SELECT * FROM " + _nameTableRekening;
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO tAnggaranUraian_A SELECT * FROM " + _nameTableUraian;

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL="DROP TABLE "+ _nameTableRekening;
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "DROP TABLE " + _nameTableUraian;
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
        public bool BersihkanDouble(int _tahun)
        {
            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                SSQL ="SELECT * INTO tempKegiatanSEP from tKegiatan_A where iTahun = " + _tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "SELECT  iTahun,IDUrusan,IDDinas, IDProgram, IDKegiatan , btJenis,count(*) FROM tempKegiatanSEP WHERE iTAhun =" + _tahun.ToString() +
                        " GROUP BY iTahun,IDUrusan,IDDinas, IDProgram, IDKegiatan,btJenis HAVING count(*)>1";
                        


                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new TKegiatanAPBD()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
                                }).ToList();
                        

                    }

                }
                foreach (TKegiatanAPBD t in _lst)
                {
                    TKegiatanAPBD o = new TKegiatanAPBD();
                    o = GetTempKegiatan((int)t.Tahun, t.IDDinas, t.IDUrusan, t.IDProgram, t.IDKegiatan, (int)t.Jenis);
                    SSQL = "DELETE tKegiatan_A WHERE iTAhun =" + _tahun.ToString() + " AND IDDInas =" + t.IDDinas.ToString() +
                       " AND IDUrusan =" + t.IDUrusan.ToString() + " AND IDProgram = " + t.IDProgram.ToString() + 
                       " AND IDKegiatan=" + t.IDKegiatan.ToString() + " AND btJenis=" + t.Jenis.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);
                    Simpan2(o);
                }
                SSQL = "DROP TABLE tempKegiatanSEP";
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }

        }
        public TKegiatanAPBD  GetTempKegiatan(int _tahun, int _iddinas, int _idUrusan, int _idProgram, int _IDKegiatan , int _jenis)
        {
            TKegiatanAPBD tKAPBD= new TKegiatanAPBD();
            try
            {
                SSQL = "SELECT * FROM tempKegiatanSEP WHERE iTAhun =" + _tahun.ToString() + " AND IDDInas =" + _iddinas.ToString() +
                       " AND IDUrusan =" + _idUrusan.ToString() + " AND IDProgram = " + _idProgram.ToString() +
                       " AND IDKegiatan=" + _IDKegiatan.ToString() + " AND btJenis=" +_jenis.ToString() + " ORDER By cPlafon DESC ";

                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        tKAPBD = new TKegiatanAPBD()
                         {
                             Tahun = DataFormat.GetInteger(dr["iTahun"]),
                             IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                             IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                             IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                             IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                             Nama = DataFormat.GetString(dr["sNama"]),
                             Pagu = DataFormat.GetDecimal(dr["cPlafon"]),
                             Lokasi = DataFormat.GetString(dr["sLokasi"]),
                             Kondisi = DataFormat.GetString(dr["sKondisi"]),
                             Waktu = DataFormat.GetString(dr["sWaktu"]),
                             TanggalPembahasan = DataFormat.GetDateTime(dr["dtPembahasan"]),
                             Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                             TahapInput = DataFormat.GetInteger(dr["btTahapInput"]),
                             AnggaranTahunDepan = DataFormat.GetDecimal(dr["cAnggaranTahunDepan"]),
                             AnggaranTahunLalu = DataFormat.GetDecimal(dr["cAnggaranTahunLalu"]),
                             KelompokSasaran = DataFormat.GetString(dr["sKelompokSasaran"]),
                             Jenis = DataFormat.GetSingle(dr["btJenis"]),
                             KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 5 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
                             
                         };
                        return tKAPBD;
                        
                    }
                    
                }

            }
            catch (Exception ex)            {

                _isError = true;
                _lastError = ex.Message;
                return null;
            }
            return tKAPBD;        
        }

        public void BersihakDoubleKegiatan()
        {

        }
        public List<TKegiatanAPBD> GetKegiatanByProgramAndSPJ(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, long nourutSPJ, List<SKPD> lst = null)
        {

            //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

            //}

            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                if (lst == null)
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, SUM(B.cJumlahOlah) as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                        " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                        " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        "  AND A.IDKegiatan in (SELECT IDKegiatan from tSPJRekening where inourut = " + nourutSPJ.ToString() + ")" +
                        " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                        " ORDER BY A.IDKegiatan ";

                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    SSQL = "SELECT distinct A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, 0 as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                   "  AND A.IDKegiatan in (SELECT IDKegiatan from tSPJRekening where inourut = " + nourutSPJ.ToString() + ")" +
                         " GROUP BY A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

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
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    //   IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama2"]),
                                    JumlahDiInput = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 2 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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
        public List<TKegiatanAPBD> GetKegiatanByProgramAndBAST(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, long nourutBAST, List<SKPD> lst = null)
        {

            //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

            //}

            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                if (lst == null)
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, SUM(B.cJumlahOlah) as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                        " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                        " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        "  AND A.IDKegiatan in (SELECT IDKegiatan from tBAST where inourut = " + nourutBAST.ToString() + ")" +
                        " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                        " ORDER BY A.IDKegiatan ";

                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    SSQL = "SELECT distinct A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, 0 as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                   "  AND A.IDKegiatan in (SELECT IDKegiatan from tBAST where inourut = " + nourutBAST.ToString() + ")" +
                   " GROUP BY A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

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
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    //   IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama2"]),
                                    JumlahDiInput = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 2 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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
        public List<TKegiatanAPBD> GetKegiatanByProgramAndSPD(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, long nourutSPD, List<SKPD> lst = null)
        {

            //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

            //}

            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                if (lst == null)
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, SUM(B.cJumlahOlah) as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                        " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                        " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        "  AND A.IDKegiatan in (SELECT IDKegiatan from tSPDKegiatan where inourut <= " + nourutSPD.ToString() + ")" +
                        " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                        " ORDER BY A.IDKegiatan ";

                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    SSQL = "SELECT distinct A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, 0 as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    "  AND A.IDKegiatan in (SELECT IDKegiatan from tSPDKegiatan where inourut <= " + nourutSPD.ToString() + ")" +
                    " GROUP BY A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

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
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    //   IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama2"]),
                                    JumlahDiInput = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 2 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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
        public List<TKegiatanAPBD> GetKegiatanByProgramAndSP2D(int pTahun, int pIDDinas, int pIDUrusan, int pIDProgram, long nourutSP2D, List<SKPD> lst = null)
        {

            //if (CekKUAdanKegiatan(pTahun, pIDDinas )==false ){

            //}

            List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
            try
            {
                if (lst == null)
                    SSQL = "SELECT A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, SUM(B.cJumlahOlah) as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                        " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                        " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas =" + pIDDinas.ToString() +
                        " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                        "  AND A.IDKegiatan in (SELECT IDKegiatan from tSPPRekening where inourut = " + nourutSP2D.ToString() + ")" +
                        " GROUP BY A.iTahun, A.IDDInas, A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                        " ORDER BY A.IDKegiatan ";

                else
                {
                    string strDinas = "(";
                    foreach (SKPD s in lst)
                    {
                        strDinas = strDinas + s.ID.ToString() + ",";

                    }
                    strDinas = strDinas + "99)";
                    SSQL = "SELECT distinct A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2, 0 as JumlahOlah, SUM(B.cPlafon) as JumlahPlafon, A.btJenis  " +
                    " FROM " + m_sNamaTabel + " A LEFT OUTER JOIN tANGGaranRekening_A B ON A.iTahun = B.iTahun AND A.IDDINas = B.IDDINas  and A.idUrusan = B.IDurusan AND A.IDProgram = B.IDProgram and A.IDKegiatan =B.IDKegiatan " +
                    " WHERE A.iTAhun =" + pTahun.ToString() + " AND A.IDDInas in " + strDinas +
                    " AND A.IDUrusan =" + pIDUrusan.ToString() + " AND A.IDProgram = " + pIDProgram.ToString() +
                    "  AND A.IDKegiatan in (SELECT IDKegiatan from tSPPRekening where inourut = " + nourutSP2D.ToString() + ")" +

                    " GROUP BY A.iTahun,  A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama, A.sNama2,A.btJenis  " +
                    " ORDER BY A.IDKegiatan ";

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
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    //   IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Nama2 = DataFormat.GetString(dr["sNama2"]),
                                    JumlahDiInput = DataFormat.GetDecimal(dr["JumlahOlah"]).ToRupiahInReport(),
                                    JumlahPagu = DataFormat.GetDecimal(dr["JumlahPlafon"]).ToRupiahInReport(),
                                    TampilanKode = DataFormat.GetInteger(dr["IDkegiatan"]).ToKodeKegiatan(m_ProfileProgKegiatan),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    KodePendek = DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Length > 2 ? DataFormat.GetInteger(dr["IDkegiatan"]).ToString().Substring(5, 3) : "000",//.Length
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
        public List<RekeningDetail> GetRekeningBasedSPD(long IDKegiatan, int IDDInas, DateTime dBatas)
        {

            List<RekeningDetail> lret = new List<RekeningDetail>();

            try
            {


                SSQL = "SELECT mRekening.iIDRekening, mRekening.sNamaRekening, sum(B.cJumlah)as cJumlah " +
                   " FROM tSPD A INNER JOIN tSPDKegiatan B ON A.inourut =B.inourut INNER JOIN mRekening ON" +
                   " B.iIDRekening = mRekening.iIDRekening" +
                   " WHERE A.iTahun = " + Tahun.ToString() +
                   " AND A.IDDInas  = " + IDDInas.ToString() +
                   " AND B.IDKegiatan = " + IDKegiatan.ToString() +
                   " AND A.iStatus = 1  AND A.dtSPD <=" + dBatas.ToSQLFormat();


                SSQL = SSQL + " GROUP BY mRekening.iIDRekening, mRekening.sNamaRekening";
                SSQL = SSQL + "  ORDER BY mRekening.iIDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lret = (from DataRow dr in dt.Rows
                                select new RekeningDetail()
                                {

                                    IDRekening = DataFormat.GetLong(dr["iIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                    Nilai = DataFormat.GetDecimal(dr["cJumlah"])

                                }).ToList();

                    }

                }
                return lret;


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }

        }
    }
}

