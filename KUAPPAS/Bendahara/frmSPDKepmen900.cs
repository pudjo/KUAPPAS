using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DTO;
using BP;
using Formatting;
using Rahul.Office.Token;
using Rahul.Office.MS.Word;

using Microsoft.Office.Interop.Word;
using System.IO;
using BP.Bendahara;
using DTO.Anggaran;
using BP.Anggaran;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;

namespace KUAPPAS.Bendahara
{
    public partial class frmSPDKepmen900 : ChildForm
    {
        List<RekapAnggaran> _lstRekap2 = new List<RekapAnggaran>();
        private List<ProgramKegiatanAnggaran> m_lstProgramKegiatan;
        private List<ProgramKegiatanAnggaran> m_lstSemuaProgramKegiatan;
        
        private int m_iJenisLama;

        private List<DisplaySPD> m_lstDisplaySPD ;
        List<SPD> mListSPD;
        List<SPDDetail> m_lstSPDDetail;
        string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

        string[] bulanpanjang = new string[12] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };

        private int m_iJenis;
        private long m_NoUrut;
        private bool m_bNew;
        
        

        private const int COL_ANGGARANMURNI =2;
        private const int COL_ANGGARANGESER=3;
        private const int COL_ANGGARANRKAP = 4;
        private const int COL_ANGGARANABT=5; 
        private const int COL_AKUMULASI =6;
        private const int COL_SPDINI =7;
        private const int COL_SISA = 8;

        private const int COL_LEVEL=9;
        private const int COL_IDURUSAN=10; 
        private const int COL_IDPROGRAM=11;
        private const int COL_IDKEGIATAN=12;
        private const int COL_IDSUBKEGIATAN=13;
        private const int COL_IDREKENING =14;
        private const int COL_IDDINAS = 0;
        private const int COL_KODEUK = 15;
        private const int COL_STATUSUPDATE = 16;


        private const int LEVEL_DINAS = 0;
        private const int LEVEL_UNIT = 1;
        private const int LEVEL_URUSAN = 2;
        private const int LEVEL_PROGRAM=3;
        private const int LEVEL_KEGIATAN = 4;
        private const int LEVEL_SUBKEGIATAN = 5;
        private const int LEVEL_REKANING = 6;




        private decimal m_cJumlahDPA;
        private decimal m_cJumlahSPDSebelum;
        private decimal m_cJumlahSISADANASEBELUM;
        private decimal m_cJumlahSPD;
        private decimal m_cJumlahSISADPA;
        private decimal m_cJumlahTotalSPD;

        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _level2style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level3style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level4style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level5style = new DataGridViewCellStyle();
        DataGridViewCellStyle _level6style = new DataGridViewCellStyle();

        DataGridViewCellStyle _level7style = new DataGridViewCellStyle();
        DataGridViewCellStyle _redstyle = new DataGridViewCellStyle();

        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();

        //public delegate void GetKegiatan();
        //public GetKegiatan getKegiatan;
        private int prevJenis = 0;
        private int preDinas = 0;

        private int m_IDSKPD;
        private Single m_iStatus;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
        CetakPDF m_oCetakPDF;
        int halaman;
        public frmSPDKepmen900()
        {
            InitializeComponent();
            mListSPD = new List<SPD>();
             m_lstDisplaySPD = new List<DisplaySPD>();
             m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
             m_oCetakPDF = new CetakPDF();
             halaman = 0;
            m_iJenis = 0;
            m_iJenisLama = 0;
      


        }
        private void FormatGrid()
        {
            FontStyle styleFont = new FontStyle();
            
            _hilightstyle.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Bold);
            _hilightstyle.ForeColor = Color.White;

            _hilightstyle.BackColor = Color.LightSlateGray;


