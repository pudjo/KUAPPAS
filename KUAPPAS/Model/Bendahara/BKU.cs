using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Bendahara
{
    public class BKU
    {
        public int Tahun {set;get;}
        public int IDDinas { set; get; }
        public long NoUrut{set;get;}
        public int Position  {set;get;}
        // POSITION : 1 -> BUD, 2->SKPD, 3-> BENDAHARA PENERIMAAN
        public  int Kodekategori  {set;get;}
        public  int KodeUrusan  {set;get;}
        public  int KodeSKPD  {set;get;}
        public  int KodeUk  {set;get;}
        public  int KodekategoriPelaksana  {set;get;}
        public  int KodeUrusanPelaksana  {set;get;}
        public  int KodeProgram  {set;get;}
        public  int KodeKegiatan  {set;get;}
        public  int KodeSubKegiatan  {set;get;}

        public  E_JENISBENDAHARA JenisBendahara {set;get;}
        public  long  NourutSumber  {set;get;}
        public  int KodeRekening  {set;get;}
        public  int JenisBKU  {set;get;}
        public   int No  {set;get;} //' Number ...-> Misalnya untuk BP :1 Pengeluaran, 2 Penerimaan
                                  //'           -> Untul BPP : 1 BPP DInas, 2, BPP BOS
        public int  NoBKU  {set;get;}
        public int  NoBKUSKPD  {set;get;}
        public int nobkuBUD  {set;get;}

        public DateTime TanggalTransaksi  {set;get;}
        public string NoBukti  {set;get;}
        public string Keterangan  {set;get;}
        public int Debet  {set;get;}
        public decimal Jumlah  {set;get;}
        public Single JenisBelanja  {set;get;}
        public int  JenisSumber {set;get;}
        public Single Pajak  {set;get;}
        public Single IsBKU  {set;get;}
        public Single Hitung   {set;get;}
        public E_LEVLETAMPILANBKU LevelTampilan { set; get; }
        public Single NoUrutOnSameNumber  {set;get;}
        public int NoUrutSaja { set; get; }
        public int Kodebank  {set;get;}  //' Posisi -> 0 Kas, > 0 btKodebank
        public int PPKD  {set;get;}
        public Single OnSPJ  {set;get;}
        public KELOMPOK_SPJ KelompokSPJ { set; get; }
        public decimal BiayaAdministrasi  {set;get;}
        public bool BNew  {set;get;}
        public int IDImport  {set;get;}
        public int nourutManual  {set;get;}
        public int IDUrusan  {set;get;}
        public int iIDProgram  {set;get;}
        public int IDkegiatan { set; get; }
        public long IDSubkegiatan { set; get; }
        public int UnitAnggaran { set; get; }
        public List<BKURekening> Details { set; get; }

        public BKU CreateFormSPP(SPP oSPP, int debet, int JenisBendahara)
        {
            this.Tahun = oSPP.Tahun;
            this.JenisBelanja = oSPP.Jenis;
            this.IDDinas = oSPP.IDDInas;
            this.KodeUk = oSPP.KodeUK;
            this.Debet = debet;
            this.TanggalTransaksi = oSPP.dtCair;
            this.Kodekategori = oSPP.Kodekategori;
            this.KodeUrusan = oSPP.KodeUrusan;
            this.KodeSKPD = oSPP.KodeSKPD;
            this.KodeUk = oSPP.KodeUK;
            this.UnitAnggaran = oSPP.UnitAnggaran;
            this.NoBukti= oSPP.NoSP2D;
            if (debet ==1)
                this.Keterangan = "Terima SP2D " + oSPP.Keterangan ;
            else
                this.Keterangan =  oSPP.Keterangan;
            this.Jumlah = oSPP.Jumlah;
            this.IsBKU = 1;
            this.JenisSumber =(int) E_JENIS_REFERENSIBKU.REFERENSI_SP2D;
            this.NourutSumber = oSPP.NoUrut;
            this.Kodebank = 1;
            if (JenisBendahara == 0)
            {

            }
            else
            {
                switch (oSPP.Jenis)
                {
                    case 0:
                    case 1:
                    case 2:
                        this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        if (debet == -1)
                        {
                            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
                        }
                        else
                            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                        break;



                }
                if (oSPP.Jenis >= 3 && debet == -1)
                {
                    //hanya keluar yang perlu detail 
                    this.Details = new List<BKURekening>();
                    foreach (SPPRekening spprek in oSPP.Rekenings)
                    {
                        BKURekening obkUREkening = new BKURekening();
                        obkUREkening.KodeUk = spprek.UnitKerja;
                        obkUREkening.IDUrusan = spprek.IDUrusan;
                        obkUREkening.iIDProgram = spprek.IDProgram;
                        obkUREkening.IDkegiatan = spprek.IDKegiatan;
                        obkUREkening.IDSubkegiatan = spprek.IDSubKegiatan;
                        obkUREkening.KodekategoriPelaksana = spprek.KodekategoriPelaksana;
                        obkUREkening.KodeUrusanPelaksana = spprek.KodeUrusanPelaksana;
                        obkUREkening.KodeProgram = spprek.KodeProgram;
                        obkUREkening.KodeKegiatan = spprek.KodeKegiatan;
                        obkUREkening.KodeSubKegiatan = spprek.KodeSubKegiatan;
                        obkUREkening.idRekening = spprek.IDRekening;
                        obkUREkening.Jumlah = spprek.Jumlah;


                        this.Details.Add(obkUREkening);
                    }

                }
            }
                    
            

            return this;

        }

        public BKU CreateFormPotonganPengeluaran(Pengeluaran pengeluaran, int JenisBendahara)
        {
            this.Tahun = pengeluaran.Tahun;
            this.JenisBelanja = pengeluaran.JenisBelanja;
            this.IDDinas = pengeluaran.IDDInas;
            this.KodeUk = pengeluaran.KodeUK;
            
            this.TanggalTransaksi = pengeluaran.Tanggal;
            this.Kodekategori = pengeluaran.Kodekategori;
            this.KodeUrusan = pengeluaran.KodeUrusan;

            this.KodeSKPD = pengeluaran.KodeSKPD;
            this.KodeUk = pengeluaran.KodeUK;
            this.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
            this.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
            this.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
            this.IDUrusan = pengeluaran.IDUrusan;
            this.iIDProgram = pengeluaran.IDProgram;
            this.IDkegiatan = pengeluaran.IDKegiatan;
            this.UnitAnggaran = pengeluaran.UnitAnggaran;
            this.NoBukti = pengeluaran.NoBukti;

            this.Keterangan = "Pungut Pajak Untuk " + pengeluaran.Uraian;
            this.IsBKU = 1;
            this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSPJPANJAR;
            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
            this.Debet = 1;
            this.NourutSumber = pengeluaran.NoUrut;        
            this.Details = new List<BKURekening>();
            this.Jumlah = 0;
            this.Kodebank = pengeluaran.IDBank;
            foreach (PotonganPanjar pr in pengeluaran.Potongans)
            {
                        BKURekening obkUREkening = new BKURekening();
                        obkUREkening.KodeUk = pengeluaran.KodeUK;
                        obkUREkening.IDUrusan = pengeluaran.IDUrusan;
                        obkUREkening.iIDProgram = pengeluaran.IDProgram;
                        obkUREkening.IDkegiatan = pengeluaran.IDKegiatan;
                        obkUREkening.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
                        obkUREkening.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
                        obkUREkening.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
                        obkUREkening.KodeProgram = pengeluaran.KodeProgram;
                        obkUREkening.KodeKegiatan = pengeluaran.Kodekegiatan;
                        obkUREkening.KodeSubKegiatan = pengeluaran.KodeSubKegiatan;
                        obkUREkening.idRekening = pr.IIDRekening;
                        obkUREkening.Jumlah = pr.Jumlah;
                        if (pr.Jumlah > 0)
                        {
                            this.Details.Add(obkUREkening);
                            this.Jumlah = this.Jumlah + pr.Jumlah;
                        }
            }
            return this;

        }
        public BKU CreateFormPengeluaran(Pengeluaran pengeluaran, int JenisBendahara, bool pengembalian = false )
        {
            this.Tahun = pengeluaran.Tahun;
            this.JenisBelanja = pengeluaran.JenisBelanja;
            this.IDDinas = pengeluaran.IDDInas;
            this.KodeUk = pengeluaran.KodeUK;
            
            this.TanggalTransaksi = pengeluaran.Tanggal;
            this.Kodekategori = pengeluaran.Kodekategori;
            this.KodeUrusan = pengeluaran.KodeUrusan;

            this.KodeSKPD = pengeluaran.KodeSKPD;
            this.KodeUk = pengeluaran.KodeUK;
            this.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
            this.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
            this.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
            this.IDUrusan = pengeluaran.IDUrusan;
            this.iIDProgram = pengeluaran.IDProgram;
            this.IDkegiatan = pengeluaran.IDKegiatan;
            this.Kodebank = pengeluaran.IDBank;
            this.NoBukti = pengeluaran.NoBukti;
            this.IDDinas = pengeluaran.IDDInas;
            this.Keterangan = pengeluaran.Uraian;
            this.Jumlah = pengeluaran.Jumlah;
            this.UnitAnggaran = pengeluaran.UnitAnggaran;

            this.IsBKU = 1;
            switch (pengeluaran.Jenis)
            {
                case  E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG:
                    this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PENGELUARANLANGSUNG;
                    this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
                    this.Debet = -1;
                    break;
                case E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR:
                    if (pengembalian == false)
                    {
                        this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PERTANGGUNGJAWABANPANJAR;
                        this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
                        this.Debet = -1;
                    }
                    else
                    {
                        this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PENGEMBALIANSISAPANJAR;
                        this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                        this.Jumlah = pengeluaran.JumlahDikembalikan;
                        this.Keterangan = "Pengembalian Panjar : " + pengeluaran.Uraian;
                        this.Debet = 1;
                    }
                    break;
                case E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR:
                    this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PENGEMBALIANSISAPANJAR;
                    this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                    this.Jumlah = pengeluaran.Jumlah;
                    this.Debet = 1;
                    break;

                case E_JENISPENGELUARAN.PENGELUARAN_PANJAR:
                    this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_PANJAR;
                    this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                    this.Debet = -1;
                    break;

            }
            


            this.NourutSumber = pengeluaran.NoUrut;
        
            this.Details = new List<BKURekening>();
            if (pengeluaran.Details != null)
            {
                foreach (PengeluaranRekening pr in pengeluaran.Details)
                {
                    BKURekening obkUREkening = new BKURekening();
                    obkUREkening.KodeUk = pengeluaran.KodeUK;
                    obkUREkening.IDUrusan = pengeluaran.IDUrusan;
                    obkUREkening.iIDProgram = pengeluaran.IDProgram;
                    obkUREkening.IDkegiatan = pengeluaran.IDKegiatan;
                    obkUREkening.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
                    obkUREkening.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
                    obkUREkening.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
                    obkUREkening.KodeProgram = pengeluaran.KodeProgram;
                    obkUREkening.KodeKegiatan = pengeluaran.Kodekegiatan;
                    obkUREkening.KodeSubKegiatan = pengeluaran.KodeSubKegiatan;
                    obkUREkening.idRekening = pr.IDRekening;

                    obkUREkening.Jumlah = pr.Jumlah;


                    if (pr.Jumlah>0)
                    this.Details.Add(obkUREkening);
                }
            }
            return this;

        }

        public BKU CreateFormTrxBank (TrxBank  tb, int JenisBendahara)
        {
            this.Tahun = tb.Tahun;
            this.JenisBelanja = 0;
            this.IDDinas = tb.IDDinas;
            this.KodeUk = tb.KodeUK;
            this.NourutSumber = tb.ID;

            this.TanggalTransaksi = tb.DTrx;
            this.Kodekategori = tb.Kodekategori;
            this.KodeUrusan = tb.KodeUrusan;

            this.KodeSKPD = tb.KodeSKPD ;
            this.KodeUk = tb.KodeUK;
            this.KodekategoriPelaksana = tb.Kodekategori;
            this.KodeUrusanPelaksana = tb.KodeUrusan;
            this.IDSubkegiatan = 0;
            this.IDUrusan = tb.Kodekategori * 100 + tb.KodeUrusan;
            this.iIDProgram = 0;
            this.IDkegiatan = 0;

            this.NoBukti = tb.NoBukti;

            this.Keterangan = tb.Uraian ;
            this.Jumlah = tb.Jumlah;
            this.IsBKU = 1;
            this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_BANK ;
            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                    
            return this;

        }
        public BKU CreateFormPotonganSPP(SPP oSPP, int debet, int JenisBendahara)
        {
            this.Tahun = oSPP.Tahun;
            this.JenisBelanja = oSPP.Jenis;
            this.IDDinas = oSPP.IDDInas;
            this.KodeUk = oSPP.KodeUK;
            this.Debet = debet;
            this.TanggalTransaksi = oSPP.dtCair;
            this.Kodekategori = oSPP.Kodekategori;
            this.KodeUrusan = oSPP.KodeUrusan;
            this.KodeSKPD = oSPP.KodeSKPD;
            this.KodeUk = oSPP.KodeUK;
            this.UnitAnggaran = oSPP.UnitAnggaran;
            this.NoBukti = oSPP.NoSP2D;
            if (debet == 1)
                this.Keterangan = "Pungut Potongan SP2D No " + oSPP.NoSP2D;
            else
                this.Keterangan = "Penyetoran Potongan SP2D No " + oSPP.NoSP2D;

            this.Jumlah = oSPP.Jumlah;
            this.Kodebank = 1;
            this.IsBKU = 1;
            
            this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_POTONGANSP2D;
            this.NourutSumber = oSPP.NoUrut;

            if (JenisBendahara == 0)
            {

            }
            else
            {
                switch (oSPP.Jenis)
                {
                    case 0:
                    case 1:
                    case 2:
                        this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                        break;
                    case 3:
                    case 4:
                    case 5:
                            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;

                            break;

                }
               
                    //hanya keluar yang perlu detail 
             this.Details = new List<BKURekening>();
             decimal JumlahPotongan = 0L;
             foreach (PotonganSPP pot in oSPP.Potongans)
             {
                        BKURekening obkUREkening = new BKURekening();
                        obkUREkening.KodeUk = oSPP.KodeUK;
                        obkUREkening.IDUrusan = 0;
                        obkUREkening.iIDProgram = 0;
                        obkUREkening.IDkegiatan = 0;
                        obkUREkening.IDSubkegiatan = 0;
                        obkUREkening.KodekategoriPelaksana = 0;
                        obkUREkening.KodeUrusanPelaksana = 0;
                        obkUREkening.KodeProgram = 0;
                        obkUREkening.KodeKegiatan = 0;
                        obkUREkening.KodeSubKegiatan = 0;
                        obkUREkening.idRekening = pot.IIDRekening;
                        obkUREkening.Jumlah = pot.Jumlah;
                        JumlahPotongan = JumlahPotongan + pot.Jumlah;

                        this.Details.Add(obkUREkening);
                }
                this.Jumlah = JumlahPotongan;
                                
            }
            return this;

        }
        public BKU CreateFormPengembalian(Setor oSetor, int debet, int  JenisSumber, int JenisBendahara)
        {
            this.Tahun = oSetor.Tahun;
            this.JenisBelanja = oSetor.JenisSP2D;
            this.IDDinas = oSetor.IDDinas;
            this.KodeUk = oSetor.KodeUK;
            this.Debet = debet;
            this.TanggalTransaksi = oSetor.dtBukuKas ;
            this.Kodekategori = oSetor.KodeKategori;
            this.KodeUrusan = oSetor.KodeUrusan;
            this.KodeSKPD = oSetor.KodeSKPD;
            this.KodeUk = oSetor.KodeUK;
            this.NoBukti = oSetor.NoBukti;
            this.Keterangan = oSetor.Keterangan;
            this.Debet = debet;
            this.Jumlah = oSetor.Jumlah;
            this.IsBKU = 1;
            this.JenisBelanja = oSetor.JenisSP2D;
            this.JenisSumber = JenisSumber;
            this.NourutSumber = oSetor.NoUrut;
            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
            this.Kodebank = oSetor.Kodebank;
            this.UnitAnggaran = oSetor.UnitAnggaran;
            //hanya keluar yang perlu detail 
            this.Details = new List<BKURekening>();
            if (oSetor.JenisSP2D >= 3 ||oSetor.Jenis==4)
            {
                this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;

                if (oSetor.Details == null){
                    oSetor.Details = new List<SetorRekening>();
                }
                foreach (SetorRekening spprek in oSetor.Details)
                {
                    BKURekening obkUREkening = new BKURekening();
                    obkUREkening.KodeUk = oSetor.KodeUK;
                    obkUREkening.IDUrusan = oSetor.IDUrusan;
                    obkUREkening.iIDProgram = oSetor.IDProgram;
                    obkUREkening.IDkegiatan = oSetor.IDKegiatan;
                    obkUREkening.IDSubkegiatan = oSetor.IDSubKegiatan;
                    obkUREkening.KodekategoriPelaksana = oSetor.KodekategoriPelaksana;
                    obkUREkening.KodeUrusanPelaksana = oSetor.KodeUrusanPelaksana;
                    obkUREkening.KodeProgram = oSetor.KodeProgram;
                    obkUREkening.KodeKegiatan = oSetor.KodeKegiatan;
                    obkUREkening.KodeSubKegiatan = oSetor.KodeSubKegiatan;
                    obkUREkening.idRekening = spprek.IDRekening;
                    obkUREkening.Jumlah = spprek.Jumlah;
                    this.Details.Add(obkUREkening);
                }
            }

            return this;

        }

        public BKU CreateFormSetorSTS(Setor oSetor, int debet, int JenisSumber, int JenisBendahara)
        {
            this.Tahun = oSetor.Tahun;
            this.JenisBelanja = 0;
            this.IDDinas = oSetor.IDDinas;
            this.KodeUk = oSetor.KodeUK;
            this.Debet = -1;
            this.TanggalTransaksi = oSetor.dtBukuKas;
            this.Kodekategori = oSetor.KodeKategori;
            this.KodeUrusan = oSetor.KodeUrusan;
            this.KodeSKPD = oSetor.KodeSKPD;
            this.KodeUk = oSetor.KodeUK;
            this.NoBukti = oSetor.NoBukti;
            this.Keterangan = oSetor.Keterangan;
            this.UnitAnggaran = oSetor.UnitAnggaran;
            this.Jumlah = oSetor.Jumlah;
            this.IsBKU = 1;
            this.JenisBelanja = 0;
            this.JenisSumber = JenisSumber;
            this.NourutSumber = oSetor.NoUrut;
          

            //hanya keluar yang perlu detail 
            this.Details = new List<BKURekening>();

            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;

            foreach (SetorRekening spprek in oSetor.Details)
            {
                    BKURekening obkUREkening = new BKURekening();
                    obkUREkening.KodeUk = oSetor.KodeUK;
                    obkUREkening.IDUrusan = oSetor.IDUrusan;
                    obkUREkening.iIDProgram = oSetor.IDProgram;
                    obkUREkening.IDkegiatan = oSetor.IDKegiatan;
                    obkUREkening.IDSubkegiatan = oSetor.IDSubKegiatan;
                    obkUREkening.KodekategoriPelaksana = oSetor.KodekategoriPelaksana;
                    obkUREkening.KodeUrusanPelaksana = oSetor.KodeUrusanPelaksana;
                    obkUREkening.KodeProgram = oSetor.KodeProgram;
                    obkUREkening.KodeKegiatan = oSetor.KodeKegiatan;
                    obkUREkening.KodeSubKegiatan = oSetor.KodeSubKegiatan;
                    obkUREkening.idRekening = spprek.IDRekening;
                    obkUREkening.Jumlah = spprek.Jumlah;
                    this.Details.Add(obkUREkening);
            }
            if (oSetor.Details.Count >0) 
                this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
            else
                this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;

            return this;

        }
        public BKU CreateFormSetorInKasda(Setor oSetor, int debet, int JenisSumber, int JenisBendahara)
        {
            this.Tahun = oSetor.Tahun;
            this.JenisBelanja = 0;
            this.IDDinas = oSetor.IDDinas;
            this.KodeUk = oSetor.KodeUK;
            this.Debet = 1;
            this.TanggalTransaksi = oSetor.dtBukuKas;
            this.Kodekategori = oSetor.KodeKategori;
            this.KodeUrusan = oSetor.KodeUrusan;
            this.KodeSKPD = oSetor.KodeSKPD;
            this.KodeUk = oSetor.KodeUK;
            this.NoBukti = oSetor.NoBukti;
            this.Keterangan = oSetor.Keterangan;
            this.UnitAnggaran = oSetor.UnitAnggaran;
            this.Jumlah = oSetor.Jumlah;
            this.IsBKU = 1;
            this.JenisBelanja = 0;
            this.JenisSumber = JenisSumber;
            this.NourutSumber = oSetor.NoUrut;

            this.Debet = 1;
            this.PPKD = 0;

            //hanya keluar yang perlu detail 
            this.Details = new List<BKURekening>();

           
            foreach (SetorRekening spprek in oSetor.Details)
            {
                BKURekening obkUREkening = new BKURekening();
                obkUREkening.KodeUk = oSetor.KodeUK;
                obkUREkening.IDUrusan = oSetor.IDUrusan;
                obkUREkening.iIDProgram = oSetor.IDProgram;
                obkUREkening.IDkegiatan = oSetor.IDKegiatan;
                obkUREkening.IDSubkegiatan = oSetor.IDSubKegiatan;
                obkUREkening.KodekategoriPelaksana = oSetor.KodekategoriPelaksana;
                obkUREkening.KodeUrusanPelaksana = oSetor.KodeUrusanPelaksana;
                obkUREkening.KodeProgram = oSetor.KodeProgram;
                obkUREkening.KodeKegiatan = oSetor.KodeKegiatan;
                obkUREkening.KodeSubKegiatan = oSetor.KodeSubKegiatan;
                obkUREkening.idRekening = spprek.IDRekening;
                obkUREkening.Jumlah = spprek.Jumlah;
                this.Details.Add(obkUREkening);
            }
            if (oSetor.Details.Count > 0)
                this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
            else
                this.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;

            return this;

        }
        public BKU CreateFormSTS(STS oSTS, int debet, int JenisSumber, int JenisBendahara)
        {
            this.Tahun = oSTS.Tahun;
            this.JenisBelanja = 0;
            this.IDDinas = oSTS.IDDinas;
            this.KodeUk = oSTS.KodeUK;
            this.Kodebank = oSTS.Bank;
            this.Debet = debet;
            this.TanggalTransaksi = oSTS.TanggalSTS;
            this.Kodekategori = oSTS.KodeKategori;
            this.KodeUrusan = oSTS.KodeUrusan;
            this.KodeSKPD = oSTS.KodeSKPD;
            this.KodeUk = oSTS.KodeUK;
            this.NoBukti = oSTS.NoBukti;
            this.Keterangan = oSTS.Keterangan;
            this.UnitAnggaran = 0;
            this.Jumlah = oSTS.Jumlah;
            this.IsBKU = 1;
            this.JenisBelanja = 0;
            this.JenisSumber = JenisSumber;
            this.NourutSumber = oSTS.NoUrut;
            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;

            //hanya keluar yang perlu detail 
            this.Details = new List<BKURekening>();

            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;

            foreach (STSRekening stsrek in oSTS.Rekenings)
            {
                BKURekening obkUREkening = new BKURekening();
                obkUREkening.KodeUk = oSTS.KodeUK;
                obkUREkening.IDUrusan = oSTS.IDUrusan;
                obkUREkening.iIDProgram = 0;
                obkUREkening.IDkegiatan = 0;
                obkUREkening.IDSubkegiatan = 0;
                obkUREkening.KodekategoriPelaksana = 0;// oSTS.KodekategoriPelaksana;
                obkUREkening.KodeUrusanPelaksana = 0;//oSTS.KodeUrusanPelaksana;
                obkUREkening.KodeProgram = 0;
                obkUREkening.KodeKegiatan = 0;
                obkUREkening.KodeSubKegiatan = 0;
                obkUREkening.idRekening = stsrek.IDRekening;
                obkUREkening.Jumlah = stsrek.Jumlah;
                this.Details.Add(obkUREkening);
            }
            return this;

        }
        
        public BKU CreateFromKoreksi(Koreksi koreksi, KoreksiDetail koreksidetail, int JenisBendahara)
        {
            this.Tahun = koreksi.Tahun;
            this.JenisBelanja = koreksi.JenisBelanja ;
            this.IDDinas = koreksi.IDDInas;
            this.KodeUk = koreksi.KodeUK;
            this.Debet = koreksidetail.Debet1;
            this.TanggalTransaksi = koreksi.DtKoreksi;
            this.Kodekategori = koreksi.Kodekategori;
            this.KodeUrusan = koreksi.KodeUrusan;
            this.KodeSKPD = koreksi.KodeSKPD;
            this.KodeUk = koreksi.KodeUK;
            this.NoBukti = koreksi.NoBukti;
            this.Keterangan = koreksi.Uraian;
            this.Jumlah = koreksi.Jumlah;
            this.UnitAnggaran = koreksi.UnitAnggaran;
            this.IsBKU = 1;
            this.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN ;
            this.JenisSumber = (int)E_JENIS_REFERENSIBKU.REFERENSI_KOREKSI;
            this.NourutSumber = koreksi.NoUrut;
            this.LevelTampilan = E_LEVLETAMPILANBKU.eBKURekening;
            this.Kodebank = 1;
            //hanya keluar yang perlu detail 
            this.Details = new List<BKURekening>();
            // hanya 
            BKURekening obkUREkening = new BKURekening();
            obkUREkening.KodeUk = koreksi.KodeUK;
            obkUREkening.IDUrusan = koreksidetail.IDurusan;
            obkUREkening.iIDProgram = koreksidetail.IDProgram;
            obkUREkening.IDkegiatan = koreksidetail.IDKegiatan;
            obkUREkening.IDSubkegiatan = koreksidetail.IDSubKegiatan;
            obkUREkening.KodekategoriPelaksana = koreksidetail.KodeKategoriPelaksana;
            obkUREkening.KodeUrusanPelaksana = koreksidetail.KodeUrusanPelaksana;
            obkUREkening.KodeProgram = koreksidetail.KodeProgram;
            obkUREkening.KodeKegiatan = koreksidetail.KodeKegiatan;
            obkUREkening.KodeSubKegiatan = koreksidetail.KodeSubKegiatan;
            obkUREkening.idRekening = koreksidetail.IDRekening1;
            obkUREkening.Jumlah = koreksidetail.Jumlah1;
            this.Jumlah = koreksidetail.Jumlah1;
            this.Debet = koreksidetail.Debet1;

            this.Details.Add(obkUREkening);
                   
            


            return this;

        }
        
    }
    



    public class BKURekening
    {
        public int Tahun { set; get; }
        public long NoUrut { set; get; }
        
        public int Kodekategori { set; get; }
        public int KodeUrusan { set; get; }
        public int KodeSKPD { set; get; }
        public int KodeUk { set; get; }
        public int KodekategoriPelaksana { set; get; }
        public int KodeUrusanPelaksana { set; get; }
        public int KodeProgram { set; get; }
        public int KodeKegiatan { set; get; }
        public int KodeSubKegiatan { set; get; }

        public int IDUrusan { set; get; }
        public int iIDProgram { set; get; }
        public int IDkegiatan { set; get; }
        public long IDSubkegiatan { set; get; }
        public long idRekening { set; get; }
        public decimal Jumlah { set; get; }
    }


    public class BKUDISPLAY
    {
        public long NoUrut { set; get; }
        public long NoUrutSumber { set; get; }
        public int JenisSumber { set; get; }
        public int IDDinas { set; get; }
        public int Bank { set; get; }
        public int IDKegiatan { set; get; }
        public long IdSubKegiatan { set; get; }
        public long IDRekening { set; get; }
        public string Uraian { set; get; }
        public int Debet { set; get; }
        public DateTime Tanggal { set; get; }
        public string NoBukti { set; get; }
        public decimal Penerimaan { set; get; }
        public decimal Pengeluaran { set; get; }
        public decimal Saldo { set; get; }
        public string SPenerimaan { set; get; }
        public string SPengeluaran { set; get; }
        public string SSaldo { set; get; }
        public string sKode { set; get; }
        public string sNoBKU { set; get; }
        public int NoBkU { set; get; }
        public int NoBkUSKPD { set; get; }

        public int Level { set; get; }
        public int LevelTampilan { set; get; }
        public int JenisBendahara { set; get; }

        public int KodeUK{ set; get; }
        public int KodeBank { set; get; }
        public decimal Jumlah { set; get; }



    }
    public class BKUINFO
    {
        public decimal SaldoAwal { set; get; }
        public decimal JumlahTerima { set; get; }
        public decimal JumlahKeluar { set; get; }
        public decimal JumlahTerimalalu { set; get; }
        public decimal JumlahKeluarLalu { set; get; }
        public decimal JumlahBank { set; get; }
        public decimal JumlahTunai { set; get; }


    }
    public enum E_JENISBENDAHARA{
        BENDAHARA_BUD=0, BENDAHARA_PENERIMAAN=1, BENDAHARA_PENGELUARAN = 2
        
    }


//Public Const JENIS_BENDAHARA_BUD As Single = 0
//Public Const JENIS_BENDAHARA_PENERIMAAN As Single = 1
//Public Const JENIS_BENDAHARA_PENGELUARAN As Single = 2
    public enum  KELOMPOK_SPJ
    {
        KELOMPOK_SP2D_SPJ = 1,
        KELOMPOK_SALDO = 2,
        KELOMPOK_PELIMPAHAN = 3,
        KELOMPOK_PAJAK = 4,
        KELOMPOK_LAIN_LAIN = 5
    }
    public enum E_JENISPENGELUARAN
    {
        PENGELUARAN_PANJAR = 1,
        PENGEMBALIAN_PANJAR = 2,
        PENGELUARAN_LANGSUNG=3,
        PERTANGGUNGJAWABAN_PANJAR = 4,
        PENGELUARAN_ADD = 5,
        PENGELUARAN_BLUD = 6,
        PENGELUARAN_BOS= 7,


        
    }
    public enum E_JENIS_REFERENSIBKU{
            REFERENSI_SP2D = 1,
            REFERENSI_TSTS = 2,// sts
            REFERENSI_TCP = 3, //pengembalian belanja
            REFERENSI_SETOR = 4,// tidak ada
            REFERENSI_PENGELUARANLANGSUNG = 5,// spj

            REFERENSI_PANJAR = 14,            
         
            REFERENSI_PERTANGGUNGJAWABANPANJAR = 6, 
           
            REFERENSI_PENGEMBALIANSISAPANJAR = 16,

            REFERENSI_PELIMPAHAN_UP = 7,
            REFERENSI_LANGSUNG = 8,      // tidak ada      
            REFERENSI_POTONGANSPJPANJAR = 9, //potongan doi dpj
            REFERENSI_POTONGANSP2D = 10,    
        
            REFERENSI_PENYETORANPAJAK = 17, //Pembayaran pajak 

            REFERENSI_BANK = 12,            
            REFERENSI_ALOKASIKAS = 13,
            REFERENSI_PENERIMAANPAJAK = 11,
            REFERENSI_ANTARBANK = 20,
            REFERENSI_SPMNIHIL = 21,
            REFERENSI_PENGELUARANBLUD = 22,
            REFERENSI_POTONGANBLUD = 23,
            REFERENSI_SETORSTS = 24,
            REFERENSI_KOREKSI = 25
        //'REFERENSI_POTONGANBLUD = 24
            
    }
    public enum E_JENISBARU_REFERENSI{

    }
    public enum E_LEVLETAMPILANBKU{

            eBKUHeader = 1,
            eBKURekening = 2,
            eBKUUraian = 3
    }
}
