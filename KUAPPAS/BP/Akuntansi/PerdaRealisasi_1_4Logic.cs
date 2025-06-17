using DTO.Akuntansi;
using DTO;
using KUAPPAS.DataAccess9.DTO.Akuntansi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using KUAPPAS;
using Formatting;

namespace BP.Akuntansi
{
    public class PerdaRealisasi_1_4_Logic : BP
    {
        List<Rekening> m_lstRekening;
        
        

       public PerdaRealisasi_1_4_Logic(int tahun):base(tahun)
       {

       }

        public List<PerdaRealisasi_1_4> Process()
        {

            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(Tahun);

                lst = ologic.GetAnggaranRealisasi(Tahun);
                if (lst == null)
                {
                    _lastError = ologic.LastError();
                    return null;
                }
                

                List<PerdaRealisasi_1_4> lstPerda = new List<PerdaRealisasi_1_4>();

                PerdaRealisasi_1_4 prJudul = new PerdaRealisasi_1_4
                {
                    Anggaran = lst.Sum(x => x.Anggaran),
                    Realisasi = lst.Sum(x => x.Realisasi),

                    AnggaranOperasi = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(x => x.Anggaran),
                    RealisasiOperasi = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(x => x.Realisasi),
                    AnggaranModal = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(x => x.Anggaran),
                    RealisasiModal = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(x => x.Realisasi),
                    AnggaranTakTerduga = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(x => x.Anggaran),
                    RealisasiTakTerduga = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(x => x.Realisasi),
                    AnggaranTransfer = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(x => x.Anggaran),
                    RealisasiTransfer = lst.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(x => x.Realisasi),

                    Nama = "BELANJA",
                    Kode = "",
                    Bold = 1,
                    
                };
                lstPerda.Add(prJudul);

                




                #region Belanja


                List<PerdaRealisasi_1_4> lstPerdaPerKategori = new List<PerdaRealisasi_1_4>();
                List<PerdaRealisasi_1_4> lstPerdaPerUrusan = new List<PerdaRealisasi_1_4>();
                List<PerdaRealisasi_1_4> lstPerdaPerSKPD = new List<PerdaRealisasi_1_4>();
                List<PerdaRealisasi_1_4> lstPerdaPerProgram = new List<PerdaRealisasi_1_4>();
                List<PerdaRealisasi_1_4> lstPerdaPerKegiatan = new List<PerdaRealisasi_1_4>();
                List<PerdaRealisasi_1_4> lstPerdaPerSubKegiatan = new List<PerdaRealisasi_1_4>();

                

                if (lst != null)
                {
                    lstPerdaPerKategori = ProcessPerKategori(lst);
                    lstPerdaPerUrusan = ProcessPerUrusan(lst);
                    lstPerdaPerSKPD = ProcessPerSKPD(lst);
                    lstPerdaPerProgram = ProcessProgram(lst);
                    lstPerdaPerKegiatan = ProcessKegiatan(lst);
                    lstPerdaPerSubKegiatan = ProcessSubKegiatan(lst);
                    foreach (PerdaRealisasi_1_4 k in lstPerdaPerKategori)
                    {
                        
                        k.Bold = 1;
                        lstPerda.Add(k);
                        
                        foreach (PerdaRealisasi_1_4 u in lstPerdaPerUrusan)
                        {
                            if (u.IDUrusan.ToString().Length > 2)
                            { // jika kode urusan kode kategorinya sama

                                if (k.Kode == u.IDUrusan.ToString().Substring(0, 1))
                                { // jika kode urusannya 3 digit
                                    u.Kode = u.IDUrusan.ToKodeUrusan();                                    
                                    u.Nama = u.Nama.ToUpper();
                                    u.Bold = 1;
                                    lstPerda.Add(u);
                                    
                                                foreach (PerdaRealisasi_1_4 pu in lstPerdaPerSKPD)
                                                {   
                                                    if (u.IDUrusan == pu.IDUrusan )
                                                    { // untuk skpd skpd, jika urusannya sama
                                                        pu.Kode = "";
                                                        pu.Bold = 1;

                                                        lstPerda.Add(pu);
                                                        foreach (PerdaRealisasi_1_4 pr in lstPerdaPerProgram)
                                                        {

                                                            if (pr.IDDinas == pu.IDDinas && pr.IDUrusan == pu.IDUrusan)
                                                            {
                                                                // jika urusan  dan dinas nya nyambung
                                                                lstPerda.Add(pr);
                                                                foreach (PerdaRealisasi_1_4 pk in lstPerdaPerKegiatan)
                                                                {
                                                                    if (pr.IDDinas == pk.IDDinas && pr.IDUrusan == pk.IDUrusan && 
                                                                        pr.IDProgram== pk.IDProgram)
                                                                    {
                                                                        lstPerda.Add(pk);
                                                            foreach (PerdaRealisasi_1_4 ps in lstPerdaPerSubKegiatan)
                                                            {
                                                                if (pk.IDDinas == ps.IDDinas && pk.IDUrusan == ps.IDUrusan &&
                                                            pk.IDProgram == ps.IDProgram && pk.IDKegiatan == ps.IDKegiatan)
                                                                {
                                                                    lstPerda.Add(ps);
                                                                }

                                                            }
                                                            }
                                                                }
                                                            }
                                                        }//--

                                                    }                       
                                                }//---
                                            }
                                        }
                                    }
                                }

                            }
                else
                {


                }

                return lstPerda;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }


