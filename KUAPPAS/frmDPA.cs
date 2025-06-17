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
using BP.Bendahara;

namespace KUAPPAS
{
    public partial class frmDPA : Form
    {

        private Single _FormTahap = 0;
        private Single _FormMode = 0;
        private int mProfile;


        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _headerstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _lokasiStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _errorSTyle = new DataGridViewCellStyle();
        List<StandardBiaya> lSB = new List<StandardBiaya>();

       // DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
       // DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _wrongstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanMerahHeader = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanMerahDetail = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanHitamHeader = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanHitamDetail = new DataGridViewCellStyle();

        //private enum BatasAtasBawah
        //{
        //    int ROW_ATAS,
        //        int ROW_BAWAH,
        //};

        private string[] Message={
                                     "Pilih Kode Rekening di sebelah kiri,double klik,edit/tambah uraian. Simpan ",
                                     "Pilih Kode Rekening di sebelah kiri,double klik,edit/tambah uraian. Simpan ",
                                     "Pilih Program Kegiatan, Edit uraian Atau Tambah Kode Rekening di tab 'Kode Rekening' di sebelah kiri,double klik,edit/tambah uraian. Simpan ",
                                     "Pilih Kode Rekening di sebelah kiri,double klik,edit/tambah uraian. Simpan ",
                                     "Pilih Kode Rekening di sebelah kiri,double klik,edit/tambah uraian. Simpan "

                                  };
        


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
        private const int COL_JUMLAH = 16;
        private const int COL_VOLMURNI = 17;
        private const int COL_SATUANMURNI = 18;
        private const int COL_HARGAMURNI = 19;
        private const int COL_JUMLAHMURNI = 20;
        private const int COL_TAHAP = 21;
        private const int COL_IDANGGARANKAS = 22;
        private const int COL_ISNEW = 23;        
        private const int COL_SHOWINREPORT = 24;
        private const int COL_PLAFON = 25;
        private const int COL_YAD = 26;

        private const int COL_AKID=0;

        private const int COL_AKIDURUSAN=1;
        private const int COL_AKIDPROGRAM=2;
        private const int COL_AKIDKEGIATAN=3;
        private const int COL_AKIDREKENING=4;
        private const int COL_AKJENIS=5;
        private const int COL_AKKODE=6;
        private const int COL_AKNAMA=7;
        private const int COL_AKANGGARAN=8;
        private const int COL_AKJUMLAH=9;
        private const int COL_AKSISAANGGARAN = 10;
        private const int COL_AKJANUARI=11;
        private const int COL_AKFEBRUARI=12;
        private const int COL_AKMARET = 13;
        private const int COL_AKAPRIL = 14;
        private const int COL_AKMEI = 15;
        private const int COL_AKJUNI = 16;
        private const int COL_AKJULI = 17;
        private const int COL_AKAGUSTUS = 18;
        private const int COL_AKSEPTEMBER = 19;
        private const int COL_AKOKTOBER =20;
        private const int COL_AKNOVEMBER = 21;
        private const int COL_AKDESEMBER = 22;





        //private int _barisRekening;
        //private int _iKolomIDParentUraian;
        //private int _iKolomIDUraian;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private int m_IDUrusan;
        private long m_IDSubKegiatan;
        private decimal m_dPagu = 0L;
        
        delegate void SetComboBoxCellType(int iRowIndex, int _col, int _value);
        //bool bIsComboBox = false;
        //bool bDesaIsComboBox = false;
        private int m_iCurrentRow;
        private int m_iCurrentRowSB;
        private int m_iRowJustAdded;
        
        public frmDPA()
        {
            InitializeComponent();
            m_iCurrentRow = 0;

    //        InitializeComponent();
            

            _hilightstyle.Font = new Font(gridRekening.Font, FontStyle.Bold);
            _hilightstyle.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            _normalstyle.Font = new Font(gridRekening.Font, FontStyle.Regular);
            _normalstyle.BackColor = Color.White;

            _errorSTyle.Font = new Font(gridRekening.Font, FontStyle.Bold);
            _errorSTyle.BackColor = Color.Red ;// new Font(gridKUA.Font, FontStyle.Bold);

            _headerstyle.Font = new Font(gridRekening.Font, FontStyle.Bold);
            _headerstyle.BackColor = Color.LightGray;

            _lokasiStyle.ForeColor = Color.Red;


            _hilightstyle.Font = new Font(gridAnggaranKas.Font, FontStyle.Bold);
            _hilightstyle.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            _normalstyle.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _normalstyle.BackColor = Color.White;

            _wrongstyle.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _wrongstyle.ForeColor = Color.Red;

            _tulisanMerahHeader.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanMerahHeader.ForeColor = Color.Red;
            _tulisanMerahHeader.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);


            _tulisanMerahDetail.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanMerahDetail.ForeColor = Color.Red;
            _tulisanMerahDetail.BackColor = Color.White;// GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);


            _tulisanHitamHeader.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanHitamHeader.ForeColor = Color.Black;
            _tulisanHitamHeader.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);


            _tulisanHitamDetail.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanHitamDetail.ForeColor = Color.Black;
            _tulisanHitamDetail.BackColor = Color.White;// GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            mProfile = 3;


        }
        
        public void SetModeAndTahap(Single _mode, Single _tahap)
        {
            _FormMode = _mode;
            _FormTahap = _tahap;
            RefreshBasedFormMode();
        }
        private void frmRKA_Load(object sender, EventArgs e)
        {
           // ctrlHeader1.SetCaption("Input RKA", "");
            this.WindowState = FormWindowState.Maximized;
            ctrlDinas1.Create();
            treeRekening1.DoubleClickOnLeafOnly(true);
            ctrlStandardHarga1.Create(0,3);
            gridRekening.FormatHeader();
            LoadStandardHarga();
            lblAnggaranTahunLalu.Text = "Anggaran Tahun " + (GlobalVar.TahunAnggaran - 1).ToString();
            lblAnggaranTahunYAD.Text = "Anggaran Tahun " + (GlobalVar.TahunAnggaran + 1).ToString();

            //if (GlobalVar.Pengguna.IsUserDinas == 1)
            //{
                chkPlafon.Visible = false;
                chkPlafon.Checked = false;
            //} else {
            //    chkPlafon.Visible = true;
            
            //} 
                ctrlHeader1.SetCaption("      Input DPA/Cetak DPA", "       Menginput DPA. DPA bisa langsung menarik data dari RKA yang diinput....");

            // Hanya panggil..
                ctrlJenisAnggaran1.Create(2);
            treeRekening1.Create(999999999);
            


        }
        private void LoadStandardHarga()
        {
        }
        private void LoadAnggaran()
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            int _iJenis = ctrlJenisAnggaran1.GetID();
            if (m_IDUrusan == 0 && _iJenis==3)
                return;
            m_IDUrusan = m_IDUrusan == 0 ? DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 3)) : m_IDUrusan;
            int _bPPKD = (int)ctrlDinas1.PPKD();
            int idDinas = ctrlDinas1.GetID();

            lstRekening = oLogic.Get(GlobalVar.TahunAnggaran, idDinas, m_IDUrusan, m_IDProgram, m_IDKegiatan, _iJenis, _bPPKD, 2,1,0);
            gridRekening.Rows.Clear();
            
            m_iCurrentRow = 0;
            int _barisRekening = 0;
            foreach(TAnggaranRekening ta in lstRekening){
                //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                string[] row = { ta.IDRekening.ToString(), "-", "Hapus", "0", "0", "0", "0", "", "", ta.IDRekening.ToString().ToKodeRekening() , "", "0", ta.Nama, "0", "", "0", ta.JumlahDPA.ToRupiahInReport(), "0", "", "0", "0", "1","0","1","",ta.Plafon.ToRupiahInReport(),ta.JumlahYADAPBD.ToRupiahInReport()  };
                string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama };
                gridRekening.Rows.Add(row);
                _barisRekening = m_iRowJustAdded;                
                gridRekening.Rows[gridRekening.Rows.Count-2].DefaultCellStyle = _hilightstyle;
                foreach (TAnggaranUraian tu in ta.ListUraian)
                {                   // 0                      ,1    ,2      ,3                      ,4                   ,5   ,6                      , 7   ,8   ,9   ,10                   ,11                                       ,12                    ,13        ,14                        ,15                         ,16  ,17 ,18  ,19  ,20                       , 21                        ,22 ,23
                    string[] rowx = { tu.IDRekening.ToString(), "-", "Hapus", tu.IDLokasi.ToString(), tu.LevelDPA.ToString(), "0", tu.IDUraian.ToString(), "<<", ">>", "", tu.Label , tu.NoUrut.ToString(), MakeSpace((int)tu.Level - 1) + tu.Uraian , tu.VolMurni.ToString(), tu.Satuan, tu.HargaMurni.ToRupiahInReport(), tu.JumlahMurni.ToRupiahInReport(), "0", "", "0", "0", tu.TahapInput.ToString(), tu.IDAnggaranKAS.ToString(), "1", tu.ShowInReport == 1 ? "true" : "false",tu.Plafon.ToRupiahInReport(),tu.JumlahYADAPBD.ToRupiahInReport() };
                    gridRekening.Rows.Add(rowx);                  
                }
            //    HitungRekeningIni(_barisRekening);

            }
            HitungJumlahRKA();
      //      LoadKegiatan();
       //     FormatBaris();

           

        }
        private void LoadKegiatan()
        {
            TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
            TKegiatanAPBD oKeg = new TKegiatanAPBD();
            gridIndikator.Rows.Clear();

            oKeg = oLogic.GetKegiatan(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan,ctrlJenisAnggaran1.GetID(),2);
            if (oKeg != null)
            {
                txtKeterangan.Text = oKeg.Keterangan;
                txtKelompokSasaran.Text = oKeg.KelompokSasaran;
                txtLokasi.Text = oKeg.Lokasi;
                if (oKeg.TanggalPembahasan.Year < 2000)
                {
                    dtPembahasan.Value = DateTime.Now.Date;// oKeg.TanggalPembahasan;
                }
                else
                {
                    dtPembahasan.Value = oKeg.TanggalPembahasan;
                }
                txtAnggaranTahunLalu.Text = oKeg.AnggaranTahunLalu.FormatUang();
                txtAnggaranYAD.Text = oKeg.AnggaranTahunDepan.FormatUang();



                if (ctrlJenisAnggaran1.GetID() == 3)
                {
                    IndikatorLogic oIndikatorLogic = new IndikatorLogic(GlobalVar.TahunAnggaran,3);
                    List<Indikator> _lst = new List<Indikator>();
                    string _sNamaIndikator = "";
                    _lst = oIndikatorLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan,2);
                    gridIndikator.Rows.Clear();

                    foreach (Indikator i in _lst)
                    {
                        if (_sNamaIndikator != i.NamaJenis)
                        {
                            string[] row = { i.NamaJenis, i.iJenis.ToString(), i.sIndikator, i.Target };
                            gridIndikator.Rows.Add(row);
                            _sNamaIndikator = i.NamaJenis;
                        }
                        else
                        {
                            string[] rowx = { "", i.iJenis.ToString(), i.sIndikator, i.Target };
                            gridIndikator.Rows.Add(rowx);
                        }

                        
                    }
                }
                //CatatanKegiatanLogic oCatatanLogic = new CatatanKegiatanLogic(GlobalVar.TahunAnggaran);
                //List<CatatanKegiatan> _lstCatatan = new List<CatatanKegiatan>();
                //_lstCatatan = oCatatanLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, ctrlJenisAnggaran1.GetID());
                //gridCatatan.Rows.Clear();
                //if (_lstCatatan != null)
                //{
                //    foreach (CatatanKegiatan c in _lstCatatan)
                //    {
                //        string[] row = { "0", c.CatatanMurni };
                //        gridCatatan.Rows.Add(row);

                //    }

                //}
                oKeg.Jenis = 3;

                //TKegiatanAPBDLogic oKegiatanAPBDLogic = new TKegiatanAPBDLogic();
                //oKegiatanAPBDLogic.CekNAmaKegiatan(oKeg);


                LoadLokasi();
            }
        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            Kosongkan();


            if (ctrlJenisAnggaran1.GetID() > 0)
            {
                LoadAnggaran();
            }


        }
        private void ctrlJenisAnggaran1_OnChanged(int pID)
        {
            int _idDinas = ctrlDinas1.GetID();
            int _id = ctrlJenisAnggaran1.GetID();
            gridIndikator.Rows.Clear();
            gridRekening.Rows.Clear();
            cmdSetYAD.Visible = false;

            if (_idDinas < 10)
            {
                MessageBox.Show("Silakan Pilih Dinas Terlebih Dahulu..");
                return;
            }
             
            cmdCopy.Visible= false;
            tabProgram.TabPages.Remove(tabProgramKegiatan);
            tabProgram.TabPages.Remove(tabRincian);
            

            lblProgram.Visible = true;
            lblKegiatan.Visible = false;
            tabRekening.TabPages.Remove(tabIndikator);

            //picIndikator.Visible = false;
            //tabRekening.TabPages[2].Hide();
           // tabRekening.TabPages.Remove(tabIndikator);
            gridRekening.Columns[COL_YAD].Visible = false;
            m_IDKegiatan = 0;
            m_IDProgram = 0;
            m_IDUrusan = 0;
            switch (_id)
            {
                case 1:
                    if (GlobalVar.ProfileRekening.LEN == 7) 
                        treeRekening1.Create(4000000);
                    else
                        treeRekening1.Create(40000000);


                    lblProgram.Text = "RKA 1 (Pendapatan)";

                    LoadAnggaran();
                    LoadAnggaranKas();
                    break;
                case 2:
                    if (GlobalVar.ProfileRekening.LEN == 7) 
                        treeRekening1.Create(5100000);
                    else
                        treeRekening1.Create(51000000);

                    lblProgram.Text = "DPA 2.1 (Belanja Tidak Langsung)";
                    tabProgram.TabPages.Add(tabRincian);
                    gridRekening.Columns[COL_YAD].Visible = true;
                    cmdSetYAD.Visible = true;

                    LoadAnggaran();
                    LoadAnggaranKas();
                    break;

                case 3:
                    tabProgram.TabPages.Insert(0, tabProgramKegiatan);
                    this.tabProgram.SelectedTab = tabProgramKegiatan;


                    //tabProgram.TabPages.Remove(tabProgramKegiatan);
                    tabProgram.TabPages.Add(tabRincian);

                    TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                    oLogic.CekKUAdanKegiatan(GlobalVar.TahunAnggaran, ctrlDinas1.GetID());
                    
            //TProgramLogic oLogic = new TProgramLogic();

            //oLogic.CekProgramDinas(GlobalVar.TahunAnggaran, ctrlDinas1.GetID());

            //TKegiatanLogic oKLogic = new TKegiatanLogic();
            //oKLogic.CekKegiatanDinas(GlobalVar.TahunAnggaran, ctrlDinas1.GetID());
                    treeProgramKegiatan1.Create(ctrlDinas1.GetID(),0);

                    if (GlobalVar.ProfileRekening.LEN == 7) 
                        treeRekening1.Create(5200000);
                    else
                        treeRekening1.Create(52000000);

                    LoadAnggaran();
                    lblProgram.Visible = true ;
                    lblKegiatan.Visible = true;
                    tabRekening.TabPages.Insert(2,tabIndikator);
                 //   tabRekening.TabPages.Add(tabTimDPA);


                    //tabRekening.TabPages.Add("tabIndikator");

                    //tabRekening.TabPages[2].Show();
                    //picIndikator.Visible=true; 

                    lblProgram.Text = "";
                    lblKegiatan.Text = "";
                //   cmdCopy.Visible= true;
                    LoadAnggaran();

                    break;
                case 4:
                    lblProgram.Text = "RKA 3.1 (Penerimaan Pembiayaan)";
                    if (GlobalVar.ProfileRekening.LEN == 7) 
                        treeRekening1.Create(6100000);
                    else
                        treeRekening1.Create(61000000);

                    LoadAnggaran();
                    LoadAnggaranKas();
                    break;
                case 5:
                    lblProgram.Text = "RKA 3.2 (Pengeluaran Pembiayaan)";
                    if (GlobalVar.ProfileRekening.LEN == 7) 
                        treeRekening1.Create(6200000);
                    else
                        treeRekening1.Create(62000000);

                    LoadAnggaran();
                    LoadAnggaranKas();
                    break;
            }
            
            
            ShowHidIndikaor();
            LoadTimDPA();
            


        }
