using DTO;
using DTO.Akuntansi;
using Formatting;
using KUAPPAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace BP.Akuntansi
{
    public class PerdaRealisasi_1_3Logic : BP
    {
        public int m_iddinas;
        SKPD skpd;
        List<Rekening> m_lstRekening;
        PerdaRealisasi_1_3 prJudulPendapatan;
        PerdaRealisasi_1_3 prJudulBelanja;
        PerdaRealisasi_1_3 prJudulPenerimaan;
        PerdaRealisasi_1_3 prJudulPengeluaran;
        private string sIDUrusan;
        private int m_iJenis;
        private List<Rekening> lstIDRekeningPendapatan;
        private int countPendapatan = 0;
        private
            List<AnggaranRealisasi> lstRealisasiPendapatan;
        public PerdaRealisasi_1_3Logic(int tahun) : base(tahun)
        {
            m_lstRekening = new List<Rekening>();
            lstRealisasiPendapatan = new List<AnggaranRealisasi>();
            lstIDRekeningPendapatan = new List<Rekening>();
        }



        public int jenis
        {
            set
            {
                m_iJenis = value;
            }
        }
        public List<Rekening> GetListRekeningPendapatan()
        {

            lstIDRekeningPendapatan = new List<Rekening>();
            lstRealisasiPendapatan = GetAnggaranRealisasiPendapatan(Tahun, m_iddinas);

            foreach (AnggaranRealisasi a in lstRealisasiPendapatan)
            {
                Rekening r = new Rekening();
                r.ID = a.IDRekening;
                if (lstIDRekeningPendapatan.Find(x => x.ID == r.ID) == null)
                    lstIDRekeningPendapatan.Add(r);
            }
            countPendapatan = lstIDRekeningPendapatan.Count;
            return lstIDRekeningPendapatan;
        }
        private List<AnggaranRealisasi> GetAnggaranRealisasi(int tahun, int iddinas)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);


                lst = ologic.GetAnggaranRealisasi(tahun, iddinas);
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
        private List<AnggaranRealisasi> GetAnggaranRealisasiPendapatan(int tahun, int iddinas)
        {
            try
            {
                m_iddinas = iddinas;
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);



                lst = ologic.GetAnggaranRealisasiPendapatan(tahun, iddinas);
                lstRealisasiPendapatan = lst;



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
        private List<AnggaranRealisasi> GetAnggaranRealisasiPenerimaanPembiayaan(int tahun, int iddinas)
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
        private List<AnggaranRealisasi> GetAnggaranRealisasiPengeluaranPembiayaan(int tahun, int iddinas)
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
        public List<PerdaRealisasi_1_3> Process(int iddinas)
        {
            try
            {

                skpd = new SKPD();
                SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
                skpd = oSKPDLogic.GetByID(iddinas);
                if (skpd == null)
                {
                    _lastError = "Kesalahan Mengambil data SKPD";

                    return null;

                }

                List<PerdaRealisasi_1_3> lstPerda = new List<PerdaRealisasi_1_3>();
                prJudulBelanja = new PerdaRealisasi_1_3();
                prJudulPendapatan = new PerdaRealisasi_1_3();
                prJudulPenerimaan = new PerdaRealisasi_1_3();
                prJudulPengeluaran = new PerdaRealisasi_1_3();


                UrusanLogic uLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
                int idUrusan = DataFormat.GetInteger(iddinas.ToString().Substring(0, 3));
                Urusan u = uLogic.GetByID(idUrusan);
                if (u == null)
                {
                    _lastError = "Tidak bisa memangggil Urusan Pemerintahan";
                    return null;
                }
                sIDUrusan = u.ID.ToKodeUrusan();
                //panggil Rekening
                if (LoadRekening() == false)
                {
                    _lastError = "Gagal memanggil Rekening";
                    return null;
                }
                lstPerda = ProccessPendapatan(iddinas);
                foreach (PerdaRealisasi_1_3 pdb in ProccessBelanja(iddinas))
                {
                    lstPerda.Add(pdb);

                }
                PerdaRealisasi_1_3 prSurplusDefisit= new PerdaRealisasi_1_3();
                prSurplusDefisit.Kode = "";
                prSurplusDefisit.Nama = "SURPLUS/DEFISIT";
                prSurplusDefisit.Anggaran = prJudulPendapatan.Anggaran - prJudulBelanja.Anggaran;
                prSurplusDefisit.Realisasi= prJudulPendapatan.Realisasi - prJudulBelanja.Realisasi;
                lstPerda.Add(prSurplusDefisit);


                foreach (PerdaRealisasi_1_3 pen in ProsesPenerimaan(iddinas))
                {
                    lstPerda.Add(pen);

                }
                foreach (PerdaRealisasi_1_3 peng in ProsesPengeluaran(iddinas))
                {
                    lstPerda.Add(peng);

                }
                //prSurplusDefisit
                    PerdaRealisasi_1_3 prNetto= new PerdaRealisasi_1_3();

                prNetto.Kode = "";
                prNetto.Nama = "SURPLUS/DEFISIT";
                prNetto.Anggaran = prJudulPenerimaan.Anggaran - prJudulPengeluaran.Anggaran;
                prNetto.Realisasi = prJudulPenerimaan.Realisasi - prJudulPengeluaran.Realisasi;
                lstPerda.Add(prNetto);

                

                prNetto.Kode = "";
                prNetto.Nama = "Sisa lebih pembiayaan anggaran tahun berkenaan (SILPA)";
                prNetto.Anggaran =(prSurplusDefisit.Anggaran+ prJudulPenerimaan.Anggaran) - (prJudulPengeluaran.Anggaran+ prSurplusDefisit.Anggaran);
                prNetto.Realisasi =(prSurplusDefisit.Realisasi+ prJudulPenerimaan.Realisasi) - (prJudulPengeluaran.Realisasi+ prSurplusDefisit.Realisasi);
                lstPerda.Add(prNetto);

                if (countPendapatan > 0)
                {
                    DasarHukumLogic oLogicDasarHukum = new DasarHukumLogic(Tahun);
                    List<DasarHukum> _lstDasarHukum = new List<DasarHukum>();
                    Single snglOnPerda = 1;

                    _lstDasarHukum = oLogicDasarHukum.Get(Tahun, snglOnPerda);
                    int i = 0;
                    foreach (DasarHukum dh in _lstDasarHukum)
                    {
                        lstPerda[i].DasarHukum = dh.Keterangan;
                        i++;

                    }

                }

                return lstPerda;




            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
        }

        #region Pendapatan
        private List<PerdaRealisasi_1_3> ProccessPendapatan(int iddinas)
        {
            try
            {
                List<PerdaRealisasi_1_3> lstPerda = new List<PerdaRealisasi_1_3>();

                List<PerdaRealisasi_1_3> lstPerdaKelompokRekeningPendapatan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaJenisRekeningPendapatan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaObjekRekeningPendapatan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaRincianObjekRekeningPendapatan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaSubRincianObjekRekeningPendapatan = new List<PerdaRealisasi_1_3>();

                List<AnggaranRealisasi> lst = GetAnggaranRealisasiPendapatan(Tahun, iddinas);
                if (lst != null)
                {

                    PerdaRealisasi_1_3 prJudulPendapatan = new PerdaRealisasi_1_3()
                    {
                        Anggaran = lst.Sum(x => x.Anggaran),
                        Realisasi = lst.Sum(x => x.Realisasi),
                        Jenis = 1,

                        Kode = "4",
                        KodeUrusan = sIDUrusan,
                        BesarFont = 12,

                        Nama = "PENDAPATAN",
                        Depth = 5,

                    };
                    prJudulPendapatan.Selisih = prJudulPendapatan.Realisasi - prJudulPendapatan.Anggaran;

                    prJudulPendapatan.Persen = Persen(prJudulPendapatan.Realisasi, prJudulPendapatan.Anggaran);




                    lstPerda.Add(prJudulPendapatan);

                    lstPerdaKelompokRekeningPendapatan = ProcessKelompokBelanja(lst.Where(x => x.Jenis == 1).ToList()).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisRekeningPendapatan = ProcessJenisBelanja(lst.Where(x => x.Jenis == 1).ToList()).OrderBy(y => y.KodeRekening).ToList();

                    if (m_iJenis > 3)
                    {

                        lstPerdaObjekRekeningPendapatan = ProcessObjeckBelanja(lst.Where(x => x.Jenis == 1).ToList()).OrderBy(y => y.KodeRekening).ToList();
                        if (lstPerdaObjekRekeningPendapatan == null)

                        {
                            return lstPerda;
                        }
                        lstPerdaRincianObjekRekeningPendapatan = ProcessRincianObjeckBelanja(lst.Where(x => x.Jenis == 1).ToList()).OrderBy(y => y.KodeRekening).ToList();
                        if (lstPerdaRincianObjekRekeningPendapatan == null)

                        {
                            return lstPerda;
                        }
                        lstPerdaSubRincianObjekRekeningPendapatan = ProcessSubRincianObjeckBelanja(lst.Where(x => x.Jenis == 1).ToList()).OrderBy(y => y.KodeRekening).ToList();
                        if (lstPerdaSubRincianObjekRekeningPendapatan == null)

                        {
                            return lstPerda;
                        }
                    }
                    foreach (PerdaRealisasi_1_3 kr in lstPerdaKelompokRekeningPendapatan)
                    {
                        kr.Garis = 1;


                        kr.Depth = 15;
                        kr.Selisih = kr.Realisasi - kr.Anggaran;
                        kr.Persen = Persen(kr.Realisasi, kr.Anggaran);
                        kr.Bold = 1;
                        kr.Jenis = 1;
                        if (kr.Realisasi != 0 || kr.Anggaran != 0)
                        {
                            lstPerda.Add(kr);
                        }
                        foreach (PerdaRealisasi_1_3 kj in lstPerdaJenisRekeningPendapatan)
                        {
                            if (kr.KelompokRekening.ToString()== kj.KelompokRekening.ToString())
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 1;

                                kj.Selisih = kj.Realisasi - kj.Anggaran;
                                kj.Persen = Persen(kj.Realisasi, kj.Anggaran);
                                lstPerda.Add(kj);
                                foreach (PerdaRealisasi_1_3 ob in lstPerdaObjekRekeningPendapatan)
                                {
                                    if (kj.JenisRekening == ob.JenisRekening)
                                    {
                                        ob.Selisih = ob.Realisasi - ob.Anggaran;
                                        ob.Persen = Persen(ob.Realisasi, ob.Anggaran);
                                        lstPerda.Add(ob);
                                        foreach (PerdaRealisasi_1_3 rob in lstPerdaRincianObjekRekeningPendapatan)
                                        {
                                            if (ob.ObjekRekening == rob.ObjekRekening)
                                            {
                                                rob.Selisih = rob.Realisasi - rob.Anggaran;
                                                rob.Persen = Persen(rob.Realisasi, rob.Anggaran);
                                                if (ob.Realisasi != 0 || ob.Anggaran != 0)
                                                {
                                                    lstPerda.Add(rob);
                                                }
                                                foreach (PerdaRealisasi_1_3 srob in lstPerdaSubRincianObjekRekeningPendapatan)
                                                {
                                                    if (srob.RincianRekening == srob.RincianRekening)
                                                    {

                                                        srob.Selisih = srob.Realisasi - srob.Anggaran;
                                                        srob.Persen = Persen(srob.Realisasi, srob.Anggaran);
                                                        if (srob.Realisasi != 0 || srob.Anggaran != 0)
                                                        {
                                                            lstPerda.Add(srob);
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
                }

                return lstPerda;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }



        }
        #region PersenSelisih
        private string Persen(decimal a, decimal b)
        {
            if (b > 0)
            {
                return (((a - b) / b) * 100).ToString("#.##") + " %";
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
        #endregion


        #endregion
        #region ProsesBelanja
        private List<PerdaRealisasi_1_3> ProccessBelanja(int iddinas)
        {
            try
            {
                List<PerdaRealisasi_1_3> lstPerda = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerProgram = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerKegiatan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerSubKegiatan = new List<PerdaRealisasi_1_3>();

                List<PerdaRealisasi_1_3> lstPerdaKelompokRekeningBelanja = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaJenisRekeningBelanja = new List<PerdaRealisasi_1_3>();

                List<PerdaRealisasi_1_3> lstPerdaObjekRekeningBelanja = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaRincianObjekRekeningBelanja = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaSubRincianObjekRekeningBelanja = new List<PerdaRealisasi_1_3>();


                List<AnggaranRealisasi> lst = GetAnggaranRealisasi(Tahun, iddinas);


                prJudulBelanja = new PerdaRealisasi_1_3
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
                    lstPerProgram = ProcessProgram(lst.Where(x => x.Jenis == 3).ToList(), 2).OrderBy(y => y.IDProgram).ToList();
                    if (lstPerProgram == null)
                    {
                        return lstPerda;
                    }
                    lstPerKegiatan = ProcessKegiatan(lst.Where(x => x.Jenis == 3).ToList(), 2).OrderBy(y => y.IDProgram).ToList();
                    if (lstPerKegiatan == null)
                    {
                        return lstPerda;
                    }
                    lstPerSubKegiatan = ProcessSubKegiatan(lst.Where(x => x.Jenis == 3).ToList(), 2).OrderBy(y => y.IDProgram).ToList();
                    if (lstPerSubKegiatan == null)

                    {
                        return lstPerda;
                    }
                    lstPerdaKelompokRekeningBelanja = ProcessKelompokBelanja(lst.Where(x => x.Jenis == 3).ToList()).OrderBy(y => y.KodeRekening).ToList();

                    if (lstPerdaKelompokRekeningBelanja == null)

                    {
                        return lstPerda;
                    }
                    lstPerdaJenisRekeningBelanja = ProcessJenisBelanja(lst.Where(x => x.Jenis == 3).ToList()).OrderBy(y => y.KodeRekening).ToList();
                    if (lstPerdaJenisRekeningBelanja == null)

                    {
                        return lstPerda;
                    }
                    if (m_iJenis > 3)
                    {
                        lstPerdaObjekRekeningBelanja = ProcessObjeckBelanja(lst.Where(x => x.Jenis == 3).ToList()).OrderBy(y => y.KodeRekening).ToList();
                        if (lstPerdaObjekRekeningBelanja == null)

                        {
                            return lstPerda;
                        }
                        lstPerdaRincianObjekRekeningBelanja = ProcessRincianObjeckBelanja(lst.Where(x => x.Jenis == 3).ToList()).OrderBy(y => y.KodeRekening).ToList();
                        if (lstPerdaRincianObjekRekeningBelanja == null)

                        {
                            return lstPerda;
                        }
                        lstPerdaSubRincianObjekRekeningBelanja = ProcessSubRincianObjeckBelanja(lst.Where(x => x.Jenis == 3).ToList()).OrderBy(y => y.KodeRekening).ToList();
                        if (lstPerdaSubRincianObjekRekeningBelanja == null)

                        {
                            return lstPerda;
                        }
                    }
                    foreach (PerdaRealisasi_1_3 p in lstPerProgram)
                    {
                        p.Selisih = p.Anggaran - p.Realisasi;
                        p.Persen = PersenBelanja(p.Anggaran, p.Realisasi);
                        p.KodeSubKegiatan = p.KodeProgram;
                        p.Kode = "";
                        lstPerda.Add(p);
                        foreach (PerdaRealisasi_1_3 k in lstPerKegiatan)
                        {
                            // tidak perlu Dinas nya. karena query udah filter dinas
                            if (k.IDProgram == p.IDProgram)
                            {
                                k.Selisih = k.Anggaran - k.Realisasi;
                                k.Persen = PersenBelanja(k.Anggaran, k.Realisasi);

                                lstPerda.Add(k);
                                foreach (PerdaRealisasi_1_3 sk in lstPerSubKegiatan)
                                {
                                    if (k.IDKegiatan == sk.IDKegiatan)
                                    {
                                        sk.Selisih = sk.Anggaran - sk.Realisasi;
                                        sk.Persen = PersenBelanja(sk.Anggaran, sk.Realisasi);

                                        lstPerda.Add(sk);

                                        foreach (PerdaRealisasi_1_3 kb in lstPerdaKelompokRekeningBelanja)
                                        {
                                            if (kb.IDSubKegiatan == sk.IDSubKegiatan)
                                            {
                                                kb.Selisih = kb.Anggaran - kb.Realisasi;
                                                kb.Persen = PersenBelanja(kb.Anggaran, kb.Realisasi);

                                                lstPerda.Add(kb);
                                                foreach (PerdaRealisasi_1_3 jb in lstPerdaJenisRekeningBelanja)
                                                {
                                                    if (sk.IDSubKegiatan == jb.IDSubKegiatan &&
                                                        kb.KelompokRekening == jb.KelompokRekening)

                                                    {

                                                        jb.Selisih = jb.Anggaran - jb.Realisasi;
                                                        jb.Persen = PersenBelanja(jb.Anggaran, jb.Realisasi);

                                                        lstPerda.Add(jb);
                                                        foreach (PerdaRealisasi_1_3 ob in lstPerdaObjekRekeningBelanja)
                                                        {
                                                            if (sk.IDSubKegiatan == ob.IDSubKegiatan &&
                                                               jb.JenisRekening == ob.JenisRekening)
                                                            {
                                                                ob.Selisih = ob.Anggaran - ob.Realisasi;
                                                                ob.Persen = PersenBelanja(ob.Anggaran, ob.Realisasi);


                                                                lstPerda.Add(ob);
                                                                foreach (PerdaRealisasi_1_3 rob in lstPerdaRincianObjekRekeningBelanja)
                                                                {
                                                                    if (rob.IDSubKegiatan == sk.IDSubKegiatan &&
                                                                    ob.ObjekRekening == rob.ObjekRekening)
                                                                    {
                                                                        rob.Selisih = rob.Anggaran - rob.Realisasi;
                                                                        rob.Persen = PersenBelanja(rob.Anggaran, rob.Realisasi);

                                                                        lstPerda.Add(rob);
                                                                        foreach (PerdaRealisasi_1_3 srob in lstPerdaSubRincianObjekRekeningBelanja)
                                                                            if (srob.IDSubKegiatan == jb.IDSubKegiatan &&
                                                                                srob.RincianRekening == rob.RincianRekening)
                                                                            {

                                                                                srob.Selisih = srob.Anggaran - srob.Realisasi;
                                                                                srob.Persen = PersenBelanja(srob.Anggaran, srob.Realisasi);

                                                                                lstPerda.Add(srob);
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
                                }
                            }
                        }
                    }
                }
                return lstPerda;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
        }
        #endregion
        #region Penerimaan
        private List<PerdaRealisasi_1_3> ProsesPenerimaan(int iddinas)
        {
            try
            {
                List<PerdaRealisasi_1_3> lstPerda = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaKelompokRekeningPenerimaan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaJenisRekeningPenerimaan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaObjekRekeningPenerimaan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaRincianObjekRekeningPenerimaan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaSubRincianObjekRekeningPenerimaan = new List<PerdaRealisasi_1_3>();

                List<AnggaranRealisasi> lstPenerimaan = GetAnggaranRealisasiPenerimaanPembiayaan(Tahun, iddinas);
                if (lstPenerimaan != null)
                {

                    prJudulPenerimaan = new PerdaRealisasi_1_3
                    {
                        Anggaran = lstPenerimaan.Sum(x => x.Anggaran),
                        Realisasi = lstPenerimaan.Sum(x => x.Realisasi),
                        KodeRekening = "",
                        Jenis = 4,

                        Kode = "",
                        Bold = 1,
                        Garis = 1,

                        BesarFont = 12,
                        Nama = "PENERIMAAN PEMBIAYAAN",
                        Depth = 5,
                    };
                    lstPerda.Add(prJudulPenerimaan);


                    lstPerdaKelompokRekeningPenerimaan = ProcessKelompokBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisRekeningPenerimaan = ProcessJenisBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                    if (m_iJenis > 3)
                       
                    {
                        lstPerdaObjekRekeningPenerimaan = ProcessObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                        lstPerdaRincianObjekRekeningPenerimaan = ProcessRincianObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                        lstPerdaSubRincianObjekRekeningPenerimaan = ProcessSubRincianObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                    }
                    foreach (PerdaRealisasi_1_3 kr in lstPerdaKelompokRekeningPenerimaan)
                    {
                        kr.Garis = 1;
                        kr.Depth = 15;
                        kr.Bold = 1;
                        kr.Jenis = 4;
                        lstPerda.Add(kr);
                        foreach (PerdaRealisasi_1_3 kj in lstPerdaJenisRekeningPenerimaan)
                        {
                            if (kr.KelompokRekening.ToString() == kj.KelompokRekening.ToString())
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 1;
                                lstPerda.Add(kj);

                                //lstPerdaObjekRekeningPenerimaan = ProcessObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                                //lstPerdaRincianObjekRekeningPenerimaan = ProcessRincianObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                                //lstPerdaSubRincianObjekRekeningPenerimaan = ProcessSubRincianObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();


                                foreach (PerdaRealisasi_1_3 ko in lstPerdaObjekRekeningPenerimaan)
                                {
                                    if (ko.JenisRekening.ToString() == kj.JenisRekening.ToString())
                                    {
                                        lstPerda.Add(ko);
                                        foreach (PerdaRealisasi_1_3 kro in lstPerdaRincianObjekRekeningPenerimaan)
                                        {
                                            if (kro.ObjekRekening.ToString() == ko.ObjekRekening.ToString())
                                            {
                                                lstPerda.Add(kro);
                                                foreach (PerdaRealisasi_1_3 ksro in lstPerdaSubRincianObjekRekeningPenerimaan)
                                                {
                                                    if (ksro.RincianRekening.ToString() == kro.RincianRekening.ToString())
                                                    {
                                                        lstPerda.Add(ksro);
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
                return lstPerda;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        #endregion
        #region Pengeluaran
        private List<PerdaRealisasi_1_3> ProsesPengeluaran(int iddinas)
        {
            try
            {
                List<PerdaRealisasi_1_3> lstPerda = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaKelompokPengeluaran = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaJenisPengeluaran = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaObjekRekeningPenerimaan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaRincianObjekRekeningPenerimaan = new List<PerdaRealisasi_1_3>();
                List<PerdaRealisasi_1_3> lstPerdaSubRincianObjekRekeningPenerimaan = new List<PerdaRealisasi_1_3>();

                List<AnggaranRealisasi> lstPengeluaran = GetAnggaranRealisasiPengeluaranPembiayaan(Tahun, iddinas);
                if (lstPengeluaran != null)
                {

                    prJudulPengeluaran = new PerdaRealisasi_1_3
                    {
                        Anggaran = lstPengeluaran.Sum(x => x.Anggaran),
                        Realisasi = lstPengeluaran.Sum(x => x.Realisasi),
                        KodeRekening = "",
                        Jenis = 4,

                        Kode = "",
                        Bold = 1,
                        Garis = 1,

                        BesarFont = 12,
                        Nama = "PENGELUARAN PEMBIAYAAN",
                        Depth = 5,
                    };
                    lstPerda.Add(prJudulPengeluaran);


                    lstPerdaKelompokPengeluaran = ProcessKelompokBelanja(lstPengeluaran.ToList()).OrderBy(y => y.KodeRekening).ToList();
                    lstPerdaJenisPengeluaran = ProcessJenisBelanja(lstPengeluaran.ToList()).OrderBy(y => y.KodeRekening).ToList();
                    if (m_iJenis > 3)
                    {
                        lstPerdaObjekRekeningPenerimaan = ProcessObjeckBelanja(lstPengeluaran.ToList()).OrderBy(y => y.KodeRekening).ToList();
                        lstPerdaRincianObjekRekeningPenerimaan = ProcessRincianObjeckBelanja(lstPengeluaran.ToList()).OrderBy(y => y.KodeRekening).ToList();
                        lstPerdaSubRincianObjekRekeningPenerimaan = ProcessSubRincianObjeckBelanja(lstPengeluaran.ToList()).OrderBy(y => y.KodeRekening).ToList();
                    }



                    foreach (PerdaRealisasi_1_3 kr in lstPerdaKelompokPengeluaran)
                    {
                        kr.Garis = 1;
                        kr.Depth = 15;
                        kr.Bold = 1;
                        kr.Jenis = 4;
                        lstPerda.Add(kr);
                        foreach (PerdaRealisasi_1_3 kj in lstPerdaJenisPengeluaran)
                        {
                            if (kr.KelompokRekening.ToString() == kj.KelompokRekening.ToString())
                            {
                                kj.Depth = 30;
                                kj.Bold = 0;
                                kj.Garis = 0;
                                kj.Jenis = 1;
                                lstPerda.Add(kj);
                                //lstPerdaObjekRekeningPenerimaan = ProcessObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                                //lstPerdaRincianObjekRekeningPenerimaan = ProcessRincianObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                                //lstPerdaSubRincianObjekRekeningPenerimaan = ProcessSubRincianObjeckBelanja(lstPenerimaan.ToList()).OrderBy(y => y.KodeRekening).ToList();
                               

                                foreach (PerdaRealisasi_1_3 ko in lstPerdaObjekRekeningPenerimaan)
                                {
                                    if (ko.JenisRekening.ToString() == kj.JenisRekening.ToString())
                                    {
                                        lstPerda.Add(ko);
                                        foreach (PerdaRealisasi_1_3 kro in lstPerdaRincianObjekRekeningPenerimaan)
                                        {
                                            if (kro.ObjekRekening.ToString()== ko.ObjekRekening.ToString())
                                            {
                                                lstPerda.Add(kro);
                                                foreach (PerdaRealisasi_1_3 ksro in lstPerdaSubRincianObjekRekeningPenerimaan)
                                                {
                                                    if (ksro.RincianRekening.ToString() == kro.RincianRekening.ToString())
                                                    {
                                                        lstPerda.Add(ksro);
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
                return lstPerda;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }

        #endregion


        private bool LoadRekening()
        {
            try
            {
                m_lstRekening = new List<Rekening>();

                RekeningLogic oRekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                m_lstRekening = oRekeningLogic.Get().Where(r => r.ID >= 400000000000 && r.ID < 800000000000 && r.Root <= 6).ToList();

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }

        private List<PerdaRealisasi_1_3> ProcessKelompokBelanja(List<AnggaranRealisasi> lst)
        {
            try
            {

                List<Rekening> lstRekening;
                lstRekening = m_lstRekening.FindAll(x => x.Root == 2);
                int lenKode = 2;

                var lstJumlah = lst
                .GroupBy(c => new { c.IDProgram, c.IDKegiatan, c.IDSUBKegiatan, c.KelompokRekening })
                .Select(x => new
                {

                    IDSUB = x.Key.IDSUBKegiatan,
                    KelompokRekening = x.Key.KelompokRekening,

                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();





                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         // on new { A = p.ID, B = p.Urusan }
                         //equals new { A = j.IDREKENING, B = j.KodeUrusan }
                         //

                         on p.ID.ToString().Substring(0, 2) equals j.KelompokRekening.ToString()

                         select new PerdaRealisasi_1_3
                         {

                             Kode = p.ID.ToString().ToKodeRekening(2),
                             KodeRekening = p.ID.ToString().ToKodeRekening(2),
                             KodeSubKegiatan = "",
                             KodeDinas = skpd.KodeSIPD,
                             Nama = p.Nama,
                             KelompokRekening = j.KelompokRekening,
                             IDSubKegiatan = j.IDSUB,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_3> ProcessJenisBelanja(List<AnggaranRealisasi> lst)
        {
            try
            {

                List<Rekening> lstRekening;
                lstRekening = m_lstRekening.FindAll(x => x.Root == 3);
                int lenKode = 4;

                var lstJumlah = lst
                .GroupBy(c => new { c.IDDinas, c.IDProgram, c.IDKegiatan, c.IDSUBKegiatan, c.KelompokRekening, c.JenisRekening })
                .Select(x => new
                {

                    IDSUB = x.Key.IDSUBKegiatan,
                    KelompokRekening = x.Key.KelompokRekening,

                    JenisRekening = x.Key.JenisRekening,

                    KodeRekening = x.Key.JenisRekening.ToString().ToKodeRekening(3),
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();





                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         on p.ID.ToString().Substring(0, lenKode) equals j.JenisRekening.ToString()

                         select new PerdaRealisasi_1_3
                         {

                             Kode = p.ID.ToString().ToKodeRekening(3),
                             KodeSubKegiatan = "",
                             KodeDinas = skpd.KodeSIPD,
                             IDSubKegiatan = j.IDSUB,
                             KodeRekening = p.ID.ToString().ToKodeRekening(3),
                             KelompokRekening = j.KelompokRekening,
                             JenisRekening = j.JenisRekening,
                             Nama = p.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_3> ProcessObjeckBelanja(List<AnggaranRealisasi> lst)
        {
            try
            {

                List<Rekening> lstRekening;

                lstRekening = m_lstRekening.FindAll(x => x.Root == 4);
                int lenKode = 6;
                foreach (Rekening r in lstRekening)
                {
                    Console.WriteLine(r.ID.ToString());
                }
                var lstJumlah = lst
                .GroupBy(c => new
                {
                    c.IDProgram,
                    c.IDKegiatan,
                    c.IDSUBKegiatan,
                    c.KelompokRekening,
                    c.JenisRekening,
                    c.ObjectRekening
                })
                .Select(x => new
                {

                    IDSUB = x.Key.IDSUBKegiatan,
                    KelompokRekening = x.Key.KelompokRekening,

                    JenisRekening = x.Key.JenisRekening,
                    ObjectRekening = x.Key.ObjectRekening,
                    KodeRekening = x.Key.ObjectRekening.ToString().ToKodeRekening(3),
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();





                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         on p.ID.ToString().Substring(0, lenKode) equals j.ObjectRekening.ToString()

                         select new PerdaRealisasi_1_3
                         {


                             Kode = j.ObjectRekening.ToString().ToKodeRekening(4),
                             IDSubKegiatan = j.IDSUB,
                             KodeSubKegiatan = "",
                             KodeRekening = j.ObjectRekening.ToString().ToKodeRekening(4),
                             KelompokRekening = j.KelompokRekening,
                             JenisRekening = j.JenisRekening,
                             ObjekRekening = j.ObjectRekening,
                             KodeDinas = skpd.KodeSIPD,
                             Nama = p.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_3> ProcessRincianObjeckBelanja(List<AnggaranRealisasi> lst)
        {
            try
            {

                List<Rekening> lstRekening;
                lstRekening = m_lstRekening.FindAll(x => x.Root == 5);
                int lenKode = 8;

                var lstJumlah = lst
                .GroupBy(c => new
                {
                    c.IDProgram,
                    c.IDKegiatan,
                    c.IDSUBKegiatan,
                    c.KelompokRekening,
                    c.JenisRekening,
                    c.ObjectRekening,
                    c.RincianObjectRekening
                })
                .Select(x => new
                {

                    IDSUB = x.Key.IDSUBKegiatan,
                    KelompokRekening = x.Key.KelompokRekening,
                    RincianObjekRekening = x.Key.RincianObjectRekening,
                    JenisRekening = x.Key.JenisRekening,
                    ObjectRekening = x.Key.ObjectRekening,
                    KodeRekening = x.Key.RincianObjectRekening.ToString().ToKodeRekening(3),
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();





                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         on p.ID.ToString().Substring(0, lenKode) equals j.RincianObjekRekening.ToString()

                         select new PerdaRealisasi_1_3
                         {


                             Kode = j.RincianObjekRekening.ToString().ToKodeRekening(5),
                             IDSubKegiatan = j.IDSUB,
                             KodeSubKegiatan = "",
                             KodeDinas = skpd.KodeSIPD,
                             KodeRekening = j.RincianObjekRekening.ToString().ToKodeRekening(5),
                             KelompokRekening = j.KelompokRekening,
                             JenisRekening = j.JenisRekening,
                             ObjekRekening = j.ObjectRekening,
                             RincianRekening = j.RincianObjekRekening,
                             Nama = p.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_3> ProcessSubRincianObjeckBelanja(List<AnggaranRealisasi> lst)
        {
            try
            {

                List<Rekening> lstRekening;
                lstRekening = m_lstRekening.FindAll(x => x.Root == 6);
                int lenKode = 12;
                // idsubrincian = idrekening
                var lstJumlah = lst
                .GroupBy(c => new
                {
                    c.IDProgram,
                    c.IDKegiatan,
                    c.IDSUBKegiatan,
                    c.KelompokRekening,
                    c.RincianObjectRekening,
                    c.IDRekening,
                    c.JenisRekening,
                    c.ObjectRekening
                })
                .Select(x => new
                {

                    IDSUB = x.Key.IDSUBKegiatan,
                    KelompokRekening = x.Key.KelompokRekening,

                    JenisRekening = x.Key.JenisRekening,
                    ObjectRekening = x.Key.ObjectRekening,
                    RincianObjectRekening = x.Key.RincianObjectRekening,
                    SubRincianObject = x.Key.IDRekening,
                    KodeRekening = x.Key.JenisRekening.ToString().ToKodeRekening(3),
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();





                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          lstRekening
                         join j in lstJumlah
                         on p.ID.ToString().Substring(0, lenKode) equals j.SubRincianObject.ToString()

                         select new PerdaRealisasi_1_3
                         {

                             KodeDinas = skpd.KodeSIPD,
                             Kode = j.SubRincianObject.ToString().ToKodeRekening(6),
                             IDSubKegiatan = j.IDSUB,
                             KodeRekening = p.ID.ToString().ToKodeRekening(3),
                             KodeSubKegiatan = "",
                             KelompokRekening = j.KelompokRekening,
                             JenisRekening = j.JenisRekening,
                             ObjekRekening = j.ObjectRekening,
                             RincianRekening = j.RincianObjectRekening,
                             Nama = p.Nama,
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,


                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_3> ProcessProgram(List<AnggaranRealisasi> lst, int level)
        {
            try
            {



                var distinctProgram = lst
                    .Select(m => new { m.IDProgram, m.NamaProgram })
                    .Distinct()
                    .ToList();

                var lstJumlah = lst
                      .GroupBy(c => new { c.IDProgram, c.NamaProgram })


                .Select(x => new
                {
                    IDProgram = x.Key.IDProgram,
                    NamaProgram = x.Key.NamaProgram,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          distinctProgram
                         join j in lstJumlah
                         on p.IDProgram equals j.IDProgram


                         select new PerdaRealisasi_1_3
                         {
                             Kode = "",
                             KodeRekening = "",
                             IDProgram = j.IDProgram,
                             IDKegiatan = 0,
                             KodeDinas = skpd.KodeSIPD,
                             IDSubKegiatan = 0,
                             KodeKategori = j.IDProgram.ToString().Substring(0, 1),
                             KodeUrusan = j.IDProgram.ToString().Substring(1, 3),
                             KodeKegiatan = "",
                             KodeSubKegiatan = "",
                             Nama = j.NamaProgram,
                             KodeProgram = j.IDProgram.ToKodeProgram(),
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,



                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private List<PerdaRealisasi_1_3> ProcessKegiatan(List<AnggaranRealisasi> lst, int level)
        {
            try
            {



                var distinctKegiatan = lst
                    .Select(m => new { m.IDProgram, m.IDKegiatan, m.NamaKegiatan })
                    .Distinct()
                    .ToList();

                var lstJumlah = lst
                      .GroupBy(c => new { c.IDProgram, c.IDKegiatan, c.NamaKegiatan })


                .Select(x => new
                {
                    IDProgram = x.Key.IDProgram,
                    IDKegiatan = x.Key.IDKegiatan,
                    NamaKegiatan = x.Key.NamaKegiatan,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          distinctKegiatan
                         join j in lstJumlah
                         on p.IDKegiatan equals j.IDKegiatan


                         select new PerdaRealisasi_1_3
                         {

                             KodeRekening = "",
                             IDProgram = j.IDProgram,
                             IDKegiatan = j.IDKegiatan,
                             KodeDinas = skpd.KodeSIPD,
                             IDSubKegiatan = 0,
                             KodeKategori = j.IDProgram.ToString().Substring(0, 1),
                             KodeUrusan = j.IDProgram.ToString().Substring(1, 3),
                             KodeKegiatan = j.IDKegiatan.ToKodeKegiatan(),
                             KodeSubKegiatan = j.IDKegiatan.ToKodeKegiatan(),
                             KodeProgram = j.IDProgram.ToKodeProgram(),
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             Nama = j.NamaKegiatan,



                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private List<PerdaRealisasi_1_3> ProcessSubKegiatan(List<AnggaranRealisasi> lst, int level)
        {
            try
            {



                var distinctSubKegiatan = lst
                    .Select(m => new { m.IDProgram, m.IDKegiatan, m.IDSUBKegiatan, m.NamaSubKegiatan })
                    .Distinct()
                    .ToList();

                var lstJumlah = lst
                      .GroupBy(c => new { c.IDProgram, c.IDKegiatan, c.IDSUBKegiatan, c.NamaSubKegiatan })


                .Select(x => new
                {
                    IDProgram = x.Key.IDProgram,
                    IDKegiatan = x.Key.IDKegiatan,
                    IDSubKegiatan = x.Key.IDSUBKegiatan,
                    NamaSubKegiatan = x.Key.NamaSubKegiatan,
                    Anggaran = x.Sum(y => y.Anggaran),
                    Realisasi = x.Sum(y => y.Realisasi),
                }).ToList();
                List<PerdaRealisasi_1_3> lstKi = new List<PerdaRealisasi_1_3>();


                lstKi = (from p in
                          distinctSubKegiatan
                         join j in lstJumlah
                         on p.IDSUBKegiatan equals j.IDSubKegiatan


                         select new PerdaRealisasi_1_3
                         {

                             KodeSubKegiatan = j.IDSubKegiatan.ToKodeSubKegiatan(),

                             KodeRekening = "",
                             IDProgram = j.IDProgram,
                             IDKegiatan = j.IDKegiatan,
                             IDSubKegiatan = j.IDSubKegiatan,
                             KodeKategori = j.IDProgram.ToString().Substring(0, 1),
                             KodeUrusan = j.IDProgram.ToString().Substring(1, 3),
                             KodeKegiatan = j.IDKegiatan.ToKodeKegiatan(),
                             KodeDinas = skpd.KodeSIPD,
                             KodeProgram = j.IDProgram.ToKodeProgram(),
                             Anggaran = j.Anggaran,
                             Realisasi = j.Realisasi,
                             Nama = j.NamaSubKegiatan,



                         }).ToList<PerdaRealisasi_1_3>();




                return lstKi;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
