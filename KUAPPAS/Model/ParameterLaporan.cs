using KUAPPAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ParameterLaporan
    {
        public int  Tahun { set; get; }
        public int Tahap { set; get; }
        public int IDDinas { set; get; }

        public string KodeUrusan { set; get; }
        public string NamaPerda { set; get; }

        public string NamaLaporan { set; get; }
        public string KodeOrganisasi { set; get; }
        public string NamaDinas { set; get; }
        public string NamaUrusan { set; get; }
        public string Tanggal { set; get; }
        public string JabatanPimpinan { set; get; }
        public string Keterangan { set; get; }
        public string Jumlah { set; get; }
        public string sTanggal { set; get; }
        public DateTime dTanggal { set; get; }
        public DateTime TanggalRealisasi { set; get; }
        public Single LastLevel { set; get; }
        public int  Jenis { set; get; }
        public int JenisAnggaran { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }
        public Single DenganPaket { set; get; }
        
        public bool bKegiatanKhusus { set; get; }
        public string NamaUser { set; get; }
        public int AwalHalaman { set; get; }
        public int bPPKD { set; get; }
        public int JenisRekening { set; get; }
        public bool  pakaiTandaTangan { set; get; }
        public bool DenganTanggal { set; get; }
        public int IDunit { set; get; }
        public Pejabat pimpinan { set; get; }
        public Pejabat Bendahara { set; get; }
        public SKPD skpd { set; get; }
        public ReportDesign Rd { set; get; }
        public string Tempat { set; get; }
        public string Nomor { set; get; }
        public Pejabat Penandatangan { set; get; }

        public ParameterLaporan()
        {
            Tahun = GlobalVar.TahapAnggaran;
            Tahap = 1;
        IDDinas = 0;
            Penandatangan = new Pejabat();
            KodeUrusan = "0";
            NamaPerda ="";
            sTanggal = "";
            NamaLaporan = "";
            KodeOrganisasi = "";
        NamaDinas = "";
            NamaUrusan = "";
            
        JabatanPimpinan = "";
            Keterangan = "";
            Jumlah = "";

            LastLevel = 6;
            Jenis = 0;
            JenisAnggaran = 0;
            IDUrusan = 0;
            IDProgram = 0;
        IDKegiatan = 0;
            IDSubKegiatan = 0;
            DenganPaket = 0;

            
            bKegiatanKhusus = false; 
            NamaUser ="";
            AwalHalaman = 1;
            bPPKD = 0;
            JenisRekening = 1;
            pakaiTandaTangan = false;
            DenganTanggal = false;
            IDunit = 0;
            Rd = new ReportDesign();
            Tempat = GlobalVar.gPemda.Ibukota;

    }


    }
}