            _level2style.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Bold);
            _level2style.BackColor = Color.LightSteelBlue;

            _level3style.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Bold);
            _level3style.BackColor = Color.LightSteelBlue;// new Font(gridKUA.Font, FontStyle.Bold);

            _level4style.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Regular);
            _level4style.BackColor = Color.LightGray;// new Font(gridKUA.Font, FontStyle.Bold);

            _level5style.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Italic);
            _level5style.BackColor = Color.Lavender;// new Font(gridKUA.Font, FontStyle.Bold);
            _level6style.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Italic);
            _level6style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);

            _level7style.Font = new System.Drawing.Font(gridSPDDetail.Font, FontStyle.Regular);

            _level7style.BackColor = Color.Honeydew;// new Font(gridKUA.Font, FontStyle.Bold);
           
            for (int idx=0; idx< gridSPDDetail.Rows.Count;idx++ ){
                int level= GetLevel(idx);
            
            switch(level){
                case LEVEL_DINAS:
                       gridSPDDetail.Rows[idx].DefaultCellStyle = _hilightstyle;

                       break;
                case LEVEL_UNIT:
                       gridSPDDetail.Rows[idx].DefaultCellStyle = _level2style;
                       break;
                
                    case LEVEL_URUSAN:
                       gridSPDDetail.Rows[idx].DefaultCellStyle = _level3style;
                       break;
                
                    case LEVEL_PROGRAM:
                       gridSPDDetail.Rows[idx].DefaultCellStyle = _level4style;
                       break;
                   case LEVEL_KEGIATAN:
                       gridSPDDetail.Rows[idx].DefaultCellStyle = _level5style;
                       break;
                   case LEVEL_SUBKEGIATAN:
                       gridSPDDetail.Rows[idx].DefaultCellStyle = _level6style;
                       break;
                 }                

            }
        }
        private void frmSPDKepmen900_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Pembuatan SPD");
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            gridSPD.FormatHeader();
            gridSPDDetail.FormatHeader();
            LoadPejabatPPKD();
            gridSPD.FormatHeader();
        }

        private void LoadPejabatPPKD()
        {

            PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
            Pejabat kepalaPPKD = new Pejabat();
            kepalaPPKD = oLogic.GetKepalaPPKD();
            if (kepalaPPKD != null)
            {
                txtNamaPPKD.Text = kepalaPPKD.Nama;
                //txtJabatanKepalaPPKD.Text = kepalaPPKD.Jabatan;
                txtNIPPPKD.Text = kepalaPPKD.NIP;
                //m_IDPPKD = kepalaPPKD.ID;
            }


        }
        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            try
            {
                m_IDSKPD = pID;
                LoadSPD();
                //  GetKegiatan();
                gridSPDDetail.Rows.Clear();
                m_bNew = false;
                if (LoadProgramKegiatan())
                {
                    m_iJenis = 3;
                    m_iJenisLama = m_iJenis;
                    m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                    if (m_lstSemuaProgramKegiatan == null)
                    {
                        MessageBox.Show("Ada kesalahan pemanggilan data.");
                        return;
                    }
                    m_lstProgramKegiatan = m_lstSemuaProgramKegiatan.FindAll(x => x.Jenis == m_iJenis);

                    DisplayProgramKegiatanSubKegiatan();

                  
                    txtJumlah.Text = "0";

                    FormatGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("On Load:" + ex.Message);
            }
        }
        private bool LoadSPD()
        {
            try
            {
                SPDLogic oLogic = new SPDLogic(GlobalVar.TahunAnggaran);
                int bPPKD = 0;
                mListSPD = new List<SPD>();
                mListSPD = oLogic.Get(m_IDSKPD, (int)GlobalVar.TahunAnggaran, bPPKD);
                if (mListSPD  == null)
                    return false;

             

                gridSPD.Rows.Clear();

                foreach (SPD s in mListSPD)
                {
                    string[] row = { s.NoUrut.ToString(), s.NoSPD, s.Tanggal.ToString("dd MMM yyyy"), s.Jumlah.ToRupiahInReport(), s.Status.ToString() };
                    gridSPD.Rows.Add(row);
                }

                m_lstSPDDetail = new List<SPDDetail>();
                m_lstSPDDetail = oLogic.GetDetailByDinas(m_IDSKPD);
                if (m_lstSPDDetail == null)
                {
                    MessageBox.Show("Kesalahan mengambil data detail..");
                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;

            }
        }

        private void EnolKan()
        {
            for (int i = 0; i < gridSPDDetail.Rows.Count; i++)
            {

                //treeGridProgram.Rows[i].Cells[2].Value = "0.00";
                gridSPDDetail.Rows[i].Cells[COL_SPDINI ].Value = "0.00";
                gridSPDDetail.Rows[i].Cells[COL_AKUMULASI].Value = "0.00";

            }
            txtJumlah.Text = "0";

        }
        private void HideColumnAnggaran(int colMustShow, bool Clicked= false)
        {

            if (Clicked == false)
            {
                rbPenyempurnaanABT.Checked = false;
                rbMurni.Checked = false;
                rbPergeseran.Checked = false;
                rbPerubahan.Checked = false;
            }
        
            gridSPDDetail.Columns[COL_ANGGARANMURNI].Visible = false;
            if (colMustShow == 0)
            {
                gridSPDDetail.Columns[COL_ANGGARANMURNI].Visible = true;
                if (Clicked== false)
                rbMurni.Checked = true;
            }
            gridSPDDetail.Columns[COL_ANGGARANGESER].Visible = false;
            if (colMustShow == 1)
            {
                gridSPDDetail.Columns[COL_ANGGARANGESER].Visible = true;
                if (Clicked == false)
                rbPergeseran.Checked = true ;
            }
            gridSPDDetail.Columns[COL_ANGGARANRKAP].Visible = false;
            if (colMustShow == 2)
            {
                gridSPDDetail.Columns[COL_ANGGARANRKAP].Visible = true;
                if (Clicked == false)
                rbPerubahan.Checked = true;
            }
            gridSPDDetail.Columns[COL_ANGGARANABT].Visible = false;
            if (colMustShow == 3)
            {
                gridSPDDetail.Columns[COL_ANGGARANABT].Visible = true;
                if (Clicked == false)
                rbPenyempurnaanABT.Checked = true ;
            }

            
            

         }
        private void gridSPD_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
               
            
                if (m_bNew == true)
                {
                    MessageBox.Show("Mode mebuat SPD Baru tidak bisa menampilkan detail SPD ini.");
                    return;

                }
                 long lNoUrut = DataFormat.GetLong(gridSPD.Rows[e.RowIndex].Cells[0].Value);
                    m_NoUrut = lNoUrut;

                    if (lNoUrut == 0)
                        return ;



                    SPD oSPD = new SPD();
                    int idx = 0;
                    if (mListSPD == null)
                    {
                        return;
                    }
                    foreach (SPD spd in mListSPD){
                        if (spd.NoUrut == m_NoUrut)
                        {
                            oSPD = mListSPD[idx];
                            break;
                        }
                        idx = idx + 1;
                    }

                    if (DisplaySPD(oSPD) == true)
                    {

                        m_lstProgramKegiatan = m_lstSemuaProgramKegiatan.FindAll(x => x.Jenis == m_iJenis);
                        if (m_iJenis != m_iJenisLama)
                        {
                            DisplayProgramKegiatanSubKegiatan();
                            m_iJenisLama = m_iJenis;
                        }
                       
                        DisplayDetail();
                        decimal jumlah = m_lstDisplaySPD.Sum(x => x.Jumlah);
                        txtJumlah.Text = jumlah.ToRupiahInReport();
                        RefreshSisa();
                        FormatGrid();
                    }

            } catch(Exception ex){
                MessageBox.Show("Kesalahan ambil data SPD");
            }
        }
        private bool DisplaySPD(SPD oSPD){
            try{   
                
                EnolKan();

                m_bNew = false;
                ctrlBulan1.Create();
                if (oSPD.NamaBendahara.Trim().Length == 0)
                  GetBEndahara();
                 else
                   txtNamaBendahara.Text = oSPD.NamaBendahara;

       
                 m_iStatus = oSPD.Status;
                 txtINO.Text = oSPD.INoSPD.ToString();
                 txtPrefix.Text = oSPD.Prefix;
                 txtNoSPD.Text = oSPD.NoSPD;
                 int JenisAnggaran = oSPD.JenisAnggaran;


                 HideColumnAnggaran(JenisAnggaran); 

                 if (m_iStatus > 0)
                 {
                            button1.Enabled = false;
                            button2.Enabled = false;
                            cmdValid.Enabled = false;
                 }
                 else
                 {
                            button1.Enabled = true;
                            button2.Enabled = true;
                            cmdValid.Enabled = true;
                 }
                 txtKeterangan.Text = oSPD.Keterangan;
                 dtSPD.Value = oSPD.Tanggal;
                 ctrlBulan1.Create();
                 ctrlBulan1.SetID(oSPD.Bulan);
                 
                 cmbJenis.SelectedIndex = (int)oSPD.Jenis;
                 
                 m_iJenis = (int)oSPD.Jenis==0?3:5;

                 return true;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan menampilakn SPD" + ex.Message);
                return false;

            }

               
           
        }
        private bool DisplayDetail(bool bforNew = false )
        {
            try
            {
                SPDLogic spdLogic = new SPDLogic(GlobalVar.TahunAnggaran);

                m_lstDisplaySPD = new List<DisplaySPD>();
                int jenis = m_iJenis == 3 ? 0 : 1;
                if (m_lstSPDDetail == null || m_NoUrut ==0 )
                {
                    m_lstDisplaySPD = spdLogic.GetDisplaySPD(m_IDSKPD, m_NoUrut);
                }
                else
                {



                    if (bforNew == false)
                    {
                        var q = from d in m_lstSPDDetail
                                where d.NoUrut < m_NoUrut && d.Jenis== jenis
                                select new DisplaySPD
                                   {
                                       IDDInas = m_IDSKPD,
                                       IDUrusan = d.IDUrusan,
                                       IDProgram = d.IDProgram,
                                       IDKegiatan = d.IDKegiatan,
                                       IDSubkegiatan = d.IDSubkegiatan,
                                       IDRekening = d.IDRekening,
                                       KodeUK = d.KodeUK,
                                       Jumlah = 0,
                                       JumlahLalu = d.Jumlah
                                   };

                        var qKini = from d in m_lstSPDDetail
                                    where d.NoUrut == m_NoUrut && d.Jenis == jenis
                                    select new DisplaySPD
                                    {
                                        IDDInas = m_IDSKPD,
                                        IDUrusan = d.IDUrusan,
                                        IDProgram = d.IDProgram,
                                        IDKegiatan = d.IDKegiatan,
                                        IDSubkegiatan = d.IDSubkegiatan,
                                        IDRekening = d.IDRekening,
                                        KodeUK = d.KodeUK,
                                        Jumlah = d.Jumlah,
                                        JumlahLalu = 0
                                    };

                        m_lstDisplaySPD = q.Union(qKini).ToList();

                    }
                    else
                    {
                        var q = from d in m_lstSPDDetail
                                select new DisplaySPD
                                {
                                    IDDInas = m_IDSKPD,
                                    IDUrusan = d.IDUrusan,
                                    IDProgram = d.IDProgram,
                                    IDKegiatan = d.IDKegiatan,
                                    IDSubkegiatan = d.IDSubkegiatan,
                                    IDRekening = d.IDRekening,
                                    KodeUK = d.KodeUK,
                                    Jumlah = 0,
                                    JumlahLalu = d.Jumlah
                                };

                        m_lstDisplaySPD = q.ToList();


                    }
                }



                if (m_lstDisplaySPD != null)
                {

                    ProsesSPDPerDinas();

                    List<int> lstKodeUK = new List<int>();
                    UnitKerjaLogic uKLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                    if (GlobalVar.gListOrganisasi== null)
                    {
                      
                        //List<Unit> lstUk = new List<Unit>();
                        GlobalVar.gListOrganisasi = uKLogic.GetBySKPD(m_IDSKPD);
                        if (GlobalVar.gListOrganisasi == null)
                        {
                            MessageBox.Show("Gagal Mengambil data Unit.");
                            return false;
                        }

                    }
                    if (GlobalVar.gListOrganisasi.FindAll(x => x.SKPD == m_IDSKPD).Count == 0)
                    {
                        GlobalVar.gListOrganisasi = uKLogic.GetBySKPD(m_IDSKPD);
                        if (GlobalVar.gListOrganisasi == null)
                        {
                            MessageBox.Show("Gagal Mengambil data Unit.");
                            return false;
                        }


                    }
                    foreach (Unit u in GlobalVar.gListOrganisasi)
                    {
                        if (u.SKPD == m_IDSKPD)
                        {
                            lstKodeUK.Add(u.UntAnggaran);
                        }
                    }

                    if (lstKodeUK.Count == 0)
                    {
                        lstKodeUK.Add(0);
                    }
                    int oldKodeuk = -1;
                    foreach (int i in lstKodeUK)
                    {
                        if (oldKodeuk != i)
                        {
                            ProsesSPDPerUK(i);
                            ProsesSPDPerurusan(i);
                            ProsesSPDPerProgram(i);
                            ProsesDisplayDetailPerKegiatan(i);
                            ProsesDisplayDetailPerSubKegiatan(i);
                            ProsesDisplayDetailPerRekening(i);
                            oldKodeuk = i;
                        }
                    }

                }
                else
                {
                    MessageBox.Show(spdLogic.LastError());
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan menampilkan detail SPD" + ex.Message);
                return false;
            }
            
        }
        private void ProsesSPDPerUK(int KodeUK)
        {
            var lstJumlah = m_lstDisplaySPD.Where (u=>u.KodeUK==KodeUK) .GroupBy(x => x.KodeUK )
                   .Select(x => new
                   {
                       KodeUK = x.Key,
                       JumlahLalu = x.Sum(y => y.JumlahLalu ),
                       JumlahKini = x.Sum(y => y.Jumlah )



                   }).ToList();


            List<DisplaySPD> lstJumlahSPDPerurusan = (from t in m_lstDisplaySPD
                                                      join j in lstJumlah
                                                      on t.KodeUK equals j.KodeUK
                                                      select new DisplaySPD
                                                      {
                                                                 KodeUK =j.KodeUK,
                                                                      IDUrusan = 0,
                                                                      IDProgram = 0,
                                                                      IDKegiatan = 0,
                                                                      IDSubkegiatan = 0,
                                                                      IDRekening = 0,
                                                                      JumlahLalu =j.JumlahLalu ,
                                                                      Jumlah=j.JumlahKini


                                                      }).ToList<DisplaySPD>();//.Distinct();// < DisplaySPD>();

            List<DisplaySPD> lstJumlahSPDPerurusanDistincted = new List<DisplaySPD>();
            int oldUrusan = 0;
            foreach (DisplaySPD u in lstJumlahSPDPerurusan)
            {

                if (u.KodeUK != oldUrusan)
                {

                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value != null &&
                            gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_UNIT
                                )
                            {

                                gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = u.JumlahLalu.ToRupiahInReport();
                                gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = u.Jumlah.ToRupiahInReport();
                            }
                        }
                    }
                    oldUrusan = u.IDUrusan;
                }

            }


        }
        private void ProsesSPDPerDinas()
        {
                  var lstJumlah = m_lstDisplaySPD.FindAll(x=>x.IDDInas==m_IDSKPD).GroupBy(g=>g.IDDInas)
                   .Select(x => new
                   {
                       IDDINAS  = x.Key,
                       JumlahLalu = x.Sum(y => y.JumlahLalu),
                       JumlahKini = x.Sum(y => y.Jumlah)
                       
                   }).ToList();     

                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (DataFormat.GetInteger(gridSPDDetail.Rows[idx].Cells[COL_IDDINAS].Value)== m_IDSKPD &&
                                DataFormat.GetInteger(gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_DINAS
                                )
                            {
                                if (lstJumlah.Count > 0)
                                {
                                    gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = lstJumlah[0].JumlahLalu.ToRupiahInReport();
                                    gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = lstJumlah[0].JumlahKini.ToRupiahInReport();
                                }
                                else
                                {
                                    gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = "0";
                                    gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = "0";
                                }
                                    

                                
                            }
                            break;
                        }
                    }
                 


        }
        private void ProsesSPDPerurusan(int KodeUK)
        {
            var lstJumlah = m_lstDisplaySPD.FindAll(sk => sk.KodeUK == KodeUK).GroupBy(x => x.IDUrusan)
                   .Select(x => new
                   {
                       IDUrusan = x.Key,
                       JumlahLalu = x.Sum(y => y.JumlahLalu),
                       JumlahKini = x.Sum(y => y.Jumlah)



                   }).ToList();

            List<DisplaySPD> lstOnThisUK = m_lstDisplaySPD.FindAll(x => x.KodeUK == KodeUK);

            List<DisplaySPD> lstJumlahSPDPerurusan = (from t in lstOnThisUK
                                                      join j in lstJumlah
                                                      on t.IDUrusan equals j.IDUrusan
                                                      select new DisplaySPD
                                                      {

                                                          IDUrusan = t.IDUrusan,
                                                          IDProgram = 0,
                                                          IDKegiatan = 0,
                                                          IDSubkegiatan = 0,
                                                          IDRekening = 0,
                                                          KodeUK = KodeUK,
                                                          JumlahLalu = j.JumlahLalu,
                                                          Jumlah = j.JumlahKini


                                                      }).ToList<DisplaySPD>();//.Distinct();// < DisplaySPD>();

            List<DisplaySPD> lstJumlahSPDPerurusanDistincted = new List<DisplaySPD>();
            int oldUrusan = -1;
            foreach (DisplaySPD u in lstJumlahSPDPerurusan)
            {

                if (u.IDUrusan != oldUrusan)
                {

                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_IDURUSAN].Value != null &&
                            gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {
                            if (gridSPDDetail.Rows[idx].Cells[COL_IDURUSAN].Value.ToString() == u.IDUrusan.ToString() &&
                                gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                               GetLevel(idx) == LEVEL_URUSAN
                                //DataFormat.GetInteger(gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_URUSAN
                                )
                            {

                                gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = u.JumlahLalu.ToRupiahInReport();
                                gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = u.Jumlah.ToRupiahInReport();
                            }
                        }
                    }
                    oldUrusan = u.IDUrusan;
                }

            }


        }
        private void ProsesSPDPerProgram(int KodeUK)
        {
            var lstJumlah = m_lstDisplaySPD.Where (d=>d.KodeUK==KodeUK ).GroupBy(x => x.IDProgram)
                   .Select(x => new
                   {
                       IDProg = x.Key,
                       JumlahLalu = x.Sum(y => y.JumlahLalu),
                       JumlahKini = x.Sum(y => y.Jumlah)

                   }).ToList();

            List<DisplaySPD> lstOnThisUK = m_lstDisplaySPD.FindAll(x => x.KodeUK == KodeUK);
            List<DisplaySPD> lstJumlahSPDPerProgram = (from t in lstOnThisUK
                                                      join j in lstJumlah
                                                      on t.IDProgram equals j.IDProg
                                                      select new DisplaySPD
                                                      {

    
                                                          NoUrut=0,
                                                          Jenis=0,

                                                          IDUrusan = t.IDUrusan,
                                                          IDProgram = t.IDProgram,
                                                          IDKegiatan = 0,
                                                          IDSubkegiatan = 0,
                                                          IDRekening = 0,
                                                          JumlahLalu = j.JumlahLalu,
                                                          Jumlah = j.JumlahKini


                                                      }).ToList<DisplaySPD>();
         

            var lst = lstJumlahSPDPerProgram
                   .Select(p => new { p.IDProgram, p.JumlahLalu, p.Jumlah })
                   .Distinct().ToList();


            foreach (var o in lst)
            {
                Console.WriteLine( o.IDProgram.ToString() + "," + o.JumlahLalu.ToString() +"," + o.Jumlah.ToString() );

            }
            //var MS = lstJumlahSPDPerProgram
            //      .Select(p => p.IDProgram)
            //      .Distinct().ToList();
            

            int oldProgram = -1;

           // foreach (DisplaySPD u in lstJumlahSPDPerProgram)
            foreach (var u in lst )
            {

                if (u.IDProgram != oldProgram)
                {
                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_IDPROGRAM].Value != null && gridSPDDetail.Rows[idx].Cells[COL_IDPROGRAM].Value != null)
                        {

                        
                            if (gridSPDDetail.Rows[idx].Cells[COL_IDPROGRAM].Value.ToString() == u.IDProgram.ToString() &&
                               gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_PROGRAM

                                )
                            {

                                gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = u.JumlahLalu.ToRupiahInReport();
                                gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = u.Jumlah.ToRupiahInReport();
                            }
                        }
                    }

                }

            }

        }
        private void ProsesDisplayDetailPerKegiatan(int KodeUK)
        {
            List<DisplaySPD> lstOnThisUK = m_lstDisplaySPD.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK.GroupBy(x => x.IDKegiatan)
                   .Select(x => new
                   {
                       IDKegiatan = x.Key,
                       JumlahLalu = x.Sum(y => y.JumlahLalu),
                       JumlahKini = x.Sum(y => y.Jumlah)

                   }).ToList();

       
            List<DisplaySPD> lstJumlahSPDPerKegiatan = (from t in lstOnThisUK
                                                       join j in lstJumlah
                                                       on t.IDKegiatan equals j.IDKegiatan
                                                       select new DisplaySPD
                                                       {


                                                           NoUrut = 0,
                                                           Jenis = 0,
                                                           KodeUK=KodeUK,
                                                           IDUrusan = t.IDUrusan,
                                                           IDProgram = t.IDProgram,
                                                           IDKegiatan = t.IDKegiatan ,
                                                           IDSubkegiatan = 0,
                                                           IDRekening = 0,
                                                           JumlahLalu = j.JumlahLalu,
                                                           Jumlah = j.JumlahKini


                                                       }).ToList<DisplaySPD>();


            var lst = lstJumlahSPDPerKegiatan
                   .Select(p => new { p.IDProgram,p.IDKegiatan, p.JumlahLalu, p.Jumlah })
                   .Distinct().ToList();


            //foreach (var o in lst)
            //{
            //    Console.WriteLine(o.IDProgram.ToString() + "," + o.JumlahLalu.ToString() + "," + o.Jumlah.ToString());

            //}
         

            int oldKegiatan  = -1;

            // foreach (DisplaySPD u in lstJumlahSPDPerProgram)
            foreach (var u in lst)
            {

                if (u.IDKegiatan != oldKegiatan)
                {
                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_IDKEGIATAN].Value != null && gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridSPDDetail.Rows[idx].Cells[COL_IDKEGIATAN].Value.ToString() == u.IDKegiatan.ToString() &&
                                gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                GetLevel(idx)==LEVEL_KEGIATAN

                                )
                            {

                                gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = u.JumlahLalu.ToRupiahInReport();
                                gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = u.Jumlah.ToRupiahInReport();
                            }
                        }
                    }
                    oldKegiatan = u.IDKegiatan;

                }

            }

        }
        private void ProsesDisplayDetailPerSubKegiatan(int KodeUK )
        {
            List<DisplaySPD> lstOnThisUK = m_lstDisplaySPD.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK.GroupBy(x => x.IDSubkegiatan)
                   .Select(x => new
                   {
                       IDSUBKegiatan = x.Key,
                       JumlahLalu = x.Sum(y => y.JumlahLalu),
                       JumlahKini = x.Sum(y => y.Jumlah)

                   }).ToList();


            List<DisplaySPD> lstJumlahSPDPerSubKegiatan = (from t in lstOnThisUK
                                                        join j in lstJumlah
                                                        on t.IDSubkegiatan equals j.IDSUBKegiatan
                                                        select new DisplaySPD
                                                        {


                                                            NoUrut = 0,
                                                            Jenis = 0,

                                                            IDUrusan = t.IDUrusan,
                                                            IDProgram = t.IDProgram,
                                                            IDKegiatan = t.IDKegiatan,
                                                            IDSubkegiatan = t.IDSubkegiatan,
                                                            IDRekening = 0,
                                                            JumlahLalu = j.JumlahLalu,
                                                            Jumlah = j.JumlahKini


                                                        }).ToList<DisplaySPD>();


            var lst = lstJumlahSPDPerSubKegiatan
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IDSubkegiatan,p.JumlahLalu, p.Jumlah })
                   .Distinct().ToList();


            foreach (var o in lst)
            {
                Console.WriteLine(o.IDProgram.ToString() + "," + o.JumlahLalu.ToString() + "," + o.Jumlah.ToString());

            }


            long  oldIdSubKegiatan = -1;

            // foreach (DisplaySPD u in lstJumlahSPDPerProgram)
            foreach (var u in lst)
            {

                if (u.IDSubkegiatan != oldIdSubKegiatan)
                {
                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_IDSUBKEGIATAN].Value != null && gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridSPDDetail.Rows[idx].Cells[COL_IDSUBKEGIATAN].Value.ToString() == u.IDSubkegiatan.ToString() &&
                               gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                DataFormat.GetInteger(gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value) == LEVEL_SUBKEGIATAN

                                )
                            {

                                gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = u.JumlahLalu.ToRupiahInReport();
                                gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = u.Jumlah.ToRupiahInReport();
                                if (m_NoUrut == 0)
                                {
                                    gridSPDDetail.Rows[idx].Cells[COL_STATUSUPDATE].Value = "0";
                                }
                                else
                                {
                                    gridSPDDetail.Rows[idx].Cells[COL_STATUSUPDATE].Value = "1";
                                }
                            }
                        }
                    }
                    oldIdSubKegiatan = u.IDSubkegiatan;
                }

               
            }

        }
        private void ProsesDisplayDetailPerRekening(int KodeUK)
        {
            List<DisplaySPD> lstOnThisUK = m_lstDisplaySPD.FindAll(x => x.KodeUK == KodeUK);
            var lstJumlah = lstOnThisUK
                  .GroupBy(x => new { x.IDSubkegiatan,x.IDRekening})
                   .Select(x => new
                   {
                       IDSUBKegiatan = x.Key.IDSubkegiatan ,
                       IDRekening = x.Key.IDRekening,
                       JumlahLalu = x.Sum(y => y.JumlahLalu ),
                       JumlahKini = x.Sum(y => y.Jumlah)

                   }).ToList();

          
            List<DisplaySPD> lstJumlahSPDPerSubKegiatanRekening = (from t in lstOnThisUK
                                                                   join j in lstJumlah
                                                           on t.IDSubkegiatan equals j.IDSUBKegiatan 
                                                           where t.IDRekening== j.IDRekening
                                                           select new DisplaySPD
                                                           {  
                                                               NoUrut = 0,
                                                               Jenis = 0,
                                                               IDUrusan = t.IDUrusan,
                                                               IDProgram = t.IDProgram,
                                                               IDKegiatan = t.IDKegiatan,
                                                               IDSubkegiatan = t.IDSubkegiatan,
                                                               IDRekening = t.IDRekening,
                                                               JumlahLalu = j.JumlahLalu,
                                                               Jumlah = j.JumlahKini


                                                           }).ToList<DisplaySPD>();


            var lst = lstJumlahSPDPerSubKegiatanRekening
                   .Select(p => new { p.IDProgram, p.IDKegiatan, p.IDSubkegiatan, p.IDRekening, p.JumlahLalu, p.Jumlah })
                   .Distinct().ToList();


            foreach (var o in lst)
            {
                Console.WriteLine(o.IDProgram.ToString() + "," + o.JumlahLalu.ToString() + "," + o.Jumlah.ToString());

            }


            long oldIdSubKegiatan = 0;
            long oldIdRekening = 0;
            // foreach (DisplaySPD u in lstJumlahSPDPerProgram)
            foreach (var u in lst)
            {

                if (u.IDSubkegiatan != oldIdSubKegiatan || u.IDRekening != oldIdRekening)
                {
                    for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                    {
                        if (gridSPDDetail.Rows[idx].Cells[COL_IDSUBKEGIATAN].Value != null && 
                            gridSPDDetail.Rows[idx].Cells[COL_IDREKENING].Value != null && 
                            gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value != null)
                        {


                            if (gridSPDDetail.Rows[idx].Cells[COL_IDSUBKEGIATAN].Value.ToString() == u.IDSubkegiatan.ToString() &&
                                gridSPDDetail.Rows[idx].Cells[COL_IDREKENING].Value.ToString() == u.IDRekening.ToString() &&
                                gridSPDDetail.Rows[idx].Cells[COL_KODEUK].Value.ToString() == KodeUK.ToString() &&
                                GetLevel(idx) == LEVEL_REKANING
                                //gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value.ToString() == "5"

                                )
                            {

                                gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value = u.JumlahLalu.ToRupiahInReport();
                                gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value = u.Jumlah.ToRupiahInReport();

                                if (u.Jumlah==0)
                                {
                                    gridSPDDetail.Rows[idx].Cells[COL_STATUSUPDATE].Value = "0";
                                }
                                else
                                {
                                    gridSPDDetail.Rows[idx].Cells[COL_STATUSUPDATE].Value = "1";
                                }
                            }
                        }
                    }
                    oldIdSubKegiatan = u.IDSubkegiatan;
                }


            }

        }
       
        private void GetBEndahara()
        {

            Pejabat oBendahara = ctrlSKPD1.GetBendahara(dtSPD.Value);

            txtNamaBendahara.Text = oBendahara.Nama ;
            txtNIPBEndahara.Text = oBendahara.NIP; 
            
        }

        public bool LoadProgramKegiatan()
        {

            try
            {

                //  m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);

                if (GlobalVar.gListProgramKegiatanRekeningAnggaran == null)
                {
                    GlobalVar.gListProgramKegiatanRekeningAnggaran = new List<ProgramKegiatanAnggaran>();
               }
               // if (GlobalVar.gListProgramKegiatanRekeningAnggaran.FindAll(p => p.IDDInas == m_IDSKPD).Count == 0)
                //{
                    List<ProgramKegiatanAnggaran> lst = new List<ProgramKegiatanAnggaran>();
                    m_lstSemuaProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                    m_lstSemuaProgramKegiatan = oLogic.GetByDInas(m_IDSKPD, 0);
                    //if (m_lstSemuaProgramKegiatan != null)
                    //{
                    //    foreach (ProgramKegiatanAnggaran p in m_lstSemuaProgramKegiatan)
                    //    {
                    //        GlobalVar.gListProgramKegiatanRekeningAnggaran.Add(p);
                    //        m_lstSemuaProgramKegiatan.Add(p);
                    //    }

                    //}
                    //else
                    //{
                    //    MessageBox.Show(oLogic.LastError());
                    //    return false;
                    //}


                ///
                //else
               // {
                 //   m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                  //  m_lstProgramKegiatan = GlobalVar.gListProgramKegiatanRekeningAnggaran.FindAll(p => p.IDDInas == m_IDSKPD);
                //}


                if (oLogic.IsError())
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }


        }
        private void DisplayProgramKegiatanSubKegiatan()
        {
            try
            {

                gridSPDDetail.Rows.Clear();
                // ***********************************

                
                if (m_lstProgramKegiatan.Count == 0)
                {
                    MessageBox.Show("Data Program Kegiatan belum berhasil dipanggil");
                    return;
                }
                

                
                var lstJumlahOPD = m_lstProgramKegiatan.FindAll(p=>p.IDDInas== m_IDSKPD ).GroupBy(x=>x.IDDInas)
                   .Select(x => new
                   {  IDDInas = x.Key,
                       JumlahMurni = x.Sum(y=>y.AnggaranMurni),
                       JumlahGeser = x.Sum(y => y.AnggaranGeser),
                       JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                       JumlahABT = x.Sum(y => y.AnggaranABT),

                   }).ToList();

                ProgramKegiatanAnggaran totalOPD = new ProgramKegiatanAnggaran
                                                                      {

                                                                          StrIDUrusan = "0",
                                                                          KodeUK=0,
                                                                          NamaUrusan = "",
                                                                          IDUrusan = 0,
                                                                          IDProgram = 0,
                                                                          IDKegiatan = 0,
                                                                          IDSubKegiatan = 0,
                                                                          IIDRekening = 0,
                                                                          AnggaranMurni = lstJumlahOPD[0].JumlahMurni,
                                                                          AnggaranGeser = lstJumlahOPD[0].JumlahGeser,
                                                                          AnggaranRKAP = lstJumlahOPD[0].JumlahRKAP,
                                                                          AnggaranABT = lstJumlahOPD[0].JumlahABT
                                                                      };


                string[] rowOPD ={
                             m_IDSKPD.ToString(), ctrlSKPD1.GetNamaSKPD(),
                             totalOPD.AnggaranMurni.ToRupiahInReport(), 
                             totalOPD.AnggaranGeser.ToRupiahInReport(), 
                             totalOPD.AnggaranRKAP.ToRupiahInReport(), 
                             totalOPD.AnggaranABT.ToRupiahInReport(), "0","0","0","0",
                             "0",
                             "0",
                             "0",
                             "0",
                             "0","0"};

                                    
                gridSPDDetail.Rows.Add(rowOPD);
                ProcessUnit();

            }catch(Exception ex){
                
                MessageBox.Show(ex.Message);

            }
       }
       private void ProcessUnit( )
        {
            try
            {
                int oldKodeUK = -1;
                List<ProgramKegiatanAnggaran> lstProgramKegiatanDinas= new List<ProgramKegiatanAnggaran>();
                int Jenis;
               
                List<ProgramKegiatanAnggaran> lstUnit = new List<ProgramKegiatanAnggaran>();
                lstProgramKegiatanDinas = m_lstProgramKegiatan.FindAll(x => x.IDDInas == m_IDSKPD );
                lstProgramKegiatanDinas.OrderBy(x => x.KodeUK);

                // ***********************************
                foreach (ProgramKegiatanAnggaran uk in lstProgramKegiatanDinas)
                {
                    if (oldKodeUK  != uk.KodeUK)
                    {
                        lstUnit.Add(uk);
                        oldKodeUK = uk.KodeUK;
                    }
                }

                var lstJumlahUK = lstProgramKegiatanDinas.GroupBy(x => x.KodeUK)
                  .Select(x => new
                  {
                      KodeUK = x.Key,
                      JumlahMurni = x.Sum(y => y.AnggaranMurni),
                      JumlahGeser = x.Sum(y => y.AnggaranGeser),
                      JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                      JumlahABT = x.Sum(y => y.AnggaranABT),

                  }).ToList();

                List<ProgramKegiatanAnggaran> lstUKDanAnggaran = (from k in lstUnit
                                                                  join j in lstJumlahUK
                                                                      on k.KodeUK equals j.KodeUK
                                                                      select new ProgramKegiatanAnggaran
                                                                      {

                                                                          KodeUK = k.KodeUK,
                                                                          NamaUK = k.NamaUK,
                                                                          AnggaranMurni= j.JumlahMurni,
                                                                          AnggaranGeser = j.JumlahGeser,
                                                                          AnggaranRKAP = j.JumlahRKAP,
                                                                          AnggaranABT = j.JumlahABT,


                                                                      }).ToList<ProgramKegiatanAnggaran>();

                oldKodeUK = -1;

                string parameterNamaUK = "";
                foreach (ProgramKegiatanAnggaran p in lstUKDanAnggaran)
                {
                    if ((p.KodeUK != oldKodeUK))
                    {
                        string[] rowOPD ={
                             m_IDSKPD.ToString(), "Unit " + p.NamaUK, 
                                            p.AnggaranMurni.ToRupiahInReport(), 
                                            p.AnggaranGeser.ToRupiahInReport(), 
                                            p.AnggaranRKAP.ToRupiahInReport(), 
                                            p.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_UNIT.ToString(),
                                            "0",
                                            "0",
                                            "0",
                                            "0",
                                            "0",p.KodeUK.ToString() };
                        if (p.KodeUK > 0)
                        {
                            parameterNamaUK = " (" + p.NamaUK + ")";
                            gridSPDDetail.Rows.Add(rowOPD);
                        }


                        ProcessUrusan(p.KodeUK, parameterNamaUK);
                      

                    }
                }
            } 
            catch (Exception ex)
            {

            }

         }

       private void ProcessUrusan ( int KodeUK, string NamaUK){

           try
           {

               int oldIdUrusan = 0;
               int oldIdProgram = 0;
               //  
           

               List<ProgramKegiatanAnggaran> lstUrusan = new List<ProgramKegiatanAnggaran>();
               List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();
               lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.IDDInas==m_IDSKPD && x.KodeUK == KodeUK );
               foreach (ProgramKegiatanAnggaran u in lstProgramKegiatanThisUK)
               {
                   if (oldIdUrusan != u.IDUrusan)
                   {
                       lstUrusan.Add(u);
                       oldIdUrusan = u.IDUrusan;
                   }
               }

               var lstJumlah = lstProgramKegiatanThisUK.GroupBy(x => x.IDUrusan)
                  .Select(x => new
                  {
                      IDUrusan = x.Key,
                      JumlahMurni = x.Sum(y => y.AnggaranMurni),
                      JumlahGeser = x.Sum(y => y.AnggaranGeser),
                      JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                      JumlahABT = x.Sum(y => y.AnggaranABT),

                  }).ToList();
               // *********************************************************



               List<ProgramKegiatanAnggaran> lstUrusanDanAnggaran = (from t in lstUrusan
                                                                     join j in lstJumlah
                                                                     on t.IDUrusan equals j.IDUrusan
                                                                     select new ProgramKegiatanAnggaran
                                                                     {
                                                                         StrIDUrusan = t.StrIDUrusan,
                                                                         NamaUrusan = t.NamaUrusan,
                                                                         IDUrusan = t.IDUrusan,
                                                                         IDProgram = 0,
                                                                         IDKegiatan = 0,
                                                                         IDSubKegiatan = 0,
                                                                         IIDRekening = 0,
                                                                         AnggaranMurni = j.JumlahMurni,
                                                                         AnggaranGeser = j.JumlahGeser,
                                                                         AnggaranRKAP = j.JumlahRKAP,
                                                                         AnggaranABT = j.JumlahABT,


                                                                     }).ToList<ProgramKegiatanAnggaran>();

               oldIdUrusan = 0;
               foreach (ProgramKegiatanAnggaran p in lstUrusanDanAnggaran)
               {
                   if ((p.IDUrusan != oldIdUrusan))
                   {

                       //TreeGridNode urusannode = treeGridProgram.Nodes.Add
                       string[] row ={

                             p.StrIDUrusan , "Urusan " + p.NamaUrusan +  NamaUK, 
                                                                                            p.AnggaranMurni.ToRupiahInReport(), 
                                                                                            p.AnggaranGeser.ToRupiahInReport(), 
                                                                                            p.AnggaranRKAP.ToRupiahInReport(), 
                                                                                            p.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_URUSAN.ToString(),
                                                                                            p.IDUrusan.ToString(),
                                                                                            p.IDProgram.ToString(),
                                                                                            p.IDKegiatan.ToString(),
                                                                                            p.IDSubKegiatan.ToString(),
                                                                                            p.IIDRekening.ToString(),KodeUK.ToString() };
                       gridSPDDetail.Rows.Add(row);

                       ProcessProgram(p.IDUrusan, KodeUK, NamaUK);
                       oldIdUrusan = p.IDUrusan;
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);

           }


        }
       private void ProcessProgram(int idUrusan , int KodeUK ,string NamaUK)
       {
           try
           {
             
               int oldIdProgram = -1;
               //  
                               List<ProgramKegiatanAnggaran> lstProgramKegiatanThisUK = new List<ProgramKegiatanAnggaran>();
                
                lstProgramKegiatanThisUK = m_lstProgramKegiatan.FindAll(x => x.IDDInas == m_IDSKPD && x.KodeUK == KodeUK && x.IDUrusan == idUrusan );
              
                 
                       List<ProgramKegiatanAnggaran> lstPDistinctrogram = new List<ProgramKegiatanAnggaran>();
               
                       foreach (ProgramKegiatanAnggaran program in lstProgramKegiatanThisUK)
                       {
                           if (oldIdProgram != program.IDProgram)
                           {
                               lstPDistinctrogram.Add(program);
                               oldIdProgram = program.IDProgram;
                           }
                       }
                       var lstJumlahProgram = lstProgramKegiatanThisUK.GroupBy(x => x.IDProgram)
                          .Select(x => new
                          {
                              IDProgram = x.Key,
                              JumlahMurni = x.Sum(y => y.AnggaranMurni),
                              JumlahGeser = x.Sum(y => y.AnggaranGeser),
                              JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                              JumlahABT = x.Sum(y => y.AnggaranABT),

                          }).ToList();
                       //*************************===
                       List<ProgramKegiatanAnggaran> lstProgramDanAnggaran = (from t in lstPDistinctrogram
                                                                              join j in lstJumlahProgram
                                                                              on t.IDProgram equals j.IDProgram
                                                                              select new ProgramKegiatanAnggaran
                                                                              {
                                                                                  StrIDProgram = t.StrIDProgram,
                                                                                  NamaProgram = t.NamaProgram,
                                                                                  IDProgram = t.IDProgram,
                                                                                  IDKegiatan = 0,
                                                                                  IDSubKegiatan = 0,
                                                                                  IIDRekening = 0,
                                                                                  AnggaranMurni = j.JumlahMurni,
                                                                                  AnggaranGeser = j.JumlahGeser,
                                                                                  AnggaranRKAP = j.JumlahRKAP,
                                                                                  AnggaranABT = j.JumlahABT,


                                                                              }).ToList<ProgramKegiatanAnggaran>();

                       //*****************************
                       oldIdProgram = -1;
                       foreach (ProgramKegiatanAnggaran pr in lstProgramDanAnggaran)
                       {
                           if (pr.IDProgram != oldIdProgram)
                           {
                               //TreeGridNode nodeprogram = urusannode.Nodes.Add( 

                               string[] rowProgram ={pr.StrIDProgram , pr.NamaProgram + NamaUK, 
                                                                                            pr.AnggaranMurni.ToRupiahInReport(),
                                                                                            pr.AnggaranGeser.ToRupiahInReport(), 
                                                                                            pr.AnggaranRKAP.ToRupiahInReport(),
                                                                                            pr.AnggaranABT.ToRupiahInReport(),"0","0","0",LEVEL_PROGRAM.ToString(),
                                                                                            pr.IDUrusan.ToString(),
                                                                                            pr.IDProgram.ToString(),
                                                                                            pr.IDKegiatan.ToString(),
                                                                                            pr.IDSubKegiatan.ToString(),
                                                                                            pr.IIDRekening.ToString(),KodeUK.ToString()};
                               gridSPDDetail.Rows.Add(rowProgram);

                               oldIdProgram = pr.IDProgram;
                               if (oldIdProgram == 0)
                               {
                                   oldIdProgram = 0;
                               }
                               ProcessKegiatan( oldIdProgram, idUrusan , KodeUK, NamaUK);
                               //     nodeprogram.Expand();
                           }
                       }
                   }

               

           
           catch (Exception ex)
           {

           }
       }

        private void ProcessKegiatan( int idProgram, int idurusan , int KodeUK, string NamaUK)
        {
            try
            {
                int oldKegiatan;
                oldKegiatan = -1;
                List<ProgramKegiatanAnggaran> lstKegiatan = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctKegiatan = new List<ProgramKegiatanAnggaran>();
                lstKegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDDInas == m_IDSKPD && keg.IDProgram == idProgram && keg.KodeUK == KodeUK );

                foreach (ProgramKegiatanAnggaran kegiatan in lstKegiatan)
                {
                    if (oldKegiatan != kegiatan.IDKegiatan)
                    {
                        lstDistinctKegiatan.Add(kegiatan);
                        oldKegiatan = kegiatan.IDKegiatan;
                    }
                }

                var lstJumlahKegiatan = lstKegiatan.GroupBy(x => x.IDKegiatan)
                .Select(x => new
                {
                    IDKegiatan = x.Key,
                    JumlahMurni = x.Sum(y => y.AnggaranMurni),
                    JumlahGeser = x.Sum(y => y.AnggaranGeser),
                    JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                    JumlahABT = x.Sum(y => y.AnggaranABT),

                }).ToList();

                List<ProgramKegiatanAnggaran> lstPKegiatanDanAnggaran = (from t in lstDistinctKegiatan
                                                                         join j in lstJumlahKegiatan
                                                                         on t.IDKegiatan equals j.IDKegiatan
                                                                         select new ProgramKegiatanAnggaran
                                                                         {
                                                                             StrIDKegiatan = t.StrIDKegiatan,
                                                                             NamaKegiatan = t.NamaKegiatan,
                                                                             IDKegiatan = t.IDKegiatan,
                                                                             IDSubKegiatan = 0,
                                                                             IIDRekening = 0,

                                                                             IDProgram = t.IDProgram,
                                                                             IDUrusan = t.IDUrusan,
                                                                             AnggaranMurni = j.JumlahMurni,
                                                                             AnggaranGeser = j.JumlahGeser,
                                                                             AnggaranRKAP = j.JumlahRKAP,
                                                                             AnggaranABT = j.JumlahABT,
                                                                         }).ToList<ProgramKegiatanAnggaran>();
                oldKegiatan = -1;
                foreach (ProgramKegiatanAnggaran keg in lstPKegiatanDanAnggaran)
                {
                    if (keg.IDKegiatan != oldKegiatan)
                    {

                        string[] rowkegiatan = { keg.StrIDKegiatan , keg.NamaKegiatan + NamaUK,
                                                                                             keg.AnggaranMurni.ToRupiahInReport(), 
                                                                                             keg.AnggaranGeser.ToRupiahInReport(), 
                                                                                             keg.AnggaranRKAP.ToRupiahInReport(), 
                                                                                             keg.AnggaranABT.ToRupiahInReport(), "0","0","0",LEVEL_KEGIATAN.ToString(),
                                                                                            keg.IDUrusan.ToString(),
                                                                                            keg.IDProgram.ToString(),
                                                                                            keg.IDKegiatan.ToString(),
                                                                                            keg.IDSubKegiatan.ToString(),
                                                                                            keg.IIDRekening.ToString(),KodeUK.ToString()};

                        gridSPDDetail.Rows.Add(rowkegiatan);

                        oldKegiatan = keg.IDKegiatan;
                        ProcessSubKegiatan( oldKegiatan,KodeUK,NamaUK);
                        //nodekegiatan.Expand();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void ProcessSubKegiatan( int idKegiatan, int KodeUK , string NamaUK)
        {
            long oldSubKegiatan;
            oldSubKegiatan = -1;
            
            List<ProgramKegiatanAnggaran> lstKSubegiatan = new List<ProgramKegiatanAnggaran>();
            List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
            lstKSubegiatan = m_lstProgramKegiatan.FindAll(keg => keg.IDKegiatan == idKegiatan 
                                                   && keg.IDDInas==m_IDSKPD
                                                  && keg.KodeUK == KodeUK);

            lstKSubegiatan.OrderBy(x => x.IDSubKegiatan);

            
            foreach (ProgramKegiatanAnggaran subkegiatan in lstKSubegiatan)
            {
                if (oldSubKegiatan != subkegiatan.IDSubKegiatan)
                {
                    lstDistinctSubKegiatan.Add(subkegiatan);
                    oldSubKegiatan = subkegiatan.IDSubKegiatan;
                }
            }

            var lstJumlahSubKegiatan = m_lstProgramKegiatan.FindAll(p => p.IDDInas == m_IDSKPD).GroupBy(x => x.IDSubKegiatan)
            .Select(x => new
            {
                IdSubKegiatan = x.Key,
                JumlahMurni = x.Sum(y => y.AnggaranMurni),
                JumlahGeser = x.Sum(y => y.AnggaranGeser),
                JumlahRKAP = x.Sum(y => y.AnggaranRKAP),
                JumlahABT = x.Sum(y => y.AnggaranABT),

            }).ToList();

            List<ProgramKegiatanAnggaran> lstPSubKegiatanDanAnggaran = (from t in lstDistinctSubKegiatan
                                                                        join j in lstJumlahSubKegiatan
                                                                        on t.IDSubKegiatan equals j.IdSubKegiatan
                                                                        select new ProgramKegiatanAnggaran
                                                                        {
                                                                            StrIDKegiatan = t.StrIDKegiatan,
                                                                            StrIDSubKegiatan = t.StrIDSubKegiatan,
                                                                            IDSubKegiatan = t.IDSubKegiatan,
                                                                            NamaSubKegiatan = t.NamaSubKegiatan,
                                                                            NamaKegiatan = t.NamaKegiatan,
                                                                            KodeUK = t.KodeUK,
                                                                            IDKegiatan = t.IDKegiatan,
                                                                            AnggaranMurni = j.JumlahMurni,
                                                                            AnggaranGeser = j.JumlahGeser,
                                                                            AnggaranRKAP = j.JumlahRKAP,
                                                                            AnggaranABT = j.JumlahABT,
                                                                        }).ToList<ProgramKegiatanAnggaran>();
            oldSubKegiatan = -1;
            foreach (ProgramKegiatanAnggaran subkeg in lstPSubKegiatanDanAnggaran)
            {
                if (subkeg.IDSubKegiatan != oldSubKegiatan)
                {

                    string[] strSubkegiatan = {subkeg.StrIDSubKegiatan ,subkeg.NamaSubKegiatan + NamaUK,
                                        subkeg.AnggaranMurni.ToRupiahInReport(), subkeg.AnggaranGeser.ToRupiahInReport(),
                                        subkeg.AnggaranRKAP.ToRupiahInReport(), subkeg.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_SUBKEGIATAN.ToString(),
                                                                                            subkeg.IDUrusan.ToString(),
                                                                                            subkeg.IDProgram.ToString(),
                                                                                            subkeg.IDKegiatan.ToString(),
                                                                                            subkeg.IDSubKegiatan.ToString(),
                                                                                            subkeg.IIDRekening.ToString(),KodeUK.ToString()};
                    gridSPDDetail.Rows.Add(strSubkegiatan);
                    oldSubKegiatan = subkeg.IDSubKegiatan;
                    ProcessRekening(oldSubKegiatan,KodeUK);
                    

                }
            }
        }
        private void ProcessRekening( long idSubKegiatan, int KodeUK)
        {
            try
            {
                List<ProgramKegiatanAnggaran> lstRekening = new List<ProgramKegiatanAnggaran>();
                List<ProgramKegiatanAnggaran> lstDistinctSubKegiatan = new List<ProgramKegiatanAnggaran>();
                lstRekening = m_lstProgramKegiatan.FindAll(rek => rek.IDSubKegiatan == idSubKegiatan 
                                              && rek.IDDInas == m_IDSKPD
                                              && rek.KodeUK == KodeUK);

                lstRekening.OrderBy(x => x.IIDRekening);

                foreach (ProgramKegiatanAnggaran rek in lstRekening)
                {


                    string[] row = { rek.IIDRekening.ToKodeRekening() ,rek.NamaRekening,
                                    rek.AnggaranMurni.ToRupiahInReport(), rek.AnggaranGeser.ToRupiahInReport(),
                                    rek.AnggaranRKAP.ToRupiahInReport(), rek.AnggaranABT.ToRupiahInReport(), "0", "0", "0", LEVEL_REKANING.ToString(),
                                                                                            rek.IDUrusan.ToString(),
                                                                                            rek.IDProgram.ToString(),
                                                                                            rek.IDKegiatan.ToString(),
                                                                                            rek.IDSubKegiatan.ToString(),
                                                                                            rek.IIDRekening.ToString(), KodeUK.ToString(),"0"};
                    gridSPDDetail.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdPanggailDariDatabase_Click(object sender, EventArgs e)
        {
            try{
                m_lstProgramKegiatan = new List<ProgramKegiatanAnggaran>();
                ProgramKegiatanAnggaranLogic oLogic = new ProgramKegiatanAnggaranLogic(GlobalVar.TahunAnggaran);
                 m_lstProgramKegiatan  = oLogic.GetByDInas(m_IDSKPD, 0);

            } catch(Exception ex){
                MessageBox.Show("Kesalahan memanggil Anggaran" + ex.Message);

            }
                

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private int GetKolomANggaranAktiv()
        {
            int colAnggaran = 3;
            if (rbMurni.Checked == true)
                colAnggaran = COL_ANGGARANMURNI;
            if (rbPergeseran.Checked == true)
            {
                colAnggaran = COL_ANGGARANGESER;
            }
            if (rbPerubahan.Checked == true)
            {
                colAnggaran = COL_ANGGARANRKAP;
            }
            if (rbPenyempurnaanABT.Checked == true)
            {
                colAnggaran = COL_ANGGARANABT;
            }
            return colAnggaran;

        }
        private void gridSPDDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == COL_SPDINI)
            {

                int colAnggaran = GetKolomANggaranAktiv();
                decimal _JumlahAnggaran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[e.RowIndex].Cells[colAnggaran].Value));                
                decimal _JumlahSPDLalu = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[e.RowIndex].Cells[COL_AKUMULASI].Value));
                decimal _jumlahSPD = DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
               if (_JumlahAnggaran < (_JumlahSPDLalu + _jumlahSPD))
                {
                    MessageBox.Show("Akumulasi SPD Tidak Boleh Melebihi Anggaran.");
                    gridSPDDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                    return ;

                }
                else
                {
                    gridSPDDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _jumlahSPD.ToRupiahInReport();

                }



            }

            // HitungJumlah();
            HitungJumlahSubKegiatan(e.RowIndex);
            HitungJumlahKegiatan(e.RowIndex);
            HitungJumlahProgram(e.RowIndex);         
            HitungJumlahUrusan(e.RowIndex);
            HitungJumlahUnit(e.RowIndex);
            HitungJumlah();
        }
        private void HitungJumlah()
        {
            try
            {
                decimal Jumlah = 0;
                int rowAtas = 2;
                int rowBawah = gridSPDDetail.Rows.Count - 1;
                for (int row = rowAtas; row <= rowBawah; row++)
                {
                    if (GetLevel(row) == LEVEL_REKANING)
                    {
                        Jumlah = Jumlah + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));
                    }



                }
                gridSPDDetail.Rows[0].Cells[COL_SPDINI].Value = Jumlah.ToRupiahInReport();

                int colAnggaran = GetKolomANggaranAktiv();
                m_cJumlahDPA = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[colAnggaran].Value));
                m_cJumlahSPDSebelum = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[COL_AKUMULASI].Value));
                m_cJumlahSISADANASEBELUM = m_cJumlahDPA - m_cJumlahSPDSebelum;// DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[1].Value)) - DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[2].Value));
                m_cJumlahSPD = GetJumlahSPDKini();// DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[3].Value));
                m_cJumlahTotalSPD = m_cJumlahSPDSebelum + m_cJumlahSPD;// DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[3].Value)) + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[0].Cells[2].Value));
                m_cJumlahSISADPA = m_cJumlahDPA - m_cJumlahTotalSPD;

                txtJumlah.Text = m_cJumlahSPD.ToRupiahInReport();// _cJumlah.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private long GetRekeningOnRow(int i)
        {
            return DataFormat.GetLong(gridSPDDetail.Rows[i].Cells[COL_IDREKENING].Value);
        }
        private decimal GetJumlahSPDKini()
        {
            decimal cJumlah = 0;

            for (int row = 0; row < gridSPDDetail.Rows.Count; row++)
            {

                if (DataFormat.GetInteger(gridSPDDetail.Rows[row].Cells[COL_LEVEL].Value) == LEVEL_REKANING)
                {
                    //Console.WriteLine(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[2].Value));
                    cJumlah = cJumlah + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));

                }
            }
            return cJumlah;

        }
        private bool GetJumlah()
        {

            try
            {

                m_cJumlahDPA = 0;
                int colAnggaran = GetKolomANggaranAktiv();
                m_cJumlahSPDSebelum = 0;
                m_cJumlahSPD = 0;
                for (int row = 0; row < gridSPDDetail.Rows.Count; row++)
                {

                    if (DataFormat.GetInteger(gridSPDDetail.Rows[row].Cells[COL_LEVEL].Value) == LEVEL_REKANING)
                    {
                        m_cJumlahDPA = m_cJumlahDPA + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[colAnggaran].Value));
                        m_cJumlahSPDSebelum = m_cJumlahSPDSebelum + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_AKUMULASI].Value));
                        m_cJumlahSPD = m_cJumlahSPD + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));



                    }
                }
                m_cJumlahTotalSPD = m_cJumlahSPDSebelum+ m_cJumlahSPD;


                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private int GetLevel(int Baris)
        {
            return DataFormat.GetInteger(gridSPDDetail.Rows[Baris].Cells[COL_LEVEL].Value);
        }

        private void HitungJumlahUnit(int baris)
        {
            try
            {
                decimal JumlahUnit = 0;
                // Dapatkan 
                int barisUnit = baris;
                int rowAtas = baris;
                bool found = false;// beberapa opd tidak ada unit. jadi tidak perlu
                for (int i = baris; i > 0; i--)
                {
                    if (GetLevel(i) == LEVEL_UNIT)
                    {
                        rowAtas = i + 1;
                        barisUnit = i;
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    return;
                }
                int rowBawah = baris;
                for (int i = baris; i < gridSPDDetail.Rows.Count; i++)
                {
                    if (GetLevel(i) < LEVEL_URUSAN|| i == gridSPDDetail.Rows.Count)
                    {
                        rowBawah = i - 1;
                        break;
                    }

                }

                for (int row = rowAtas; row <= rowBawah; row++)
                {
                    if (GetLevel(row) == LEVEL_REKANING)
                    {
                        JumlahUnit = JumlahUnit + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));
                    }



                }
                gridSPDDetail.Rows[barisUnit].Cells[COL_SPDINI].Value = JumlahUnit.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HitungJumlahUrusan(int baris)
        {
            try
            {
                decimal JumlahUrusan = 0;
                // Dapatkan 
                int barisUrusan = baris;
                int rowAtas = baris;
                for (int i = baris; i > 0; i--)
                {
                    if (GetLevel(i) == LEVEL_URUSAN)
                    {
                        rowAtas = i + 1;
                        barisUrusan = i;
                        break;
                    }
                }

                int rowBawah = baris;
                for (int i = baris; i < gridSPDDetail.Rows.Count; i++)
                {
                    if (GetLevel(i) < LEVEL_PROGRAM || i == gridSPDDetail.Rows.Count ) 
                    {
                        rowBawah = i - 1;
                        break;
                    }
                 //   if (i == gridSPDDetail.Rows.Count-1)
              //      {
                 //       rowBawah = i ;
                //        break;
                   // }

                }

                for (int row = rowAtas; row <= rowBawah; row++)
                {
                    if (GetLevel(row) == LEVEL_REKANING)
                    {
                        JumlahUrusan = JumlahUrusan + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));
                    }



                }
                gridSPDDetail.Rows[barisUrusan].Cells[COL_SPDINI].Value = JumlahUrusan.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HitungJumlahProgram(int baris)
        {
            try
            {
                decimal JumlahPrg = 0;
                // Dapatkan 
                int barisPrg = baris;
                int rowAtas = baris;
                for (int i = baris; i > 0; i--)
                {
                    if (GetLevel(i) == LEVEL_PROGRAM)
                    {
                        rowAtas = i + 1;
                        barisPrg = i;
                        break;
                    }
                }

                int rowBawah = baris;
                for (int i = baris; i < gridSPDDetail.Rows.Count; i++)
                {
                    if (GetLevel(i) < LEVEL_KEGIATAN|| i == gridSPDDetail.Rows.Count)
                    {
                        rowBawah = i - 1;
                        break;
                    }


                }

                for (int row = rowAtas; row <= rowBawah; row++)
                {
                    if (GetLevel(row) == LEVEL_REKANING)
                    {
                        JumlahPrg = JumlahPrg + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));
                    }

                }
                gridSPDDetail.Rows[barisPrg].Cells[COL_SPDINI].Value = JumlahPrg.ToRupiahInReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HitungJumlahKegiatan(int baris)
        {
            try
            {
                decimal JumlahKeg = 0;
                // Dapatkan 
                int barisKeg = baris;
                int rowAtas = baris;
                for (int i = baris; i > 0; i--)
                {
                    if (GetLevel(i) == LEVEL_KEGIATAN)
                    {
                        rowAtas = i + 1;
                        barisKeg = i;
                        break;
                    }
                }

                int rowBawah = baris;
                for (int i = baris; i < gridSPDDetail.Rows.Count; i++)
                {
                    if (GetLevel(i) < LEVEL_SUBKEGIATAN || i == gridSPDDetail.Rows.Count)
                    {
                        rowBawah = i - 1;
                        break;

                    }

                }

                for (int row = rowAtas; row <= rowBawah; row++)
                {
                    if (GetLevel(row) == LEVEL_REKANING)
                    {
                        JumlahKeg = JumlahKeg + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));
                    }



                }
                gridSPDDetail.Rows[barisKeg].Cells[COL_SPDINI].Value = JumlahKeg.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HitungJumlahSubKegiatan(int baris)
        {
            try
            {

                decimal JumlahSub = 0;
                // Dapatkan 
                int barisSub = baris;
                int rowAtas = baris;
                for (int i = baris; i > 0; i--)
                {
                    if (GetLevel(i) == LEVEL_SUBKEGIATAN)
                    {
                        rowAtas = i + 1;
                        barisSub = i;
                        break;
                    }
                }

                int rowBawah = baris;
                for (int i = baris; i < gridSPDDetail.Rows.Count; i++)
                {
                    if (GetLevel(i) < LEVEL_REKANING|| i == gridSPDDetail.Rows.Count)
                    {
                        rowBawah = i - 1;
                        break;
                    }

                }
                for (int row = rowAtas; row <= rowBawah; row++)
                {
                    JumlahSub = JumlahSub + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value));
                }
                gridSPDDetail.Rows[barisSub].Cells[COL_SPDINI].Value = JumlahSub.ToRupiahInReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {
            int row = 0;
            try
            {

                if (ctrlSKPD1.GetID() == 0)
                {
                    MessageBox.Show("Silakan pilih Dinas Terlebih dahulu.");
                    return;
                }
                m_bNew = true;
                tabControl1.SelectedTab = tabControl1.TabPages[0];
                m_NoUrut = 0;
                row = 1;
                OnNew();
                row = 2;
                ctrlBulan1.Create();

                //ExpandAll();
                //if (m_bNew == true)
                EnolKan();
                row = 3;
                dtSPD.Value = DateTime.Now.Date;
                txtPrefix.Text = "BD/" + GlobalVar.TahunAnggaran.ToString();
                row = 4;
                //GetSPDLalu();
                DisplayDetail();
                ctrlBulan1.SetBlank();
                row = 5;
                cmdBatal.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(row.ToString() + " " + ex.Message);
            }
        }
        private void OnNew()
        {
            try
            {
                m_NoUrut = 0;

                ctrlBulan1.Create();
                txtNoSPD.Text = "";
                txtNamaBendahara.Text = "";
                txtNIPBEndahara.Text = "";
                txtKeterangan.Text = "";

                txtINO.Text = "";
                ctrlBulan1.SetBlank();

                ctrlBulan1.SetID(DateTime.Now.Month);

                DisplayDetail();

                button1.Enabled = true;

                GetBEndahara();


                cmdTambah.Enabled = false;
                cmdValid.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                //    button3.Enabled = false;
                cmbJenisRekening.Visible = false;
                lblJenisRekening.Visible = false;

                m_bNew = true;
                m_iStatus = 0;
            }catch (Exception ex){
                MessageBox.Show ("enolkan:"+ ex.Message );
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simpan();
            cmdBatal.Visible = false;
        }
        private void Simpan()
        {
            try
            {
                int barisError = 0;
                List<SPDDetail> lst = new List<SPDDetail>();
                if (m_iStatus > 0)
                {
                    MessageBox.Show("Data sudah valid. Tidak Bisa dirubah. Hanya");
                    return;
                }

                if (ctrlBulan1.IsValid() == false)
                {
                    MessageBox.Show("Bulan Belum  Dipilih");
                    return;
                }
                if (DataFormat.GetInteger(txtINO.Text) == 0)
                {
                    MessageBox.Show("Nomor Belum diisi..");
                    return;
                }
                if (txtNamaBendahara.Text.Length == 0)
                {
                    MessageBox.Show("Nama Bendahara asih kosong..");
                    return;
                }
                SPD oSPD = new SPD();
                SPDLogic oLogic = new SPDLogic(GlobalVar.TahunAnggaran);

                oSPD.Tahun = GlobalVar.TahunAnggaran;
                oSPD.IDDInas = ctrlSKPD1.GetID();
                oSPD.NoSPD = txtNoSPD.Text;
                oSPD.Bulan = ctrlBulan1.GetID();
                oSPD.Bulan2 = ctrlBulan1.GetID();

                oSPD.Jenis = cmbJenis.SelectedIndex;// +3;
                oSPD.PPKD = 0;
                oSPD.Triwulan = dtSPD.Value.Date.Triwulan();
                oSPD.Keterangan = txtKeterangan.Text;      
                oSPD.INoSPD = DataFormat.GetInteger(txtINO.Text);
                oSPD.Prefix = txtPrefix.Text;
                oSPD.NamaBendahara = DataFormat.GetString(txtNamaBendahara.Text);
                oSPD.NamaPPTK = DataFormat.GetString(txtNIPBEndahara.Text);
                oSPD.Tanggal = dtSPD.Value.Date;
                oSPD.KetentuanLain = "";
                oSPD.NoUrut = m_NoUrut;
                oSPD.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                oSPD.JenisAnggaran = BacaTahap();

                
                if (cmbJenisRekening.SelectedIndex == 0)
                    oSPD.JenisRekening = 2;
                else
                    oSPD.JenisRekening = 5;

                //int LevelToSave = LEVEL_REKANING;

                
                

                
                for (int i = 0; i < gridSPDDetail.Rows.Count; i++)
                {
                    if (GetLevel(i) == LEVEL_REKANING) { 
                        barisError = i;
                        SPDDetail oDetail = new SPDDetail();
                        oDetail.IDDInas = ctrlSKPD1.GetID();
                        oDetail.IDKegiatan = DataFormat.GetInteger(gridSPDDetail.Rows[i].Cells[COL_IDKEGIATAN].Value);
                        oDetail.KodeUK = DataFormat.GetInteger(gridSPDDetail.Rows[i].Cells[COL_KODEUK].Value);
                        oDetail.IDSubkegiatan = DataFormat.GetLong(gridSPDDetail.Rows[i].Cells[COL_IDSUBKEGIATAN].Value);

                        oDetail.IDProgram = DataFormat.GetInteger(gridSPDDetail.Rows[i].Cells[COL_IDPROGRAM].Value);
                        oDetail.IDUrusan = DataFormat.GetInteger(gridSPDDetail.Rows[i].Cells[COL_IDURUSAN].Value);
                        
                        oDetail.IDRekening = DataFormat.GetLong(gridSPDDetail.Rows[i].Cells[COL_IDREKENING].Value);
                        oDetail.Jumlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[i].Cells[COL_SPDINI].Value));
                        oDetail.StatusUpdate =  DataFormat.GetSingle(gridSPDDetail.Rows[i].Cells[COL_STATUSUPDATE].Value);

                        if (oDetail.Jumlah != 0 || oDetail.StatusUpdate == 1)
                        {
                            lst.Add(oDetail);
                        }

                    }
                }


                oSPD.ListDetail = lst;
                if (oLogic.Simpan(ref oSPD) == true)
                {
                    MessageBox.Show("Penyimpanan Selesai");
                    m_NoUrut = oSPD.NoUrut;



                    cmdValid.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                    cmdCetakLampiran.Enabled = true;
                    cmdTambah.Enabled = true;
                    m_bNew = false;
                    LoadSPD();
                    for (int row = 0; row < gridSPDDetail.Rows.Count; row++)
                    {
                        if (GetLevel(row) == LEVEL_REKANING &&
                            DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSPDDetail.Rows[row].Cells[COL_SPDINI].Value)) != 0)
                        {
                            gridSPDDetail.Rows[row].Cells[COL_STATUSUPDATE].Value = "1";
                        }
                    }

                }
                else
                {
                    MessageBox.Show(oLogic.LastError() + " " + barisError.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private int BacaTahap()
        {
            if (rbMurni.Checked == true)
                return 0;
            if (rbPergeseran.Checked == true)
                return 1;
            if (rbPerubahan.Checked == true)
                return 2;
            if (rbPenyempurnaanABT.Checked == true)
                return 3;
            return 0;


        }
        private void SetTahap(int iTahap)
        {
            switch (iTahap)
            {
                case 0:
                    rbMurni.Checked = true;
                    break;
                case 1:
                    rbPergeseran.Checked = true;
                    break;
                case 2:
                    rbPerubahan.Checked = true;
                    break;
                case 3:
                    rbPenyempurnaanABT.Checked = true;
                    break;

            }


        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            cmdTambah.Enabled = true;
            cmdValid.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = false;
            //    button3.Enabled = false;
            cmbJenisRekening.Visible = false;
            lblJenisRekening.Visible = false;

            m_bNew = true;
        }

        private void rbPergeseran_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTahapAnggaram();
        }
        private void RefreshTahapAnggaram()
        {
            int tahap = BacaTahap();
            HideColumnAnggaran(tahap, true );

            //switch
            //SetTahap(2);
        }

        private void rbMurni_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTahapAnggaram();
        }

        private void rbPerubahan_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTahapAnggaram();
        }

        private void rbPenyempurnaanABT_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTahapAnggaram();
        }

        private void txtINO_TextChanged(object sender, EventArgs e)
        {
            txtNoSPD.Text = DataFormat.GetInteger(txtINO.Text).ToString("00000") +"/" + txtPrefix.Text;
        }

        private void gridSPDDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridSPDDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (GetLevel(e.RowIndex) != LEVEL_REKANING || e.ColumnIndex != COL_SPDINI)
            {
                e.Cancel = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
             try
            {
                string namaFile = AppDomain.CurrentDomain.BaseDirectory + "SPD.docx";
                string newFIle = AppDomain.CurrentDomain.BaseDirectory + System.DateTime.Now.ToString() + "SPD.docx";
                List<TokenReplacementInfo> list = new List<TokenReplacementInfo>();
                HitungJumlah();
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Temp"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Temp");

                string n = DateTime.Now.ToString("ddMMMYYYY_HH_mm_ss");

                newFIle = AppDomain.CurrentDomain.BaseDirectory + "Temp\\" + n.Trim() + txtNoSPD.Text.Trim().Replace("/", "_") + "_SPD.docx";

                System.IO.File.Copy(namaFile, newFIle);
                TokenReplacement t = new TokenReplacement(newFIle, "[$", "$]");
                list.Add(new TokenReplacementInfo("[$DINAS$]", ctrlSKPD1.GetNamaSKPD()));
                list.Add(new TokenReplacementInfo("[$BENDAHARA$]", txtNamaBendahara.Text));
                list.Add(new TokenReplacementInfo("[$JUMLAH$]", m_cJumlahSPD.ToRupiahInReport()));
                list.Add(new TokenReplacementInfo("[$TERBILANG$]", m_cJumlahSPD.Terbilang()));
                list.Add(new TokenReplacementInfo("[$JUMLAHDPA$]", m_cJumlahDPA.ToRupiahInReport()));
                list.Add(new TokenReplacementInfo("[$SPDSEBELUM$]", m_cJumlahSPDSebelum.ToRupiahInReport()));
                list.Add(new TokenReplacementInfo("[$SISADANASEBELUM$]", m_cJumlahSISADANASEBELUM.ToRupiahInReport()));
                list.Add(new TokenReplacementInfo("[$JUMLAHSISADPA$]", m_cJumlahSISADPA.ToRupiahInReport()));
                list.Add(new TokenReplacementInfo("[$NOMOR$]", txtNoSPD.Text));
                list.Add(new TokenReplacementInfo("[$SISAANGGARAN$]", m_cJumlahSISADPA.ToRupiahInReport()));
                list.Add(new TokenReplacementInfo("[$BULAN$]", "Bulan " + ctrlBulan1.GetNama()));
                list.Add(new TokenReplacementInfo("[$TANGGAL$]", DataFormat.FormatTanggal(dtSPD.Value.Date)));

                t.replaceTokens(list);
                t.save();

                Microsoft.Office.Interop.Word.Application objWordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document objDoc = new Microsoft.Office.Interop.Word.Document();


                objDoc = objWordApp.Documents.Open(newFIle);
                objWordApp.Documents.Open(newFIle);
               
        } catch(Exception ex){
            MessageBox.Show(ex.Message);
        }
        }

        private void txtCari_Click(object sender, EventArgs e)
        {
            try
            {
                containingCells.Clear();
                currentContainingCellListIndex = 0;
                foreach (DataGridViewRow row in gridSPDDetail.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == DBNull.Value || cell.Value == null)
                            continue;
                        if (cell.Value.ToString().ToUpper().Contains(txtCari.Text.Trim().ToUpper()) && cell.Visible == true)
                        {
                            containingCells.Add(cell);
                        }
                    }
                }
                if (containingCells.Count > 0)
                    gridSPDDetail.CurrentCell = containingCells[currentContainingCellListIndex++];
                else
                    MessageBox.Show("Tidak diketemukan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdCariLagi_Click(object sender, EventArgs e)
        {
            if (containingCells.Count > 0 && currentContainingCellListIndex < containingCells.Count)
                gridSPDDetail.CurrentCell =
                        containingCells[currentContainingCellListIndex++];
        }

        private void cmdValid_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah benar akan mengesyahkan SPD ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SPDLogic oLogic = new SPDLogic(GlobalVar.TahunAnggaran);

                if (oLogic.Valid(m_NoUrut) == true)
                {
                    MessageBox.Show("SPD sudah valid dan bisa digunakan.");
                    m_iStatus = 1;
                    button1.Enabled = false;
                    cmdValid.Enabled = false;

                    m_bNew = false;
                    button2.Enabled = false;

                }
                else
                {
                    MessageBox.Show(oLogic.LastError());
                }
            }
        }

        private void cmdCetakLampiran_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Size = PdfPageSize.Legal;



                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Bottom= 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                halaman = 1;
                SaatnyacetakKesimpulan = false;
                ///*
                ///Header
                ///

                PdfGrid headerGrid = new PdfGrid();
                List<object> dataHeader = new List<object>();
                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                float kiri = 20;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = 10;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEMERINTAH KABUPATEN KETAPANG", 14, kiri, yPos, 
                    page.GetClientSize().Width , stringFormat, true, false, true);
 
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "TAHUN ANGGARAN "+
                         GlobalVar.TahunAnggaran.ToString(), 12, kiri, yPos,
                         page.GetClientSize().Width, stringFormat, true, false, true);

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "LAMPIRAN SPD NO" 
                        , 8, 620, yPos,
                         300, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtNoSPD.Text
                        , 8, 720, yPos,
                         150, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "BELANJA"
                        , 8, 620, yPos,
                         300, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ctrlSKPD1.GetNamaSKPD()
                        , 7, 720, yPos,
                         400, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PERIODE"
                        , 8, 620, yPos,
                         300, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, ctrlBulan1.NamaBulan.ToString()
                        , 8, 720, yPos,
                         150, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "TAHUN"
                        , 8, 620, yPos,
                         300, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.TahunAnggaran.ToString()
                        , 8, 720, yPos,
                         150, stringFormat, true, false, true);


                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                System.Data.DataTable table = new System.Data.DataTable();
                //Add columns to table
                table.Columns.Add("Kode ");
                table.Columns.Add("Uraian");
                table.Columns.Add("Anggaran");
                table.Columns.Add("Akumulasi SPD Sebelumnya");
                table.Columns.Add("Jumlah SPD pada Periode ini");
                table.Columns.Add("Akumulasi SPD sampai Periode Ini");
                table.Columns.Add("Sisa Anggaran");
                //table.Columns.Add("Level");



                //table. Columns[0]
                //Assign Column count
                int columnCount = table.Columns.Count;
                List<object> data = new List<object>();

                int colAnggaran = 3;
                if (rbMurni.Checked == true)
                    colAnggaran = COL_ANGGARANMURNI;
                if (rbPergeseran.Checked == true)
                {
                    colAnggaran = COL_ANGGARANGESER;
                }
                if (rbPerubahan.Checked == true)
                {
                    colAnggaran = COL_ANGGARANRKAP;
                }
                if (rbPenyempurnaanABT.Checked == true)
                {
                    colAnggaran = COL_ANGGARANABT;
                }

                decimal akumulasi = 0L;
                decimal sisa = 0;

                for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
                {
                    if (gridSPDDetail.Rows[idx].Cells[1].Value != null)
                    {
                        akumulasi = 0L;
                        sisa = 0;
                        akumulasi = DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value) +
                                    DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value);

                        sisa = DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[idx].Cells[colAnggaran].Value) - akumulasi; 
                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridSPDDetail.Rows[idx].Cells[0].Value),
                       DataFormat.GetString(gridSPDDetail.Rows[idx].Cells[1].Value),                      
                       DataFormat.GetString(gridSPDDetail.Rows[idx].Cells[colAnggaran].Value),
                       DataFormat.GetString(gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value),
                       DataFormat.GetString(gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value),
                       akumulasi.ToRupiahInReport(),
                       sisa.ToRupiahInReport(),
                        
                    });
                    }


                }
                if (GetJumlah() == true)
                {

                    table.Rows.Add(new string[]
                    {

                       "" ,
                        "J U M L A H",                      
                        
                        m_cJumlahDPA.ToRupiahInReport(),
                        m_cJumlahSPDSebelum.ToRupiahInReport(),
                        m_cJumlahSPD.ToRupiahInReport(),
                        m_cJumlahTotalSPD.ToRupiahInReport(),
                        (m_cJumlahDPA-m_cJumlahTotalSPD).ToRupiahInReport(),
                        
                        //DataFormat.GetString(gridSPDDetail.Rows[idx].Cells[COL_LEVEL].Value)

                        
                    });


                }

                //Add list to IEnumerable.
                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 95;
                pdfGrid.Columns[1].Width = 290;

                // Angka 
                pdfGrid.Columns[2].Width = 110;
                pdfGrid.Columns[3].Width = 100;
                pdfGrid.Columns[4].Width = 100;
                pdfGrid.Columns[5].Width = 100;
                pdfGrid.Columns[6].Width = 100;

                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(5, 5, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;
                pdfGrid.Columns[2].Format = formatKolomAngka;
                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;










                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 12));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();

                pdfGrid.RepeatHeader = true;
                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;

                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", font.Size, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                
                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;
                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }                
            
        
                for (int idx = 0; idx < pdfGrid.Rows.Count;idx++ )
                {
                    if (idx == pdfGrid.Rows.Count - 1)
                    {
                        pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10, FontStyle.Bold)); 
                    }
                    else
                    {

                        if (GetLevel(idx) == 0 || GetLevel(idx) == 1)
                        {
                            pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);
                        }

                        //                    if (pdfGrid.Rows[idx].Cells[7].Value == "2" || pdfGrid.Rows[idx].Cells[7].Value == "3")
                        if (GetLevel(idx) == 2 || GetLevel(idx) == 3)
                        {
                            pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 9, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);
                        }
                        if (GetLevel(idx) == 4 || GetLevel(idx) == 5)
                        {
                            pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);
                        }
                        if (GetLevel(idx) == 6 || GetLevel(idx) == 7)
                        {
                            pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8, FontStyle.Regular)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f);
                        }
                    }

                }

                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.

                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(kiri, yPos));

                
                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();
                //document.Pages. 
                //MemoryStream stream = new MemoryStream();
                //document.Save(stream);
                ////Closes the document.
                ////document.Save("header.pdf");

                //document.Close(true);
                ////Save and close the document

                ////This will open the PDF file so, the result will be seen in default PDF viewer
                //System.Diagnostics.Process.Start(stream);


                
                
                //string namaFile = "SPD" + txtNoSPD.Text;

                //document.Save(namaFile);
                //document.Close();

                //System.Diagnostics.Process.Start(namaFile);


                //string namaFile = Path.GetFullPath(@"../../../SPD_" + txtINO.Text.Trim() + "_" + ctrlSKPD1.GetNamaSKPD() + ".pdf");
                string namaFile = Path.GetFullPath(@"../../../SPD_" + txtINO.Text.Trim() + "_" + ctrlSKPD1.GetNamaSKPD() + ".pdf");

                //using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../SPD.pdf"), FileMode.Create, FileAccess.ReadWrite))
                using (FileStream outputFileStream = new FileStream(namaFile, FileMode.Create, FileAccess.ReadWrite))

                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);

                //
                if (chkDefaultPrinter.Checked == true)
                {
                    System.Diagnostics.Process.Start(namaFile);
                }
                else
                {
                    pdfViewer pV = new pdfViewer();
                    pV.Document = namaFile;// Path.GetFullPath(@"../../../BKU.pdf");
                    pV.Show();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Pages_PageAdded(object sender, PageAddedEventArgs args)
        {
            float yPos = PosisiTerakhir + 5;

            float setengah = (previousPage.GetClientSize().Width / 2) - 20;
            float posisiTengah =(previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();

            stringFormat.Alignment = PdfTextAlignment.Right ;
            CetakPDF oCetakPDF = new CetakPDF();
            Pejabat bendahara = new Pejabat ();
            bendahara= ctrlSKPD1.GetBendahara(dtSPD.Value);
            oCetakPDF.TulisItem(previousPage.Graphics, "Halaman " + halaman.ToString(), 8, posisiTengah, previousPage.GetClientSize().Height-5,
            setengah-3, stringFormat,false,false);
            halaman++;

            if (SaatnyacetakKesimpulan == true)
            {

               
                
                
                decimal jumlah = GetJumlahSPDKini();

                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "JUMLAH DANA BELANJA Rp," + jumlah.ToRupiahInReport(), 10, 20, yPos, setengah, stringFormat, false, false, true);
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Ditetapkan di Ketapang", 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                stringFormat.Alignment = PdfTextAlignment.Left;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Terbilang: " + jumlah.Terbilang(), 10, 20, yPos, setengah, stringFormat, false, false, true);
                stringFormat.Alignment = PdfTextAlignment.Center;

                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "Pada Tanggal "+ dtSPD.Value.ToTanggalIndonesia() , 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "PEJABAT PENGELOLA KEUANGAN DAERAH", 10, posisiTengah , yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "SELAKU BENDAHARA UMUM DAERAH", 10, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, txtNamaPPKD.Text , 10, posisiTengah, yPos + 30, setengah, stringFormat, true, true, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP "+ txtNIPPPKD.Text , 10, posisiTengah, yPos , setengah, stringFormat, true);
                
            }
            
            // yPos = TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtSPP.Value.ToTanggalIndonesia(), mfont10, posisiTengah, yPos,
           //setengah, m_stringFormatCenter, true);

           // yPos = TulisItem(graphics, m_oBendahara.Jabatan, mfont10, posisiTengah, yPos,
           //setengah, m_stringFormatCenter, true);


           // //Object grid2row5 = new { Name = "", Age = m_oBendahara.Nama };

           // yPos = TulisItem(graphics, m_oBendahara.Nama, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true, true);
           // yPos = TulisItem(graphics, m_oBendahara.NIP, mfont10, posisiTengah, yPos, setengah, m_stringFormatCenter, true);
            

            previousPage = args.Page;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (m_NoUrut == 0)
            {
                MessageBox.Show("Tidak ada SPD yang di pilih");
                return;
            }
            if (m_iStatus > 0)
            {
                MessageBox.Show("SPD sudah valid. Tidak bisa dihapus....");
                return;
            }
            if (MessageBox.Show("Apakah benar akan menghapus SPD No " + txtNoSPD.Text + "?", "Konfirmasi", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {

                SPDLogic oLogic = new SPDLogic(GlobalVar.TahunAnggaran);
                SPD oSPD = new SPD();
                oSPD.NoUrut = m_NoUrut;
                oSPD.IDDInas = ctrlSKPD1.GetID();


                if (oLogic.Hapus(ref oSPD) == true)
                {
                    m_NoUrut = 0;
                    txtNoSPD.Text = "";
                    ctrlBulan1.Create();
                    dtSPD.Value = DateTime.Now.Date;
                    LoadSPD();
                    MessageBox.Show("Penghapusan Selesai");
                }
                else
                {
                    MessageBox.Show("Penghapusan Gagal " + oLogic.LastError());
                }
            }
        }

        private void cmdBatalValid_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Apakah benar akan membatalkan status valid SPD ini?", "Confirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SPD oSPD = new SPD();
                    SPDLogic oLogic = new SPDLogic(GlobalVar.TahunAnggaran);
                    oSPD.Tahun = GlobalVar.TahunAnggaran;
                    oSPD.IDDInas = ctrlSKPD1.GetID();
                    oSPD.NoSPD = txtNoSPD.Text;
                    oSPD.Bulan = ctrlBulan1.GetID();
                    oSPD.Bulan2 = ctrlBulan1.GetID();
                    oSPD.Jenis = cmbJenis.SelectedIndex;
                    oSPD.Triwulan = 1;
                    oSPD.NamaBendahara = DataFormat.GetString(txtNamaBendahara.Text);
                    oSPD.NamaPPTK = DataFormat.GetString(txtNIPBEndahara.Text);
                    oSPD.Tanggal = dtSPD.Value.Date;
                    oSPD.KetentuanLain = "";
                    oSPD.NoUrut = m_NoUrut;
                    oSPD.Jumlah = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                    oSPD.Status = 0;

                    if (oLogic.UnValid(oSPD) == true)
                    {
                        MessageBox.Show("SPD sudah kembali dalam status draft.");
                        m_iStatus = 0;

                    }
                    else
                    {
                        MessageBox.Show(oLogic.LastError());
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }

        private void RefreshSisa()
        {
            decimal akumulasi = 0;
            decimal sisa = 0;

            int colAnggaran = GetKolomANggaranAktiv();

            for (int idx = 0; idx < gridSPDDetail.Rows.Count; idx++)
            {
                if (gridSPDDetail.Rows[idx].Cells[1].Value != null)
                {
                    akumulasi = 0L;
                    sisa = 0;
                    akumulasi = DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[idx].Cells[COL_AKUMULASI].Value) +
                                DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[idx].Cells[COL_SPDINI].Value);

                    sisa = DataFormat.FormatUangReportKeDecimal(gridSPDDetail.Rows[idx].Cells[colAnggaran].Value) - akumulasi;
                    gridSPDDetail.Rows[idx].Cells[COL_SISA].Value = sisa.ToRupiahInReport();

                }


            }

        }

        private void cmbJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_NoUrut == 0 && m_IDSKPD == 5020200)
            {
                m_iJenis = cmbJenis.SelectedIndex == 0 ? 3 : 5;
                m_lstProgramKegiatan = m_lstSemuaProgramKegiatan.FindAll(x => x.Jenis == m_iJenis);
                DisplayProgramKegiatanSubKegiatan();

            }
        }
    }
}
