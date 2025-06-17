using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Rekening
    {
        public long ID {set;get;}
        public long IDParent {set;get;}
        public string Kode {set;get;}
        public string Nama { set; get; }

        public Single Root {set;get;}
        public Single Leaf {set;get;}
        public String Tampilan { set; get; }
        public bool Baru { set; get; }
        public int Debet { set; get; }
        public int NoBaris { set; get; }

        public string KodeLevel1{set;get;}
        public string KodeLevel2 { set; get; }
        public string KodeLevel3 { set; get; }
        public string KodeLevel4 { set; get; }
        public string KodeLevel5 { set; get; }
        public string KodeLevel6 { set; get; }

        public Rekening(){
            ID =0;
            IDParent =0;
            Nama  ="";
            Root =0;
            Leaf =0;
            Tampilan="";
            Baru =true;
            Debet = 1;
            KodeLevel1="";
            KodeLevel2="";
            KodeLevel3="";
            KodeLevel4="";
            KodeLevel5="";
            KodeLevel6 = "";
        }
        public Rekening(Rekening _pIDRekening, ProfileRekening oProfile)
        {
            if (_pIDRekening != null)
            {
                ID = _pIDRekening.ID;
                IDParent = _pIDRekening.IDParent;
                Nama = _pIDRekening.Nama;
                Root = _pIDRekening.Root;
                Leaf = _pIDRekening.Leaf;
                Tampilan = _pIDRekening.Tampilan;
                Baru = _pIDRekening.Baru;
                Debet = _pIDRekening.Debet;
                KodeLevel1 = _pIDRekening.ID.ToString().Substring(0, oProfile.LEN1);
                KodeLevel2 = "";
                KodeLevel3 = "";
                KodeLevel4 = "";
                KodeLevel5 = "";
                KodeLevel6 = "";
                if (_pIDRekening.Root > 1)
                {
                    KodeLevel2 = _pIDRekening.ID.ToString().Substring(1, oProfile.Kode2);
                    if (_pIDRekening.Root > 2)
                    {
                        KodeLevel3 = _pIDRekening.ID.ToString().Substring(oProfile.LEN2, oProfile.Kode3);
                        if (_pIDRekening.Root > 3)
                        {
                            KodeLevel4 = _pIDRekening.ID.ToString().Substring(oProfile.LEN3, oProfile.Kode4);
                            if (_pIDRekening.Root > 4)
                            {
                                KodeLevel5 = _pIDRekening.ID.ToString().Substring(oProfile.LEN4, oProfile.Kode5);
                                if (_pIDRekening.Root > 5)
                                {
                                    KodeLevel6 = _pIDRekening.ID.ToString().Substring(oProfile.LEN5, oProfile.Kode6);
                                }
                            }
                        }
                    }
                }
            }
        }
        public Rekening(string _sIDRekening, int root, ProfileRekening oProfile)
        {
            //ID = _pIDRekening.ID ;
            //IDParent = _pIDRekening.IDParent;
            //Nama = _pIDRekening.Nama;
            //Root = _pIDRekening.Root ;
            //Leaf = _pIDRekening.Leaf;
            //Tampilan = _pIDRekening.Tampilan;
            //Baru = _pIDRekening.Baru;
            //Debet = _pIDRekening.Debet;
            //KodeLevel1 = _pIDRekening.ID.ToString().Substring(0,1);            
            //KodeLevel2 = "";
            //KodeLevel3 = "";
            //KodeLevel4 = "";
            //KodeLevel5 = "";
            //KodeLevel6 = "";
            //if (_pIDRekening.Root > 1)
            //{
            //    KodeLevel2 = _pIDRekening.ID.ToString().Substring(1, 1);
            //    if (_pIDRekening.Root > 2)
            //    {
            //        KodeLevel3 = _pIDRekening.ID.ToString().Substring(2, 1);
            //        if (_pIDRekening.Root > 3)
            //        {
            //            KodeLevel4 = _pIDRekening.ID.ToString().Substring(3, 2);
            //            if (_pIDRekening.Root > 4)
            //            {
            //                KodeLevel5 = _pIDRekening.ID.ToString().Substring(5, oProfile.Kode5 );
            //                if (_pIDRekening.Root > 5)
            //                {
            //                    KodeLevel6 = _pIDRekening.ID.ToString().Substring(oProfile.LEN5);
            //                }
            //            }
            //        }
            //    }
            //}
            KodeLevel1 = _sIDRekening.Trim().Substring(0, oProfile.LEN1);
            KodeLevel2 = "";
            KodeLevel3 = "";
            KodeLevel4 = "";
            KodeLevel5 = "";
            KodeLevel6 = "";
            if (root > 1)
            {
                KodeLevel2 = _sIDRekening.Trim().Substring(1, oProfile.Kode2);
                if (root > 2)
                {
                    KodeLevel3 = _sIDRekening.Trim().Substring(oProfile.LEN2, oProfile.Kode3);
                    if (root > 3)
                    {
                        KodeLevel4 = _sIDRekening.Trim().Substring(oProfile.LEN3, oProfile.Kode4);
                        if (root > 4)
                        {
                            KodeLevel5 = _sIDRekening.Trim().Substring(oProfile.LEN4, oProfile.Kode5);
                            if (root > 5)
                            {
                                KodeLevel6 = _sIDRekening.Trim().Substring(oProfile.LEN5, oProfile.Kode6);
                            }
                        }
                    }
                }
            }
        }
    }
}