#region Tree Rekening Hadle
        private void treeRekening1_DoubleClicking(global::DTO.Rekening rek)
        {
            if (ctrlDinas1.GetID() == 0)
            {
                MessageBox.Show("Pilihan Dinasnya terlebih dahulu.");
                
                return;
            }

            // cek apakah sudah ada dalam grid.
            if (ctrlJenisAnggaran1.GetID() == 3)
            {
                if (m_IDKegiatan == 0 || m_IDProgram == 0 || m_IDUrusan == 0)
                {
                    MessageBox.Show("Program Kegiatan Belum dipilih. Silakan pilih Program terlebih dahulu.");
                    return;
                }
            }


            bool bFound = false;
            for (int id = 0; id < gridRekening.Rows.Count; id++)
            {
                if (rek.ID == DataFormat.GetLong(gridRekening.Rows[id].Cells[0].Value))
                {
                    bFound = true;
                }
            }
            if (bFound == false)
            {
                string[] row = { rek.ID.ToString(), "-", "Hapus", "0", "0", "0", "0", "", "", rek.Tampilan,"", "0", rek.Nama, "0", "", "0", "0", "0", "", "0", "0", GlobalVar.TahapAnggaran.ToString(),"0","0" };

                gridRekening.Rows.Add(row);
                gridRekening.Rows[gridRekening.Rows.Count-2].DefaultCellStyle = _hilightstyle;
            }
            else
                MessageBox.Show("Rekening sudah ada dalam RKA");
        }
#endregion Tree Rekening

        private int InsertRow(int currentrow)
        {



            string currentRekening = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[COL_IDREKENING].Value);
            string currentLokasi = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[COL_IDLOKASI].Value);
            string currentIDUraian = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[COL_IDURAIAN].Value);
            string currentParentIDUraian= DataFormat.GetString(gridRekening.Rows[currentrow].Cells[5].Value);
            int currentLevel = GetLevel(currentrow);//
            //DataFormat.GetInteger(gridRekening.Rows[currentrow].Cells[COL_LEVEL].Value);
            int iNoUrut = 0;

            currentLevel=currentLevel == 0 ? currentLevel+1 : currentLevel;

            for (int idx = 0; idx < currentrow; idx++)
            {
                if (GetLevel(idx) == 0)
                {
                    iNoUrut = 0;
                }
                else
                {
                    iNoUrut++;
                }
            }
            iNoUrut++;
            //               0              ,1   ,2       ,3   ,4                       ,5                     ,6               ,7    ,8    ,9  ,10                 , 11,12  , 13, 14 , 15 , 16 , 17, 18 , 19 , 20                               ,21,22                               
            string[] row = { currentRekening, "-", "Hapus", "0", currentLevel.ToString(), currentParentIDUraian, currentIDUraian, "<<", ">>", "","", iNoUrut.ToString(), "", "0", "", "0", "0", "0", "", "0", "0",GlobalVar.TahapAnggaran.ToString(),"0","0","true","0" };
            //string[] row = { currentRekening, currentLevel.ToString(), currentParentIDUraian, currentIDUraian, "<<", ">>" };
            gridRekening.Rows.Insert(currentrow  + 1, row);

            for (int idx = currentrow ; idx < gridRekening.Rows.Count; idx++)
            {
                if (GetLevel(idx) == 0)
                    iNoUrut = 0;
                    //break;
               gridRekening.Rows[idx].Cells[COL_NO].Value = ++iNoUrut;
            }
            m_iCurrentRow = currentrow;



            return 0;

        }



        private void gridRekening_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // BeginEdit
            if (GetLevel(e.RowIndex) == 0)
            {
                string msg;
                msg = "Baris ini tidak bisa di isi data.";
                if (GetIDRekening(e.RowIndex) == "0")
                {

                }
                if (e.ColumnIndex == COL_PLAFON)
                {
                    e.Cancel = false;
                }
                else
                {
                    MessageBox.Show("Baris ini tidak bisa di edit");
                    e.Cancel = true ;
                    return;
                }
            }

            if (e.ColumnIndex == COL_HARGA || e.ColumnIndex == COL_PLAFON)
            {
                if (gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    //DataGridViewCell _cell = gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    //DataFormat.FormatUangKeDecimal(ref _cell);
                    gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);


                }
            }
            if (e.ColumnIndex == COL_LEVEL && GetLevel(e.RowIndex) == 0 )
            {
                e.Cancel = true;
            }
        }

        private void gridRekening_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int _col = e.ColumnIndex;
            
            int _currentIDX = 0;
            int _currentParent = 0;
            string sUraian ;
            int iLevel ;
            //           0 ID
            //           1 +/-
            //2    Hapus
            //3  IDLokasi
            //4. Level
            //5...Induk
            //6...IDUraian

            //7... <<
            //8....>>
            //9....KodeRekening
            //10...No
            //11.. Uraian
            //12...VolMurni
            //13..Satuan Murni
            //14...Harga Murni
            //15...Jumlah Murni
            //16...Volume
            //17...Satuan  
            //18...Harga  
            //19..Jumlah  
            //20...Tahap
            if (e.RowIndex > -1)
            {
                sUraian = DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[COL_URAIAN].Value).Trim();
                iLevel  = DataFormat.GetInteger(gridRekening.Rows[e.RowIndex].Cells[COL_LEVEL].Value);

                if (e.ColumnIndex == COL_EXPAND)
                {
                    if (DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[COL_EXPAND].Value).Trim() == "-")
                    {
                        HideChild(e.RowIndex, false);
                        gridRekening.Rows[e.RowIndex].Cells[COL_EXPAND].Value = "+";
                    }
                    else
                    {
                        HideChild(e.RowIndex, true);
                        gridRekening.Rows[e.RowIndex].Cells[COL_EXPAND].Value = "-";
                    }
                }
                if (e.ColumnIndex == COL_HAPUS)
                {
                    if (MessageBox.Show("Benarakah akan menghapus baris ini?", "Konfirmasi Penghapusan", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        //if (gridRekening.SelectedRows.Count > 0)
                        //{

                        //    foreach (DataGridViewRow r in gridRekening.SelectedRows)
                        //    {
                        //        //Code to add selected row to new datagrid.
                        //        //Important to note that dataGridView2.Rows.Add(r) will not work 
                        //        //because each row can only belong to one data grid.  You'll have 
                        //        //to create a new Row with the same info for an exact copy
                                
                        //        gridRekening.Rows.Remove(r);

                        //    }
                        //}
                        //else
                        //{
                        if (GetTahap(e.RowIndex) == "0")
                        {
                            MessageBox.Show("Tidak Bisa dihapus");
                            return;
                        }
                        HapusBarisIni(e.RowIndex);
                        gridRekening.Rows.RemoveAt(e.RowIndex);

                       // }
                        HitungJumlahBasedOnThisRow(e.RowIndex);
                        HitungPerRekening();
                        
                        MessageBox.Show("Baris Sudah dihapus.", "KonfirmasiPenghapusan", MessageBoxButtons.OK);

                    }
                }

                if (e.ColumnIndex == COL_TORIGHT)
                {
                    if (GetLevel(e.RowIndex)> 0) {
                        if (BisaMaju(e.RowIndex))
                        {
                            iLevel++;
                            gridRekening.Rows[e.RowIndex].Cells[COL_LEVEL].Value = iLevel.ToString();
                            HitungJumlahBasedOnThisRow(e.RowIndex);
                            HitungPerRekening();
                            HitungJumlahRKA();
                        }
                    }
                }

                if (e.ColumnIndex == COL_TOLEFT)
                {
                    if (GetLevel(e.RowIndex) > 0)
                    {
                        if (BisaMundur(e.RowIndex))
                        {
                            //as
                            if (CekLevelBarisBerikutnya(e.RowIndex) == GetLevel(e.RowIndex))
                            {
                                if (MessageBox.Show("Apakah baris ini akan menjadi induk dari penjabaran barisberikutnya?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    gridRekening.Rows[e.RowIndex].Cells[COL_VOL].Value = "0";
                                    gridRekening.Rows[e.RowIndex].Cells[COL_SATUAN].Value = "";
                                    gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value = "0";
                                    gridRekening.Rows[e.RowIndex].Cells[COL_JUMLAH].Value = "0";
                                }
                            }

                            iLevel--;
                            gridRekening.Rows[e.RowIndex].Cells[COL_LEVEL].Value = iLevel.ToString();
                            gridRekening.Rows[e.RowIndex].Cells[COL_URAIAN].Value = MakeSpace(iLevel - 1) + DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[COL_URAIAN].Value).Trim();



                            //MakeSpace((int)tu.Level - 1) + tu.Uraian

                            HitungJumlahBasedOnThisRow(e.RowIndex);
                            HitungPerRekening();
                            HitungJumlahRKA();
                        }
                    }
                }                
            }

            //Jika ke kanan:
            // id yang sekarang jadi id parent. 
                // cek dengan id parent yang sekarang, berapa nilai maksimalnya
                // tambah satu: terus format string
            //Jika ke kiri
                // cek id parent, 
                // cari id parentdari parent tsb dan jadikan parent 
                // // cari maksimal id yang sekarang pada parent tsb dan tambah satu
                // format stringnya
                
            //no urutnya punya kolom sendiri

        }
        private bool HapusBarisIni(int i)
        {
            if (GetIsNEw(i) > 0)
            {
                if (GetLevel(i) == 0 && DataFormat.GetString(gridRekening.Rows[i].Cells[COL_DISPLAYREKENING].Value).Trim() != "")
                {
                    TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
                    TAnggaranRekening ta = new TAnggaranRekening();
                    ta = GetRekeningOnRow(i);
                    return oLogic.Hapus(ta);
                }
                else
                {

                    TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(GlobalVar.TahunAnggaran,3);
                    TAnggaranUraian ta = new TAnggaranUraian();
                    ta = GetUraianOnRow(i);
                    return oLogic.Hapus(ta);
                }
            } else 
                return true;
        }

        private void HideChild(int row, bool bHide)
        {
            int iLevel = GetLevel(row);

            for (int r = row+1; r< gridRekening.Rows.Count; r++){
                
                int rLevel = GetLevel(r);
                if (rLevel > iLevel)
                {
                    gridRekening.Rows[r].Visible = bHide;
                }
                else
                    break;

            }

        }
        private int CariRowInduk(int iRow)
        {
            int _ilevel = GetLevel(iRow);
            int _parentLevel;
            for (int i = iRow-1; i > 0; iRow--)
            {
                _parentLevel = GetLevel(i);
                if (_parentLevel - _ilevel == 1)
                {
                    return i;
                    
                }               

            }
            return 0;
        }
 
        private int GetLevel(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow <0 )
                return 0;

            if (gridRekening.Rows[iRow].Cells[COL_LEVEL].Value != null)
            {
                return DataFormat.GetInteger(gridRekening.Rows[iRow].Cells[COL_LEVEL].Value);
            }
            else return 0;
        }
        private bool BisaMundur(int iRow)
        {
            // mundur jika lebih dari 1
            if (GetLevel(iRow) <= 1) return false;

            return true;
        }
        private bool BisaMaju(int iRow)
        {
            int levelSebelum = GetLevel(iRow - 1);
            int iLevel = GetLevel(iRow);
            if (iLevel - levelSebelum > 0)
            {
                return false;
            }
            return true;
        }

        private string MakeSpace(int berapaKali)
        {
            string sRet=" ";
            for (int i = 0; i < berapaKali; i++)
            {
                sRet = sRet + "    ";
            }
            return sRet;
        }
        private int  GetID(int _iRow,int _iCol)
        {
            if (_iRow > -1 && _iRow < gridRekening.Rows.Count)
            {
                int _posAwal = 0;
                //int _posAkhir = 2;

                string _nilaiCell = DataFormat.GetString(gridRekening.Rows[_iRow].Cells[_iCol].Value);
                switch (_nilaiCell.Length)
                {
                    case 3:
                        _posAwal = 0;
                        //_posAkhir = 2;    
                        break;
                    case 6:
                        _posAwal = 3;
                        //_posAkhir = 5;
                        break;
                    case 9:
                        _posAwal = 6;
                        //_posAkhir = 8;
                        break;
                    case 12:
                        _posAwal = 9;
                        //_posAkhir = 11;
                        break;
                    case 15:
                        _posAwal = 12;
                        //_posAkhir = 14;
                        break;
                    case 18:
                        _posAwal = 15;
                        //_posAkhir = 17;
                        break;
                }
                return Convert.ToInt32(_nilaiCell.Substring(_posAwal, 3));
            }
            return 0;
        }

        private void gridRekening_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == COL_VOL )
            {
                decimal c = 0L;
                //string s = DataFormat.ToRupiahInReport(DataFormat.GetDecimal(gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value));

                //gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value = s;


                c = DataFormat.GetDecimal(gridRekening.Rows[e.RowIndex].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value));
                //gridRekening.Rows[e.RowIndex].Cells[COL_JUMLAH].Value = c.ToRupiah();
                //string xc = c.ToRupiah();
                gridRekening.Rows[e.RowIndex].Cells[COL_JUMLAH].Value =DataFormat.ToRupiahInReport( c);//.ToRupiahInReport();
                HitungJumlahBasedOnThisRow(e.RowIndex);
                HitungJumlahRKA();
            }
            if (e.ColumnIndex == COL_HARGA || e.ColumnIndex == COL_YAD)
            {
                decimal c = 0L;
                string s = DataFormat.ToRupiahInReport(DataFormat.GetDecimal(gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value));

                gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value = s;

                c = DataFormat.GetDecimal(gridRekening.Rows[e.RowIndex].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[COL_HARGA].Value));
                //gridRekening.Rows[e.RowIndex].Cells[COL_JUMLAH].Value = c.ToRupiah();
                //string xc = c.ToRupiah();
                gridRekening.Rows[e.RowIndex].Cells[COL_JUMLAH].Value = DataFormat.ToRupiahInReport(c);//.ToRupiahInReport();
                HitungJumlahBasedOnThisRow(e.RowIndex);
                HitungJumlahRKA();
            }

            if (e.ColumnIndex == COL_PLAFON )
            {
                if (gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewCell _cell = gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.ToRupiahInReport(DataFormat.GetDecimal(gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));

                    //DataFormat.FormatUang(ref _cell);//
                    
                    HitungJumlahBasedOnThisRow(e.RowIndex);
                    HitungJumlahRKA();

                }
            }
            if (e.ColumnIndex == COL_URAIAN)
            {
                if (gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string _sUraian = DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    _sUraian = MakeSpace(DataFormat.GetInteger(GetLevel(e.RowIndex)) - 1) + _sUraian;
                    gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _sUraian;
                }
            }
            if (e.ColumnIndex == COL_VOL || e.ColumnIndex == COL_SATUAN)
            {
                string sVal= DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
               // string sValToCopy = DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (sVal=="="){
                    if (e.RowIndex > 0)
                    {
                        string sValToCopy = DataFormat.GetString(gridRekening.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value);
                        gridRekening.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sValToCopy;

                    }

                }
                
            }




        }
        private void HitungJumlahRKA()
        {
            decimal cJumlah = 0L;
            decimal cJumlahPlafon = 0L;

            for (int i = 0 ; i < gridRekening.Rows.Count; i++)
            {
                if (GetLevel(i) == 0)
                {
                    cJumlah += DataFormat.FormatUangReportKeDecimal( DataFormat.GetString(gridRekening.Rows[i].Cells[COL_JUMLAH].Value));
                    cJumlahPlafon += DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_PLAFON].Value));
                }
            }
            
            lblJumlah.Text =DataFormat.ToRupiahInReport( cJumlah);//.ToRupiahInReportFormatUang();
            txtSelisih.Text = DataFormat.ToRupiahInReport(cJumlahPlafon - cJumlah);//.FormatUang();
            
            lblPagu.Text = cJumlahPlafon.ToRupiahInReport();

            

            if (cJumlah > m_dPagu)
            {
               // MessageBox.Show("Jumlah yang diinput melebihi Pagu");
                lblJumlah.BackColor = Color.Red;

            } else
            {
                lblJumlah.BackColor = Color.White;
            }
            gridIndikator.Rows[GetRowIndikatorMasukan()].Cells[3].Value = lblJumlah.Text ;

        }
        private void HitungPerRekening()
        {
            decimal cJumlah = 0L;
            int _barisRekening = 0;
            decimal cJumlahRKA = 0L;
            bool bBolehSImpan = true;
            for (int i = 0; i < gridRekening.Rows.Count; i++)
            {
                if (GetLevel(i) == 0)
                {

                    if (cJumlah > DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[_barisRekening].Cells[COL_JUMLAH].Value)))
                    {
                        
                        bBolehSImpan = bBolehSImpan || false;


                    }

                    cJumlahRKA = cJumlahRKA + cJumlah;

                    gridRekening.Rows[_barisRekening].Cells[COL_JUMLAH].Value = DataFormat.ToRupiahInReport( cJumlah);//.FormatUang();
                    _barisRekening = i;
                    cJumlah = 0;
                }
                if (DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal( DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value)) > 0)
                       cJumlah +=DataFormat.FormatUangReportKeDecimal( DataFormat.GetString(gridRekening.Rows[i].Cells[COL_JUMLAH].Value));
            }
            gridRekening.Rows[_barisRekening].Cells[COL_JUMLAH].Value =DataFormat.ToRupiahInReport(  cJumlah);//.FormatUang();
            cJumlahRKA = cJumlahRKA + cJumlah;

            lblJumlah.Text = DataFormat.ToRupiahInReport( cJumlahRKA);//.FormatUang();

            if (bBolehSImpan == false)
            {
                //MessageBox.Show("Jumlah Melebihi Rekening.");
                gridRekening.Rows[_barisRekening].DefaultCellStyle = _errorSTyle;
           //     cmdSimpanRekening.Enabled = false;
            } else {
                cmdSimpanRekening.Enabled = true;
                gridRekening.Rows[_barisRekening].DefaultCellStyle = _hilightstyle;
            }

            
            //lblJumlah.Text = cJumlah.FormatUang();


        }


        private void gridRekening_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            m_iCurrentRow = e.RowIndex;
            m_iCurrentRowSB = e.RowIndex;
        }

        private void gridRekening_KeyDown(object sender, KeyEventArgs e)
        {
// grid KeyDown
            try
            {
                if (e.Modifiers == Keys.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.C:
                            CopyToClipboard();
                            break;

                        case Keys.V:
                            //gridKUA.PasteInData( );// 
                            PasteClipboard();
                            //HitungJumlahBasedOnThisRow()

                            //HitungJumlahBL();
                            break;
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    // gridKUA.Rows.Add();
                    if (m_iCurrentRow > -1 && m_iCurrentRow< gridRekening.Rows.Count)
                    {
                        InsertRow(m_iCurrentRow); 
                        
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {

                    foreach (DataGridViewCell cell in gridRekening.SelectedCells)
                    {
                        
                        int rowIndex = cell.RowIndex;
                        int colIndex = cell.ColumnIndex;
                        if (colIndex >= COL_URAIAN && colIndex <= COL_HARGA)
                        {
                            gridRekening.Rows[rowIndex].Cells[colIndex].Value = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Copy/paste operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ctrlJenisAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void gridRekening_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            
            
            string sUraian ;
            int iLevel ;
            
            if (e.RowIndex > 0)
            {
                if (e.ColumnIndex==COL_LEVEL){
                    sUraian = DataFormat.GetString(gridRekening.Rows[e.RowIndex].Cells[COL_URAIAN].Value).Trim();
                    iLevel  = DataFormat.GetInteger(gridRekening.Rows[e.RowIndex].Cells[COL_LEVEL].Value);
                    if (iLevel > 0)
                    {
                        gridRekening.Rows[e.RowIndex].Cells[COL_URAIAN].Value = MakeSpace(iLevel - 1) + sUraian;
                    }
                    
                    HitungJumlahBasedOnThisRow(e.RowIndex);
                }
                if (e.ColumnIndex == COL_HARGA || e.ColumnIndex == COL_VOL || e.ColumnIndex == COL_JUMLAH)
                {

                    if (DataFormat.GetDecimal(GetVOL(e.RowIndex)) == 0 && DataFormat.FormatUangReportKeDecimal(GetHarga(e.RowIndex)) == 0 && DataFormat.GetDecimal(GetJumlah(e.RowIndex)) > 0 && GetLevel(e.RowIndex)>0)
                    {
                        gridRekening.Rows[e.RowIndex].DefaultCellStyle = _headerstyle;
                    }
                    else
                    {
                        if (GetLevel(e.RowIndex)>0)
                        gridRekening.Rows[e.RowIndex].DefaultCellStyle = _normalstyle;
                    }
                }


            }

        }
        private void FormatBaris()
        {

            for (int i = 0; i < gridRekening.Rows.Count; i++)
            {
                if (GetLevel(i) > 0)
                {
                    if (DataFormat.GetDecimal(GetVOL(i)) == 0 && DataFormat.FormatUangReportKeDecimal(GetHarga(i)) == 0)
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
        private void HitungJumlahBasedOnThisRow(int iRow)
        {
            // 
            //Jumlah akan mempengaruhi jumlah induknya dan seterusny sampai pada jumlah DPA Kegiatan.
            // 
            // Cari Parent...
            // For Hitung Jumlah parentnya. 
            // 
            //cari baris Rekening 
            int _barisRekening = 0;
            decimal _cJumlahYAD = 0m;

            decimal _cJumlahRekening = 0m;
            decimal _cJumlahPafon = 0m;

            if (chkPlafon.Checked == false)
            {
                for (int y = iRow - 1; y > 0; y--)
                {
                    if (GetLevel(y) == 0)
                    {
                        _barisRekening = y;
                        break;
                    }
                }
                int i = _barisRekening + 1;
                while (GetLevel(i) > 0)
                {
                    if (DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal( DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value)) == 0)
                    {
                        HitungBarisIni(i);
                    }
                    else
                    {
                        _cJumlahRekening = _cJumlahRekening + DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal( DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value));
                        //_cJumlahYAD = _cJumlahYAD + DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_YAD].Value);
                    }
                    i++;
                }
            }
            else
            {
                for (int y = iRow; y > 0; y--)
                {
                    if (GetLevel(y) == 0)
                    {
                        _barisRekening = y;
                        break;
                    }
                }
                if (_barisRekening >= 0)
                {
                    if (_barisRekening < iRow)
                    {
                        int i = _barisRekening + 1;
                        //for (int i = _barisRekening+1; i < gridRekening.Rows.Count; i++)
                        //{

                        while (GetLevel(i) > 0)
                        {
                            _cJumlahPafon = _cJumlahPafon + DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_PLAFON].Value);
                            i++;
                        }
                    }
                    else
                    {
                        _cJumlahPafon = DataFormat.GetDecimal(gridRekening.Rows[_barisRekening].Cells[COL_PLAFON].Value);
                    }
                }
                else
                {
                    _cJumlahPafon = DataFormat.GetDecimal(gridRekening.Rows[_barisRekening].Cells[COL_PLAFON].Value);

                }
            }
            if (chkPlafon.Checked == true)
            {
                gridRekening.Rows[_barisRekening].Cells[COL_PLAFON].Value = _cJumlahPafon.FormatUang();
            }
            else
            {
              
                cmdSimpanRekening.Enabled = true ;
                gridRekening.Rows[_barisRekening].Cells[COL_JUMLAH].Value =DataFormat.ToRupiahInReport( _cJumlahRekening);//.FormatUang();
        //        gridRekening.Rows[_barisRekening].Cells[COL_YAD].Value = _cJumlahYAD.FormatUang();

                if (_cJumlahRekening != DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[_barisRekening].Cells[COL_PLAFON].Value)))
                {
                   // MessageBox.Show("Jumlah Rekening melebihi Jumlah APBD.");                    
        //            cmdSimpanRekening.Enabled = false;                    
                    gridRekening.Rows[_barisRekening].DefaultCellStyle = _errorSTyle;
                } else{
                    gridRekening.Rows[_barisRekening].DefaultCellStyle = _hilightstyle;

                    

                }

            }   
             
        }
        private void HitungRekeningIni(int baris)
        {
            int _barisRekening = baris;

            int i = _barisRekening + 1;
            decimal _cJumlahRekening = 0;
            decimal _cJumlahYAD=0;

            while (GetLevel(i) > 0)
            {            
                if (DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value)) == 0)
                {
                    HitungBarisIni(i);
                }
                else
                {
                    _cJumlahRekening = _cJumlahRekening + DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value));
                    _cJumlahYAD = _cJumlahYAD + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_YAD].Value));
                }
                i++;
                

            }
            gridRekening.Rows[_barisRekening].Cells[COL_JUMLAH].Value =DataFormat.ToRupiahInReport( _cJumlahRekening);//.FormatUang();
            gridRekening.Rows[_barisRekening].Cells[COL_YAD].Value = DataFormat.ToRupiahInReport(_cJumlahYAD);//.FormatUang();

        }
        
        private void HitungBarisIni(int i){
            int _level = GetLevel(i);
            decimal cJumlah=0L;
            decimal cJumlahYAD = 0L;
                for (int row = i + 1; row < gridRekening.Rows.Count; row++)
                {
                    int _levelRow = GetLevel(row);

                    if (_levelRow <= _level)
                        break;

                    if (DataFormat.GetDecimal(gridRekening.Rows[row].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[row].Cells[COL_HARGA].Value)) > 0)
                    {
                        cJumlah = cJumlah + DataFormat.GetDecimal(gridRekening.Rows[row].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[row].Cells[COL_HARGA].Value));
                        cJumlahYAD = cJumlahYAD + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[row].Cells[COL_YAD].Value));

                    }
                    gridRekening.Rows[i].Cells[COL_JUMLAH].Value =DataFormat.ToRupiahInReport( cJumlah);//.FormatUang();
