using DTO;
using DTO.Akuntansi;
using Formatting;
using KUAPPAS;
using KUAPPAS.DTO.Akuntansi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace BP.Akuntansi
{
    public class PerkdaRealisasiRinciLogic : BP
    {
        public int m_iddinas;
        SKPD skpd;
        List<Rekening> m_lstRekening;
        List<Urusan> m_lstUrusan;


        private string sIDUrusan;
        private int m_iJenis;
        
            
        public PerkdaRealisasiRinciLogic(int tahun) : base(tahun)
        {
            m_lstRekening = new List<Rekening>();
            m_lstUrusan = new List<Urusan>();
        }



       
       
        private List<AnggaranRealisasi> GetAnggaranRealisasi(int tahun)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();
                AnggaranRealisasiLogic ologic = new AnggaranRealisasiLogic(tahun);
                lst = ologic.GetAnggaranRealisasiRinci(tahun);
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
        
        
        
        public List<RealisasiRinci> Process()
        {
            try
            {

                
                List<RealisasiRinci> lstPerda = new List<RealisasiRinci>();
                UrusanLogic uLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
                
                m_lstUrusan = new List<Urusan>();
                m_lstUrusan = uLogic.Get();
                if (m_lstUrusan == null)
                {
                    _lastError = "Tidak bisa memangggil Urusan Pemerintahan";
                    return null;
                }                
                if (LoadRekening() == false)
                {
                    _lastError = "Gagal memanggil Rekening";
                    return null;
                }

                lstPerda = ProccessBelanja();
                

                return lstPerda;




            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
        }


        #region ProsesBelanja
        private List<RealisasiRinci> ProccessBelanja()
        {
            try
            {
                List<RealisasiRinci> lstPerda = new List<RealisasiRinci>();
                List<AnggaranRealisasi> lst = GetAnggaranRealisasi(Tahun);

                List<Rekening> lstKelomokRekening = m_lstRekening.FindAll(x => x.Root == 2);
                List<Rekening> lstJenisRekening = m_lstRekening.FindAll(x => x.Root == 3);
                List<Rekening> lstObjectRekening = m_lstRekening.FindAll(x => x.Root == 4);
                List<Rekening> lstRincianObjectRekening = m_lstRekening.FindAll(x => x.Root == 5);
                List<Rekening> lstSubRincianObjectRekening = m_lstRekening.FindAll(x => x.Root == 6);
                lstPerda = new List<RealisasiRinci>();


                lstPerda = (from l in
                          lst
                         join k in lstKelomokRekening
                         on l.KelompokRekening.ToString().Substring(0, 2) equals k.ID.ToString().Substring(0, 2)
                            join j in lstJenisRekening
                            on l.JenisRekening.ToString().Substring(0, 4) equals j.ID.ToString().Substring(0, 4)
                            join o in lstObjectRekening
                            on l.ObjectRekening.ToString().Substring(0, 6) equals o.ID.ToString().Substring(0, 6)
                            join ro in lstRincianObjectRekening
                            on l.RincianObjectRekening.ToString().Substring(0, 8) equals ro.ID.ToString().Substring(0, 8)
                            join sro in lstSubRincianObjectRekening
                            on l.IDRekening.ToString() equals sro.ID.ToString()
                            join u in m_lstUrusan
                            on l.IDDinas.ToString().Substring(0,3) equals u.ID.ToString()
                            select new RealisasiRinci
                            
                         {
                               
                                NamaUrusanBidang= u.Nama,
                                NamaDinas= l.NamaDinas,
                                NamaUnitDinas= l.NamaUnit,
                                NamaUrusan= l.NamaUrusan,
                                NamaProgram=l.NamaProgram,
                                NamaKegiatan= l.NamaKegiatan,
                                NamaSubKegiatan= l.NamaSubKegiatan,
                                NamaAkun=k.Nama,
                                NamaKelompok=k.Nama,
                                NamaJenis= j.Nama,
                                NamaObject=o.Nama,
                                NamaRincianObject= ro.Nama,
                                NamaSubRincianObject=sro.Nama,
                                Anggaran = l.Anggaran,
                                Realisasi = l.Realisasi


                         }).ToList<RealisasiRinci>();
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

    }
}
