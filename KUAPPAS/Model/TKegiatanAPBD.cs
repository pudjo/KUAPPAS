using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    //BEda dengan TKEGIATAN 
    // TKegiatan datanya da di KUA. 
    // Kegiatan di KUA akan di export ke sini pada saatKUA di syahkan

    public class TKegiatanAPBD
    {
        public Single Tahun { set; get; }
        public int IDDinas { set; get; }
        public int IDUnit{ set; get; }

        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public int IDUrusanMaster { set; get; }
        public int IDProgramMaster { set; get; }
        public int IDKegiatanMaster { set; get; }


        public int KodeKegiatan { set; get; }
        public string Nama { set; get; }
        public decimal Pagu { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }

        public string Nama2 { set; get; }

        public decimal Plafon { set; get; }
        public decimal PlafonABT { set; get; }

        public string Lokasi { set; get; }//sLokasi
        public string Sifat { set; get; }//sSifat
        public string Kondisi { set; get; }//sKondisi
        public string Waktu { set; get; }//sWaktu
        public string NamaPPTK { set; get; }//sNamaPPTK
        public string JabatanPPTK { set; get; }//sJabatanPPTK
        public string NIPPPTK { set; get; }//sNIPPPTK


        public DateTime TanggalPembahasan { set; get; }// dtPembahasan
        public string Keterangan { set; get; }//sKeterangan
        public Single TahapInput { set; get; }//btTahapInput
        public decimal AnggaranTahunDepan { set; get; }//cAnggaranTahunDepan
        public decimal AnggaranTahunLalu { set; get; }//cAnggaranTahunLalu
        public decimal APBDPTahunLalu { set; get; }//cAnggaranTahunLalu
        public decimal Realisasi { set; get; }//cAnggaranTahunLalu

        public string KelompokSasaran { set; get; }//sKelompokSasaran
        public string SumberDana { set; get; }//iSumberDana
        public int TahunAwal { set; get; }//iTahunAwal
        public int ApakahLanjutan { set; get; }//bLanjutan
        public string NamaSumberDana { set; get; }//       sSumberDana
        public string AlasanPerubahan { set; get; }//sAlasanPerubahan


        public long ID { set; get; }//ID
        public Single Jenis { set; get; }
        public Single Tahap { set; get; }// btTahap 
        public bool PunyaSubKegiatan { set; get; }//btIDSubKegiatan
        public List<Indikator> ListIndikator { set; get; }
        public List<CatatanKegiatan> ListCatatan { set; get; }
        public string TampilanKode { set; get; }
        public List<SumberDana> ListSumberDana { set; get; }

        public string JumlahPagu { set; get; }
        public string JumlahDiInput { set; get; }
        public int KodeProgram { set; get; }
        public decimal PaguABT { set; get; }
        public string KodePendek { set; get; }
        public string Outcome { set; get; }
        public string Keluaran { set; get; }



        //////public Single Tahun { set; get; }
        //////public int IDDinas { set; get; }
        //////public int IDUrusan { set; get; }
        //////public int IDProgram { set; get; }
        //////public int IDKegiatan { set; get; }
        //////public int KodeKegiatan { set; get; }
        //////public string Nama { set; get; }
        //////public decimal Pagu { set; get; }
        //////public int KodeKategoriPelaksana { set; get; }
        //////public int KodeUrusanPelaksana { set; get; }
        //////public int KodeKategori { set; get; }
        //////public int KodeUrusan { set; get; }
        //////public int KodeSKPD { set; get; }
        //////public int KodeUK { set; get; }

        //////public string Nama2 { set; get; }

        //////public decimal Plafon { set; get; }

        //////public string Lokasi { set; get; }//sLokasi
        //////public string Sifat { set; get; }//sSifat
        //////public string Kondisi { set; get; }//sKondisi
        //////public string Waktu { set; get; }//sWaktu
        //////public string NamaPPTK { set; get; }//sNamaPPTK
        //////public string JabatanPPTK { set; get; }//sJabatanPPTK
        //////public string NIPPPTK { set; get; }//sNIPPPTK


        //////public DateTime TanggalPembahasan { set; get; }// dtPembahasan
        //////public string Keterangan { set; get; }//sKeterangan
        //////public Single TahapInput { set; get; }//btTahapInput
        //////public decimal AnggaranTahunDepan { set; get; }//cAnggaranTahunDepan
        //////public decimal AnggaranTahunLalu { set; get; }//cAnggaranTahunLalu
        //////public decimal APBDPTahunLalu { set; get; }//cAnggaranTahunLalu

        //////public string KelompokSasaran { set; get; }//sKelompokSasaran
        //////public string SumberDana { set; get; }//iSumberDana
        //////public int TahunAwal { set; get; }//iTahunAwal
        //////public int ApakahLanjutan { set; get; }//bLanjutan
        //////public string NamaSumberDana { set; get; }//       sSumberDana
        //////public string AlasanPerubahan { set; get; }//sAlasanPerubahan


        //////public long ID { set; get; }//ID
        //////public Single Jenis { set; get; }
        //////public Single Tahap { set; get; }// btTahap 
        //////public bool PunyaSubKegiatan { set; get; }//btIDSubKegiatan
        //////public List<Indikator> ListIndikator { set; get; }
        //////public List<CatatanKegiatan> ListCatatan { set; get; }
        //////public string TampilanKode { set; get; }
        //////public List<SumberDana> ListSumberDana { set; get; }

        //////public string JumlahPagu { set; get; }
        //////public string JumlahDiInput { set; get; }
        //////public int KodeProgram { set; get; }
        //////public decimal PaguABT { set; get; }



        //public Single Tahun { set; get; }
        //public int IDDinas { set; get; }
        //public int IDUrusan { set; get; }        
        //public int IDProgram { set; get; }
        //public int IDKegiatan { set; get; }       
        //public int KodeKegiatan { set; get; }      
        //public string Nama { set; get; }
        //public decimal Pagu { set; get; }
        //public int KodeKategoriPelaksana { set; get; }
        //public int KodeUrusanPelaksana { set; get; }
        //public int KodeKategori { set; get; }
        //public int KodeUrusan { set; get; }
        //public int KodeSKPD { set; get; }
        //public int KodeUK { set; get; }
        
        //public string Nama2 { set; get; }
        
        //public decimal Plafon { set; get; }
        
        //public string Lokasi  { set; get; }//sLokasi
        //public string Sifat  { set; get; }//sSifat
        //public string  Kondisi { set; get; }//sKondisi
        //public string Waktu { set; get; }//sWaktu
        //public string  NamaPPTK { set; get; }//sNamaPPTK
        //public string  JabatanPPTK { set; get; }//sJabatanPPTK
        //public string  NIPPPTK { set; get; }//sNIPPPTK
        

        //public DateTime TanggalPembahasan { set; get; }// dtPembahasan
        //public string Keterangan { set; get; }//sKeterangan
        //public Single TahapInput { set; get; }//btTahapInput
        //public decimal AnggaranTahunDepan { set; get; }//cAnggaranTahunDepan
        //public decimal AnggaranTahunLalu { set; get; }//cAnggaranTahunLalu
        //public string  KelompokSasaran { set; get; }//sKelompokSasaran
        //public string SumberDana  { set; get; }//iSumberDana
        //public int TahunAwal  { set; get; }//iTahunAwal
        //public Single ApakahLanjutan { set; get; }//bLanjutan
        //public string NamaSumberDana  { set; get; }//       sSumberDana
        //public string   AlasanPerubahan  { set; get; }//sAlasanPerubahan

        //public decimal APBDPTahunLalu { set; get; }//cAnggaranTahunLalu

        //public long ID { set; get; }//ID
        //public Single Jenis { set; get; }
        //public Single  Tahap { set; get; }// btTahap 
        //public bool PunyaSubKegiatan { set; get; }//btIDSubKegiatan
        //public List<Indikator> ListIndikator { set; get; }
        //public List<CatatanKegiatan> ListCatatan { set; get; }
        //public string TampilanKode { set; get; }
        ////public string Nama2{ set; get; }
        //public string JumlahPagu { set; get; }
        //public string JumlahDiInput { set; get; }
        //public int KodeProgram { set; get; }





    }
}
