using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.Bendahara;
namespace DTO.Akuntansi
{
    public class Jurnal
    {
        
            public  long NoJurnal {set;get;}
            public int IDDinas { set; get; }
            public int KodeUK { set; get; }
            public int UnitAnggaran { set; get; }
            public  int  Tahun {set;get;}
            public  int  Jenis {set;get;}
            public  int  Status {set;get;}
            public  String NoBukti {set;get;}
            public  DateTime TanggalBukti {set;get;}
            public DateTime TanggalJurnal { set; get; } 
            public  long NoUrutSumber {set;get;}
            public  int  JenisSumber {set;get;}
            public  string Uraian {set;get;}
            public List<JurnalRekeningShow> Details { set; get; }
   
    
#region BuatdariTransaksi

            public Jurnal CreateFormSPP(SPP oSPP,  JENIS_DETAILJURNAL jenis)
        {

            this.Tahun = oSPP.Tahun;
            this.IDDinas = oSPP.IDDInas;
            this.KodeUK = oSPP.KodeUK;
            
            this.TanggalBukti= oSPP.dtCair;
            this.UnitAnggaran = oSPP.UnitAnggaran;

            this.NoBukti = oSPP.NoSP2D;
            this.KodeUK = oSPP.KodeUK;
            this.JenisSumber = (int)JENIS_SUMBERJURNAL.E_SUMBER_SP2D;  
            this.NoUrutSumber= oSPP.NoUrut;

            if (jenis == JENIS_DETAILJURNAL.E_JENISLRA)
            {


            }
        
        
        
        
        return this;


        }
        /*
                public Jurnal CreateFormPotonganPengeluaran(Pengeluaran pengeluaran, int JenisBendahara)
                {
                    this.Tahun = pengeluaran.Tahun;
                    this.JenisBelanja = pengeluaran.JenisBelanja;
                    this.IDDinas = pengeluaran.IDDInas;
                    this.KodeUK = pengeluaran.KodeUK;
            
                    this.TanggalTransaksi = pengeluaran.Tanggal;
                    this.Kodekategori = pengeluaran.Kodekategori;
                    this.KodeUrusan = pengeluaran.KodeUrusan;

                    this.KodeSKPD = pengeluaran.KodeSKPD;
                    this.KodeUK = pengeluaran.KodeUK;
                    this.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
                    this.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
                    this.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
                    this.IDUrusan = pengeluaran.IDUrusan;
                    this.iIDProgram = pengeluaran.IDProgram;
                    this.IDkegiatan = pengeluaran.IDKegiatan;
                    this.UnitAnggaran = pengeluaran.UnitAnggaran;
                    this.NoBukti = pengeluaran.NoBukti;

                    this.Keterangan = "Pungut Pajak Untuk " + pengeluaran.Uraian;
                    this.IsJurnal = 1;
                    this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_POTONGANSPJPANJAR;
                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;
                    this.Debet = 1;
                    this.NourutSumber = pengeluaran.NoUrut;        
                    this.Details = new List<JurnalRekening>();
                    this.Jumlah = 0;
                    this.Kodebank = pengeluaran.IDBank;
                    foreach (PotonganPanjar pr in pengeluaran.Potongans)
                    {
                                JurnalRekening oJurnalREkening = new JurnalRekening();
                                oJurnalREkening.KodeUk = pengeluaran.KodeUK;
                                oJurnalREkening.IDUrusan = pengeluaran.IDUrusan;
                                oJurnalREkening.iIDProgram = pengeluaran.IDProgram;
                                oJurnalREkening.IDkegiatan = pengeluaran.IDKegiatan;
                                oJurnalREkening.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
                                oJurnalREkening.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
                                oJurnalREkening.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
                                oJurnalREkening.KodeProgram = pengeluaran.KodeProgram;
                                oJurnalREkening.KodeKegiatan = pengeluaran.Kodekegiatan;
                                oJurnalREkening.KodeSubKegiatan = pengeluaran.KodeSubKegiatan;
                                oJurnalREkening.idRekening = pr.IIDRekening;
                                oJurnalREkening.Jumlah = pr.Jumlah;
                                if (pr.Jumlah > 0)
                                {
                                    this.Details.Add(oJurnalREkening);
                                    this.Jumlah = this.Jumlah + pr.Jumlah;
                                }
                    }
                    return this;

                }
                public Jurnal CreateFormPengeluaran(Pengeluaran pengeluaran, int JenisBendahara, bool pengembalian = false )
                {
                    this.Tahun = pengeluaran.Tahun;
                    this.JenisBelanja = pengeluaran.JenisBelanja;
                    this.IDDinas = pengeluaran.IDDInas;
                    this.KodeUK = pengeluaran.KodeUK;
            
                    this.TanggalTransaksi = pengeluaran.Tanggal;
                    this.Kodekategori = pengeluaran.Kodekategori;
                    this.KodeUrusan = pengeluaran.KodeUrusan;

                    this.KodeSKPD = pengeluaran.KodeSKPD;
                    this.KodeUK = pengeluaran.KodeUK;
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

                    this.IsJurnal = 1;
                    switch (pengeluaran.Jenis)
                    {
                        case  E_JENISPENGELUARAN.PENGELUARAN_LANGSUNG:
                            this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_PENGELUARANLANGSUNG;
                            this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;
                            this.Debet = -1;
                            break;
                        case E_JENISPENGELUARAN.PERTANGGUNGJAWABAN_PANJAR:
                            if (pengembalian == false)
                            {
                                this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_PERTANGGUNGJAWABANPANJAR;
                                this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;
                                this.Debet = -1;
                            }
                            else
                            {
                                this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_PENGEMBALIANSISAPANJAR;
                                this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalHeader;
                                this.Jumlah = pengeluaran.JumlahDikembalikan;
                                this.Keterangan = "Pengembalian Panjar : " + pengeluaran.Uraian;
                                this.Debet = 1;
                            }
                            break;
                        case E_JENISPENGELUARAN.PENGEMBALIAN_PANJAR:
                            this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_PENGEMBALIANSISAPANJAR;
                            this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalHeader;
                            this.Jumlah = pengeluaran.Jumlah;
                            this.Debet = 1;
                            break;

                        case E_JENISPENGELUARAN.PENGELUARAN_PANJAR:
                            this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_PANJAR;
                            this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalHeader;
                            this.Debet = -1;
                            break;

                    }
            


                    this.NourutSumber = pengeluaran.NoUrut;
        
                    this.Details = new List<JurnalRekening>();
                    if (pengeluaran.Details != null)
                    {
                        foreach (PengeluaranRekening pr in pengeluaran.Details)
                        {
                            JurnalRekening oJurnalREkening = new JurnalRekening();
                            oJurnalREkening.KodeUk = pengeluaran.KodeUK;
                            oJurnalREkening.IDUrusan = pengeluaran.IDUrusan;
                            oJurnalREkening.iIDProgram = pengeluaran.IDProgram;
                            oJurnalREkening.IDkegiatan = pengeluaran.IDKegiatan;
                            oJurnalREkening.IDSubkegiatan = pengeluaran.IDSUbKegiatan;
                            oJurnalREkening.KodekategoriPelaksana = pengeluaran.KodekategoriPelaksana;
                            oJurnalREkening.KodeUrusanPelaksana = pengeluaran.KodeurusanPelaksana;
                            oJurnalREkening.KodeProgram = pengeluaran.KodeProgram;
                            oJurnalREkening.KodeKegiatan = pengeluaran.Kodekegiatan;
                            oJurnalREkening.KodeSubKegiatan = pengeluaran.KodeSubKegiatan;
                            oJurnalREkening.idRekening = pr.IDRekening;

                            oJurnalREkening.Jumlah = pr.Jumlah;


                            if (pr.Jumlah>0)
                            this.Details.Add(oJurnalREkening);
                        }
                    }
                    return this;

                }

                public Jurnal CreateFormTrxBank (TrxBank  tb, int JenisBendahara)
                {
                    this.Tahun = tb.Tahun;
                    this.JenisBelanja = 0;
                    this.IDDinas = tb.IDDinas;
                    this.KodeUK = tb.KodeUK;
                    this.NourutSumber = tb.ID;

                    this.TanggalTransaksi = tb.DTrx;
                    this.Kodekategori = tb.Kodekategori;
                    this.KodeUrusan = tb.KodeUrusan;

                    this.KodeSKPD = tb.KodeSKPD ;
                    this.KodeUK = tb.KodeUK;
                    this.KodekategoriPelaksana = tb.Kodekategori;
                    this.KodeUrusanPelaksana = tb.KodeUrusan;
                    this.IDSubkegiatan = 0;
                    this.IDUrusan = tb.Kodekategori * 100 + tb.KodeUrusan;
                    this.iIDProgram = 0;
                    this.IDkegiatan = 0;

                    this.NoBukti = tb.NoBukti;

                    this.Keterangan = tb.Uraian ;
                    this.Jumlah = tb.Jumlah;
                    this.IsJurnal = 1;
                    this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_BANK ;
                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalHeader;
                    
                    return this;

                }
                public Jurnal CreateFormPotonganSPP(SPP oSPP, int debet, int JenisBendahara)
                {
                    this.Tahun = oSPP.Tahun;
                    this.JenisBelanja = oSPP.Jenis;
                    this.IDDinas = oSPP.IDDInas;
                    this.KodeUK = oSPP.KodeUK;
                    this.Debet = debet;
                    this.TanggalTransaksi = oSPP.dtCair;
                    this.Kodekategori = oSPP.Kodekategori;
                    this.KodeUrusan = oSPP.KodeUrusan;
                    this.KodeSKPD = oSPP.KodeSKPD;
                    this.KodeUK = oSPP.KodeUK;
                    this.UnitAnggaran = oSPP.UnitAnggaran;
                    this.NoBukti = oSPP.NoSP2D;
                    if (debet == 1)
                        this.Keterangan = "Pungut Potongan SP2D No " + oSPP.NoSP2D;
                    else
                        this.Keterangan = "Penyetoran Potongan SP2D No " + oSPP.NoSP2D;

                    this.Jumlah = oSPP.Jumlah;
                    this.Kodebank = 1;
                    this.IsJurnal = 1;
            
                    this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_POTONGANSP2D;
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
                                this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalHeader;
                                break;
                            case 3:
                            case 4:
                            case 5:
                                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;

                                    break;

                        }
               
                            //hanya keluar yang perlu detail 
                     this.Details = new List<JurnalRekening>();
                     decimal JumlahPotongan = 0L;
                     foreach (PotonganSPP pot in oSPP.Potongans)
                     {
                                JurnalRekening oJurnalREkening = new JurnalRekening();
                                oJurnalREkening.KodeUk = oSPP.KodeUK;
                                oJurnalREkening.IDUrusan = 0;
                                oJurnalREkening.iIDProgram = 0;
                                oJurnalREkening.IDkegiatan = 0;
                                oJurnalREkening.IDSubkegiatan = 0;
                                oJurnalREkening.KodekategoriPelaksana = 0;
                                oJurnalREkening.KodeUrusanPelaksana = 0;
                                oJurnalREkening.KodeProgram = 0;
                                oJurnalREkening.KodeKegiatan = 0;
                                oJurnalREkening.KodeSubKegiatan = 0;
                                oJurnalREkening.idRekening = pot.IIDRekening;
                                oJurnalREkening.Jumlah = pot.Jumlah;
                                JumlahPotongan = JumlahPotongan + pot.Jumlah;

                                this.Details.Add(oJurnalREkening);
                        }
                        this.Jumlah = JumlahPotongan;
                                
                    }
                    return this;

                }
                public Jurnal CreateFormPengembalian(Setor oSetor, int debet, int  JenisSumber, int JenisBendahara)
                {
                    this.Tahun = oSetor.Tahun;
                    this.JenisBelanja = oSetor.JenisSP2D;
                    this.IDDinas = oSetor.IDDinas;
                    this.KodeUK = oSetor.KodeUK;
                    this.Debet = debet;
                    this.TanggalTransaksi = oSetor.dtBukuKas ;
                    this.Kodekategori = oSetor.KodeKategori;
                    this.KodeUrusan = oSetor.KodeUrusan;
                    this.KodeSKPD = oSetor.KodeSKPD;
                    this.KodeUK = oSetor.KodeUK;
                    this.NoBukti = oSetor.NoBukti;
                    this.Keterangan = oSetor.Keterangan;
                    this.Debet = debet;
                    this.Jumlah = oSetor.Jumlah;
                    this.IsJurnal = 1;
                    this.JenisBelanja = oSetor.JenisSP2D;
                    this.JenisSumber = JenisSumber;
                    this.NourutSumber = oSetor.NoUrut;
                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalHeader;
                    this.Kodebank = oSetor.Kodebank;
                    this.UnitAnggaran = oSetor.UnitAnggaran;
                    //hanya keluar yang perlu detail 
                    this.Details = new List<JurnalRekening>();
                    if (oSetor.JenisSP2D >= 3 ||oSetor.Jenis==4)
                    {
                        this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;

                        foreach (SetorRekening spprek in oSetor.Details)
                        {
                            JurnalRekening oJurnalREkening = new JurnalRekening();
                            oJurnalREkening.KodeUk = oSetor.KodeUK;
                            oJurnalREkening.IDUrusan = oSetor.IDUrusan;
                            oJurnalREkening.iIDProgram = oSetor.IDProgram;
                            oJurnalREkening.IDkegiatan = oSetor.IDKegiatan;
                            oJurnalREkening.IDSubkegiatan = oSetor.IDSubKegiatan;
                            oJurnalREkening.KodekategoriPelaksana = oSetor.KodekategoriPelaksana;
                            oJurnalREkening.KodeUrusanPelaksana = oSetor.KodeUrusanPelaksana;
                            oJurnalREkening.KodeProgram = oSetor.KodeProgram;
                            oJurnalREkening.KodeKegiatan = oSetor.KodeKegiatan;
                            oJurnalREkening.KodeSubKegiatan = oSetor.KodeSubKegiatan;
                            oJurnalREkening.idRekening = spprek.IDRekening;
                            oJurnalREkening.Jumlah = spprek.Jumlah;
                            this.Details.Add(oJurnalREkening);
                        }
                    }

                    return this;

                }

                public Jurnal CreateFormSetorSTS(Setor oSetor, int debet, int JenisSumber, int JenisBendahara)
                {
                    this.Tahun = oSetor.Tahun;
                    this.JenisBelanja = 0;
                    this.IDDinas = oSetor.IDDinas;
                    this.KodeUK = oSetor.KodeUK;
                    this.Debet = -1;
                    this.TanggalTransaksi = oSetor.dtBukuKas;
                    this.Kodekategori = oSetor.KodeKategori;
                    this.KodeUrusan = oSetor.KodeUrusan;
                    this.KodeSKPD = oSetor.KodeSKPD;
                    this.KodeUK = oSetor.KodeUK;
                    this.NoBukti = oSetor.NoBukti;
                    this.Keterangan = oSetor.Keterangan;
                    this.UnitAnggaran = oSetor.UnitAnggaran;
                    this.Jumlah = oSetor.Jumlah;
                    this.IsJurnal = 1;
                    this.JenisBelanja = 0;
                    this.JenisSumber = JenisSumber;
                    this.NourutSumber = oSetor.NoUrut;
                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;

                    //hanya keluar yang perlu detail 
                    this.Details = new List<JurnalRekening>();

                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;

                    foreach (SetorRekening spprek in oSetor.Details)
                    {
                            JurnalRekening oJurnalREkening = new JurnalRekening();
                            oJurnalREkening.KodeUk = oSetor.KodeUK;
                            oJurnalREkening.IDUrusan = oSetor.IDUrusan;
                            oJurnalREkening.iIDProgram = oSetor.IDProgram;
                            oJurnalREkening.IDkegiatan = oSetor.IDKegiatan;
                            oJurnalREkening.IDSubkegiatan = oSetor.IDSubKegiatan;
                            oJurnalREkening.KodekategoriPelaksana = oSetor.KodekategoriPelaksana;
                            oJurnalREkening.KodeUrusanPelaksana = oSetor.KodeUrusanPelaksana;
                            oJurnalREkening.KodeProgram = oSetor.KodeProgram;
                            oJurnalREkening.KodeKegiatan = oSetor.KodeKegiatan;
                            oJurnalREkening.KodeSubKegiatan = oSetor.KodeSubKegiatan;
                            oJurnalREkening.idRekening = spprek.IDRekening;
                            oJurnalREkening.Jumlah = spprek.Jumlah;
                            this.Details.Add(oJurnalREkening);
                    }
                    return this;

                }
                public Jurnal CreateFormSTS(STS oSTS, int debet, int JenisSumber, int JenisBendahara)
                {
                    this.Tahun = oSTS.Tahun;
                    this.JenisBelanja = 0;
                    this.IDDinas = oSTS.IDDinas;
                    this.KodeUK = oSTS.KodeUK;
                    this.Kodebank = oSTS.Bank;
                    this.Debet = debet;
                    this.TanggalTransaksi = oSTS.TanggalSTS;
                    this.Kodekategori = oSTS.KodeKategori;
                    this.KodeUrusan = oSTS.KodeUrusan;
                    this.KodeSKPD = oSTS.KodeSKPD;
                    this.KodeUK = oSTS.KodeUK;
                    this.NoBukti = oSTS.NoBukti;
                    this.Keterangan = oSTS.Keterangan;
                    this.UnitAnggaran = 0;
                    this.Jumlah = oSTS.Jumlah;
                    this.IsJurnal = 1;
                    this.JenisBelanja = 0;
                    this.JenisSumber = JenisSumber;
                    this.NourutSumber = oSTS.NoUrut;
                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;

                    //hanya keluar yang perlu detail 
                    this.Details = new List<JurnalRekening>();

                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;

                    foreach (STSRekening stsrek in oSTS.Rekenings)
                    {
                        JurnalRekening oJurnalREkening = new JurnalRekening();
                        oJurnalREkening.KodeUk = oSTS.KodeUK;
                        oJurnalREkening.IDUrusan = oSTS.IDUrusan;
                        oJurnalREkening.iIDProgram = 0;
                        oJurnalREkening.IDkegiatan = 0;
                        oJurnalREkening.IDSubkegiatan = 0;
                        oJurnalREkening.KodekategoriPelaksana = 0;// oSTS.KodekategoriPelaksana;
                        oJurnalREkening.KodeUrusanPelaksana = 0;//oSTS.KodeUrusanPelaksana;
                        oJurnalREkening.KodeProgram = 0;
                        oJurnalREkening.KodeKegiatan = 0;
                        oJurnalREkening.KodeSubKegiatan = 0;
                        oJurnalREkening.idRekening = stsrek.IDRekening;
                        oJurnalREkening.Jumlah = stsrek.Jumlah;
                        this.Details.Add(oJurnalREkening);
                    }
                    return this;

                }
        
                public Jurnal CreateFromKoreksi(Koreksi koreksi, KoreksiDetail koreksidetail, int JenisBendahara)
                {
                    this.Tahun = koreksi.Tahun;
                    this.JenisBelanja = koreksi.JenisBelanja ;
                    this.IDDinas = koreksi.IDDInas;
                    this.KodeUK = koreksi.KodeUK;
                    this.Debet = koreksidetail.Debet1;
                    this.TanggalTransaksi = koreksi.DtKoreksi;
                    this.Kodekategori = koreksi.Kodekategori;
                    this.KodeUrusan = koreksi.KodeUrusan;
                    this.KodeSKPD = koreksi.KodeSKPD;
                    this.KodeUK = koreksi.KodeUK;
                    this.NoBukti = koreksi.NoBukti;
                    this.Keterangan = koreksi.Uraian;
                    this.Jumlah = koreksi.Jumlah;
                    this.UnitAnggaran = koreksi.UnitAnggaran;
                    this.IsJurnal = 1;
                    this.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN ;
                    this.JenisSumber = (int)E_JENIS_REFERENSIJurnal.REFERENSI_KOREKSI;
                    this.NourutSumber = koreksi.NoUrut;
                    this.LevelTampilan = E_LEVLETAMPILANJurnal.eJurnalRekening;
                    this.Kodebank = 1;
                    //hanya keluar yang perlu detail 
                    this.Details = new List<JurnalRekening>();
                    // hanya 
                    JurnalRekening oJurnalREkening = new JurnalRekening();
                    oJurnalREkening.KodeUk = koreksi.KodeUK;
                    oJurnalREkening.IDUrusan = koreksidetail.IDurusan;
                    oJurnalREkening.iIDProgram = koreksidetail.IDProgram;
                    oJurnalREkening.IDkegiatan = koreksidetail.IDKegiatan;
                    oJurnalREkening.IDSubkegiatan = koreksidetail.IDSubKegiatan;
                    oJurnalREkening.KodekategoriPelaksana = koreksidetail.KodeKategoriPelaksana;
                    oJurnalREkening.KodeUrusanPelaksana = koreksidetail.KodeUrusanPelaksana;
                    oJurnalREkening.KodeProgram = koreksidetail.KodeProgram;
                    oJurnalREkening.KodeKegiatan = koreksidetail.KodeKegiatan;
                    oJurnalREkening.KodeSubKegiatan = koreksidetail.KodeSubKegiatan;
                    oJurnalREkening.idRekening = koreksidetail.IDRekening1;
                    oJurnalREkening.Jumlah = koreksidetail.Jumlah1;
                    this.Jumlah = koreksidetail.Jumlah1;
                    this.Debet = koreksidetail.Debet1;

                    this.Details.Add(oJurnalREkening);
                   
            


                    return this;

                }
        
    
        
         * */
# endregion BuatdariTransaksi
    }
    public class JurnalRekening
    {
        public  long NoJurnal{set;get;}
        
