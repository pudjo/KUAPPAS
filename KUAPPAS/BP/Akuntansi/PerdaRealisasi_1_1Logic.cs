using DTO.Akuntansi;
using DTO;
using KUAPPAS.DataAccess9.DTO.Akuntansi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using KUAPPAS;
using NPOI.SS.Formula.Functions;
using Syncfusion.Pdf.Grid;

namespace BP.Akuntansi
{
    public class PerdaRealisasi_1_1Logic:BP
    {
        List<Rekening> m_lstRekening;
        PerdaRealisasi_1 SurplusDefisit = new PerdaRealisasi_1();
        public PerdaRealisasi_1_1Logic(int tahun): base(tahun)
        {
            m_lstRekening = new List<Rekening>();
        }

        private List<AnggaranRealisasi> GetAnggaranRealisasiPendapatan(int tahun)
        {
            try
            {
                List<AnggaranRealisasi> lstPendapatan = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);

                lstPendapatan = ologic.GetAnggaranRealisasiPendapatan(tahun);
                if (lstPendapatan == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }

                
                return lstPendapatan;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }
        private List<AnggaranRealisasi> GetAnggaranRealisasiPembiayaanPenerimaan(int tahun)
        {
            try
            {
                List<AnggaranRealisasi> lstPenerimaanPembuayaan = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);

                lstPenerimaanPembuayaan = ologic.GetPenerimaanPembiayaan(tahun);
                if (lstPenerimaanPembuayaan == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }


                return lstPenerimaanPembuayaan;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }
        private List<AnggaranRealisasi> GetAnggaranRealisasiPembiayaanPengeluaran(int tahun)
        {
            try
            {
                List<AnggaranRealisasi> lstPenerimaanPengeluaran = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);

                lstPenerimaanPengeluaran = ologic.GetPengeluaranPembiayaan(tahun);
                if (lstPenerimaanPengeluaran == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }


                return lstPenerimaanPengeluaran;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }


        private List<AnggaranRealisasi> GetAnggaranRealisasi(int tahun)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);


