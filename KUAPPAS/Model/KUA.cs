using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class KUA
    {
        public int Tahun { set; get; }
        public int ID { set; get; }
        public int IDDinas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDKegiatan { set; get; }
        public long IDSubKegiatan { set; get; }

        public int KodeKategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUK { set; get; }
        public int KodeKategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public long KodeRekening { set; get; }
        public decimal JumlahOlah { set; get; }
        public decimal JumlahMurni { set; get; }
        public decimal JumlahRKPD { set; get; }
        public decimal JumlahPerubahan { set; get; }
        public string NamaKegiatan { set; get; }
        public string NamaUsulan { set; get; }
        public int Kecamatan { set; get; }
        public int Desa { set; get; }
        public int Dusun { set; get; }
        public string KeteranganLokasi { set; get; }
        public int IDLokasi { set; get; }
        public int Jenis { set; get; }
        public int IDUrusanMaster { set; get; }
        public int IDProgramMaster { set; get; }
        public int IDKegiatanMaster { set; get; }
        public long IDSubKegiatanMaster { set; get; }
        public int UserID { set; get; }
        public int NoUrut { set; get; }
        public int PPKD { set; get; }
        public string namaprogram { set; get; }
        public string namaKegiatan { set; get; }


        public string ToStrin()
        {
            return ID.ToString() + "|" + IDKegiatan.ToString() + "|" + NoUrut.ToString() + "|" + NamaUsulan + "|" + JumlahOlah.ToString() + "|" + JumlahRKPD.ToString() +
                    "|" + Kecamatan.ToString() + "|" + Kecamatan.ToString() + "|" + Desa.ToString() + "|" + Dusun;
        }

        //public int Tahun { set; get; }
        //public int ID  { set; get; }
        //public int IDDinas { set; get; }
        //public int IDUrusan { set; get;}
        //public int IDProgram { set; get; }
        //public long  IDKegiatan { set; get; }
        //public int KodeKategori { set; get; }
        //public int KodeUrusan { set; get; }
        //public int KodeSKPD { set; get; }
        //public int KodeUK { set; get; }
        //public int KodeKategoriPelaksana { set; get; }
        //public int KodeUrusanPelaksana { set; get; }
        //public int KodeProgram { set; get; }
        //public int KodeKegiatan { set; get; }
        //public long KodeRekening { set; get; }
        //public decimal JumlahOlah { set; get; }
        //public decimal JumlahMurni { set; get; }
        //public decimal JumlahRKPD { set; get; }
        //public decimal JumlahPerubahan { set; get; }
        //public string NamaKegiatan { set; get; }
        //public string NamaUsulan { set; get; }
        //public int Kecamatan { set; get; }
        //public int Desa { set; get; }
        //public int  Dusun { set; get; }
        //public string KeteranganLokasi { set; get; }
        //public int IDLokasi { set; get; }
        //public int Jenis { set; get; }
        //public int IDUrusanMaster { set; get; }
        //public int IDProgramMaster { set; get; }
        //public int IDKegiatanMaster { set; get; }
        //public int UserID { set; get; }
        //public int NoUrut { set; get; }
        //public int PPKD { set; get; }
        //public string namaprogram { set; get; }

        //public string ToStrin()
        //{
        //    return ID.ToString() +"|" +IDKegiatan.ToString()+"|"+ NoUrut.ToString()+ "|"+ NamaUsulan + "|" + JumlahOlah.ToString() + "|" + JumlahRKPD.ToString() +
        //            "|" + Kecamatan.ToString() + "|" + Kecamatan.ToString() + "|" + Desa.ToString() + "|" + Dusun;
        //}


    }
}
