using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ProfileReportPerda
    {

        public int Tahap { set; get; }
        public string  KolomKiri { set; get; }
        public string KolomKanan { set; get; }
        public string INSERT_REKENING  { set; get; }
        public string UPDATE_REKENING { set; get; }
        public string INSERT_URAIAN { set; get; }
        public string UPDATE_URAIAN { set; get; }


        public string _namaKolomUraian1 { set; get; }
        public string _namaKolomUraian2 { set; get; }
        public string _namaKolomvolume1 { set; get; }
        public string _namaKolomvolume2 { set; get; }
        public string _namaKolomharga1 { set; get; }
        public string _namaKolomharga2 { set; get; }
        public string _namaKolomjumlah1 { set; get; }
        public string _namaKolomjumlah2 { set; get; }
        public string columnToInsert { set; get; }


        public ProfileReportPerda(int pTahap)
        {
            switch (pTahap)
            {
                case -1: //RKA
                    KolomKiri = "cPlafon";
                    KolomKanan = "cPlafon";
                    break;
                case 1: //RKA

                    //KolomKiri = "cPlafon";
                    //KolomKanan = "cPlafon";
                   
                    KolomKiri = "cJumlahMurni";
                    KolomKanan = "cJumlahMurni";

                    _namaKolomUraian1 ="sUraianMurni";   // +
                    _namaKolomUraian2 = "sUraianMurni"; // +
                    _namaKolomvolume1 = "VolMurni";  // +
                    _namaKolomvolume2 = "VolMurni";  // +
                    _namaKolomharga1 = "cHargaMurni";  // +
                    _namaKolomharga2 = "cHargaMurni";  // +
                    _namaKolomjumlah1 ="JumlahMurni";   // /+
                    _namaKolomjumlah2 = "JumlahMurni";
  

                    break;                    
                case 2:
                    KolomKiri = "cJumlahMurni";
                    KolomKanan = "cJumlahMurni";
                    _namaKolomUraian1 ="sUraianMurni";   
                    _namaKolomUraian2 = "sUraianMurni"; 
                    _namaKolomvolume1 = "VolMurni";  
                    _namaKolomvolume2 = "VolMurni";  
                    _namaKolomharga1 = "cHargaMurni";  
                    _namaKolomharga2 = "cHargaMurni";  
                     _namaKolomjumlah1 ="JumlahMurni";  
                     _namaKolomjumlah2 = "JumlahMurni";  

                    break;
                case 3: // Geser 1'
                    KolomKiri = "cJumlahMurni";
                    KolomKanan = "cJumlahGeser";

                    _namaKolomUraian1 ="sUraianMurni";   
                    _namaKolomUraian2 = "sUraianGeser"; 
                    _namaKolomvolume1 = "VolMurni";  
                    _namaKolomvolume2 = "VolGeser";  
                    _namaKolomharga1 = "cHargaMurni";  
                    _namaKolomharga2 = "cHargaGeser";  
                     _namaKolomjumlah1 ="JumlahMurni";   
                     _namaKolomjumlah2 = "JumlahGeser";  

                    break;
                case 4: // RKA ABT
                    //KolomKiri = "cJumlahMurni";
                    //KolomKanan = "cJumlahRKAP";                    
                    
                    //_namaKolomUraian1 ="sUraianMurni";   
                    //_namaKolomUraian2 = "sUraianRKAP"; // +
                    //_namaKolomvolume1 = "VolMurni";  
                    //_namaKolomvolume2 = "VolRKAP";  // +
                    //_namaKolomharga1 = "cHargaMurni"; 
                    //_namaKolomharga2 = "cHargaRKAP";  // +
                    //_namaKolomjumlah1 ="JumlahMurni";   
                    //_namaKolomjumlah2 = "JumlahRKAP";  /// +

                    KolomKiri = "cJumlahGeser";
                    KolomKanan = "cJumlahRKAP";

                    _namaKolomUraian1 = "sUraianGeser";   
                    _namaKolomUraian2 = "sUraianRKAP"; // +
                    _namaKolomvolume1 = "VolGeser";  
                    _namaKolomvolume2 = "VolRKAP";  // +
                    _namaKolomharga1 = "cHargaGeser"; 
                    _namaKolomharga2 = "cHargaRKAP";  // +
                    _namaKolomjumlah1 = "JumlahGeser";   
                    _namaKolomjumlah2 = "JumlahRKAP";  /// +


                    
                    break;
                case 5:
                    // DPA ABT
                    KolomKiri = "cJumlahRKAP";
                    KolomKanan = "cJumlahABT";
                    
                    _namaKolomUraian1 ="sUraianRKAP";   
                    _namaKolomUraian2 = "sUraianABT"; // +
                    _namaKolomvolume1 = "VolRKAP";  
                    _namaKolomvolume2 = "VolABT";  // +
                    _namaKolomharga1 = "cHargaRKAP"; 
                    _namaKolomharga2 = "cHargaABT";  
                     _namaKolomjumlah1 ="JumlahRKAP";   
                     _namaKolomjumlah2 = "JumlahABT";  /// +
                    
                    
                    break;

                case 6:
                    // Geser ABT
                    KolomKiri = "cJumlahDPA";
                    KolomKanan = "cJumlahGeser2";
                    break;
                case 7:
                    // Geser ABT
                    KolomKiri = "cJumlahRKAP";
                    
                    KolomKanan = "cRealisasi";
                    
                    break;


            }

            /*
      
             
             * ++sUraianOlah
             * 
             * 
               * sUraian
                sUraianAPBD
                sUraianMurni
                sUraianGeser
             * 
             * 
             * 
             cHarga
             cHargaOlah
             cHargaGeser
              cHargaABT
                IDstandardHarga
                cHargaMurni
             * 
             * 
             *  VolOlah
                VolGeser
                Vol
                VolABT
                VolMurni
             
             * 
             * 
                JumlahOlah
                cJumlahYAD
                Jumlah
                JumlahYADAPBD
                JumlahMurni
                JumlahGeser
             * 
             * 
             * */

        }


    }
}
