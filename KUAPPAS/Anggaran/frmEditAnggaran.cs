using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Anggaran;

using DTO;
using BP;
using DataAccess;
using Formatting;

namespace KUAPPAS.Anggaran
{
    public partial class frmEditAnggaran : Form
    {
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _headerstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _lokasiStyle = new DataGridViewCellStyle();
        private const int COL_IDREKENING = 0;
        private const int COL_EXPAND = 1;
        private const int COL_HAPUS = 2;
        private const int COL_IDLOKASI = 3;
        private const int COL_LEVEL = 4;
        private const int COL_IDURAIAN = 6;
        private const int COL_TOLEFT = 7;
        private const int COL_TORIGHT = 8;

        private const int COL_DISPLAYREKENING = 9;
        private const int COL_LABEL = 10;
        private const int COL_NO = 11;//10;
        private const int COL_URAIAN = 12;
        private const int COL_VOL = 13;
        private const int COL_SATUAN = 14;
        private const int COL_HARGA = 15;
        private const int COL_PPN = 16;

        private const int COL_JUMLAH = 17;
        private const int COL_VOLMURNI = 18;
        private const int COL_SATUANMURNI = 19;
        private const int COL_HARGAMURNI = 20;
        private const int COL_JUMLAHMURNI = 21;

        private const int COL_TAHAP = 22;
        private const int COL_IDANGGARANKAS = 23;
        private const int COL_ISNEW = 24;
        private const int COL_SHOWINREPORT = 25;
        private const int COL_PLAFON = 26;
        private const int COL_YAD = 27;
        private const int COL_DPA = 28;

        private const int COL_APBD = 29;  // APBD 
        private const int COL_STANDARD = 30;

        private const int COL_IDBARANG = 32;
        private const int COL_IIDRKBMD = 33;
        private const int COL_IDRKBMDBARANG = 34;
        private const int COL_KETERANGAN = 35;
        private int m_IDDinas ;
        private  int m_idUK;

        private int m_IDKegiatan ;
        private int m_IDProgram;
        private long m_IDSubKegiatan;
        private int m_IDUrusan;
        private int m_iTahap;
        private int m_iTahun;
        private int m_iCurrentRow;
        private int m_iCurrentRowSB;
        private int m_iRowJustAdded;
        List<string> lstKodeRekening = new List<string>();
        public frmEditAnggaran()
        {
            InitializeComponent();
             m_iTahun = GlobalVar.TahunAnggaran;
            _hilightstyle.Font = new Font(gridRekening.Font, FontStyle.Bold);
            _hilightstyle.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            
            _normalstyle.Font = new Font(gridRekening.Font, FontStyle.Regular);
            _normalstyle.BackColor = Color.White;

            _headerstyle.Font = new Font(gridRekening.Font, FontStyle.Bold);
            //    _headerstyle.BackColor = Color.LightGray;

            _lokasiStyle.Font = new Font(gridRekening.Font, FontStyle.Bold);

            _lokasiStyle.BackColor = Color.Cyan;

        }
        private string MakeSpace(int berapaKali)
        {
            string sRet = " ";
            for (int i = 0; i < berapaKali; i++)
            {
                sRet = sRet + "    ";
            }
            return sRet;
        }
        public int IDDInas {
            set{
                m_IDDinas = value;
                GetTahap();
            }
        }
        public int KodeUk{
            set {m_idUK= value; }
        }
        public string NamaDinas{
            set {
                //lblOrganisasi.Text = value;
            }
        }
        public int GetTahap()
        {
            TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            TahapanAnggaran ta = oTALogic.GetByDinas(m_IDDinas, GlobalVar.TahunAnggaran);
            m_iTahap = ta.Tahap;
            return ta.Tahap;
        }
        private void frmEditAnggaran_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Data Anggaran");
            gridAnggaranRekening.FormatHeader();
      

          
          