        public  int KodeKategori {set;get;}
        public  int KodeUrusan{set;get;}
        public  int KodeSKPD {set;get;}
        public  int KodeUK {set;get;}
        public  int KodeKategoriPelaksana{set;get;}
        public  int KodeUrusanPelaksana{set;get;}
        public  int IDProgram {set;get;}
        public  int IDKegiatan {set;get;}
        public  long IIDRekening {set;get;}
        public  int Nourut{set;get;}
        public  int Debet {set;get;}
        public  decimal Jumlah {set;get;}
        public  string  Keterangan {set;get;}
        public  int Penutup{set;get;}
        public  int Kelompok{set;get;}
        public long  IDSubKegiatan { set; get; }
        

    }
   
    public enum JENIS_SUMBERJURNAL{
            E_SUMBER_DPA = 0,
            E_SUMBER_SKR = 1,
            E_SUMBER_STS = 2,
            E_SUMBER_SETOR = 3,
            S_SUMBER_BAST = 4,
            E_SUMBER_SP2D = 5,
            E_SUMBER_PANJAR = 6,
            E_SUMBER_MANUAL = 7,
            E_JURNAL_PENYESUAIAN = 9,
            E_JURNAL_PENYUSUTAN = 10,
            E_SUMBER_TRXASET = 11,
            E_SUMBER_INVESTASI = 12,
            E_SUMBER_UTANG = 13,
            E_SUMBER_PENUTUP = 20,
       
    }
    public enum JENIS_DETAILJURNAL
    {
        E_JENISLRA = 1,
        E_JENISLO = 2,
        E_JENISKSDA = 3,
    }
    public enum JENIS_JURNAL
    {
        

    JENIS_JURNALPENERIMAAN = 1,
    JENIS_JURNALPENGELUARAN = 2,
    JENIS_JURNALUMUM = 3,
    JENIS_JURNALPENUTUP = 4,
    JENIS_JURNALELIMINASI = 5,
    JENIS_JURNALANGGARAN = 6,
    JENIS_JURNALSUSUT = 7,
    JENIS_JURNALPENYESUAIAN=8,

    }
    
}
