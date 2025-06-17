using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using Formatting;
using Syncfusion.Pdf;
using KUAPPAS.SP2DOnline;

using DTO.SP2DOnLine;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using DTO.SP2DOnLine.DetailTransaksiSP2DOnline57;
using DTO.SP2DOnLine._511;
using KUAPPAS.BP.SP2DOnline;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;

//using PdfFileWriter;



namespace KUAPPAS.Bendahara
{
    public partial class frmSPP : Form
    {
        public delegate void UpdateUIDelegate(bool IsDataLoaded);
        private enum MODE_FORM {
            SPP=0,
            SPM,
            SP2D
        }
        private int m_iJenisSubSPP;
        private long m_NoUrut = 0;
        private int m_iPPKD;
        private int m_iJenisSPP;
        private bool m_bNew;
        private int m_iMode ;
        private int m_IDDInas;
        private int m_iKodeUk;

        private int m_IDUrusan;
        private int m_IDProgram;
        private long m_iNoUrutSPD;
        private int m_IDKegiatan;
        private long m_IDSubKegiatan;
        private int m_iStatus;
        private long m_iNoKontrak;
        private SPP m_oSPP;
        private Pejabat m_oBendahara;
        private Pejabat m_oPimpinan;
        private int m_iUnitAnggaran;
        /// <summary>
        /// Cetakkan 
        /// 
        private string m_sJudul1;
        private string m_sJudul2;
        private string m_sJudul3;

        ///*
        ///
        ///  private int m_iKodeUK;
        PdfLayoutResult layoutResult = null;
        int columnCount;
        int count = 0;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
        string[] headerValues = { "OrderID", "CustomerID", "ShipName", "ShipAddress", "ShipCity", "ShipPostalCode" };
        bool isNewPage;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        private int miJenisBendahara;
        private decimal m_cSaldoAwal;
        private int m_iBankSaldoAwal;
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int m_rowSelected = 0;
        int currentContainingCellListIndex;
        /// 
        /// </summary>
        /// 
        private PdfStringFormat m_stringFormat = new PdfStringFormat();
        private PdfStringFormat m_stringFormatLeft = new PdfStringFormat();
        private PdfStringFormat m_stringFormatCenter = new PdfStringFormat();
        private PdfStringFormat m_stringFormatRight = new PdfStringFormat();
        private PdfStringFormat m_stringFormatJustify = new PdfStringFormat();
        private PdfFont mfont6 ;
        private PdfFont mfont7;
        private PdfFont mfont8 ;
        private PdfFont mfont9 ;
        private PdfFont mfont10 ;
        private PdfFont mfont12 ;
        int CONST_IDJABATAN = 50;

        public frmSPP()
        {
            InitializeComponent();
            m_iMode = 0;
            m_iJenisSPP = 0;
            m_iPPKD = 0;
            m_oSPP = new SPP();
            m_bNew = false;
            m_iKodeUk = 0;

            m_oBendahara = new Pejabat ();
            m_oPimpinan = new Pejabat();

            m_stringFormat.Alignment = PdfTextAlignment.Left;

            m_stringFormatLeft.Alignment = PdfTextAlignment.Left;
            m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
            m_stringFormatRight.Alignment = PdfTextAlignment.Right;
            m_stringFormatJustify.Alignment = PdfTextAlignment.Justify;
            m_iUnitAnggaran = 0;
            mfont6 = new PdfTrueTypeFont(new Font("Arial", 6));
            mfont7 = new PdfTrueTypeFont(new Font("Arial", 7));
            mfont8 = new PdfTrueTypeFont(new Font("Arial", 8));
            mfont9 = new PdfTrueTypeFont(new Font("Arial", 9));
            mfont10 = new PdfTrueTypeFont(new Font("Arial", 10));
            mfont12 = new PdfTrueTypeFont(new Font("Arial", 12));
        }
        public void SetModeForm(int  _iMode)
        {
            m_iMode = _iMode;
            switch (m_iMode)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                   
                    break;
            }
            ctrlBendaharaBUD.CreatePenandaTanganSP2D();
        }
        private void frmSPP_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("S P P / S P M / S P 2 D");

