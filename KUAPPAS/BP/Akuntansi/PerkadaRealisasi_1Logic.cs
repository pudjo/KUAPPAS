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
using Syncfusion.Pdf.Graphics;

namespace BP.Akuntansi
{
    public class PerkadaRealisasi_1Logic : BP
    {
        List<Rekening> m_lstRekening;

        public PerkadaRealisasi_1Logic(int tahun) : base(tahun)
        {
            m_lstRekening = new List<Rekening>();
        }



        private List<AnggaranRealisasi> GetAnggaranRealisasi(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun );


                lst = ologic.GetAnggaranRealisasi(tahun,iddinas);
                if (lst == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }
        private List<AnggaranRealisasi> GetAnggaranRealisasiPendapatan(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);


                lst = ologic.GetAnggaranRealisasiPendapatan(tahun,iddinas);
                if (lst == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }
        private List<AnggaranRealisasi> GetAnggaranRealisasiPenerimaanPembiayaan(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);


                lst = ologic.GetPenerimaanPembiayaan(tahun, iddinas);
                if (lst == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }
        private List<AnggaranRealisasi> GetAnggaranRealisasiPengeluaranPembiayaan(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);


                lst = ologic.GetPengeluaranPembiayaan(tahun, iddinas);
                if (lst == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;


            }
        }
        public List<PerdaRealisasi_1_2> Process()
        {
            #region Process
            try
            {
                List<PerdaRealisasi_1_2> lstPerda = new List<PerdaRealisasi_1_2>();

                List<PerdaRealisasi_1_2> lstPerdaKelompokRekeningPendapatan = new List<PerdaRealisasi_1_2>();
                List<PerdaRealisasi_1_2> lstPerdaJenisRekeningPendapatan = new List<PerdaRealisasi_1_2>();
                
                List<PerdaRealisasi_1_2> lstPerdaKelompokRekeningBelanja = new List<PerdaRealisasi_1_2>();
                List<PerdaRealisasi_1_2> lstPerdaJenisRekeningBelanja = new List<PerdaRealisasi_1_2>();



                List<AnggaranRealisasi> lst = GetAnggaranRealisasiPendapatan(Tahun);


                if (LoadRekening() == false)
                {
                    _lastError = "Gagal memanggil Rekening";
                    return null;
                }


                #region Pendapatan
                PerdaRealisasi_1_2 prJudulPendapatan  = new PerdaRealisasi_1_2()
                {
                    Anggaran = lst.Sum(x => x.Anggaran),
                    Realisasi = lst.Sum(x => x.Realisasi),
                    KodeRekening = "",
                    Jenis = 1,

                    Kode = "",
                    Bold = 1,
                    Garis = 2,

                    BesarFont = 12,
                    Nama = "PENDAPATAN",
                    Depth = 5,
                };
                prJudulPendapatan.Kode = "4";
                prJudulPendapatan.Selisih = prJudulPendapatan.Realisasi - prJudulPendapatan.Anggaran;

                prJudulPendapatan.Persen = Persen(prJudulPendapatan.Realisasi, prJudulPendapatan.Anggaran);
                lstPerda.Add(prJudulPendapatan);

                if (lst != null)
                {
                    lstPerdaKelompokRekeningPendapatan = ProcessKelompokBelanja(lst.Where(x => x.Jenis == 1).ToList(), 2).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisRekeningPendapatan = ProcessKelompokBelanja(lst.Where(x => x.Jenis == 1).ToList(), 3).OrderBy(y => y.KodeRekening).ToList();
                    foreach (PerdaRealisasi_1_2 kr in lstPerdaKelompokRekeningPendapatan)
                    {
                        kr.Garis = 1;
                        kr.Kode = kr.KodeRekening.ToKodeRekening(2);
                        
                        kr.Depth = 15;
                        kr.Bold = 1;
                        kr.Jenis = 1;
                       
                        kr.Selisih = kr.Realisasi - kr.Anggaran;
                        kr.Persen = Persen(kr.Realisasi, kr.Anggaran);
                        
                        lstPerda.Add(kr);
                        foreach (PerdaRealisasi_1_2 kj in lstPerdaJenisRekeningPendapatan)
                        {
                            if (kr.KodeRekening.ToString().Substring(0, 2) == kj.KodeRekening.ToString().Substring(0, 2))
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 1;
                                kj.Kode = kj.KodeRekening.ToKodeRekening(3);
                                kj.Selisih = kj.Realisasi - kj.Anggaran;

                                kj.Persen = Persen(kj.Realisasi, kj.Anggaran);
                                lstPerda.Add(kj);
                            }
                        }
                    }
                }
                #endregion

                #region KelompokBelanja

                lst = GetAnggaranRealisasi(Tahun);
                PerdaRealisasi_1_2 prJudulBelanja;
                prJudulBelanja = new PerdaRealisasi_1_2
                {
                    Anggaran = lst.Sum(x => x.Anggaran),
                    Realisasi = lst.Sum(x => x.Realisasi),
                    KodeRekening = "",
                    Jenis = 3,
                    Kode = "5",
                    Bold = 1,
                    Garis = 2,

                    BesarFont = 12,
                    Nama = "BELANJA",
                    Depth = 5,
                };
                prJudulBelanja.Selisih = prJudulBelanja.Anggaran - prJudulBelanja.Realisasi;
                
                prJudulBelanja.Persen = PersenBelanja(prJudulBelanja.Anggaran, prJudulBelanja.Realisasi);
                lstPerda.Add(prJudulBelanja);

                if (lst != null)
                {
                    lstPerdaKelompokRekeningBelanja = ProcessKelompokBelanja(lst.Where(x => x.Jenis == 3).ToList(), 2).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisRekeningBelanja = ProcessKelompokBelanja(lst.Where(x => x.Jenis == 3).ToList(), 3).OrderBy(y => y.KodeRekening).ToList();
                    foreach (PerdaRealisasi_1_2 kr in lstPerdaKelompokRekeningBelanja)
                    {
                        kr.Garis = 1;
                        kr.Depth = 15;
                        kr.Bold = 1;
                        kr.Jenis = 3;
                        kr.Selisih = kr.Anggaran - kr.Realisasi;
                        kr.Persen = PersenBelanja(kr.Anggaran, kr.Realisasi);
                        lstPerda.Add(kr);
                        foreach (PerdaRealisasi_1_2 kj in lstPerdaJenisRekeningBelanja)
                        {
                            if (kr.KodeRekening.ToString().Substring(0, 2) == kj.KodeRekening.ToString().Substring(0, 2))
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 3;
                                kj.Selisih = kj.Anggaran - kj.Realisasi;
                        kj.Persen = PersenBelanja(kj.Anggaran, kj.Realisasi);

                                lstPerda.Add(kj);
                            }
                        }
                    }
                }