//                    gridRekening.Rows[i].Cells[COL_YAD].Value = cJumlahYAD.FormatUang();

                }
            
        }

        private bool CekDPAAPBD()
        {
            bool bIsOK = true;
            for (int i = 0; i < gridRekening.Rows.Count; i++)
            {
                if (GetLevel(i) == 0)
                {
                    if (DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_JUMLAH].Value)) != DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_PLAFON].Value)))
                    {
                        bIsOK =  false;
                        gridRekening.Rows[i].DefaultCellStyle = _errorSTyle;

                    }
                    else
                    {
                        gridRekening.Rows[i].DefaultCellStyle= _hilightstyle;
                    }
                }
            }
            
            return bIsOK;

        }
        private void cmdSimpanRekening_Click(object sender, EventArgs e)
        {

            //TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            //List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            //if (CekDPAAPBD() == false)
            //{
            //    MessageBox.Show("Ada rekening yang melebihi APBD. (Bandingkan kolom Jumlah dan Kolom Jumlah APBD pada tiap Rekening).");
                
            //   // return;

            //}
            
            //for (int i = 0; i < gridRekening.Rows.Count; i++)
            //{
            //    if (GetLevel(i) == 0  && DataFormat.GetString(gridRekening.Rows[i].Cells[COL_DISPLAYREKENING].Value).Trim()!="")
            //    {
                    
            //            TAnggaranRekening o = new TAnggaranRekening();
            //            o.Tahun = GlobalVar.TahunAnggaran;
            //            o.IDDinas = ctrlDinas1.GetID();
            //            o.Jenis = ctrlJenisAnggaran1.GetID();
            //            if (ctrlJenisAnggaran1.GetID() != 3)
            //            {
            //                o.IDKegiatan = 0;
            //                o.IDProgram = 0;
            //                o.IDUrusan = 0;
            //                o.KodeKegiatan = 0;
            //                o.KodeKegiatan = 0;
            //                o.KodeKategoriPelaksana = DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 1));
            //                o.KodeUrusanPelaksana = DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(1, 2));
            //                o.IDUrusan = DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 3));
            //            }
            //            else
            //            {
            //                //o.IDKegiatan = treeProgramKegiatan1                        
            //                o.IDProgram = m_IDProgram;
            //                o.IDKegiatan = m_IDKegiatan;
            //                o.IDUrusan = m_IDUrusan;

            //                o.KodeKegiatan = 0;
            ////                o.KodeKegiatan = 0;
            ////                o.KodeKategoriPelaksana = 0;
            ////                o.KodeUrusanPelaksana = 0;

            ////            }
            ////            o.PPKD = ctrlDinas1.PPKD();

            ////            o.IDRekening = DataFormat.GetLong(gridRekening.Rows[i].Cells[COL_IDREKENING].Value);
            ////            //o.JumlahOlah = DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_JUMLAH].Value);
            ////            //o.Plafon = DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_PLAFON].Value);
            ////            o.JumlahYADAPBD = DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[i].Cells[COL_YAD].Value);
            ////            o.JumlahDPA =DataFormat.FormatUangReportKeDecimal( DataFormat.GetString(gridRekening.Rows[i].Cells[COL_JUMLAH].Value));
                        
            ////            o.StatusUpdate = GetIsNEw(i);
            ////            o.ListUraian = GetUraian(i);
            ////            lstRekening.Add(o);
                    
            ////    }
            //}
            ////if ()

            
            //    if (oLogic.SimpanDPA(lstRekening, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, ctrlJenisAnggaran1.GetID(), GlobalVar.TahapAnggaran) == true)
            //    { //  SimpanIndikator();
            //        LoadAnggaran();
            //        MessageBox.Show("Penyimpanan selesai");
            //    }           
            
        }

        private TAnggaranRekening GetRekeningOnRow(int i)
        {
            TAnggaranRekening o = new TAnggaranRekening();
            o.Tahun = GlobalVar.TahunAnggaran;
            o.IDDinas = ctrlDinas1.GetID();
            o.Jenis = ctrlJenisAnggaran1.GetID();
            if (ctrlJenisAnggaran1.GetID() != 3)
            {
                o.IDKegiatan = 0;
                o.IDProgram = 0;
                o.IDUrusan = 0;
                o.KodeKegiatan = 0;
                o.KodeKegiatan = 0;
                o.KodeKategoriPelaksana = 0;
                o.KodeUrusanPelaksana = 0;
                o.IDUrusan = DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 3));

            }
            else
            {
                //o.IDKegiatan = treeProgramKegiatan1                        
                o.IDProgram = m_IDProgram;
                o.IDKegiatan = m_IDKegiatan;
                o.IDUrusan = m_IDUrusan;

                o.KodeKegiatan = 0;
                o.KodeKegiatan = 0;
                o.KodeKategoriPelaksana = 0;
                o.KodeUrusanPelaksana = 0;

            }

            o.IDRekening = DataFormat.GetLong(gridRekening.Rows[i].Cells[COL_IDREKENING].Value);
            o.JumlahOlah = DataFormat.FormatUangReportKeDecimal(gridRekening.Rows[i].Cells[COL_JUMLAH].Value);
            o.StatusUpdate = GetIsNEw(i);

            o.ListUraian = GetUraian(i);
            return o;
          
        }
        private TAnggaranUraian GetUraianOnRow(int i)
        {
            TAnggaranUraian o = new TAnggaranUraian();
            o.Tahun = GlobalVar.TahunAnggaran;
            o.HargaOlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value));
            o.IDDinas = ctrlDinas1.GetID();
            o.IDUrusan = m_IDUrusan;
            o.IDProgram = m_IDProgram;
            o.IDKegiatan = m_IDKegiatan;
            o.IDLokasi = DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_IDLOKASI].Value);
            o.Level = DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_LEVEL].Value);
            o.NoUrut = DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_NO].Value);
            o.PPKD = ctrlDinas1.PPKD();
            //o.VolOlah = DataFormat.GetDouble(gridRekening.Rows[i].Cells[COL_VOL].Value);
            o.Satuan = DataFormat.GetString(gridRekening.Rows[i].Cells[COL_SATUAN].Value);
            //o.HargaOlah = DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_HARGA].Value);
            o.IDRekening = DataFormat.GetLong(gridRekening.Rows[i].Cells[COL_IDREKENING].Value);
            o.IDUraian = DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_IDURAIAN].Value);
            o.Uraian = DataFormat.GetString(gridRekening.Rows[i].Cells[COL_URAIAN].Value);
            o.Jumlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_JUMLAH].Value));
            o.StatusUpdate = GetIsNEw(i);
            o.Jenis = ctrlJenisAnggaran1.GetID();
            o.TahapInput = DataFormat.GetSingle(GetTahap(i));

            o.ShowInReport = 1;



            //o.PPKD = 0;
            return o;

        }
        private EventResponseMessage treeProgramKegiatan1_Changed(int ID, float ObjectType)
        {
            m_IDKegiatan=ID;
            EventResponseMessage lRet = new EventResponseMessage();
            lRet.ResponseStatus = true;
            return lRet;
        //private int m_IDUrusan;
        }
        private List<TAnggaranUraian> GetUraian(int row)
        {
            //           0 ID
            //           1 +/-
            //2    Hapus
            //3  IDLokasi
            //4. Level
            //5...Induk
            //6...IDUraian

            //7... <<
            //8....>>
            //9....KodeRekening
            //10...No
            //11.. Uraian
            //12...VolMurni
            //13..Satuan Murni
            //14...Harga Murni
            //15...Jumlah Murni
            //16...Volume
            //17...Satuan  
            //18...Harga  
            //19..Jumlah  
            //20...Tahap
            long _IDRekening = DataFormat.GetLong(gridRekening.Rows[row].Cells[COL_IDREKENING].Value);
            List<TAnggaranUraian> _lst = new List<TAnggaranUraian>();
            for (int i = row + 1; row < gridRekening.Rows.Count; i++)
            {
                //JIka bukan Rekening
                if (DataFormat.GetLong(gridRekening.Rows[i].Cells[COL_IDREKENING].Value) != _IDRekening || DataFormat.GetString(gridRekening.Rows[i].Cells[COL_IDREKENING].Value).Trim().Length == 0|| GetLevel(i)==0)
                    break;

                    TAnggaranUraian o = new TAnggaranUraian();
                    o.Tahun = GlobalVar.TahunAnggaran;
                    //o.HargaOlah = DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_HARGA].Value);
                    o.IDDinas = ctrlDinas1.GetID();
                    o.IDUrusan = m_IDUrusan;
                    o.IDProgram = m_IDProgram;
                    o.IDKegiatan = m_IDKegiatan;
                    o.IDLokasi = DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_IDLOKASI].Value);
                    o.LevelDPA= DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_LEVEL].Value);
                    o.NoUrutDPA= i;// DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_NO].Value);
                    o.PPKD = ctrlDinas1.PPKD();
                    o.Vol = DataFormat.GetDouble(gridRekening.Rows[i].Cells[COL_VOL].Value);
                    o.SatuanDPA= DataFormat.GetString(gridRekening.Rows[i].Cells[COL_SATUAN].Value);
                    o.Harga = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_HARGA].Value));
                    o.IDRekening = DataFormat.GetLong(gridRekening.Rows[i].Cells[COL_IDREKENING].Value);
                    o.IDUraian = DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_IDURAIAN].Value);
                    o.UraianAPBD= DataFormat.GetString(gridRekening.Rows[i].Cells[COL_URAIAN].Value).Trim();
                    o.Jumlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_JUMLAH].Value));
                    o.StatusUpdate = GetIsNEw(i);
                    o.Jenis = ctrlJenisAnggaran1.GetID();
                    o.LabelDPA = DataFormat.GetString(gridRekening.Rows[i].Cells[COL_LABEL].Value).Trim();
                    o.ShowInReport = 0;                    
                    o.JumlahYADAPBD= DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[i].Cells[COL_YAD].Value));                
                    DataGridViewCheckBoxCell chkchecking = gridRekening.Rows[i].Cells[COL_SHOWINREPORT] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chkchecking.Value) == true)
                    {
                        o.ShowInReport = 1;
                    }
                    _lst.Add(o);                
                
            }
            return _lst;

        }

        private EventResponseMessage treeProgramKegiatan1_KegiatanChanged(int ID)
        {
           
            EventResponseMessage lRet = new EventResponseMessage();
            if (m_IDKegiatan> 0){
                if (MessageBox.Show("Apakah benar akan berganti kegiatan? (Jangan lupa menyimpan data. Pilih 'No'dan klik 'Simpan' untuk menyimpan", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    lRet.ResponseStatus = false;
                    return lRet;
                }
            }
            if (ctrlDinas1.GetID() == 0)
            {
                MessageBox.Show("Pilihan Dinasnya terlebih dahulu.");
                lRet.ResponseStatus = false;
                return lRet;
            }
            if (ctrlJenisAnggaran1.GetID() != 3)
            {
                MessageBox.Show("Pilihan jenis Aanggaran belum tepat");
                lRet.ResponseStatus = false;
                return lRet;
            }
            
            m_IDKegiatan = ID;
            m_IDUrusan = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 3));
            m_IDProgram = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 5));
            Urusan oUrusan = new Urusan();
            UrusanLogic oUrusanLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
            oUrusan = oUrusanLogic.GetByID(m_IDUrusan);
            if (oUrusan == null)
            {
                MessageBox.Show(oUrusanLogic.LastError());
                lRet.ResponseStatus = false;
                return lRet;

            }
            lblUrusan.Text = oUrusan.Tampilan + " " + oUrusan.Nama;

            TProgramLogic oPLogic = new TProgramLogic(GlobalVar.TahunAnggaran,3);
            TPrograms oProgram = new TPrograms();
            if (oPLogic.CekProgramDinas(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram))
            {
                oProgram = oPLogic.GetByDinasAndUrusanProgram(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram);
                if (oProgram == null)
                {
                    MessageBox.Show(oPLogic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }
                lblProgram.Text = "";
                if (oProgram != null)
                    lblProgram.Text = oProgram.KodeProgram.ToString() + " " + oProgram.Nama;
            //    TKegiatanLogic oKLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran);
            //    TKegiatan oKegiatan = new TKegiatan();
            //    oKegiatan = oKLogic.GetByID(ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan);
            //    if (oKegiatan == null)
            //    {
            //        MessageBox.Show(oKLogic.LastError());
            //        lRet.ResponseStatus = false;
            //        return lRet;

            //    }
            //    lblKegiatan.Text = oKegiatan.KodeKegiatan.ToString() + " " + oKegiatan.Nama;
            ////    lblPagu.Text = oKegiatan.Pagu.FormatUang();
            //    TKegiatanAPBDLogic oKegiatanAPBDLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
            //    oKegiatanAPBDLogic.CekNAmaKegiatan(oKegiatan);

            //    m_dPagu = oKegiatan.Pagu;
            //    LoadAnggaran();
            //    LoadAnggaranKas();
            //    HitungJumlahRKA();
                //TKegiatanLogic oKLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran);
                TKegiatanAPBDLogic oKAPBDLOgic = new TKegiatanAPBDLogic((int)GlobalVar.TahunAnggaran);
                TKegiatanAPBD oKegiatan = new TKegiatanAPBD();
                //TKegiatan tk = oKLogic.GetByID(ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan);
                oKegiatan = oKAPBDLOgic.GetKegiatan((int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, ctrlJenisAnggaran1.GetID(),2);

                //     oKegiatan = oKLogic.GetByIDAPBD(ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan);
                if (oKegiatan == null)
                {
                    MessageBox.Show(oKAPBDLOgic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }



                DisplayKegiatan(oKegiatan);
                m_dPagu = oKegiatan.Pagu;
                LoadAnggaran();
                HitungJumlahRKA();
            }
            return lRet;
        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            if (ctrlJenisAnggaran1.GetID() == 0)
            {
                MessageBox.Show("Jenis Anggaran Belum dipilih.");
                return;
            }


            if (ctrlJenisAnggaran1.GetID() == 3 && m_IDKegiatan == 0)
            {
                MessageBox.Show("Program Kegiatan belum dipilih.");
                return;

            }
            
            ParameterLaporan p = new ParameterLaporan();
            
            Urusan oUrusan = new Urusan();
            UrusanLogic oUrusanLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
            oUrusan = oUrusanLogic.GetByID(m_IDUrusan);
            p.KodeUrusan = oUrusan.ID.ToString().Substring(0, 1) + "." + oUrusan.ID.ToString().Substring(1, 2);// ctrlDinas1.KodeUrusanPemerintahan();
            p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();

            p.NamaUrusan= oUrusan.Nama;
            p.NamaDinas = ctrlDinas1.GetNamaSKPD();
            p.Keterangan = txtKeterangan.Text;
            p.Jumlah = lblJumlah.Text;

            //p.Tanggal = dtCetak.Value.ToString("dd MMM yyyy");
            //p.dTanggal = dtCetak.Value.Date;

            dtCetak.Value = new DateTime((int)GlobalVar.TahunAnggaran, 2, 2);
            p.Tanggal = dtCetak.Value.ToString("dd MMM yyyy");
            p.dTanggal = dtCetak.Value.Date;
            
            p.Tahap = 2;
            decimal cJumlah = lblJumlah.Text.UangToDecimal();
            int idDInas = ctrlDinas1.GetID();
            int iJenisAnggaran = ctrlJenisAnggaran1.GetID();
            //frmReportViewer f = new frmReportViewer();
            //if (ctrlJenisAnggaran1.GetID() != 3)
            //{
            //    int _iPPKD = (int)ctrlDinas1.PPKD();
            //    if (_iPPKD == 0)
            //    {
            //        if (ctrlJenisAnggaran1.GetID() == 1)
            //        {
            //            p.NamaLaporan = "RKA SKPD 1";
            //            p.Title1 = "Rincian Anggaran Pendapatan Satuan Kerja Perangkat Daerah";
            //            f.CetakDPAPendapatan(p, GlobalVar.TahunAnggaran, idDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, iJenisAnggaran, cJumlah, _iPPKD);
            //        }
            //        else
            //        {
            //            dtCetak.Value = new DateTime((int)GlobalVar.TahunAnggaran, 1, 2);
            //            p.Tanggal = dtCetak.Value.ToString("dd MMM yyyy");
            //            p.dTanggal = dtCetak.Value.Date;
            

            //            p.NamaLaporan = "RKA SKPD 2.1";
            //            p.Title1 = "Rincian Anggaran Belanja Tidak Langsung Satuan Kerja Perangkat Daerah";
            //            f.CetakDPABelanjaTidakLangsung(p, GlobalVar.TahunAnggaran, idDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, iJenisAnggaran, cJumlah, _iPPKD);
            //        }
            //    }
            //    else
            //    {
            //        if (ctrlJenisAnggaran1.GetID() == 1)
            //        {
            //            p.NamaLaporan = "RKA PPKD 1";
            //            p.Title1 = "Rincian Anggaran Pendapatan Pejabat Pengelola Keuangan Daerah";
            //            f.CetakRKAPendapatan(p, GlobalVar.TahunAnggaran, idDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, iJenisAnggaran, cJumlah, _iPPKD, null);

            //        }
            //        else
            //        {
            //            dtCetak.Value = new DateTime((int)GlobalVar.TahunAnggaran, 1, 2);
            //            p.Tanggal = dtCetak.Value.ToString("dd MMM yyyy");
            //            p.dTanggal = dtCetak.Value.Date;
            

            //            p.NamaLaporan = "RKA PPKD 2.1";
            //            p.Title1 = "Rincian Anggaran Belanja Tidak Langsung Pejabat Pengelola Keuangan Daerah";
            //            f.CetakDPABelanjaTidakLangsung(p, GlobalVar.TahunAnggaran, idDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, iJenisAnggaran, cJumlah, _iPPKD);
            //        }

            //    }                
            //}
            //else
            //{
            //    //f.CetakDPABelanjaLangsung(p, (int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, ctrlJenisAnggaran1.GetID(), cJumlah, 0);
            //    //f.CetakDPAPendapatan2(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, ctrlJenisAnggaran1.GetID(), cJumlah, _iPPKD, false);
            //    f.CetakDPABelanjaLangsung(p, GlobalVar.TahunAnggaran, idDInas, m_IDUrusan, m_IDProgram, m_IDKegiatan, iJenisAnggaran, cJumlah, 0, false);
                     

            //    ///f.CetakDPABelanjaLangsung (p,(int) GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, ctrlJenisAnggaran1.GetID(), cJumlah,0);
            //}
            //f.Show();

        }
        private void PasteClipboard()
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');

                int iRow =gridRekening.CurrentCell.RowIndex;
                int iCol = gridRekening.CurrentCell.ColumnIndex;
                // _barisKUA--;
                DataGridViewCell oCell;

                for (int i = 0; i < lines.Length; i++)
                {

                    //if (iCol == COL_URAIAN) 
                       // InsertRow(iRow);

                }
                //int _added = 0;
                foreach (string line in lines)
                {
                    if (iRow < gridRekening.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            //if (iCol == COL_URAIAN)
                            //{
                                if (iCol + i < this.gridRekening.ColumnCount)
                                {
                                    oCell = gridRekening[iCol + i, iRow];

                                    oCell.Value = Convert.ChangeType(sCells[i].Replace("\r", ""), oCell.ValueType);
                                    if (iCol + i == COL_VOL || iCol + i == COL_HARGA)
                                    {
                                        decimal c = 0L;
                                        c = DataFormat.GetDecimal(gridRekening.Rows[iRow].Cells[COL_VOL].Value) * DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_HARGA].Value));
                                        gridRekening.Rows[iRow].Cells[COL_JUMLAH].Value =DataFormat.ToRupiahInReport( c);//.FormatUang();
                                        HitungJumlahBasedOnThisRow(iRow);
                                    }

                                    //{
                                    //    oCell = gridRekening[9, iRow];
                                    //    oCell.Value = Convert.ChangeType(sCells[i].Replace("\r", ""), oCell.ValueType);
                                    //}
                                }
                                else
                                {
                                    break;
                                }
                            //}
                            //else
                            //{

                            //}
                        }
                        iRow++;
                    }
                    else
                    {
                        break;
                    }
                    HitungJumlahRKA();
                }

                Clipboard.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
        private void PasteClipboardIndikator()
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');

                int iRow = gridIndikator.CurrentCell.RowIndex;
                int iCol = gridIndikator.CurrentCell.ColumnIndex;
                // _barisKUA--;
                DataGridViewCell oCell;

                
                //int _added = 0;
                foreach (string line in lines)
                {
                    if (iRow < gridIndikator.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i < this.gridIndikator.ColumnCount)
                            {
                                oCell = gridIndikator[iCol + i, iRow];

                                oCell.Value = Convert.ChangeType(sCells[i].Replace("\r", ""), oCell.ValueType);
                              
                            }
                            else
                            {
                                break;
                            }
                        }
                        iRow++;
                    }
                    else
                    {
                        break;
                    }
                   
                }

                Clipboard.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
        private void CopyToClipboard()
        {
            //Copy to clipboard
            DataObject dataObj = gridRekening.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void cmdSimpanIndikator_Click(object sender, EventArgs e)
        {

        }
        // private void SimpanIndikator(){

        //     TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
        //    TKegiatanAPBD o = new TKegiatanAPBD();
        //    o.IDDinas = ctrlDinas1.GetID();
        //    o.IDKegiatan = m_IDKegiatan;
        //    o.IDProgram = m_IDProgram;
        //    o.IDUrusan = m_IDUrusan;
        //    o.Tahun = GlobalVar.TahunAnggaran;
        //    o.KelompokSasaran = txtKelompokSasaran.Text;
        //    o.Keterangan = txtKeterangan.Text;
        //    o.Lokasi = txtLokasi.Text;
        //    //o.Waktu=
        //    o.AnggaranTahunDepan = txtAnggaranYAD.Text.UangToDecimal();
        //    o.AnggaranTahunLalu = txtAnggaranTahunLalu.Text.UangToDecimal();

        //    o.Jenis = ctrlJenisAnggaran1.GetID();
        //    o.TanggalPembahasan = dtPembahasan.Value.Date;
        //    o.TahapInput = GlobalVar.TahapAnggaran;


        //    o.ListIndikator = LoadIndikator();
        //    //o.ListCatatan = LoadCatatanKegiatan();
        //    if (oLogic.Simpan(o) == true)
        //    {
        //     //   MessageBox.Show("Penyimpanan catatan selesai");
        //    }
        //    else
        //    {
        //       // MessageBox.Show("Kesalahan dalam penyimpanan "+ oLogic.LastError());

        //    }

        //}
        //private List<CatatanKegiatan> LoadCatatanKegiatan()
        //{
        //    List<CatatanKegiatan> mListUnit = new List<CatatanKegiatan>();

        //    for (int i=0; i < gridCatatan.Rows.Count; i++)
        //    {
        //        if (DataFormat.GetString(gridCatatan.Rows[i].Cells[1].Value).ToString().Trim() != "")
        //        {
        //            CatatanKegiatan o = new CatatanKegiatan();
        //            o.IDDInas = ctrlDinas1.GetID();
        //            o.IDKegiatan = m_IDKegiatan;
        //            o.IDProgram = m_IDProgram;
        //            o.IDUrusan = m_IDUrusan;
        //            o.Tahun = GlobalVar.TahunAnggaran;
        //            o.NoCatatan = i+1;
        //            o.CatatanMurni = DataFormat.GetString(gridCatatan.Rows[i].Cells[1].Value).ToString().Trim();
        //            o.CatatanPerubahan = DataFormat.GetString(gridCatatan.Rows[i].Cells[2].Value).ToString().Trim();
        //            mListUnit.Add(o);

        //        }
        //    }
        //    return mListUnit;


        //}
        private List<Indikator> LoadIndikator()
        {
         


            List<Indikator> _lst = new List<Indikator>();

            for (int i = 0; i < gridIndikator.Rows.Count; i++)
            {
                if (DataFormat.GetInteger(gridIndikator.Rows[i].Cells[1].Value) > 0)
                {

                    if (DataFormat.GetString(gridIndikator.Rows[i].Cells[0].Value).Length > 0 || DataFormat.GetString(gridIndikator.Rows[i].Cells[2].Value).Length > 0 || DataFormat.GetString(gridIndikator.Rows[i].Cells[3].Value).Length > 0)
                    {
                        Indikator oi = new Indikator();
                        oi.Tahun = GlobalVar.TahunAnggaran;

                        oi.IDDInas = ctrlDinas1.GetID();
                        oi.IDKegiatan = m_IDKegiatan;
                        oi.IDProgram = m_IDProgram;
                        oi.IDUrusan = m_IDUrusan;
                        oi.iJenis = DataFormat.GetInteger(gridIndikator.Rows[i].Cells[1].Value);
                        oi.iIndikator = i;
                        oi.sIndikator = DataFormat.GetString(gridIndikator.Rows[i].Cells[2].Value);
                        oi.Target = DataFormat.GetString(gridIndikator.Rows[i].Cells[3].Value);
                        _lst.Add(oi);
                    }
                }
            }
            return _lst;

            //oLogic.Simpan(mListUnit, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan);


        }
        private void tabCatatan_Click(object sender, EventArgs e)
        {


        }
        private void ShowHidIndikaor(){
            int _iJenisDOk = ctrlJenisAnggaran1.GetID();


                lblAnggaranTahunLalu.Visible = _iJenisDOk == 3 ? true : false;
                lblAnggaranTahunYAD.Visible = _iJenisDOk == 3 ? true : false;
                
                if (_iJenisDOk == 3)
                {
                    //tabRekening.TabPages[2].Show();
                  //  tabRekening.TabPages.Remove(tabIndikator);
                }
                else
                {
                    //tabRekening.TabPages[2].Hide();
                   // tabRekening.TabPages.Add("tabIndikator");
                }
                lblKelompokSasaran.Visible = _iJenisDOk == 3 ? true : false;
                lblLokasi.Visible = _iJenisDOk == 3 ? true : false;
                lblIndikator.Visible = _iJenisDOk == 3 ? true : false;
                txtLokasi.Visible = _iJenisDOk == 3 ? true : false;
                txtKelompokSasaran.Visible = _iJenisDOk == 3 ? true : false;
                gridIndikator.Visible = _iJenisDOk == 3 ? true : false;
                txtAnggaranTahunLalu.Visible = _iJenisDOk == 3 ? true : false;
                txtAnggaranYAD.Visible = _iJenisDOk == 3 ? true : false;
                txtAnggaranTahunLalu.Text = "0";
                txtAnggaranYAD.Text = "0";

            

        }

        private void gridIndikator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        CopyToClipboard();
                        break;

                    case Keys.V:
                        //gridKUA.PasteInData( );// 
                        PasteClipboardIndikator();                        

                        break;
                }
            }
            
            if (e.KeyCode == Keys.Enter)
            {
                DataGridViewCell _cell = gridIndikator.CurrentCell;

                // gridKUA.Rows.Add();
                if (_cell.RowIndex >=0 && _cell.RowIndex < gridIndikator.Rows.Count)
                {
                    string[] row = { "", DataFormat.GetString(gridIndikator.Rows[_cell.RowIndex].Cells[1].Value) };
                    gridIndikator.Rows.Insert(_cell.RowIndex+1, row);
                }
            }
            if (e.KeyCode == Keys.Delete)
            {

                foreach (DataGridViewCell cell in gridIndikator.SelectedCells)
                {

                    int rowIndex = cell.RowIndex;
                    int colIndex = cell.ColumnIndex;
                    if (colIndex >= 2)
                    {
                        gridIndikator.Rows[rowIndex].Cells[colIndex].Value = "";
                    }
                }
            }
        }
        private void InsertRowIndikator()
        {

        }
        private void LoadLokasi()
        {

            KUALogic oLogic = new KUALogic(GlobalVar.TahunAnggaran);
            List<KUA> _lst = new List<KUA>();
            int _pIDDInas = ctrlDinas1.GetID();
            _lst = oLogic.GetByIDKegiatan(ctrlDinas1.GetID(),m_IDUrusan, m_IDProgram,m_IDKegiatan, GlobalVar.TahunAnggaran);
            //EmptyValue();
            int iRow=0;
            gridRincian.Rows.Clear();
            if (_lst != null)
            {
                foreach (KUA k in _lst) 
                {
                    string[] row = { 
                                    k.IDLokasi.ToString(),
                                    k.NoUrut.ToString(),
                                    k.NamaUsulan,k.JumlahOlah.FormatUang(),"0",
                                    ">>"};

                    gridRincian.Rows.Add(row);                  

                }
                    
                }
            }

        private void cmdLokasi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridRincian.Rows.Count; i++)
            {
                if (InsertRIncian(i) == false)
                {
                    MessageBox.Show("Belum memilih Kode Rekening.");
                    break;
                }
                    

            }
        }

        private void gridRincian_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // int currentrow = e.RowIndex;
            if (e.ColumnIndex == 5)
            {

                if (InsertRIncian(e.RowIndex) == false)
                {
                    MessageBox.Show("Belum memilih Kode Rekening.");

                }

            }
        }


        private bool InsertRIncian(int currentrow)
        {

            string currentRekening = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDREKENING].Value);
            if (DataFormat.GetInteger(currentRekening) == 0)
            {
                MessageBox.Show("Belum pilih Kode Rekening..");
                return false;
            }
            string currentLokasi = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDLOKASI].Value);
            string currentIDUraian = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDURAIAN].Value);
            string currentParentIDUraian = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[5].Value);
            int currentLevel = GetLevel(currentrow);//
            //DataFormat.GetInteger(gridRekening.Rows[currentrow].Cells[COL_LEVEL].Value);
            int iNoUrut = 0;
            //if (currentLevel < 1)
            //    return false;

            // always 1
            currentLevel = 1;// currentLevel == 0 ? currentLevel + 1 : 1;


            for (int idx = 0; idx < m_iCurrentRow; idx++)
            {
                if (GetLevel(idx) == 0)
                    iNoUrut = 0;
                iNoUrut++;
            }

            string sKode = DataFormat.GetInteger(gridRincian.Rows[currentrow].Cells[0].Value).ToString();
            string[] row = { currentRekening, "-", "Hapus", sKode, currentLevel.ToString(), currentParentIDUraian, currentIDUraian, "<<", ">>", "", sKode,iNoUrut.ToString(), DataFormat.GetString(gridRincian.Rows[currentrow].Cells[2].Value), "0", "", "0", "0", "0", "", "0", "0", GlobalVar.TahapAnggaran.ToString(), "0", "0" };

            gridRekening.Rows.Insert(m_iCurrentRow+1 , row);

            for (int idx = m_iCurrentRow; idx < gridRekening.Rows.Count; idx++)
            {
                if (GetLevel(idx) == 0)
                    iNoUrut = 0;
                //break;
                gridRekening.Rows[idx].Cells[COL_NO].Value = ++iNoUrut;
            }
        m_iCurrentRow++;

            return true;
        }
        private void treeProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }

        private void gridSB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                //string currentRekening = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[COL_IDREKENING].Value);
                //string currentLokasi = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[COL_IDLOKASI].Value);
                //string currentIDUraian = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[COL_IDURAIAN].Value);
                //string currentParentIDUraian = DataFormat.GetString(gridRekening.Rows[currentrow].Cells[5].Value);
                //int currentLevel = GetLevel(e.RowIndex);//
                
                int iNoUrut = 0;

                TambahStandardHarga(m_iCurrentRowSB, e.RowIndex);



            }
        }
        private void TambahStandardHarga(int currentrow, int rowSB )
        {

            //string currentRekening = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDREKENING].Value);
            //string currentLokasi = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDLOKASI].Value);
            //string currentIDUraian = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDURAIAN].Value);
            //string currentParentIDUraian = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[5].Value);
            //string sUraian = DataFormat.GetString(gridSB.Rows[rowSB].Cells[1].Value) + DataFormat.GetString(gridSB.Rows[rowSB].Cells[2].Value);
            //string sSatuan = DataFormat.GetString(gridSB.Rows[rowSB].Cells[3].Value);
            //string sJumlah = DataFormat.GetString(gridSB.Rows[rowSB].Cells[4].Value);
            //string sIDSB = DataFormat.GetString(gridSB.Rows[rowSB].Cells[0].Value);


            //int currentLevel = GetLevel(m_iCurrentRow);//
            //DataFormat.GetInteger(gridRekening.Rows[currentrow].Cells[COL_LEVEL].Value);
            //int iNoUrut = 0;

            //currentLevel = currentLevel == 0 ? currentLevel + 1 : currentLevel;

            //for (int idx = 0; idx < currentrow; idx++)
            //{
            //    if (GetLevel(idx) == 0)
            //        iNoUrut = 0;
            //    iNoUrut++;
            //}
            //iNoUrut++;

            //string[] row = { currentRekening, "-", "Hapus", "0", currentLevel.ToString(), currentParentIDUraian, currentIDUraian, "<<", ">>", "", iNoUrut.ToString(), sUraian, "0", sSatuan, sJumlah, "0", "0", "", "0", "0", GlobalVar.TahapAnggaran.ToString(), sIDSB, "0" };
            //string[] row = { currentRekening, currentLevel.ToString(), currentParentIDUraian, currentIDUraian, "<<", ">>" };
            //gridRekening.Rows.Insert(currentrow + 1, row);

            //for (int idx = currentrow; idx < gridRekening.Rows.Count; idx++)
            //{
            //    if (GetLevel(idx) == 0)
            //        iNoUrut = 0;
            //    break;
            //    gridRekening.Rows[idx].Cells[COL_NO].Value = ++iNoUrut;
            //}
            //m_iCurrentRow++;// = currentrow + 1;
        }

        private int GetRowIndikatorMasukan()
        {
            for (int iRow = 0; iRow < gridIndikator.Rows.Count; iRow++)
            {
                if (DataFormat.GetSingle(gridIndikator.Rows[iRow].Cells[1].Value) == 2)
                {
                    return iRow;
                    break;
                }
            }
            return 0;
        }
        private string[] CreateRowString(string pCOL_IDREKENING, string pCOL_EXPAND, string pCOL_HAPUS, string pCOL_IDLOKASI, string pCOL_LEVEL, string pCOL_IDURAIAN,
               string pCOL_TOLEFT, string pCOL_TORIGHT, string pCOL_DISPLAYREKENING, string pCOL_NO, string pCOL_URAIAN, string pCOL_VOL,
               string pCOL_SATUAN, string pCOL_HARGA, string pCOL_JUMLAH, string pCOL_VOLMURNI, string pCOL_SATUANMURNI, string pCOL_HARGAMURNI, string pCOL_JUMLAHMURNI,
               string pCOL_TAHAP, string pCOL_IDANGGARANKAS, string pCOL_ISNEW, string pCOL_LABEL)
        {


            string[] row ={pCOL_IDREKENING,pCOL_EXPAND,pCOL_HAPUS,pCOL_IDLOKASI ,pCOL_LEVEL , pCOL_IDURAIAN ,
                         pCOL_TOLEFT , pCOL_TORIGHT, pCOL_DISPLAYREKENING, pCOL_NO , pCOL_URAIAN, pCOL_VOL ,
                         pCOL_SATUAN , pCOL_HARGA , pCOL_JUMLAH , pCOL_VOLMURNI , pCOL_SATUANMURNI , pCOL_HARGAMURNI,  pCOL_JUMLAHMURNI ,
                         pCOL_TAHAP , pCOL_IDANGGARANKAS,  pCOL_ISNEW , pCOL_LABEL };
            return row;
        }