                lst = ologic.GetAnggaranRealisasi(tahun);
                if (lst== null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }
                return lst;
            } catch(Exception ex){
                _lastError= ex.Message;

                return null;


            }
        }
        private string Persen(decimal a, decimal b)
        {
            if (b > 0)
            {
                return (((a ) / b) * 100).ToString("#.##") + " %";
            }
            else
            {
                return "100 % ";
            }
        }
        private string PersenBelanja(decimal a, decimal b)
        {
            if (a > 0)
            {
                //return prJudulPendapatan.Persen = (((a - b) / a) * 100).ToString("#.##") + " %";
                return (((b) / a) * 100).ToString("#.##") + " %";
            }
            else
            {
                return "100 % ";
            }
        }
        public List<PerdaRealisasi_1> Process()
        {

            try
            {
                List<PerdaRealisasi_1> lstPerda = new List<PerdaRealisasi_1>();
                List<AnggaranRealisasi> lstPendapatan = GetAnggaranRealisasiPendapatan(Tahun);
                PerdaRealisasi_1 prJudul = new PerdaRealisasi_1
                {
                    Anggaran = lstPendapatan.Sum(x => x.Anggaran),
                    Realisasi = lstPendapatan.Sum(x => x.Realisasi),
                    KodeRekening = "",
                    KodeSKPD = "",
                    KodeUrusan = "",
                    Kode = "",
                    Bold = 1,
                    Nama = "PENDAPATAN",
                    
                };
                prJudul.Selisih = prJudul.Realisasi - prJudul.Anggaran;
                prJudul.Persen = Persen(prJudul.Realisasi, prJudul.Anggaran);
                SurplusDefisit.Anggaran = prJudul.Anggaran;
                SurplusDefisit.Realisasi = prJudul.Realisasi;
                lstPerda.Add(prJudul);
                List<PerdaRealisasi_1> lstPerdaPerKategoriPendapatan = new List<PerdaRealisasi_1>();
                List<PerdaRealisasi_1> lstPerdaPerUrusanPendapatan = new List<PerdaRealisasi_1>();
                List<PerdaRealisasi_1> lstPerdaPerSKPDPendapatan = new List<PerdaRealisasi_1>();

                lstPerdaPerKategoriPendapatan = ProcessPerKategori(lstPendapatan);
                lstPerdaPerUrusanPendapatan = ProcessPerUrusan(lstPendapatan);
                lstPerdaPerSKPDPendapatan = ProcessPerSKPD(lstPendapatan);



                #region Pendapatan
                foreach (PerdaRealisasi_1 k in lstPerdaPerKategoriPendapatan)
                {
                    k.KodeSKPD = "";
                    k.Bold = 1;

                    k.Selisih = k.Realisasi - k.Anggaran;
                    k.Persen = Persen(k.Realisasi, k.Anggaran);
                    lstPerda.Add(k);
                    foreach (PerdaRealisasi_1 u in lstPerdaPerUrusanPendapatan)
                    {
                        if (u.KodeUrusan.Length > 2)
                        {
                            if (k.Kode == u.KodeUrusan.Substring(0, 1))
                            {
                                u.Kode = "";
                                u.KodeSKPD = "";
                                u.Nama = u.Nama.ToUpper();
                                u.Bold = 1;
                                u.Selisih = u.Realisasi - u.Anggaran;
                                u.Persen = Persen(u.Realisasi, u.Anggaran);
                                lstPerda.Add(u);
                                foreach (PerdaRealisasi_1 pu in lstPerdaPerSKPDPendapatan)
                                {

                                    if (u.KodeUrusan == pu.KodeUrusan)
                                    {
                                        string s = pu.KodeUrusan;// simpan mau dipakai nanti

                                        pu.Kode = "";
                                        pu.KodeUrusan = "";
                                        pu.KodeSKPD = pu.KodeSKPD;
                                        pu.Bold = 1;
                                        pu.Selisih = pu.Realisasi - pu.Anggaran;
                                        pu.Persen = Persen(pu.Realisasi, pu.Anggaran);
                                        Console.WriteLine(pu.IdDinas);
                                        lstPerda.Add(pu);
                                    }
                                }
                            }
                        }
                    }
                }

                                                #endregion




                                                #region Belanja


                                                List<PerdaRealisasi_1> lstPerdaPerKategori = new List<PerdaRealisasi_1>();
            List<PerdaRealisasi_1> lstPerdaPerUrusan = new List<PerdaRealisasi_1>();
            List<PerdaRealisasi_1> lstPerdaPerSKPD = new List<PerdaRealisasi_1>();

            List<PerdaRealisasi_1> lstPerdaKelompokRekening = new List<PerdaRealisasi_1>();
            List<PerdaRealisasi_1> lstPerdaJenisRekening = new List<PerdaRealisasi_1>();

                List<AnggaranRealisasi> lst = GetAnggaranRealisasi(Tahun);
                prJudul = new PerdaRealisasi_1
                {
                    Anggaran = lst.Sum(x => x.Anggaran),
                    Realisasi = lst.Sum(x => x.Realisasi),
                    KodeRekening = "",
                    KodeSKPD = "",
                    KodeUrusan = "",
                    Kode = "",
                    Bold = 1,
                    Nama = "BELANJA",
                };

                prJudul.Selisih = prJudul.Anggaran - prJudul.Realisasi;
                prJudul.Persen = PersenBelanja(prJudul.Anggaran, prJudul.Realisasi);

                SurplusDefisit.Kode="";
                SurplusDefisit.KodeUrusan = "";
                SurplusDefisit.KodeSKPD = "";
                

                SurplusDefisit.Anggaran = SurplusDefisit.Anggaran- prJudul.Anggaran;
                SurplusDefisit.Realisasi = SurplusDefisit.Realisasi - prJudul.Realisasi;
                SurplusDefisit.Selisih = 0;
                lstPerda.Add(prJudul);

                if (LoadRekening() == false)
                {
                    _lastError = "Gagal memanggil Rekening";
                    return null;
                }
                
                if (lst != null)
                {
                    lstPerdaPerKategori = ProcessPerKategori(lst);
                    lstPerdaPerUrusan = ProcessPerUrusan(lst);
                    lstPerdaPerSKPD = ProcessPerSKPD(lst);
                    lstPerdaKelompokRekening = ProcessKelompokBelanja(lst, 2);
                    lstPerdaJenisRekening = ProcessKelompokBelanja(lst, 3);
                    foreach (PerdaRealisasi_1 k in lstPerdaPerKategori)
                    {
                        k.KodeSKPD = "";
                        k.Bold = 1;
                        k.Selisih = k.Anggaran - k.Realisasi;
                        k.Persen = PersenBelanja(k.Anggaran, k.Realisasi);

                        lstPerda.Add(k);
                        foreach (PerdaRealisasi_1 u in lstPerdaPerUrusan)
                        {
                            if (u.KodeUrusan.Length > 2) {
                                if (k.Kode == u.KodeUrusan.Substring(0, 1))
                                {
                                    u.Kode = "";
                                    u.KodeSKPD = "";
                                    u.Nama = u.Nama.ToUpper();
                                    u.Bold = 1;
                                    u.Selisih = u.Anggaran - u.Realisasi;
                                    u.Persen = PersenBelanja(u.Anggaran, u.Realisasi);

                                    lstPerda.Add(u);
                                    foreach (PerdaRealisasi_1 pu in lstPerdaPerSKPD)
                                    {

                                        if (u.KodeUrusan == pu.KodeUrusan )
                                        {
                                            string s = pu.KodeUrusan;// simpan mau dipakai nanti

                                            pu.Kode = "";
                                            pu.KodeUrusan = "";
                                            pu.KodeSKPD = pu.KodeSKPD;
                                            pu.Bold = 1;
                                            pu.Selisih = pu.Anggaran - pu.Realisasi;
                                            pu.Persen = PersenBelanja(pu.Anggaran, pu.Realisasi);

                                            Console.WriteLine(pu.IdDinas);
                                            lstPerda.Add(pu);
                                            foreach (PerdaRealisasi_1 kr in lstPerdaKelompokRekening)
                                            {

                                                if (kr.KodeUrusan == s & pu.IDDinas==kr.IDDinas)
                                                {
                                                    kr.Kode = "";
                                                    kr.KodeUrusan = "";
                                                    kr.KodeSKPD = "";
                                                    kr.Bold = 1;
                                                    kr.Selisih = kr.Anggaran - kr.Realisasi;
                                                    kr.Persen = PersenBelanja(kr.Anggaran, kr.Realisasi);

                                                    lstPerda.Add(kr);
                                                    
                                                    foreach (PerdaRealisasi_1 jr in lstPerdaJenisRekening)
                                                    {

                                                        if (jr.KodeUrusan == s & kr.IDDinas == jr.IDDinas && 
                                                            kr.KodeRekening.Substring(0,2) == jr.KodeRekening.Substring(0, 2))
                                                        {
                                                            jr.Kode = "";
                                                            jr.KodeUrusan = "";
                                                            jr.KodeSKPD = "";
                                                            jr.Selisih = prJudul.Anggaran - jr.Realisasi;
                                                            jr.Persen = PersenBelanja(jr.Anggaran, jr.Realisasi);

                                                            lstPerda.Add(jr);
                                                           
        

                                                }


                                                    }

                                                }


                                            }


                                        }


                                    }
                                }
                            }


                        }


                    }

                    //roses per katagori
                }
                else {
                    

                }
                SurplusDefisit.Nama = "(SURPLUS/DEFISIT)";
                lstPerda.Add(SurplusDefisit);
                return lstPerda;
            } catch(Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }


        #region Belanja
        private List<PerdaRealisasi_1> ProcessPerKategori(List<AnggaranRealisasi> lst)
        {
            try
            {
                List<Kategori> lstKategori;
                KategoriLogic kategriLogic = new KategoriLogic(Tahun);
                lstKategori = kategriLogic.Get();
                if (lstKategori == null)
                {
                    return null;
                }
                var lstJumlah = lst
                      .GroupBy(c => c.KodeKategori)

                .Select(x => new
                {
                    Kode= x.Key,
                    Anggaran=x.Sum( y=> y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),

                }).ToList();
                List<PerdaRealisasi_1> lstKi = new List<PerdaRealisasi_1>();


                lstKi = (from t in
                          lstKategori
                         join j in lstJumlah
                         on t.ID equals j.Kode
                         select new PerdaRealisasi_1
                         {
                             Kode= j.Kode.ToString(),
                             KodeUrusan="",
                             KodeSKPD="",
                             Nama= t.Nama,
                             Anggaran= j.Anggaran,
                             Realisasi= j.Realisasi,

                             
                         }).ToList<PerdaRealisasi_1>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private List<PerdaRealisasi_1> ProcessPerUrusan(List<AnggaranRealisasi> lst)
        {


            try
            {



                List<Urusan> lstUrusan;
                UrusanLogic kategriLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
                lstUrusan = kategriLogic.Get();
                if (lstUrusan == null)
                {
                    return null;
                }
                var lstJumlah = lst
                      .GroupBy(c => c.IDUrusan)

                .Select(x => new
                {
                    Kode = x.Key,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),

                }).ToList();
                List<PerdaRealisasi_1> lstKi = new List<PerdaRealisasi_1>();


                lstKi = (from t in
                          lstUrusan
                         join j in lstJumlah
                         on t.ID equals j.Kode

                         select new PerdaRealisasi_1
                         {
                             Kode = "",
                             KodeUrusan = j.Kode.ToString(),
                             KodeSKPD = "",
                             Nama = t.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1> ProcessPerSKPD(List<AnggaranRealisasi> lst)
        {
            try
            {
            List<PelaksanaUrusan> lstPelaksanaUrusan;
                PelaksanaUrusanLogic pelaksanaUrusanLogic = new PelaksanaUrusanLogic(GlobalVar.TahunAnggaran);
                lstPelaksanaUrusan = pelaksanaUrusanLogic.GetWithNamaDinas(Tahun);
                if (lstPelaksanaUrusan == null)
                {
                    return null;
                }
                var lstJumlah = lst
                      .GroupBy(c => new { c.IDUrusan, c.IDDinas  } )

                .Select(x => new
                {
                    KodeUrusan = x.Key.IDUrusan,
                    KodeDinas = x.Key.IDDinas,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),

                }).ToList();
                List<PerdaRealisasi_1> lstKi = new List<PerdaRealisasi_1>();


                lstKi = (from p in
                          lstPelaksanaUrusan
                         join j in lstJumlah
                         //on p.Dinas equals j.KodeDinas && p
                         on new { A = p.Dinas, B = p.Urusan}
                         equals new { A = j.KodeDinas, B = j.KodeUrusan}

                         select new PerdaRealisasi_1
                         {
                             Kode = "",
                             KodeSKPD = p.KodeDinas,
                             IDDinas=p.Dinas,
                             KodeUrusan= p.Urusan.ToString(),
                             Nama = p.NamaDinas,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        #region KelompokBelanja
        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                m_lstRekening = oRekeningLogic.Get().Where(r => r.ID >= 500000000000 && r.ID < 600000000000 && r.Root <= 3).ToList();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }

        private List<PerdaRealisasi_1> ProcessKelompokBelanja(List<AnggaranRealisasi> lst,int level)
        {
            try
            {

                List<Rekening> lstRekening;

                lstRekening = m_lstRekening.FindAll(x => x.Root == level && x.ID < 520000000000);
                int lenKode = 12;
                switch (level)
                {
                    case 1:
                        lenKode = 1;
                        break;
                    case 2:
                        lenKode = 2;
                        break;
                    case 3:
                        lenKode = 4;
                        break;
                    case 4:
                        lenKode = 6;
                        break;
                    case 5:
                        lenKode = 8;
                        break;
                }

                var lstJumlah = lst
                      .GroupBy(c => new { c.IDUrusan, c.IDDinas, kode=c.IDRekening.ToString().Substring(0, lenKode ) })

                .Select(x => new
                {
                    KodeUrusan = x.Key.IDUrusan,
                    KodeDinas = x.Key.IDDinas,
                    KodeRek = x.Key.kode,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),

                }).ToList();
                List<PerdaRealisasi_1> lstKi = new List<PerdaRealisasi_1>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         on p.ID.ToString().Substring(0, lenKode) equals j.KodeRek

                         select new PerdaRealisasi_1
                         {
                             Kode = "",
                             KodeSKPD = "",
                             IDDinas = j.KodeDinas,
                             KodeRekening=p.ID.ToString(),
                             KodeUrusan = j.KodeUrusan.ToString(),
                             Nama = p.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        #endregion
        #endregion
        #endregion
    }
}