        #region Belanja
        private List<PerdaRealisasi_1_4> ProcessPerKategori(List<AnggaranRealisasi> lst)
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
                    Kode = x.Key,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                    AnggaranOperasi = x.Where (i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Anggaran),
                    RealisasiOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Realisasi),
                    AnggaranModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Anggaran),
                    RealisasiModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Realisasi),
                    AnggaranTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Anggaran),
                    RealisasiTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Realisasi),
                    AnggaranTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Anggaran),
                    RealisasiTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Realisasi),

                }).ToList();
                List<PerdaRealisasi_1_4> lstKi = new List<PerdaRealisasi_1_4>();


                lstKi = (from t in
                          lstKategori
                         join j in lstJumlah
                         on t.ID equals j.Kode
                         select new PerdaRealisasi_1_4
                         {
                             Kode = j.Kode.ToString(),
                             Nama = t.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             AnggaranOperasi = j.AnggaranOperasi,
                             RealisasiOperasi =j.RealisasiOperasi,
                             AnggaranModal =j.AnggaranModal,
                             RealisasiModal =j.RealisasiModal,
                             AnggaranTakTerduga = j.AnggaranTakTerduga,
                             RealisasiTakTerduga =j.RealisasiTakTerduga,
                             AnggaranTransfer =j.AnggaranTransfer,
                             RealisasiTransfer =j.RealisasiTransfer,




                         }).ToList<PerdaRealisasi_1_4>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private List<PerdaRealisasi_1_4> ProcessPerUrusan(List<AnggaranRealisasi> lst)
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
                    IDUrusan= x.Key,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                    AnggaranOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Anggaran),
                    RealisasiOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Realisasi),
                    AnggaranModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Anggaran),
                    RealisasiModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Realisasi),
                    AnggaranTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Anggaran),
                    RealisasiTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Realisasi),
                    AnggaranTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Anggaran),
                    RealisasiTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_4> lstKi = new List<PerdaRealisasi_1_4>();


                lstKi = (from t in
                          lstUrusan
                         join j in lstJumlah
                         on t.ID equals j.IDUrusan

                         select new PerdaRealisasi_1_4
                         {
                             Kode = j.IDUrusan.ToString(),
                             IDUrusan= j.IDUrusan,
                             Nama = t.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             AnggaranOperasi = j.AnggaranOperasi,
                             RealisasiOperasi = j.RealisasiOperasi,
                             AnggaranModal = j.AnggaranModal,
                             RealisasiModal = j.RealisasiModal,
                             AnggaranTakTerduga = j.AnggaranTakTerduga,
                             RealisasiTakTerduga = j.RealisasiTakTerduga,
                             AnggaranTransfer = j.AnggaranTransfer,
                             RealisasiTransfer = j.RealisasiTransfer,



                         }).ToList<PerdaRealisasi_1_4>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_4> ProcessPerSKPD(List<AnggaranRealisasi> lst)
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
                      .GroupBy(c => new { c.IDUrusan, c.IDDinas })

                .Select(x => new
                {
                    Dinas = x.Key.IDDinas,
                    Urusan= x.Key.IDUrusan,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                    AnggaranOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Anggaran),
                    RealisasiOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Realisasi),
                    AnggaranModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Anggaran),
                    RealisasiModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Realisasi),
                    AnggaranTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Anggaran),
                    RealisasiTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Realisasi),
                    AnggaranTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Anggaran),
                    RealisasiTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_4> lstKi = new List<PerdaRealisasi_1_4>();


                lstKi = (from p in
                          lstPelaksanaUrusan
                         join j in lstJumlah
                         //on p.Dinas equals j.KodeDinas && p
                         on new { A = p.Dinas, B = p.Urusan }
                         equals new { A = j.Dinas, B = j.Urusan }

                         select new PerdaRealisasi_1_4
                         {
                             Kode = p.KodeDinas,
                             Nama = p.NamaDinas,
                             IDUrusan= p.Urusan,
                            IDDinas=p.Dinas,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             AnggaranOperasi = j.AnggaranOperasi,
                             RealisasiOperasi = j.RealisasiOperasi,
                             AnggaranModal = j.AnggaranModal,
                             RealisasiModal = j.RealisasiModal,
                             AnggaranTakTerduga = j.AnggaranTakTerduga,
                             RealisasiTakTerduga = j.RealisasiTakTerduga,                             AnggaranTransfer = j.AnggaranTransfer,
                             RealisasiTransfer = j.RealisasiTransfer,



                         }).ToList<PerdaRealisasi_1_4>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_4> ProcessProgram(List<AnggaranRealisasi> lst)
        {
            try
            {



                var distinctProgram = lst
                    .Select(m => new { m.IDDinas, m.IDUrusan,m.IDProgram, m.NamaProgram })
                    .Distinct()
                    .ToList();

                var lstJumlah = lst
                      .GroupBy(c => new {c.IDDinas,c.IDUrusan, c.IDProgram })


                .Select(x => new
                {
                    Dinas = x.Key.IDDinas,
                    Urusan = x.Key.IDUrusan,
                    Program =x.Key.IDProgram,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                    AnggaranOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Anggaran),
                    RealisasiOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Realisasi),
                    AnggaranModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Anggaran),
                    RealisasiModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Realisasi),
                    AnggaranTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Anggaran),
                    RealisasiTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Realisasi),
                    AnggaranTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Anggaran),
                    RealisasiTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_4> lstKi = new List<PerdaRealisasi_1_4>();


                lstKi = (from p in
                          distinctProgram
                         join j in lstJumlah
                         on new { A = p.IDDinas, B = p.IDProgram}
                         equals new { A = j.Dinas, B = j.Program }


                         select new PerdaRealisasi_1_4
                         {
                             Kode = p.IDProgram.ToKodeProgram(),
                             Nama = p.NamaProgram,
                             IDProgram=p.IDProgram,
                             IDUrusan = p.IDUrusan,
                             IDDinas = p.IDDinas,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             AnggaranOperasi = j.AnggaranOperasi,
                             RealisasiOperasi = j.RealisasiOperasi,
                             AnggaranModal = j.AnggaranModal,
                             RealisasiModal = j.RealisasiModal,
                             AnggaranTakTerduga = j.AnggaranTakTerduga,
                             RealisasiTakTerduga = j.RealisasiTakTerduga,
                             AnggaranTransfer = j.AnggaranTransfer,
                             RealisasiTransfer = j.RealisasiTransfer,



                         }).ToList<PerdaRealisasi_1_4>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_4> ProcessKegiatan(List<AnggaranRealisasi> lst)
        {
            try
            {



                var distinctKegiatan = lst
                    .Select(m => new {m.IDDinas,m.IDUrusan, m.IDProgram, m.IDKegiatan, m.NamaKegiatan })
                    .Distinct()
                    .ToList();

                var lstJumlah = lst
                      .GroupBy(c => new { c.IDDinas,c.IDUrusan , c.IDProgram, c.IDKegiatan, c.NamaKegiatan })


                .Select(x => new
                {
                    Dinas = x.Key.IDDinas,
                    Program=x.Key.IDProgram,
                    Kegiatan= x.Key.IDKegiatan,
                    Urusan = x.Key.IDUrusan,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                    AnggaranOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Anggaran),
                    RealisasiOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Realisasi),
                    AnggaranModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Anggaran),
                    RealisasiModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Realisasi),
                    AnggaranTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Anggaran),
                    RealisasiTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Realisasi),
                    AnggaranTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Anggaran),
                    RealisasiTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_4> lstKi = new List<PerdaRealisasi_1_4>();


                lstKi = (from p in
                          distinctKegiatan
                         join j in lstJumlah
                         on new { A = p.IDDinas, B = p.IDKegiatan}
                         equals new { A = j.Dinas , B = j.Kegiatan}

                         select new PerdaRealisasi_1_4
                         {
                             Kode = p.IDKegiatan.ToKodeKegiatan(),
                             Nama = p.NamaKegiatan,
                             IDProgram = p.IDProgram,
                             IDUrusan = p.IDUrusan,
                             IDDinas = p.IDDinas,
                             IDKegiatan=p.IDKegiatan,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             AnggaranOperasi = j.AnggaranOperasi,
                             RealisasiOperasi = j.RealisasiOperasi,
                             AnggaranModal = j.AnggaranModal,
                             RealisasiModal = j.RealisasiModal,
                             AnggaranTakTerduga = j.AnggaranTakTerduga,
                             RealisasiTakTerduga = j.RealisasiTakTerduga,
                             AnggaranTransfer = j.AnggaranTransfer,
                             RealisasiTransfer = j.RealisasiTransfer,


                         }).ToList<PerdaRealisasi_1_4>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private List<PerdaRealisasi_1_4> ProcessSubKegiatan(List<AnggaranRealisasi> lst)
        {
            try
            {



                var distinctSubKegiatan = lst
                    .Select(m => new { m.IDDinas,m.IDUrusan,m.IDProgram, m.IDKegiatan, m.IDSUBKegiatan, m.NamaSubKegiatan })
                    .Distinct()
                    .ToList();

                var lstJumlah = lst
                      .GroupBy(c => new { c.IDDinas,c.IDUrusan,c.IDProgram, c.IDKegiatan, c.IDSUBKegiatan, c.NamaSubKegiatan })


                .Select(x => new
                {
                    Dinas = x.Key.IDDinas,
                    Program = x.Key.IDProgram,
                    Kegiatan = x.Key.IDKegiatan,
                    SubKegiatan=x.Key.IDSUBKegiatan,
                    Urusan = x.Key.IDUrusan,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                    AnggaranOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Anggaran),
                    RealisasiOperasi = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "51").Sum(a => a.Realisasi),
                    AnggaranModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Anggaran),
                    RealisasiModal = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "52").Sum(a => a.Realisasi),
                    AnggaranTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Anggaran),
                    RealisasiTakTerduga = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "53").Sum(a => a.Realisasi),
                    AnggaranTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Anggaran),
                    RealisasiTransfer = x.Where(i => i.IDRekening.ToString().Substring(0, 2) == "54").Sum(a => a.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_4> lstKi = new List<PerdaRealisasi_1_4>();


                lstKi = (from p in
                          distinctSubKegiatan
                         join j in lstJumlah
                         on new { A = p.IDDinas, B = p.IDKegiatan, C= p.IDSUBKegiatan }
                         equals new { A = j.Dinas, B = j.Kegiatan ,C= j.SubKegiatan }



                         select new PerdaRealisasi_1_4
                         {
                             Kode = p.IDSUBKegiatan.ToKodeSubKegiatan(),
                             Nama = p.NamaSubKegiatan,
                             IDProgram = p.IDProgram,
                             IDUrusan = p.IDUrusan,
                             IDDinas = p.IDDinas,
                             IDKegiatan = p.IDKegiatan,
                             IDSubKegiatan= p.IDSUBKegiatan,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             AnggaranOperasi = j.AnggaranOperasi,
                             RealisasiOperasi = j.RealisasiOperasi,
                             AnggaranModal = j.AnggaranModal,
                             RealisasiModal = j.RealisasiModal,
                             AnggaranTakTerduga = j.AnggaranTakTerduga,
                             RealisasiTakTerduga = j.RealisasiTakTerduga,
                             AnggaranTransfer = j.AnggaranTransfer,
                             RealisasiTransfer = j.RealisasiTransfer,




                         }).ToList<PerdaRealisasi_1_4>();




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

        #endregion
        #endregion
        #endregion
    }
}