#region GetDataPerkolom
        private string GetIDUraian(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_IDURAIAN].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_IDURAIAN].Value);
            }
            else return "";
        }
        private string GetNoUrut(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_NO].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_NO].Value);
            }
            else return "";
        }
        private string GetNilaiUraian(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_URAIAN].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_URAIAN].Value);
            }
            else return "";
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
        private string GetSatuan(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_SATUAN].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_SATUAN].Value);
            }
            else return "";
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
        private string GetJumlah(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_JUMLAH].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_JUMLAH].Value);
            }
            else return "";
        }

        private string GetVOLMurni(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_VOLMURNI].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_VOLMURNI].Value);
            }
            else return "";
        }
        
        private string GetSatuanMurni(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_SATUANMURNI].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_SATUANMURNI].Value);
            }
            else return "";
        }
        private string GetHargaMurni(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_HARGAMURNI].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_HARGAMURNI].Value);
            }
            else return "";
        }
        private string GetJumlahMurni(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_JUMLAHMURNI].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_JUMLAHMURNI].Value);
            }
            else return "";
        }

        private string GetTahap(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_TAHAP].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_TAHAP].Value);
            }
            else return "";
        }

        private Single GetIsNEw(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return 0;

            if (gridRekening.Rows[iRow].Cells[COL_ISNEW].Value != null)
            {
                return DataFormat.GetSingle(gridRekening.Rows[iRow].Cells[COL_ISNEW].Value);
            }
            else return 0;
        }

        private string GetLabel(int iRow)
        {
            // Jika 
            if (iRow >= gridRekening.Rows.Count || iRow < 0)
                return "";

            if (gridRekening.Rows[iRow].Cells[COL_LABEL].Value != null)
            {
                return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_LABEL].Value);
            }
            else return "";
        }


         private string GetIDRekening(int iRow)
         {
             // Jika 
             if (iRow >= gridRekening.Rows.Count || iRow < 0 )
                 return "";

             if (gridRekening.Rows[iRow].Cells[COL_IDREKENING].Value != null)
             {
                 return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_IDREKENING].Value);
             }
             else return "";
         }
         private string GetIDLokasi(int iRow)
         {
             // Jika 
             if (iRow >= gridRekening.Rows.Count || iRow < 0)
                 return "";

             if (gridRekening.Rows[iRow].Cells[COL_IDLOKASI].Value != null)
             {
                 return DataFormat.GetString(gridRekening.Rows[iRow].Cells[COL_IDLOKASI].Value);
             }
             else return "";
         }