           if (m_iTahap== 2)
           {   gridAnggaranRekening.Columns[3].ReadOnly = false;
               gridAnggaranRekening.Columns[4].ReadOnly = false;
               gridAnggaranRekening.Columns[5].ReadOnly = false;
               gridAnggaranRekening.Columns[6].ReadOnly = false;

               gridAnggaranRekening.Columns[4].Visible= false;
               gridAnggaranRekening.Columns[5].Visible = false;
               gridAnggaranRekening.Columns[6].Visible = false;
           }
           if (m_iTahap == 3)
           {

               gridAnggaranRekening.Columns[3].ReadOnly = true;
               gridAnggaranRekening.Columns[4].ReadOnly = false;
               gridAnggaranRekening.Columns[5].ReadOnly = false;
               gridAnggaranRekening.Columns[6].ReadOnly = false;
               gridAnggaranRekening.Columns[5].Visible = false;
               gridAnggaranRekening.Columns[6].Visible = false;
           }

           if (m_iTahap == 4)
           {

               gridAnggaranRekening.Columns[3].ReadOnly = true;
               gridAnggaranRekening.Columns[4].ReadOnly = true;
               gridAnggaranRekening.Columns[5].ReadOnly = false;
               gridAnggaranRekening.Columns[6].ReadOnly = false;
               gridAnggaranRekening.Columns[6].Visible = false;
           }

           if (m_iTahap == 5)
           {

               gridAnggaranRekening.Columns[3].ReadOnly = true;
               gridAnggaranRekening.Columns[4].ReadOnly = true;
               gridAnggaranRekening.Columns[5].ReadOnly = true;
               gridAnggaranRekening.Columns[6].ReadOnly = false;
           }
           gridAnggaranRekening.Columns[7].ReadOnly = false;
           gridAnggaranRekening.Columns[7].Visible = false;
          
           // if (m_TA.StatusInput ==9){
              cmdSimpan.Visible = false ;
            //} else {
            //  cmdSimpan.Visible = true ;
           //}
        }
        public void SetProgramKegiatanAnggaran(List<ProgramKegiatanAnggaran> lstpk )
        {
            ProgramKegiatan pk = new ProgramKegiatan();
            if (lstpk != null)
            {
                pk.IDKegiatan = lstpk[0].IDKegiatan;
                pk.IDProgram = lstpk[0].IDProgram;
                pk.IDSubKegiatan = lstpk[0].IDSubKegiatan;
                pk.IDUrusan = lstpk[0].IDUrusan;

                m_IDKegiatan =pk.IDKegiatan;
                m_IDProgram=pk.IDProgram;
                m_IDSubKegiatan=pk.IDSubKegiatan ;
                m_IDUrusan=pk.IDUrusan;

                pk.StrIDKegiatan = lstpk[0].StrIDKegiatan;
                pk.StrIDProgram = lstpk[0].StrIDProgram;
                pk.StrIDSubKegiatan = lstpk[0].StrIDSubKegiatan;
                pk.StrIDUrusan = lstpk[0].StrIDUrusan;
                if (pk.KodeUK > 0)
                {
                   // lblOrganisasi.Text = lblOrganisasi.Text + "  |  " + pk.NamaUK;
                }

                pk.NamaProgram = lstpk[0].NamaProgram;
                pk.NamaSubKegiatan = lstpk[0].NamaSubKegiatan;
                pk.NamaKegiatan = lstpk[0].NamaKegiatan;
                pk.NamaUrusan = lstpk[0].NamaUrusan;

                ctrlProgramKegiatanSub1.SetProgramKegiatan(pk);
                foreach (ProgramKegiatanAnggaran p in lstpk)
                {
                    string[] row = { p.IIDRekening.ToString(),p.IIDRekening.ToKodeRekening () ,p.NamaRekening, p.AnggaranMurni.ToRupiahInReport(), p.AnggaranGeser.ToRupiahInReport(), p.AnggaranRKAP.ToRupiahInReport(), p.AnggaranABT.ToRupiahInReport() };
                    gridAnggaranRekening.Rows.Add(row);

                }
            }
            refreshJumah();
            LoadAnggaran();



        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();

            for (int i = 0; i < gridAnggaranRekening.Rows.Count; i++)
            {
                
                TAnggaranRekening o = new TAnggaranRekening();
                o.Tahun = GlobalVar.TahunAnggaran;
                o.Jenis = 3;
                o.IDDinas = m_IDDinas;
                o.IDUrusan = m_IDUrusan;
                o.IDProgram = m_IDProgram;
                o.IDKegiatan = m_IDKegiatan;
                o.IDSubKegiatan = m_IDSubKegiatan;
                o.IDUnit = m_IDDinas  + m_idUK;
                o.IDRekening = DataFormat.GetLong(gridAnggaranRekening.Rows[i].Cells[0].Value);
                o.PPKD = 0;// ctrlDinas1.PPKD();
                o.StatusUpdate = 1;
                o.Tahap = m_iTahap;


                o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));

