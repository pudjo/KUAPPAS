using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine._511
{
    public class DataInformasi511Response
    {  public string nomorSP2D { set; get; } 
    public string nomorSPM { set; get; } 
    public string tanggalTransaksi { set; get; } 
    public string referenceNo { set; get; } 
    public string notes { set; get; } 
    public string responseCode { set; get; } 
    public string messageDetail { set; get; }

    public DataPengirim pengirim { set; get; }
    public DataPenerima penerima { set; get; }

    public string jumlahNominalTransaksi { set; get; } 
    public string jumlahPotonganMpn { set; get; } 
    public string jumlahPotonganNonMpn { set; get; } 
    public string jumlahDibayar { set; get; } 
    public List<DetailMpn511> detailPotonganMpn { set; get; }
    public List<DetailPotonganNonMpn> detailPotonganNonMpn { set; get; }
}
   public class DataPengirim
{
    public string noRekening { set; get; } 
    public string kodeOpd { set; get; } 
    public string namaOpd { set; get; } 
}
    public class DataPenerima
{
    public string kodeBank { set; get; }
    public string namaBank { set; get; } 
    public string noRekening { set; get; } 
    public string namaPenerima { set; get; } 
    public string npwp { set; get; }
}
public class DetailMpn511
{
    public string idBilling { set; get; } 
    public string referenceNo { set; get; } 
    public string keteranganPotongan { set; get; } 
    public string nominalPotongan { set; get; } 
    public string ntpn { set; get; } 
    public string responseCode { set; get; } 
    public string messageDetail { set; get; } 
}
public class DetailPotonganNonMpn
{
    public string kodeMap { set; get; } 
    public string keteranganKodeMap { set; get; } 
    public string nominalPotongan { set; get; } 
    public string responseCode { set; get; } 
    public string messageDetail { set; get; } 

}
public class DataInformasi511ResponseEx: DataInformasi511Response
{
    public string error_kode { set; get; } 
    public string message { set; get; }

}
}