#endregion GetDataPerkolom

         private void button1_Click(object sender, EventArgs e)
         {
           ////  ParameterLaporan p = new ParameterLaporan();
           ////  p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
           ////  p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
           ////  p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
           ////  p.NamaDinas = ctrlDinas1.GetNamaSKPD();
           ////  p.Tanggal = dtCetak.Value.ToString("DD MMM yyyy");
           ////  p.Tahap = 2;             

           ////  frmReportViewer f = new frmReportViewer();
             
           //////  f.CetakRKARekap(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID());
             
           ////  f.Show();
         }

         private void button2_Click(object sender, EventArgs e)
         {
             ////frmRekapBelanjaLangsung fRekap = new frmRekapBelanjaLangsung();
             ////fRekap.ShowDialog();

             //ParameterLaporan p = new ParameterLaporan();
             //p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
             //p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
             //p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
             //p.NamaDinas = ctrlDinas1.GetNamaSKPD();
             //p.Tanggal = dtCetak.Value.ToString("DD MMM yyyy");
             //p.dTanggal = dtCetak.Value.Date;

             //if (ctrlDinas1.GetID() == 0)
             //{
             //    MessageBox.Show("Dinas Belum dipilih");
             //    return;
             //}

             //frmReportViewer f = new frmReportViewer();

             //f.CetakDPA22 (p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID());
             //f.Show();

             
         }

         private void cmdHapusYangDIpilih_Click(object sender, EventArgs e)
         {
             if (MessageBox.Show("Benar akan menghapus datayang dipilih? ","Konfirmasi",MessageBoxButtons.YesNo)==DialogResult.No)
                 return;


             if (gridRekening.SelectedRows.Count > 0)
             {

                 foreach (DataGridViewRow r in gridRekening.SelectedRows)
                 {
                     
                     HapusBarisIni(r.Index);
                     //Code to add selected row to new datagrid.
                     //Important to note that dataGridView2.Rows.Add(r) will not work 
                     //because each row can only belong to one data grid.  You'll have 
                     //to create a new Row with the same info for an exact copy

                     gridRekening.Rows.Remove(r);

                 }

                 foreach (DataGridViewRow r in gridRekening.SelectedRows)
                 {
                     
                     //Code to add selected row to new datagrid.
                     //Important to note that dataGridView2.Rows.Add(r) will not work 
                     //because each row can only belong to one data grid.  You'll have 
                     //to create a new Row with the same info for an exact copy

                     gridRekening.Rows.Remove(r);

                 }


                 HitungPerRekening();
                 HitungJumlahRKA();
             }
                       
                       
         }
         private int CekLevelBarisBerikutnya(int iBaris)
         {

             if (iBaris < gridRekening.Rows.Count - 1)
             {
                 return GetLevel(iBaris + 1);
             }
             else return 0;
         }

         private void ctrlStandardHarga1_DoubleClicking(StandardBiaya rek)
         {
             StandardBiayaLogic oLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
             StandardBiaya oSB = new StandardBiaya();
             oSB = oLogic.GetByID(oSB.IDBiaya);
             if (rek  != null)
             {
                 if (rek.Harga > 0)
                 {
                     string currentRekening = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDREKENING].Value);
                     string currentLokasi = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDLOKASI].Value);
                     string currentIDUraian = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[COL_IDURAIAN].Value);
                     string currentParentIDUraian = DataFormat.GetString(gridRekening.Rows[m_iCurrentRow].Cells[5].Value);
                     string sUraian = rek.Nama + rek.Uraian;
                     string sSatuan = rek.NamaSatuan;// DataFormat.GetString(gridSB.Rows[rowSB].Cells[3].Value);
                     string sJumlah = rek.Harga.ToString();// DataFormat.GetString(gridSB.Rows[rowSB].Cells[4].Value);
                     string sIDSB = rek.IDBiaya;// DataFormat.GetString(gridSB.Rows[rowSB].Cells[0].Value);


                     int currentLevel = GetLevel(m_iCurrentRow);//
                     //DataFormat.GetInteger(gridRekening.Rows[currentrow].Cells[COL_LEVEL].Value);
                     int iNoUrut = 0;

                     currentLevel = currentLevel == 0 ? currentLevel + 1 : currentLevel;

                     for (int idx = 0; idx < m_iCurrentRow; idx++)
                     {
                         if (GetLevel(idx) == 0)
                             iNoUrut = 0;
                         iNoUrut++;
                     }
                     iNoUrut++;

                     string[] row = { currentRekening, "-", "Hapus", "0", currentLevel.ToString(), currentParentIDUraian, "0", "<<", ">>", "", iNoUrut.ToString(), sUraian, "0", sSatuan, sJumlah, "0", "0", "", "0", "0", GlobalVar.TahapAnggaran.ToString(), sIDSB, "0" };
                     //string[] row = { currentRekening, currentLevel.ToString(), currentParentIDUraian, currentIDUraian, "<<", ">>" };
                     gridRekening.Rows.Insert(m_iCurrentRow + 1, row);

                     for (int idx = m_iCurrentRow; idx < gridRekening.Rows.Count; idx++)
                     {
                         if (GetLevel(idx) == 0)
                             iNoUrut = 0;
                         //break;
                         gridRekening.Rows[idx].Cells[COL_NO].Value = ++iNoUrut;
                     }
                     m_iCurrentRow++;// m_iCurrentRowSB + 1;
                 }
             }
         }

         private void cmdSimpanTIMTAPD_Click(object sender, EventArgs e)
         {
             TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
             List<TimAnggaran> _lst = new List<TimAnggaran>();
             for (int i = 0; i < gridTimDPA.Rows.Count; i++)
             {
                 if (DataFormat.GetString(gridTimDPA.Rows[i].Cells[1].Value).Length > 0)
                 {
                     TimAnggaran ta = new TimAnggaran();
                     ta.ID = i;
                     ta.Nama = DataFormat.GetString(gridTimDPA.Rows[i].Cells[1].Value);
                     ta.NIP = DataFormat.GetString(gridTimDPA.Rows[i].Cells[2].Value);
                     ta.Jabatan = DataFormat.GetString(gridTimDPA.Rows[i].Cells[3].Value);
                     ta.Type = 1;
                     ta.DInas = ctrlDinas1.GetID();
                     ta.Jenis = ctrlJenisAnggaran1.GetID();
                     _lst.Add(ta);
                 }

             }
             if (taLogic.Simpan(_lst, GlobalVar.TahunAnggaran, 1, ctrlDinas1.GetID(), ctrlJenisAnggaran1.GetID(),1) == true)
             {
                 MessageBox.Show("Penyimpanan TIM DPA berhasil.");
             }
             else
             {
                 MessageBox.Show("Kesalahan Penyimpanan TIM DPA." + taLogic.LastError());
             }


         }

         private void cmdSimpanReview_Click(object sender, EventArgs e)
         {
             TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
             List<TimAnggaran> _lst = new List<TimAnggaran>();
             for (int i = 0; i < gridReviewer.Rows.Count; i++)
             {
                 if (DataFormat.GetString(gridReviewer.Rows[i].Cells[1].Value).Length > 0)
                 {
                     TimAnggaran ta = new TimAnggaran();
                     ta.Nama = DataFormat.GetString(gridReviewer.Rows[i].Cells[1].Value);
                     ta.NIP = DataFormat.GetString(gridReviewer.Rows[i].Cells[2].Value);
                     ta.Jabatan = DataFormat.GetString(gridReviewer.Rows[i].Cells[3].Value);
                     ta.Type = 2;
                     ta.DInas = ctrlDinas1.GetID();
                     ta.Jenis = ctrlJenisAnggaran1.GetID();
                     _lst.Add(ta);
                 }

             }
             if (taLogic.Simpan(_lst, GlobalVar.TahunAnggaran, 1, ctrlDinas1.GetID(), ctrlJenisAnggaran1.GetID(),2) == true)
             {
                 MessageBox.Show("Penyimpanan TIM reviewer berhasil.");
             }
             else
             {
                 MessageBox.Show("Kesalahan Penyimpanan TIM reviewer ." + taLogic.LastError());
             }

         }
         private void LoadTimDPA()
         {
             TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
             List<TimAnggaran> _lst = new List<TimAnggaran>();
             _lst = taLogic.Get(GlobalVar.TahunAnggaran, 1,ctrlDinas1.GetID(), ctrlJenisAnggaran1.GetID());
             gridTimDPA.Rows.Clear();

             gridReviewer.Rows.Clear();
             foreach (TimAnggaran ta in _lst)
             {
                 string[] row = { "", ta.Nama, ta.NIP, ta.Jabatan };
                 if (ta.Type == 1)
                 {
                     gridTimDPA.Rows.Add(row);
                 }
                 else
                 {
                     gridReviewer.Rows.Add(row);

                 }
             }
         }

         private void cmdCopy_Click(object sender, EventArgs e)
         {
             //frmCariProgramKegiatan fCari = new frmCariProgramKegiatan();
             //fCari.ShowDialog();
             //if (fCari.IsOK() == true)
             //{
             //    if (MessageBox.Show("Apakah benar akan mencopy isi RKA kegiatan ini ke RKA Kegiatan tujuan?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
             //    {
             //        TKegiatanAPBDLogic oKLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
             //        TKegiatanAPBD oKSumber = new TKegiatanAPBD();
             //        oKSumber.Tahun = GlobalVar.TahunAnggaran;
             //        oKSumber.IDDinas = ctrlDinas1.GetID();
             //        oKSumber.IDUrusan = m_IDUrusan;
             //        oKSumber.IDProgram = m_IDProgram;
             //        oKSumber.IDKegiatan = m_IDKegiatan;

             //        TKegiatanAPBD oKTujuan = new TKegiatanAPBD();
             //        oKTujuan.Tahun = GlobalVar.TahunAnggaran;
             //        oKTujuan.IDDinas = ctrlDinas1.GetID();
             //        oKTujuan.IDUrusan = fCari.GetUrusanPemerintahan();
             //        oKTujuan.IDProgram = fCari.GetIDProgram();
             //        oKTujuan.IDKegiatan = fCari.GetIDKegiatan();

             //        if (oKSumber.Tahun == oKTujuan.Tahun &&
             //        oKSumber.IDDinas == oKTujuan.IDDinas &&
             //        oKSumber.IDUrusan == oKTujuan.IDUrusan &&
             //        oKSumber.IDProgram == oKTujuan.IDProgram &&
             //        oKSumber.IDKegiatan == oKTujuan.IDKegiatan)
             //        {
             //            MessageBox.Show("Kegiatan sumber dan tujuan sama.");
             //            return;
             //        }


             //        if (oKLogic.CopyRKA(oKSumber, oKTujuan) == true)
             //            MessageBox.Show("RKA sudah di copy");
             //        else
             //            MessageBox.Show("RKA gagal dicopy " + oKLogic.LastError());


             //    }
             //}
         }

         private void gridTimDPA_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 0)
             {
                 if (DataFormat.GetString(gridTimDPA.Rows[e.RowIndex].Cells[1].Value).Length > 0)
                 {
                     gridTimDPA.Rows.RemoveAt(e.RowIndex);
                 }
             }
         }

         private void gridReviewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 0)
             {
                 if (DataFormat.GetString(gridReviewer.Rows[e.RowIndex].Cells[1].Value).Length > 0)
                 {
                     gridReviewer.Rows.RemoveAt(e.RowIndex);
                 }
             }
         }

         private void ctrlJenisAnggaran1_Load_1(object sender, EventArgs e)
         {

         }

         private void cmdRefrshIndikator_Click(object sender, EventArgs e)
         {
             IndikatorLogic oLogic = new IndikatorLogic(GlobalVar.TahunAnggaran,3);
             if (MessageBox.Show("Refrsh ini akan menghapus yang sudah ada. Anda akan melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
             {
                 Indikator oIndikator = new Indikator();

                 if (oLogic.Refresh(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan))
                 {
                     List<Indikator> _lst = new List<Indikator>();
                     string _sNamaIndikator = "";
                     _lst = oLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan,2);
                     gridIndikator.Rows.Clear();

                     foreach (Indikator i in _lst)
                     {
                         if (_sNamaIndikator != i.NamaJenis)
                         {
                             string[] row = { i.NamaJenis, i.iJenis.ToString(), i.sIndikator, i.Target };
                             gridIndikator.Rows.Add(row);
                             _sNamaIndikator = i.NamaJenis;
                         }
                         else
                         {
                             string[] rowx = { "", i.iJenis.ToString(), i.sIndikator, i.Target };
                             gridIndikator.Rows.Add(rowx);
                         }

                     }
                 }
             }
         }
         private void Kosongkan()
         {
             gridRekening.Rows.Clear();
             gridIndikator.Rows.Clear();
             lblKegiatan.Text = "";
             lblProgram.Text = "";
             lblUrusan.Text = "";
             lblPagu.Text = "";
             lblJumlah.Text = "";
             txtSelisih.Text ="";

         }

         private void gridRekening_DataError(object sender, DataGridViewDataErrorEventArgs e)
         {
             e.Cancel = true;

         }

         private void gridRekening_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
         {
             m_iRowJustAdded = e.RowIndex;
         }

         private void cmdSetYAD_Click(object sender, EventArgs e)
         {
             for (int i = 0; i < gridRekening.Rows.Count - 1; i++)
             {
                 gridRekening.Rows[i].Cells[COL_YAD].Value = (DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_JUMLAH].Value) + DataFormat.GetDecimal(gridRekening.Rows[i].Cells[COL_JUMLAH].Value) / 10).FormatUang();
             }
         }
         private void RefreshBasedFormMode()
         {
             //if (_FormMode == 0) // RKA MODE
             //{
             //    if (_FormTahap < 2)
             //    {
             //        lblJenis.Text = "Jenis RKA";
             //        cmdCetak.Text = "Cetak RKA Ini";
             //        button1.Text = "RKA 1";
             //        button2.Text = "RKA 2.2";
             //    }
             //    else
             //    {
             //        lblJenis.Text = "Jenis RKAP";
             //        cmdCetak.Text = "Cetak RKAP Ini";
             //        button1.Text = "RKA 1";
             //        button2.Text = "RKA 2.2";
             //    }
             //    tabRekening.TabPages.Remove(tabTriwulan);
             //}
             //else
             //{
               //  if (_FormTahap < 2)
             //    {
                     lblJenis.Text = "Jenis DPA";
                     cmdCetak.Text = "Cetak DPA Ini";
                     button1.Text = "DPA 1";
                     button2.Text = "DPA 2.2";
              //   }
              //   else
              //   {
                //     lblJenis.Text = "Jenis DPAP";
               //      cmdCetak.Text = "Cetak DPAP Ini";
               //      button1.Text = "DPAP 1";
               //      button2.Text = "DPAP 2.2";
              //   }
                 tabRekening.TabPages.Remove(tabIndikator);
                 tabRekening.TabPages.Remove(tabTimDPA);

            // }
             
         }

         private void tabSumberDana_Click(object sender, EventArgs e)
         {

         }
         private void PrepareGridSumberDana()
         {
             if (m_IDKegiatan == 0 || m_IDUrusan == 0 || m_IDProgram == 0)
             {
                 return;
             }
             SumberDanaLogic oLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
             List<SumberDana> _lst = new List<SumberDana>();
             _lst = oLogic.Get();
             if (_lst != null)
             {

             }
         }

         
         private void button3_Click(object sender, EventArgs e)
         {
             SamakanDenganRKA();
             


         }
         private void SamakanDenganRKA()
         {
             TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
             if (oLogic.SamakanDPAdenganRKA((int)GlobalVar.TahunAnggaran, m_IDUrusan, ctrlDinas1.GetID(), m_IDProgram, m_IDKegiatan,(int)_FormTahap) == true)
             {
                 MessageBox.Show("Data DPA sama dengan RKA, kecuali yang sudah pernah diinput.");
                 LoadAnggaran();
             }
             else
             {
                 MessageBox.Show(oLogic.LastError());
             }
         }
         private void SamakanRKADenganDPA()
         {
             TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
             if (oLogic.SamakanRKAdenganDPA((int)GlobalVar.TahunAnggaran, m_IDUrusan, ctrlDinas1.GetID(), m_IDProgram, m_IDKegiatan,2))
             {
                 MessageBox.Show("Data DPA sama dengan RKA, kecuali yang sudah pernah diinput.");
                 LoadAnggaran();
             }
             else
             {
                 MessageBox.Show(oLogic.LastError());
             }
         }



         private void ctrlDinas1_Load(object sender, EventArgs e)
         {

         }

         private void treeRekening1_DoubleClicking_1(Rekening rek)
         {
             if (ctrlDinas1.GetID() == 0)
             {
                 MessageBox.Show("Pilihan Dinasnya terlebih dahulu.");

                 return;
             }

             // cek apakah sudah ada dalam grid.
             if (ctrlJenisAnggaran1.GetID() == 3)
             {
                 if (m_IDKegiatan == 0 || m_IDProgram == 0 || m_IDUrusan == 0)
                 {
                     MessageBox.Show("Program Kegiatan Belum dipilih. Silakan pilih Program terlebih dahulu.");
                     return;
                 }
             }


             bool bFound = false;
             for (int id = 0; id < gridRekening.Rows.Count; id++)
             {
                 if (rek.ID == DataFormat.GetLong(gridRekening.Rows[id].Cells[0].Value))
                 {
                     bFound = true;
                 }
             }
             if (bFound == false)
             {
                 string[] row = { rek.ID.ToString(), "-", "Hapus", "0", "0", "0", "0", "", "", rek.Tampilan, "", "0", rek.Nama, "0", "", "0", "0", "0", "", "0", "0", GlobalVar.TahapAnggaran.ToString(), "0", "0" };

                 gridRekening.Rows.Add(row);
                 gridRekening.Rows[gridRekening.Rows.Count - 2].DefaultCellStyle = _hilightstyle;
             }
             else
                 MessageBox.Show("Rekening sudah ada dalam RKA");
         }

         private void button3_Click_1(object sender, EventArgs e)
         {

         }

         private void LoadDataAnggaranKas()
         {
             int _pJenis = 0;
             _pJenis = ctrlJenisAnggaran1.GetID();


             AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
             List<AnggaranKas> _lst = new List<AnggaranKas>();
             _lst = oLogic.Prepare(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(),0, m_IDUrusan, m_IDKegiatan, _pJenis, 2, m_IDSubKegiatan);
             gridAnggaranKas.Rows.Clear();
             //int i = 0;
             foreach (AnggaranKas ak in _lst)
             {
                 string[] row = { ak.Level.ToString(), ak.IDUrusan.ToString(), ak.IDProgram.ToString(), ak.IDKegiatan.ToString(), ak.IDRekening.ToString(), ak.Jenis.ToString(), ak.TampilanKode(GlobalVar.ProfileProgramKegiatan), ak.Nama, ak.Anggaran.ToRupiahInReport(), "0", ak.Anggaran.ToRupiahInReport() };

                 gridAnggaranKas.Rows.Add(row);
             }
             //Format();
             List<AnggaranKas> _lst2 = new List<AnggaranKas>();
             _lst2 = oLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(),2);

             foreach (AnggaranKas ak in _lst2)
             {
                 for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
                 {
                     if (ak.IDUrusan == GetIntValue(i, COL_AKIDURUSAN) && ak.IDProgram == GetIntValue(i, COL_AKIDPROGRAM) &&
                         ak.IDKegiatan == GetIntValue(i, COL_AKIDKEGIATAN) && ak.IDRekening == GetIntValue(i, COL_AKIDREKENING) && ak.Jenis == GetSngValue(i, COL_AKJENIS))
                     {
                         SetValue(i, COL_AKJANUARI, ak.Bulan1);
                         SetValue(i, COL_AKFEBRUARI, ak.Bulan2);
                         SetValue(i, COL_AKMARET, ak.Bulan3);
                         SetValue(i, COL_AKAPRIL, ak.Bulan4);
                         SetValue(i, COL_AKMEI, ak.Bulan5);
                         SetValue(i, COL_AKJUNI, ak.Bulan6);
                         SetValue(i, COL_AKJULI, ak.Bulan7);
                         SetValue(i, COL_AKAGUSTUS, ak.Bulan8);
                         SetValue(i, COL_AKSEPTEMBER, ak.Bulan9);
                         SetValue(i, COL_AKOKTOBER, ak.Bulan10);
                         SetValue(i, COL_AKNOVEMBER, ak.Bulan11);
                         SetValue(i, COL_AKDESEMBER, ak.Bulan12);
                         //RekapHorisontal(i);
                         //HitungHOrizontal(i);
                     }
                 }
             }
             for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
             {
               //  RekapHorisontal(i);
             }
         }
         private Single GetSngValue(int i, int c)
         {
             return DataFormat.GetSingle(gridAnggaranKas.Rows[i].Cells[c].Value);
         }
         private void SetValue(int i, int c, decimal val)
         {
             gridAnggaranKas.Rows[i].Cells[c].Value = val.ToRupiahInReport();// FormatUang();

         }

         private decimal GetValue(int i, int c)
         {
             // if (c > 10)
             //{
             string sNIlai = DataFormat.GetString(gridAnggaranKas.Rows[i].Cells[c].Value);
             decimal lRet = DataFormat.FormatUangReportKeDecimal(sNIlai);//DataFormat.GetString(gridAnggaranKas.Rows[i].Cells[c].Value));
             return lRet;
             //}
             //else
             //{
             //  return 0;
             //}
         }
         private Int32 GetIntValue(int i, int c)
         {
             return DataFormat.GetInteger(gridAnggaranKas.Rows[i].Cells[c].Value);
         }
         private Single GetSngAKValue(int i, int c)
         {
             return DataFormat.GetSingle(gridAnggaranKas.Rows[i].Cells[c].Value);
         }

         private void cmdCekDPA_Click(object sender, EventArgs e)
         {
             //frmCekDPAPlafon fCek = new frmCekDPAPlafon();
             //fCek.ShowDialog();
         }

         private void cmdCetakSatuDinas_Click(object sender, EventArgs e)
         {

         }

         private void cmdSimpan_Click(object sender, EventArgs e)
         {

         }

         private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
         {

         }

         private void LoadAnggaranKas()
         {
             int _pJenis = 0;
             _pJenis = ctrlJenisAnggaran1.GetID();


             AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
             List<AnggaranKas> _lst = new List<AnggaranKas>();
             _lst = oLogic.Prepare(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(),0, m_IDUrusan, m_IDKegiatan, _pJenis, 2, m_IDSubKegiatan);
             gridAnggaranKas.Rows.Clear();
             //int i = 0;
             foreach (AnggaranKas ak in _lst)
             {
                 string[] row = { ak.Level.ToString(), ak.IDUrusan.ToString(), ak.IDProgram.ToString(), ak.IDKegiatan.ToString(), ak.IDRekening.ToString(), ak.Jenis.ToString(), ak.TampilanKode(GlobalVar.ProfileProgramKegiatan), ak.Nama, ak.Anggaran.ToRupiahInReport(), "0", ak.Anggaran.ToRupiahInReport() };

                 gridAnggaranKas.Rows.Add(row);
             }
             Format();
             List<AnggaranKas> _lst2 = new List<AnggaranKas>();
             _lst2 = oLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(),2);

             foreach (AnggaranKas ak in _lst2)
             {
                 for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
                 {
                     if (ak.IDUrusan == GetIntValue(i, COL_AKIDURUSAN) && ak.IDProgram == GetIntValue(i, COL_AKIDPROGRAM) &&
                         ak.IDKegiatan == GetIntValue(i, COL_AKIDKEGIATAN) && ak.IDRekening == GetIntValue(i, COL_AKIDREKENING) && ak.Jenis == GetSngValue(i, COL_AKJENIS))
                     {
                         SetValue(i, COL_AKJANUARI, ak.Bulan1);
                         SetValue(i, COL_AKFEBRUARI, ak.Bulan2);
                         SetValue(i, COL_AKMARET, ak.Bulan3);
                         SetValue(i, COL_AKAPRIL, ak.Bulan4);
                         SetValue(i, COL_AKMEI, ak.Bulan5);
                         SetValue(i, COL_AKJUNI, ak.Bulan6);
                         SetValue(i, COL_AKJULI, ak.Bulan7);
                         SetValue(i, COL_AKAGUSTUS, ak.Bulan8);
                         SetValue(i, COL_AKSEPTEMBER, ak.Bulan9);
                         SetValue(i, COL_AKOKTOBER, ak.Bulan10);
                         SetValue(i, COL_AKNOVEMBER, ak.Bulan11);
                         SetValue(i, COL_AKDESEMBER, ak.Bulan12);
                         //RekapHorisontal(i);
                         //HitungHOrizontal(i);
                     }
                 }
             }
             for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
             {
                 RekapHorisontal(i);
             }

         }
         private void Format()
         {
             for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
             {
                 if (GetIntValue(i, COL_AKID) == 0)
                 {
                     gridAnggaranKas.Rows[i].DefaultCellStyle = _hilightstyle;
                 }
                 else
                 {
                     gridAnggaranKas.Rows[i].DefaultCellStyle = _normalstyle;
                     for (int col = 0; col < COL_AKJANUARI; col++)
                     {
                         Color clr = new Color();

                         gridAnggaranKas.Rows[i].Cells[col].Style.BackColor = Color.LightGray;

                     }
                 }
                 //i++;

             }
         }
         private void RekapHorisontal(int baris)
         {
             decimal jumlahdialokasikan = HitungHOrizontal(baris);
             SetValue(baris, 9, jumlahdialokasikan);
             SetValue(baris, 10, GetValue(baris, 8) - GetValue(baris, 9));

             if (GetValue(baris, 8) - GetValue(baris, 9) < 0)
             {
                 gridAnggaranKas.Rows[baris].DefaultCellStyle = _wrongstyle;
                 MessageBox.Show("Melebihi Pagu");
                 cmdSimpanAK.Enabled = false;
             }
             else
             {
                 gridAnggaranKas.Rows[baris].DefaultCellStyle = _normalstyle;
                 cmdSimpanAK.Enabled = true;
             }
         }
         private decimal HitungHOrizontal(int row)
         {
             decimal cJumlah = 0L;
             for (int i = COL_AKJANUARI; i <= COL_AKDESEMBER; i++)
             {
                 cJumlah = cJumlah + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranKas.Rows[row].Cells[i].Value));

             }
             return cJumlah;
         }
         private decimal HitungVerical(int row, int col)
         {
             decimal cJumlah = 0L;
             Single iLevel = GetLevel(row);// DataFormat.GetInteger(gridAnggaranKas.Rows[row].Cells[0].Value);

             for (int i = row + 1; i < gridAnggaranKas.Rows.Count; i++)
             {
                 if (GetLevel(i) > 0)
                 {

                     if (GetLevel(i) == 1)
                     {
                         cJumlah = cJumlah + DataFormat.GetDecimal(gridAnggaranKas.Rows[i].Cells[col].Value);
                     }
                     else
                     {
                         break;
                     }
                 }
                 else
                     break;


             }

             return cJumlah;

         }

         private void ctrlJenisAnggaran1_Load_2(object sender, EventArgs e)
         {

         }
          
        private Single GetLevelAK(int row)
        {
            return GetSngValue(row, COL_AKID);
        }
         private void gridAnggaranKas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
         {
       
            if (GetLevelAK(e.RowIndex) == 0)
            {
                gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }else{
                if (e.ColumnIndex >= COL_AKJANUARI)
                {
                    gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }
                
            }
            // BeginEdit
            if (e.ColumnIndex >=COL_AKJANUARI)
            {
                if (gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    //DataFormat.ToRupiahInReport(
                    gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));

                    //DataGridViewCell _cell = gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    //DataFormat.FormatUangKeDecimal(ref _cell);
                }
            }
        }

        private void gridAnggaranKas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= COL_AKJANUARI && e.ColumnIndex <= COL_AKDESEMBER)
            {
                string _val = (DataFormat.GetString(gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                if (_val.Length < 1)
                    return;
                if (_val.Substring(0, 1) == "/")
                {
                    string _valDiinput = _val.Substring(1);
                    int _devider = 0;
                    int _step = 1;
                    int idxKoma = 0;
                    idxKoma = _valDiinput.IndexOf(",");
                    if (idxKoma > 0)
                    {
                        _step = DataFormat.GetInteger(_valDiinput.Substring(idxKoma + 1));
                        _devider = DataFormat.GetInteger(_valDiinput.Substring(0, idxKoma));

                    }
                    else
                    {
                        _devider = DataFormat.GetInteger(_val.Substring(1));
                        _step = 1;
                    }



                    if (_devider == 0)
                    {
                        return;
                    }
                    else
                    {

                        if (e.ColumnIndex + _devider - 1 > COL_AKDESEMBER)
                        {
                            if (MessageBox.Show("Bulan Bagi Lebih besar dari sisa bulan " + _devider.ToString() + " Mulai bulan ini.., Apakah ingin melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            //    return ;
                        }

                        long _valPerMonth = 0L;
                        decimal _anggaran = GetValue(e.RowIndex, COL_AKANGGARAN);
                        _valPerMonth = (long)_anggaran / _devider;
                        long runningSUm = 0;
                        for (int idc = e.ColumnIndex; idc < gridAnggaranKas.ColumnCount; )
                        {

                            if (idc == e.ColumnIndex + _devider - 1)
                            {
                                SetValue(e.RowIndex, idc, _anggaran - runningSUm);
                                break;
                            }
                            else
                            {
                                SetValue(e.RowIndex, idc, _valPerMonth);
                                runningSUm = runningSUm + _valPerMonth;
                            }
                            idc = idc + _step;


                        }

                        // _valPerMonth

                    }
                    //HitungBaris()
                    RekapHorisontal(e.RowIndex);
                    //RekapHorizontal(e.RowIndex);


                }
                else
                {
                    decimal val = DataFormat.FormatUangReportKeDecimal(gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val.ToRupiahInReport();// FormatUang();


                    decimal jumlahdialokasikan = HitungHOrizontal(e.RowIndex);
                    SetValue(e.RowIndex, 9, jumlahdialokasikan);
                    SetValue(e.RowIndex, 10, GetValue(e.RowIndex, 8) - GetValue(e.RowIndex, 9));

                    if (GetValue(e.RowIndex, 8) - GetValue(e.RowIndex, 9) < 0)
                    {
                        gridAnggaranKas.Rows[e.RowIndex].DefaultCellStyle = _wrongstyle;
                        MessageBox.Show("Melebihi Pagu");
                        cmdSimpanAK.Enabled = false;
                    }
                    else
                    {
                        gridAnggaranKas.Rows[e.RowIndex].DefaultCellStyle = _normalstyle;
                        cmdSimpanAK.Enabled = true;
                    }
                    //DataFormat.FormatUang(ref _cell);
                }
                // HitungBaris(e.ColumnIndex, e.RowIndex);

            }
        
        }

        private void LoadSumberMasterDana()
        {

            SumberDanaLogic oLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
            List<SumberDana> _lst = new List<SumberDana>();
            gridSumberDana.Rows.Clear();
            try
            {
                _lst = oLogic.Get();
                if (_lst != null)
                {
                    foreach (SumberDana sd in _lst)
                    {
                        string[] row = { sd.ID.ToString(), "false", sd.Nama };
                        gridSumberDana.Rows.Add(row);

                        //gridSumberd
                        //DataGridViewCellStyle celType = new DataGridViewCellStyle();
                        //celType.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
                        //newCol.Name = sd.ID.ToString();
                        //newCol.HeaderText = sd.Nama;
                        //newCol.DefaultCellStyle = celType;
                        //DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
                        //newCol.CellType = colType;

                        //newCol.CellType=DataGridViewCell

                        //newCol.ValueType = typeof(decimal);

                        //gridSumberDana.Columns.Add(newCol);
                    }
                }
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            //}
            //for (int i = 0; i < gridSumberDana.Rows.Count; i++)
            //{
            //    CekSumberDana(i);
            //}

        }

        private void gridAnggaranKas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (GetLevelAK(e.RowIndex) == 0)
            {
                gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }
            else
            {
                if (e.ColumnIndex >= COL_AKJANUARI)
                {
                    gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }

            }
        }

        private void DisplayKegiatan(TKegiatanAPBD oKeg)
        {

            LoadSumberMasterDana();
            txtAnggaranTahunLalu.Text = "";
            txtAnggaranYAD.Text = "";
            txtAPBDPLalu.Text = "";
            lblKegiatan.Text = ""; //.Nama;
            lblPagu.Text = "";
            txtKeterangan.Text = "";
            txtKelompokSasaran.Text = "";
            txtLokasi.Text = "";
            chkLuncuram.Checked = false;


            if (oKeg != null)
            {
                lblKegiatan.Text = oKeg.TampilanKode + "  " + oKeg.Nama; //.Nama;
                lblPagu.Text = oKeg.Pagu.ToRupiahInReport();
                txtKeterangan.Text = oKeg.Keterangan;
                txtKelompokSasaran.Text = oKeg.KelompokSasaran;
                txtLokasi.Text = oKeg.Lokasi;
                chkLuncuram.Checked = oKeg.ApakahLanjutan == 1 ? true : false;


                if (oKeg.TanggalPembahasan.Year < 2000)
                {
                    dtPembahasan.Value = DateTime.Now.Date;// oKeg.TanggalPembahasan;
                }
                else
                {
                    dtPembahasan.Value = oKeg.TanggalPembahasan;
                }
                txtAnggaranTahunLalu.Text = oKeg.AnggaranTahunLalu.ToRupiahInReport();
                txtAnggaranYAD.Text = oKeg.AnggaranTahunDepan.ToRupiahInReport();
                txtAPBDPLalu.Text = oKeg.APBDPTahunLalu.ToRupiahInReport();
                txtAPBDPLalu.Text = oKeg.APBDPTahunLalu.ToRupiahInReport();

                if (ctrlJenisAnggaran1.GetID() == 3)
                {
                    IndikatorLogic oIndikatorLogic = new IndikatorLogic(GlobalVar.TahunAnggaran,3);
                    List<Indikator> _lst = new List<Indikator>();
                    string _sNamaIndikator = "";
                    _lst = oIndikatorLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan,2);
                    gridIndikator.Rows.Clear();
                    foreach (Indikator i in _lst)
                    {
                        if (_sNamaIndikator != i.NamaJenis)
                        {
                            string[] row = { i.NamaJenis, i.iJenis.ToString(), i.sIndikator, i.Target };
                            gridIndikator.Rows.Add(row);
                            _sNamaIndikator = i.NamaJenis;
                        }
                        else
                        {
                            string[] rowx = { "", i.iJenis.ToString(), i.sIndikator, i.Target };
                            gridIndikator.Rows.Add(rowx);
                        }


                    }
                }

       
                LoadLokasi();
                LoadSumberDana();
            }

        }
        private void LoadSumberDana()
        {
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran,3);
            List<TSumberDana> _lst = new List<TSumberDana>();
            _lst = oLogic.Get((int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan,0);

            if (_lst != null)
            {
                foreach (TSumberDana ts in _lst)
                {
                    for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                    {
                        if (ts.IDRekening == 0 && DataFormat.GetInteger(gridSumberDana.Rows[i].Cells[0].Value) == ts.IDSUmberDana)
                        {

                            DataGridViewRow row = gridSumberDana.Rows[i];
                            DataGridViewCheckBoxCell chk = row.Cells[1] as DataGridViewCheckBoxCell;
                            chk.Value = true;

                        }

                    }
                }
            }

        }
        private void cmdSimpanAK_Click(object sender, EventArgs e)
        {
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            for (int row = 0; row < gridAnggaranKas.Rows.Count; row++)
            {
                if (GetIntValue(row, COL_AKIDREKENING) > 0)
                {
                    AnggaranKas ak = new AnggaranKas(
                        GetIntValue(row, COL_AKIDURUSAN), GlobalVar.TahunAnggaran,
                        GetIntValue(row, COL_AKIDPROGRAM), GetIntValue(row, COL_AKIDKEGIATAN), ctrlDinas1.GetID(),
                        GetIntValue(row, COL_AKIDREKENING),
                        GetValue(row, COL_AKJANUARI), GetValue(row, COL_AKFEBRUARI), GetValue(row, COL_AKMARET), GetValue(row, COL_AKAPRIL), GetValue(row, COL_AKMEI), GetValue(row, COL_AKJUNI), GetValue(row, COL_AKJULI),
                        GetValue(row, COL_AKAGUSTUS), GetValue(row, COL_AKSEPTEMBER), GetValue(row, COL_AKOKTOBER), GetValue(row, COL_AKNOVEMBER), GetValue(row, COL_AKDESEMBER), GetSngValue(row, COL_AKJENIS), 0, 0, 0, 0, "0", 2, m_IDSubKegiatan, 0);

                    _lst.Add(ak);
                }
            }
            if (oLogic.Simpan(_lst, (int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(),0, m_IDUrusan, m_IDProgram, m_IDKegiatan, (int)ctrlJenisAnggaran1.GetID(),2,m_IDSubKegiatan) == true)
            {
                MessageBox.Show("Penyimpanan selesai");
            }
            else
            {
                MessageBox.Show("Penyimpanan Gagal" + oLogic.LastError());
            }     
        }

        private void treeProgramKegiatan1_Load_1(object sender, EventArgs e)
        {

        }

        private void cmdSimpanUmum_Click(object sender, EventArgs e)
        {

            TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
            TKegiatanAPBD o = new TKegiatanAPBD();
            //o.IDDinas = ctrlDinas1.GetID();
            //o.IDKegiatan = m_IDKegiatan;
            //o.IDProgram = m_IDProgram;
            //o.IDUrusan = m_IDUrusan;
            //o.Tahun = GlobalVar.TahunAnggaran;
            //o.KelompokSasaran = txtKelompokSasaran.Text;
            //o.Keterangan = txtKeterangan.Text;
            //o.Lokasi = txtLokasi.Text;
            o.SumberDana = GetStringSumberDana();
            //o.AnggaranTahunDepan = DataFormat.FormatUangReportKeDecimal(txtAnggaranYAD.Text);
            //o.AnggaranTahunLalu = DataFormat.FormatUangReportKeDecimal(txtAnggaranTahunLalu.Text);
            //o.APBDPTahunLalu = 0;// DataFormat.FormatUangReportKeDecimal(txtAPBDPLalu.Text);
            //o.Jenis = ctrlJenisAnggaran1.GetID();
            //o.TanggalPembahasan = dtPembahasan.Value.Date;
            //o.TahapInput = GlobalVar.TahapAnggaran;
            //o.ApakahLanjutan = chkLuncuram.Checked == true ? 1 : 0;

            //o.ListIndikator = LoadIndikator();
            //o.ListCatatan = LoadCatatanKegiatan();
            if (oLogic.SimpanKolomSumberDana(o) == true)
            {
                SimpanSumberDana();
                MessageBox.Show("Penyimpanan  selesai");
            }
            else
            {
               MessageBox.Show("Kesalahan dalam penyimpanan " + oLogic.LastError());

            }

          
        }


        private bool SimpanSumberDana()
        {


            try
            {
                List<TSumberDana> _lsd = new List<TSumberDana>();
                TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran,3);
                for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                {



                    DataGridViewRow row = gridSumberDana.Rows[i];
                    DataGridViewCheckBoxCell chk = row.Cells[1] as DataGridViewCheckBoxCell;
                    if (chk.Value != null)
                    {
                        if (DataFormat.GetBoolean(row.Cells[1].Value) == true)
                        {
                            TSumberDana oTSD = new TSumberDana();
                            oTSD.Tahun = (int)GlobalVar.TahunAnggaran;
                            oTSD.IDDinas = ctrlDinas1.GetID();
                            oTSD.IDKegiatan = m_IDKegiatan;
                            oTSD.IDProgram = m_IDProgram;
                            oTSD.IDUrusan = m_IDUrusan;
                            oTSD.IDRekening = 0;
                            oTSD.IDSUmberDana = DataFormat.GetInteger(gridSumberDana.Rows[i].Cells[0].Value);
                            oTSD.Jumlah = 0;
                            _lsd.Add(oTSD);


                        }
                    }
                }
                oLogic.Simpan(_lsd, GlobalVar.TahunAnggaran, m_IDUrusan, ctrlDinas1.GetID(), m_IDProgram, m_IDKegiatan);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penyimpanan sumber dana gagal" + ex.Message);

                return false;
            }

        }
        private string GetStringSumberDana()
        {
            string sSumberDana = "";
            for (int i = 0; i < gridSumberDana.Rows.Count; i++)
            {



                DataGridViewRow row = gridSumberDana.Rows[i];
                DataGridViewCheckBoxCell chk = row.Cells[1] as DataGridViewCheckBoxCell;
                if (chk.Value != null)
                {
                    if (DataFormat.GetBoolean(row.Cells[1].Value) == true)
                    {
                        sSumberDana = sSumberDana + DataFormat.GetString((gridSumberDana.Rows[i].Cells[2].Value));


                    }
                }
            }
            return sSumberDana;

        }
    }
}