                #endregion
                #region surplusdefisit
                decimal surplusdefisitanggaran = prJudulPendapatan.Anggaran - prJudulBelanja.Anggaran;
                decimal surplusdefisitrealisasi = prJudulPendapatan.Realisasi- prJudulBelanja.Realisasi;
                PerdaRealisasi_1_2 SD = new PerdaRealisasi_1_2
                {
                    Anggaran = surplusdefisitanggaran,
                    Realisasi = surplusdefisitrealisasi,
                    KodeRekening = "",
                    Jenis = 4,

                    Kode = "",
                    Bold = 1,
                    Garis = 1,

                    BesarFont = 12,
                    Nama = "SURPLUS/DEFISIT",
                    Depth = -1,
                };
                lstPerda.Add(SD);
                #endregion
                #region PenerimanPembiayaan
                List<AnggaranRealisasi> lstPenerimaan = GetAnggaranRealisasiPenerimaanPembiayaan(Tahun);
                


                PerdaRealisasi_1_2 prJudulPenerimaan = new PerdaRealisasi_1_2
                {
                    Anggaran = lstPenerimaan.Sum(x => x.Anggaran),
                    Realisasi = lstPenerimaan.Sum(x => x.Realisasi),
                    KodeRekening = "",
                    Jenis = 4,

                    Kode = "4.1",
                    Bold = 1,
                    Garis = 1,

                    BesarFont = 12,
                    Nama = "PENERIMAAN PEMBIAYAAN",
                    Depth = 5,
                };

