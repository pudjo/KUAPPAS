using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
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
    
       public CreateIdBillingRequest()
    {
           nomorPokokWajibPajak ="";//mandatory field
    kodeMap =""; //mandatory field
    kodeSetor ="";//mandatory field
    masaPajak ="";//mandatory field | MMMM
    tahunPajak ="";//mandatory field | yyyy
    jumlahBayar ="";//mandatory field
    nomorObjekPajak =""; 
    nomorSK ="";
    nomorPokokWajibPajakPenyetor =""; //mandatory field
    namaWajibPajak ="";
    alamatWajibPajak ="";
    kota ="";
    nik =""; 
    nomorPokokWajibPajakRekanan =""; 
    nikRekanan="";
    nomorFakturPajak =""; 
    nomorSKPD =""; //6digit kode Pemda + max 15digit kode OPD/SKPD
    nomorSPM = "";

    }
   
   }

}
