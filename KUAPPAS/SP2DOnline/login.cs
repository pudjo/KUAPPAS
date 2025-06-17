using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP2DOnline
{
     [Serializable]
    public class TrxResponse
    {
        public string Kode { set; get; }
       // public string 
        public string Authorization { set; get; }
        public string Bearer { set; get; }

    }
     [Serializable]
    public class TrxLogin
    {
        public string username { set; get; }
        public string password { set; get; }

    }
     [Serializable]
    
     public class loginresponse
     {
         public bool success { set; get; }
         public string message { set; get; }

     }
     [Serializable]
    public class Balances
    {
        public bool success { set; get; }
        public string message { set; get; }
        public string balance { set; get; }

    }

    //[Serializable]
    // public class Banks
    // {
    //     public string accountNumber { set; get; }
    //     public string bankCode { set; get; }
    //     public string bic { set; get; }
    //     public string participantName { set; get; }

    // }
     [Serializable]
    public class Kodebic
    {
         public string bic { set; get; }
         public string name { set; get; }
         public string shortName { set; get; }
         public string cityCode { set; get; }
         public string postalCode { set; get; }
         public string address { set; get; }
        
    }


     [Serializable]

     public class InquiriesRequest
     {
         public string sandiBank { set; get; }
         public string nomorRekening { set; get; }


        
     }

     [Serializable]

     public class InquiriesRespond
     {
         public string response_code {set;get;}
         public string message { set; get; }
         public dataInquiriesRespond data { set; get; }
         
     }

     [Serializable]
     public class dataInquiriesRespond
     {
         public string nomorRekening { set; get; }
         public string namaPemilikRekening { set; get; }


     }

     [Serializable]

     public class InquiriesNPWPRequest
     {
        public string  kodeMap { set; get; }
        public string  kodeSetor{ set; get; }
        public string nomorPokokWajibPajak { set; get; }


     }

     [Serializable]

     public class InquiriesNPWPRespond
     {
         public string response_code { set; get; }
         public string message { set; get; }
         public dataInquiriesNPWPRespond data { set; get; }

     }

     [Serializable]
     public class dataInquiriesNPWPRespond
     {
        public string  nomorPokokWajibPajak{ set; get; }
        public string namaWajibPajak{ set; get; }
        public string alamatWajibPajak{ set; get; }
        public string kota{ set; get; }
        public string kodeMap{ set; get; }
        public string kodeSetor{ set; get; }
        public string keteranganKodeMap{ set; get; }
        public string keteranganKodeSetor { set; get; }


     }

    /// <summary>
    /// /  INQUIRY ID BILING 
    /// </summary>

     public class InquiriesIdBillingRequest
     {
         public string idBilling { set; get; }


     }

     [Serializable]

     public class InquiriesIdBillingRespon
     {
         public string response_code { set; get; }
         public string message { set; get; }
         public dataInquiriesIdBillingRespon data { set; get; }

     }

     [Serializable]
     public class dataInquiriesIdBillingRespon
     {
         
        public string statusTransaksi{set;get;}
        public string idBilling{set;get;}
        public string ntpn{set;get;}
        public string ntb{set;get;}
        public string jenisPajak{set;get;}
        public string tanggalDanWaktuTransaksi{set;get;}
        public string tanggalBuku{set;get;} //yyyy-MM-dd
        public string nominal{set;get;} //rupiah penuh tampa koma
        public string nomorPokokWajibPajak{set;get;}
        public string namaWajibPajak{set;get;}
        public string alamatWajibPajak{set;get;}
        public string kodeMap{set;get;}
        public string kodeSetor{set;get;}
        public string masaPajak{set;get;} //MMMM
        public string tahunPajak{set;get;} //yyyy
        public string nomorSk{set;get;}
        public string nomorObjekPajak{set;get;}
        public string idWajibBayar{set;get;} //khusus DJBC
        public string jenisDokumen{set;get;} //khusus DJBC
        public string nomorDokumen{set;get;} //khusus DJBC
        public string tanggalDokumen{set;get;} //yyyy-MM-dd //khusus DJBC


     }
    //
    //
    // END OF INQUIRY IDBILLING 


     /// <summary>
     /// CREATE ID BILING 
     /// </summary>

     public class CreateIdBillingRequest
     {
         public string nomorPokokWajibPajak { set; get; } //mandatory field
         public string kodeMap { set; get; } //mandatory field
         public string kodeSetor { set; get; } //mandatory field
         public string masaPajak { set; get; } //mandatory field | MMMM
         public string tahunPajak { set; get; } //mandatory field | yyyy
         public string jumlahBayar { set; get; } //mandatory field
         public string nomorObjekPajak { set; get; }
         public string nomorSK { set; get; }
         public string nomorPokokWajibPajakPenyetor { set; get; } //mandatory field
         public string namaWajibPajak { set; get; }
         public string alamatWajibPajak { set; get; }
         public string kota { set; get; }
         public string nik { set; get; }
         public string nomorPokokWajibPajakRekanan { set; get; }
         public string nikRekanan { set; get; }
         public string nomorFakturPajak { set; get; }
         public string nomorSKPD { set; get; } //6digit kode Pemda + max 15digit kode OPD/SKPD
         public string nomorSPM { set; get; }

     }

     [Serializable]

     public class CreateIdBillingRespon
     {
         public string response_code { set; get; }
         public string message { set; get; }
         public dataCreateIdBillingRespon data { set; get; }

     }

     [Serializable]
     public class dataCreateIdBillingRespon
     {


         public string nomorPokokWajibPajak { set; get; }
         public string namaWajibPajak { set; get; }
         public string alamatWajibPajak { set; get; }
         public string kota { set; get; }
         public string nik { set; get; }
         public string kodeMap { set; get; }
         public string kodeSetor { set; get; }
         public string masaPajak { set; get; }
         public string tahunPajak { set; get; }
         public string jumlahBayar { set; get; }
         public string nomorObjekPajak { set; get; }
         public string nomorSK { set; get; }
         public string nomorPokokWajibPajakPenyetor { set; get; }
         public string nomorPokokWajibPajakRekanan { set; get; }
         public string nikRekanan { set; get; }
         public string nomorFakturPajak { set; get; }
         public string nomorSKPD { set; get; }
         public string nomorSPM { set; get; }
         public string idBilling { set; get; }
         public string tanggalExpiredBilling { set; get; }
         public string keteranganKodeMap { set; get; }
         public string keteranganKodeSetor { set; get; }




     }
    // END Create IDBilling 


     /// <summary>
     /// 5.5 Inquiry Payment MPN
     /// </summary>

     public class InquiryMPN
     {
         
        public string  idBilling { set; get; }
        public string  reInquiry { set; get; } // "true" | "false" | mandatory field
        public string referenceNo { set; get; } //terdiri 4 digit parameter user 8 digit sequence

     }

     [Serializable]

     public class InquiryMPNespon
     {
         public string response_code { set; get; }
         public string message { set; get; }
         public dataCreateIdBillingRespon data { set; get; }

     }

     [Serializable]
     public class DataInquiryMPNespon
     {


         public string idBilling { set; get; }
         public string nomorSP2D { set; get; }
         public string statusTransaksi { set; get; }
         public string jumlahBayar { set; get; }
         public string referenceNo { set; get; }
         public string jenisPajak { set; get; } //DJP | DJA | DJBC
         public string namaWajibPajak { set; get; }
         public string ntpn { set; get; }
         public string tanggalDanWaktuTransaksi { set; get; }//yyyy-MM-dd HH:mm:ss
         public string tanggalBuku { set; get; } //yyyy-MM-dd
         public string waktuBuku { set; get; } //HH:mm:ss
         public string msgSTAN { set; get; }
         public string nomorPokokWajibPajak { set; get; }
         public string alamatWajibPajak { set; get; }
         public string kodeMap { set; get; }
         public string kodeSetor { set; get; }
         public string masaPajak { set; get; }
         public string tahunPajak { set; get; }
         public string nomorSk { set; get; }
         public string nomorObjekPajak { set; get; }
         public string kementrianLembaga { set; get; }
         public string unitEselonI { set; get; }
         public string kodeSatker { set; get; }




     }
     // END Inquiry MPN 
    //
    //
    /// <summary>
    /// Generate Report 
    /// </summary>

     [Serializable]
     public class GenerateReportRequest {

         public string noReferensi { set; get; }
         public string jenisReport { set; get; }
         public string tanggalReportAwal { set; get; } //"yyyy-MM-dd HH:mm:ss"
         public string tanggalReportAkhir { set; get; } //"yyyy-MM-dd HH:mm:ss"
         public string formatReport { set; get; }
     }
     [Serializable]

     public class GenerateReportRespon
     {
         public string response_code { set; get; }
         public string message { set; get; }
         public DataGenerateReportRespon data { set; get; }

     }
    [Serializable]
    public class DataGenerateReportRespon{

        public string noReferensi { set; get; }
        public string jenisReport { set; get; }
        public string formatReport { set; get; }
        public string tanggalReportAwal { set; get; }
        public string tanggalReportAkhir { set; get; }
        public string tanggalExpiredLink { set; get; }
        public string linkDownload { set; get; }

    }

    /// <summary>
    /// <![CDATA[ ]]>
    /// End of generate report 
    /// </summary>

    ///
    /// Detail transaksi SP2D Online
    /// 

    [Serializable ]
    public class PengirimSP2Donline
    {
        public string  noRekening{ set; get; }
        public string kodeOpd{ set; get; }
        public string namaOpd { set; get; }

    }

    [Serializable]
    public class PenerimaSP2DOnline
    {
        public string   kodeBank{ set; get; }
        public string  namaBank{ set; get; }
        public string  noRekening{ set; get; }
        public string  namaPenerima{ set; get; }
        public string  npwp{ set; get; }

    }
    [Serializable]
    public class PotonganMPN
    {
        public string   idBilling{ set; get; }
        public string   referenceNo{ set; get; }
        public string   keteranganPotongan{ set; get; }
        public string nominalPotongan { set; get; }

    }

    [Serializable]

    public class PotonganNonMPN
    {
        public string   kodeMap{ set; get; }
        public string   keteranganKodeMap{ set; get; }
        public string nominalPotongan { set; get; }

    }

    [Serializable]

    public class SP2DOnLineRequest
    {
       
        public string nomorSP2D{ set; get; }
        public string nomorSPM{ set; get; }
        public string tanggalTransaksi{ set; get; }//yyyy-MM-dd
        public string referenceNo{ set; get; }
        public string kodeJenisTransaksi{ set; get; }//Transfer-OnUs||Transfer-SKN||Transfer-RTGS
        public string notes { set; get; }//optional
        public PengirimSP2Donline pengirim { set; get; }//optional
        public PenerimaSP2DOnline penerima { set; get; }
        public string jumlahNominalTransaksi{ set; get; }
        public string jumlahPotonganMpn{ set; get; }
        public string jumlahPotonganNonMpn{ set; get; }
        public string jumlahDibayar{ set; get; }
        
        public List<PotonganMPN> detailPotonganMpn { set; get; }
        public List<PotonganNonMPN> detailPotonganNonMpn { set; get; }



    }

    public class SP2DOnLineRespon
    {
        public string tx_id { set; get; }
        public string nomorSP2D { set; get; }
        public string nomorSPM { set; get; }
    }


    /// <summary>
    /// END of SP2D online 
    /// </summary>
     [Serializable]

     public class OTP
     {
         public int jumlahTransaksi { set; get; }
         public string releaserPassword { set; get; }
         public string releaserUsername { set; get; }

     }





    [Serializable]

    public class Account
    {
       
        public string acountNo { set; get; }
        public string accountName { set; get; }
      //  public string bankCode { set; get; }


    }
  
     [Serializable]
    public class TransferOnUs
    {
        public string signature { set; get; }
        public string amount { set; get; }
        public string beneficiaryAccountNo { set; get; }
        public string beneficiaryName { set; get; }

        public string reference{ set; get; }
        public string description{ set; get; }
        public string email{ set; get; }
        public string phoneNo { set; get; }
        public string pphAmount { set; get; }
        public string ppnAmount { set; get; }
        public string totalAmount { set; get; }
        public string timestamp { set; get; }

        public string GetToSignature(string username , string key, string otp)
        {
            string keyandTimeStamp= key+ otp;
            string localTimeStamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
           timestamp = localTimeStamp.Replace("AM","").Replace("PM","");
            
            string stToHash = timestamp + keyandTimeStamp;
          //  DESEncrypt.CalculateSHA256(stToHash);

            string hash = "";// DESEncrypt.CalculateSHA256(stToHash);//, Hashing.HashingTypes.SHA256);
         //   hash = "EDE585B9319B61A85890CF886A8B175194135171D3A10200264F6787736A7B69";
            return  amount + beneficiaryAccountNo + beneficiaryName + reference + description +
                  email+phoneNo+pphAmount +ppnAmount +totalAmount+hash;





        }
    }
     [Serializable]
    public class RTGS
    {
                
          public string amount { set; get; }
          public string beneficiaryAccountNo { set; get; }
          public string beneficiaryBankCode { set; get; }
          public string kodebic { set; get; }
          public string beneficiaryName { set; get; }
          public string description { set; get; }
          public string email { set; get; }
          public string phoneNo { set; get; }
          public string pphAmount { set; get; }
          public string ppnAmount{ set; get; }
          public string reference{ set; get; }
          public string signature{ set; get; }
          public string timestamp{ set; get; }
          public string totalAmount{ set; get; }
          public string transferType { set; get; }
        
         public string GetToSignature(string username , string key, string otp)
        {

            string lRet = "";
            // string keyandTimeStamp= key+ otp;
            //string localTimeStamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            //timestamp = localTimeStamp.Replace("AM","").Replace("PM","");
            
            //string stToHash = timestamp + keyandTimeStamp;

            //string hash =  DESEncrypt.CalculateSHA256(stToHash);
            //lRet= username + amount + beneficiaryAccountNo + kodebic + beneficiaryName + reference + description +
            //      email+phoneNo+pphAmount +ppnAmount +totalAmount+ transferType + hash;

            return lRet;



        }

    
    }


}