            // tabSPP.TabPages.Remove(tabPenerima);
            ctrlPotongan1.FormatTampilan();
            ctrlPotongan2.FormatTampilan();
            ctrlRekeningKegiatan1.Keperluan = 1;
            ctrlDinas1.Create();
            
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD) 
            {
                cmdCetak.Visible = false;
                cmdCetakSP2D.Visible = false;
                tabSPP.TabPages.Remove(tabSPM);
                tabSPP.TabPages.Remove(tabSP2D);

            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK)
            {
                cmdSImpan.Visible = false;
                cmdCetak.Visible = true ;
                cmdCetakSP2D.Visible = false;
                
                tabSPP.TabPages.Remove(tabSP2D);


            }
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUD )
            {
                cmdSImpan.Visible = false;
                cmdCetak.Visible = false;
                cmdCetakSP2D.Visible = true ;
               

            }

            cmbJenisTrabsfer.Items.Add("SKN");
            cmbJenisTrabsfer.Items.Add("RTGS");
            btnGetDataOnUs.Visible = true;

        }
        public bool SetID(long pNoUrut)
        {
            m_NoUrut = pNoUrut;
            SPPLogic oLogic = new SPPLogic((int)GlobalVar.TahunAnggaran);
            SPP oSPP = new SPP();
            oSPP = oLogic.GetByID(m_NoUrut);
            if (oSPP == null)
            {
                if (oLogic.IsError() == true)
                {
                    MessageBox.Show(oLogic.LastError());
                }
                return false;
            }
            else
                return SetSPP(oSPP);
        }
        public bool SetSPP(SPP oSPP )
        {
            try
            {
                m_bNew = false;
                m_oSPP = oSPP;
                m_IDDInas = m_oSPP.IDDInas;
                m_iKodeUk = m_oSPP.KodeUK;
                ctrlRekeningKegiatan1.Dinas = m_IDDInas;
                ctrlDinas1.SetID(m_IDDInas,m_iKodeUk);

                m_iUnitAnggaran = oSPP.UnitAnggaran;

                if (oSPP.Status <= 2)
                {
                    CreateSPD();
                }
                else
                {
                    CreateSPD(oSPP.NoUrutSPD);
                }

                ctrlSPD1.SetID(oSPP.NoUrutSPD);

                m_NoUrut = m_oSPP.NoUrut;
                m_iPPKD = m_oSPP.PPKD;
                m_iJenisSPP = m_oSPP.Jenis;
                m_IDDInas = m_oSPP.IDDInas;

                ctrlJenisSPP1.Create();
                ctrlJenisSPP1.SetValue(oSPP.Jenis);

                txtNamaPenerima.Text = oSPP.NamaPenerima;
                ctrlDaftarBank1.Create();
                ctrlDaftarBank1.SetKode(oSPP.KodeBank, oSPP.NoRek.Trim());
                ctrlDaftarBank1.KeteranganNamaBank = oSPP.KeteranganNamaBank;

                txtJabatanPenerima.Text = oSPP.JabatanPenerima;
          
                txtNoRekening.Text = oSPP.NoRek;
                txtNoNPWP.Text = oSPP.NoNPWP;
                txtNamaPenerimadalamRekeningBank.Text = oSPP.NamaDalamRekeningBank;
                ATurTinggiTabProgram(m_iJenisSPP);
                RefreshBatasUP();
                if (m_iJenisSPP == 3)
                {
                    ctrlJenisBelanjaSPP1.Create();

                    if (oSPP.JenisKegiatan > 0)
                    {
                        ctrlJenisBelanjaSPP1.SetID(oSPP.JenisKegiatan);

                        if (oSPP.JenisKegiatan == 62)
                  {
                      ctrlRekeningKegiatan1.Clear();
                      ctrlRekeningKegiatan1.CreateNewSPP(
                          m_IDDInas,
                          0,
                          0,
                          0,
                          m_iNoUrutSPD,
                          dtSPP.Value,
                          m_iJenisSPP,
                          m_iUnitAnggaran
                          );
                  }



                    }
                }
                if (m_iJenisSPP == 1)
                {
                    ctrlSPJUP1.Create (m_IDDInas,1,DataFormat.GetLong(oSPP.NoSPJUP));
                    ctrlSPJUP1.Visible = true;

                  //  ctrlSPJUP1.SetID(DataFormat.GetLong(oSPP.NoSPJUP));
                }
                txtUraian.Text = m_oSPP.Peruntukan;
                txtJumlah.Text = m_oSPP.Jumlah.ToRupiahInReport();

                if (m_oSPP.Jenis > 1)
                {
                    ctrlUrusanPemerintahan1.Create(m_IDDInas, m_oSPP.Tahun);
                    
                    if (m_oSPP.Rekenings.Count > 0)
                    {

                        ctrlUrusanPemerintahan1.SetID(m_oSPP.Rekenings[0].IDUrusan);

                        
                    }
                    
                    int iduruusan = 0;
                    foreach (SPPRekening sr in m_oSPP.Rekenings)
                    {
                        iduruusan = sr.IDUrusan;
                    }
                    chkBanyakKegiatan.Checked = m_oSPP.BanyakKegiatan == 1 ? true : false;
                    ShowHideBanyakKegiatan(m_oSPP.BanyakKegiatan == 1); 
                    
                    txtNoSPP.Width = ctrlJenisSPP1.Width;
                    if (oSPP.JenisKegiatan != 62)
                    {
                        ctrlProgram1.Create(m_oSPP.Tahun, m_IDDInas, iduruusan, m_oSPP.Rekenings);
                        //ctrlKegiatanAPBD1.Create(m_oSPP.Tahun, m_IDDInas, iduruusan, ctrlProgram1.GetID(), m_oSPP.Rekenings);
                        m_IDProgram = ctrlProgram1.GetID();
                        ctrlKegiatanAPBD1.CreateWIthUK(m_oSPP.Tahun, m_IDDInas, m_iUnitAnggaran, m_IDProgram, m_oSPP.Rekenings);
                        m_IDKegiatan = ctrlKegiatanAPBD1.GetID();

                        ctrlSubKegiatan1.CreateWithUK(m_oSPP.Tahun, m_IDDInas, m_iUnitAnggaran, m_IDKegiatan, m_oSPP.Rekenings);
                        m_IDSubKegiatan = ctrlSubKegiatan1.GetID();
                        //ctrlSubKegiatan1.Create(m_oSPP.Tahun, m_IDDInas, iduruusan, ctrlProgram1.GetID(),ctrlKegiatanAPBD1.GetID(), 0, m_oSPP.Rekenings );
                    }
                }
           
                
                //if(oSPP.Jenis==3){
                //    ctrlPPTK.Create(oSPP.IDDInas,oSPP.KodeUK,CONST_IDJABATAN);
                //    ctrlPPTK.SetID (DataFormat.GetInteger(oSPP.IDPPTK));
                //}
                dtSPP.Value = oSPP.dtSPP.Date;
                txtNoSPP.Text = oSPP.NoSPP;
                txtNoSP2D.Text = oSPP.NoSP2D;
                m_iStatus = oSPP.Status;
                if (m_iStatus == 0)
                {
                    cmdCetak.Visible = false;
                    cmdRefreshBendahara.Visible = true;
                }
                if (m_iStatus > 0)
                {
                    txtPrefixSPM.Visible = false;
                    txtNoSPM.Width = txtNoSPM.Width + txtPrefixSPM.Width;
                    txtPrefixSPM.Text = "";

                    dtSPM.Value = oSPP.dtSPM.Date;
                    cmdRefreshBendahara.Visible = false;

                }
                if (m_iStatus == 3 || m_iStatus == 4)
                {
                    txtPrefixSP2d.Visible = false;
                    txtPrefixSP2d.Text = "";
                    txtNoSP2D.Width = txtNoSP2D.Width + txtPrefixSP2d.Width;
                    dtSP2D.Value = oSPP.dtTerbit.Date;
                }
                if (m_iStatus == 10)
                {
                    optDiTolak.Checked = true;
                }                
               



                ctrlBendaharaBUD.CreatePenandaTanganSP2D();
                ctrlBendaharaBUD.SetID(oSPP.PenandatanganSP2d);
                txtUraian.Text = oSPP.Keterangan;
                UpdateByJenis(oSPP.Jenis);


                ctrlSumberDana2.Create();
                ctrlSumberDana2.SetID(oSPP.SUmberDana);//, oSPP.SubSumberDana, oSPP.KeteranganSumberDana);
                if (oSPP.INoUrutKontrak > 0)
                {
                    ctrlKontrak1.Create(m_IDDInas, oSPP.dtSPP);//, oSPP.INoUrutKontrak.ToString());
                    ctrlKontrak1.SetID(oSPP.INoUrutKontrak);
                    ctrlBAST1.Create(m_IDDInas, DataFormat.GetLong(oSPP.INoUrutKontrak));
                    ctrlBAST1.SetID(DataFormat.GetLong(oSPP.NoBAST));
                    rbBendahara.Checked = oSPP.Penerima == 0 ? true : false;
                }
                else
                {
                    ctrlKontrak1.Enabled = false;
                    ctrlBAST1.Enabled = false;
                }
                if (oSPP.Jenis > 1)
                {
                    if (m_oSPP.BanyakKegiatan == 0)
                    {
                        ctrlRekeningKegiatan1.CreateSPP(oSPP);
                    }
                    else
                    {
                        decimal jumlah = 0;
                        foreach (SPPRekening sr in m_oSPP.Rekenings)
                        {
                            if (sr.IDRekening == 510202010060)
                            {
                                Console.WriteLine(sr.IDRekening.ToString() );
                            }
                            if (sr.IDSubKegiatan == 214032010012)
                            {
                                MessageBox.Show(m_IDSubKegiatan.ToString());
                            }
                            ctrlRekeningKegiatan1.DisplaySPP(
                                m_IDDInas,
                                sr.IDProgram ,
                                sr.IDKegiatan,
                                sr.IDSubKegiatan,
                                m_oSPP.NoUrutSPD,
                                m_oSPP.dtSPP,
                                m_oSPP.Jenis,
                                m_oSPP.UnitAnggaran, true, sr.IDRekening, sr.Jumlah, m_oSPP.NoUrut

                            );
                            jumlah = jumlah + sr.Jumlah;


                        }
                        txtJumlahSPP.Text = jumlah.ToRupiahInReport();
                    }


                }
                
                rbPihakIII.Checked = oSPP.Penerima > 0 ? true : false;
                rbBendahara.Checked = oSPP.Penerima > 0 ? false : true;




                if (oSPP.Penerima > 0)
                {

                    DisplayPerusahaan(true, oSPP.Penerima);

                }
                else
                {
                    ctrlPerusahaan1.Visible = false;



                }

                
       
                ctrlPotongan1.SetNoUrut(1, m_NoUrut);
                txtJumlahMpn.Text = ctrlPotongan1.JumlahPotongan.ToRupiahInReport();

                ctrlPotongan2.SetNoUrut(0, m_NoUrut);
                txtJumlahNonMpn.Text = ctrlPotongan2.JumlahPotongan.ToRupiahInReport();

                switch (oSPP.Status)
                {
                    case 0:
                        txtPrefixSPP.Text = "";
                        switch (oSPP.Jenis)
                        {
                            case 0:
                                txtPrefixSPM.Text = "/SPM/UP/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 1:
                                txtPrefixSPM.Text = "/SPM/GU/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 2:
                                txtPrefixSPM.Text = "/SPM/TU/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 3:
                                txtPrefixSPM.Text = "/SPM/LS/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 4:
                                txtPrefixSPM.Text = "/SPM/GJ/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 5:
                                txtPrefixSPM.Text = "/SPM/PPKD/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                        }
                        break;

                    case 1:
                    case 2:
                    case 6:
                    case 3:

                        //txtPrefixSPP.Text = "";
                        //txtPrefixSPM.Text = "";

                        txtNoSPM.Text = oSPP.NoSPM;
                        txtNoSPM.Text = oSPP.NoSPM;
                        txtNoSPP.Text = oSPP.NoSPP;
                        txtNoSPP.Width = ctrlJenisSPP1.Width;
                        txtNoSPM.Width = dtSPM.Width;
                        txtPrefixSPM.Visible = false;
                        txtPrefixSPP.Visible= false;
                        
                        switch (oSPP.Jenis)
                        {
                            case 0:
                                txtPrefixSP2d.Text = "/SP2D/UP/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 1:
                                txtPrefixSP2d.Text = "/SP2D/GU/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 2:
                                txtPrefixSP2d.Text = "/SP2D/TU/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 3:
                                txtPrefixSP2d.Text = "/SP2D/LS/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 4:
                                txtPrefixSP2d.Text = "/SP2D/GJ/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                            case 5:
                                txtPrefixSP2d.Text = "/SP2D/PPKD/" + GlobalVar.TahunAnggaran.ToString();
                                break;
                        }
                        break;
                    case 4:
                    case 9:
                        //txtPrefixSPP.Text = "";
                        //txtPrefixSPM.Text = "";
                        //txtPrefixSP2d.Text = "";

                        txtNoSPM.Text = oSPP.NoSPM;
                        txtNoSPP.Text = oSPP.NoSPP;
                        txtNoSP2D.Text = oSPP.NoSP2D;
                        txtNoSPP.Width = ctrlJenisSPP1.Width;
                        txtNoSPM.Width = dtSPM.Width;
                        txtNoSP2D.Width = dtSP2D.Width;
                     
                        txtPrefixSPM.Visible = false;
                        txtPrefixSPP.Visible= false;
                        txtPrefixSP2d.Visible = false;
                        break;


                }
                //if (GlobalVar.Pengguna.Kelompok == 2)
                //{
                if (oSPP.NamaPenandaTanganSPM == "" || oSPP.NamaPenandaTanganSPM==null)
                    {
                        Pejabat pimpinan = new Pejabat();
                        DateTime d = DateTime.Now.Date;
                        pimpinan = ctrlDinas1.GetPimpinan(d);
                        if (pimpinan != null)
                        {
                            txtNamaPA.Text = pimpinan.Nama;
                            txtNIPPA.Text = pimpinan.NIP;
                            txtJabatanPA.Text = pimpinan.Jabatan;

                        }


                    }
                    else
                    {
                        txtNamaPA.Text = oSPP.NamaPenandaTanganSPM ;
                        txtNIPPA.Text = oSPP.NIPPenandaTanganSPM;
                        txtJabatanPA.Text = oSPP.JabatanPenandaTanganSPM;
                    }

                //}
                    if (oSPP.Jenis == 0)
                        // Kalau non UP akan ter refresh saat menampilkan rekeningnya ..
                        txtJumlah.Text = oSPP.Jumlah.ToRupiahInReport();

                    if (DataFormat.GetInteger(oSPP.IDPPTK)>0)
                    {
                    //ctrlPPTK.Create(oSPP.IDDInas,oSPP.KodeUK,CONST_IDJABATAN);
                    ctrlPPTK.SetID (DataFormat.GetInteger(oSPP.IDPPTK));
                    }
                if (oSPP.idcrt != null && oSPP.idcrt > 0)
                {
                    if (ctrlFooter1 != null)
                    {
                        ctrlFooter1.IDCrt = oSPP.idcrt;
                        ctrlFooter1.WaktuBuat = oSPP.tcrt;
                    }
                }
                if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_PPK)
                {
                    cmdSImpan.Visible = false;
                    cmdCetakSP2D.Visible = false;
                    cmdAdd.Visible = false;
                    cmdSimpanSP2D.Visible = false;

                }
                if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD || 
                    GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_PEMBANTU_SKPD
                    )
                {
                    cmdCetak.Visible = false;
                    cmdCetakSP2D.Visible = false;
                    cmdSimpanSP2D.Visible = false;
                    cmdSimpanSPM.Visible = false;

                }
                if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUD ||
                    GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDCETAKSP2D ||
                    GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDSP2DONLINE 
                    )
                {
                    cmdCetak.Visible = false;
                    cmdSimpanSPM.Visible = false;
                    cmdSImpan.Visible = false;
                    cmdAdd.Visible = false;

                }
                if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDVERIFIKASISPM)
                {
                    ctrlAlasanPenolakan1.Create();
                }
                else
                {
                    ctrlAlasanPenolakan1.Visible= false;
                }
                LoadAlasanPenolakan();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }  

        }
        private void  DisplayPerusahaan(bool bDisplay ,int iPenerima){

            ctrlPerusahaan1.Visible = bDisplay;
            lblPerusahaan.Visible = bDisplay;
            lblBentukPerusahaan.Visible = bDisplay;
            lblAlamatPerusahaan.Visible = bDisplay;
            lblNamaPimpinan.Visible = bDisplay;
            lblNPWP.Visible = bDisplay;
            lblBankPihakIII.Visible = bDisplay;
            lblNoRekeningPihakIII.Visible = bDisplay;

            if (iPenerima != 0)
            {
                rbPihakIII.Checked = true;
                ctrlPerusahaan1.SetID(iPenerima);
                Perusahaan perusahaan = ctrlPerusahaan1.GetPerusahaan();
                txtNamaPenerima.Text = perusahaan.Pimpinan;

                txtNamaPenerimadalamRekeningBank.Text = perusahaan.NamaDalamRekeningBank;
                ctrlDaftarBank1.SetKode(perusahaan.KodeBank, perusahaan.Rekening);
                txtNoRekening.Text = perusahaan.Rekening;
                txtNoNPWP.Text = perusahaan.NPWP;

            }
        
        }
    
        public void SetPerusahaan(Perusahaan p){


            if (p != null)
            {
               
                SetDisplayMode(false);



            }

        }
        private void SetDisplayMode(bool bDisplay)
        {

            txtNoRekening.BackColor = bDisplay == true ? UserControl.DefaultBackColor : Color.White;
            txtNoRekening.BorderStyle = bDisplay == true ? BorderStyle.None : BorderStyle.FixedSingle;


        }
        private void ShowHideProgramKegiatan()
        {

            List<SPPRekening> lst = new List<SPPRekening>();
            SPPRekening sr = new SPPRekening();
            if (m_iPPKD == 0)
            {
                switch (m_iJenisSPP)
                {
                    case 0:
         
                        break;
                    case 1:
                        break;
                    case 2:
                        tabProgramKegiatan.Visible = true;
                        //ctrlProgramKegiatan1.SetSumber(3);
                        //ctrlProgramKegiatan1.SetNoUrutSP2D(m_NoUrut);
                        //ctrlProgramKegiatan1.Visible = true;
                        //ctrlProgramKegiatan1.SetValue(m_IDDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan,0);

                        
                        ctrlRekeningKegiatan1.Top = tabProgramKegiatan.Top + tabProgramKegiatan.Height;
                        break;
                    case 3:
                    
                        lst = m_oSPP.Rekenings;
                        //ctrlProgramKegiatan1.Visible = true;
                        //ctrlProgramKegiatan1.SetNoUrutSP2D(m_NoUrut);
                        //ctrlProgramKegiatan1.SetSumber(3);
                        if (m_oSPP.Rekenings != null)
                        {
                            if (m_oSPP.Rekenings.Count > 0)
                            {
                                sr = m_oSPP.Rekenings[0];
                                m_IDUrusan = sr.IDUrusan;
                                m_IDProgram = sr.IDProgram;
                                m_IDKegiatan = sr.IDKegiatan;
                                m_IDSubKegiatan = sr.IDSubKegiatan;
                            }
     //                       ctrlProgramKegiatan1.SetValue(m_IDDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSUbKegiatan);
                    
                        }

                        
                        //ctrlRekeningKegiatan1.Top = tabProgramKegiatan.Top + tabProgramKegiatan.Height;
                        break;
                    case 4:
                        if (GlobalVar.TahunAnggaran <= 2020)
                        {
                            tabProgramKegiatan.Visible = false;
                         
                        }
                        else
                        {
                            lst = m_oSPP.Rekenings;
                            //ctrlProgramKegiatan1.SetNoUrutSP2D(m_NoUrut);
                            //ctrlProgramKegiatan1.SetSumber(3);
                            if (m_oSPP.Rekenings != null)
                            {
                                if (m_oSPP.Rekenings.Count > 0) {
                                
                                    sr = m_oSPP.Rekenings[0];
                                    m_IDUrusan = sr.IDUrusan;
                                    m_IDProgram = sr.IDProgram;
                                    m_IDKegiatan = sr.IDKegiatan;
                                    m_IDSubKegiatan = sr.IDSubKegiatan;
                                    //                            ctrlProgramKegiatan1.SetValue(m_IDDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSUbKegiatan);
                                    // LS hanya satu peogram kegiatan 
                                }
                            }


                            //ctrlRekeningKegiatan1.Top = tabProgramKegiatan.Top + tabProgramKegiatan.Height;
 

                        }
                        break;


                }
            }
            else
            {
               //// tabProgramKegiatan.Visible = false;
               // ctrlRekeningKegiatan1.Top = tabProgramKegiatan.Top;
               // ctrlRekeningKegiatan1.Height = ctrlRekeningKegiatan1.Height + tabProgramKegiatan.Height;
            }
        }
        private void tcbUmum_Click(object sender, EventArgs e)
        {


        }
        private void GetBEndahara()
        {

            if (ctrlBendaharaBUD.Create(m_IDDInas,m_iKodeUk) > 0)
            {
                ctrlBendaharaBUD.Visible = true;

            }
            else
            {
                ctrlBendaharaBUD.Visible = false;

           
      
            }

        }
        //private void CreateSPD()
        //{
        //    DateTime dt = dtSPP.Value.Date;
        //    int iJenisSPP = ctrlJenisSPP1.GetID();
        //    int iJenisSPD = 0;

        //    switch (iJenisSPP)
        //    {
        //        case 0:
        //            iJenisSPD = 4;
        //            break;
        //        case 1:
        //            iJenisSPD = 4;
        //            break;
        //        case 2:
        //            iJenisSPD = 4;
        //            break;
        //        case 3:
        //            iJenisSPD = 4;
        //            break;
        //        case 4:
        //            iJenisSPD = 3;
        //            break;


        //    }

        //    ctrlSPD1.Create(m_IDDInas, dt.Date, iJenisSPD);

        //}

        private void ctrlSPD1_Load(object sender, EventArgs e)
        {
           
        }

        private void ctrlBAST1_OnChanged(long pID)
        {
            BAST oBAST = new BAST();
            oBAST = ctrlBAST1.GetBAST(pID);

            if (oBAST != null)
            {
                

                m_IDUrusan = oBAST.IDUrusan;
                m_IDProgram = oBAST.IDProgram;
                m_IDKegiatan = oBAST.IDKegiatan;
                m_IDSubKegiatan = oBAST.IDSubKegiatan;
                txtKeteranganBAST.Text = oBAST.Uraian;
                lblKeteranganBAST.Visible = true;
          
                ctrlUrusanPemerintahan1.Create(m_IDDInas, GlobalVar.TahunAnggaran);
                ctrlUrusanPemerintahan1.SetID(m_IDUrusan);
                ctrlUrusanPemerintahan1.Enabled = false;
                ctrlProgram1.Create(GlobalVar.TahunAnggaran, m_IDDInas, m_IDUrusan);
                ctrlProgram1.SetID(m_IDProgram);
                ctrlProgram1.Enabled = false;
                ctrlKegiatanAPBD1.CreateWIthUK(GlobalVar.TahunAnggaran, m_IDDInas, m_iUnitAnggaran, m_IDProgram);
                ctrlKegiatanAPBD1.SetID(m_IDKegiatan);
                ctrlKegiatanAPBD1.Enabled = false;
                ctrlSubKegiatan1.CreateWithUK(GlobalVar.TahunAnggaran, m_IDDInas, m_iUnitAnggaran, m_IDKegiatan);
                ctrlSubKegiatan1.SetID(m_IDSubKegiatan);
                ctrlSubKegiatan1.Enabled = false;



                // ctrlRekeningKegiatan1.LoadAnggaran(ctrlDinas1.GetTahapAnggaran());
                ctrlRekeningKegiatan1.Clear();
                ctrlRekeningKegiatan1.SetProgramKegiatan(m_IDDInas, m_iUnitAnggaran, m_IDUrusan, m_IDProgram, m_IDKegiatan, 3, 0, m_IDSubKegiatan);

                if (ctrlSPD1.GetID() > 0)
                {
                    ctrlRekeningKegiatan1.CreateNewSPP(m_IDDInas, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan, ctrlSPD1.GetID(), dtSPP.Value.Date, 3, m_iUnitAnggaran);
                }

                ctrlRekeningKegiatan1.SetBAST(oBAST);
                txtJumlah.Text = ctrlRekeningKegiatan1.JumlahRekening.ToRupiahInReport();
                txtJumlahSPP.Text = ctrlRekeningKegiatan1.JumlahRekening.ToRupiahInReport();
                rbPihakIII.Checked = true;
                DisplayPerusahaan(true, oBAST.PihakKetiga);


            }
        }

        private void ctrlJenisSPP1_OnChanged(int pID)
        {
            
            m_iJenisSPP = pID;
            if (ctrlDinas1.PilihanValid == false)
            {
                return;
            }
            UpdateByJenis(pID);
            RefreshBatasUP();
          
                if (m_iJenisSPP == 3 || m_iJenisSPP==4)
                {
                    //ctrlUrusanPemerintahan1.Create(m_IDDInas,
                    //         GlobalVar.TahunAnggaran);
                    ctrlKontrak1.Create(m_IDDInas, dtSPP.Value);

                }
                ATurTinggiTabProgram(m_iJenisSPP);

               
        }
        private void RefreshBatasUP(){
            try
            {
               
                    if (m_iJenisSPP == 0)
                    {
                        if (m_iStatus == 0)
                        {
                            BatasUPLogic oLogic = new BatasUPLogic(GlobalVar.TahunAnggaran);
                            BatasUP bu = new BatasUP();
                            bu = oLogic.GetByDinas(m_IDDInas, GlobalVar.TahunAnggaran);
                            if (bu != null)
                            {
                                txtBatasUP.Text = bu.Jumlah.ToRupiahInReport();
                                lblBataUP.Visible = true;
                                txtBatasUP.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show(oLogic.LastError());
                                cmdSImpan.Enabled = false;

                            }
                        }
                    }
                    else
                    {
                        lblBataUP.Visible = false;
                        txtBatasUP.Visible = false;

                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ATurTinggiTabProgram(int jenisSPP)
        {
            if (jenisSPP == 1)
            {
                tabProgramKegiatan.Height = 73;
                ctrlRekeningKegiatan1.Top = 76;
                ctrlRekeningKegiatan1.Height = 370;
                tabProgramKegiatan.TabPages.Remove(tapLSTU); 

            }
            else
            {
                tabProgramKegiatan.Height = 207;
                ctrlRekeningKegiatan1.Top = 210;
                ctrlRekeningKegiatan1.Height = 245;
                tabProgramKegiatan.TabPages.Remove(tabGU); 
                

            }
        }
        private void SetKontrakVisibility()
        {
            //m_iJenisSPP = pID;

            //UpdateByJenis(pID);
           // panel1.Visible = false;
            ctrlKontrak1.Visible = false;
            ctrlBAST1.Visible = false;
            lblKontrak.Visible = false;
            lblBAST.Visible = false;
            txtJumlah.Visible = false;

            if (m_iJenisSPP == 0)
            {
                txtJumlah.Visible = true;

                
            }
            if (m_iJenisSPP == 3)
            {
               // panel1.Visible = true;
                ctrlKontrak1.Visible = true;
                ctrlBAST1.Visible = true;
                lblKontrak.Visible = true;
                lblBAST.Visible = true;

        

            }


        }
        private void UpdateByJenis(int pJenis)
        {
           
        
            DisplayPerusahaan(false, 0);

            cmdAdd.Visible = false;
            ctrlKontrak1.Visible = false;
            ctrlBAST1.Visible = false;
            txtKeteranganBAST.Visible = false;
            lblKeteranganBAST.Visible = false;
            lblKontrak.Visible = false;
            lblBAST.Visible = false;



            switch (pJenis)
            {
                case 0:
                    tabSPP.TabPages.Remove(tabRekening);
                    tabSPP.TabPages.Remove(tabPotongan);

                    ctrlJenisBelanjaSPP1.Visible = false;
                    m_iJenisSubSPP = 0;
                    if (m_iStatus == 0)
                    {
                        if (m_bNew == true)
                        {
                            txtPrefixSPP.Text = "/SPP/UP/" + GlobalVar.TahunAnggaran.ToString();
                        }
                        else
                        {
                            txtPrefixSPM.Text = "/SPM/UP/" + GlobalVar.TahunAnggaran.ToString();
                        }
                    }
                    txtJumlah.Visible = true;
    
                    DisplayBendahara();
                    txtJumlah.Enabled = true;
                    txtJumlah.Visible = true;
                    label20.Visible = true;
                    label20.Text = "Jumlah SPP UP";
                    lblKeteranganBAST.Visible = false;


                    ctrlSumberDana2.Create();
            
                    break;
                case 1:
                    
                    if (m_iStatus == 0)
                    {
                        if (m_bNew == true)
                        txtPrefixSPP.Text= "/SPP/GU/" + GlobalVar.TahunAnggaran.ToString();
                        else 
                        txtPrefixSPM.Text = "/SPM/GU/" + GlobalVar.TahunAnggaran.ToString();
                    }
                    //tabSPP.TabPages.Add(tabRekening);
                    //tabSPP.TabPages.Add(tabPenerima);
                    tabSPP.TabPages.Remove(tabPotongan);
                    if (tabProgramKegiatan.TabPages.Contains(tabGU) == false)
                    {
                        tabProgramKegiatan.TabPages.Add(tabGU);
                    }
                    DisplayBendahara();
               
                    ctrlKontrak1.Visible = false ;
                    ctrlBAST1.Visible = false;
                    lblKontrak.Visible = false;
                    lblBAST.Visible = false;
                    txtKeteranganBAST.Visible = false;
                    lblKeteranganBAST.Visible = false;
                    label20.Enabled = false;
                    label20.Text = "Jumlah SPP";
                    lblKeteranganBAST.Visible = false;

                    ctrlJenisBelanjaSPP1.Visible = false;
                    m_iJenisSubSPP = 0;
                    lblJenisBelanja.Visible = false;
                    ctrlSPJUP1.Create(m_IDDInas, 1, DataFormat.GetLong( m_oSPP.NoSPJUP) );
                    ctrlSPJUP1.Visible = true;

                    ctrlSumberDana2.Create();

                    break;
                case 2:
                    cmdAdd.Visible = true;
                    DisplayBendahara();
                   DisplayPerusahaan(false,0);
                    ctrlKontrak1.Visible = true;
                    ctrlBAST1.Visible = true;
                    txtKeteranganBAST.Visible = true;
                    lblKeteranganBAST.Visible = true;
                    if (tabSPP.TabPages.Contains(tabRekening) == false)
                    {
                        tabSPP.TabPages.Contains(tabRekening);
                    }

                    //ctrlKontrak1.Visible = true;
                    // ctrlBAST1.Visible = true;
                    //lblKontrak.Visible = true;
                    //lblBAST.Visible = true;
           
                    label20.Enabled = false;
                    label20.Text = "Jumlah SPP";
           
            
                   // tabProgramKegiatan.TabPages.Add(tapLSTU);
                    ctrlUrusanPemerintahan1.Create(m_IDDInas,
            GlobalVar.TahunAnggaran);
                    if (m_iStatus == 0)
                    {
                        if (m_bNew == true)
                        txtPrefixSPP.Text = "/SPP/TU/" + GlobalVar.TahunAnggaran.ToString();
                        else
                        txtPrefixSPM.Text = "/SPM/TU/" + GlobalVar.TahunAnggaran.ToString();
                  
                    }
                    ctrlPPTK.Create(m_IDDInas, 0, CONST_IDJABATAN);
            

                    break;
                case 3:
                    
                    DisplayPerusahaan(true,0);
                    ctrlKontrak1.Visible = true;
                    ctrlBAST1.Visible = true;
                    txtKeteranganBAST.Visible = true;
                    lblKeteranganBAST.Visible = true;
                    if (tabSPP.TabPages.Contains(tabRekening) == false)
                    {
                        tabSPP.TabPages.Contains(tabRekening);
                    }
                    ctrlUrusanPemerintahan1.Create(m_IDDInas,
            GlobalVar.TahunAnggaran);
                    ctrlKontrak1.Visible = true;
                     ctrlBAST1.Visible = true;
                    lblKontrak.Visible = true;
                    lblBAST.Visible = true;
           
                    label20.Enabled = false;
                    label20.Text = "Jumlah SPP";
           
            
                   // tabProgramKegiatan.TabPages.Add(tapLSTU);

                    if (m_iStatus == 0)
                    {
                        if (m_bNew == true)
                        txtPrefixSPP.Text = "/SPP/LS/" + GlobalVar.TahunAnggaran.ToString();
                        else
                        txtPrefixSPM.Text = "/SPM/LS/" + GlobalVar.TahunAnggaran.ToString();
                        if (m_bNew == true )
                        ctrlJenisBelanjaSPP1.Create();
                    }
                    //tabProgramKegiatan.TabPages.Add(tapLSTU);
                    ctrlJenisBelanjaSPP1.Visible = true;

                    ctrlSumberDana2.Create();
                   lblJenisBelanja.Visible = true;
                   ctrlPPTK.Create(m_IDDInas, 0, CONST_IDJABATAN);

                   break;
                case 4:
                   DisplayPerusahaan(false ,0);
                   DisplayBendahara();
                   ctrlKontrak1.Visible = false;
                   ctrlBAST1.Visible = false;
                   txtKeteranganBAST.Visible = true;
                   lblKeteranganBAST.Visible = true;
                   ctrlKontrak1.Visible = false ;
                   ctrlBAST1.Visible = false;
                   lblKontrak.Visible = false;
                   lblBAST.Visible = false;
                   
                   label20.Enabled = false;
                   label20.Text = "Jumlah SPP";
                   lblKeteranganBAST.Visible = false;


                   
                   lblJenisBelanja.Visible = false;
                   ctrlJenisBelanjaSPP1.Visible = false;
                   ctrlUrusanPemerintahan1.Create(m_IDDInas,
           GlobalVar.TahunAnggaran);
                   // tabProgramKegiatan.TabPages.Add(tapLSTU);
                   if (tabSPP.TabPages.Contains(tabRekening) == false)
                   {
                       tabSPP.TabPages.Contains(tabRekening);
                   }
                    if (m_iStatus == 0)
                    {
                        if (m_bNew == true)
                        txtPrefixSPP.Text = "/SPP/GJ/" + GlobalVar.TahunAnggaran.ToString();
                        else
                        txtPrefixSPM.Text = "/SPM/GJ/" + GlobalVar.TahunAnggaran.ToString();
                    }
                    ctrlPPTK.Create(m_IDDInas, 0, CONST_IDJABATAN);
                    ctrlSumberDana2.Create();
                    break;

                case 5:

                    DisplayPerusahaan(true,0);
                    ctrlKontrak1.Visible = true;
                    ctrlBAST1.Visible = true;
                    txtKeteranganBAST.Visible = true;
                    lblKeteranganBAST.Visible = true;
                    if (tabSPP.TabPages.Contains(tabRekening) == false)
                    {
                        tabSPP.TabPages.Contains(tabRekening);
                    }

                    ctrlKontrak1.Visible = true;
                     ctrlBAST1.Visible = true;
                    lblKontrak.Visible = true;
                    lblBAST.Visible = true;
           
                    label20.Enabled = false;
                    label20.Text = "Jumlah SPP";
           
            
                   // tabProgramKegiatan.TabPages.Add(tapLSTU);

                    if (m_iStatus == 0)
                    {
                        if (m_bNew == true)
                        txtPrefixSPP.Text = "/SPP/LSPPKD/" + GlobalVar.TahunAnggaran.ToString();
                        else
                            txtPrefixSPM.Text = "/SPM/LSPPKD/" + GlobalVar.TahunAnggaran.ToString();
                        if (m_bNew == true )
                        ctrlJenisBelanjaSPP1.Create();
                    }
                   ctrlJenisBelanjaSPP1.Visible = false;
                   ctrlSumberDana2.Create();
                   lblJenisBelanja.Visible = false;
                   ctrlPPTK.Create(m_IDDInas, 0, CONST_IDJABATAN);
                   ctrlUrusanPemerintahan1.Visible = false;
                   ctrlProgram1.Visible = false;
                   ctrlKegiatanAPBD1.Visible = false;
                   ctrlSubKegiatan1.Visible = false;
                   chkBanyakKegiatan.Visible = false;
                   label6.Visible = false;
                   label16.Visible = false;
                   label17.Visible = false;
                   label18.Visible = false;


                   break;

                case 6:
            
                    if (m_iStatus == 0)
                    {
                        txtNoSPP.Text = "    /SPP/TU-NIHIL/" + GlobalVar.TahunAnggaran.ToString();
                        txtNoSPM.Text = "    /SPM/TU-NIHIL/" + GlobalVar.TahunAnggaran.ToString();
                    }
                    break;


            }
            
            //tabSPP.TabPages.Remove(tabSPM);
            //tabSPP.TabPages.Remove(tabSP2D);


            ShowHideProgramKegiatan();

        }

        private void txtNoSPM_TextChanged(object sender, EventArgs e)
        {

        }
        private bool CekJumlah()
        {
            try
            {


                return true;
            }catch(Exception ex)
            {

                return false;
            }

        }
        private void cmdSimpanSPM_Click(object sender, EventArgs e)
        {
            
            if (m_iStatus == 3 || m_iStatus == 4 )
            {
                MessageBox.Show("SPM ini sudah Terbit SP2D.. Tidak bisa diubah. ");
                return;

            }

            if (m_iStatus == 7){
                MessageBox.Show("SPM ini sudah Terbit SP2D dan dalam tahap pencairan..");
                return;

            }
            if (GlobalVar.Pengguna.Status >= 2)
            {
                MessageBox.Show("Pengguna tidak bisa melakukan penyimpanan data..");
                return;
            }
            if (dtSPM.Value.Year  != GlobalVar.TahunAnggaran)
            {
                 MessageBox.Show("Tahun tanggal SPM salah");
                 return;
            }

            if (dtSPM.Value < dtSPP.Value)
            {
                MessageBox.Show("Tanggal SPM Tidak boleh lebih kecil dari Tanggal SPP..");
                return;
            }

            if (txtNoSPM.Text.Trim().Length == 0)
            {
                MessageBox.Show("Belum ada Nomor SPM..");
                return;
            }


            m_oSPP.NoSPM = txtNoSPM.Text + txtPrefixSPM.Text;
            m_oSPP.dtSPM = dtSPM.Value;

            if (  txtNamaPA.Text.Trim().Length==0 || txtNIPPA.Text.Trim().Length==0 || txtJabatanPA.Text.Trim().Length==0){
                MessageBox.Show("Data Penanda Tangan belum Lengkap. Sila Lengkapi data di data Master/Pejabat.. ");
                return;
            }

            m_oSPP.NamaPenandaTanganSPM = txtNamaPA.Text;
            m_oSPP.NIPPenandaTanganSPM = txtNIPPA.Text;
            m_oSPP.JabatanPenandaTanganSPM = txtJabatanPA.Text;
           
            SPPLogic oSPPLogic= new SPPLogic(GlobalVar.TahunAnggaran);

            if (oSPPLogic.SimpanSPM(m_oSPP)){
                m_oSPP.Status = 1;
                txtNoSPM.Text = m_oSPP.NoSPM;
                txtNoSPM.Width = dtSPM.Width;
                m_oSPP.Status = 1;
                txtPrefixSPM.Text = "";
                txtPrefixSPM.Visible = false;
                MessageBox.Show("Berhasil menyimpan SPM.");
                cmdCetak.Visible = true;
                int idx = 0;
                foreach (SPP spp in GlobalVar.gListSPP)
                {
                    if (spp.NoUrut == m_NoUrut)
                    {
                        idx = GlobalVar.gListSPP.IndexOf(spp);
                    }
                }
                if (idx > 0)
                {

                    GlobalVar.gListSPP[idx] = m_oSPP;

                }
               
            } else {
                MessageBox.Show("Gagal menyimpan SPM."+ oSPPLogic.LastError());
            }
            return ;


        }
        private void CreateIdBilling()
        {

            List<PotonganSPP> lst =  ctrlPotongan1.getDisplayRekening();

            for (int idx = 0; idx < lst.Count(); idx++)
            {
                PotonganSPP p = lst[idx];
                CreateIdBillingInRow(p);
                ctrlPotongan1.SetNoUrut(1, m_NoUrut);
                txtJumlahMpn.Text = ctrlPotongan1.JumlahPotongan.ToRupiahInReport();
            }
        }
        private bool CreateIdBillingInRow(PotonganSPP p)
        {
            CreateIdBillingRequest requestInquiry = new CreateIdBillingRequest();


            requestInquiry.nomorPokokWajibPajak = txtNoNPWP.Text.Trim().Replace("-", "").Replace(".", "");
            requestInquiry.kodeSetor = p.KodeSetor.Trim();
            requestInquiry.kodeMap = p.KodeMap.Trim();

            requestInquiry.masaPajak = dtSPM.Value.Month.ToString("00") + dtSPM.Value.Month.ToString("00");

            requestInquiry.tahunPajak= GlobalVar.TahunAnggaran.ToString();
            requestInquiry.jumlahBayar = DataFormat.GetDecimal(p.Jumlah.ToString("###")).ToString(); 

            requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text.Trim().Replace("-","").Replace(".","");
            
            requestInquiry.nomorSPM = txtNoSPM.Text;

            if (requestInquiry.kodeMap == "411121" && requestInquiry.kodeSetor == "100")
            {
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorPokokWajibPajakRekanan = "";
                requestInquiry.nikRekanan = "";
            }
            if (requestInquiry.kodeMap == "411124" && requestInquiry.kodeSetor == "100")
            {
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorPokokWajibPajakRekanan = txtNoNPWP.Text.Trim().Replace("-","").Replace(".","");
                requestInquiry.nikRekanan = "3200000612701111";
            }
            if (requestInquiry.kodeMap == "411211" && requestInquiry.kodeSetor == "920")
            {
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorFakturPajak = "0200001000000000";
                requestInquiry.nomorPokokWajibPajakRekanan = txtNoNPWP.Text.Trim().Replace("-","").Replace(".","");
              //  requestInquiry.nikRekanan = p.NoFaktur.Trim();// "3200000612701111";
                
            }
            if (requestInquiry.kodeMap == "411128" && requestInquiry.kodeSetor == "409")
            {
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorPokokWajibPajakRekanan =txtNoNPWP.Text.Trim().Replace("-","").Replace(".","");

                requestInquiry.nikRekanan = "3200000612701111";
            }
           // requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text.Trim().Replace("-", "").Replace(".", "");
            // requestInquiry.nomorPokokWajibPajakPenyetor = "020652384429000";
            // case 6.3 
            if (requestInquiry.kodeMap == "411122" && requestInquiry.kodeSetor == "920")
            {
                requestInquiry.nomorPokokWajibPajakPenyetor = txtNoSPM.Text; //"020652384429000";
                requestInquiry.nomorPokokWajibPajakRekanan = txtNoSPM.Text; //; //"020652384429000";


                requestInquiry.nomorPokokWajibPajak = txtNoNPWP.Text.Trim().Replace("-", "").Replace(".", "");

                requestInquiry.nomorPokokWajibPajakPenyetor= "020652384429000";
                requestInquiry.nomorPokokWajibPajakRekanan = "020652384429000";

            }

            if (p.KodeMap.Trim() == "411122" && p.KodeSetor.Trim()=="100"){
                requestInquiry.nomorSKPD = "";
                requestInquiry.nomorSPM = "";
            } else {
                requestInquiry.nomorSKPD = m_IDDInas.ToString();
            }
            if (p.KodeMap.Trim() == "411211" && p.KodeSetor.Trim()=="100")// case 4.3
            {
//                "nomorPokokWajibPajak": "147542823701000",
//"kodeMap": "411122",
//"kodeSetor": "920",
//"nomorPokokWajibPajakPenyetor": "020652384429000",
//"nomorPokokWajibPajakRekanan": "020652384429000",

                requestInquiry.nomorSKPD = "";
                requestInquiry.nomorSPM = "";
          //      requestInquiry.nomorPokokWajibPajakRekanan =  txtNoNPWP.Text.Trim();// dikosongkan 
                requestInquiry.nomorPokokWajibPajakPenyetor = "001496827018000";// txtNoNPWP.Text;
            }

            if (p.KodeMap.Trim() == "411128" && p.KodeSetor.Trim() == "423")// case 6.5
            {
                requestInquiry.nomorSKPD = m_IDDInas.ToString();
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorPokokWajibPajakRekanan = txtNoNPWP.Text.Trim();
                //requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text;==> harus berbeda antara npwp penyetor dan npwp 
                /// yang benar adalah nPWP harus berbeda
                /// 
                if (p.NPWPPenyetor.Replace(" ", "") == "")
                {
                    requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text.Trim();
                }
                else
                {
                    requestInquiry.nomorPokokWajibPajakPenyetor = p.NPWPPenyetor.Replace(" ", "");
                }
                //
 
            }
            if (p.KodeMap.Trim() == "411121" && p.KodeSetor.Trim() == "100")
            { // case 6.6

                requestInquiry.nomorSKPD = m_IDDInas.ToString();
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorPokokWajibPajakRekanan = "";// txtNoNPWP.Text.Trim();// dikosongkan 
                requestInquiry.nomorPokokWajibPajakPenyetor = txtNoNPWP.Text;//==> harus berbeda antara npwp penyetor dan npwp 
                // harus sama NPWP penyetor .. ini salah  maka di comment 
               // requestInquiry.nomorPokokWajibPajakPenyetor = "020652384429000";
                
                //
                //untuk casenya, NOP disi. Sehingga error. Harusnya dikosongkan ... 
                 //requestInquiry.nomorObjekPajak = "112233344455566667";
            }
            if (p.KodeMap.Trim() == "411128" && p.KodeSetor.Trim() == "402")
            { // case 6.7 ... NOP harus diisi

                requestInquiry.nomorSKPD = m_IDDInas.ToString();
                requestInquiry.nomorSPM = txtNoSPM.Text;
                requestInquiry.nomorPokokWajibPajakRekanan = "020652384429000";
                requestInquiry.nikRekanan = "3200000612701111";

                requestInquiry.nomorPokokWajibPajakPenyetor = "001496827018000";
                //
                //untuk casenya, NOP tidak disi. Sehingga error. Harusnya diisi ... 
              //  requestInquiry.nomorObjekPajak = "112233344455566667";
            }
            // case 6.8 masa pajak range 
            if (requestInquiry.kodeMap == "411121" && requestInquiry.kodeSetor == "100")
            {  //ini salah maka di comment  
               // requestInquiry.masaPajak = dtSPM.Value.Month.ToString("##") + (dtSPM.Value.Month+1).ToString("##");
            }


             
            string url = "";//
            DataCreateIdBillingResponseEx resp = new DataCreateIdBillingResponseEx();


            url = GlobalVar.BANK_URL + "InquiryIdBilling";
            //url = "http://36.92.240.142:8080/InquiryRekening";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
         request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
        // request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataCreateIdBillingResponseEx>(responseData);

                if (resp.error_kode != "00")
                {
                    MessageBox.Show(resp.message);
                    return false;
                }
                else
                {
                    PotonganSPPLogic oLogic = new PotonganSPPLogic(GlobalVar.TahunAnggaran);
                    p.IDBilling = resp.idBilling;
                    if (oLogic.UpdateIDBilling(p) == false)
                    {
                        MessageBox.Show(oLogic.LastError());

                    }

                    frmDataCreateIdilling fHasilCreateIDBilling = new frmDataCreateIdilling();
                    fHasilCreateIDBilling.SetData(resp);
                    fHasilCreateIDBilling.ShowDialog();

                    p.IDBilling = resp.idBilling;
                    // isi ke gctrlPotongan 



                    return true;
                 //   MessageBox.Show(resp.message);

                }
                // return resp;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
        }
        private void tabSP2D_Click(object sender, EventArgs e)
        {

        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            OnNew();

        }
        public bool OnNew(){

            try
            {
                m_oSPP = new SPP();
                m_bNew = true;
                m_NoUrut = 0;
                ctrlDinas1.Create();
          
                //ctrlUrusanPemerintahan1.Create(m_IDDInas, 
                //    GlobalVar.TahunAnggaran);

                txtNoSPP.Text = "0";
                txtPrefixSPP.Text = "";
                dtSPP.Value = DateTime.Now.Date;
                ctrlRekeningKegiatan1.Clear();
                txtUraian.Text = "";
                m_iStatus = 0;
                ctrlUrusanPemerintahan1.Clear();
                ctrlProgram1.Clear();
                ctrlKegiatanAPBD1.Clear();//Create(1,1,,11)// ();// Clear();
                ctrlSubKegiatan1.Clear();
                ctrlJenisSPP1.Clear();
                ctrlSPD1.Clear();
                txtJumlah.Text = "0";
                txtJumlah.Enabled = false;
                txtUraian.Text = "";
                ctrlPerusahaan1.Clear();
                cmdTambah.Enabled = false;
                cmdSImpan.Enabled = true;
                ctrlDaftarBank1.Create();
                txtNoSPP.Width = ctrlJenisSPP1.Width / 2;
                txtPrefixSPP.Width = ctrlJenisSPP1.Width / 2;
                txtPrefixSPP.Left = txtNoSPP.Width + txtNoSPP.Left;
                txtPrefixSPP.Visible = true;
                ctrlPotongan2.CrreateNonMpn();
                CreateSPD();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void dtSPP_ValueChanged(object sender, EventArgs e)
        {
            if (m_bNew == true)
            {
                if (m_IDDInas == 0)
                {
                    m_IDDInas = ctrlDinas1.GetID();
                    m_iKodeUk = ctrlDinas1.GetKodeUK();
                }
                ctrlKontrak1.Create(m_IDDInas, dtSPP.Value);

            }
        }
        private void CreateSPD(long nourut=0)
        {
            int iJenisSPD = 0;// GetJenisSPDBasedJenisSPP();
            ctrlSPD1.Create(m_IDDInas, dtSPP.Value, iJenisSPD, nourut);
            ctrlSPD1.Enabled = true;

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void LoadSPDThread()
        {
            //CreateSPD();
            try
            {
                SPDLogic oSPDLogic = new SPDLogic(GlobalVar.TahunAnggaran);
                if (GlobalVar.gListSPD == null)
                {
                    GlobalVar.gListSPD = new List<SPD>();
                }

                if (GlobalVar.gListSPD.FindAll(x => x.IDDInas == m_IDDInas).Count == 0)
                {
                    List<SPD> lstSPD = new List<SPD>();
                    lstSPD = oSPDLogic.Get(m_IDDInas, GlobalVar.TahunAnggaran, 0);
                    //GlobalVar.gListSPD.AddRange(lstSPD);
                    GlobalVar.gListSPD = lstSPD;
                }
                if (IsHandleCreated)
                {
                    Invoke(new UpdateUIDelegate(BisaPilihSPD), new object[] { true });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            
        }
        private void BisaPilihSPD(bool bolehPilih)
        {
            if (bolehPilih == true)
            {
                ctrlSPD1.Create(m_IDDInas, dtSPP.Value, 0);
                ctrlSPD1.Enabled = true;
            }
            else
            {
                ctrlSPD1.Enabled = false;
            }
        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            
            m_IDDInas=pIDSKPD;
            ctrlRekeningKegiatan1.Dinas = pIDSKPD;
            m_iKodeUk = pIDUK;
            m_iPPKD =  ctrlDinas1.PPKD();
            m_iUnitAnggaran = 0;
            if (ctrlDinas1.Unit != null)
            {
                m_iUnitAnggaran = ctrlDinas1.Unit.UntAnggaran;

            }
            
                BisaPilihSPD(false);
                Thread t = new Thread(new ThreadStart(LoadSPDThread));
            
                t.IsBackground = true;
                t.Start();

            


            if (ctrlDinas1.WithUnitKerja() == true)
            {
                lblUnit.Visible = true;
            }
            else
            {
                lblUnit.Visible = false;

            }
            // If m_iPPKD = 0 And (m_iJenisSPP = 3 Or m_iJenisSPP = 1 Or m_iJenisSPP = 2 Or m_iJenisSPP = 0) Then
            if (m_iStatus== 0)
            {
                CreateSPD();
                ctrlUrusanPemerintahan1.Create(m_IDDInas, GlobalVar.TahunAnggaran);
            }
            DateTime d = DateTime.Now.Date;
            m_oBendahara = ctrlDinas1.GetBendaharaPengeluaran(d);
            m_oPimpinan = ctrlDinas1.GetPimpinan(d);

        }
        private int GetJenisSPDBasedJenisSPP()
        {
            m_iJenisSPP = ctrlJenisSPP1.GetID();

            if (m_iJenisSPP == 4)
            {
                return 3;
            }
            else
            {
               // if (m_iPPKD == 1)
                if (m_iPPKD == 0 && (m_iJenisSPP == 3 || m_iJenisSPP == 1 || m_iJenisSPP == 2 ||m_iJenisSPP == 0) )
                {
                    return 4;
                }
                else
                    return 5;
            }
        }

        private void ctrlSPD1_OnChanged(long pID)
        {
            m_iNoUrutSPD = pID ;
              if ( m_iStatus == 0){         //'Log "ctrlSPD_OnChange"
                  ctrlRekeningKegiatan1.Clear();
                  //ctrlRekeningKegiatan1.CreateNewSPP(m_IDDInas, 0, 0, 0,m_iNoUrutSPD, dtSPP.Value,0, m_iKodeUk, false);

                  if (ctrlJenisSPP1.GetID()==5)
                  {
                      
                      ctrlRekeningKegiatan1.Clear();
                      ctrlRekeningKegiatan1.CreateNewSPP(
                          m_IDDInas,
                          0,
                          0,
                          0,
                          m_iNoUrutSPD,
                          dtSPP.Value,
                          m_iJenisSPP,
                          m_iUnitAnggaran
                          );
                  }



             }
        }

        private void rbBendahara_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBendahara.Checked== true)
            {
                DisplayBendahara();
            }
            else
            {
                DisplayPerusahaan(true, 0);
            }
        }
        private bool GetBendahara()
        {
            try
            {
          
                Pejabat oBendahara = new Pejabat();
                m_oBendahara = ctrlDinas1.GetBendaharaPengeluaran(dtSPP.Value);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan pengambilan data Bendahara.");
                return false;
            }
        }
        private void DisplayBendahara()
        {
            rbBendahara.Checked = true;
            rbPihakIII.Checked = false;
            DisplayPerusahaan(false, 0);

            Pejabat oBendahara = new Pejabat();
            if (m_oBendahara == null)
            {
                GetBendahara();
            }
            else { 
                if (m_oBendahara.Nama == "" || m_iStatus == 0)
               {
                GetBendahara();
                }
            }
            if (m_oBendahara != null && m_iStatus == 0  )
            {
                //if (txtNamaPenerimadalamRekeningBank.Text.Trim().Length == 0 ||
                //    ctrlDaftarBank1.Kode.Length == 0 ||
                //    txtNoRekening.Text.Trim().Length == 0)
                //{
                    txtNamaPenerima.Text = m_oBendahara.Nama;
                    ctrlDaftarBank1.SetKode(m_oBendahara.NamaBank, m_oBendahara.NoRekening);
                    txtNoRekening.Text = m_oBendahara.NoRekening;
                    txtNoNPWP.Text = m_oBendahara.NPWP;
                    txtJabatanPenerima.Text = m_oBendahara.Jabatan;
                    txtNamaPenerimadalamRekeningBank.Text = m_oBendahara.NamaDalamRekeningBank;
               // }
            }
        }
        private void rbPihakIII_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPihakIII.Checked==false )
            {
               
                
                DisplayPerusahaan(true, 0);

            }
        }

        private void ctrlSPD1_OnFocus()
        {
         //   CreateSPD();
        }

        private void ctrlProgramKegiatan1_OnChanged(int pIDurusan, int pIDProgram, int  pIDKegiaan, int pIDSubKegiatan)
        {
            int jenisSPD = GetJenisSPDBasedJenisSPP();
            m_IDProgram = pIDProgram;
            m_IDKegiatan = pIDKegiaan;
            m_IDUrusan = pIDurusan;

            if (m_iJenisSPP ==2 )
                ctrlRekeningKegiatan1.CreateNewSPP(m_IDDInas, m_IDProgram, pIDKegiaan, 0, m_iNoUrutSPD, dtSPP.Value,jenisSPD,m_iKodeUk,true);
            else 
                ctrlRekeningKegiatan1.CreateNewSPP(m_IDDInas, m_IDProgram, pIDKegiaan, 0,m_iNoUrutSPD, dtSPP.Value, jenisSPD,  m_iKodeUk,false);
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            long idrekening =ctrlPilihanRekeningAnggaran1.GetID();
            ctrlRekeningKegiatan1.CreateNewSPP(
                        m_IDDInas,
                        m_IDProgram,
                        m_IDKegiatan,
                        m_IDSubKegiatan,
                        m_iNoUrutSPD,
                        dtSPP.Value,
                        m_iJenisSPP,
                        m_iUnitAnggaran, true, idrekening
                        );

        }

        private void ctrlJenisSPP1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlKontrak1_OnChanged(long pID)
        {
            try
            {
                if (ctrlSPD1.GetID() == 0)
                {
                    MessageBox.Show("Sila Pilih SPD terlebih dahulu");
                    return;

                }
                ctrlBAST1.CreateByNoKontrak(pID, m_IDDInas);

            }
            catch (Exception ex)
            {
                return;
            }
       
        }

        private void txtUraian_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrlProgramKegiatan1_OnChanged(int pIDurusan, int pIDProgram, int pIDKegiaan, long pIDSubKegiatan)
        {
            int jenisSPD = GetJenisSPDBasedJenisSPP();
            m_IDProgram = pIDProgram;
            m_IDKegiatan = pIDKegiaan;
            m_IDUrusan = pIDurusan;
            m_IDSubKegiatan = pIDSubKegiatan;
            m_iNoUrutSPD = m_oSPP.NoUrutSPD;
            if (GlobalVar.TahunAnggaran <= 2020)
                m_IDSubKegiatan = 0;
     //       if (m_iJenisSPP == 2)
     ////           ctrlRekeningKegiatan1.CreateNewSPP(m_IDDInas, m_IDProgram, pIDKegiaan, m_IDSUbKegiatan, m_iNoUrutSPD, dtSPP.Value, jenisSPD, true);
     //       else
     //  //         ctrlRekeningKegiatan1.CreateNewSPP(m_IDDInas, m_IDProgram, pIDKegiaan, m_IDSUbKegiatan, m_iNoUrutSPD, dtSPP.Value, jenisSPD, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            menuSPM.Show(ptLowerLeft);
            
          
        }

        private PdfLayoutResult AddQuestion(string text, PdfPage page, float bottom)
        {
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12f);
            PdfBrush brush = PdfBrushes.Blue;
            float prevBottom = bottom + font.Height;
            //Rendering text
            PdfTextElement textElement = new PdfTextElement(text, font, brush);
            // Formatting layout
            PdfMetafileLayoutFormat format = new PdfMetafileLayoutFormat();
            format.Layout = PdfLayoutType.Paginate;
            format.Break = PdfLayoutBreakType.FitPage;
            //Drawing string
            PdfLayoutResult result = textElement.Draw(page, new RectangleF(0, prevBottom, page.GetClientSize().Width, 0), format);
            return result;
        }
        private PdfLayoutResult TambahText(string text, PdfPage page, float width, float bottom)
        {
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            PdfBrush brush = PdfBrushes.Black;
            float prevBottom = bottom + font.Height;
            //Rendering text
            PdfTextElement textElement = new PdfTextElement(text, font, brush);
            // Formatting layout
            PdfMetafileLayoutFormat format = new PdfMetafileLayoutFormat();
            format.Layout = PdfLayoutType.Paginate;
            format.Break = PdfLayoutBreakType.FitPage;
            //Drawing string
            PdfLayoutResult result = textElement.Draw(page, new RectangleF(0, prevBottom, width, 0), format);
            return result;
        }

        private PdfLayoutResult AddText(string text, PdfPage page, float bottom)
        {
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12f);
            PdfBrush brush = PdfBrushes.Black;
            float prevBottom = bottom + font.Height;
            //Rendering text
            PdfTextElement textElement = new PdfTextElement(text, font, brush);
            // Formatting layout
            PdfMetafileLayoutFormat format = new PdfMetafileLayoutFormat();
            format.Layout = PdfLayoutType.Paginate;
            format.Break = PdfLayoutBreakType.FitPage;
            //Drawing string
            PdfLayoutResult result = textElement.Draw(page, new RectangleF(0, prevBottom, page.GetClientSize().Width, 0), format);
            return result;
        }
        

        private void txtNoSPP_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdCetakSP2D_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                CetakSP2D();
            }

            catch (Exception Ex)
            {
                // error exit
                string[] ExceptionStack = ExceptionReport.GetMessageAndStack(Ex);
                MessageBox.Show(this, "PDF Document creation falied\n" + ExceptionStack[0] + "\n" + ExceptionStack[1],
                    "PDFDocument Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private bool CekInput()
        {
            try
            {
                if (GlobalVar.Pengguna.Status >= 2)
                {
                    MessageBox.Show("Pengguna tidak bisa melakukan penyimpanan data..");
                    return false;
                }
                if (m_IDDInas == 0)
                {
                    MessageBox.Show("Dinas belum dipilih");
                    return false;
                }
                m_iKodeUk = ctrlDinas1.KodeUK();
                MessageBox.Show(m_iKodeUk.ToString());
                if (ctrlDinas1.HasUK == true && m_iKodeUk == 0)
                {
                    MessageBox.Show("Belum Pilih Unit Kerja..");
                    return false;
                }
                if (ctrlJenisSPP1.GetID() < 0)
                {
                    MessageBox.Show("Belum pilih Jenis SPP");
                    return false;
                }
                if (ctrlJenisSPP1.GetID() == 3 && ctrlJenisBelanjaSPP1.GetID() == 0)
                {
                    MessageBox.Show("Belum pilih Jenis Belanja SPP untuk SPP LS (Barang Jasa)...");
                    return false;
                }
                if (ctrlJenisSPP1.GetID() >=3 && ctrlSumberDana2.GetID() == 0)
                {
                    MessageBox.Show("Belum pilih Sumber Dana SPP untuk SPP LS (Barang Jasa) atau Gaji...");
                    return false;
                }

                if (ctrlSPD1.GetID() == 0)
                {
                    MessageBox.Show("Belum pilih SPD");
                    return false;
                }
                if (ctrlJenisSPP1.GetID() > 1 && ctrlJenisSPP1.GetID() < 5)
                {
                    if (ctrlUrusanPemerintahan1.GetID() == 0 || ctrlProgram1.GetID() == 0 || ctrlKegiatanAPBD1.GetID() == 0 || ctrlSubKegiatan1.GetID() == 0)
                    {
                        MessageBox.Show("Urusan Pemerintahan / Program /Kegiatan/Sub Kegiatan belum dipilih");
                        return false;
                    }
                }
                if (txtNamaPenerima.Text == "" || txtNoRekening.Text == "" || txtNoNPWP.Text == "" || ctrlDaftarBank1.Kode == "" || txtNamaPenerimadalamRekeningBank.Text.Trim()=="" )
                {
                    MessageBox.Show("Data penerima belum lengkap ");
                    return false;
                }
                

                if (ctrlPotongan1.JumlahPotongan > 0)
                {
                   
                    string NoNPWP = txtNoNPWP.Text.Replace(".", "").Replace("-", "").Replace(" ", "");

                    if (NoNPWP.Length<15)
                    {
                        MessageBox.Show("No NPWP belu lengkap angkanya ");
                        return false;
                    }

                }
               /*
                * DataInquiriyRekeningResponEx resp = CekRekeningBank();
                if (resp.error_kode != "00")
                {
                    MessageBox.Show(resp.message);
                    //return false;
                }
                else
                {
                    if (txtNamaPenerimadalamRekeningBank.Text.Trim() != resp.namaPemilikRekening.Trim())
                    {
                        MessageBox.Show("Nama Pemilik No Rekening Bank tidak sesuai denagn nomor Rekening. ");
                        return false;
                    }
                    
                }

                */

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Cek Input " + ex.Message);
                return false;
            }
        }

        private void cmdSImpan_Click(object sender, EventArgs e)
        {
            string tahap;
            tahap = "";
            try
            {
                tahap = "Cek Input";
                if (CekInput() == false)
                    return;

                tahap = "Cek Data SPD";
                SPD oSPD = ctrlSPD1.GetSPD();
                
                if (oSPD == null)
                {
                    MessageBox.Show("Kesalahan penyimpanan. Data SPD tidak bisa terbaca. Sila Cek");
                    return;

                }
                SPP oSPP = new SPP();
       
                m_oSPP = oSPP;
                //   ctrlDinas1.SetID(m_oSPP.IDDInas);

                m_oSPP.NoUrut =  m_NoUrut;
                oSPP.NoUrutSPD = oSPD.NoUrut;
                m_oSPP.PPKD = m_iPPKD;
                tahap = "Cek Data Jenis SPP";
                m_oSPP.Jenis = ctrlJenisSPP1.GetID();
                m_oSPP.NoSPJUP = "0";
                if (m_oSPP.Jenis == 1)
                {
                    m_oSPP.NoSPJUP = ctrlSPJUP1.GetID().ToString();

                }
                m_oSPP.BanyakKegiatan = chkBanyakKegiatan.Checked ? 1 : 0;
                m_oSPP.WaktuPelaksanaan = "";
                m_oSPP.Bulan = "";
                m_oSPP.Bulan2 = 1;

                m_oSPP.IDDInas = m_IDDInas;
        
                oSPP.dtSPP = dtSPP.Value;
       
                oSPP.NoSPP = txtNoSPP.Text + txtPrefixSPP.Text;

                oSPP.Status = m_iStatus;

             

                oSPP.NamaPenerima = txtNamaPenerima.Text.Replace("'","''");
                oSPP.Alamat = "";
                oSPP.NamaBank = ctrlDaftarBank1.NamaBank;

                oSPP.NoRek = txtNoRekening.Text;//.Rekening;
                oSPP.NoNPWP = txtNoNPWP.Text;
                tahap = "Cek Data Nama Bank";
                oSPP.NamaBank = ctrlDaftarBank1.NamaBank; ;
                oSPP.KeteranganNamaBank = ctrlDaftarBank1.KeteranganNamaBank ;

                oSPP.NamaPenerima = txtNamaPenerima.Text;
                oSPP.NamaDalamRekeningBank = txtNamaPenerimadalamRekeningBank.Text;
                tahap = "Cek Data Kode Bank";
                oSPP.KodeBank = ctrlDaftarBank1.Kode;
                
                oSPP.JabatanPenerima = txtJabatanPenerima.Text;
                tahap = "Cek Data Jenis Belanja";
                oSPP.JenisKegiatan = ctrlJenisBelanjaSPP1.GetID();
                m_iJenisSubSPP = ctrlJenisBelanjaSPP1.GetID();

                oSPP.Kodekategori = m_IDDInas.ToString().ToKodeKategori ();
                oSPP.KodeUrusan = m_IDDInas.ToString().ToKodeUrusan();
                oSPP.KodeSKPD = m_IDDInas.ToString().ToKodeSKPD();
                oSPP.KodeUK = ctrlDinas1.GetKodeUK();// Bottom m_iKodeUk;
                oSPP.Tahun = GlobalVar.TahunAnggaran;

                if (rbBendahara.Checked == true)
                {
                    oSPP.Penerima = 0;
                    oSPP.NamaPerusahaan = ctrlDinas1.GetNamaSKPD();
                }
                else
                {
                    Perusahaan oPerusahaan = new Perusahaan();
                    oPerusahaan = ctrlPerusahaan1.GetPerusahaan();
                    tahap = "Cek Data Pihak ketiga";
                    if (oPerusahaan != null)
                    {
                        oSPP.Penerima = oPerusahaan.IDPerusahaan;
                        oSPP.NamaPerusahaan = oPerusahaan.NamaPerusahaan;
                    }
                    else
                    {
                        MessageBox.Show("Kesalahan baca data pihak ketiga");
                        return;

                    }
                }





                oSPP.Keterangan = txtUraian.Text;

                tahap = "Cek Data Sumber Dana";
                    
                oSPP.SUmberDana = ctrlSumberDana2.GetID();//
                
                oSPP.SubSumberDana = 0;
                oSPP.KeteranganSumberDana = "";
                oSPP.INoUrutKontrak = ctrlKontrak1.GetID();
                if (oSPP.INoUrutKontrak > 0)
                {
                    if (ctrlKontrak1.GetKontrak() != null)
                    {
                        oSPP.NoKontrak = ctrlKontrak1.GetKontrak().NoKontrak;
                        oSPP.dtKontrak = ctrlKontrak1.GetKontrak().DtKontrak;
                    }
                    else
                    {
                        tahap = "Cek Data Kontrak";
                        oSPP.NoKontrak = "0";
                        oSPP.dtKontrak = DateTime.Now.Date;
                       
                    }
                }
                else {
                    oSPP.NoKontrak = "";
                    oSPP.dtKontrak = new DateTime (2000,1,1);
                }
                
                BAST oBAST = new BAST();

                oBAST = ctrlBAST1.GetBAST(DataFormat.GetLong(oSPP.NoBAST));
                if (oBAST != null)
                {
                    oSPP.NoBAST = oBAST.NoUrut.ToString();
                }
                else
                {
                    oSPP.NoBAST = "0";

                }
         


                if (m_iJenisSPP >= 2 )
                {
                    Pejabat pptk = new Pejabat();
                    pptk = ctrlPPTK.GetPejabat();
                    if (pptk != null)
                    {
                        oSPP.IDPPTK = pptk.ID.ToString();
                        oSPP.NamaPPTK = pptk.Nama.Replace("'", "''''");
                        oSPP.NIPPPTK = pptk.NIP;
                        oSPP.JabatanPPTK = pptk.Jabatan;
                    }
                    else
                    {
                        oSPP.IDPPTK = "";
                        oSPP.NamaPPTK = "";
                        oSPP.NIPPPTK = "";
                        oSPP.JabatanPPTK = "";
                    }
                }
                else
                {
                    oSPP.IDPPTK = "";
                    oSPP.NamaPPTK = "";
                    oSPP.NIPPPTK = "";
                    oSPP.JabatanPPTK = "";
                }
                oSPP.UnitAnggaran = ctrlDinas1.UnitAnggaran;

                List<SPPRekening> lstdetail = new List<SPPRekening>();
                List<PotonganSPP> lstPotongan = new List<PotonganSPP>();


                if (m_iJenisSPP == 0)
                {
                    oSPP.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);                  
                   
                    oSPP.Rekenings = lstdetail;
                    oSPP.Potongans = lstPotongan;
                }
                else {
                    tahap = "Cek Data Baca data detail";

                        lstdetail = ctrlRekeningKegiatan1.getDisplayRekening();
                        tahap = "Cek Data Baca data Potongan";

                    lstPotongan = ctrlPotongan1.getDisplayRekening();
                        lstPotongan.AddRange(ctrlPotongan2.getDisplayRekening());
                        oSPP.Jumlah = 0;
                        oSPP.Rekenings = lstdetail;

                        foreach (SPPRekening sr in lstdetail)
                        {
                            oSPP.Jumlah = oSPP.Jumlah + sr.Jumlah;
                        }
                        oSPP.Potongans = lstPotongan;
                     }

                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                oSPP.idcrt = GlobalVar.Pengguna.ID;
                oSPP.idupdate = GlobalVar.Pengguna.ID;

                long retnoUrut = m_NoUrut;
                tahap = "Siap Simpan";

                retnoUrut = oLogic.Simpan(ref oSPP);

                if (oLogic.IsError() == true)
                
                {
                    MessageBox.Show(tahap + " " + oLogic.LastError());
                    return;
                }
                else
                {
                    //MessageBox.Show("Simpan potongan");
                    m_NoUrut = retnoUrut;
                    m_bNew = false;
                    SimpanPotongan();
                }

                if (GlobalVar.gListSPP != null)
                {
                    SPP updateSPP = GlobalVar.gListSPP.FirstOrDefault(spp => spp.NoUrut == oSPP.NoUrut);

                    if (updateSPP != null)
                    {
                        for (int idx = 0; idx < GlobalVar.gListSPP.Count; idx++)
                        {
                            if (GlobalVar.gListSPP[idx].NoUrut == oSPP.NoUrut)
                            {
                                oSPP.dtSPM = GlobalVar.gListSPP[idx].dtSPM;
                                oSPP.NoSPM = GlobalVar.gListSPP[idx].NoSPM;
                                oSPP.NoSP2D = GlobalVar.gListSPP[idx].NoSP2D;
                                oSPP.dtTerbit = GlobalVar.gListSPP[idx].dtTerbit;
                                oSPP.NamaPenandaTanganSPM = GlobalVar.gListSPP[idx].NamaPenandaTanganSPM;
                                oSPP.JabatanPenandaTanganSPM = GlobalVar.gListSPP[idx].JabatanPenandaTanganSPM;
                                oSPP.NIPPenandaTanganSPM = GlobalVar.gListSPP[idx].NIPPenandaTanganSPM;
                                oSPP.BankBUD = GlobalVar.gListSPP[idx].BankBUD;
                                oSPP.PenandatanganSP2d = GlobalVar.gListSPP[idx].PenandatanganSP2d;
                                oSPP.PenandatanganBUD = GlobalVar.gListSPP[idx].PenandatanganBUD;

                                oSPP.dtCair = GlobalVar.gListSPP[idx].dtCair;



                                GlobalVar.gListSPP[idx] = oSPP;


                                List<int> lstIndexOfRekening = GetIndexSPPRekening(oSPP.NoUrut);
                                lstIndexOfRekening = lstIndexOfRekening.OrderBy(x => -x).ToList();
                                foreach (int id in lstIndexOfRekening)
                                {
                                    GlobalVar.gListSPPRekening.RemoveAt(id);
                                }
                                foreach (SPPRekening sr in oSPP.Rekenings)
                                {
                                    sr.IDDinas = m_IDDInas;
                                    if (GlobalVar.gListSPPRekening!=null)
                                    GlobalVar.gListSPPRekening.Add(sr);
                                }

                                break;
                            }
                        }


                    }
                    else
                    {
                        GlobalVar.gListSPP.Add(oSPP);
                        foreach (SPPRekening sr in oSPP.Rekenings)
                        {
                            sr.IDDinas = m_IDDInas;
                            GlobalVar.gListSPPRekening.Add(sr);
                        }



                    }
                }
                txtNoSPP.Text = txtNoSPP.Text + txtPrefixSPP.Text;
 
                txtPrefixSPP.Text = "";
                txtNoSPP.Width = ctrlJenisSPP1.Width;
                txtPrefixSPP.Visible = false;



                MessageBox.Show("Penyimpanan SPP Berhasil. Selanjutnya Proses SPM Oleh PPK...");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Penyimpanan SPP " + tahap + " " +ex.Message);
            }

           



        }
        private List<int> GetIndexSPPRekening(long NoUrut)
        {
            List<int> lst = new List<int>();
            if (GlobalVar.gListSPPRekening != null)
            {
                for (int idx = 0; idx < GlobalVar.gListSPPRekening.Count; idx++)
                {
                    if (GlobalVar.gListSPPRekening[idx].NoUrut == NoUrut)
                    {
                        lst.Add(idx);
                    }
                }
            }
            return lst ;
        }
        private void ctrlUrusanPemerintahan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlUrusanPemerintahan1_MouseCaptureChanged(object sender, EventArgs e)
        {
      
        }

        private void tapLSTU_Click(object sender, EventArgs e)
        {

        }

        private void tabPenerima_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPengeluaranCariPerusahaan fCariPerusahaan = new frmPengeluaranCariPerusahaan();
            fCariPerusahaan.ShowDialog();
            if (fCariPerusahaan.IsOK == true)
            {
                Perusahaan p = fCariPerusahaan.Perusahaan;
                SetPerusahaan(p);
            }
        }

        private void ctrlRekeningKegiatan1_OnChanged(decimal pJumlah)
        {
            string stringRupiah =pJumlah.ToRupiahInReport();
            if (DataFormat.FormatUangReportKeDecimal(stringRupiah)!= pJumlah){
                txtJumlahSPP.Text= pJumlah.ToString();
                txtJumlah.Text= pJumlah.ToString();
            } else {
                txtJumlahSPP.Text = pJumlah.ToRupiahInReport();
                txtJumlah.Text = pJumlah.ToRupiahInReport();
            }
            

        }

        private void cmdTambahPajak_Click(object sender, EventArgs e)
        {
            try
            {
                //if (m_oSPP.Status < 1)
                //{
                //    MessageBox.Show("Buat dan Simpan terlebih dulu SPM baru menambahkan potongan..");
                //    return;
                //}
                frmTambahPajak fTPajak = new frmTambahPajak();
                string sNPWPDinas = ctrlDinas1.GetBendaharaPengeluaran(dtSPM.Value).NPWP.Replace(".", "").Replace("-", "").Replace(" ", ""); ;
                if (sNPWPDinas.Length == 0)
                {
                    MessageBox.Show("NPWP dinas/penyetor belum disetting");
                    return;
                }
                fTPajak.NPWP = txtNoNPWP.Text.Replace(".", "").Replace("-", "").Replace(" ", "");
               // if (txtNoNPWP.Text.Replace(".", "").Replace("-", "").Replace(" ", "").Length == 0)
                //{
               //     MessageBox.Show("NPWP belum Penerima belum disetting");
                //    return;
               // }
                fTPajak.TanggalSPM = m_oSPP.dtSPM;// dtSPM.Value;
                fTPajak.NoSPM = m_oSPP.NoSPM;// txtNoSPM.Text;
                fTPajak.NPWPDinas = sNPWPDinas;
                fTPajak.Dinas = ctrlDinas1.GetID().ToString();

                fTPajak.ShowDialog();
                if (fTPajak.OK)
                {
                    PotonganSPP p = new PotonganSPP();
                    p.IIDRekening = fTPajak.IDPotongan;
                    p.KodeSetor = fTPajak.KodeSetor;
                    p.KodeMap = fTPajak.KodeMap.ToString();
                    p.Nama = fTPajak.NamaPajak;
                    p.Jumlah = fTPajak.Jumlah;
                    p.IDBilling = "";
                    p.NoUrut = m_NoUrut;
                    p.NoFaktur = fTPajak.NoFaktur;
                    p.NIKRekeninan = fTPajak.NIKRekanan;
                    //if (fTPajak.SetorBendahara == true)
                    //{
                    //if (GetBendahara() == true)
                    //{
                    //    p.NPWPPenyetor = m_oBendahara.NPWP;
                    //}

                    //}

                    ctrlPotongan1.AddPotongan(p);
                    txtJumlahMpn.Text = ctrlPotongan1.JumlahPotongan.ToRupiahInReport();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CetakSPM()
        {
            //Create a new PDF document.

          
            try
            {
                PdfDocument document = new PdfDocument();

                //Section - 1
                //Add new section to the document
                PdfSection section = document.Sections.Add();


                //Create page settings to the section
                //  section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle0;
                //section.PageSettings.Size = PdfPageSize.Legal;
                section.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section.PageSettings.Height = 935;// = PdfPageSize.Legal;

                section.PageSettings.Orientation = PdfPageOrientation.Landscape;
                //section.PageSettings.Width = 300;
                float yPos = 0;
                //section.PageSettings.Height = 400;
                PdfPage page = section.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                float kiri = 5;
                float posAtas = 5;
                float lebarArena = page.GetClientSize().Width - 2 * kiri;
                float tinggiArena = page.GetClientSize().Height - 2 * posAtas;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);
                PdfPen penRekening = new PdfPen(PdfBrushes.Black, 0.1f);

                FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
                PdfBitmap image = new PdfBitmap(imageStream);
                //Draw the image
                PointF p = new PointF(kiri + 5, 10);
                Size sizeImage = new Size(30, 40);
                graphics.DrawImage(image, p, sizeImage);

                Rectangle rect = new Rectangle((int)kiri,
                                                (int)posAtas,
                                                (int)lebarArena,
                                                (int)tinggiArena);



                // graphics.DrawRectangle(pen, rect);

                PdfGrid headerGrid = new PdfGrid();
                List<object> dataHeader = new List<object>();


                PdfFont font;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;
                font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold));
                string text = "PEMERINTAH " + GlobalVar.gPemda.Nama.ToUpper();

                SizeF size = font.MeasureString(text);

                yPos = TulisItem(graphics, text, font, kiri + 2, posAtas + 2, lebarArena, stringFormat, true);
                text = "SURAT PERINTAH MEMBAYAR";
                yPos = TulisItem(graphics, text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
                text = "TAHUN ANGGARAN " + GlobalVar.TahunAnggaran.ToString();
                yPos = TulisItem(graphics, text, font, kiri + 2, yPos, lebarArena, stringFormat, false);
                stringFormat.Alignment = PdfTextAlignment.Right;

                yPos = TulisItem(graphics, "No SPM: " + txtNoSPM.Text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
                stringFormat.Alignment = PdfTextAlignment.Center;


                font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Regular);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarArena, yPos + 1);

                text = "(Diisi oleh PPK SKPD)";

                yPos = TulisItem(graphics, text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
                graphics.DrawLine(pen, kiri, yPos + 1, kiri + lebarArena, yPos + 1);
                float posisiAtas = yPos;
                yPos = yPos + 3;
                float yPosKol2 = yPos;

                float AwalKol2 = page.GetClientSize().Width / 2 + 5;

                font = new PdfTrueTypeFont(new Font("Arial", 10));
                stringFormat.Alignment = PdfTextAlignment.Left;
                text = "KUASA BENDAHARA UMUM DAERAH";
                yPos = TulisItem(graphics, text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
                text = "Kabupaten Ketapang";
                yPos = TulisItem(graphics, text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
                text = "Supaya Menerbitkan SP2D Kepada: ";
                yPos = TulisItem(graphics, text, font, kiri + 2, yPos, page.GetClientSize().Width, stringFormat, true);
                font = new PdfTrueTypeFont(new Font("Arial", 9));
                stringFormat.CharacterSpacing = 0.5f;
                graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

                yPos = yPos + 6;

                SPD oSPD = ctrlSPD1.GetSPD();
                float RealYPosYKol2 = yPos;
                yPos = TulisItem(graphics, "SKPD", font, kiri + 5, yPos, 340, stringFormat);

                yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD(), font, 130, yPos, 340, stringFormat, true);
                if (m_oSPP.Penerima == 0)
                {

                    yPos = TulisItem(graphics, "Bendahara Pengeluaran ", font, kiri + 5, yPos, 340, stringFormat);
                    yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                    yPos = TulisItem(graphics, txtNamaPenerima.Text + " / " + txtJabatanPenerima.Text, font, 130, yPos, 340, stringFormat, true);
                    //txtNama.Text +"/" + txtJabatan.Text
                }
                else
                {
                    yPos = TulisItem(graphics, "Pihak Ketiga", font, kiri + 5, yPos, 340, stringFormat);
                    yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                    yPos = TulisItem(graphics, txtNamaPenerima.Text + " / " + txtJabatanPenerima.Text + " " + ctrlPerusahaan1.GetPerusahaan().NamaPerusahaan, font, 130, yPos, (lebarArena/2)-130, stringFormat, true);


                }
                //yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                //yPos = TulisItem(graphics, txtNamaPenerimadalamRekeningBank.Text, font, 130, yPos, 340, stringFormat, true);

                yPos = TulisItem(graphics, "Rekening Bank", font, kiri + 5, yPos, 340, stringFormat);
                yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                yPos = TulisItem(graphics, txtNoRekening.Text, font, 130, yPos, 340, stringFormat, true);

                yPos = TulisItem(graphics, "Nama Bank", font, kiri + 5, yPos, 340, stringFormat);
                yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                yPos = TulisItem(graphics, ctrlDaftarBank1.NamaBank + " " + ctrlDaftarBank1.KeteranganNamaBank, font, 130, yPos, 340, stringFormat, true);

                yPos = TulisItem(graphics, "NPWP", font, kiri + 5, yPos, 340, stringFormat);
                yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                yPos = TulisItem(graphics, txtNoNPWP.Text, font, 130, yPos, 340, stringFormat, true);
                yPos = TulisItem(graphics, "Nomor dan Tanggal SPD", font, kiri + 5, yPos, 340, stringFormat);
                yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                yPos = TulisItem(graphics, oSPD.NoSPD + " / " + oSPD.Tanggal.ToString("dd MMM yyyy"), font, 130, yPos, 340, stringFormat, true);


                // font.Size = 9;
                //font = new PdfStandardFont(PdfFontFamily.Helvetica, 1, PdfFontStyle.Regular);
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);

                yPos = TulisItem(graphics, "UNTUK KEPERLUAN", font, kiri + 2, yPos, 460, stringFormat, true);

                PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Justify);
                yPos = TulisItem(graphics, txtUraian.Text.ReplaceUnicode(), 9, 10, yPos, 400, format, true);
                List<SPPRekening> lst = new List<SPPRekening>();
                yPos = yPos + 1;
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);

                lst = ctrlRekeningKegiatan1.getDisplayRekening();
                //if (lst.Count <= 15)
                //{
                //    font = new PdfTrueTypeFont(new Font("Arial", 9));
                //}
                //if (lst.Count > 15 && lst.Count <= 18)
                //{
                //    font = new PdfTrueTypeFont(new Font("Arial", 8));
                //}
                //if (lst.Count > 18 && lst.Count <= 22)
                //{
                font = new PdfTrueTypeFont(new Font("Arial", 7));
                //}
                //if (lst.Count > 22)
                //{
                //    font = new PdfTrueTypeFont(new Font("Arial", 7));
                //}
                if (m_iJenisSPP == 0)
                {
                    format = new PdfStringFormat(PdfTextAlignment.Left);
                    yPos = TulisItem(graphics, "", font, 10, yPos, 180, stringFormat);
                    yPos = TulisItem(graphics, "Rp", font, 370, yPos, 20, stringFormat);

                    format = new PdfStringFormat(PdfTextAlignment.Right);
                    yPos = TulisItem(graphics, txtJumlah.Text, font, 340, yPos, 80, format);

                    yPos = TulisItem(graphics, "Uang Persediaan", font, 165, yPos, 195, stringFormat, true);
                    txtJumlahSPP.Text = txtJumlah.Text;
                    graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);
                }
                else
                {
                    if (m_iJenisSPP == 1)
                    {
                        SPPLogic osppLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                        List<SPPRekening> lstGU = osppLogic.GetRingkasanGU(m_NoUrut);
                        if (lstGU != null)
                        {
                            foreach (SPPRekening sr in lstGU)
                            {
                                if (sr.Jumlah > 0)
                                {


                                    format = new PdfStringFormat(PdfTextAlignment.Left);
                                    yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening(), font, 10, yPos, 180, stringFormat);
                                    yPos = TulisItem(graphics, "Rp", font, 340, yPos, 20, stringFormat);

                                    format = new PdfStringFormat(PdfTextAlignment.Right);
                                    yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, 345, yPos, 80, format);

                                    yPos = TulisItem(graphics, sr.NamaRekening, font, 125, yPos, 220, stringFormat, true);

                                }
                            }

                        }
                    }
                    else
                    {
                        foreach (SPPRekening sr in lst)
                        {
                            if (sr.Jumlah > 0)
                            {
                                format = new PdfStringFormat(PdfTextAlignment.Left);
                                yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening(), font, 10, yPos, 180, stringFormat);
                                yPos = TulisItem(graphics, "Rp", font, 340, yPos, 20, stringFormat);

                                format = new PdfStringFormat(PdfTextAlignment.Right);
                                yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, 345, yPos, 80, format);

                                yPos = TulisItem(graphics, sr.NamaRekening, font, 125, yPos, 220, stringFormat, true);

                            }
                        }

                    }
                }

                yPos = page.GetClientSize().Height - 5 * size.Height;
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);

                yPos = TulisItem(graphics, txtJumlahSPP.Text, font, 340, yPos, 80, format);

                yPos = TulisItem(graphics, "Jumlah SPP yang diminta", font, 10, yPos, 195, stringFormat, true);
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);
                decimal JumlahSPP = DataFormat.FormatUangReportKeDecimal(txtJumlahSPP.Text);
                //    yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, 340, yPos, 80, format);
                yPos = TulisItem(graphics, JumlahSPP.Terbilang(), font, kiri + 5, yPos, AwalKol2 - kiri - 50, stringFormat, true);
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);
                yPos = TulisItem(graphics, "Nomor dan Tanggal SPP", font, 10, yPos, 195, stringFormat);
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri, yPos);
                yPos = TulisItem(graphics, txtNoSPP.Text + dtSPP.Value.ToString("dd MMM yyyy"), font, 200, yPos, 250, stringFormat, true);
                graphics.DrawLine(pen, kiri, yPos, AwalKol2 - kiri + 5, yPos);
                yPos = TulisItem(graphics, "Sumber Dana", font, 10, yPos, 100, stringFormat);
                yPos = TulisItem(graphics, ctrlSumberDana2.NamaSumberDana, font, 60, yPos, 320, stringFormat, true);

                graphics.DrawLine(pen, AwalKol2 + 2, posisiAtas, AwalKol2 + 2, yPos);// garis Naik

                graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = TulisItem(graphics, "SPM ini sah apabila ditadatangani dan distempel oleh KepalaSKPD. ", font, kiri, yPos, lebarArena, stringFormat, true);
                rect = new Rectangle((int)kiri,
                                                (int)posAtas,
                                                (int)lebarArena,
                                                (int)yPos - (int)posAtas);

                graphics.DrawRectangle(pen, rect);
                graphics.DrawLine(pen, kiri, yPos + 3, lebarArena, yPos + 3);

                font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold));
                stringFormat.Alignment = PdfTextAlignment.Left;

                text = "Potongan Potongan";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 5, yPosKol2, 100, stringFormat, true);
                yPos = RealYPosYKol2;
                // graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);

                stringFormat.Alignment = PdfTextAlignment.Center;
                text = "No";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 5, yPos, 100, stringFormat);
                float pos1 = AwalKol2 + 5;
                text = "Uraian";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 25, yPos, 250, stringFormat);
                float pos2 = AwalKol2 + 25;
                text = "Jumlah";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 225, yPos, 100, stringFormat);
                float pos3 = AwalKol2 + 225;
                text = "Keterangan";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 325, yPos, 100, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                float pos4 = AwalKol2 + 325;
                List<PotonganSPP> lstPotongan = ctrlPotongan1.getDisplayRekening();
                int No = 0;
                stringFormat.Alignment = PdfTextAlignment.Left;
                foreach (PotonganSPP ps in lstPotongan)
                {

                    if (ps.Jumlah > 0)
                    {
                        No++;

                        format = new PdfStringFormat(PdfTextAlignment.Right);
                        font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));

                        yPos = TulisItem(graphics, No.ToString(), font, pos1, yPos, 15, format);



                        yPos = TulisItem(graphics, "Rp", font, pos3, yPos, 20, stringFormat);
                        yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, pos3, yPos, 100, format);

                        yPos = TulisItem(graphics, ps.NamaRekening, font, pos2, yPos, 250, stringFormat, true);
                        //  graphics.DrawLine(penRekening , AwalKol2 + 5, yPos, kiri + lebarArena, yPos);


                    }



                }
                yPos = yPos + 3;
                text = "Jumlah";
                yPos = TulisItem(graphics, txtJumlahMpn.Text, font, pos3, yPos, 100, format);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                yPos = TulisItem(graphics, "JUMLAH", font, pos2, yPos, 250, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                yPos = yPos + 3;

                text = "Potongan";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 5, yPos, 100, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);

                stringFormat.Alignment = PdfTextAlignment.Center;
                text = "No";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 5, yPos, 100, stringFormat);
                text = "Uraian";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 25, yPos, 250, stringFormat);
                text = "Jumlah";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 225, yPos, 100, stringFormat);
                text = "Keterangan";
                yPos = TulisItem(graphics, text, font, AwalKol2 + 325, yPos, 100, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                List<PotonganSPP> lstPotonganNonMpn = ctrlPotongan2.getDisplayRekening();
                No = 0;
                foreach (PotonganSPP ps in lstPotonganNonMpn)
                {

                    if (ps.Jumlah > 0)
                    {
                        No++;

                        format = new PdfStringFormat(PdfTextAlignment.Right);
                        font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));
                        stringFormat.Alignment = PdfTextAlignment.Left;
                        yPos = TulisItem(graphics, No.ToString(), font, pos1, yPos, 15, format);



                        yPos = TulisItem(graphics, "Rp", font, pos3, yPos, 20, stringFormat);
                        yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, pos3, yPos, 100, format);

                        yPos = TulisItem(graphics, ps.NamaRekening, font, pos2, yPos, 250, stringFormat, true);

                        //  graphics.DrawLine(penRekening, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);


                    }



                }
                yPos = yPos + 3;
                text = "Jumlah";
                yPos = TulisItem(graphics, txtJumlahNonMpn.Text, font, pos3, yPos, 100, format);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = TulisItem(graphics, "JUMLAH", font, pos2, yPos, 250, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                yPos = yPos + 3;
                decimal JumlahPajak = DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text) + DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text);

                decimal JumlahMurni = 0L;
                if (m_iJenisSPP == 4)
                {
                    JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) -
                    ((DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text)) + DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text));
                }
                else
                {
                    JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) -
                    (DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text));

                }

                yPos = TulisItem(graphics, JumlahPajak.ToRupiahInReport(), font, pos3, yPos, 100, format);

                yPos = TulisItem(graphics, "JUMLAH PAJAK ", font, pos2, yPos, 250, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                yPos = yPos + 3;

                yPos = TulisItem(graphics, JumlahMurni.ToRupiahInReport(), font, pos3, yPos, 150, format);

                yPos = TulisItem(graphics, "JUMLAH SPM", font, pos2, yPos, 250, stringFormat, true);
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                yPos = yPos + 3;
                Single hight = yPos;

                yPos = TulisItem(graphics, "Sejumlah", font, pos1, yPos, 250, stringFormat, true);
                hight = hight - yPos;
                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                font = new PdfTrueTypeFont(new Font("Arial",8 , FontStyle.Regular));
                string terbilang = JumlahMurni.Terbilang();
                yPos = TulisItem(graphics, terbilang, font, pos2, yPos,360, stringFormat, true);
                yPos = yPos +  hight;
                font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));
               // graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);

                yPos = yPos + 15;
                format = new PdfStringFormat(PdfTextAlignment.Center);

                graphics.DrawLine(pen, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                yPos = yPos + 5;
                font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold));
                yPos = TulisItem(graphics, "Ketapang, " + dtSPM.Value.ToString("dd MMM yyyy"), font, pos1, yPos, 400, format, true);
                yPos = TulisItem(graphics, txtJabatanPA.Text, font, pos1, yPos, 400, format, true);
                yPos = yPos + 30;
                font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold));
                yPos = TulisItem(graphics, txtNamaPA.Text, font, pos1, yPos, 400, format, true, true);

                yPos = TulisItem(graphics, "NIP " + txtNIPPA.Text, font, pos1, yPos, 400, format, true);







                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPM.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../SPM.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CetakSPMPortrait() {

            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;

            //document.PageSettings.Size = new SizeF(1039,1323);
            //if (m_iJenisSPP == 4)
            //{
            //27.5 × 35.5
            document.PageSettings.Size = new Size(1006, 630); // new Size(800, 780);//(1006, 780);
            //document.PageSettings.Size = new Size(970, 580); // new Size(800, 780);//(1006, 780);
            //}
            //else { 
            //     document.PageSettings.Size = new Size(992, 765);
            //}
            PdfSection section = document.Sections.Add();
            section.PageSettings.Orientation = PdfPageOrientation.Portrait;
            float yPos = 0;
            //section.PageSettings.Height = 400;
            PdfPage page = section.Pages.Add();
            PdfGraphics graphics = page.Graphics;


            
            float kiri = 5;
            float posAtas = 5;
            float lebarArena = page.GetClientSize().Width - 2* kiri;
            float tinggiArena=page.GetClientSize().Height - 2* posAtas;
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);
            PdfPen penRekening = new PdfPen(PdfBrushes.Black, 0.1f);

            FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            //Draw the image
            PointF p = new PointF(kiri + 5, 10);
            Size sizeImage = new Size(30, 40);
            graphics.DrawImage(image, p, sizeImage);

            Rectangle rect = new Rectangle((int)kiri,
                                            (int)posAtas,
                                            (int)lebarArena,
                                            (int)tinggiArena);
           
        

           // graphics.DrawRectangle(pen, rect);

            PdfGrid headerGrid = new PdfGrid();
            List<object> dataHeader = new List<object>();
            Single yPosAtas = 10;

            PdfFont font ;
            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Center;
            stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            //stringFormat.CharacterSpacing = 2f;
            font = new PdfTrueTypeFont(new Font("Arial", 10 , FontStyle.Bold));
            string text = "PEMERINTAH " + GlobalVar.gPemda.Nama.ToUpper();

            SizeF size = font.MeasureString(text);

            
            yPos = TulisItem(graphics, text, font, kiri+2, posAtas+2 , lebarArena, stringFormat, true);
            text = "SURAT PERINTAH MEMBAYAR";
            yPos = TulisItem(graphics, text, font, kiri+2, yPos, lebarArena, stringFormat, true);
            text = "TAHUN ANGGARAN "+ GlobalVar.TahunAnggaran.ToString();
            yPos = TulisItem(graphics, text, font, kiri+2, yPos, lebarArena, stringFormat, false);

            stringFormat.Alignment = PdfTextAlignment.Right;
            yPos = TulisItem(graphics, "No SPM: " + txtNoSPM.Text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
            stringFormat.Alignment = PdfTextAlignment.Center;
                       

            font = new PdfStandardFont(PdfFontFamily.Helvetica, 10,PdfFontStyle.Regular);
            graphics.DrawLine(pen, kiri, yPos , kiri + lebarArena, yPos + 1);

            text = "(Diisi oleh PPK SKPD)";
            
            yPos = TulisItem(graphics, text, font, kiri+2, yPos, lebarArena, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos + 1, kiri + lebarArena , yPos + 1);
            float posisiAtas = yPos;
            yPos = yPos + 3;
            float yPosKol2 = yPos;
            float AwalKol2 = kiri;

            font = new PdfTrueTypeFont(new Font("Arial", 10));
            stringFormat.Alignment = PdfTextAlignment.Left;
            text = "KUASA BENDAHARA UMUM DAERAH";
            yPos = TulisItem(graphics, text, font, kiri+2, yPos, lebarArena, stringFormat, true);
            text = "Kabupaten Ketapang";
            yPos = TulisItem(graphics, text, font, kiri + 2, yPos, lebarArena, stringFormat, true);
            text = "Supaya Menerbitkan SP2D Kepada: ";
            yPos = TulisItem(graphics, text, font, kiri + 2, yPos, page.GetClientSize().Width, stringFormat, true);
            font = new PdfTrueTypeFont(new Font("Arial", 9));
            stringFormat.CharacterSpacing = 0.5f;
            graphics.DrawLine(pen, kiri, yPos , lebarArena+5, yPos );

            yPos = yPos + 6;
            
            SPD oSPD = ctrlSPD1.GetSPD();
            float RealYPosYKol2 = yPos;
            yPos = TulisItem(graphics, "SKPD", font, kiri + 5, yPos, 340, stringFormat);

            yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
            yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD(), font, 130, yPos, 340, stringFormat, true);
            if (m_oSPP.Penerima == 0)
            {
               
                    yPos = TulisItem(graphics, "Bendahara Pengeluaran ", font, kiri + 5, yPos, lebarArena, stringFormat);
                    yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                    yPos = TulisItem(graphics, txtNamaPenerima.Text + " / " + txtJabatanPenerima.Text, font, 130, yPos, lebarArena, stringFormat, true);
                //txtNama.Text +"/" + txtJabatan.Text
            }
            else
            {
                yPos = TulisItem(graphics, "Pihak Ketiga", font, kiri + 5, yPos, lebarArena, stringFormat);
                yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
                yPos = TulisItem(graphics, txtNamaPenerima.Text + " / " + txtJabatanPenerima.Text +" " + ctrlPerusahaan1.GetPerusahaan().NamaPerusahaan , font, 130, yPos, lebarArena, stringFormat, true);


            }
            //yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
            //yPos = TulisItem(graphics, txtNamaPenerimadalamRekeningBank.Text, font, 130, yPos, lebarArena, stringFormat, true);

            yPos = TulisItem(graphics, "Rekening Bank", font, kiri + 5, yPos, lebarArena, stringFormat);
            yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
            yPos = TulisItem(graphics, txtNoRekening.Text, font, 130, yPos, lebarArena, stringFormat, true);

            yPos = TulisItem(graphics, "Nama Bank", font, kiri + 5, yPos, lebarArena, stringFormat);
            yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
            yPos = TulisItem(graphics, ctrlDaftarBank1.NamaBank, font, 130, yPos, lebarArena, stringFormat, true);

            yPos = TulisItem(graphics, "NPWP", font, kiri + 5, yPos, lebarArena, stringFormat);
            yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
            yPos = TulisItem(graphics, txtNoNPWP.Text, font, 130, yPos, lebarArena, stringFormat, true);
            yPos = TulisItem(graphics, "Nomor dan Tanggal SPD", font, kiri + 5, yPos, lebarArena, stringFormat);
            yPos = TulisItem(graphics, ":", font, 125, yPos, 10, stringFormat);
            yPos = TulisItem(graphics, oSPD.NoSPD + " / " + oSPD.Tanggal.ToString("dd MMM yyyy"), font, 130, yPos, lebarArena, stringFormat, true);


           // font.Size = 9;
            //font = new PdfStandardFont(PdfFontFamily.Helvetica, 1, PdfFontStyle.Regular);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

            yPos = TulisItem(graphics, "UNTUK KEPERLUAN", font, kiri + 2, yPos, lebarArena, stringFormat, true);

            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Justify);
            yPos = TulisItem(graphics, txtUraian.Text.ReplaceUnicode(), 8, 10, yPos, lebarArena-15, format, true);
            List<SPPRekening> lst = new List<SPPRekening>();
            yPos = yPos + 1;
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = TulisItem(graphics, "Pembebanan Pada Kode Rekening", 9, kiri + 2, yPos, lebarArena, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

            stringFormat.Alignment = PdfTextAlignment.Center;
            float height = 0;
            float posAwal = yPos;
            yPosAtas = yPos;
            yPos = TulisItem(graphics, "No", 9, kiri + 2, yPos, 15, stringFormat, false);
            yPos = TulisItem(graphics, "Kode Rekening", 9, kiri + 15, yPos, 180, stringFormat, false);
            yPos = TulisItem(graphics, "Uraian", 9, 125, yPos, 290, stringFormat, false);
            yPos = TulisItem(graphics, "Jumlah", 9, 450, yPos, 80, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            
            height = yPos -posAwal;
            stringFormat.Alignment = PdfTextAlignment.Left ;
            
            //format = new PdfStringFormat(PdfTextAlignment.Left);
            //yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening(), font, 10, yPos, 180, stringFormat);
            //yPos = TulisItem(graphics, "Rp", font, 340, yPos, 20, stringFormat);

            //format = new PdfStringFormat(PdfTextAlignment.Right);
            //yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, 345, yPos, 80, format);

            //yPos = TulisItem(graphics, sr.NamaRekening, font, 125, yPos, 220, stringFormat, true);

          
            lst = ctrlRekeningKegiatan1.getDisplayRekening();
            int idx = 0;
                font = new PdfTrueTypeFont(new Font("Arial", 7));
                pen.DashStyle = PdfDashStyle.Dot;
            if (m_iJenisSPP == 0) {
                format = new PdfStringFormat(PdfTextAlignment.Left);
                yPos = TulisItem(graphics, "", font, 10, yPos, 180, stringFormat);
                yPos = TulisItem(graphics, "Rp", font, 370, yPos, 20, stringFormat);

                format = new PdfStringFormat(PdfTextAlignment.Right);
                yPos = TulisItem(graphics, txtJumlah.Text, font, 340, yPos, 80, format);

                yPos = TulisItem(graphics, "Uang Persediaan", font, 165, yPos, 195, stringFormat, true);
                txtJumlahSPP.Text = txtJumlah.Text;
                graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            }
            else
            {
                if (m_iJenisSPP == 1)
                {
                    SPPLogic osppLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                    List<SPPRekening> lstGU = osppLogic.GetRingkasanGU(m_NoUrut);
                    if (lstGU != null)
                    {
                        foreach (SPPRekening sr in lstGU)
                        {
                            if (sr.Jumlah > 0)
                            {


                                format = new PdfStringFormat(PdfTextAlignment.Left);
                                yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening(), font, 10, yPos, 180, stringFormat);
                                yPos = TulisItem(graphics, "Rp", font, 435, yPos, 20, stringFormat);

                                format = new PdfStringFormat(PdfTextAlignment.Right);
                                yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, 440, yPos, 85, format);

                                yPos = TulisItem(graphics, sr.NamaRekening, font, 170, yPos, 230, stringFormat, true);

                            }
                        }

                    }
                }
                else {
                    foreach (SPPRekening sr in lst)
                    {
                        if (sr.Jumlah > 0)
                        {
                            format = new PdfStringFormat(PdfTextAlignment.Right);
                            yPos = TulisItem(graphics, (++idx).ToString(), font, kiri, yPos, 20, format);
                            format = new PdfStringFormat(PdfTextAlignment.Left);

                            yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + 
                                       sr.IDRekening.ToKodeRekening(), font, 35, yPos, 180, stringFormat);
                            yPos = TulisItem(graphics, "Rp", font, 450, yPos, 20, stringFormat);

                            format = new PdfStringFormat(PdfTextAlignment.Right);
                            yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, 460, yPos, 80, format);

                            yPos = TulisItem(graphics, sr.NamaRekening, font, 170, yPos, 250, stringFormat, true);
                            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
                            if (yPos >= page.GetClientSize().Height - 40)
                            {
                                graphics.DrawLine(pen, kiri - 1, yPosAtas, kiri - 1, yPos);
                                graphics.DrawLine(pen, 34 - 1, yPosAtas, 34- 1, yPos);
                                graphics.DrawLine(pen, 449 - 1, yPosAtas, 449 - 1, yPos);
                                graphics.DrawLine(pen, 180 - kiri - 10, yPosAtas, 180 - kiri - 10, yPos);
                                graphics.DrawLine(pen, 445, yPosAtas, 445, yPos);
                                rect = new Rectangle((int)kiri,
                                      (int)5,
                                      (int)lebarArena,
                                      (int)yPos - (int)5);

                                graphics.DrawRectangle(pen, rect);

                                page = document.Pages.Add();
                                graphics = page.Graphics;
                                yPos = 10;
                                graphics.DrawLine(pen, kiri, yPos, lebarArena +kiri , yPos);
                                //yPosAtasSPD = 10;
                                //yPosAtasSPD = 10;
                                yPosAtas = 10;
                                yPosAtas = 10;


                            }
                        }

                    }

                }
            }

            for (int baristambahan = 0; baristambahan < 5 - idx; baristambahan++)
            {
                yPos = yPos + height;
                graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

            }
            pen.DashStyle = PdfDashStyle.Solid;
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = TulisItem(graphics, "Rp", font, 450, yPos, 20, stringFormat,false );
            graphics.DrawLine(pen, 32, yPos, 32, posAwal);
            graphics.DrawLine(pen, 165, yPos, 165, posAwal);

            yPos = TulisItem(graphics, "Jumlah", font, 35, yPos, 80, stringFormat, false);
            format = new PdfStringFormat(PdfTextAlignment.Right);
            yPos = TulisItem(graphics, txtJumlahSPP.Text, font, 460, yPos, 80, format,true );
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            graphics.DrawLine(pen, 445, yPos, 445, posAwal);
            
            //yPos = TulisItem(graphics, txtJumlahSPP.Text, font, 340, yPos, 80, format);

            yPos = TulisItem(graphics, "Jumlah SPP yang diminta", font, 10, yPos, 195, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            stringFormat = new PdfStringFormat(PdfTextAlignment.Left);
            decimal JumlahSPP = DataFormat.FormatUangReportKeDecimal(txtJumlahSPP.Text);
            yPos = TulisItem(graphics, JumlahSPP.Terbilang(), font, kiri + 5 , yPos,
                lebarArena - 5, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            
            yPos = TulisItem(graphics, "Nomor dan Tanggal SPP", font, 10, yPos, 195, stringFormat);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = TulisItem(graphics, txtNoSPP.Text + dtSPP.Value.ToString("dd MMM yyyy"), font, 200, yPos, 250, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = TulisItem(graphics, "Sumber Dana", font, 10, yPos, 100, stringFormat);
            yPos = TulisItem(graphics, ctrlSumberDana2.NamaSumberDana, font, 60, yPos, 320, stringFormat, true);


            graphics.DrawLine(pen, kiri, yPos+3, lebarArena + 5, yPos+3);

            font = new PdfTrueTypeFont(new Font("Arial", 10,FontStyle.Bold));
            stringFormat.Alignment = PdfTextAlignment.Left;
            yPos = yPos + 2;
            text = "Potongan";
            yPos = TulisItem(graphics, text, font, kiri+5 , yPos, 100, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos , lebarArena + 5, yPos);
            posAwal = yPos;
            stringFormat.Alignment = PdfTextAlignment.Center;
            text = "No";
            yPos = TulisItem(graphics, text, font, kiri  + 5, yPos , 50, stringFormat);
            float pos1 = kiri + 5;
            text = "Uraian";
            yPos = TulisItem(graphics, text, font, kiri  + 50, yPos, 250, stringFormat);
            float pos2 = kiri + 50;
            text = "Jumlah";
            yPos = TulisItem(graphics, text, font, kiri + 300, yPos, 100, stringFormat);
            float pos3 = kiri + 300;
            text = "Keterangan";
            yPos = TulisItem(graphics, text, font, kiri  + 400, yPos, 100, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            float pos4 = kiri + 405;
            List<PotonganSPP> lstPotongan= ctrlPotongan1.getDisplayRekening();
            int No = 0;
            stringFormat.Alignment = PdfTextAlignment.Left;
            foreach (PotonganSPP ps in lstPotongan)
            {

                if (ps.Jumlah > 0)
                {
                    No++;

                    format = new PdfStringFormat(PdfTextAlignment.Right );
                    font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));

                    yPos = TulisItem(graphics, No.ToString(), font, pos1, yPos, 15, format);
                    yPos = TulisItem(graphics, "Rp", font, pos3, yPos, 20, stringFormat);
                    yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, pos3, yPos, 100, format);
                     yPos = TulisItem(graphics, ps.NamaRekening, font, pos2, yPos, 250, stringFormat, true);
  
                }
  
            }
            
            yPos = yPos + 3;
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            graphics.DrawLine(pen, pos1, posAwal, pos1, yPos);
            graphics.DrawLine(pen, pos2, posAwal, pos2, yPos);

            text = "Jumlah";
            stringFormat.Alignment = PdfTextAlignment.Left;
            yPos = TulisItem(graphics, txtJumlahMpn.Text, font, pos3, yPos, 100, format);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = TulisItem(graphics, "JUMLAH", font, pos2, yPos, 250, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            graphics.DrawLine(pen, pos3, posAwal, pos3, yPos);
            graphics.DrawLine(pen, pos4, posAwal, pos4, yPos);
            yPos = yPos + 3;

            text = "Informasi";
            yPos = TulisItem(graphics, text, font, kiri  + 5, yPos, 100, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            posAwal= yPos;
            stringFormat.Alignment = PdfTextAlignment.Center;
            text = "No";
            yPos = TulisItem(graphics, text, font, pos1, yPos, 50, stringFormat);
            text = "Uraian";
            yPos = TulisItem(graphics, text, font, pos2, yPos, 250, stringFormat);
            text = "Jumlah";
            yPos = TulisItem(graphics, text, font, pos3, yPos, 100, stringFormat);
            text = "Keterangan";
            yPos = TulisItem(graphics, text, font, pos4, yPos, 100, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            List<PotonganSPP> lstPotonganNonMpn = ctrlPotongan2.getDisplayRekening();
            No = 0;
            foreach (PotonganSPP ps in lstPotonganNonMpn)
            {

                if (ps.Jumlah > 0)
                {
                    No++;

                    format = new PdfStringFormat(PdfTextAlignment.Right);
                    font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));
                    stringFormat.Alignment = PdfTextAlignment.Left;
                    yPos = TulisItem(graphics, No.ToString(), font, pos1, yPos, 15, format);



                    yPos = TulisItem(graphics, "Rp", font, pos3, yPos, 20, stringFormat);
                    yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, pos3, yPos, 100, format);

                    yPos = TulisItem(graphics, ps.NamaRekening, font, pos2, yPos, 250, stringFormat, true);

                  //  graphics.DrawLine(penRekening, AwalKol2 + 5, yPos, kiri + lebarArena, yPos);
                   

                }



            }
            yPos = yPos + 3;
            graphics.DrawLine(pen, pos1, posAwal, pos1, yPos);
            graphics.DrawLine(pen, pos2, posAwal, pos2, yPos);

         
            yPos = TulisItem(graphics, txtJumlahNonMpn.Text, font, pos3, yPos, 100, format);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            //610, yPos, 80

            stringFormat.Alignment = PdfTextAlignment.Left;
            yPos = TulisItem(graphics, "JUMLAH", font, pos2, yPos, 250, stringFormat, true);
            graphics.DrawLine(pen, pos3, posAwal, pos3, yPos);
            graphics.DrawLine(pen, pos4, posAwal, pos4, yPos);

            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = yPos + 3;
            decimal JumlahPajak=DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text)+ DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text);
            decimal JumlahMurni = 0l;
            if (m_iJenisSPP == 4)
            {
                JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) -
                ((DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text)) + DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text));
            }
            else
            {
                JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) -
                (DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text));

            }


       
            yPos = TulisItem(graphics,JumlahPajak.ToRupiahInReport()   , font, pos3, yPos, 100, format);

            yPos = TulisItem(graphics, "JUMLAH PAJAK ", font, kiri + 5, yPos, 250, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = yPos + 1;

            yPos = TulisItem(graphics, "Rp.", font, 450, yPos, 20, stringFormat,false);
            yPos = TulisItem(graphics, JumlahMurni.ToRupiahInReport(), font, 460, yPos, 80, format);

            yPos = TulisItem(graphics, "JUMLAH SPM", font, kiri + 5, yPos, 250, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = yPos + 1;

            yPos = TulisItem(graphics, "Sejumlah", font, kiri + 5, yPos, 250, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);
            yPos = TulisItem(graphics, JumlahMurni.Terbilang(), font, 10, yPos, lebarArena, stringFormat, true);
            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

            yPos = yPos + 2;
            format = new PdfStringFormat(PdfTextAlignment.Center);
            font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold));
            yPos = TulisItem(graphics, "Ketapang, " + dtSPM.Value.ToString("dd MMM yyyy"), font, kiri, yPos, lebarArena, format, true);
            yPos = TulisItem(graphics, txtJabatanPA.Text, font, kiri, yPos, lebarArena, format, true);
            yPos = yPos + 15;
            font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold ));
            yPos = TulisItem(graphics, txtNamaPA.Text, font, kiri, yPos, lebarArena, format, true, true);

            yPos = TulisItem(graphics, "NIP " + txtNIPPA.Text, font, kiri, yPos, lebarArena, format, true);

            yPos = yPos + 3;

            graphics.DrawLine(pen, kiri, yPos, lebarArena + 5, yPos);

            stringFormat.Alignment = PdfTextAlignment.Center;
            yPos = TulisItem(graphics, "SPM ini sah apabila ditadatangani dan distempel oleh KepalaSKPD. ", font, kiri, yPos, lebarArena, stringFormat, true);
            rect = new Rectangle((int)kiri,
                                            (int)posAtas,
                                            (int)lebarArena,
                                            (int)yPos - (int)posAtas);

            graphics.DrawRectangle(pen, rect);





            using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPM.pdf"), FileMode.Create, FileAccess.ReadWrite))
            {
                //Save the PDF document to file stream.
                document.Save(outputFileStream);

            }

            //Close the document.
            document.Close(true);
            pdfViewer pV = new pdfViewer();
            pV.Document = Path.GetFullPath(@"../../../SPM.pdf");
            pV.Show();
           

        }
        private void CetakSP2D()
        {
            //Create a new PDF document.
            int rowmasalah=0;
            try
            {
                PdfDocument document = new PdfDocument();
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;

                //document.PageSettings.Size = new SizeF(1039,1323);
                //if (m_iJenisSPP == 4)
                //{
                //27.5 × 35.5
                document.PageSettings.Size = new Size(1006, 780); // new Size(800, 780);//(1006, 780);
                //}
                //else { 
                //     document.PageSettings.Size = new Size(992, 765);
                //}
                PdfSection section = document.Sections.Add();
                section.PageSettings.Orientation = PdfPageOrientation.Portrait;
                float yPos = 0;
                //section.PageSettings.Height = 400;
                PdfPage page = section.Pages.Add();
                PdfGraphics graphics = page.Graphics;


                float kiri = 20;
                float posAtas = 0;

              
                float tinggiArena = page.GetClientSize().Height;//- 2 * posAtas;

                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.2f);
                PdfPen penRekening = new PdfPen(PdfBrushes.Black, 0.1f);
                penRekening.DashStyle = PdfDashStyle.Dot;


                rowmasalah = 1;

                FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
                PdfBitmap image = new PdfBitmap(imageStream);
                //Draw the image
                PointF p = new PointF(kiri + 5, 5);
                Size sizeImage = new Size(30, 40);
                graphics.DrawImage(image, p, sizeImage);


                rowmasalah = 2;
    

                PdfFont font;
               // PdfStringFormat stringFormat = new PdfStringFormat();
                PdfStringFormat stringFormatLeft = new PdfStringFormat();

                //stringFormat.Alignment = PdfTextAlignment.Left;

               
                stringFormatLeft.Alignment = PdfTextAlignment.Left;
                PdfStringFormat stringFormatCenter = new PdfStringFormat();
                stringFormatCenter.Alignment = PdfTextAlignment.Center;

                PdfStringFormat stringFormatRight = new PdfStringFormat();
                stringFormatRight.Alignment = PdfTextAlignment.Right;
                PdfStringFormat stringFormatJustify = new PdfStringFormat();
                stringFormatJustify.Alignment = PdfTextAlignment.Justify;
                rowmasalah = 3;
                yPos = 0;
         
                float lebarCetakan =(page.GetClientSize().Width) - 80;
                float setengah = lebarCetakan / 2;
                //font = new PdfTrueTypeFont(@"arialn.ttf", 15);
          
               font = new PdfTrueTypeFont(new Font("Arial", 10, FontStyle.Bold));
               float ukuranFontHeader = 10;
               string text = "";
               float characterSpace = stringFormatCenter.CharacterSpacing;
               stringFormatCenter.CharacterSpacing = 2f;
                yPos = TulisItem(graphics, "Nomor: " + txtNoSP2D.Text, ukuranFontHeader+2, 
                       setengah + kiri, yPos, setengah, stringFormatCenter, true,false,true);
                stringFormatCenter.CharacterSpacing = characterSpace;
                
                yPos = TulisItem(graphics, "Pemerintah Kabupaten Ketapang", ukuranFontHeader+2, kiri + 40 , yPos+5, 
                                     lebarCetakan, stringFormatLeft,false,false, true);
                yPos = TulisItem(graphics, "SURAT PERINTAH PENCAIRAN DANA", ukuranFontHeader, setengah + kiri  , yPos, setengah, stringFormatCenter, true);
                yPos = TulisItem(graphics, "(SP2D)", ukuranFontHeader, setengah + kiri  , yPos, setengah, stringFormatCenter, true);

                rowmasalah = 4;
                yPos = yPos + 2;

              //  font = new PdfTrueTypeFont(new Font("Arial Narrow", 10, FontStyle.Regular));
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                yPos = yPos + 1;
                yPos = TulisItem(graphics, "No SPM", ukuranFontHeader, kiri + 5, yPos, 100, stringFormatLeft);
                yPos = TulisItem(graphics, ": " + txtNoSPM.Text, ukuranFontHeader, 140, yPos, 400, stringFormatLeft);
                
                text = "DARI";
                yPos = TulisItem(graphics, text, ukuranFontHeader, kiri + setengah + 50, yPos, 100, stringFormatLeft);
                yPos = TulisItem(graphics, "KUASA BUD", ukuranFontHeader, kiri + setengah + 20 + 150, yPos, 100, stringFormatLeft, true);


                rowmasalah = 5;
                yPos = TulisItem(graphics, "Tanggal SPM ", ukuranFontHeader, kiri + 5, yPos, 100, stringFormatLeft);
                yPos = TulisItem(graphics, ": " + dtSPM.Value.ToTanggalIndonesia(), ukuranFontHeader, 140, yPos, 200, stringFormatLeft);
                yPos = TulisItem(graphics, "TAHUN", ukuranFontHeader, kiri + setengah + 50, yPos, 100, stringFormatLeft);
                yPos = TulisItem(graphics, GlobalVar.TahunAnggaran.ToString(), ukuranFontHeader, kiri + setengah + 20 + 150, yPos, 100, stringFormatLeft, true);

                rowmasalah = 6;
                text = "SKPD";

                yPos = TulisItem(graphics, text, ukuranFontHeader, kiri + 5, yPos, 100, stringFormatLeft, false);
                yPos = TulisItem(graphics, ": ", ukuranFontHeader, 140, yPos, (page.GetClientSize().Width / 2) - 150, stringFormatLeft, false);
                yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD(), ukuranFontHeader, 145, yPos, (page.GetClientSize().Width / 2) - 150, stringFormatLeft, true);
                //  yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD(), font, 105, yPos, 350, stringFormatLeft, true);
                yPos = yPos + 2;

                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                graphics.DrawLine(pen, page.GetClientSize().Width / 2 + 8, posAtas, page.GetClientSize().Width / 2 + 8, yPos);
                yPos = yPos + 2;

                rowmasalah = 7;
                text = "Bank/Pos";
                yPos = TulisItem(graphics, text, font, kiri + 5, yPos, page.GetClientSize().Width, stringFormatLeft);
                yPos = TulisItem(graphics, ": " + "PT Bank Kalimantan Barat Ketapang ", font, 140, yPos, 300, stringFormatLeft, true);
            
                font = new PdfTrueTypeFont(new Font("Arial Narrow", 9, FontStyle.Bold));

                yPos = TulisItem(graphics, "Hendaklah mencairkan/memindahbukukan dari Bank Rekening Nomor P-700.100.737-2", font, kiri + 5, yPos, page.GetClientSize().Width, stringFormatLeft, true);
                font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));

                yPos = TulisItem(graphics, "Uang sebesar: ", font, kiri + 5, yPos, page.GetClientSize().Width, stringFormatLeft);
                yPos = TulisItem(graphics, ":", font, 140, yPos, 140, stringFormatLeft);
                yPos = TulisItem(graphics, txtJumlah.Text, font, 145, yPos, page.GetClientSize().Width, stringFormatLeft, true);

                yPos = TulisItem(graphics, "(" + DataFormat.FormatUangReportKeDecimal(txtJumlah.Text).Terbilang() + ")", font, 145, yPos, page.GetClientSize().Width, stringFormatLeft, true);
                rowmasalah = 8;
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                yPos = TulisItem(graphics, "Kepada ", font, kiri + 5, yPos, 340, stringFormatLeft);
                yPos = TulisItem(graphics, ":", font, 140, yPos, 10, stringFormatLeft);
                
                if (m_oSPP.Penerima > 0)
                {
                    yPos = TulisItem(graphics, txtNamaPenerima.Text + "/" + txtJabatanPenerima.Text + " " + ctrlPerusahaan1.GetPerusahaan().NamaPerusahaan, font, 145, yPos, 340, stringFormatLeft, true);
                    
                }
                else
                {
                    yPos = TulisItem(graphics, txtNamaPenerima.Text + "/" + txtJabatanPenerima.Text, font, 145, yPos, 340, stringFormatLeft, true);
                }
                yPos = TulisItem(graphics, "NPWP", font, kiri + 5, yPos, 340, stringFormatLeft);
                yPos = TulisItem(graphics, ":", font, 140, yPos, 150, stringFormatLeft);
                yPos = TulisItem(graphics, txtNoNPWP.Text, font, 145, yPos, 400, stringFormatLeft, true);
                rowmasalah = 9;
                yPos = TulisItem(graphics, "Nomor Rekening Bank", font, kiri + 5, yPos, 340, stringFormatLeft);
                yPos = TulisItem(graphics, ":", font, 140, yPos, 150, stringFormatLeft);
                yPos = TulisItem(graphics, txtNoRekening.Text, font, 145, yPos, 340, stringFormatLeft, true);

                yPos = TulisItem(graphics, "Bank/Pos", font, kiri + 5, yPos, 340, stringFormatLeft);
                yPos = TulisItem(graphics, ":", font, 140, yPos, 140, stringFormatLeft);
                yPos = TulisItem(graphics, ctrlDaftarBank1.NamaBank + " " + ctrlDaftarBank1.KeteranganNamaBank, font, 145, yPos, 340, stringFormatLeft, true);
                yPos = TulisItem(graphics, "Keperluan Untuk", font, kiri + 5, yPos, 340, stringFormatLeft);
                yPos = TulisItem(graphics, ":", font, 140, yPos, 125, stringFormatLeft);
                rowmasalah = 10;
                char tab = '\u0009';
                PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Justify);
                //yPos = TulisItem(graphics, txtUraian.Text.ReplaceUnicode().Replace(System.Environment.NewLine, "").Replace(tab.ToString(), " ").TrimEnd('\r', '\n'),
                //    font, 145, yPos,
                //    lebarCetakan - kiri - 125, format, true);
                yPos = TulisItem(graphics, txtUraian.Text.ReplaceUnicode(),
             font, 145, yPos,
             lebarCetakan - kiri - 125, format, true);
        
                yPos = yPos + 2;
                rowmasalah = 11;
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                posAtas = yPos;
                List<SPPRekening> lst = new List<SPPRekening>();
                yPos = yPos + 2;

                lst = ctrlRekeningKegiatan1.getDisplayRekening();
                if (lst.Count <= 10)
                {
                    font = new PdfTrueTypeFont(new Font("Arial", 9));
                }
                if (lst.Count > 10 && lst.Count <= 15)
                {
                    font = new PdfTrueTypeFont(new Font("Arial", 8));
                }
                if (lst.Count > 15 && lst.Count <= 20)
                {
                    font = new PdfTrueTypeFont(new Font("Arial", 7));
                }
                if (lst.Count > 20)
                {
                    font = new PdfTrueTypeFont(new Font("Arial", 7));

                }

                int i = 0;
            


                format = new PdfStringFormat(PdfTextAlignment.Center);
                yPos = TulisItem(graphics, "NO", font, kiri , yPos, 15, stringFormatCenter, false, false, true);

                yPos = TulisItem(graphics, "KODE REKENING", font, kiri + 20, yPos, 110, stringFormatCenter, false, false, true);
                // 195

                format = new PdfStringFormat(PdfTextAlignment.Right);
                yPos = TulisItem(graphics, "JUMLAH (Rp)", font, kiri + 490, yPos, 125, stringFormatCenter, false, false, true);
                //495-kiri-
                yPos = TulisItem(graphics, "URAIAN", font,  150, yPos, 200, stringFormatCenter, true, false, true);

                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                rowmasalah = 13;
                int rowRekening = 0;
                float barisSebelum = yPos;
                float jarakantarbaris = 0;
                if (m_iJenisSPP == 0)
                {
                    format = new PdfStringFormat(PdfTextAlignment.Left);
                    yPos = TulisItem(graphics, "", font, kiri + 2, yPos, 15, stringFormatLeft);

                    
                    yPos = TulisItem(graphics, "Rp", font, kiri + 500, yPos, 20, stringFormatLeft);

                    format = new PdfStringFormat(PdfTextAlignment.Right);
                    yPos = TulisItem(graphics, txtJumlah.Text, font, kiri + 500, yPos, 110, format);


                    yPos = TulisItem(graphics, "Uang Persediaan ", font, 195, yPos, 400, stringFormatLeft, true);

                    if (rowRekening < 6)
                    {
                        for (int idx = rowRekening; idx < 6; idx++)
                        {

                            format = new PdfStringFormat(PdfTextAlignment.Left);
                            yPos = TulisItem(graphics, " ", font, kiri, yPos, 15, stringFormatLeft);

                            yPos = TulisItem(graphics, "", font, kiri + 15, yPos, 180, stringFormatLeft);
                            yPos = TulisItem(graphics, " ", font, kiri + 490, yPos, 20, stringFormatLeft);

                            format = new PdfStringFormat(PdfTextAlignment.Right);
                            yPos = TulisItem(graphics, "", font, kiri + 500, yPos, 130, format);

                            yPos = TulisItem(graphics, "", font, 195, yPos, 400, stringFormatLeft, false);

                            yPos = yPos + jarakantarbaris;
                            graphics.DrawLine(penRekening, kiri, yPos, kiri + lebarCetakan, yPos);

                            rowRekening++;
                        }

                    }

                }
                else
                {
                    if (m_iJenisSPP == 1)
                    {
                        SPPLogic osppLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                        List<SPPRekening> lstGU = osppLogic.GetRingkasanGU(m_NoUrut);
                        if (lstGU != null)
                        {
                            foreach (SPPRekening sr in lstGU)
                            {
                                if (sr.Jumlah != 0)
                                {

                                    format = new PdfStringFormat(PdfTextAlignment.Left);

                                    yPos = TulisItem(graphics, (++i).ToString(), font, kiri + 2, yPos, 15, stringFormatCenter);

                                    yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening(), font, kiri + 18, yPos, 180, stringFormatLeft);

                                    yPos = TulisItem(graphics, "Rp", font, kiri + 500, yPos, 20, stringFormatLeft);

                                    format = new PdfStringFormat(PdfTextAlignment.Right);
                                    yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, kiri + 500, yPos, 110, format);

                                    yPos = TulisItem(graphics, sr.NamaRekening, font, 165, yPos, 400, stringFormatLeft, true);
                                    if (jarakantarbaris > 0)
                                    {
                                        if (yPos - barisSebelum < jarakantarbaris)
                                        {
                                            jarakantarbaris = yPos - barisSebelum;
                                        }
                                    }
                                    else
                                    {
                                        jarakantarbaris = yPos - barisSebelum;
                                    }
                                    barisSebelum = yPos;


                                    graphics.DrawLine(penRekening, kiri, yPos, kiri + lebarCetakan, yPos);

                                    rowRekening++;

                                }
                            }

                        }
                    }
                    else {
                        foreach (SPPRekening sr in lst)
                        {
                            if (sr.Jumlah != 0)
                            {

                                format = new PdfStringFormat(PdfTextAlignment.Left);

                                yPos = TulisItem(graphics, (++i).ToString(), font, kiri + 2, yPos, 15, stringFormatCenter);

                                yPos = TulisItem(graphics, sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening(), font, kiri + 18, yPos, 180, stringFormatLeft);

                                yPos = TulisItem(graphics, "Rp", font, kiri + 500, yPos, 20, stringFormatLeft);

                                format = new PdfStringFormat(PdfTextAlignment.Right);
                                yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), font, kiri + 500, yPos, 110, format);

                                yPos = TulisItem(graphics, sr.NamaRekening, font, 165, yPos, 400, stringFormatLeft, true);
                                if (jarakantarbaris > 0)
                                {
                                    if (yPos - barisSebelum < jarakantarbaris)
                                    {
                                        jarakantarbaris = yPos - barisSebelum;
                                    }
                                }
                                else
                                {
                                    jarakantarbaris = yPos - barisSebelum;
                                }
                                barisSebelum = yPos;


                                graphics.DrawLine(penRekening, kiri, yPos, kiri + lebarCetakan, yPos);

                                if (yPos >= page.GetClientSize().Height - 40)
                                {
                                    graphics.DrawLine(pen, kiri , posAtas, kiri , yPos);
                                    graphics.DrawLine(pen, kiri + 15, posAtas, kiri + 15, yPos);
                                  //  graphics.DrawLine(pen, kiri + 178, posAtas, kiri + 178, yPos);
                                    graphics.DrawLine(pen, kiri + 490, posAtas, kiri + 490, yPos);
                                    graphics.DrawLine(pen, lebarCetakan + kiri, posAtas, lebarCetakan + kiri, yPos);

                                    graphics.DrawLine(pen, 160, posAtas, 160, yPos);
                                    Rectangle rect = new Rectangle((int)kiri,
                                               (int)0,
                                               (int)lebarCetakan, (int)(yPos + 5));


                                    graphics.DrawRectangle(pen, rect);
                                    
                                    page = document.Pages.Add();
                                    graphics = page.Graphics;
                                    yPos = 10;
                                    graphics.DrawLine(pen, kiri, yPos, lebarCetakan + kiri, yPos);
                                    //yPosAtasSPD = 10;
                                    //yPosAtasSPD = 10;
                                    posAtas = 10;
                                    

                                }


                                rowRekening++;

                            }

                        }

                    }

               
                }
                //kiri + 500, yPos, 140,
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                graphics.DrawLine(pen, kiri+15, yPos, kiri+15, posAtas);
             
                graphics.DrawLine(pen, 160, yPos, 160, posAtas);
                

                format = new PdfStringFormat(PdfTextAlignment.Right);
                yPos = TulisItem(graphics, "JUMLAH.", font, kiri + 195, yPos, 125, format);

                yPos = TulisItem(graphics, txtJumlah.Text, font, kiri + 500, yPos, 110, format, true, false, true);


                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                graphics.DrawLine(pen, kiri + 495, yPos, kiri + 495, posAtas);
                yPos = yPos + 20;

                rowmasalah = 14;
                i = 0;
                // Potongan
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                text = "Potongan ";
                yPos = TulisItem(graphics, text, font, kiri + 5, yPos, 100, stringFormatLeft, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                posAtas = yPos;
                yPos = TulisItem(graphics, "No", font, kiri, yPos, 15, stringFormatCenter, false, false, true);
                float pos1 = kiri;
                yPos = TulisItem(graphics, "Uraian", font, kiri + 20, yPos, 110, stringFormatCenter, false, false, true);
             
                float pos2 = kiri + 20;
                yPos = TulisItem(graphics, "Jumlah", font, kiri + 350, yPos, 200, stringFormatCenter, false, false, true);


                float pos3 = 150;
                text = "Keterangan";
                yPos = TulisItem(graphics, text, font, kiri + 500, yPos, 100, stringFormatCenter, true);

                i = 0;
                float pos4 = kiri + 500;
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                rowmasalah = 15;

                List<PotonganSPP> lstPotongan = ctrlPotongan1.getDisplayRekening();
                int No = 0;
              
                int rowPotongan = 0;
                foreach (PotonganSPP ps in lstPotongan)
                {

                    if (ps.Jumlah > 0)
                    {
                
                        format = new PdfStringFormat(PdfTextAlignment.Right);
                        font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));

                        yPos = TulisItem(graphics, (++i).ToString(), font, kiri + 2, yPos, 15, stringFormatCenter);
                        yPos = TulisItem(graphics, ps.NamaRekening, font, kiri + 18, yPos, 180, stringFormatLeft);
                        yPos = TulisItem(graphics, "Rp", font, kiri + 400, yPos, 20, stringFormatLeft);
                        yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, 410, yPos, 90, format, true );
                        graphics.DrawLine(penRekening, kiri, yPos, kiri + lebarCetakan, yPos);
                        rowPotongan++;

                    }



                }
              
                yPos = yPos + 3;
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                graphics.DrawLine(pen, kiri + 15, yPos, kiri + 15, posAtas);
             
            

               // yPos = TulisItem(graphics, "Rp", font, kiri + 500, yPos, 20, stringFormatLeft);
     
                yPos = TulisItem(graphics, "Jumlah", font, kiri+200, yPos, 90, format, false );
               // yPos = TulisItem(graphics, txtJumlahMpn.Text, font, pos3, yPos, 100, format);
                yPos = TulisItem(graphics, txtJumlahMpn.Text, font, 410, yPos, 90, format, true);
                graphics.DrawLine(pen, 413, yPos, 413, posAtas);
                graphics.DrawLine(pen, kiri + 495, yPos, kiri + 495, posAtas);

                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                yPos = TulisItem(graphics, "JUMLAH", font, pos2, yPos, 250, stringFormatLeft, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);


                yPos = yPos + 3;
                i = 0;
                text = "Informasi";
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
           
                yPos = TulisItem(graphics, text, font, kiri + 5, yPos, 100, stringFormatLeft, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                posAtas = yPos;

                rowmasalah = 16;
   
                yPos = TulisItem(graphics, "NO", font, kiri, yPos, 15, stringFormatCenter, false, false, true);


                yPos = TulisItem(graphics, "Uraian", font, kiri + 20, yPos, 110, stringFormatCenter, false, false, true);


                yPos = TulisItem(graphics, "Jumlah", font, kiri+ 350, yPos, 200, stringFormatCenter, false , false, true);

                text = "Keterangan";
                yPos = TulisItem(graphics, text, font, pos4, yPos, 100, stringFormatCenter, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                List<PotonganSPP> lstPotonganNonMpn = ctrlPotongan2.getDisplayRekening();
                No = 0;
                i = 0;
                decimal jumlahNonMPN = 0;
                rowPotongan = 0;
                foreach (PotonganSPP ps in lstPotonganNonMpn)
                {

                    if (ps.Jumlah > 0)
                    {
                        No++;

            
                        font = new PdfTrueTypeFont(new Font("Arial", 9, FontStyle.Regular));
                        yPos = TulisItem(graphics, (++i).ToString(), font, kiri + 2, yPos, 15, stringFormatCenter);
                        yPos = TulisItem(graphics, ps.NamaRekening, font, kiri + 18, yPos, 180, stringFormatLeft);
                        yPos = TulisItem(graphics, "Rp", font, kiri + 400, yPos, 20, stringFormatLeft);
                        yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, 410, yPos, 90, format, true);
                


                        //yPos = TulisItem(graphics, (++i).ToString(), font, kiri + 5, yPos, 15, stringFormatRight);



                        //yPos = TulisItem(graphics, "Rp", font, pos3, yPos, 20, stringFormatLeft);
                        //yPos = TulisItem(graphics, ps.Jumlah.ToRupiahInReport(), font, pos3, yPos, 100, format);
                        jumlahNonMPN = jumlahNonMPN + ps.Jumlah;

                       // yPos = TulisItem(graphics, ps.NamaRekening, font, pos2, yPos, 250, stringFormatLeft, true);

                        rowPotongan++;

                    }



                }

                rowmasalah = 17;             
                yPos = yPos + 3;
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                graphics.DrawLine(pen, kiri + 15, yPos, kiri + 15, posAtas);
           
             

                yPos = TulisItem(graphics, "Jumlah", font, kiri + 200, yPos, 90, format, false);
                //if (jumlahNonMPN == 0)
                //{
                //    yPos = TulisItem(graphics, "0", font, pos3, yPos, 100, format);
                //}
                //else
                //{
                    //yPos = TulisItem(graphics, jumlahNonMPN.ToRupiahInReport(), font, pos3, yPos, 100, format);
                    yPos = TulisItem(graphics, jumlahNonMPN.ToRupiahInReport(), font, 410, yPos, 90, format, true);

               // }
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                graphics.DrawLine(pen, 413, yPos, 413, posAtas);
                graphics.DrawLine(pen, kiri + 495, yPos, kiri + 495, posAtas);
       

                yPos = yPos + 3;

                yPos = TulisItem(graphics, "JUMLAH YANG DIBAYARKAN ", font, kiri + 5, yPos, 400, stringFormatLeft, true, false, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                yPos = TulisItem(graphics, "Jumlah yang diminta ", font, kiri + 5, yPos, 400, stringFormatLeft);
                decimal JumlahSPP = 0L;
                if (m_iJenisSPP > 0)
                {
                    JumlahSPP = DataFormat.FormatUangReportKeDecimal(txtJumlahSPP.Text);
                    //yPos = TulisItem(graphics, txtJumlahSPP.Text, font, kiri + 500, yPos, 125, stringFormatRight, true);
                    yPos = TulisItem(graphics, txtJumlahSPP.Text, font, kiri + 500, yPos, 110, format, true, false, true);

                }
                else
                {
                    JumlahSPP = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                    yPos = TulisItem(graphics, txtJumlah.Text, font, kiri + 500, yPos, 110, format, true, false, true);

                    //yPos = TulisItem(graphics, txtJumlah.Text, font, kiri + 500, yPos, 125, stringFormatRight, true);
                }

                rowmasalah = 18;
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                decimal JumlahPajak = DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text) + 
                                      DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text);
                
                decimal JumlahMurni = 0L;
                if (m_iJenisSPP == 4)
                {
                    JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) -
                    ((DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text)) + DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text));
                }
                else
                {
                    JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text) -
                    (DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text));

                }

                rowmasalah = 19;
               // yPos = TulisItem(graphics, JumlahPajak.ToRupiahInReport(), font, kiri + 500, yPos, 125, stringFormatRight, true);

                if (m_iJenisSPP != 4)
                {

                    if (DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text) > 0)
                    {
                        yPos = TulisItem(graphics, "Jumlah Potongan ", font, kiri + 5, yPos, 400, stringFormatLeft, false);
                        yPos = TulisItem(graphics, txtJumlahMpn.Text, font, kiri + 500, yPos, 110, format, true, false, true);
                    }
                    else
                    {
                        yPos = TulisItem(graphics, "Jumlah Potongan ", font, kiri + 5, yPos, 400, stringFormatLeft, true);

                    }
                }
                else
                {
                    yPos = TulisItem(graphics, "Jumlah Potongan ", font, kiri + 5, yPos, 400, stringFormatLeft, false);
                    yPos = TulisItem(graphics, (DataFormat.FormatUangReportKeDecimal(txtJumlahMpn.Text)+ DataFormat.FormatUangReportKeDecimal(txtJumlahNonMpn.Text)).ToRupiahInReport(), font, kiri + 500, yPos, 110, format, true, false, true);
                }

                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);

                yPos = TulisItem(graphics, "Jumlah Yang Dibayarkan  ", font, kiri + 5, yPos, 400, stringFormatLeft, false, false, true);
                //yPos = TulisItem(graphics, JumlahMurni.ToRupiahInReport(), font, kiri + 500, yPos, 125, stringFormatRight, true);
                yPos = TulisItem(graphics, JumlahMurni.ToRupiahInReport(), font, kiri + 500, yPos, 110, format, true, false, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                yPos = TulisItem(graphics, "Uang Sejumlah: Rp. " + JumlahMurni.ToRupiahInReport(), font, kiri + 5, yPos, 400, stringFormatLeft, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                if (JumlahMurni < 0)
                {
                    MessageBox.Show("Sila cek nilai nilai potongannya. Ada yang salah..");
                    return;
                }
                string terbilang = JumlahMurni.Terbilang().ReplaceUnicode();
                yPos = TulisItem(graphics, terbilang, font, kiri + 20, yPos, lebarCetakan - 30, stringFormatLeft, true);
                yPos = TulisItem(graphics, "", font, kiri + 20, yPos, lebarCetakan - 30, stringFormatLeft, true);
                graphics.DrawLine(pen, kiri, yPos, kiri + lebarCetakan, yPos);
                clsCetakSPP oCetak = new clsCetakSPP();
                yPos = yPos + 20;



                rowmasalah = 20;

                Pejabat pejabat = new Pejabat();
                pejabat = ctrlBendaharaBUD.GetPejabat();
               setengah = (page.GetClientSize().Width / 2) - 20;
                float posisiTengah = page.GetClientSize().Width / 2 + 10;

                yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSP2D.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
                setengah, m_stringFormatCenter, true);

                yPos = TulisItem(graphics, pejabat.Jabatan, mfont10, posisiTengah, yPos,
                 setengah, m_stringFormatCenter, true);

                yPos = yPos + 30;
                //Object grid2row5 = new { Name = "", Age = m_oBendahara.Nama };

                rowmasalah = 21;
                yPos = TulisItem(graphics, pejabat.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
                yPos = TulisItem(graphics, "NIP " + pejabat.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);

                yPos = TulisItem(graphics, "Lembar 1 ", font, kiri + 5, yPos, 40, stringFormatLeft);
                yPos = TulisItem(graphics, "Bank yang ditunjuk ", font, kiri + 45, yPos, 200, stringFormatLeft, true);

                yPos = TulisItem(graphics, "Lembar 2 ", font, kiri + 5, yPos, 40, stringFormatLeft);
                yPos = TulisItem(graphics, "Pengguna Anggaran/Kuasa Pengguna Anggaran", font, kiri + 45, yPos, 200, stringFormatLeft, true);

                yPos = TulisItem(graphics, "Lembar 3 ", font, kiri + 5, yPos, 40, stringFormatLeft);
                yPos = TulisItem(graphics, "Arsip Kuasa BUD ", font, kiri + 45, yPos, 200, stringFormatLeft, true);

                yPos = TulisItem(graphics, "Lembar 4 ", font, kiri + 5, yPos, 40, stringFormatLeft);
                yPos = TulisItem(graphics, "Pihak ketiga", font, kiri + 45, yPos, 200, stringFormatLeft, true);


                rowmasalah = 22;
                Rectangle rct = new Rectangle((int)kiri,
                                               (int)0,
                                               (int)lebarCetakan, (int)(yPos + 5));


                graphics.DrawRectangle(pen, rct);

                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SP2D.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }


                rowmasalah = 23;
                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../SP2D.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan:   " + 
                rowmasalah.ToString() + "     " + ex.Message);

            }


        }
        private float TulisItem(PdfGraphics graphics , string text=null, PdfFont font =null, 
            float x=0,
            float y=0,
            float widthSpace=10,  
            PdfStringFormat stringFormat=null,
            bool changeLine = false, bool underline = false , 
            bool bold = false  )
        {
            
            float yPos = y;
            try
            {
                

                PdfFont lfont;

                //stringFormat.Alignment = PdfTextAlignment.Center;
                //stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;
                text = text.Replace("\t", "").Replace("\r\n", "");
                FontStyle style;
                style = FontStyle.Regular;
                if (underline == true)
                    style = style | FontStyle.Underline;
                if (bold == true)
                    style = style | FontStyle.Bold;

                lfont = new PdfTrueTypeFont(new Font("Arial Narrow", font.Size, style));
              

                SizeF size = lfont.MeasureString(text);
                float height = size.Height;
                int pengali = CariPengali(size.Width, widthSpace);

                if (size.Width > widthSpace)
                {

                    height = size.Height * pengali;
                }

                //stringFormat.Alignment=PdfAlignmentStyle.
                RectangleF rect = new RectangleF(x, y, widthSpace, height);
                if (text == null)
                {
                    graphics.DrawString("  ", lfont, PdfBrushes.Black, rect, stringFormat);
                }
                else
                {
                    graphics.DrawString(text, lfont, PdfBrushes.Black, rect, stringFormat);
                }
                if (changeLine)
                {

                    yPos = y + (pengali * size.Height) + 3;

                }

                return yPos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return yPos;

            }
        }
        private float TulisItem(PdfGraphics graphics, string text = null, float fontSize = 10,
            float x = 0,
            float y = 0,
            float widthSpace = 10,
            PdfStringFormat stringFormat = null,
            bool changeLine = false, bool underline = false,
            bool bold = false)
        {
            float yPos = y;
            try
            {


                PdfFont lfont;
                text = text.Replace("\t", "").Replace("\r\n", "");
                FontStyle style;
                style = FontStyle.Regular;
                if (underline == true)
                    style = style | FontStyle.Underline;

                if (bold == true)
                    style = style | FontStyle.Bold;

                lfont = new PdfTrueTypeFont(new Font("Arial Narrow", fontSize, style));
         
                SizeF size = lfont.MeasureString(text);
                float height = size.Height;
                int pengali = CariPengali(size.Width, widthSpace);

                if (size.Width > widthSpace)
                {

                    height = size.Height * pengali;
                }

                //stringFormat.Alignment=PdfAlignmentStyle.
                RectangleF rect = new RectangleF(x, y, widthSpace, height);
              

                if (text == null)
                {
                    graphics.DrawString("  ", lfont, PdfBrushes.Black, rect, stringFormat);
                }
                else
                {
                    graphics.DrawString(text, lfont, PdfBrushes.Black, rect, stringFormat);
                }
                if (changeLine)
                {

                    yPos = y + (pengali * size.Height) + 3;

                }

                return yPos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return yPos;

            }
        }
        
        private int CariPengali(float x, float y){

            if (x > 15 * y)
                return 20;
            else
            {
                if (x > 14 * y)
                    return 19;
                else
                {
                    if (x > 13 * y)
                        return 17;
                    else
                    {
                        if (x > 12 * y)
                            return 15;
                        else
                        {
                            if (x > 11 * y)
                                return 13;
                            else
                            {
                                if (x > 10 * y)
                                    return 20;
                                else
                                {
                                    if (x > 9 * y)
                                        return 12;
                                    else
                                    {

                                        if (x > 8 * y)
                                            return 11;
                                        else
                                        {

                                            if (x > 7 * y)
                                                return 11;
                                            else
                                            {

                                                if (x > 5 * y)
                                                    return 9;
                                                else
                                                {
                                                    if (x > (4 * y))
                                                        return 7;
                                                    else
                                                    {
                                                        if (x > (3 * y))
                                                            return 5;
                                                        else
                                                        {
                                                            if (x > (2 * y))
                                                                return 4;
                                                            else
                                                            {
                                                                if (x > (1 * y))
                                                                    return 2;
                                                                else
                                                                    return 1;
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void cmdCetakSPP_Click(object sender, EventArgs e)
        {
            m_iJenisSubSPP = ctrlJenisBelanjaSPP1.GetID();
            if (ctrlJenisSPP1.GetID() == 3 && ctrlJenisBelanjaSPP1.GetID() == 0)
            {
                MessageBox.Show("Belum pilih Jenis Belanja SPP...");
                return;
            }

            if (GlobalVar.gListRedaksiSPP == null)
            {
                GlobalVar.gListRedaksiSPP= new List<RedaksiSPP>();

            }
            if (GlobalVar.gListRedaksiSPP.Count ==0){
                RedaksiSPPLogic oRedaksiLogic = new RedaksiSPPLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListRedaksiSPP=oRedaksiLogic.Get();
            }
            
            
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();

            
            
            ProcessHalaman1(ref document);
            //Create file stream.
            ProcessHalaman2(ref document);
            ProcessHalaman3(ref document);
            ProcessHalaman4(ref document);
            ProcessHalaman5(ref document);
            ProcessHalaman6(ref document);


            using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPP.pdf"), FileMode.Create, FileAccess.ReadWrite))
            {
                //Save the PDF document to file stream.
                document.Save(outputFileStream);
              
            }

            //Close the document.
            document.Close(true);
            pdfViewer pV = new pdfViewer();
            pV.Document = Path.GetFullPath(@"../../../SPP.pdf");
            pV.Show();
           

        }
        private void ProcessHalaman1Lama(ref PdfDocument document)
        {
            int rowx = 0;
            //Add a page.
            try
            {

                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                float yPos;
                // Cari Judul 
                SetJudul1();
                yPos = 10;

                rowx++;
                PdfFont font10;
                PdfFont font12;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;

                rowx++;



                font10 = new PdfTrueTypeFont(new Font("Arial", 10));
                font12 = new PdfTrueTypeFont(new Font("Arial", 12));
                //SizeF size = font12.MeasureString("xxx");
                yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true,false , true );
                yPos = TulisItem(graphics, m_sJudul1, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
                yPos = TulisItem(graphics, m_sJudul2, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
                rowx++;
                yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);

                yPos = yPos + 20;
                yPos = TulisItem(graphics, "SURAT PENGANTAR", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 3;
                m_stringFormatLeft.Alignment = PdfTextAlignment.Left;
                yPos = TulisItem(graphics, "Kepada Yth.", mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                rowx++;
                Pejabat pimpinan = new Pejabat();

                pimpinan = ctrlDinas1.GetPimpinan(dtSPP.Value);

                yPos = TulisItem(graphics, pimpinan.Jabatan, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                yPos = TulisItem(graphics, "SKPD " + ctrlDinas1.GetNamaSKPD(), mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                yPos = TulisItem(graphics, "di Tempat", mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);

                yPos = yPos + 10;
                RedaksiSPP redaksi = GlobalVar.gListRedaksiSPP.FirstOrDefault(r => r.Jenis == m_iJenisSPP &&
                                                      r.JenisLaporan == 1);

                rowx++;
                string text = redaksi.Redaksi;
                m_stringFormatJustify.Alignment= PdfTextAlignment.Justify;
               
                yPos  = TulisItem(graphics, text, mfont10, 10, yPos,
                    (page.GetClientSize().Width - 20), m_stringFormatJustify, true);

                float x0 = 10;
                float x1 = 40;
                float x2 = 190;
                float x3 = 195;
                //float x4 = 10;

                
                PdfGrid pdfGrid = new PdfGrid();
                List<object> data = new List<object>();
                
                pdfGrid.Columns.Add();
                pdfGrid.Columns.Add();
                pdfGrid.Columns.Add();
                pdfGrid.Columns.Add();

                rowx++;
                
                pdfGrid.Columns[0].Width = 30;
                rowx++;
                pdfGrid.Columns[1].Width = 150;
                rowx++;
                pdfGrid.Columns[2].Width = 5;
                rowx++;
                pdfGrid.Columns[3].Width = 290;
              

                rowx++;
                Object grid1row1 = new { No = "a.", Name = "Urusan Pemerintahan", Titikdua = ":", Nulai = "Keuangan Daerah" };
                Object grid1row2 = new { No = "b.", Name = "SKPD", Titikdua = ":", Nulai = m_IDDInas.ToKodeDinas() + "   " + ctrlDinas1.GetNamaSKPD() };
                Object grid1row3 = new { No = "c.", Name = "Tahun Anggaran", Titikdua = ":", Nulai = GlobalVar.TahunAnggaran.ToString() };
                SPD oSPD = new SPD();
                oSPD = ctrlSPD1.GetSPD();

                Object grid1row4 = new { No = "d.", Name = "Dasar Pengeluaran Nomor SPD", Titikdua = ":", Nulai = oSPD.NoSPD + " / " + oSPD.Tanggal.ToTanggalIndonesia() };
                Object grid1row5 = new { No = "e.", Name = "Jumlah SPD", Titikdua = ":", Nulai = oSPD.Jumlah.ToRupiahInReport() };
                Object grid1row6 = new { No = "", Name = " ", Titikdua = " ", Nulai = oSPD.Jumlah.Terbilang() };

                Object grid1row7 = new { No = "f.", Name = "Jumlah Pembayaran yang diminta", Titikdua = ":", Nulai = txtJumlah.Text };
                Object grid1row8 = new { No = "", Name = " ", Titikdua = " ", Nulai = txtJumlah.Text.FormatUangReportKeDecimal().Terbilang() };
                Object grid1row9 = new { No = "g.", Name = "Untuk keperluan", Titikdua = ":", Nulai = txtUraian.Text.ReplaceUnicode() };
                Object grid1row10 = new { No = "h.", Name = "Nama dan Nomor Rekening Bank", Titikdua = ":", Nulai = txtNamaPenerimadalamRekeningBank.Text + ", No Rek: " + txtNoRekening.Text + " , BANK:" + ctrlDaftarBank1.NamaBank };

                data.Add(grid1row1);

                rowx++;
                data.Add(grid1row2);
                data.Add(grid1row3);
                data.Add(grid1row4);
                data.Add(grid1row5);
                data.Add(grid1row6);
                data.Add(grid1row7);
                data.Add(grid1row8);
                data.Add(grid1row9);
                data.Add(grid1row10);

                rowx++;

                //Add list to IEnumerable.
                List<object> dataTable = data;
                //Declare and define the table cell style.            
                rowx++;

                //Assign data source.
                pdfGrid.DataSource = dataTable;

                rowx++;
                pdfGrid.Headers.Clear();

                //Set the border color for the table.
                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                rowx++;
                cellStyle.Borders.All = new PdfPen(Color.Transparent);
                cellStyle.Font = font10;
                //pdfGrid.Style.BorderOverlapStyle = new PdfPens(Color.Transparent);


                for (int i = 0; i < pdfGrid.Rows.Count; i++)
                {
                    //Get the row. 
                    PdfGridRow row = pdfGrid.Rows[i];

                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        //Get the cell. 
                        PdfGridCell cell = row.Cells[j];
                        //Apply the cell style. 
                        
                        if (cell != null)
                        cell.Style = cellStyle;
                    }
                }
                rowx++;
                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10, yPos));
                rowx++;
                Pejabat pptk = ctrlPPTK.GetPejabat();
                if (pptk == null)
                {
                    MessageBox.Show("Kesalahan mengambil data pptk");
                }
                rowx++;
                m_oBendahara = ctrlDinas1.GetBendaharaPengeluaran(dtSPP.Value);
                rowx++;
                yPos = pdfGridLayoutResult.Bounds.Bottom + 50;
                rowx++;
                float setengah = (page.GetClientSize().Width / 2)-20;
                rowx++;
                float posisiTengah = page.GetClientSize().Width / 2 + 10;
                rowx++;
                m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
                rowx++;
                yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10,posisiTengah , yPos,
                setengah, m_stringFormatCenter, true);
                rowx++;
                if (m_iJenisSPP >= 3 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Jabatan, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, m_oBendahara.Jabatan, mfont10, posisiTengah, yPos,
               setengah, m_stringFormatCenter, true);


                yPos = yPos + 30;

                if (m_iJenisSPP >= 2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Nama, mfont10, 10, yPos, setengah, m_stringFormatCenter, false,true );
                }
                yPos = TulisItem(graphics, m_oBendahara.Nama, mfont10, posisiTengah, yPos,setengah, m_stringFormatCenter, true, true);
                if (m_iJenisSPP >= 2 && pptk != null && pptk.ID >0 )
                {
                    yPos = TulisItem(graphics, "NIP " + pptk.NIP , mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, "NIP " +  m_oBendahara.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);
                rowx++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 1 pada "+ rowx.ToString() + System.Environment.NewLine + ex.Message);
            }
        }
        private void ProcessHalaman1(ref PdfDocument document)
        {
            int rowx = 0;
            //Add a page.
            try
            {

                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                float yPos;
                // Cari Judul 
                SetJudul1();
                yPos = 10;

                rowx++;
                PdfFont font10;
                PdfFont font12;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;

                rowx++;



                font10 = new PdfTrueTypeFont(new Font("Arial", 10));
                font12 = new PdfTrueTypeFont(new Font("Arial", 12));
                //SizeF size = font12.MeasureString("xxx");
                yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
                yPos = TulisItem(graphics, m_sJudul1, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
                yPos = TulisItem(graphics, m_sJudul2, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
                rowx++;
                yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);

                yPos = yPos + 20;
                yPos = TulisItem(graphics, "SURAT PENGANTAR", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 3;
                m_stringFormatLeft.Alignment = PdfTextAlignment.Left;
                yPos = TulisItem(graphics, "Kepada Yth.", mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                rowx++;
                Pejabat pimpinan = new Pejabat();

                pimpinan = ctrlDinas1.GetPimpinan(dtSPP.Value);

                yPos = TulisItem(graphics, pimpinan.Jabatan, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                yPos = TulisItem(graphics, "SKPD " + ctrlDinas1.GetNamaSKPD(), mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                yPos = TulisItem(graphics, "di Tempat", mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);

                yPos = yPos + 10;
                RedaksiSPP redaksi = GlobalVar.gListRedaksiSPP.FirstOrDefault(r => r.Jenis == m_iJenisSPP &&
                                                      r.JenisLaporan == 1);

                rowx++;
                string text = redaksi.Redaksi;
                m_stringFormatJustify.Alignment = PdfTextAlignment.Justify;

                yPos = TulisItem(graphics, text, mfont10, 10, yPos,
                    (page.GetClientSize().Width - 20), m_stringFormatJustify, true);

                float x0 = 10;
                float x1 = 40;
                float x2 = 190;
                float x3 = 195;
                //float x4 = 10;


                PdfGrid pdfGrid = new PdfGrid();
                List<object> data = new List<object>();

                pdfGrid.Columns.Add();
                pdfGrid.Columns.Add();
                pdfGrid.Columns.Add();
                pdfGrid.Columns.Add();

                rowx++;

                pdfGrid.Columns[0].Width = 30;
                rowx++;
                pdfGrid.Columns[1].Width = 150;
                rowx++;
                pdfGrid.Columns[2].Width = 5;
                rowx++;
                pdfGrid.Columns[3].Width = 290;


                rowx++;

                yPos = TulisItem(graphics, "a.",10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Urusan Pemerintahan", 10, x1, yPos, 150, m_stringFormatJustify, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                if (m_iJenisSPP >= 2)
                {
                    yPos = TulisItem(graphics, ctrlUrusanPemerintahan1.GetNama(), 10, x3, yPos, 290, m_stringFormatJustify, true);
                }
                else
                {
                    ctrlUrusanPemerintahan1.SetID(ctrlDinas1.IDUrusan());
                    yPos = TulisItem(graphics, ctrlUrusanPemerintahan1.GetNama(), 10, x3, yPos, 290, m_stringFormatLeft, true);
                }

                yPos = TulisItem(graphics, "b.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "S K P D", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, m_IDDInas.ToKodeDinas() + "   " + ctrlDinas1.GetNamaSKPD(), 10, x3, yPos, 290, m_stringFormatLeft, true);

                yPos = TulisItem(graphics, "c.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Tahun Anggaran", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, GlobalVar.TahunAnggaran.ToString(), 10, x3, yPos, 290, m_stringFormatLeft, true);
                SPD oSPD = new SPD();
                oSPD = ctrlSPD1.GetSPD();

                yPos = TulisItem(graphics, "d.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Dasar Pengeluaran Nomor SPD", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, oSPD.NoSPD + " / " + oSPD.Tanggal.ToTanggalIndonesia(), 10, x3, yPos, 290, m_stringFormatLeft, true);

                yPos = TulisItem(graphics, "e.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Jumlah SPD", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, oSPD.Jumlah.ToRupiahInReport(), 10, x3, yPos, 290, m_stringFormatLeft, true);

                //yPos = TulisItem(graphics, " ", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Jumlah SPD", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, oSPD.Jumlah.Terbilang(), 10, x3, yPos, 290, m_stringFormatLeft, true);

                yPos = TulisItem(graphics, "f.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Jumlah Pembayaran yang diminta", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, txtJumlah.Text, 10, x3, yPos, 290, m_stringFormatLeft, true);
                
                //yPos = TulisItem(graphics, "f.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                //yPos = TulisItem(graphics, "Jumlah Pembayaran yang diminta", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, txtJumlah.Text.FormatUangReportKeDecimal().Terbilang(), 10, x3, yPos, 290, m_stringFormatLeft, true);

                yPos = TulisItem(graphics, "g.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Untuk keperluan", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, txtUraian.Text.ReplaceUnicode(), 10, x3, yPos, 300, m_stringFormatLeft, true);

                yPos = TulisItem(graphics, "h.", 10, x0, yPos, 30, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Nama dan Nomor Rekening Bank", 10, x1, yPos, 150, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":", 10, x2, yPos, 5, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, txtNamaPenerimadalamRekeningBank.Text + "  , No Rek: " + txtNoRekening.Text + " , BANK:" + ctrlDaftarBank1.NamaBank + " " + ctrlDaftarBank1.KeteranganNamaBank  , 10, x3, yPos, 290, m_stringFormatLeft, true);
                
                rowx++;

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10, yPos));
                rowx++;
                Pejabat pptk = ctrlPPTK.GetPejabat();
                if (pptk == null)
                {
                    MessageBox.Show("Kesalahan mengambil data pptk");
                }
                rowx++;
                m_oBendahara = ctrlDinas1.GetBendaharaPengeluaran(dtSPP.Value);
                rowx++;
                yPos = pdfGridLayoutResult.Bounds.Bottom + 50;
                rowx++;
                float setengah = (page.GetClientSize().Width / 2) - 20;
                rowx++;
                float posisiTengah = page.GetClientSize().Width / 2 + 10;
                rowx++;
                m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
                rowx++;
                yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
                setengah, m_stringFormatCenter, true);
                rowx++;
                if (m_iJenisSPP >=2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Jabatan, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, m_oBendahara.Jabatan, mfont10, posisiTengah, yPos,
               setengah, m_stringFormatCenter, true);


                yPos = yPos + 30;

                if (m_iJenisSPP >=2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Nama, mfont10, 10, yPos, setengah, m_stringFormatCenter, false, true);
                }
                yPos = TulisItem(graphics, m_oBendahara.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
                if (m_iJenisSPP >=2 && pptk != null && pptk.ID > 0)
                {
                    yPos = TulisItem(graphics, "NIP " + pptk.NIP, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, "NIP " + m_oBendahara.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);
                rowx++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 1 pada " + rowx.ToString() + System.Environment.NewLine + ex.Message);
            }
        }
        private void SetJudul1(){
            switch (m_iJenisSPP)
            {
                case 0:
                    m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN UANG PERSEDIAAN ";
                    m_sJudul2 = "(SPP-UP)";
                    m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP UANG PERSEDIAAN";
                    //m_sJudul3 = "SPP UP";
                    break;

                case 1:
                    m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN GANTI UANG PERSEDIAAN ";
                    m_sJudul2 = "(SPP-GU)";
                    m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP GANTI UANG PERSEDIAAN";
                    //m_sJudul3 = "SPP UP";
                    break;

                case 2:
                    m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN TAMBAHAN UANG PERSEDIAAN ";
                    m_sJudul2 = "(SPP-TU)";
                    m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP TAMBAHAN UANG PERSEDIAAN";
                    //m_sJudul3 = "SPP TU";
                    break;

                case 3:
                
                    switch (m_iJenisSubSPP)
                    {
                        case 51:
                            //m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN LANGSUNG BELANJA PEGAWAI";
                            m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN BELANJA OPERASI";
                            m_sJudul2 = "(SPP-LS BELANJA OPERASI)";
                            m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA OPERASI";

                            break;
                        case 52:
                            m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN BELANJA MODAL";
                            m_sJudul2 = "(SPP-LS BELANJA MODAL)";
                            m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA MODAL";
                            break;
                        case 53:
                            m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN BELANJA  TAK TERDUGA";
                            m_sJudul2 = "(SPP-LS BELANJA TAK TERDUGA)";
                            m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS TAK TERDUGA";
                            break;
                        case 54:
                            m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN BELANJA  TRANSFER";
                            m_sJudul2 = "(SPP-LS BELANJA TRANSFER)";
                            m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS TRANSFER";
                            break;
                        case 62:                        
                            m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN PEMBIAYAAN";
                            m_sJudul2 = "(PEMBIAYAAN)";
                            m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS PEMBIAYAAN";
                            break;

                    }
                    break;
                case 4:
                    m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN LANGSUNG GAJI DAN TUNJANGAN(SPP-LS-GAJI-TUNJANGAN)";
                    m_sJudul2 = "(SPP-LS DAN GAJI TUNJANGAN)";
                    m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS GAJI DAN TUNJANGAN";

                     break;
                case 5:
                     m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN LANGSUNG PPKD";
                     m_sJudul2 = "(SPP-LS DAN PPKD)";
                     m_sJudul3 = "SURAT PERNYATAAN PENGAJUAN SPP LS PPKD";

                     break;
                     
                    //m_sJudul1 = "SURAT PERMINTAAN PEMBAYARAN TAMBAHAN UANG PERSEDIAAN ";
                    //m_sJudul2 = "(SPP-TU)";
                    //m_sJudul3 = "SPP TU";
                    //break;


            }

        }
        private void CetakRincian()
        { 
            int baris = 0;
            int barisLuar = 0;
            int nobku = 0;
            try
            {

                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                
                section1.PageSettings.Orientation = PdfPageOrientation.Portrait;
                //document.PageSettings.Margins.Bottom = 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;

                PdfGraphics graphics = page.Graphics;

                float yPos;
                yPos = 10;
                //document.Pages.PageAdded += Pages_PageAdded;
                //halaman = 1;
                SaatnyacetakKesimpulan = false;
                PdfFont font10;
                PdfFont font12;

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;

                
                 font10 = new PdfTrueTypeFont(new Font("Arial", 10));
                font12 = new PdfTrueTypeFont(new Font("Arial", 12));
                //SizeF size = font12.MeasureString("xxx");
                yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                SetJudul1();
                SetJudul3();
                yPos = TulisItem(graphics, m_sJudul1, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, m_sJudul2, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 20;

                PdfGrid pdfGrid = new PdfGrid();


                List<object> data = new List<object>();


                yPos = TulisItem(graphics, "RINCIAN RENCANA PENGGUNAAN", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 10;


                List<SPPRekening> lstdetail = ctrlRekeningKegiatan1.getDisplayRekening();
                int i = 0;
                float x0 = 10;
                float x1 = 40;
                float x2 = 170;
                float x3 = 420;
                float x4 = 500;
                float yPosAtas;
                PdfPen penLine = new PdfPen(PdfBrushes.Black, 0.5f);
                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);
                yPosAtas = yPos;
                yPos = TulisItem(graphics, "No", mfont10, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Kode Rekening", mfont10, x1, yPos, x2 - x1, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Jumlah", mfont10, x3, yPos, x4 - x3, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Nama", mfont10, x2, yPos, x3 - x2, m_stringFormatCenter, true);

                yPos = yPos + 2;
                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                

                foreach (SPPRekening sr in lstdetail)
                {



                       yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                       string sKode = "";
                       if (ctrlJenisBelanjaSPP1.GetID() != 62)
                       {
                                  sKode = sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening();
                       }
                       else
                       {
                           sKode = sr.IDRekening.ToKodeRekening();
                        }
                        yPos = TulisItem(graphics, sKode, mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);

                        yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                        yPos = TulisItem(graphics, sr.NamaRekening, mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                        graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                        if (yPos + 10 > page.GetClientSize().Height)
                        {
                            page = document.Pages.Add();
                            // yPos = 20;
                        }

                     
                    }
                    
                    //yPos = yPos + 2;
                    graphics.DrawLine(penLine, x1 - 1, yPosAtas, x1 - 1, yPos);
                    graphics.DrawLine(penLine, x2 - 1, yPosAtas, x2 - 1, yPos);

                    yPos = TulisItem(graphics, "JUMLAH", mfont8, x0 + 2, yPos, x3 - 3 - x0, m_stringFormatRight, false);
                    yPos = TulisItem(graphics, txtJumlahSPP.Text, mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);
                
                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);


                graphics.DrawLine(penLine, x0, yPosAtas, x0, yPos);
                graphics.DrawLine(penLine, x3 - 1, yPosAtas, x3 - 1, yPos);
                graphics.DrawLine(penLine, x4, yPosAtas, x4, yPos);



                yPos = yPos + 5;
                Pejabat pptk = ctrlPPTK.GetPejabat();
                float setengah = (page.GetClientSize().Width / 2) - 20;
                float posisiTengah = page.GetClientSize().Width / 2 + 10;
                m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
                yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
               setengah, m_stringFormatCenter, true);
                if (m_iJenisSPP >=2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Jabatan, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, m_oBendahara.Jabatan, mfont10, posisiTengah, yPos,
                 setengah, m_stringFormatCenter, true);



                yPos = yPos + 30;
                if (m_iJenisSPP >=2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Nama, mfont10, 10, yPos, setengah, m_stringFormatCenter, false, true);
                }
                yPos = TulisItem(graphics, m_oBendahara.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
                if (m_iJenisSPP >=2 && pptk != null && pptk.ID > 0)
                {
                    yPos = TulisItem(graphics, "NIP " + pptk.NIP, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, "NIP " + m_oBendahara.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);

            
                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../LampiranSPP.pdf"), FileMode.Create, FileAccess.ReadWrite))
               {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

            //    //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../LampiranSPP.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "   " + barisLuar.ToString() + " bku:  " + nobku.ToString() + "   " + baris.ToString());
            }

        }
        private void Pages_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            //stringFormat.Alignment = PdfTextAlignment.Right ;
            CetakPDF oCetakPDF = new CetakPDF();



            if (SaatnyacetakKesimpulan == true)
            {

            }



            previousPage = args.Page;
        }
        private void ProcessHalaman2(ref PdfDocument document)
        {
            try
            {
                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                float yPos;
                // Cari Judul 
                SetJudul1();
                yPos = 10;
                Single yPosAtasSPD = 10;
                PdfFont font10;
                PdfFont font12;
     
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                //stringFormat.CharacterSpacing = 2f;

                font10 = new PdfTrueTypeFont(new Font("Arial", 10));
                font12 = new PdfTrueTypeFont(new Font("Arial", 12));
                //SizeF size = font12.MeasureString("xxx");
                yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, m_sJudul1, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, m_sJudul2, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 20;

                PdfGrid pdfGrid = new PdfGrid();


                List<object> data = new List<object>();


                yPos = TulisItem(graphics, "RINCIAN RENCANA PENGGUNAAN", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 10;


                List<SPPRekening> lstdetail = ctrlRekeningKegiatan1.getDisplayRekening();
                int i = 0;
                float x0 = 10;
                float x1 = 40;
                float x2 = 170;
                float x3 = 420;
                float x4 = 500;
                float yPosAtas;
                PdfPen penLine = new PdfPen(PdfBrushes.Black, 0.5f);
                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);
                yPosAtas = yPos;
                yPos = TulisItem(graphics, "No", mfont10, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Kode Rekening", mfont10, x1, yPos, x2 - x1, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Jumlah", mfont10, x3, yPos, x4 - x3, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Nama", mfont10, x2, yPos, x3 - x2, m_stringFormatCenter, true);

                yPos = yPos + 2;
                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                if (m_iJenisSPP == 0)
                {
                    yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);

                    

                    yPos = TulisItem(graphics, "", mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, txtJumlah.Text, mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                    yPos = TulisItem(graphics, "Uang Persediaan", mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                    graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                    //yPos = yPos + 2;
                    graphics.DrawLine(penLine, x1 - 1, yPosAtas, x1 - 1, yPos);
                    graphics.DrawLine(penLine, x2 - 1, yPosAtas, x2 - 1, yPos);

                    yPos = TulisItem(graphics, "JUMLAH", mfont8, x0 + 2, yPos, x3 - 3 - x0, m_stringFormatRight, false);
                    yPos = TulisItem(graphics, txtJumlah.Text, mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);

                    
                }

                else
                {
                    if (m_iJenisSPP == 1)
                    {
                        SPPLogic osppLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                        List<SPPRekening> lst = osppLogic.GetRingkasanGU(m_NoUrut);
                        if (lst != null)
                        {
                            foreach (SPPRekening sr in lst)
                            {
                                if (sr.Jumlah != 0)
                                {


                                    yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);

                                    string sKode = sr.IDRekening.ToKodeRekening();

                                    yPos = TulisItem(graphics, sKode, mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                                    yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                                    yPos = TulisItem(graphics, sr.NamaRekening, mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                                    graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                                    if (yPos + 10 > page.GetClientSize().Height)
                                    {
                                        page = document.Pages.Add();
                                        // yPos = 20;
                                    }

                                }
                            }

                        }

                    }
                    else
                    {

                        foreach (SPPRekening sr in lstdetail)
                        {
                            if (sr.Jumlah > 0)
                            {


                                yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                                string sKode = "";
                                if (ctrlJenisBelanjaSPP1.GetID() != 62) { 
                                        sKode = sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening();
                                }
                                else {
                                        sKode =  sr.IDRekening.ToKodeRekening();
                                }
                                yPos = TulisItem(graphics, sKode, mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                                
                                yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                                yPos = TulisItem(graphics, sr.NamaRekening, mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                                if (yPos + 10 > page.GetClientSize().Height)
                                {
                                    page = document.Pages.Add();
                                    // yPos = 20;
                                }
                                if (yPos >= page.GetClientSize().Height - 40)
                                {
                                    graphics.DrawLine(penLine, x0 - 1, yPosAtas, x0 - 1, yPos);
                                    graphics.DrawLine(penLine, x4 - 1, yPosAtas, x4 - 1, yPos);
                                    graphics.DrawLine(penLine, x1 - 1, yPosAtas, x1 - 1, yPos);
                                    graphics.DrawLine(penLine, x2 - 1, yPosAtas, x2 - 1, yPos);
                                 

                                    page = document.Pages.Add();
                                    graphics = page.Graphics;
                                    yPos = 10;
                                    graphics.DrawLine(penLine, x0, yPos, x4, yPos);
                                    //yPosAtasSPD = 10;
                                    //yPosAtasSPD = 10;
                                    yPosAtas = 10;
                                    yPosAtas = 10;


                                }

                            }
                        }
                    }
                    //yPos = yPos + 2;
                    graphics.DrawLine(penLine, x1 - 1, yPosAtas, x1 - 1, yPos);
                    graphics.DrawLine(penLine, x2 - 1, yPosAtas, x2 - 1, yPos);

                    yPos = TulisItem(graphics, "JUMLAH", mfont8, x0 + 2, yPos, x3 - 3 - x0, m_stringFormatRight, false);
                    yPos = TulisItem(graphics, txtJumlahSPP.Text, mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);
                }
                 graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);


                graphics.DrawLine(penLine, x0, yPosAtas, x0, yPos);
                graphics.DrawLine(penLine, x3 - 1, yPosAtas, x3 - 1, yPos);
                graphics.DrawLine(penLine, x4, yPosAtas, x4, yPos);



                yPos = yPos + 5;
                Pejabat pptk = ctrlPPTK.GetPejabat();
                float setengah = (page.GetClientSize().Width / 2) - 20;
                float posisiTengah = page.GetClientSize().Width / 2 + 10;
                m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
                yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
               setengah, m_stringFormatCenter, true);
                if (m_iJenisSPP >=2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Jabatan, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, m_oBendahara.Jabatan, mfont10, posisiTengah, yPos,
                 setengah, m_stringFormatCenter, true);



                yPos = yPos + 30;
                if (m_iJenisSPP >=2 && pptk != null)
                {
                    yPos = TulisItem(graphics, pptk.Nama, mfont10, 10, yPos, setengah, m_stringFormatCenter, false, true);
                }
                yPos = TulisItem(graphics, m_oBendahara.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
                if (m_iJenisSPP >=2 && pptk != null && pptk.ID > 0)
                {
                    yPos = TulisItem(graphics, "NIP " + pptk.NIP, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
                }
                yPos = TulisItem(graphics, "NIP " + m_oBendahara.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 2" + System.Environment.NewLine + ex.Message);
            }


        }
        private void ProcessHalaman3(ref PdfDocument document)
        {
            int i = 0;
            try { 
            PdfPage page = document.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            PdfGraphics graphics = page.Graphics;

            float yPos;
            i = 1;
            List<object> data = new List<object>();

            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Right;
            stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            stringFormat.CharacterSpacing = 2f;
            yPos = 10;
            i = 2;
            //SizeF size = font12.MeasureString("xxx");
            yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
           // yPos = TulisItem(graphics, m_sJudul1, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
         
            SetJudul3();
            i = 3;
            yPos = yPos + 30;
            yPos = TulisItem(graphics, "SURAT PERNYATAAN PENGAJUAN " + m_sJudul3.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
            yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);


            List<RedaksiSPP> lstRedaksi = GlobalVar.gListRedaksiSPP.FindAll(x => x.Jenis == m_iJenisSPP
                                        && x.JenisLaporan == (int)EnumJenisRedaksi.CETAKAN_PERNYATAAN);

            i = 4;
            IEnumerable<RedaksiSPP> query = lstRedaksi.OrderBy(xo => xo.No);



            foreach (RedaksiSPP red in query)
            {
                string textRedaksi = red.Redaksi;
                if (red.No == 0)
                {
                    textRedaksi =textRedaksi.Replace(System.Environment.NewLine, "");
                    textRedaksi =textRedaksi.Replace("#JUMLAH#", txtJumlah.Text  );
                    textRedaksi =textRedaksi.Replace("#TERBILANG#", txtJumlah.Text.FormatUangReportKeDecimal().Terbilang());
                    textRedaksi =textRedaksi.Replace("#NAMASKPD#", ctrlDinas1.GetNamaSKPD());
                    textRedaksi =textRedaksi.Replace("#NOMORSPP#", txtNoSPP.Text );
                    textRedaksi =textRedaksi.Replace("#TAHUN#", GlobalVar.TahunAnggaran.ToString());
                    textRedaksi = textRedaksi.Replace("#TANGGALSPP#", dtSPP.Value.ToTanggalIndonesia());
                    yPos = TulisItem(graphics, textRedaksi, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatJustify, true);
                    yPos = yPos + 5;
                }
                else
                {
                    yPos = TulisItem(graphics, red.No.ToString(), mfont12, 10, yPos, 20, m_stringFormatJustify, false );
                    yPos = TulisItem(graphics, textRedaksi, mfont10, 20, yPos, (page.GetClientSize().Width - 30), m_stringFormatJustify, true);
                    yPos = yPos + 5;



                }


            
            }
            i = 5;
            yPos = TulisItem(graphics, m_sJudul2, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatJustify, true);
            yPos = yPos + 5;


            float setengah = (page.GetClientSize().Width / 2) - 20;
            float posisiTengah = page.GetClientSize().Width / 2 + 10;
            m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
            yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
           setengah, m_stringFormatCenter, true);
            Pejabat pimpinan = new Pejabat();
            i = 6;
            pimpinan = ctrlDinas1.GetPimpinan(dtSPP.Value);


            if (pimpinan == null)
            {
                MessageBox.Show("Belum Setting pimpinan/kepala dinas");
                return;
            }
            i = 7;
            yPos = TulisItem(graphics, pimpinan.Jabatan, mfont10, posisiTengah, yPos,
             setengah, m_stringFormatCenter, true);




            yPos = yPos + 30;

            yPos = TulisItem(graphics, pimpinan.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
            yPos = TulisItem(graphics, "NIP "+ pimpinan.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 3-> " + i.ToString() + System.Environment.NewLine + ex.Message);
            }

        }
        private void ProcessHalaman4(ref PdfDocument document)
        {
            try
            {
                PdfPage page = document.Pages.Add();
                PdfGrid pdfGrid = new PdfGrid();
                PdfGraphics graphics = page.Graphics;

                float yPos;

                List<object> data = new List<object>();

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Right;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                stringFormat.CharacterSpacing = 2f;
                yPos = 10;

                //SizeF size = font12.MeasureString("xxx");
                yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD(), mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);


                yPos = yPos + 30;
                yPos = TulisItem(graphics, "SURAT PERNYATAAN TANGGUNG JAWAB ", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                string sx;
                sx = "";
                switch (m_iJenisSPP)
                {
                    case 0:
                        sx = " Pembayaran Uang Persediaan (UP)";
                        break;
                    case 1:
                        sx = " Pembayaran Ganti Uang Persediaan (GU)";
                        break;
                    case 2:
                        sx = " Pembayaran Tambahan Uang Persediaan (TU)";
                        break;
                    case 3:
                        sx = " Pembayaran Langsung (LS)";
                        break;
                    case 4:
                        sx = " Pembayaran Langsung (LS)";
                        break;
                }
                string sText;

                if (ctrlDinas1.GetNamaSKPD().Contains("BADAN PENGELOLA") == true)
                {
                    sText = "Yang bertanda tangan di bawah ini, KUASA PENGGUNA ANGGARAN " + ctrlDinas1.GetNamaSKPD().ToUpper() + " menyatakan bahwa seluruh dokumen " +
                         " / bukti pendukung persyaratan untuk " + sx + " keperluan " + ctrlDinas1.GetNamaSKPD() + " Tahun Anggaran  " + GlobalVar.TahunAnggaran.ToString() +
                         ", sebagaimana terlampir adalah benar, dan merupakan tanggung jawab kami selaku KUASA PENGGUNA ANGGARAN";

                }
                else
                {
                    if (m_iKodeUk > 1)
                        sText = "Yang bertanda tangan di bawah ini, KUASA PENGGUNA ANGGARAN " + ctrlDinas1.GetNamaSKPD().ToUpper() + " menyatakan bahwa seluruh dokumen " +
                             " / bukti pendukung persyaratan untuk " + sx + " keperluan " + ctrlDinas1.GetNamaSKPD().ToUpper() + " Tahun Anggaran  " + GlobalVar.TahunAnggaran.ToString() + ", sebagaimana terlampir adalah benar, dan merupakan tanggung jawab kami selaku KUASA PENGGUNA ANGGARAN";

                    else

                        sText = "Yang bertanda tangan di bawah ini, PENGGUNA ANGGARAN " + ctrlDinas1.GetNamaSKPD().ToUpper() + " menyatakan bahwa seluruh dokumen " +
                            " / bukti pendukung persyaratan untuk " + sx + " keperluan " + ctrlDinas1.GetNamaSKPD().ToUpper() + " Tahun Anggaran  " + GlobalVar.TahunAnggaran.ToString() + ", sebagaimana terlampir adalah benar, dan merupakan tanggung jawab kami selaku PENGGUNA ANGGARAN";

                }


                yPos = TulisItem(graphics, sText, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatJustify, true);
                yPos = yPos + 5;

                sText = "Demikian Surat Pernyataan tanggung jawab ini dibuat untuk dapat digunakan sebagaimana mestinya";
                yPos = TulisItem(graphics, sText, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatJustify, true);
                yPos = yPos + 5;






                float setengah = (page.GetClientSize().Width / 2) - 20;
                float posisiTengah = page.GetClientSize().Width / 2 + 10;
                Pejabat pimpinan = new Pejabat();
                pimpinan = ctrlDinas1.GetPimpinan(dtSPP.Value);
                m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
                yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
               setengah, m_stringFormatCenter, true);
                
                yPos = TulisItem(graphics, pimpinan.Jabatan, mfont10, posisiTengah, yPos,
                 setengah, m_stringFormatCenter, true);




                yPos = yPos + 30;

                yPos = TulisItem(graphics, pimpinan.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
                yPos = TulisItem(graphics, "NIP " + pimpinan.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 4" + System.Environment.NewLine + ex.Message);
            }


        }

        private void ProcessHalaman5(ref PdfDocument document)
        {
            try { 
            PdfPage page = document.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            PdfGraphics graphics = page.Graphics;
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
            float yPos;

            List<object> data = new List<object>();

            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Right;
            stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            stringFormat.CharacterSpacing = 2f;
            yPos = 10;
                string namaDinas = ctrlDinas1.GetNamaSKPD();
            //SizeF size = font12.MeasureString("xxx");
            yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
            yPos = TulisItem(graphics, m_sJudul1, mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
             //  yPos = TulisItem(graphics, m_sJudul3, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
            yPos = TulisItem(graphics, "Nomor: " + txtNoSPP.Text, mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);

            yPos = yPos + 20;
            yPos = TulisItem(graphics, "RINGKASAN", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
            yPos = yPos + 3;
            int tahapAnggaran = ctrlDinas1.GetTahapAnggaran();
            decimal JumlahDPA=0L;
            decimal JumlahSPD = 0L;

            if (m_iJenisSPP == 0)
            {
                string textUP = "Berdasarkan surat Keputusan Bupati Ketapang nomor 113/BPKAD-C/2025 tgl  14 Februari 2025 tentang Uang Persediaan Satuan Kerja Perangkat Daerah dilingkungan Pemerintah Kabupaten Ketapang tahun anggaran 2025, dan Keputusan Bupati Ketapang Nomor : 114/BPKAD-C/2025 tgl 14 Februari 2025 tentang uang persediaan satuan kerja perangkat daerah di lingkungan pemerintah kabupaten ketapang dalam rangka implementasi kartu kredit pemerintah daerah tahun anggaran 2025" +
                     namaDinas + " sejumlah Rp. " + txtJumlah.Text + " (" + DataFormat.FormatUangReportKeDecimal(txtJumlah.Text).Terbilang() + ")";

                yPos = TulisItem(graphics, textUP, mfont10, 10, yPos, (page.GetClientSize().Width - 20),
                      m_stringFormatJustify, true);





            }
            else
            {
                switch (tahapAnggaran)
                {
                    case 2:
                        JumlahDPA = GlobalVar.gListRekeningAnggaran.Where(x => x.IDDinas == m_IDDInas && x.Jenis == 3).Sum(x => x.JumlahMurni);
                        break;
                    case 3:
                        JumlahDPA = GlobalVar.gListRekeningAnggaran.Where(x => x.IDDinas == m_IDDInas && x.Jenis == 3).Sum(x => x.JumlahPergeseran);
                        break;
                    case 4:
                        JumlahDPA = GlobalVar.gListRekeningAnggaran.Where(x => x.IDDinas == m_IDDInas && x.Jenis == 3).Sum(x => x.JumlahRKAP);
                        break;
                    case 5:
                        JumlahDPA = GlobalVar.gListRekeningAnggaran.Where(x => x.IDDinas == m_IDDInas && x.Jenis == 3).Sum(x => x.JumlahABT);
                        break;

                }

                float x0 = 10;
                float x1 = 40;
                float x2 = 170;
                float x3 = 420;
                float x4 = 500;
                float yPosAtas;
                float yPosAtasSPD;


                yPosAtas = yPos;
                graphics.DrawLine(pen, x0, yPos , x4, yPos);
                if (m_iJenisSPP==3 && m_oSPP.Penerima>0 && ctrlKontrak1.GetID()>0)
                {
                    yPos = yPos + 10;
                    string[] sBentukPerusahaan = { "CV", "PT", "Lainnya" };
                    float posisikiri =15;
                    float posisititi2 = 155;
                    float lebarNamaItem = 160;
                    float posisiKode = 160;
                    float posisiIsi = 180;
                    float panjangIsi = x4- 180 - 10;
                    yPos = TulisItem(graphics, " 1. Program ", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ctrlProgram1.GetID().ToKodeProgram(true).ToString(), 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ctrlProgram1.GetNamaProgram().Replace (".",""), 10, posisiIsi, yPos, panjangIsi, m_stringFormatLeft, true);

                    yPos = TulisItem(graphics, " 2. Kegiatan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ctrlKegiatanAPBD1.GetID().ToKodeKegiatan(true ), 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ctrlKegiatanAPBD1.GetNamaKegiatan().Replace(".", ""), 10, posisiIsi, yPos, panjangIsi, m_stringFormatLeft, true);

                    yPos = TulisItem(graphics, " 3. Sub Kegiatan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ctrlSubKegiatan1.GetID().ToKodeSubKegiatan(true), 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ctrlSubKegiatan1.GetNamaSubKegiatan().Replace(".", ""), 10, posisiIsi, yPos, panjangIsi, m_stringFormatLeft, true);

                    yPos = TulisItem(graphics, " 4. Nomor Tanggal DPA/DPPA", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                    //yPos = TulisItem(graphics, "", 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, "5.2." + ctrlSubKegiatan1.GetID().ToKodeSubKegiatan(), 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                    Perusahaan p = ctrlPerusahaan1.GetPerusahaan();
                    if (p!= null){

                        yPos = TulisItem(graphics, " 5. Nama Perusahaan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, p.NamaPerusahaan, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);

                        yPos = TulisItem(graphics, " 6. Bentuk Perusahaan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, sBentukPerusahaan[p.Bentuk], 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);

                        yPos = TulisItem(graphics, " 7. Alamat Perusahaan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, p.Alamat, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);


                        yPos = TulisItem(graphics, " 8. Nama dan Pimpinan Perusahaan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, p.Pimpinan, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);


                        yPos = TulisItem(graphics, " 9. Nama dan No.Rekening Bank ", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, p.NamaDalamRekeningBank + " " + p.Rekening, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);

                        Kontrak oKontrak = ctrlKontrak1.GetKontrak();
                        if (oKontrak != null)
                        {
                            yPos = TulisItem(graphics, "10. Nomor Kontrak", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                            yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                            yPos = TulisItem(graphics, oKontrak.NoKontrak, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                        }
                        else
                        {
                            yPos = TulisItem(graphics, "10. Nomor Kontrak", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                            yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                            yPos = TulisItem(graphics, " - ", 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                        }
                        yPos = TulisItem(graphics, "11. Kegiatan Lanjutan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, m_oSPP.SifatKegiatan==1?"Ya": "Tidak" , 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);


                        yPos = TulisItem(graphics, "12. Waktu Pelaksanaan Kegiatan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        if (oKontrak != null)
                        {
                            yPos = TulisItem(graphics, oKontrak.WaktuPelaksanaan, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                        }
                        else
                        {
                            yPos = TulisItem(graphics, m_oSPP.WaktuPelaksanaan, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                        }
                        yPos = TulisItem(graphics, "13. Deskripsi Pekerjaan", 10, posisikiri, yPos, lebarNamaItem, m_stringFormatLeft, false);
                        yPos = TulisItem(graphics, ":", 10, posisititi2, yPos, 40, m_stringFormatLeft, false);
                        if (oKontrak != null)
                        {
                            yPos = TulisItem(graphics, oKontrak.Uraian, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                        }
                        else
                        {
                            yPos = TulisItem(graphics, m_oSPP.Keterangan, 10, posisiKode, yPos, panjangIsi, m_stringFormatLeft, true);
                        }

                       

                    }



                    yPos = yPos + 10;
                   


                }


                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
      
                
                yPos = TulisItem(graphics, "JUMLAH DPA-SKPD/DPPA-SKPD/DPAL-SKPD", mfont8, x0, yPos, x4 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, JumlahDPA.ToRupiahInReport(), mfont8, x3, yPos, x4 - x3 - 2, m_stringFormatRight, true);

                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "RINGKASAN SPD", mfont8, x0, yPos, x4 - x0, m_stringFormatLeft, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPosAtasSPD = yPos;


                List<SPD> lstSPD = new List<SPD>();
                lstSPD = ctrlSPD1.GetListSPDBefore();

                yPos = TulisItem(graphics, "No", mfont10, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Nomor SPD", mfont10, x1, yPos, x2 - x1, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Tanggal SPD", mfont10, x2, yPos, x3 - x2, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "Jumlah", mfont10, x3, yPos, x4 - x3, m_stringFormatCenter, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);

                int i = 0;
                foreach (SPD ospd in lstSPD)
                {

                    yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                    yPos = TulisItem(graphics, ospd.NoSPD, mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ospd.Tanggal.ToTanggalIndonesia(), mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, ospd.Jumlah.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);

                    graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                    JumlahSPD = JumlahSPD + ospd.Jumlah;
                    if (yPos >= page.GetClientSize().Height - 40)
                    {

                        graphics.DrawLine(pen, x1, yPosAtasSPD, x1, yPos);
                        graphics.DrawLine(pen, x2, yPosAtasSPD, x2, yPos);
                        graphics.DrawLine(pen, x0, yPosAtas, x0, yPos - 1);
                        graphics.DrawLine(pen, x4, yPosAtas, x4, yPos - 1);


                        page = document.Pages.Add();
                        graphics = page.Graphics;
                        yPos = 10;
                        graphics.DrawLine(pen, x0, yPos, x4, yPos);
                        yPosAtasSPD = 10 ;
                        yPosAtasSPD = 10;
                        yPosAtas = 10;
                        yPosAtas = 10;


                    }


                }
                
                graphics.DrawLine(pen, x1, yPosAtasSPD, x1, yPos);
                graphics.DrawLine(pen, x2, yPosAtasSPD, x2, yPos);
                //page = document.Pages.Add();
                //graphics = page.Graphics;
                //yPos = 20;

                yPos = TulisItem(graphics, "", mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "", mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "JUMLAH", mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, JumlahSPD.ToRupiahInReport(), mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);


                if (yPos >= page.GetClientSize().Height -190)
                {

                    graphics.DrawLine(pen, x1, yPosAtasSPD, x1, yPos);
                    graphics.DrawLine(pen, x2, yPosAtasSPD, x2, yPos);
                    graphics.DrawLine(pen, x0, yPosAtas, x0, yPos );
                    graphics.DrawLine(pen, x4, yPosAtas, x4, yPos);
                    graphics.DrawLine(pen, x0, yPos, x4, yPos);
                        

                    page = document.Pages.Add();
                    graphics = page.Graphics;
                    yPos = 10;
                    graphics.DrawLine(pen, x0, yPos, x4, yPos);
                    yPosAtasSPD = 10;
                    yPosAtasSPD = 10;
                    yPosAtas = 10;
                    yPosAtas = 10;


                }



                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "", mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
                yPos = TulisItem(graphics, "", mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "Sisa Dana yang Belum di SPD kan", mfont8, x1, yPos, x3 - x1, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, (JumlahDPA - JumlahSPD).ToRupiahInReport(), mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);

                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "RINKASAN BELANJA", mfont8, x1, yPos, x3 - x1, m_stringFormatLeft, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                
                GlobalVar.gListSPP = new List<SPP>();
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                ParameterBendahara pb = new ParameterBendahara(GlobalVar.TahunAnggaran);
                pb.IDDInas = m_IDDInas;
                pb.Jenis = -1;
                pb.Status = -1;
                
                GlobalVar.gListSPP = oLogic.Get(pb);
                
                List<SPP> lstSPP = GlobalVar.gListSPP.FindAll(x => x.IDDInas == m_IDDInas && x.NoUrut <= m_NoUrut);
                

                var ringkasanbelanja = lstSPP.Where(s=>s.NoUrutSPD<= m_iNoUrutSPD ).GroupBy(x => x.Jenis)
                                       .Select(t => new
                               {
                                   Jenis = t.Key,
                                   jumlah = t.Sum(ta => ta.Jumlah),
                               }).ToList();

                decimal jumlahUPGU = 0L;
                decimal jumlahTU = 0L;
                decimal jumlahLS = 0L;
                decimal jumlahGJ = 0L;
                foreach (var o in ringkasanbelanja)
                {
                    switch (o.Jenis)
                    {
                      //  case 0:
                        case 1:
                            jumlahUPGU = jumlahUPGU + o.jumlah;
                            break;
                        case 2:
                            jumlahTU = jumlahTU + o.jumlah;
                            break;
                        case 3:
                            jumlahLS = jumlahLS + o.jumlah;
                            break;
                        case 4:
                            jumlahGJ = jumlahGJ + o.jumlah;
                            break;


                    }

                }

                yPos = TulisItem(graphics, "Belanja UP/GU", mfont8, x0 + 4, yPos, x2 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, jumlahUPGU.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "Belanja TU", mfont8, x0 + 4, yPos, x2 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, jumlahTU.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "Belanja LS Gaji dan Tunjangan ", mfont8, x0 + 4, yPos, x2 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, jumlahGJ.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "Belanja LS Barang dan Jasa", mfont8, x0 + 4, yPos, x2 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, jumlahLS.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "Belanja LS PPKD", mfont8, x0 + 4, yPos, x2 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "0", mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "JUMLAH", mfont8, x0 + 4, yPos, x2 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, (jumlahUPGU + jumlahTU + jumlahGJ + jumlahLS).ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos - 1, x4, yPos - 1);
                yPos = TulisItem(graphics, "Sisa SPD yang telah diterbitkan, belum dibelanjakan ", mfont8, x0 + 4, yPos, x3 - x0, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ((JumlahSPD) - (jumlahUPGU + jumlahTU + jumlahGJ + jumlahLS)).ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, true);
                graphics.DrawLine(pen, x0, yPos, x4, yPos);
                graphics.DrawLine(pen, x0, yPosAtas, x0, yPos - 1);
                graphics.DrawLine(pen, x4, yPosAtas, x4, yPos - 1);
                graphics.DrawLine(pen, x3, yPosAtasSPD, x3, yPos);




            }





            float setengah = (page.GetClientSize().Width / 2) - 20;
            float posisiTengah = page.GetClientSize().Width / 2 + 10;
            m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
            yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
           setengah, m_stringFormatCenter, true);
            Pejabat pptk = ctrlPPTK.GetPejabat();
            if (m_iJenisSPP >=2 && pptk != null)
            {
                yPos = TulisItem(graphics, pptk.Jabatan, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
            }

            yPos = TulisItem(graphics, m_oBendahara.Jabatan, mfont10, posisiTengah, yPos,
             setengah, m_stringFormatCenter, true);




            yPos = yPos + 30;
            if (m_iJenisSPP >=2 && pptk != null)
            {
                yPos = TulisItem(graphics, pptk.Nama, mfont10, 10, yPos, setengah, m_stringFormatCenter, false, true);
            }
            yPos = TulisItem(graphics, m_oBendahara.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
            if (m_iJenisSPP >=2 && pptk != null && pptk.ID > 0)
            {
                yPos = TulisItem(graphics, "NIP " + pptk.NIP, mfont10, 10, yPos, setengah, m_stringFormatCenter, false);
            }
                yPos = TulisItem(graphics, "NIP " +  m_oBendahara.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 5" + System.Environment.NewLine + ex.Message);
            }



        }
        private void ProcessHalaman6(ref PdfDocument document)
        {
            try
            {
                PdfPage page = document.Pages.Add();
                PdfGrid pdfGrid = new PdfGrid();
                PdfGraphics graphics = page.Graphics;
                PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
                float yPos;

                List<object> data = new List<object>();

                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Right;
                stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                stringFormat.CharacterSpacing = 2f;
                yPos = 10;

                FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
                //Draw the image
                PdfBitmap image = new PdfBitmap(imageStream);
                //Draw the image
                PointF p = new PointF(10 + 5, 10);
                Size size = new Size(30, 40);

                graphics.DrawImage(image, p, size);

                yPos = yPos + 20;

                //SizeF size = font12.MeasureString("xxx");
                yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = TulisItem(graphics, "PENELITIAN KELENGKAPAN DOKUMEN SPP", mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true);
                yPos = yPos + 20;
                yPos = TulisItem(graphics, "Nomor SPP ", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":" + txtNoSPP.Text, mfont12, 100, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);

                yPos = TulisItem(graphics, "Tanggal SPP ", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":" + dtSPP.Value.ToTanggalIndonesia(), mfont12, 100, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);

                List<KelengkapanSPM> lst = new List<KelengkapanSPM>();
                KelengkapanSPMLogic oLogic = new KelengkapanSPMLogic(GlobalVar.TahunAnggaran);
                yPos = yPos + 30;
                lst = oLogic.GetByJenisSPP(m_iJenisSPP);
                if (lst != null)
                {
                    foreach (KelengkapanSPM kSPP in lst)
                    {
                        float awalPos = yPos;

                        yPos = TulisItem(graphics, kSPP.Uraian, mfont12, 50, yPos, (page.GetClientSize().Width - 70), m_stringFormatJustify, true);

                        float tinggi = yPos - awalPos;
                        float titik = awalPos + tinggi / 2;


                        Rectangle rect = new Rectangle(10, (int)titik - 10, 20, 20);
                        // yPos = yPos - 5;
                        graphics.DrawRectangle(pen, rect);
                        yPos = yPos + 30;


                    }
                }
                yPos = yPos + 30;
                yPos = TulisItem(graphics, "PENELITIAN KELENGKAPAN SPP", mfont12, 10, yPos, (page.GetClientSize().Width - 50), m_stringFormatLeft, true);
       
                yPos = yPos + 30;
                Pejabat ppk = new Pejabat();
                ppk = ctrlDinas1.GetPPK(dtSPP.Value);
                yPos = TulisItem(graphics, "Nama ", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":" + ppk.Nama, mfont12, 90, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);
                yPos = TulisItem(graphics, "NIP ", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, false);
                yPos = TulisItem(graphics, ":" + ppk.NIP, mfont12, 90, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);

                yPos = yPos + 20;
                yPos = TulisItem(graphics, "Tanda Tangan", mfont12, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, false);
                yPos = TulisItem(graphics, "........................", mfont12, 40, yPos, (page.GetClientSize().Width - 20), m_stringFormatLeft, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cetak SPP Halaman 6" + System.Environment.NewLine + ex.Message);
            }
        }

        private void SetJudul3()
        {

            m_sJudul1 = "";
            m_sJudul2 = "";
            switch(m_iJenisSPP){ 
                case 0 :
                m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP UP";
                m_sJudul2 = "Demikian Surat Pernyataan ini dibuat untuk melengkapi persyaratan pengajuan SPM UP SKPD Kami";
                    break;

                case 1:
                m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP GU";
                m_sJudul2 = "Demikian Surat Pernyataan ini dibuat untuk melengkapi persyaratan pengajuan SPP GU";
                    break;
            case 2:
                m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP TU";
                m_sJudul2 = "Demikian Surat Pernyataan ini dibuat untuk melengkapi persyaratan pengajuan SPP TU";
                    break;
           case  3:
                    if (m_iJenisSubSPP==51 ) //m_oSPP.JenisGaji = 0 Then
                
                        m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA OPERASI";


                    if (m_iJenisSubSPP == 52)
                        m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA MODAL";


                    if (m_iJenisSubSPP == 53)
                        m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA TAK TERDUGA";


                    if (m_iJenisSubSPP == 54)
                        m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA TRANSFER";
                    if (m_iJenisSubSPP == 62)
                        m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS PEMBIAYAAN";


                    m_sJudul2 = "Demikian Surat Pernyataan ini dibuat untuk melengkapi persyaratan pengajuan SPP LS-SKPD";
                    
                    break;     
                case  4:
                    m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS GAJI DAN TUNJANGAN";
                    m_sJudul2 = "Demikian Surat Pernyataan ini dibuat untuk melengkapi persyaratan pengajuan SPP LS-SKPD";
                    break;
                case  5:
                    m_sJudul1 = "SURAT PERNYATAAN PENGAJUAN SPP LS PPKD";
                    m_sJudul2 = "Demikian Surat Pernyataan ini dibuat untuk melengkapi persyaratan pengajuan SPP LS-PPKD";
                    break;

            }

        }
   private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            m_IDUrusan=pID;
            if (m_iStatus==0 )
            {
        
                ctrlProgram1.Create(GlobalVar.TahunAnggaran, m_IDDInas, pID);
            }
        }

        private void ctrlProgram1_OnChanged(int pID)
        {
            m_IDProgram =pID;
            if (m_iStatus ==0 )
            {
                ctrlKegiatanAPBD1.CreateWIthUK(GlobalVar.TahunAnggaran, m_IDDInas, m_iKodeUk, m_IDProgram);
                   
            }
            
        }

        private void ctrlKegiatanAPBD1_OnChanged(int pID)
        {
            m_IDKegiatan = pID;
            if (m_iStatus==0)
            {
                
                    //
               //ctrlSubKegiatan1.CreateFromBidangAnggaran(GlobalVar.TahunAnggaran, m_IDDInas, m_iKodeUk, m_IDKegiatan, m_iUnitAnggaran);
               ctrlSubKegiatan1.CreateWithUK(m_oSPP.Tahun, m_IDDInas, m_iUnitAnggaran, m_IDKegiatan, m_oSPP.Rekenings);

               
            }

        }

        private void ctrlSubKegiatan1_OnChanged(long pID)
        {

            ctrlRekeningKegiatan1.Dinas = m_IDDInas;
            m_IDSubKegiatan = pID;
            if (m_iStatus==0)
            {
                if (chkBanyakKegiatan.Checked == false)
                {
                    ctrlRekeningKegiatan1.Clear();
                    ctrlRekeningKegiatan1.CreateNewSPP(
                        m_IDDInas,
                        m_IDProgram,
                        m_IDKegiatan,
                        m_IDSubKegiatan,
                        m_iNoUrutSPD,
                        dtSPP.Value,
                        m_iJenisSPP,
                         m_iUnitAnggaran
                        );
                }
                else
                {
                    ctrlPilihanRekeningAnggaran1.Create(m_IDDInas,m_IDUrusan,
                        m_IDProgram,
                        m_IDKegiatan,
                        m_IDSubKegiatan,
                        m_iUnitAnggaran, ctrlDinas1.GetTahapAnggaran(), 3);

                }

               

            }

        }

        private void ctrlSubKegiatan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlKegiatanAPBD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlProgram1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPotongan1_OnChanged(decimal pJumlah)
        {
            txtJumlahMpn.Text = pJumlah.ToRupiahInReport();
        }

        private void ctrlPotongan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPotongan2_OnChanged(decimal pJumlah)
        {

            txtJumlahNonMpn.Text = pJumlah.ToRupiahInReport();
        }

        private void ctrlPerusahaan1_OnFinishPencarian(Perusahaan perusahaan)
        {
            rbPihakIII.Checked = true;
            txtNamaPenerima.Text = perusahaan.Pimpinan;
            txtJabatanPenerima.Text = "";
            txtNamaPenerimadalamRekeningBank.Text = perusahaan.NamaDalamRekeningBank;
            ctrlDaftarBank1.SetKode(perusahaan.KodeBank, perusahaan.Rekening);
            txtNoRekening.Text = perusahaan.Rekening;
            txtNoNPWP.Text = perusahaan.NPWP;
            ctrlDaftarBank1.KeteranganNamaBank= perusahaan.KeteranganNamaBank;


        }

        private void ctrlPerusahaan1_OnSavingPerusahaan(Perusahaan perusahaan)
        {
            rbPihakIII.Checked = true;
            txtNamaPenerima.Text = perusahaan.Pimpinan;
            txtJabatanPenerima.Text = "";
            txtNamaPenerimadalamRekeningBank.Text = perusahaan.NamaDalamRekeningBank;
            ctrlDaftarBank1.SetKode(perusahaan.KodeBank, perusahaan.Rekening);
            txtNoRekening.Text = perusahaan.Rekening;
            txtNoNPWP.Text = perusahaan.NPWP;
            ctrlDaftarBank1.KeteranganNamaBank = perusahaan.KeteranganNamaBank;
        }

        

        private void cmdCekRekening_Click(object sender, EventArgs e)
        {
            DataInquiriyRekeningResponEx resp = CekRekeningBank();
            if (resp.error_kode != "00")
            {
                MessageBox.Show(resp.message);
            }
            else
            {
                txtNamaPenerimadalamRekeningBank.Text = resp.namaPemilikRekening;
                MessageBox.Show(resp.message);
            }

        }
        private DataInquiriyRekeningResponEx CekRekeningBank(){


            InquiryRekeningRequest requestInquiry = new InquiryRekeningRequest();
            requestInquiry.nomorRekening = txtNoRekening.Text.Replace(".", "");
            requestInquiry.sandiBank = ctrlDaftarBank1.Kode;
            SP2DOnlineService service = new SP2DOnlineService();
            DataInquiriyRekeningResponEx resp = service.CekRekening(requestInquiry);
            return resp;
            


            //string url = "";//
            //DataInquiriyRekeningResponEx resp = new DataInquiriyRekeningResponEx();


            //url = GlobalVar.BANK_URL + "InquiryRekening";
            ////url = "http://36.92.240.142:8080/InquiryRekening";



            //WebResponse objResponse = null;
            //WebRequest request = null;
            //try
            //{
            //    request = WebRequest.Create(url);
            //    request.Method = "POST";


            //  //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
            //    string JsonData = JsonConvert.SerializeObject(requestInquiry);
            //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
            //    request.ContentType = "application/Json";
            //    //request.Headers.Set("client_secret", "");
            //    request.ContentLength = byteArray.Length;
            //    request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
            //    System.IO.Stream dataStream = request.GetRequestStream();
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //    dataStream.Close();
                

            //    objResponse = (WebResponse)request.GetResponse();
            //    Stream streamdata = objResponse.GetResponseStream();
            //    StreamReader strReader = new StreamReader(streamdata);
            //    string responseData = strReader.ReadToEnd();
            //    resp = JsonConvert.DeserializeObject<DataInquiriyRekeningResponEx>(responseData);

            //    if (resp.error_kode != "00")
            //    {
                    

            //        MessageBox.Show(resp.message);
            //    }
            //    else
            //    {
            //        txtNamaPenerimadalamRekeningBank.Text = resp.namaPemilikRekening;
            //        MessageBox.Show(resp.message);
            //    }
            //   // return resp;
            //    return;

            //} catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void cmdSimpanPajak_Click(object sender, EventArgs e)
        {
            if (SimpanPotongan() == true)
            {
                MessageBox.Show("Penyimpanan selesai.");
            }

        }
        private bool SimpanPotongan(){
            try
            {
                if (m_NoUrut == 0)
                {
                    MessageBox.Show("Sila simpan SPP terlebih dahulu.");
                }
                PotonganSPPLogic oLogic = new PotonganSPPLogic(GlobalVar.TahunAnggaran);


                List<PotonganSPP> lstPajak = ctrlPotongan1.getDisplayRekening();
                foreach (PotonganSPP pot in lstPajak)
                {
                    if (pot.Jumlah > 0)
                    {
                        pot.NoUrut = m_NoUrut;
                        pot.Informasi = 0;
                        oLogic.Simpan(pot);
                    }
                }
                List<PotonganSPP> lstPajak2 = ctrlPotongan2.getDisplayRekening();
                foreach (PotonganSPP pot in lstPajak2)
                {
                    if (pot.Jumlah > 0)
                    {
                        pot.NoUrut = m_NoUrut;
                        pot.Informasi = 1;
                        oLogic.Simpan(pot);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void cmdSimpanSP2D_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVar.Pengguna.Status >= 2)
                {
                    MessageBox.Show("Pengguna tidak bisa melakukan penyimpanan data..");
                    return;
                }
                SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                // m_oSPP.iNOSP2D
                string sNomorNoSP2d = "";
                int inoSP2d;
                if (m_iStatus < 3)
                {
                    inoSP2d = DataFormat.GetInteger(txtNoSP2D);
                    sNomorNoSP2d = txtNoSP2D.Text.Trim();
                }
                else
                {
                    inoSP2d = m_oSPP.iNOSP2D;
                    if (inoSP2d == 0)
                    {
                        int indexSlash = txtNoSP2D.Text.IndexOf("/");
                        if (indexSlash < 0)
                        {
                            sNomorNoSP2d = txtNoSP2D.Text;
                        }
                        else
                        {
                            sNomorNoSP2d = txtNoSP2D.Text.Substring(0, indexSlash);
                        }
                    }
                }


                if (oSPPLogic.CekNoSP2D(m_NoUrut, inoSP2d, sNomorNoSP2d, txtNoSP2D.Text + txtPrefixSP2d.Text) == false)
                {
                    MessageBox.Show("Nomor Sudah dipakai");
                    return;

                }

                if (m_iJenisSPP == 4 && txtPrefixSP2d.Text.Contains("GJ") == false)
                {
                    if (MessageBox.Show("Jenis SPM Gaji, Nomor SPD tidak memakai 'GJ'. Apakah Tetap akan menyimpan?", "Komfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                    {

                        return;

                    }

                }
                if (dtSP2D.Value.Year != GlobalVar.TahunAnggaran)
                {
                    MessageBox.Show("Tanggal SP2D salah");
                    return;
                }

                if (txtNoSP2D.Text.Trim().Contains("LS") == true || txtNoSP2D.Text.Trim().Contains("GJ") == true ||
                    txtNoSP2D.Text.Trim().Contains("UP") == true || txtNoSP2D.Text.Trim().Contains("TU") == true)
                {
                    m_oSPP.NoSP2D = txtNoSP2D.Text;
                }
                else
                {

                    m_oSPP.NoSP2D = txtNoSP2D.Text + txtPrefixSP2d.Text;
                }


                m_oSPP.dtTerbit = dtSP2D.Value;
                m_oSPP.PenandatanganBUD = ctrlBendaharaBUD.GetID();
                m_oSPP.iNOSP2D = DataFormat.GetInteger(txtNoSP2D.Text);
                m_oSPP.BankBUD = 1;
                m_oSPP.NamaBankBUD = "Bank Kalimantan Barat";
                m_oSPP.NoRekBUD = "";
                m_oSPP.JenisTransfer = "";
                /*
                if (ctrlDaftarBank1.Kode != "123" || txtNoRekening.Text.Length > 10)
                {
                    if (cmbJenisTrabsfer.Text.Trim() == "")
                    {
                        MessageBox.Show("Bukan No rekening Bank Kalbar, harus menentukan Jenis Transfer..");
                        return;
                    }
                    m_oSPP.JenisTransfer = cmbJenisTrabsfer.Text.Trim();

                }
                else
                {
                    m_oSPP.JenisTransfer = "";
                }
                 * */

                //m_oSPP.PrefixSP2D = txtPrefixSP2d.Text



                if (oSPPLogic.SimpanSP2D(m_oSPP))
                {
                    txtNoSP2D.Text = m_oSPP.NoSP2D;
                    txtNoSP2D.Width = dtSP2D.Width;

                    m_oSPP.Status = 3;
                    // cmdSimpanPajak.Visible = false;
                    txtPrefixSP2d.Text = "";
                    txtPrefixSP2d.Visible = false;

                    int idx = 0;
                    foreach (SPP spp in GlobalVar.gListSPP)
                    {
                        if (spp.NoUrut == m_NoUrut)
                        {
                            idx = GlobalVar.gListSPP.IndexOf(spp);
                        }
                    }
                    if (idx > 0)
                    {
                        GlobalVar.gListSPP[idx].NoSP2D = m_oSPP.NoSP2D;
                        GlobalVar.gListSPP[idx].Status = 3;
                        GlobalVar.gListSPP[idx].dtTerbit = dtSP2D.Value;
                        GlobalVar.gListSPP[idx].PenandatanganBUD = ctrlBendaharaBUD.GetID();

                    }


                    MessageBox.Show("Berhasil simpan SP2D.");

                }
                else
                {
                    MessageBox.Show("Gagal simpan SP2D." + oSPPLogic.LastError());
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdCekNPWP_Click(object sender, EventArgs e)
        {
            InquiryNPWPRequest requestInquiry;

            List<PotonganSPP> lstPotongan = ctrlPotongan1.getDisplayRekening();
            foreach (PotonganSPP p in lstPotongan)
            {
                requestInquiry = new InquiryNPWPRequest();
                requestInquiry.kodeMap = p.KodeMap.Trim();
                requestInquiry.kodeSetor = p.KodeSetor.Trim();
                requestInquiry.nomorPokokWajibPajak = txtNoNPWP.Text.Trim();
                if (p.KodeMap == "" || p.KodeSetor == "" || txtNoNPWP.Text == "")
                {
                    MessageBox.Show("Data Inquiry NPWP kurang lengkap ");

                } else {
                  InQuiryNPWP(requestInquiry);
                }
            }
        }

        private DataInquiriesNPWPResponseEx InQuiryNPWP(InquiryNPWPRequest requestInquiry ){

            string url = "";//
            DataInquiriesNPWPResponseEx resp = new DataInquiriesNPWPResponseEx();

            

            url = GlobalVar.BANK_URL + "InquiryNPWP";
            //url = "http://36.92.240.142:8080/InquiryRekening";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
         request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInquiriesNPWPResponseEx>(responseData);

                if (resp.error_kode != "00")
                {
                    string message = requestInquiry.nomorPokokWajibPajak + ", " +
                                     requestInquiry.kodeMap + " dan " + requestInquiry.kodeSetor;

                    message = message + "-> " + resp.message;
                    MessageBox.Show(message);
                }
                else
                {
                    frmDataNPWP fDataNPWP = new frmDataNPWP();
                    fDataNPWP.SetData(resp);
                    fDataNPWP.ShowDialog();

                    
                }
                 return resp;
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (m_oSPP.Status < 1)
            {
                MessageBox.Show("Sila Simpan SPM terlebihh dahulu..");
                return;
            }
            CreateIdBilling();
         //   InquiryMON();
        }

        private void cmdInquiryMPN_Click(object sender, EventArgs e)
        {
        
        }
        //private void InquiryMON(){
        //    int idx = 0;
        //    SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
        //    idx= oSPPLogic.GetNoPeguji();

        //    List<PotonganSPP> lstPot = new List<PotonganSPP>();
        //    lstPot = ctrlPotongan1.getDisplayRekening();
        //    foreach (PotonganSPP p in lstPot)
        //    {
        //        InquiryPaymentMPNRequest request = new InquiryPaymentMPNRequest();
        //        request.idBilling = p.IDBilling.Trim();
        //        request.reInquiry = "false";
        //        request.referenceNo = (101300000000 + idx).ToString();
        //        InQuiryMPN(request);
        //        idx = oSPPLogic.GetNoPeguji();

        //    }

        //}

       

        private void cmdGenerateReport_Click(object sender, EventArgs e)
        {
            List<PotonganSPP> lstPot = new List<PotonganSPP>();
            lstPot = ctrlPotongan1.getDisplayRekening();
            foreach (PotonganSPP p in lstPot)
            {
                frmReportIdBilling fReport = new frmReportIdBilling();
                fReport.NoReference = p.IDBilling.Trim();
                fReport.ShowDialog();


            }

        }

        private void cmdTransaksiSp2DCase58_Click(object sender, EventArgs e)
        {
            RequestSP2d requestInquiry = new RequestSP2d();
            requestInquiry.nomorSP2D = txtNoSP2D.Text.Trim();


            string url = "";//
            DataInformasi511ResponseEx resp = new DataInformasi511ResponseEx();


           // url = "https://localhost:7139/InformasiSP2D511";
            url = GlobalVar.BANK_URL + "InformasiSP2D511";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
         request.Headers.Add("client_secret", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInformasi511ResponseEx>(responseData);

                if (resp.error_kode == "00" || resp.error_kode == "11")
                {


                    frmRespond511 fD = new frmRespond511();
                    fD.Setdata(resp);
                    fD.Show();

                }
                else
                {
                    MessageBox.Show(resp.message);
                }
                // return resp;
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void ctrlBAST1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlKontrak1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }

        private void cmdSimpanPenerima_Click(object sender, EventArgs e)
        {
            try
            {
                SPP oSPP = new SPP();
                SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);

       
                oSPP.NamaPenerima = txtNamaPenerima.Text;
                oSPP.Alamat = "";
                oSPP.NamaBank = ctrlDaftarBank1.NamaBank;
                oSPP.NoRek = txtNoRekening.Text;//.Rekening;
                oSPP.NoNPWP = txtNoNPWP.Text;
      
     
                oSPP.NamaDalamRekeningBank = txtNamaPenerimadalamRekeningBank.Text;
                oSPP.KodeBank = ctrlDaftarBank1.Kode;
                oSPP.JabatanPenerima = txtJabatanPenerima.Text;
                oSPP.NoUrut = m_NoUrut;

                oSPPLogic.SimpanPenerima(ref oSPP);



            }
            catch (Exception ex)
            {

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGetDataOnUs_Click(object sender, EventArgs e)
        {
            txtNoRekening.Text = "1025284549";
            ctrlDaftarBank1.SetKode( "123");
            txtNoNPWP.Text = "001496827018000";
        }

        private void btnSampleDataOffUs_Click(object sender, EventArgs e)
        {
            txtNoRekening.Text = "1640000145393";
            ctrlDaftarBank1.SetKode("008");
            txtNoNPWP.Text = "001496827018000";
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void mnuCetakSPM_Click(object sender, EventArgs e)
        {
            CetakSPM();

        }
///*
///Cetak Dokumen SPM 
///
        private void CetakDokumenSPM()
        {

            if (GlobalVar.gListRedaksiSPP == null)
            {
                GlobalVar.gListRedaksiSPP = new List<RedaksiSPP>();

            }
            if (GlobalVar.gListRedaksiSPP.Count == 0)
            {
                RedaksiSPPLogic oRedaksiLogic = new RedaksiSPPLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListRedaksiSPP = oRedaksiLogic.Get();
            }


            //Create a new PDF document.
            PdfDocument document = new PdfDocument();



            ProcessDokumenSPM1(ref document);
            ////Create file stream.
            ProcessDokumenSPM2(ref document);
            ProcessDokumenSPM3(ref document);


            using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPP.pdf"), FileMode.Create, FileAccess.ReadWrite))
            {
                //Save the PDF document to file stream.
                document.Save(outputFileStream);

            }

            //Close the document.
            document.Close(true);
            pdfViewer pV = new pdfViewer();
            pV.Document = Path.GetFullPath(@"../../../SPP.pdf");
            pV.Show();
           

        }
        private string  GetJudulSPM()
        {
            string sJudul="";

            switch(m_iJenisSPP){
                case 0:
                    sJudul = "SURAT PERNYATAAN PENGAJUAN SPP UP";
                    break;
                case 1:
                    sJudul = "SURAT PERNYATAAN PENGAJUAN SPP GU";
                    break;

                    case 2:
                        sJudul = "SURAT PERNYATAAN PENGAJUAN SPP TU";
                        break;
                    case 3:
                        switch(m_iJenisSubSPP){
                            case 51:
                                sJudul = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA OPERASI";
                                break;

                            case 52:
                                sJudul = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA MODAL";
                                break;
                            case 53:
                                sJudul = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA TAK TERDUGA";
                                break;
                        case 54:
                             sJudul = "SURAT PERNYATAAN PENGAJUAN SPP LS BELANJA TRANSFER";
                            break;

                        }
                        break;
                    
                    case 4:
                        sJudul = "SURAT PERNYATAAN PENGAJUAN SPP GAJI DAN TUNJANGAN";
                        break;
                                   
            }
            return sJudul;
       
        }
        private void ProcessDokumenSPM1(ref PdfDocument document){


            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
            float yPos;
     
            yPos = 20;

            PdfFont font10;
            PdfFont font12;
            
            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Center;
            stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            //stringFormat.CharacterSpacing = 2f;
            float x0 = 15L;
            float x1 = 35L;
            float x2 = 280L;
            float x3 = 340L;

            float x4 = (page.GetClientSize().Width - 15);

            font10 = new PdfTrueTypeFont(new Font("Arial", 10));
            font12 = new PdfTrueTypeFont(new Font("Arial", 12));

            float setengah = (page.GetClientSize().Width / 2) - 20;
            float posisiTengah = page.GetClientSize().Width / 2 + 10;
            m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
            FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);

            PdfBitmap image = new PdfBitmap(imageStream);
            //Draw the image
            PointF p = new PointF(x0 + 5, 10);
            Size size = new Size(30, 40);

            graphics.DrawImage(image, p, size);

            //SizeF size = font12.MeasureString("xxx");
            yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, x0, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true,false , true );
            yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD().ToUpper(), mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
            yPos = yPos + 10;
            graphics.DrawLine(pen, x0, yPos , x4, yPos );

            yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPM.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
            setengah, m_stringFormatLeft, true, false, true);

            yPos = yPos + 10;
            yPos = TulisItem(graphics, "Kepada Yth.", mfont9, posisiTengah, yPos, setengah, m_stringFormatLeft, true);
            yPos = TulisItem(graphics, "PPKD Selaku BUD", mfont9, posisiTengah, yPos, setengah, m_stringFormatLeft, true);
            yPos = TulisItem(graphics, "up.Kuasa BUD Kabupaten Ketapang ", mfont9, posisiTengah, yPos, setengah, m_stringFormatLeft, true);

            yPos = yPos + 5;
            yPos = TulisItem(graphics, "di ", mfont9, posisiTengah, yPos, setengah, m_stringFormatLeft, true);
            yPos = TulisItem(graphics, "   KETAPANG", mfont9, posisiTengah, yPos, setengah, m_stringFormatLeft, true);

            yPos = yPos + 5;
            yPos = TulisItem(graphics, "SURAT PENGANTAR", mfont9, x0, yPos, x4-x0, m_stringFormatCenter, true, false, true);
            yPos = TulisItem(graphics, "Nomor:             ", mfont9, x0, yPos, x4 - x0, m_stringFormatCenter, true, false, true);
            yPos = yPos + 5;
            graphics.DrawLine(pen, x0, yPos , x4, yPos );
            float posisiAtas = yPos;
            yPos = TulisItem(graphics, "No", mfont8, x0, yPos, x1-x0, m_stringFormatCenter,false,false,true  );
            yPos = TulisItem(graphics, "Uraian", mfont8, x1, yPos, x2 - x1, m_stringFormatCenter, false, false, true);
            yPos = TulisItem(graphics, "Banyaknya", mfont8, x2, yPos, x3 - x2, m_stringFormatCenter, false, false, true);
            yPos = TulisItem(graphics, "Keterangan", mfont8, x3, yPos, x4 - x3, m_stringFormatCenter, true, false, true);
       
            
            graphics.DrawLine(pen, x0, yPos , x4, yPos );
            yPos = TulisItem(graphics, "1", mfont8, x0, yPos, x1 - x0, m_stringFormatCenter, false);
            
            yPos = TulisItem(graphics, "1 (satu) Berkas", mfont8, x2, yPos, x3 - x2, m_stringFormatCenter, false);
            TulisItem(graphics, "Disampaikan dengan hormat sebagai bahan pembuatan SP2D", mfont8, x3, yPos, x4 - x3, m_stringFormatJustify, true, false, true);
            yPos = TulisItem(graphics, "Nomor SPM:", mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, true);
            yPos = TulisItem(graphics, txtNoSPM.Text , mfont8, x1+2, yPos, x2 - x1, m_stringFormatLeft, true );
            yPos = TulisItem(graphics, txtUraian.Text.ReplaceUnicode(), mfont8, x1 + 2, yPos, x2 - 4 - x1, m_stringFormatLeft, true);
            yPos = yPos + 30;
            graphics.DrawLine(pen, x0, yPos, x4, yPos );

            graphics.DrawLine(pen, x0, yPos, x0, posisiAtas);
            graphics.DrawLine(pen, x1-2, yPos, x1-2, posisiAtas);
            graphics.DrawLine(pen, x2, yPos, x2, posisiAtas);
            graphics.DrawLine(pen, x3, yPos, x3, posisiAtas);
            graphics.DrawLine(pen, x4, yPos, x4, posisiAtas);

       

            yPos = yPos + 30;
            Pejabat pimpinan = new Pejabat();
            pimpinan = ctrlDinas1.GetPimpinan(dtSPP.Value);
        
            yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPM.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
           setengah, m_stringFormatCenter, true);

            yPos = TulisItem(graphics, pimpinan.Jabatan, mfont10, posisiTengah, yPos,
             setengah, m_stringFormatCenter, true);

            yPos=yPos+ 30;
            //Object grid2row5 = new { Name = "", Age = m_oBendahara.Nama };

            yPos = TulisItem(graphics, pimpinan.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
            yPos = TulisItem(graphics, "NIP " + pimpinan.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);


        }
        private void ProcessDokumenSPM2(ref PdfDocument document)
        {
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;


            float yPos;
            // Cari Judul 
            SetJudul1();
            yPos = 10;
            
            PdfFont font10;
            PdfFont font12;

            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Center;
            stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            //stringFormat.CharacterSpacing = 2f;
            string namaDinas = ctrlDinas1.GetNamaSKPD();
            float lebarArena = (page.GetClientSize().Width - 30);
            float kiri = 15;

            font10 = new PdfTrueTypeFont(new Font("Arial", 10));
            font12 = new PdfTrueTypeFont(new Font("Arial", 12));
            FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);

            PdfBitmap image = new PdfBitmap(imageStream);
            //Draw the image
            PointF p = new PointF(kiri + 5, 10);
            Size size = new Size(30, 40);

            graphics.DrawImage(image, p, size);

            //SizeF size = font12.MeasureString("xxx");
            yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, kiri, yPos, lebarArena, m_stringFormatCenter, true);
            yPos = TulisItem(graphics, namaDinas, mfont10, kiri, yPos,lebarArena, m_stringFormatCenter, true);
            
            

            

            yPos = yPos + 15;
            yPos = TulisItem(graphics, "SURAT PERNYATAAN PERTANGGUNGJAWABAN", mfont12, kiri, yPos, lebarArena, m_stringFormatCenter, true);
            yPos = TulisItem(graphics, "PENELITIAN DAN VERIFIKASI KELENGKAPAN DOKUMEN", mfont12, kiri, yPos, lebarArena, m_stringFormatCenter, true);
            
            yPos = yPos + 20;
            string text = "";

            text = "Yang bertanda tangan di bawah ini, PEJABAT PENATAUSAHAAN KEUANGAN SKPD " + namaDinas + " menyatakan bahwa:";
            yPos = TulisItem(graphics, text, mfont10, kiri, yPos, lebarArena, m_stringFormatJustify, true);
            
            yPos = yPos + 15;

            text = "Kelengkapan   dokumen   Surat Perintah  Membayar  sudah diverifikasi serta DINYATAKAN BENAR untuk memenuhi seluruh dokumen " +
                   " persyaratan   pembayaran,  sesuai  dengan  peraturan perundang-undangan yang berlaku";
           
            yPos = TulisItem(graphics, text, mfont10, kiri, yPos, lebarArena, m_stringFormatJustify, true);

            text = "Demikian Surat pernyataan  ini   dibuat  dengan  sebenar-benarnya  dan menjadi tanggung jawab kami sepenuhnya selaku PEJABAT PENATAUSAHAAN KEUANGAN " +
              "SKPD " + namaDinas;

            yPos = yPos + 15;
            yPos = TulisItem(graphics, text, mfont10, kiri, yPos, lebarArena, m_stringFormatJustify, true);


            float setengah = (page.GetClientSize().Width / 2) - 20;
            float posisiTengah = page.GetClientSize().Width / 2 + 10;
            Pejabat ppk = new Pejabat();
            ppk = ctrlDinas1.GetPPK( dtSPM.Value);
            m_stringFormatCenter.Alignment = PdfTextAlignment.Center;
            yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPM.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
           setengah, m_stringFormatCenter, true);

            yPos = TulisItem(graphics, ppk.Jabatan, mfont10, posisiTengah, yPos,
             setengah, m_stringFormatCenter, true);
            yPos = yPos + 30;
            yPos = TulisItem(graphics, ppk.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
            yPos = TulisItem(graphics, "NIP " + ppk.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);



        }
        private void ProcessDokumenSPM3(ref PdfDocument document)
        {
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
            float yPos;

            yPos = 20;

            PdfFont font10;
            PdfFont font12;

            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Center;
            stringFormat.LineAlignment = PdfVerticalAlignment.Middle;
            

           
            List<SPPRekening> lstdetail = ctrlRekeningKegiatan1.getDisplayRekening();
            int i = 0;
            float x0 = 10;
            float x1 = 40;
            float x2 = 170;
            float x3 = 420;
            float x4 = 500;
            float yPosAtas;
            PdfPen penLine = new PdfPen(PdfBrushes.Black, 0.5f);

            FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            //Draw the image
            PointF p = new PointF(x0+5, 10);
            Size size = new Size(30,40);

            graphics.DrawImage(image, p, size);
            //float x4 = (page.GetClientSize().Width - 25);

            font10 = new PdfTrueTypeFont(new Font("Arial", 10));
            font12 = new PdfTrueTypeFont(new Font("Arial", 12));

            float setengah = (page.GetClientSize().Width / 2) - 20;
            float posisiTengah = page.GetClientSize().Width / 2 + 10;
            m_stringFormatCenter.Alignment = PdfTextAlignment.Center;

            //SizeF size = font12.MeasureString("xxx");
            yPos = TulisItem(graphics, "PEMERINTAH KABUPATEN " + GlobalVar.gPemda.Nama.ToUpper(), mfont12, x0, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);
            yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD().ToUpper(), mfont10, 10, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true, false, true);

            yPos = yPos + 15;
            graphics.DrawLine(pen, x0, yPos, x4, yPos);

            yPos = yPos + 20;
            yPos = TulisItem(graphics, "SURAT PERNYATAAN TANGGUNG JAWAB", mfont10, x0, yPos, x4 - x0, m_stringFormatCenter, true, false, true);
            yPos = TulisItem(graphics, "Nomor SPM: " + txtNoSPM.Text , mfont10, x0, yPos, x4 - x0, m_stringFormatCenter, true, false, true);
            yPos = yPos + 20;
            string text;
            string namaDinas = ctrlDinas1.GetNamaSKPD();
            string namaPemda = GlobalVar.gPemda.NamaPanjang;
            if (namaDinas.Contains("BADAN PENGELOLA") == true)
            {
                text = "Yang bertanda tangan di bawah ini, KUASA PENGGUNA ANGGARAN " + namaDinas + "  selaku Pengguna Anggaran " + namaDinas + "  " + namaPemda  +
                      " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

            } else {

                text="Yang bertanda tangan di bawah ini, KEPALA DINAS " + 
                    namaDinas.Replace("DINAS", "") + 
                    " selaku Pengguna Anggaran "+  namaDinas + 
                    " "+ namaPemda  +
                   " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";
            
            }

            if (namaDinas.Contains("KEPALA DAERAH") == true ||  namaDinas.Contains("KANTOR") == true ){
                if (m_iKodeUk>1){
                    text = "Yang bertanda tangan di bawah ini, SEKRETARIS DAERAH selaku Pengguna Anggaran " + namaDinas + " " + namaPemda +
                         " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

                }
                else
                {
                    text = "Yang bertanda tangan di bawah ini, Kepala Bagian " + ctrlDinas1.GetNamaUnit()+ " selaku Kuasa Pengguna Anggaran " + namaDinas +" " + namaPemda + 
                       " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

                }
                 
            }
            if ( namaDinas.Contains("BADAN") == true || namaDinas.Contains("KANTOR") == true ) {
                    if ( namaDinas.Contains("BADAN PENGELOLA") == true || namaDinas.Contains("KANTOR") == true ) {
                    } else{
                        text  = "Yang bertanda tangan di bawah ini, KUASA PENGGUNA ANGGARAN "+ namaDinas + " selaku Pengguna Anggaran " + namaDinas+" " + namaPemda + 
                        " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

                    }

            }
                
            if (namaDinas.Contains("CAMAT") == true ) {
             text  = "Yang bertanda tangan di bawah ini, CAMAT " + namaDinas.Replace("KECAMATAN", "") + " selaku Pengguna Anggaran " +namaDinas.ToUpper() + " " + namaPemda +
                  " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

            }
        if  (namaDinas.Contains("POLISI PAMONG")== true ) 
         text  = "Yang bertanda tangan di bawah ini, KEPALA  " +namaDinas + " selaku Pengguna Anggaran " + namaDinas + " " + namaPemda +
                    " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

        if (namaDinas.Contains("INSPEK") == true)
            text = "Yang bertanda tangan di bawah ini, INSPEKTUR selaku Pengguna Anggaran " + namaDinas + " " + namaPemda +
                " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";
        if (namaDinas.Contains("DPRD") == true)
            text = "Yang bertanda tangan di bawah ini, SEKRETARIS " + namaDinas + " selaku Pengguna Anggaran " + namaDinas + " " + namaPemda +
                   " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";

    if (namaDinas.Contains("SEKRE") ==true )
      if (m_iKodeUk>1){
        text  = "Yang bertanda tangan di bawah ini, SEKRETARIS " + namaDinas.Replace ("SEKRETARIAT ", "") + "  selaku Pengguna Anggaran " + namaDinas + " " + namaPemda+
             " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";
      }else{
          text = "Yang bertanda tangan di bawah ini, Kepala Bagian " + namaDinas.Replace("BAGIAN ", "") + " selaku Kuasa Pengguna Anggaran " + namaDinas + " " + namaPemda +
            " bahwa saya bertanggung jawab atas kebenaran dokumen yang menjadi persyaratan dan segala akibat yang timbul dari pencairan dan kegiatan tersebut.";
           
      }

    yPos = TulisItem(graphics, text, mfont10, x0, yPos, x4 - x0, m_stringFormatJustify, true);
    yPos = yPos + 10;



            string s ="";
            switch(m_iJenisSPP){
                case 0:
                    s="(SPM UP)";
                    break;
                 case 1:
                    s="(SPM GU)";
                    break;
            case 2:
                    s="(SPM TU)";
                    break;
                case 3:
                case 4:
                    s="(SPM LS)";
                    break;


            }
            
            text = "1. ";
            yPos = TulisItem(graphics, text, mfont10, x0, yPos, x4 - x0, m_stringFormatLeft, false);

            text = "Surat Perintah Membayar " + s + " Nomor " + txtNoSPM.Text + " tanggal " + dtSPM.Value.ToTanggalIndonesia() + " pembebanan pada :";
            yPos = TulisItem(graphics, text, mfont10, x0+10, yPos, x4 - x0-10, m_stringFormatLeft, true);

            yPosAtas = yPos;
            graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

            yPos = TulisItem(graphics, "No", mfont10, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);
            yPos = TulisItem(graphics, "Kode Rekening", mfont10, x1, yPos, x2 - x1, m_stringFormatCenter, false);
            yPos = TulisItem(graphics, "Jumlah", mfont10, x3, yPos, x4 - x3, m_stringFormatCenter, false);
            yPos = TulisItem(graphics, "Nama", mfont10, x2, yPos, x3 - x2, m_stringFormatCenter, true);

            yPos = yPos + 2;
            graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);
            if (m_iJenisSPP == 0)
            {
                yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);



                yPos = TulisItem(graphics, "", mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                yPos = TulisItem(graphics, txtJumlah.Text, mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                yPos = TulisItem(graphics, "Uang Persediaan", mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                //yPos = yPos + 2;
                graphics.DrawLine(penLine, x1 - 1, yPosAtas, x1 - 1, yPos);
                graphics.DrawLine(penLine, x2 - 1, yPosAtas, x2 - 1, yPos);

                yPos = TulisItem(graphics, "JUMLAH", mfont8, x0 + 2, yPos, x3 - 3 - x0, m_stringFormatRight, false);
                yPos = TulisItem(graphics, txtJumlah.Text, mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);


            }
            else
            {
                if (m_iJenisSPP == 1)
                {
                    SPPLogic osppLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                    List<SPPRekening> lstGU = osppLogic.GetRingkasanGU(m_NoUrut);
                    if (lstGU != null)
                    {
                        foreach (SPPRekening sr in lstGU)
                        {
                            if (sr.Jumlah > 0)
                            {


                                yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);

                                string sKode = sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening();

                                yPos = TulisItem(graphics, sKode, mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                                yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                                yPos = TulisItem(graphics, sr.NamaRekening, mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                                graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                                if (yPos + 10 > page.GetClientSize().Height)
                                {
                                    page = document.Pages.Add();
                                    // yPos = 20;
                                }

                            }
                        }
                    }
                    

                }
                else
                {

                    foreach (SPPRekening sr in lstdetail)
                    {
                        if (sr.Jumlah > 0)
                        {


                            yPos = TulisItem(graphics, (++i).ToString(), mfont8, x0 + 2, yPos, x1 - x0, m_stringFormatCenter, false);

                            string sKode = sr.IDSubKegiatan.ToKodeSubKegiatan() + "." + sr.IDRekening.ToKodeRekening();

                            yPos = TulisItem(graphics, sKode, mfont8, x1, yPos, x2 - x1, m_stringFormatLeft, false);
                            yPos = TulisItem(graphics, sr.Jumlah.ToRupiahInReport(), mfont8, x3, yPos, x4 - 2 - x3, m_stringFormatRight, false);

                            yPos = TulisItem(graphics, sr.NamaRekening, mfont8, x2, yPos, x3 - x2, m_stringFormatLeft, true);
                            graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);

                            if (yPos + 10 > page.GetClientSize().Height)
                            {
                                page = document.Pages.Add();
                                // yPos = 20;
                            }

                        }
                    }
                }
                //yPos = yPos + 2;

                graphics.DrawLine(penLine, x1 - 1, yPosAtas, x1 - 1, yPos);
                graphics.DrawLine(penLine, x2 - 1, yPosAtas, x2 - 1, yPos);
                //graphics.DrawLine(penLine, x3, yPosAtas, x3 , yPos);
                //graphics.DrawLine(penLine, x4 , yPosAtas, x4 , yPos);

                yPos = TulisItem(graphics, "JUMLAH", mfont8, x0 + 2, yPos, x3 - 3 - x0, m_stringFormatRight, false);
                yPos = TulisItem(graphics, txtJumlahSPP.Text, mfont8, x3, yPos, x4 - x3, m_stringFormatRight, true);
              
            }
            graphics.DrawLine(penLine, x0, yPos - 1, x4, yPos - 1);
            graphics.DrawLine(penLine, x0, yPosAtas, x0, yPos);
            graphics.DrawLine(penLine, x3, yPosAtas, x3, yPos);
            graphics.DrawLine(penLine, x4, yPosAtas, x4, yPos);

            yPos = yPos + 30;


            text = "2. ";
            yPos = TulisItem(graphics, text, mfont10, x0, yPos, x4 - x0, m_stringFormatLeft, false);

            text = "Kebenaran material surat-surat bukti" ;
            yPos = TulisItem(graphics, text, mfont10, x0 + 10, yPos, x4 - x0 - 10, m_stringFormatLeft, true);
            
            text = "3. ";
            yPos = TulisItem(graphics, text, mfont10, x0, yPos, x4 - x0, m_stringFormatLeft, false);

            text = "Kebenaran dokumen yang menjadi pernyataan kelengkapan pencairan dana";
            yPos = TulisItem(graphics, text, mfont10, x0 + 10, yPos, x4 - x0 - 10, m_stringFormatLeft, true);

             text = "4. ";
             yPos = TulisItem(graphics, text, mfont10, x0, yPos, x4 - x0, m_stringFormatLeft, false );

            text = "Dan dokumen lainnya sesuai ketentuan ";
            yPos = TulisItem(graphics, text, mfont10, x0 + 10, yPos, x4 - x0 - 10, m_stringFormatLeft, true);



            yPos = yPos + 10;

            text = "Demikian pernyataan Tanggung Jawab ini dibuat agar dapat dipergunakan sebagaimana mestinya";

            yPos = TulisItem(graphics, text, mfont10, x0 , yPos, x4 - x0 , m_stringFormatLeft, true);

            

            Pejabat ppk = ctrlDinas1.GetPimpinan(dtSPP.Value);

            yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
           setengah, m_stringFormatCenter, true);

            yPos = TulisItem(graphics, ppk.Jabatan, mfont10, posisiTengah, yPos,
             setengah, m_stringFormatCenter, true);

            yPos = yPos + 30;
            //Object grid2row5 = new { Name = "", Age = m_oBendahara.Nama };

            yPos = TulisItem(graphics, ppk.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
            yPos = TulisItem(graphics, "NIP " + ppk.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);



        }

        private void mnCetakDokSPM_Click(object sender, EventArgs e)
        {
            CetakDokumenSPM();
        }

        private void cmdRefreshBendahara_Click(object sender, EventArgs e)
        {
            DisplayBendahara();
        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);

                    if (oLogic.Hapus(m_NoUrut) == true)
                    {
                        MessageBox.Show("Penghapusan berhasil");

                    }
                    else
                    {
                        MessageBox.Show("Penghapusan GAGAL");
                    }
                        ;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtNoSPP_Enter(object sender, EventArgs e)
        {
            if (m_NoUrut == 0)
            {
                if (ctrlJenisSPP1.GetID() == 3)
                {
                    if (ctrlJenisBelanjaSPP1.GetID() == 0)
                    {
                        MessageBox.Show("Sila pilih Jenis Belanja..");
                        return;
                    }
                }
            }
        }

        private void cmdBatalkanSPM_Click(object sender, EventArgs e)
        {
            if (m_iStatus == 3 || m_iStatus == 4)
            {
                MessageBox.Show("SPM ini sudah Terbit SP2D.. Tidak bisa diubah. ");
                return;

            }

            if (m_iStatus == 7)
            {
                MessageBox.Show("SPM ini sudah Terbit SP2D dan dalam tahap pencairan..");
                return;

            }
            if (GlobalVar.Pengguna.Status >= 2)
            {
                MessageBox.Show("Pengguna tidak bisa melakukan penyimpanan data..");
                return;
            }
            if (dtSPM.Value.Year != GlobalVar.TahunAnggaran)
            {
                MessageBox.Show("Tahun tanggal SPM salah");
                return;
            }

           


           


            SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);

            if (oSPPLogic.BatalSPM(m_oSPP))
            {

                txtNoSPM.Text ="";

                txtNoSPM.Width = dtSPM.Width - txtPrefixSPM.Width;

                m_oSPP.Status = 0;
                //txtPrefixSPM.Text = "";
                txtPrefixSPM.Visible = true;
                MessageBox.Show("Berhasil membatalkan SPM.");
                cmdCetak.Visible = false;

                int idx = 0;
                foreach (SPP spp in GlobalVar.gListSPP)
                {
                    if (spp.NoUrut == m_NoUrut)
                    {
                        idx = GlobalVar.gListSPP.IndexOf(spp);
                    }
                }
                if (idx > 0)
                {

                    GlobalVar.gListSPP[idx] = m_oSPP;

                }

            }
            else
            {
                MessageBox.Show("Gagal membatalkan  SPM." + oSPPLogic.LastError());
            }
            return;

        }

        private void ctrlRekeningKegiatan1_Load(object sender, EventArgs e)
        {

        }

        private void txtJumlah_TextChanged(object sender, EventArgs e)
        {
            if (m_iJenisSPP == 0)
            {
                decimal jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                decimal batasUP = DataFormat.FormatUangReportKeDecimal(txtBatasUP.Text);
                if (jumlah > batasUP)
                {
                    MessageBox.Show("Melebihi Batas UP.");
                    txtJumlah.Text = "0";

                }
            }
        }

        private void ctrlSPJUP1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSPJUP1_OnChanged(long pID)
        {
            long inourutSPD = ctrlSPD1.GetID();
            if (inourutSPD == 0)
            {
                MessageBox.Show("Belum Pilih SPD");
                return;
            }
            ctrlRekeningKegiatan1.CreateNewSPPSariSPJUP(m_IDDInas, pID, inourutSPD);


           // }


        }

        private void mnuCetakSPMPortrait_Click(object sender, EventArgs e)
        {
            CetakSPMPortrait();
        }

        private void cmdBatalSP2D_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.Status >= 2)
            {
                MessageBox.Show("Pengguna tidak bisa melakukan penyimpanan data..");
                return;
            }
            SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);

            if (MessageBox.Show("Benar akan membatalkan SP2D ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (oSPPLogic.BatalkanSP2D(m_NoUrut) == true)
                {
                    MessageBox.Show("SP2D sudah batal");
                }
                else
                {
                    MessageBox.Show(oSPPLogic.LastError());
                }
            }
            
        }

        private void cmdUbahKeterangan_Click(object sender, EventArgs e)
        {

            SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            if (oSPPLogic.SimpanUbahUraian(m_NoUrut, txtUraian.Text) == true)
            {
                MessageBox.Show("Data sudah diubah");
            }
            else
            {
                MessageBox.Show("Kesalahan merubah Uraian " + oSPPLogic.LastError());
            }

        }

        private void ShowHideBanyakKegiatan (bool bDispley){
            ctrlPilihanRekeningAnggaran1.Visible = bDispley;
            label37.Visible = bDispley;
            cmdAdd.Visible = bDispley;
        }
        private void chkBanyakKegiatan_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideBanyakKegiatan(chkBanyakKegiatan.Checked);
            if (chkBanyakKegiatan.Checked)
                ctrlRekeningKegiatan1.Clear();
        }

        private void ctrlPerusahaan1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPilihanRekeningAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlJenisBelanjaSPP1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdUpdateSilakan_Click(object sender, EventArgs e)
        {
            try
            {
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                if (optTerima.Checked == true)
                {
                    oLogic.VerifikasiSPM(m_NoUrut);
                }
                else
                {
                    List<string> lst = new List<string>();
                    for (int i = 0; i < gridAlasan.Rows.Count; i++)
                    {
                        lst.Add(DataFormat.GetString(gridAlasan.Rows[i].Cells[1].Value) );
                    }
                    oLogic.TolakSPM(m_NoUrut, DateTime.Now.Date, lst, 1);
                }
                if (oLogic.IsError() == false)
                {
                    CatatSilakan();
                }
                else
                {
                    MessageBox.Show(oLogic.LastError());
                }


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void CatatSilakan()
        {
            try
            {




                string Cadena = "https://api-silakan.seibutomasua.xyz/api/sp2d/update_status/" + m_NoUrut.ToString();
                SilakanVerifikasi oSilakan = new SilakanVerifikasi();

                if (optTerima.Checked == true)
                {
                    oSilakan.status = "accept";
                    oSilakan.catatan_verifikator = "";
                }
                if (optTerima.Checked == false)
                {
                    string s = "";
   
                    for (int i = 0; i < gridAlasan.Rows.Count; i++)
                    {
                        s = s +"," + DataFormat.GetString(gridAlasan.Rows[i].Cells[1].Value);
                    }
                    if (s.Length > 0)
                    {
                        s = s.Substring(1, s.Length - 1);
                    }
                    oSilakan.status = "reject";
                    oSilakan.catatan_verifikator =s;
                }
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(oSilakan);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                    HttpContent jsonContent = new StringContent(JsonConvert.SerializeObject(oSilakan), System.Text.Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "3|ajVZEgNjgFuSDy1c3edVPhFLtoILOEAf8A1DgmoY");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    try
                    {
                        var result = client.PostAsync(Cadena, stringContent).Result;
                        if (result.IsSuccessStatusCode == true)
                        {
                            MessageBox.Show("Catat Silakan berhasil");
                        }
                    }
                    catch (AggregateException err)
                    {
                        foreach (var errInner in err.InnerExceptions)
                        {
                            Console.WriteLine(errInner); //this will call ToString() on the inner execption and get you message, stacktrace and you could perhaps drill down further into the inner exception of it if necessary 
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdCetakPenolakan_Click(object sender, EventArgs e)
        {
            

                 PdfDocument document= new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfPen pen = new PdfPen(PdfBrushes.Black, 0.5f);
            float yPos;
     
            yPos = 20;

            PdfFont font10;
            PdfFont font12;
            
            PdfStringFormat stringFormat = new PdfStringFormat();
            stringFormat.Alignment = PdfTextAlignment.Center;
            stringFormat.LineAlignment = PdfVerticalAlignment.Top;
            //stringFormat.CharacterSpacing = 2f;
            float x0 = 15L;
            float x1 = 85L;
      

            float x4 = (page.GetClientSize().Width - 15);

            font10 = new PdfTrueTypeFont(new Font("Arial", 10));
            font12 = new PdfTrueTypeFont(new Font("Arial", 12));

            float setengah = (page.GetClientSize().Width ) - 20;
        
            m_stringFormatCenter.Alignment = PdfTextAlignment.Left;
        
            yPos = TulisItem(graphics, "PENOLAKAN SPM", mfont12, x0, yPos, (page.GetClientSize().Width - 20), m_stringFormatCenter, true,false , true );
           yPos = yPos + 5;
            graphics.DrawLine(pen, x0, yPos, x0 + setengah, yPos);

        

            yPos = yPos + 5;
             yPos = TulisItem(graphics, "Dinas", mfont9, x0, yPos, setengah , m_stringFormatLeft, false);
           yPos = TulisItem(graphics, ctrlDinas1.GetNamaSKPD(), mfont9, x1, yPos, setengah-x1, m_stringFormatLeft, true);
               yPos = TulisItem(graphics, "No SPM ", mfont9, x0, yPos, setengah, m_stringFormatLeft, false);
               yPos = TulisItem(graphics, m_oSPP.NoSPM, mfont9, x1, yPos, setengah - x1, m_stringFormatLeft, true);
                 yPos = TulisItem(graphics, "Jumlah", mfont9, x0, yPos, setengah, m_stringFormatLeft, false);
            yPos = TulisItem(graphics, "Rp. " + txtJumlah.Text, mfont9, x1, yPos, setengah, m_stringFormatLeft, true);
            yPos = TulisItem(graphics, "Alasan", mfont9, x0, yPos, setengah, m_stringFormatLeft, true);
            int no = 0;
            for (int i = 0; i < gridAlasan.Rows.Count; i++)
            {
                if (DataFormat.GetString(gridAlasan.Rows[i].Cells[1].Value).Length > 0)
                {
                    yPos = TulisItem(graphics, (++no).ToString(), mfont9, x0, yPos, setengah, m_stringFormatLeft, false);
                    yPos = TulisItem(graphics, DataFormat.GetString(gridAlasan.Rows[i].Cells[1].Value), mfont9, x0 + 20, yPos, setengah, m_stringFormatLeft, true);
                    yPos = yPos - 3;
                }
      
            
            }     
      
                 using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../TolakSPM.pdf"), FileMode.Create, FileAccess.ReadWrite))
                 {
                     //Save the PDF document to file stream.
                     document.Save(outputFileStream);

                 }


             
                 //Close the document.
                 document.Close(true);
                 pdfViewer pV = new pdfViewer();
                 pV.Document = Path.GetFullPath(@"../../../TolakSPM.pdf");
                 pV.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cmdPilih_Click(object sender, EventArgs e)
        {
            txtAlasanPenolakanSPM.Text = ctrlAlasanPenolakan1.Text;

        }

        private void cmdMasukkanKeListAlasan_Click(object sender, EventArgs e)
        {
            string[] r = { (gridAlasan.Rows.Count).ToString(),  txtAlasanPenolakanSPM.Text, "Hapus" };
            gridAlasan.Rows.Add(r);
        }

        private void gridAlasan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GlobalVar.Pengguna.Kelompok == (int)Otoritas.CON_OTORITAS_BUDVERIFIKASISPM)
            {
                if (e.ColumnIndex == 2 && e.RowIndex < gridAlasan.Rows.Count)
                {
                    gridAlasan.Rows.RemoveAt(e.RowIndex);
                }
            }
            else { }
        }

        private void LoadAlasanPenolakan()
        {
            try
            {
                gridAlasan.Rows.Clear();
                List<TemplateAlasan> lst = new List<TemplateAlasan>();
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                lst = oLogic.GetDaftarPenolakanByNoUrut(m_NoUrut);
                if (lst != null)
                {
                    gridAlasan.Rows.Clear();
                    foreach (TemplateAlasan t in lst)
                    {
                        if (t.Alasan.Length > 0)
                        {
                            string[] r = { (gridAlasan.Rows.Count).ToString(), 
                                         t.Alasan, "Hapus" };
                            gridAlasan.Rows.Add(r);
                        }
                    }

                }

           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memanggil alasan penolakan");
            }
        }

        private void smdMasukkanKeTempale_Click(object sender, EventArgs e)
        {
            try
            {
                TemplateAlasanLogic oLogic = new TemplateAlasanLogic((int)GlobalVar.TahunAnggaran);
                TemplateAlasan ta = new TemplateAlasan();
                ta.Alasan = txtAlasanPenolakanSPM.Text;
                if (oLogic.Simpan(ta))
                {
                    MessageBox.Show("Kalimat sudah disimpan dan bisa digunakan lagi.");
                }
                else
                {
                    MessageBox.Show("Kalimat GAGAL disimpan ."+ oLogic.LastError());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ctrlPilihanRekeningAnggaran1_OnChanged(long pID)
        {
           
        }

        private void tabRekening_Click(object sender, EventArgs e)
        {

        }

        private void cmdUpdatePPTK_Click(object sender, EventArgs e)
        {
            try
            {

                Pejabat pejabat = ctrlPPTK.GetPejabat();


                SPP oSPP = new SPP();
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahapAnggaran);

                m_oSPP = oSPP;
                //   ctrlDinas1.SetID(m_oSPP.IDDInas);

                m_oSPP.NoUrut = m_NoUrut;
                oLogic.UpdatePPTK(m_NoUrut, pejabat.ID);
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void cmdRefreshPenandaTanganSPM_Click(object sender, EventArgs e)
        {
            try
            {
                Pejabat p = new Pejabat();
                PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                int iddinas = ctrlDinas1.GetID();
                int uk = ctrlDinas1.KodeUK();
                p = oLogic.GetKepalaDinas(iddinas, 0, dtSPM.Value, uk);

                txtNamaPA.Text = p.Nama;
                txtJabatanPA.Text = p.Jabatan;
                txtNIPPA.Text = p.NIP;
                if (MessageBox.Show("Apakah datanya benar?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SPPLogic sppLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                    sppLogic.UpdateUK(m_NoUrut, uk, p.Nama, p.Jabatan, p.NIP);
                }




            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuSPM_Opening(object sender, CancelEventArgs e)
        {

        }
    }
    public  class RequestSP2d
    {
        public string nomorSP2D { set; get; }
    }
   

}
