using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class RekapPerJenis
    {
        public int Level { set; get; }
        public string Kode { set; get; }
        public int IDDInas { set; get; }
        public int IDUrusan { set; get; }
        public int IDProgram { set; get; }
        public int IDkegiatam { set; get; }
        public int IDRekening { set; get; }
        public decimal BelanjaLangsung { set; get; }
        public decimal BelanjaPegawai { set; get; }
        public decimal BelanjaBarangJasa { set; get; }
        public decimal BelanjaModal { set; get; }
        public decimal BelanjaTidakLangsung { set; get; }
        public decimal BelanjaLangsungPagu { set; get; }
        public decimal BelanjaPegawaiPagu { set; get; }
        public decimal BelanjaBarangJasaPagu { set; get; }
        public decimal BelanjaModalPagu { set; get; }
        public decimal BelanjaTidakLangsungPagu { set; get; }       
        public string Nama { set; get; }
        public Single Jenis { set; get; }
        public string Input { set; get; }
        public string Pagu { set; get; }
        public string Selisih { set; get; }
        public string PaguABT { set; get; }

    }
}