                if (m_iTahap == 2)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                }
                if (m_iTahap == 3)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[4].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[4].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[4].Value));
                }
                if (m_iTahap == 4)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[4].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[5].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[5].Value));
                }
                if (m_iTahap == 5)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[4].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[5].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranRekening.Rows[i].Cells[6].Value));
                }
                o.IDSubKegiatan = m_IDSubKegiatan;
                if (o.IDRekening> 0)
                    _lstRek.Add(o);

            }
            //if (m_IDJenis == 3)
            //    oLogic.SimpanPlafon(_lsd, (int)GlobalVar.TahunAnggaran, m_IDUrusan, m_IDDinas, m_IDProgram, m_IDKegiatan);
            int iUnit = 0;
            iUnit = m_IDDinas +m_idUK;
        
              if (m_idUK > 0)
                {
                    oLogicRek.SimpanPlafon(_lstRek, (int)GlobalVar.TahunAnggaran, m_IDDinas, iUnit, m_IDUrusan, m_IDProgram, m_IDKegiatan, 3, GlobalVar.TahapAnggaran);
                }
                else
                {
                    oLogicRek.SimpanPlafon(_lstRek, (int)GlobalVar.TahunAnggaran, m_IDDinas, 0, m_IDUrusan, m_IDProgram, m_IDKegiatan, 3, GlobalVar.TahapAnggaran);

                }
         

            MessageBox.Show("Penyimpanan Selesai");
                
        }

        private void treeRekening1_DoubleClicking(global::DTO.Rekening rek)
        {
            if (rek.Root < 6)
            {
                MessageBox.Show("Harus Level Sub Rincian Objek..");
                return;
            }
            bool bFound = false;
            for (int id = 0; id < gridAnggaranRekening.Rows.Count; id++)
            {
                if (rek.ID == DataFormat.GetLong(gridAnggaranRekening.Rows[id].Cells[0].Value))
                {
                    bFound =  true;
                    break;
                }
            }

            if (bFound == false)
            {
                string[] row = { rek.ID.ToString(), rek.ID.ToKodeRekening(), rek.Nama, "0", "0", "0", "0" };
                    
                gridAnggaranRekening.Rows.Add(row);
         
            }
            else
                MessageBox.Show("Rekening sudah ada dalam RKA");
        }
        private void refreshJumah()
        {
            decimal jumlah = 0L;
            decimal jumlahPerubahan = 0;


            for (int i = 0; i < gridAnggaranRekening.Rows.Count; i++)
            {
                if (gridAnggaranRekening.Rows[i].Cells[3].Value != null)
                {
                    decimal d = gridAnggaranRekening.Rows[i].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                    jumlah = jumlah + d;
                }
            }


            txtJumlahMurni.Text = jumlah.ToRupiahInReport();


            for (int i = 0; i < gridAnggaranRekening.Rows.Count; i++)
            {
                if (gridAnggaranRekening.Rows[i].Cells[4].Value != null)
                {
                    decimal d = gridAnggaranRekening.Rows[i].Cells[4].Value.ToString().FormatUangReportKeDecimal();
                    jumlahPerubahan = jumlahPerubahan + d;
                }
            }




            txtJumlahPergeseran.Text = jumlahPerubahan.ToRupiahInReport();

            jumlahPerubahan = 0;
            for (int i = 0; i < gridAnggaranRekening.Rows.Count; i++)
            {
                if (gridAnggaranRekening.Rows[i].Cells[5].Value != null)
                {
                    decimal d = gridAnggaranRekening.Rows[i].Cells[5].Value.ToString().FormatUangReportKeDecimal();
                    jumlahPerubahan = jumlahPerubahan + d;
                }
            }




            txtJumlahPerubahan.Text = jumlahPerubahan.ToRupiahInReport();
            jumlahPerubahan = 0;

            for (int i = 0; i < gridAnggaranRekening.Rows.Count; i++)
            {
                if (gridAnggaranRekening.Rows[i].Cells[6].Value != null)
                {
                    decimal d = gridAnggaranRekening.Rows[i].Cells[6].Value.ToString().FormatUangReportKeDecimal();
                    jumlahPerubahan = jumlahPerubahan + d;
                }
            }




            txtJumlahPergeseranPerubahan.Text = jumlahPerubahan.ToRupiahInReport();





        }

        private void gridAnggaranRekening_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (mProfile < 3)
            //{
            //    if (gridSumberDana.Rows[e.RowIndex].Cells[6].Value.ToString().FormatUangReportKeDecimal() <
            //        gridSumberDana.Rows[e.RowIndex].Cells[7].Value.ToString().FormatUangReportKeDecimal())
            //    {
            //        MessageBox.Show("Tidak boleh lebih kecil dari Realisasi..");
            //        cmdSimpan.Enabled = true;
            //        // return;

            //    }
            //    else
            //    {
            //        cmdSimpan.Enabled = true;
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
                    {
                        refreshJumah();
                    }
        //        }
        //    }
        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }
        private void LoadAnggaran()
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, 0,1);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            int _iJenis = 3; // ctrlJenisAnggaran1.GetID();


            if (m_IDUrusan == 0 && _iJenis == 3)
                return;
            // m_IDUrusan = m_IDUrusan == 0 ? DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 3)) : m_IDUrusan;

            int _bPPKD = 0;
            int idUnit = m_idUK;
            lstRekening = oLogic.Get(m_iTahun,m_IDDinas, m_IDUrusan, m_IDProgram, m_IDKegiatan, _iJenis, _bPPKD, m_iTahap, 0, m_IDSubKegiatan, idUnit);


            gridRekening.Rows.Clear();

            m_iCurrentRow = 0;
            int _barisRekening = 0;
            if (m_iTahap <= 2)
            {
                foreach (TAnggaranRekening ta in lstRekening)
                {

                    string[] row = { ta.IDRekening.ToString(), "-", "Hapus", "0", "0",
                                       "0","0", "", "", ta.IDRekening.ToString().ToKodeRekening(), 
                                       "", "", ta.Nama, "0", "", 
                                       "0","0", ta.JumlahOlah.ToRupiahInReport(), "", "",
                                       "", ta.JumlahMurni.ToRupiahInReport(), ta.TahapInput.ToString(), "0", "1",
                                       "", ta.Plafon.ToRupiahInReport(), ta.JumlahYAD.ToRupiahInReport(), ta.Jumlah.ToRupiahInReport(),"0" ,
                                        "0",  ta.Realisasi.ToRupiahInReport() };


                    gridRekening.Rows.Add(row);
                    _barisRekening = m_iRowJustAdded;
                    gridRekening.Rows[gridRekening.Rows.Count - 2].DefaultCellStyle = _hilightstyle;
                    lstKodeRekening.Add(ta.IDRekening.ToString());
                    foreach (TAnggaranUraian tu in ta.ListUraian)
                    {                   // 0                      ,1    ,2      ,3                      ,4                   ,5   ,6                      , 7   ,8   ,9   ,10                   ,11                                       ,12                    ,13        ,14                        ,15                         ,16  ,17 ,18  ,19  ,20                       , 21                        ,22 ,23
                        string[] rowx = { tu.IDRekening.ToString(),   //0
                                            "-",   
                                            "Hapus",
                                            tu.IDLokasi.ToString(), 
                                            tu.Level.ToString(), 
                                            "0",                         //5
                                            tu.IDUraian.ToString(), 
                                            "<<", 
                                            ">>", 
                                            "", 
                                            tu.Label,                          //10
                                            tu.NoUrut.ToString(), 
                                            MakeSpace((int)tu.Level - 1) + tu.Uraian, 
                                            tu.VolOlah.ToString(), 
                                            tu.Satuan, 
                                            tu.HargaOlah.ToRupiahInReport(),   //15
                                            tu.PPNOlah.ToRupiahInReport(), 
                                            tu.JumlahOlah.ToRupiahInReport(), 
                                            "0", 
                                            "", 
                                            "0",                              //20
                                            "0",
                                            tu.TahapInput.ToString(),
                                            tu.IDAnggaranKAS.ToString(), 
                                            "1", 
                                            tu.ShowInReport == 1 ? "true" : "false",  //25
                                            tu.Plafon.ToRupiahInReport(), 
                                            tu.JumlahYAD.ToRupiahInReport(), 
                                            "", 
                                            tu.IDStandardHarga.ToString(), 
                                            tu.StandardHarga.ToRupiahInReport() ,   //30
                                            "",
                                            tu.IDBarang.ToString(), 
                                            tu.IDRKBMD.ToString(), 
                                            tu.IDRKBMDBArang.ToString()      //34
                                        };

                        gridRekening.Rows.Add(rowx);
                    }

                }
            }
            else
            {
                foreach (TAnggaranRekening ta in lstRekening)
                {
                    //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22


                    string[] row = { ta.IDRekening.ToString(),"-", "Hapus", "0", "0",
                                       "0", "0", "", "", ta.IDRekening.ToString().ToKodeRekening(), 
                                       "", "", ta.Nama, "", "", "","", ta.JumlahOlah.ToRupiahInReport(), "", "", "0",
                                       ta.JumlahMurni.ToRupiahInReport(), "1", "0", "1", "",  //24
                                       ta.Plafon.ToRupiahInReport(), ta.JumlahYAD.ToRupiahInReport(), ta.Jumlah.ToRupiahInReport(),"","",ta.Realisasi.ToRupiahInReport() }; // Realisasi ko 30


                    string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama };
                    gridRekening.Rows.Add(row);
                    _barisRekening = m_iRowJustAdded;
                    gridRekening.Rows[gridRekening.Rows.Count - 2].DefaultCellStyle = _hilightstyle;
                    lstKodeRekening.Add(ta.IDRekening.ToString());

                    foreach (TAnggaranUraian tu in ta.ListUraian)
                    {                   // 0                      ,1    ,2      ,3                      ,4                   ,5   ,6                      , 7   ,8   ,9   ,10                   ,11                                       ,12                    ,13        ,14                        ,15                         ,16  ,17 ,18  ,19  ,20                       , 21                        ,22 ,23
                        string[] rowx = { tu.IDRekening.ToString(), "-", "Hapus", tu.IDLokasi.ToString(), tu.Level.ToString(), "0", tu.IDUraian.ToString(), "<<", ">>", "", tu.Label, tu.NoUrut.ToString(), MakeSpace((int)tu.Level - 1) + tu.Uraian, tu.VolOlah.ToString(), tu.Satuan, tu.HargaOlah.ToRupiahInReport(),tu.PPNOlah.ToRupiahInReport(), tu.JumlahOlah.ToRupiahInReport(), tu.VolMurni.ToString(), tu.Satuan, tu.HargaMurni.ToRupiahInReport(),
                                          tu.JumlahMurni.ToRupiahInReport(), tu.TahapInput.ToString(), tu.IDAnggaranKAS.ToString(), "1", tu.ShowInReport == 1 ? "true" : "false", tu.Plafon.ToRupiahInReport(), tu.JumlahYAD.ToRupiahInReport(), "", tu.IDStandardHarga.ToString(), tu.StandardHarga.ToRupiahInReport() };
                        gridRekening.Rows.Add(rowx);

                        foreach (TAnggaranUraian tu2 in ta.ListUraian)
                        {                   // 0                      ,1    ,2      ,3                      ,4                   ,5   ,6                      , 7   ,8   ,9   ,10                   ,11                                       ,12                    ,13        ,14                        ,15                         ,16  ,17 ,18  ,19  ,20                       , 21                        ,22 ,23
                            string[] rowx2 = { tu2.IDRekening.ToString(),   //0
                                            "-",   
                                            "Hapus",
                                            tu2.IDLokasi.ToString(), 
                                            tu2.Level.ToString(), 
                                            "0",                         //5
                                            tu2.IDUraian.ToString(), 
                                            "<<", 
                                            ">>", 
                                            "", 
                                            tu2.Label,                          //10
                                            tu2.NoUrut.ToString(), 
                                            MakeSpace((int)tu.Level - 1) + tu.Uraian, 
                                            tu2.VolOlah.ToString(), 
                                            tu2.Satuan, 
                                            tu2.HargaOlah.ToRupiahInReport(),   //15
                                            tu2.PPNOlah.ToRupiahInReport(), 
                                            tu2.JumlahOlah.ToRupiahInReport(), 
                                            "0", 
                                            "", 
                                            "0",                              //20
                                            "0",
                                            tu2.TahapInput.ToString(),
                                            tu2.IDAnggaranKAS.ToString(), 
                                            "1", 
                                            tu2.ShowInReport == 1 ? "true" : "false",  //25
                                            tu2.Plafon.ToRupiahInReport(), 
                                            tu2.JumlahYAD.ToRupiahInReport(), 
                                            "", 
                                            tu2.IDStandardHarga.ToString(), 
                                            tu2.StandardHarga.ToRupiahInReport() ,   //30
                                            "",
                                            tu2.IDBarang.ToString(), 
                                            tu2.IDRKBMD.ToString(), 
                                            tu2.IDRKBMDBArang.ToString()      //34
                                        };

                            gridRekening.Rows.Add(rowx2);
                        }
                    }
                    //HitungRekeningIni(_barisRekening);
                }

            }

        
            FormatBaris();
           

            string[] rowadd = { "",   //0
                                            "-",   
                                            "",
                                            "", 
                                            "", 
                                            "",                         //5
                                            "", 
                                            "", 
                                            "", 
                                            "", 
                                            "",                          //10
                                            "", 
                                            "", 
                                            "", 
                                            "", 
                                            "",   //15
                                            "", 
                                            "", 
                                            "", 
                                            "", 
                                            "",                              //20
                                            "",
                                            "",
                                            "", 
                                            "1", 
                                            "",  //25
                                            "", 
                                            "", 
                                            "", 
                                            "", 
                                            "",   //30
                                            "",
                                            "", 
                                            "", 
                                            ""//34
                                        };

            gridRekening.Rows.Add(rowadd);


        }
        private string GetHarga(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_HARGA].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_HARGA].Value);
            }
            else return "";
        }
        private int GetLevel(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return 0;

            if (gridRekening.Rows[iRow].Cells[COL_LEVEL].Value != null)
            {
                return DataFormat.GetInteger(gridRekening.Rows[iRow].Cells[COL_LEVEL].Value);
            }
            else return 0;
        }
        private void FormatBaris()
        {

            for (int i = 0; i < gridRekening.Rows.Count; i++)
            {
                if (GetLevel(i) > 0)
                {
                    if (DataFormat.GetDecimal(GetVOL(i)) == 0 && DataFormat.GetDecimal(GetHarga(i)) == 0)
                    {
                        gridRekening.Rows[i].DefaultCellStyle = _headerstyle;
                    }
                    else
                    {
                        gridRekening.Rows[i].DefaultCellStyle = _normalstyle;
                    }
                }
            }
        }
        private string GetVOL(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_VOL].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_VOL].Value);
            }
            else return "";
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
