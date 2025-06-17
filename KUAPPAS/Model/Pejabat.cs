using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Pejabat
    {
        public int ID { set; get; }
        public int Jenis { set; get; }  //1 Pimpinan Daerah
                                        //2. Sekretaris Daerah
                                        // 3. BUD
                                        // 4
                                        // 4. BEndahara Penerimaan
                                        // 5 BEndahara Penerimaan Pembantu
                                        // 6 BEndahara Pengeluaran
                                        // 7 Bendahara Pengeluaran pembantu
                                        // 8. PPTK

        public int IDDInas { set; get; }
        public string Jabatan { set; get; }
        public string Nama { set; get; }
        public int Unit { set; get; }
        public string NIP { set; get; }
        public Single Active { set; get; }
        public int  PPKD { set; get; }

        public string NoRekening { set; get; }
        public string NPWP { set; get; }
        public string NamaBank { set; get; }
        public string NamaDalamRekeningBank { set; get; }
        public DateTime TanggalAktiv { set; get; }
        public int BANK { set; get; }
        public string NoHP { set; get; }

        public Pejabat()
        {

            Jabatan ="";
            Nama ="";
            Unit =0;
            NIP ="";
            NoRekening ="";
            NPWP ="";
            NamaBank ="";
            NamaDalamRekeningBank = "";

        }

    }
}
