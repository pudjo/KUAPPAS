
namespace DTO.Akuntansi
{
    public class PerdaRealisasi_1
    {
        public string Kode { set; get; }
        public string KodeUrusan { set; get; }
        public int IDDinas { set; get; }
        public string KodeSKPD { set; get; }
        public string Nama { set; get; }
        public decimal Anggaran { set; get; }
        public decimal Realisasi { set; get; }
        public int IdDinas { set; get; }
        public string KodeRekening { set; get; }

        public int IDUrusan { set; get; }
        public int Bold { set; get; }
        public int Garis { set; get; }
        public decimal Selisih { set; get; }
        public string Persen { set; get; }
    }
}