                prJudulPenerimaan.Persen = Persen(prJudulPenerimaan.Realisasi, prJudulPenerimaan.Anggaran);
                prJudulPenerimaan.Selisih = prJudulPenerimaan.Realisasi- prJudulPenerimaan.Anggaran;
                lstPerda.Add(prJudulPenerimaan);

                List<PerdaRealisasi_1_2> lstPerdaKelompokRekeningPenerimaan = new List<PerdaRealisasi_1_2>();
                List<PerdaRealisasi_1_2> lstPerdaJenisRekeningPenerimaan = new List<PerdaRealisasi_1_2>();

                    lstPerdaKelompokRekeningPenerimaan = ProcessKelompokBelanja(lstPenerimaan.ToList(), 2).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisRekeningPenerimaan = ProcessKelompokBelanja(lstPenerimaan.ToList(), 3).OrderBy(y => y.KodeRekening).ToList();
                    foreach (PerdaRealisasi_1_2 kr in lstPerdaKelompokRekeningPenerimaan)
                    {
                        kr.Garis = 1;
                        kr.Depth = 15;
                        kr.Bold = 1;
                        kr.Jenis = 4;
                    kr.Persen = Persen(kr.Realisasi, kr.Anggaran);
                    kr.Selisih = kr.Realisasi - kr.Anggaran;
                    lstPerda.Add(kr);
                        foreach (PerdaRealisasi_1_2 kj in lstPerdaJenisRekeningPenerimaan)
                        {
                            if (kr.KodeRekening.ToString().Substring(0, 2) == kj.KodeRekening.ToString().Substring(0, 2))
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 1;

                            kj.Persen = Persen(kj.Realisasi, kj.Anggaran);
                            kj.Selisih = kj.Realisasi - kj.Anggaran;

                            lstPerda.Add(kj);
                            }
                        }
                    }
                
                #endregion
                #region PengeluaranPembiayaan
                List<AnggaranRealisasi> lstPengeluaranPembiayaan = GetAnggaranRealisasiPengeluaranPembiayaan(Tahun);



                PerdaRealisasi_1_2 prJudulPengeluaran = new PerdaRealisasi_1_2
                    {
                        Anggaran = lstPengeluaranPembiayaan.Sum(x => x.Anggaran),
                        Realisasi = lstPengeluaranPembiayaan.Sum(x => x.Realisasi),
                        KodeRekening = "",
                        Jenis = 5,

                        Kode = "",
                        Bold = 1,
                        Garis = 1,

                        BesarFont = 12,
                        Nama = "PENGELUARAN PEMBIAYAAN",
                        Depth = 5,
                    };
                prJudulPengeluaran.Persen = PersenBelanja(prJudulPengeluaran.Anggaran, prJudulPengeluaran.Realisasi);
                lstPerda.Add(prJudulPengeluaran);

                    List<PerdaRealisasi_1_2> lstPerdaKelompokRekeningPengeluaran = new List<PerdaRealisasi_1_2>();
                    List<PerdaRealisasi_1_2> lstPerdaJenisRekeningPengeluaran = new List<PerdaRealisasi_1_2>();

                    lstPerdaKelompokRekeningPengeluaran = ProcessKelompokBelanja(lstPengeluaranPembiayaan.ToList(), 2).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisRekeningPengeluaran = ProcessKelompokBelanja(lstPengeluaranPembiayaan.ToList(), 3).OrderBy(y => y.KodeRekening).ToList();
                    foreach (PerdaRealisasi_1_2 kr in lstPerdaKelompokRekeningPengeluaran)
                    {
                        kr.Garis = 1;
                        kr.Depth = 15;
                        kr.Bold = 1;
                        kr.Jenis = 4;
                    kr.Selisih = kr.Anggaran - kr.Realisasi;

                    kr.Persen = PersenBelanja(kr.Anggaran, kr.Realisasi);
                    lstPerda.Add(kr);
                        foreach (PerdaRealisasi_1_2 kj in lstPerdaJenisRekeningPengeluaran)
                        {
                            if (kr.KodeRekening.ToString().Substring(0, 2) == kj.KodeRekening.ToString().Substring(0, 2))
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 1;
                            kj.Selisih = kj.Anggaran - kj.Realisasi;

        
                            kj.Persen = PersenBelanja(kj.Anggaran, kj.Realisasi);


                            lstPerda.Add(kj);
                            }
                        }
                    }


                #endregion
                #region SILPA
                decimal netoanggaran = prJudulPenerimaan.Anggaran - prJudulPengeluaran.Anggaran;
                decimal netorealisasi = prJudulPenerimaan.Realisasi - prJudulPengeluaran.Realisasi;

                PerdaRealisasi_1_2 neto = new PerdaRealisasi_1_2
                {
                    Anggaran = netoanggaran,
                    Realisasi = netorealisasi,
                    KodeRekening = "",
                    Jenis = 9,
                    Persen="",
                    Kode = "",
                    Bold = 1,
                    Garis = 1,

                    BesarFont = 12,
                    Nama = "PEMBIAYAAN NETO",
                    Depth = -1,
                };
                lstPerda.Add(neto);

                decimal silpatanggaran = ((prJudulPendapatan.Anggaran - prJudulBelanja.Anggaran)+
                                          (prJudulPenerimaan.Anggaran-prJudulPengeluaran.Anggaran));
                decimal silparealisasi = ((prJudulPendapatan.Realisasi - prJudulBelanja.Realisasi) +
                                            (prJudulPenerimaan.Realisasi - prJudulPengeluaran.Realisasi));
                
                PerdaRealisasi_1_2 silpa = new PerdaRealisasi_1_2
                {
                    Anggaran = silpatanggaran,
                    Realisasi = silparealisasi,
                    KodeRekening = "",
                    Jenis = 9,

                    Kode = "",
                    Bold = 1,
                    Garis = 2,

                    BesarFont = 12,
                    Nama = "Sisa Lebih Pembiayaan Anggaran",
                    Depth = 0,
                };
                lstPerda.Add(silpa);
                #endregion



                return lstPerda;//.OrderBy(x => x.Jenis).ToList();
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
            #endregion
        }
        private string Persen(decimal realisasi, decimal anggaran)
        {
            if (anggaran > 0)
            {
                return (((realisasi) / anggaran) * 100).ToString("#.##") + " %";
            }
            else
            {
                return "100 % ";
            }
        }
        private string PersenBelanja(decimal anggaran, decimal realisasi)
        {
            if (anggaran > 0)
            {
                
                return ((realisasi / anggaran) * 100).ToString("#.##") + " %";
            }
            else
            {
                return "100 % ";
            }
        }

        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();
                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                m_lstRekening = oRekeningLogic.Get().Where(r => r.ID >= 400000000000 && r.ID < 700000000000 && r.Root <= 3).ToList();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }

        private List<PerdaRealisasi_1_2> ProcessKelompokBelanja(List<AnggaranRealisasi> lst, int level)
        {
            try
            {

                List<Rekening> lstRekening;
                lstRekening = m_lstRekening.FindAll(x => x.Root == level);
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
                      .GroupBy(c => c.IDRekening.ToString().Substring(0, lenKode) )


                .Select(x => new
                {
                    Kode = x.Key,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();

                List<PerdaRealisasi_1_2> lstKi = new List<PerdaRealisasi_1_2>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         on p.ID.ToString().Substring(0, lenKode) equals j.Kode

                         select new PerdaRealisasi_1_2
                         {
                             Kode = p.ID.ToString().ToKodeRekening(level),                             
                             KodeRekening = p.ID.ToString(),
                             Nama = p.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             Selisih= j.Anggaran-j.Realisasi,
                             Persen= PersenBelanja(j.Anggaran, j.Realisasi)


                         }).ToList<PerdaRealisasi_1_2>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        
        
    }
}
