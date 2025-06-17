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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.IO;


namespace KUAPPAS
{
    public partial class frmAnggaranKas : Form 
    {
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _wrongstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanMerahHeader = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanMerahDetail = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanHitamHeader = new DataGridViewCellStyle();
        DataGridViewCellStyle _tulisanHitamDetail = new DataGridViewCellStyle();
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;

        private const int COL_ID=0;

        private const int COL_IDURUSAN=1;
        private const int COL_IDPROGRAM=2;
        private const int COL_IDKEGIATAN=3;
        private const int COL_IDREKENING=4;
        private const int COL_JENIS=5;
        private const int COL_KODE=6;
        private const int COL_NAMA=7;
        private const int COL_ANGGARAN=8;
        private const int COL_JUMLAH=9;
        private const int COL_SISAANGGARAN = 10;
        private const int COL_JANUARI=11;
        private const int COL_FEBRUARI=12;
        private const int COL_MARET = 13;
        private const int COL_APRIL = 14;
        private const int COL_MEI = 15;
        private const int COL_JUNI = 16;
        private const int COL_JULI = 17;
        private const int COL_AGUSTUS = 18;
        private const int COL_SEPTEMBER = 19;
        private const int COL_OKTOBER =20;
        private const int COL_NOVEMBER = 21;
        private const int COL_DESEMBER = 22;

         private int  m_IDKegiatan;
         private int m_IDUrusan ;
         private int m_IDProgram;
         private int m_iTahap;
         private int mProfile;
         private long m_IDSubKegiatan;
         private TSubKegiatan m_oSubKegiatan;
         private string NamaUrusan;
         private string NamaProgram;
         private string NamaKegiatan;
         private string NamaSubKegiatan;
         private int m_IDDInas;
         private int m_iUnitAnggaran;
         private int m_IDUit;
         private int m_iKodeuk;

         PdfPage previousPage;

    //        InitializeComponent();
            
        public frmAnggaranKas()
        {
            InitializeComponent();
            _hilightstyle.Font = new Font(gridAnggaranKas.Font, FontStyle.Bold);
            _hilightstyle.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            _normalstyle.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _normalstyle.BackColor = Color.White;
            mProfile = 2;
            _wrongstyle.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _wrongstyle.ForeColor = Color.Red;

            _tulisanMerahHeader.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanMerahHeader.ForeColor = Color.Red;
            _tulisanMerahHeader.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);


            _tulisanMerahDetail.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanMerahDetail.ForeColor = Color.Red;
            _tulisanMerahDetail.BackColor = Color.White;// GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            m_iUnitAnggaran = 0;

            _tulisanHitamHeader.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanHitamHeader.ForeColor = Color.Black;
            _tulisanHitamHeader.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            m_IDSubKegiatan = 0;
            m_oSubKegiatan = new TSubKegiatan();


            _tulisanHitamDetail.Font = new Font(gridAnggaranKas.Font, FontStyle.Regular);
            _tulisanHitamDetail.ForeColor = Color.Black;
            _tulisanHitamDetail.BackColor = Color.White;// GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            SaatnyacetakKesimpulan = false; ;
            
        }

        public int Profile
        {
            set { mProfile = value; }
        }
        public void SetTahap(int pTahap)
        {
            m_iTahap = pTahap;
        }
        private void cmdLoad_Click(object sender, EventArgs e)
        {
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            if (ctrlDinas1.WithUnitKerja() == false) { 
            _lst = oLogic.Prepare(GlobalVar.TahunAnggaran, m_IDDInas,0, 0, 0, 0, m_iTahap,m_IDSubKegiatan);
            } else {
                _lst = oLogic.Prepare(GlobalVar.TahunAnggaran, m_IDDInas,m_IDUit, 0, 0, 0, m_iTahap,m_IDSubKegiatan);
            }
            
            gridAnggaranKas.Rows.Clear();
            //int i = 0;
            foreach (AnggaranKas ak in _lst){
                string[] row = { ak.Level.ToString(), ak.IDUrusan.ToString(), ak.IDProgram.ToString(), ak.IDKegiatan.ToString(), ak.IDRekening.ToString(), ak.Jenis.ToString(), ak.TampilanKode(GlobalVar.ProfileProgramKegiatan), ak.Nama,ak.Anggaran.ToRupiahInReport(), "0", ak.Anggaran.ToRupiahInReport() };                
                gridAnggaranKas.Rows.Add(row);
            }
            Format();
            List<AnggaranKas> _lst2 = new List<AnggaranKas>();
            _lst2 = oLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_iTahap);

            foreach (AnggaranKas ak in _lst2)
            {
                for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
                {
                    if (ak.IDUrusan == GetIntValue(i,COL_IDURUSAN) && ak.IDProgram == GetIntValue(i,COL_IDPROGRAM) &&
                        ak.IDKegiatan == GetIntValue(i, COL_IDKEGIATAN) && ak.IDRekening== GetIntValue(i,COL_IDREKENING) && ak.Jenis == GetSngValue(i, COL_JENIS))
                    {
                        SetValue(i, COL_JANUARI, ak.Bulan1);
                        SetValue(i, COL_FEBRUARI, ak.Bulan2);
                        SetValue(i,COL_MARET, ak.Bulan3);
                        SetValue(i, COL_APRIL, ak.Bulan4);
                        SetValue(i,COL_MEI, ak.Bulan5);
                        SetValue(i, COL_JUNI, ak.Bulan6);
                        SetValue(i, COL_JULI, ak.Bulan7);
                        SetValue(i, COL_AGUSTUS, ak.Bulan8);
                        SetValue(i,COL_SEPTEMBER, ak.Bulan9);
                        SetValue(i, COL_OKTOBER, ak.Bulan10);
                        SetValue(i, COL_NOVEMBER, ak.Bulan11);
                        SetValue(i, COL_DESEMBER, ak.Bulan12);
                    }
                }                
            }
        }
        private void Persiapkan()
        {
            //int _pJenis = 0;
            //_pJenis = ctrlJenisAnggaran1.GetID();
            //AnggaranKasLogic ologic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            //ologic.PersiapkanAKPerubahan(ctrlDinas1.GetID(), m_IDKegiatan, _pJenis);

        }

        private void LoadData()
        {
            int _pJenis = 0;
            _pJenis = ctrlJenisAnggaran1.GetID();

            // ANGGRAN KAS ----
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            //if (ctrlDinas1.WithUnitKerja() == false|| ctrlJenisAnggaran1.GetID() !=3)
            //{
            //    _lst = oLogic.Prepare(GlobalVar.TahunAnggaran, m_IDDInas, 0,m_IDUrusan, m_IDKegiatan, _pJenis, m_iTahap, m_IDSubKegiatan);
            //}
            //else
            //{
            //int kodeuk = ctrlDinas1.UnitAnggaran;
           m_iUnitAnggaran= ctrlDinas1.UnitAnggaran;
            _lst = oLogic.Prepare(GlobalVar.TahunAnggaran, m_IDDInas, m_iUnitAnggaran , m_IDUrusan, m_IDKegiatan, _pJenis, m_iTahap, m_IDSubKegiatan);
           // }
            
            gridAnggaranKas.Rows.Clear();
            //int i = 0;
            if (_lst != null)
            {
                foreach (AnggaranKas ak in _lst)
                {

                    string[] row = { ak.Level.ToString(), ak.IDUrusan.ToString(), ak.IDProgram.ToString(),
                                   ak.IDKegiatan.ToString(), ak.IDRekening.ToString(), ak.Jenis.ToString(),
                                   ak.Level ==0? m_IDSubKegiatan.ToKodeSubKegiatan(true) :ak.IDRekening.ToKodeRekening(6), ak.Nama, ak.Anggaran.ToRupiahInReport(), "0", ak.Anggaran.ToRupiahInReport() };

                    gridAnggaranKas.Rows.Add(row);
                }
            }
            Format();
            List<AnggaranKas> _lst2 = new List<AnggaranKas>();


            _lst2 = oLogic.Get(GlobalVar.TahunAnggaran, m_IDDInas, m_iUnitAnggaran, m_IDKegiatan, _pJenis, m_iTahap, m_IDSubKegiatan);
            

            foreach (AnggaranKas ak in _lst2)
            {
                for (int i = 0; i < gridAnggaranKas.Rows.Count; i++)
                {
                    if (ak.IDUrusan == GetIntValue(i, COL_IDURUSAN) && ak.IDProgram == GetIntValue(i, COL_IDPROGRAM) &&
                        ak.IDKegiatan == GetIntValue(i, COL_IDKEGIATAN) && ak.IDRekening == DataFormat.GetLong(gridAnggaranKas.Rows[i].Cells[COL_IDREKENING].Value.ToString()) && ak.Jenis == GetSngValue(i, COL_JENIS))
                    {
                        SetValue(i, COL_JANUARI, ak.Bulan1);
                        SetValue(i, COL_FEBRUARI, ak.Bulan2);
                        SetValue(i, COL_MARET, ak.Bulan3);
                        SetValue(i, COL_APRIL, ak.Bulan4);
                        SetValue(i, COL_MEI, ak.Bulan5);
                        SetValue(i, COL_JUNI, ak.Bulan6);
                        SetValue(i, COL_JULI, ak.Bulan7);
                        SetValue(i, COL_AGUSTUS, ak.Bulan8);
                        SetValue(i, COL_SEPTEMBER, ak.Bulan9);
                        SetValue(i, COL_OKTOBER, ak.Bulan10);
                        SetValue(i, COL_NOVEMBER, ak.Bulan11);
                        SetValue(i, COL_DESEMBER, ak.Bulan12);
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
                if (GetIntValue(i, COL_ID) == 0)
                {
                    gridAnggaranKas.Rows[i].DefaultCellStyle = _hilightstyle;
                }
                else
                {
                    gridAnggaranKas.Rows[i].DefaultCellStyle = _normalstyle;
                    for (int col = 0; col < COL_JANUARI; col++ )
                    {
                        Color clr = new Color();

                        gridAnggaranKas.Rows[i].Cells[col].Style.BackColor = Color.LightGray;

                    }
                }
                //i++;
                
            }
        }
        private void frmAnggaranKas_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlHeader1.SetCaption("Setting Anggaran Kas");
            gridAnggaranKas.FormatHeader();
            ctrlDinas1.Create();

            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDDInas = GlobalVar.Pengguna.SKPD;
            }
            int tahapanAnggaran = ctrlDinas1.GetTahapAnggaran();

            
            //TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            //TahapanAnggaran ta = new TahapanAnggaran();

            //ta = oLogic.GetByDinas(ctrlDinas1.GetID(), (int)GlobalVar.TahunAnggaran);
            if (tahapanAnggaran == 2)
            {
                cmdSimpanAK.Enabled = false;
            }
            else
            {
                cmdSimpanAK.Enabled = true ;
            }

            ctrlJenisAnggaran1.Create(1);
            lblUrusan.Text = "Urusan Pemerintahan";
            lblProgram.Text = "Program ";
            lblKegiatan.Text = "Kegiatan";
            lblSubKegiatan.Text = "Sub Kegiatan";

            ctrlJabatan1.CreatePenandaTangan();
        }
        private Single GetLevel(int row)
        {
            return GetSngValue(row, COL_ID);
        }
        private void gridAnggaranKas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (GetLevel(e.RowIndex) == 0)
            {
                gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }else{
                if (e.ColumnIndex >= COL_JANUARI)
                {
                    gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }
                
            }
            // BeginEdit
            if (e.ColumnIndex >=COL_JANUARI)
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
        // rekap hitung jan des dan 
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
            decimal cJumlah=0L;
            for (int i = COL_JANUARI; i <= COL_DESEMBER ; i++)
            {
                cJumlah = cJumlah + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridAnggaranKas.Rows[row].Cells[i].Value));

            }
            return cJumlah;
        }
        private decimal HitungVerical(int row,int col)
        {
            decimal cJumlah=0L;
            Single  iLevel = GetLevel(row);// DataFormat.GetInteger(gridAnggaranKas.Rows[row].Cells[0].Value);

            for (int i = row+1; i < gridAnggaranKas.Rows.Count; i++)

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

        private Single GetSngValue(int i, int c)
        {
            return DataFormat.GetSingle(gridAnggaranKas.Rows[i].Cells[c].Value);
        }
        private void SetValue(int i, int c, decimal val)
        {
            gridAnggaranKas.Rows[i].Cells[c].Value = val.ToRupiahInReport();// FormatUang();
            
        }
        private void gridAnggaranKas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= COL_JANUARI && e.ColumnIndex <=COL_DESEMBER )
            {
                string _val=(DataFormat.GetString(gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                if (_val.Length < 1)
                    return;
                if (_val.Substring(0, 1) == "/")
                {
                    string _valDiinput = _val.Substring(1);
                    int _devider = 0;
                    int _step = 1;
                    int idxKoma=0;
                    idxKoma=_valDiinput.IndexOf(",");
                    if (idxKoma > 0)
                    {
                        _step = DataFormat.GetInteger(_valDiinput.Substring(idxKoma + 1));
                        _devider = DataFormat.GetInteger(_valDiinput.Substring(0, idxKoma ));

                    }
                    else
                    {
                        _devider = DataFormat.GetInteger(_val.Substring(1));
                        _step = 1;
                    }
                    

                    
                    if(_devider ==0){
                        return ;
                    } else {

                        if(e.ColumnIndex + _devider-1 > COL_DESEMBER){
                            if (MessageBox.Show("Bulan Bagi Lebih besar dari sisa bulan " + _devider.ToString() + " Mulai bulan ini.., Apakah ingin melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                        //    return ;
                        }

                        long  _valPerMonth=0L;
                        decimal _anggaran = GetValue(e.RowIndex, COL_ANGGARAN);
                        _valPerMonth =(long) _anggaran / _devider;
                        long runningSUm = 0;
                        for (int idc = e.ColumnIndex; idc < gridAnggaranKas.ColumnCount; )
                        {

                            if (idc == e.ColumnIndex + _devider-1)
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
                    decimal val = DataFormat.GetDecimal(gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

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

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            
            AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            List<AnggaranKas> _lst = new List<AnggaranKas>();
            TahapanAnggaran ta = new TahapanAnggaran();
            TahapanAnggaranLogic taLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            ta = taLogic.GetByDinas(ctrlDinas1.GetID(), GlobalVar.TahunAnggaran);
            if (ta.StatusAnggaranKas == 9)
            {
                MessageBox.Show("Anggaran Kas Sudah di Kunci");
                return;
            }
            
            
            for (int row = 0; row < gridAnggaranKas.Rows.Count; row++)
            {
                if (gridAnggaranKas.Rows[row].Cells[COL_IDREKENING].Value != null)
                {
                    if (DataFormat.GetLong(gridAnggaranKas.Rows[row].Cells[COL_IDREKENING].Value.ToString()) > 0)
                    {
                        AnggaranKas ak = new AnggaranKas(
                            GetIntValue(row, COL_IDURUSAN), GlobalVar.TahunAnggaran,
                            GetIntValue(row, COL_IDPROGRAM), GetIntValue(row, COL_IDKEGIATAN), m_IDDInas,
                            DataFormat.GetLong(gridAnggaranKas.Rows[row].Cells[COL_IDREKENING].Value.ToString()),
                            GetValue(row, COL_JANUARI), GetValue(row, COL_FEBRUARI), GetValue(row, COL_MARET), GetValue(row, COL_APRIL), GetValue(row, COL_MEI), GetValue(row, COL_JUNI), GetValue(row, COL_JULI),
                            GetValue(row, COL_AGUSTUS), GetValue(row, COL_SEPTEMBER), GetValue(row, COL_OKTOBER), GetValue(row, COL_NOVEMBER), GetValue(row, COL_DESEMBER), GetSngValue(row, COL_JENIS), 0, 0, 0, 0, "0", m_iTahap, m_IDSubKegiatan,m_IDUit);

                        _lst.Add(ak);
                    }
                }
            }
            bool lRetSimpan = false;

           
            lRetSimpan = oLogic.Simpan(_lst, (int)GlobalVar.TahunAnggaran, m_IDDInas, m_iUnitAnggaran, m_IDUrusan, m_IDProgram, m_IDKegiatan, (int)ctrlJenisAnggaran1.GetID(), m_iTahap, m_IDSubKegiatan);
            
            if (lRetSimpan == true)
            {
                MessageBox.Show("Penyimpanan selesai");
            } else {
                MessageBox.Show("Penyimpanan Gagal" + oLogic.LastError());
            }            

        }

        private void gridAnggaranKas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CekEdit(int col, int baris )
        {
            if (baris < 0)
                return;

            if (col >= COL_JANUARI && col<= COL_DESEMBER)
            {
                string _val = (DataFormat.GetString(gridAnggaranKas.Rows[baris].Cells[col].Value));
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

                        if (col+ _devider - 1 > COL_DESEMBER)
                        {
                            if (MessageBox.Show("Bulan Bagi Lebih besar dari sisa bulan " + _devider.ToString() + " Mulai bulan ini.., Apakah ingin melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            //    return ;
                        }

                        long _valPerMonth = 0L;
                        decimal _anggaran = GetValue(baris, COL_ANGGARAN);
                        _valPerMonth = (long)_anggaran / _devider;
                        long runningSUm = 0;
                        for (int idc = col; idc < gridAnggaranKas.ColumnCount; )
                        {

                            if (idc == col + _devider - 1)
                            {
                                SetValue(baris, idc, _anggaran - runningSUm);
                                break;
                            }
                            else
                            {
                                SetValue(baris, idc, _valPerMonth);
                                runningSUm = runningSUm + _valPerMonth;
                            }
                            idc = idc + _step;


                        }

                        // _valPerMonth

                    }



                }
                else
                {

             
                    gridAnggaranKas.Rows[baris].Cells[col].Value = DataFormat.GetDecimal(gridAnggaranKas.Rows[baris].Cells[col].Value).ToRupiahInReport();

                    //DataFormat.FormatUang(ref _cell);
                }
            }

        }
        //private void OlahData()
        //{

        //}
        private void gridAnggaranKas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //////CekEdit(e.ColumnIndex,e.RowIndex);
            //if (e.RowIndex < 0)
            //    return;
            //string _val = (DataFormat.GetString(gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
            //if (_val.Length < 1)
            //    return;
            //if (_val.Substring(0, 1) != "/") {



            //    if (e.ColumnIndex >= COL_JANUARI && e.ColumnIndex <= COL_DESEMBER)
            //    {
            //        if (e.RowIndex >= 0 && e.RowIndex < gridAnggaranKas.Rows.Count)
            //        {

            //            decimal jumlahdialokasikan = HitungHOrizontal(e.RowIndex);
            //            SetValue(e.RowIndex, COL_JUMLAH, jumlahdialokasikan);
            //            SetValue(e.RowIndex, COL_SISAANGGARAN, GetValue(e.RowIndex, COL_ANGGARAN) - GetValue(e.RowIndex, COL_JUMLAH));

            //            if (GetValue(e.RowIndex, COL_ANGGARAN) - GetValue(e.RowIndex, COL_JUMLAH) < 0)
            //            {
            //                if (GetLevel(e.RowIndex) == 0)
            //                {
            //                    gridAnggaranKas.Rows[e.RowIndex].DefaultCellStyle = _tulisanMerahHeader;
            //                }
            //                else
            //                {
            //                    gridAnggaranKas.Rows[e.RowIndex].DefaultCellStyle = _tulisanMerahDetail;
            //                }

            //                // MessageBox.Show("Melebihi Pagu");
            //                SetValue(e.RowIndex, e.ColumnIndex, 0);
            //                //SetValue ( e.RowIndex, COL_JUMLAH,0);
            //                //GetValue(e.RowIndex, COL_JUMLAH));
            //                cmdSimpan.Enabled = false;
            //            }
            //            else
            //            {

            //                if (GetLevel(e.RowIndex) == 0)
            //                {
            //                    gridAnggaranKas.Rows[e.RowIndex].DefaultCellStyle = _tulisanHitamHeader;
            //                }
            //                else
            //                {
            //                    gridAnggaranKas.Rows[e.RowIndex].DefaultCellStyle = _tulisanHitamDetail;
            //                }

            //                cmdSimpan.Enabled = true;
            //            }
                        //int _barisKegiatan = 0;
                        //for (int i = e.RowIndex; i > 0; i--)
                        //{
                        //    if (GetSngValue(i, 0) == 0)
                        //    {
                        //        _barisKegiatan = i;
                        //        break;
                        //    }
                        //}
                        //decimal jumlahKeg = 0L;
                        //jumlahKeg = HitungVerical(_barisKegiatan, e.ColumnIndex);
                        //SetValue(_barisKegiatan, e.ColumnIndex, jumlahKeg);

                        //SetValue(_barisKegiatan, COL_JUMLAH, HitungHOrizontal(_barisKegiatan));
                        //SetValue(_barisKegiatan, COL_SISAANGGARAN, GetValue(_barisKegiatan, COL_ANGGARAN) - GetValue(_barisKegiatan, COL_JUMLAH));

            //            //HitungHOrizontal(_barisKegiatan);
            //        }
            //    }
            //}
        }

        private void HitungBaris(int col, int baris)
        {
            decimal jumlahdialokasikan = HitungHOrizontal(baris);
            SetValue(baris, COL_JUMLAH, jumlahdialokasikan);
            SetValue(baris, COL_SISAANGGARAN, GetValue(baris, COL_ANGGARAN) - GetValue(baris, COL_JUMLAH));

            if (GetValue(baris, COL_ANGGARAN) - GetValue(baris, COL_JUMLAH) < 0)
            {
                if (GetLevel(baris) == 0)
                {
                    gridAnggaranKas.Rows[baris].DefaultCellStyle = _tulisanMerahHeader;
                }
                else
                {
                    gridAnggaranKas.Rows[baris].DefaultCellStyle = _tulisanMerahDetail;
                }

                MessageBox.Show("Melebihi Pagu");
                SetValue(baris, col, 0);
                //SetValue ( baris, COL_JUMLAH,0);
                //GetValue(baris, COL_JUMLAH));
                cmdSimpanAK.Enabled = false;
            }
            else
            {

                if (GetLevel(baris) == 0)
                {
                    gridAnggaranKas.Rows[baris].DefaultCellStyle = _tulisanHitamHeader;
                }
                else
                {
                    gridAnggaranKas.Rows[baris].DefaultCellStyle = _tulisanHitamDetail;
                }

                cmdSimpanAK.Enabled = true;
            }
            int _barisKegiatan = 0;
            for (int i = baris; i > 0; i--)
            {
                if (GetSngValue(i, 0) == 0)
                {
                    _barisKegiatan = i;
                    break;
                }
            }
            decimal jumlahKeg = 0L;
            jumlahKeg = HitungVerical(_barisKegiatan, col);
            SetValue(_barisKegiatan, col, jumlahKeg);

            SetValue(_barisKegiatan, COL_JUMLAH, HitungHOrizontal(_barisKegiatan));
            SetValue(_barisKegiatan, COL_SISAANGGARAN, GetValue(_barisKegiatan, COL_ANGGARAN) - GetValue(_barisKegiatan, COL_JUMLAH));
                
        }
        private void gridAnggaranKas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (GetLevel(e.RowIndex) == 0)
            {
                gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }
            else
            {
                if (e.ColumnIndex >= COL_JANUARI)
                {
                    gridAnggaranKas.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }

            }
        }
        private void CetakAnggaranKas()
        {
             try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                //section1.PageSettings.Size = PdfPageSize.Ledger;
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 935;// = PdfPageSize.Legal;

                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Bottom= 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                SaatnyacetakKesimpulan = false;

                //PdfGrid headerGrid = new PdfGrid();

                //List<object> dataHeader = new List<object>();

                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                PdfGraphics graphics = page.Graphics;
                float kiri = 10;
                float postitikdua = 120;
                float posNama = 200;
                float lebarArena = page.GetClientSize().Width;
                yPos = 5;
                yPos = oCetakPDF.TulisItem(graphics, "PEMERINTAH KABUPATEN KETAPANG", 12, 10, yPos,
                    lebarArena, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(graphics, "ANGGARAN KAS", 12, 10, yPos,
                    lebarArena, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(graphics, "TAHUN ANGGARAN " + 
                         GlobalVar.TahunAnggaran.ToString (), 12,10 , yPos ,
                         lebarArena, stringFormat, true, false, true);

                stringFormat.Alignment = PdfTextAlignment.Left ;

                yPos = oCetakPDF.TulisItem(graphics, "OPD"
                       , 10,kiri , yPos,
                        150, stringFormat,false, false, true);

                //float kiri = 10;
                //float postitikdua = 120;
                //float posNama = 150;

                yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDDInas.ToKodeDinas()
                        , 10, postitikdua, yPos,
                         150, stringFormat, false , false, true);
                yPos = oCetakPDF.TulisItem(graphics, ctrlDinas1.GetNamaSKPD(),
                        10, posNama, yPos,
                        lebarArena-posNama, stringFormat, true, false, true);

                if (ctrlJenisAnggaran1.GetID() == 3)
                {
                    if (m_IDProgram > 0)
                    {
                        yPos = oCetakPDF.TulisItem(graphics, "Urusan Pemerintahan"
                              , 10, kiri, yPos,
                               150, stringFormat, false, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDUrusan.ToKodeUrusan()
                                , 10, postitikdua, yPos,
                                 150, stringFormat, false, false, true);
                        yPos = oCetakPDF.TulisItem(graphics, NamaUrusan
                               , 10, posNama, yPos,
                                lebarArena - posNama, stringFormat, true, false, true);
                        // Program
                        yPos = oCetakPDF.TulisItem(graphics, "Program"
                             , 10, kiri, yPos,
                              150, stringFormat, false, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDProgram.ToKodeProgram()
                                , 10, postitikdua, yPos,
                                 150, stringFormat, false, false, true);
                        yPos = oCetakPDF.TulisItem(graphics, NamaProgram
                               , 10, posNama, yPos,
                                lebarArena - posNama, stringFormat, true, false, true);

                        // Kegiatan

                        yPos = oCetakPDF.TulisItem(graphics, "Kegiatan"
                            , 10, kiri, yPos,
                             150, stringFormat, false, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDKegiatan.ToKodeKegiatan()
                                , 10, postitikdua, yPos,
                                 150, stringFormat, false, false, true);
                        yPos = oCetakPDF.TulisItem(graphics, NamaKegiatan
                               , 10, posNama, yPos,
                                lebarArena - posNama, stringFormat, true, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, "Sub Kegiatan"
                           , 10, kiri, yPos,
                            150, stringFormat, false, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDSubKegiatan.ToKodeSubKegiatan()
                                , 10, postitikdua, yPos,
                                 150, stringFormat, false, false, true);
                        yPos = oCetakPDF.TulisItem(graphics, NamaSubKegiatan
                               , 10, posNama, yPos,
                                lebarArena - posNama, stringFormat, true, false, true);
                    }
                }
                else
                {
                    if (ctrlJenisAnggaran1.GetID() == 1)
                    {
                        yPos = oCetakPDF.TulisItem(graphics, "Jenis "
                              , 10, kiri, yPos,
                               150, stringFormat, false, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, ": "
                                , 10, postitikdua, yPos,
                                 150, stringFormat, false, false, true);
                        yPos = oCetakPDF.TulisItem(graphics, "PENDAPATAN"
                               , 10, posNama, yPos,
                                lebarArena - posNama, stringFormat, true, false, true);
                    }
                    if (ctrlJenisAnggaran1.GetID() == 5)
                    {
                        yPos = oCetakPDF.TulisItem(graphics, "Jenis "
                              , 10, kiri, yPos,
                               150, stringFormat, false, false, true);

                        yPos = oCetakPDF.TulisItem(graphics, ": "
                                , 10, postitikdua, yPos,
                                 150, stringFormat, false, false, true);
                        yPos = oCetakPDF.TulisItem(graphics, "PEMBIAYAAN"
                               , 10, posNama, yPos,
                                lebarArena - posNama, stringFormat, true, false, true);
                    } 
                }
                yPos = yPos + 10;
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                table.Columns.Add("Anggaran");

                table.Columns.Add("Januari");
                table.Columns.Add("Februari");
                table.Columns.Add("Maret");
                table.Columns.Add("April");
                table.Columns.Add("Mei");
                table.Columns.Add("Juni");
                table.Columns.Add("Juli");
                table.Columns.Add("Agustus");
                table.Columns.Add("September");
                table.Columns.Add("Oktober");
                table.Columns.Add("November");
                table.Columns.Add("Desember");
                //table.Columns.Add("Jumlah ");

                decimal jumlahTW1 = 0L;
                decimal jumlahTW2 = 0L;
                decimal jumlahTW3 = 0L;
                decimal jumlahTW4 = 0L;

                decimal akumulasi = 0L;
                decimal sisa = 0;
                int rowtriwulan = 0;
                for (int idx = 0; idx < gridAnggaranKas.Rows.Count; idx++)
                {
                    
                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[6].Value),
                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[7].Value),                      
                       
                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[8].Value),
                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[11].Value),
                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[12].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[13].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[14].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[15].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[16].Value),
                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[17].Value),
                       DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[18].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[19].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[20].Value),
                        DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[21].Value),
                         DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[22].Value),

                        
                    });
                    rowtriwulan++;

                    jumlahTW1 = jumlahTW1 + gridAnggaranKas.Rows[idx].Cells[11].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[12].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[13].Value.FormatUangReportKeDecimal();
                    jumlahTW2 = jumlahTW2 + gridAnggaranKas.Rows[idx].Cells[14].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[15].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[16].Value.FormatUangReportKeDecimal();
                    jumlahTW3 = jumlahTW3 + gridAnggaranKas.Rows[idx].Cells[17].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[18].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[19].Value.FormatUangReportKeDecimal();
                    jumlahTW4 = jumlahTW4 + gridAnggaranKas.Rows[idx].Cells[20].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[21].Value.FormatUangReportKeDecimal() +
                    gridAnggaranKas.Rows[idx].Cells[22].Value.FormatUangReportKeDecimal();

                }
                table.Rows.Add(new string[]
                    {

                       "",
                       "JUMLAH ALOKASI DANA PER TRIWULAN",                      
                       "",
                       jumlahTW1.ToRupiahInReport(),
                       jumlahTW1.ToRupiahInReport(),
                       jumlahTW1.ToRupiahInReport(),
                       jumlahTW2.ToRupiahInReport(),
                       jumlahTW2.ToRupiahInReport(),
                       jumlahTW2.ToRupiahInReport(),
                       jumlahTW3.ToRupiahInReport(),
                       jumlahTW3.ToRupiahInReport(),
                       jumlahTW3.ToRupiahInReport(),
                       jumlahTW4.ToRupiahInReport(),
                       jumlahTW4.ToRupiahInReport(),
                       jumlahTW4.ToRupiahInReport(),

                  //      DataFormat.GetString(gridAnggaranKas.Rows[idx].Cells[10].Value),
                       
                        
                    });
                 
                //Add list to IEnumerable.
                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 100;

                // Angka 
                pdfGrid.Columns[2].Width = 50;
                pdfGrid.Columns[3].Width = 50;
                pdfGrid.Columns[4].Width = 50;
                pdfGrid.Columns[5].Width = 50;
                pdfGrid.Columns[6].Width = 50;
                pdfGrid.Columns[7].Width = 50;
                pdfGrid.Columns[8].Width = 50;
                pdfGrid.Columns[9].Width = 50;
                pdfGrid.Columns[10].Width = 50;
                pdfGrid.Columns[11].Width = 50;
                pdfGrid.Columns[12].Width = 50;
                pdfGrid.Columns[13].Width = 50;
                pdfGrid.Columns[14].Width = 50;
                //pdfGrid.Columns[15].Width =60;

                pdfGrid.Rows[rowtriwulan].Cells[3].ColumnSpan= 3;
                pdfGrid.Rows[rowtriwulan].Cells[6].ColumnSpan = 3;
                pdfGrid.Rows[rowtriwulan].Cells[9].ColumnSpan = 3;
                pdfGrid.Rows[rowtriwulan].Cells[12].ColumnSpan = 3;

                PdfGridStyle gridStyle = new PdfGridStyle();
     
                gridStyle.CellPadding = new PdfPaddings(3, 1, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                pdfGrid.Style = gridStyle;
    
                

                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[2].Format = formatKolomAngka;
                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;
                pdfGrid.Columns[7].Format = formatKolomAngka;
                pdfGrid.Columns[8].Format = formatKolomAngka;
                pdfGrid.Columns[9].Format = formatKolomAngka;
                pdfGrid.Columns[10].Format = formatKolomAngka;
                pdfGrid.Columns[11].Format = formatKolomAngka;
                pdfGrid.Columns[12].Format = formatKolomAngka;
                pdfGrid.Columns[13].Format = formatKolomAngka;
                pdfGrid.Columns[14].Format = formatKolomAngka;
              
                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();

              

              

                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();
                pdfGrid.RepeatHeader = true;

                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;


                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                
                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;
                
                 for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }                
                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow",6, FontStyle.Regular));
                cellStyle.Font = font;

                pdfGrid.Style.Font = font;// = cellStyle;
                pdfGrid.Style.CellPadding.Top = 1;
                cellStyle.Borders.All = new PdfPen(new PdfColor(192, 192, 217), 0.50f);
                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style = cellStyle;
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {
                       
                       
                            pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.1F;
                            pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.1F;
                            pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.1F;
                            pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.1F;
                        

                    }
                }
              



            
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10,yPos));
                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                 
                // yPos = pdfGridLayoutResult.Bounds.Bottom + 20;
                // Pejabat pimpinan = new Pejabat();
                //pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
                //yPos = oCetakPDF.TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtCetak.Value.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true);
                //yPos = oCetakPDF.TulisItem(graphics, pimpinan.Jabatan, 10, posisiTengah, yPos,
                // setengah, stringFormat, true);




                //yPos = yPos + 25;

                //yPos = oCetakPDF.TulisItem(graphics, pimpinan.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true);
                //yPos = oCetakPDF.TulisItem(graphics, "NIP " + pimpinan.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);

                
             
                //document.Pages. 
                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../AnggaranKas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../AnggaranKas.pdf");
                pV.Show();
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
            float posisiTengah = (previousPage.GetClientSize().Width / 2) + 10;
            PdfStringFormat stringFormat = new PdfStringFormat();


            CetakPDF oCetakPDF = new CetakPDF();


            if (SaatnyacetakKesimpulan == true)
            {
                Pejabat pimpinan = new Pejabat();
                DateTime hariini = DateTime.Now.Date;
                if (ctrlJabatan1.ID == 6)
                {
                    ctrlDinas1.KodeUk = ctrlDinas1.UnitAnggaran;
                    pimpinan = ctrlDinas1.GetPimpinan(hariini);
                }
                else
                {
                 
                    pimpinan = ctrlDinas1.GetPimpinan(hariini);
                }
                yPos = yPos + 10;
                stringFormat.Alignment = PdfTextAlignment.Center;
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, GlobalVar.gPemda.Ibukota + "," + dtCetak.Value.Date.ToTanggalIndonesia(), 8, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Jabatan, 8, posisiTengah, yPos, setengah, stringFormat, true, false, true);
                yPos = yPos + 30;
                 yPos = oCetakPDF.TulisItem(previousPage.Graphics, pimpinan.Nama, 8, posisiTengah, yPos, setengah, stringFormat, true, true, true);


                yPos = oCetakPDF.TulisItem(previousPage.Graphics, "NIP " + pimpinan.NIP, 8, posisiTengah, yPos, setengah, stringFormat, true);




            }

            previousPage = args.Page;
            
         
        }

        private void CetakAnggaranKasDinas(ParameterLaporan p, int iTahun, int pIDDInas, DateTime d)
        {


            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                //section1.PageSettings.Size = PdfPageSize.Ledger;
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 935;// = PdfPageSize.Legal;

                section1.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.Bottom = 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                SaatnyacetakKesimpulan = false;

                //PdfGrid headerGrid = new PdfGrid();

                //List<object> dataHeader = new List<object>();

                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                PdfGraphics graphics = page.Graphics;
                float kiri = 10;
                float postitikdua = 120;
                float posNama = 200;
                float lebarArena = page.GetClientSize().Width;
                yPos = 5;
                yPos = oCetakPDF.TulisItem(graphics, "PEMERINTAH KABUPATEN KETAPANG", 10, 10, yPos,
                    lebarArena, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(graphics, "ANGGARAN KAS", 10, 10, yPos,
                    lebarArena, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(graphics, "TAHUN ANGGARAN " +
                         GlobalVar.TahunAnggaran.ToString(), 10, 10, yPos,
                         lebarArena, stringFormat, true, false, true);

                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(graphics, "OPD"
                       , 10, kiri, yPos,
                        150, stringFormat, false, false, true);

                //float kiri = 10;
                //float postitikdua = 120;
                //float posNama = 150;

                yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDDInas.ToKodeDinas()
                        , 10, postitikdua, yPos,
                         150, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(graphics, ctrlDinas1.GetNamaSKPD(),
                        10, posNama, yPos,
                        lebarArena - posNama, stringFormat, true, false, true);

                
                yPos = yPos + 10;
                #region header
                PdfGrid pdfGridHeader = new PdfGrid();
                DataTable tableHeader = new DataTable();
                tableHeader.Columns.Add("Kode Rekening");
                tableHeader.Columns.Add("Uraian");
                tableHeader.Columns.Add("Anggaran");

                tableHeader.Columns.Add("Januari");
                tableHeader.Columns.Add("Februari");
                tableHeader.Columns.Add("Maret");
                tableHeader.Columns.Add("April");
                tableHeader.Columns.Add("Mei");
                tableHeader.Columns.Add("Juni");
                tableHeader.Columns.Add("Juli");
                tableHeader.Columns.Add("Agustus");
                tableHeader.Columns.Add("September");
                tableHeader.Columns.Add("Oktober");
                tableHeader.Columns.Add("November");
                tableHeader.Columns.Add("Desember");


                tableHeader.Rows.Add(new string[]
                    {            " Kode Rekening","Uraian", "Anggaran","Triwulan I","Triwulan I","Triwulan I",
                                "Triwulan II","Triwulan II","Triwulan II","Triwulan III",
                                "Triwulan III","Triwulan III","Triwulan IV","Triwulan IV","Triwulan IV"});

                tableHeader.Rows.Add(new string[]
                    {            " Kode Rekening","Uraian"," Anggaran","Januari","Februari","Maret",
                                "April","Mei","Juni","Juli",
                                "Agustus","September","Oktober","November","Desember"});

             

         

                pdfGridHeader.DataSource = tableHeader; //data
                pdfGridHeader.Columns[0].Width = 60;
                pdfGridHeader.Columns[1].Width = 100;

                // Angka 
                pdfGridHeader.Columns[2].Width = 60;
                pdfGridHeader.Columns[3].Width = 50;
                pdfGridHeader.Columns[4].Width = 51;
                pdfGridHeader.Columns[5].Width = 51;
                pdfGridHeader.Columns[6].Width = 51;
                pdfGridHeader.Columns[7].Width = 51;
                pdfGridHeader.Columns[8].Width = 51;
                pdfGridHeader.Columns[9].Width = 51;
                pdfGridHeader.Columns[10].Width = 52;
                pdfGridHeader.Columns[11].Width = 52;
                pdfGridHeader.Columns[12].Width = 52;
                pdfGridHeader.Columns[13].Width = 52;
                pdfGridHeader.Columns[14].Width = 52;


                pdfGridHeader.Rows[0].Cells[0].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[1].RowSpan = 2;
                pdfGridHeader.Rows[0].Cells[2].RowSpan = 2;
               
          
                pdfGridHeader.Rows[0].Cells[3].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[6].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[9].ColumnSpan = 3;
                pdfGridHeader.Rows[0].Cells[12].ColumnSpan = 3;


                PdfFont fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8));
                PdfGridCellStyle cellHeaderStyle0 = new PdfGridCellStyle();

                PdfStringFormat stringFormatHeader0 = new PdfStringFormat();
                stringFormatHeader0.Alignment = PdfTextAlignment.Center;
                stringFormatHeader0.LineAlignment = PdfVerticalAlignment.Middle;

                fontHeader = new PdfTrueTypeFont(new System.Drawing.Font("Arial", fontHeader.Size,
                     FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));
                cellHeaderStyle0.Font = fontHeader;
                cellHeaderStyle0.StringFormat = stringFormatHeader0;


                for (int col = 0; col < pdfGridHeader.Columns.Count; col++)
                    pdfGridHeader.Columns[col].Format = stringFormatHeader0;
                pdfGridHeader.Headers.Clear();
                PdfGridLayoutResult pdfHeaderGridResult = pdfGridHeader.Draw(page, new PointF(kiri, yPos));
                yPos = pdfHeaderGridResult.Bounds.Bottom;

                #endregion header


                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                table.Columns.Add("Anggaran");

                table.Columns.Add("Januari");
                table.Columns.Add("Februari");
                table.Columns.Add("Maret");
                table.Columns.Add("April");
                table.Columns.Add("Mei");
                table.Columns.Add("Juni");
                table.Columns.Add("Juli");
                table.Columns.Add("Agustus");
                table.Columns.Add("September");
                table.Columns.Add("Oktober");
                table.Columns.Add("November");
                table.Columns.Add("Desember");
       

                //table.Columns.Add("Jumlah ");

                decimal jumlahTW1 = 0L;
                decimal jumlahTW2 = 0L;
                decimal jumlahTW3 = 0L;
                decimal jumlahTW4 = 0L;

                decimal akumulasi = 0L;
                decimal sisa = 0;
                int rowtriwulan = 0;

                AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
                List<AnggaranKas> _lst = oLogic.GetAnggaranKas(p, iTahun, pIDDInas, p.Tahap);

                if (oLogic.IsError())
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }

                List<int> lstBarisTriwulan = new List<int>();
                List<int> lstBaris = new List<int>();
                List<int> lstBarisBold= new List<int>();

                int row=0;
                foreach (AnggaranKas ak in _lst )
                {

                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(ak.Kode),
                       ak.Nama,
                       ak.Anggaran.ToRupiahInReport(),                      
                       ak.Bulan1.ToRupiahInReport(),
                       ak.Bulan2.ToRupiahInReport(),
                       ak.Bulan3.ToRupiahInReport(),
                       ak.Bulan4.ToRupiahInReport(),
                       ak.Bulan5.ToRupiahInReport(),
                       ak.Bulan6.ToRupiahInReport(),
                       ak.Bulan7.ToRupiahInReport(),
                       ak.Bulan8.ToRupiahInReport(),
                       ak.Bulan9.ToRupiahInReport(),
                       ak.Bulan10.ToRupiahInReport(),
                        ak.Bulan11.ToRupiahInReport(),
                       
                       ak.Bulan12.ToRupiahInReport(),
                       
                       
                        
                    });
                    if (ak.Sifat == 2)
                    {
                        lstBarisTriwulan.Add(row);
                    }
                    if (ak.Sifat==3){
                        lstBaris.Add(row);
                    }
                    if (ak.Bold == 1)
                    {
                        lstBarisBold.Add(row);
                    }
                    row++;

                    

                }
              
                //Add list to IEnumerable.
                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 60;
                pdfGrid.Columns[1].Width = 100;

                // Angka 
                pdfGrid.Columns[2].Width = 60;
                pdfGrid.Columns[3].Width = 50;
                pdfGrid.Columns[4].Width = 51;
                pdfGrid.Columns[5].Width = 51;
                pdfGrid.Columns[6].Width = 51;
                pdfGrid.Columns[7].Width = 51;
                pdfGrid.Columns[8].Width = 51;
                pdfGrid.Columns[9].Width = 51;
                pdfGrid.Columns[10].Width = 52;
                pdfGrid.Columns[11].Width = 52;
                pdfGrid.Columns[12].Width = 52;
                pdfGrid.Columns[13].Width = 52;
                pdfGrid.Columns[14].Width = 52;
                //pdfGrid.Columns[15].Width =0;

                pdfGrid.Rows[rowtriwulan].Cells[3].ColumnSpan = 3;
                pdfGrid.Rows[rowtriwulan].Cells[6].ColumnSpan = 3;
                pdfGrid.Rows[rowtriwulan].Cells[9].ColumnSpan = 3;
                pdfGrid.Rows[rowtriwulan].Cells[12].ColumnSpan = 3;

                PdfGridStyle gridStyle = new PdfGridStyle();

                gridStyle.CellPadding = new PdfPaddings(3, 1, DataFormat.GetInteger(txtSpasi.Text), DataFormat.GetInteger(txtSpasi.Text));
                pdfGrid.Style = gridStyle;

                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[2].Format = formatKolomAngka;
                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;
                pdfGrid.Columns[7].Format = formatKolomAngka;
                pdfGrid.Columns[8].Format = formatKolomAngka;
                pdfGrid.Columns[9].Format = formatKolomAngka;
                pdfGrid.Columns[10].Format = formatKolomAngka;
                pdfGrid.Columns[11].Format = formatKolomAngka;
                pdfGrid.Columns[12].Format = formatKolomAngka;
                pdfGrid.Columns[13].Format = formatKolomAngka;
                pdfGrid.Columns[14].Format = formatKolomAngka;

                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();





                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();
                pdfGrid.RepeatHeader = true;

                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;


                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 8, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;

                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }
                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 7, FontStyle.Regular));
                cellStyle.Font = font;

                pdfGrid.Style.Font = font;// = cellStyle;
                pdfGrid.Style.CellPadding.Top = 1;
                cellStyle.Borders.All = new PdfPen(new PdfColor(192, 192, 217), 0.01f);
                for (int idx = 0; idx < pdfGrid.Rows.Count; idx++)
                {
                    pdfGrid.Rows[idx].Style = cellStyle;
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {


                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Bottom.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Top.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Left.Width = 0.1F;
                        pdfGrid.Rows[idx].Cells[c].Style.Borders.Right.Width = 0.1F;


                    }
                    
                    if (lstBarisTriwulan.Contains(idx)==true){
                        pdfGrid.Rows[idx].Cells[3].ColumnSpan=3;
                        pdfGrid.Rows[idx].Cells[6].ColumnSpan=3;
                        pdfGrid.Rows[idx].Cells[9].ColumnSpan=3;
                        pdfGrid.Rows[idx].Cells[12].ColumnSpan = 3;

                    }
                    if (lstBaris.Contains(idx) == true)
                    {
                        pdfGrid.Rows[idx].Cells[1].ColumnSpan = 14;
                        
                    }
                    //if (lstBarisBold.Contains(idx) == true)
                    //{
                    //    pdfGrid.Rows[idx].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 7, FontStyle.Bold));

                    //}
                    //else
                    //{
                    //  pdfGrid.Rows[idx].Style.Font =new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 6, FontStyle.Regular));
                    //}
                }

                pdfGrid.Headers.Clear();


                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(pdfHeaderGridResult.Page, new PointF(10, yPos));
               
                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();


                // yPos = pdfGridLayoutResult.Bounds.Bottom + 20;
                // Pejabat pimpinan = new Pejabat();
                //pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
                //yPos = oCetakPDF.TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtCetak.Value.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true);
                //yPos = oCetakPDF.TulisItem(graphics, pimpinan.Jabatan, 10, posisiTengah, yPos,
                // setengah, stringFormat, true);




                //yPos = yPos + 25;

                //yPos = oCetakPDF.TulisItem(graphics, pimpinan.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true);
                //yPos = oCetakPDF.TulisItem(graphics, "NIP " + pimpinan.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);



                //document.Pages. 
                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../AnggaranKas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../AnggaranKas.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CetakAnggaranKasTW()
        {
            try
            {
                //Create a new PDF document.
                PdfDocument document = new PdfDocument();
                PdfSection section1 = document.Sections.Add();
                section1.PageSettings.Width = 612;// = PdfPageSize.Legal;
                section1.PageSettings.Height = 935;// = PdfPageSize.Legal;
                section1.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.Bottom = 0;
                PdfPage page = section1.Pages.Add();
                previousPage = page;
                document.Pages.PageAdded += Pages_PageAdded;
                SaatnyacetakKesimpulan = false; ;
            
                CetakPDF oCetakPDF = new CetakPDF();
                float yPos;
                PdfStringFormat stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                PdfGraphics graphics = page.Graphics;
                float kiri = 10;
                float postitikdua = 120;
                float posNama = 200;
                float lebarArena = page.GetClientSize().Width;
                yPos = 5;
                yPos = oCetakPDF.TulisItem(graphics, "PEMERINTAH KABUPATEN KETAPANG", 12, 10, yPos,
                    lebarArena, stringFormat, true, false, true);
                yPos = oCetakPDF.TulisItem(graphics, "ANGGARAN KAS", 12, 10, yPos,
                    lebarArena, stringFormat, true, false, true);

                yPos = oCetakPDF.TulisItem(graphics, "TAHUN ANGGARAN " +
                         GlobalVar.TahunAnggaran.ToString(), 12, 10, yPos,
                         lebarArena, stringFormat, true, false, true);

                stringFormat.Alignment = PdfTextAlignment.Left;

                yPos = oCetakPDF.TulisItem(graphics, "OPD"
                       , 10, kiri, yPos,
                        150, stringFormat, false, false, true);

               

                yPos = oCetakPDF.TulisItem(graphics, ": " + m_IDDInas.ToKodeDinas()
                        , 10, postitikdua, yPos,
                         150, stringFormat, false, false, true);
                yPos = oCetakPDF.TulisItem(graphics, ctrlDinas1.GetNamaSKPD(),
                        10, posNama, yPos,
                        lebarArena - posNama, stringFormat, true, false, true);

                
                yPos = yPos + 10;
                PdfGrid pdfGrid = new PdfGrid();

                int count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Kode Rekening");
                table.Columns.Add("Uraian");
                if (m_iTahap <= 2)
                {
                    table.Columns.Add("Anggaran ");
                }
                if (m_iTahap == 3)
                {
                    table.Columns.Add("Anggaran Pergeseran");
                }

                if (m_iTahap == 4)
                {
                    table.Columns.Add("Anggaran Perubahan");
                }
                if (m_iTahap == 5)
                {
                    table.Columns.Add("Anggaran Pergeseran Perubahan");
                }


                table.Columns.Add("Triwulan 1");
                table.Columns.Add("Triwulan 2");
                table.Columns.Add("Triwulan 3");
                table.Columns.Add("Triwulan 4");
                
              
                decimal sisa = 0;
                int rowtriwulan = 0;
                AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
                ParameterLaporan p = new ParameterLaporan();
                p.JenisAnggaran = 0;
                p.IDDinas = m_IDDInas;

                List<AnggaranKasTW> lst = oLogic.GetAnggaranKasTW(p,GlobalVar.TahunAnggaran, m_IDDInas, m_iTahap);

                if (oLogic.IsError())
                {
                    MessageBox.Show(oLogic.LastError());
                    return;
                }

                foreach (AnggaranKasTW atw in lst)
                {

                    table.Rows.Add(new string[]
                    {

                       DataFormat.GetString(atw.Kode),
                       DataFormat.GetString(atw.Nama),                      
                       
                       DataFormat.GetString(atw.Anggaran),
                       DataFormat.GetString(atw.Tw1),
                       DataFormat.GetString(atw.Tw2),
                       DataFormat.GetString(atw.Tw3),
                       DataFormat.GetString(atw.Tw4),

                        
                    });
                    rowtriwulan++;

                }
               

                //Add list to IEnumerable.
                pdfGrid.DataSource = table; //data
                pdfGrid.Columns[0].Width = 80;
                pdfGrid.Columns[1].Width = 100;

                // Angka 
                pdfGrid.Columns[2].Width = 70;
                pdfGrid.Columns[3].Width = 65;
                pdfGrid.Columns[4].Width = 65;
                pdfGrid.Columns[5].Width = 65;
                pdfGrid.Columns[6].Width = 65;
         
                
                PdfGridStyle gridStyle = new PdfGridStyle();
                //Adding cell padding
                gridStyle.CellPadding = new PdfPaddings(1, 1, 1, 1);

                pdfGrid.Style = gridStyle;


                PdfStringFormat formatKolomAngka = new PdfStringFormat();
                formatKolomAngka.Alignment = PdfTextAlignment.Right;

                pdfGrid.Columns[2].Format = formatKolomAngka;
                pdfGrid.Columns[3].Format = formatKolomAngka;
                pdfGrid.Columns[4].Format = formatKolomAngka;
                pdfGrid.Columns[5].Format = formatKolomAngka;
                pdfGrid.Columns[6].Format = formatKolomAngka;
                
                PdfFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                PdfGridCellStyle cellHeaderStyle = new PdfGridCellStyle();
                pdfGrid.RepeatHeader = true;

                PdfStringFormat stringFormatHeader = new PdfStringFormat();
                stringFormatHeader.Alignment = PdfTextAlignment.Center;
                stringFormatHeader.LineAlignment = PdfVerticalAlignment.Middle;


                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 8, FontStyle.Bold)); // font10;// new PdfStandardFont(PdfFontFamily.TimesRoman, 8f); PdfTrueTypeFont(new System.Drawing.Font("Arial", 10));

                cellHeaderStyle.Font = font;

                cellHeaderStyle.StringFormat = stringFormatHeader;

                for (int c = 0; c < pdfGrid.Headers.Count; c++)
                {
                    pdfGrid.Headers[c].ApplyStyle(cellHeaderStyle);
                    pdfGrid.Headers[c].Height = 30;

                }
                font = new PdfTrueTypeFont(new System.Drawing.Font("Arial Narrow", 7, FontStyle.Regular));
                cellStyle.Font = font;
                pdfGrid.Style.Font = font;// = cellStyle;
                pdfGrid.Style.CellPadding.Top = 1;

                for (int i = 0; i < pdfGrid.Rows.Count; i++)
                {
                    for (int c = 0; c < pdfGrid.Columns.Count; c++)
                    {

                        //pdfGrid.Rows[i].Cells[c].ApplyStyle(cellHeaderStyle);
                        //pdfGrid.Headers[c].Height = 50;

                    }
                }


                //Draw grid on the page of PDF document and store the grid position in PdfGridLayoutResult.
                stringFormat.Alignment = PdfTextAlignment.Center;
                float posisiTengah = lebarArena / 2;
                float setengah = lebarArena / 2;
                PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10, yPos));

                PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
                SaatnyacetakKesimpulan = true;
                page = document.Pages.Add();

                //yPos = pdfGridLayoutResult.Bounds.Bottom + 20;
                //Pejabat pimpinan = new Pejabat();
                //pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
                //yPos = oCetakPDF.TulisItem(graphics, GlobalVar.gPemda.Ibukota + ", " + dtCetak.Value.ToTanggalIndonesia(), 10, posisiTengah, yPos, setengah, stringFormat, true);
                //yPos = oCetakPDF.TulisItem(graphics, pimpinan.Jabatan, 10, posisiTengah, yPos,
                // setengah, stringFormat, true);




                //yPos = yPos + 25;

                //yPos = oCetakPDF.TulisItem(graphics, pimpinan.Nama, 10, posisiTengah, yPos, setengah, stringFormat, true, true);
                //yPos = oCetakPDF.TulisItem(graphics, "NIP " + pimpinan.NIP, 10, posisiTengah, yPos, setengah, stringFormat, true);



                //document.Pages. 
                using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../AnggaranKas.pdf"), FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the PDF document to file stream.
                    document.Save(outputFileStream);

                }

                //Close the document.
                document.Close(true);
                pdfViewer pV = new pdfViewer();
                pV.Document = Path.GetFullPath(@"../../../AnggaranKas.pdf");
                pV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private void cmdCetak_Click(object sender, EventArgs e)
        {
            CetakAnggaranKas();
          
        }

        private void ctrlJenisAnggaran1_OnChanged(int pID)
        {
            int _idDinas = ctrlDinas1.GetIDSKPD();
            int _id = ctrlJenisAnggaran1.GetID();
            m_IDKegiatan = 0;
            m_IDUrusan = ctrlDinas1.IDUrusan();
            m_IDSubKegiatan = 0;
            m_IDProgram = 0;
            if (_idDinas < 10)
            {
                MessageBox.Show("Silakan Pilih Dinas Terlebih Dahulu..");
                return;
            }
            switch (_id)
            {
                case 1:
                    splitContainer1.Panel1Collapsed = true;
                    splitContainer1.Panel1.Hide();

                    LoadData();
                    lblUrusan.Text = "Pendapatan";
                    lblProgram.Text = "";
                    lblKegiatan.Text = "";
                    lblSubKegiatan.Text = "";
                    break;
                case 2:
                    
                    LoadData();
                    break;

                case 3:
                    splitContainer1.Panel1Collapsed = false ;
                    splitContainer1.Panel1.Show();


                    lblUrusan.Text = "Urusan Pemerintahan";
                    lblProgram.Text = "Program ";
                    lblKegiatan.Text = "Kegiatan";
                    lblSubKegiatan.Text = "Sub Kegiatan";

                    //if (ctrlDinas1.WithUnitKerja() == false)
                    //{
                    //    treeProgramKegiatan1.Create(m_IDDInas, 2);
                    //}
                    //else
                    //{
                    //    treeProgramKegiatan1.Create(m_IDDInas, 2, m_IDUit,m_iKodeuk);
                    //}
                    break;
                case 4:
                    splitContainer1.Panel1Collapsed = true;
                    splitContainer1.Panel1.Hide();

                    lblUrusan.Text = "Penerimaan Pembiayaan ";
                    lblProgram.Text = "";
                    lblKegiatan.Text = "";
                    lblSubKegiatan.Text = "";
                    LoadData();
                    break;
                case 5:
                    splitContainer1.Panel1Collapsed = true;
                    splitContainer1.Panel1.Hide();


                    lblUrusan.Text = "Pengeluaran Pembiayaan ";
                    lblProgram.Text = "P";
                    lblKegiatan.Text = "";
                    lblSubKegiatan.Text = "";
                    LoadData();
                    break;
            }

        }

        private EventResponseMessage treeProgramKegiatan1_KegiatanChanged(int ID)
        {

            gridAnggaranKas.Rows.Clear();
            lblUrusan.Text = "";
            lblProgram.Text = "";
            lblKegiatan.Text = "";
            lblSubKegiatan.Text = ""; 
            EventResponseMessage lRet = new EventResponseMessage();
            if (GlobalVar.TahunAnggaran <= 2020)
            {
             

                if (m_IDKegiatan > 0)
                {
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
                m_IDSubKegiatan = 0;
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
                TProgramLogic oPLogic = new TProgramLogic(GlobalVar.TahunAnggaran, mProfile);
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
                    TKegiatanAPBDLogic oKLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                    TKegiatanAPBD oKegiatan = new TKegiatanAPBD();
                    oKegiatan = oKLogic.GetKegiatan((int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, (int)ctrlJenisAnggaran1.GetID(), 1);
                    if (oKegiatan == null)
                    {
                        MessageBox.Show(oKLogic.LastError());
                        lRet.ResponseStatus = false;
                        return lRet;

                    }
                    lblKegiatan.Text = oKegiatan.KodeKegiatan.ToString() + " " + oKegiatan.Nama;
                    //    lblPagu.Text = oKegiatan.Pagu.FormatUang();
                    //TKegiatanAPBDLogic oKegiatanAPBDLogic = new TKegiatanAPBDLogic();
                    //oKegiatanAPBDLogic.CekNAmaKegiatan(oKegiatan);

                    LoadData();

                }
            }
            return lRet;
        }

        private void ctrlJenisAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void treeProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }

        private void gridAnggaranKas_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.C:
                            
                          //CopyToClipboard();
                
                           break;

                        case Keys.V:
                            
                            PasteClipboard();                           

                            
                            break;
                    }
                }
                
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (DataGridViewCell cell in gridAnggaranKas.SelectedCells)
                    {

                        int rowIndex = cell.RowIndex;
                        int colIndex = cell.ColumnIndex;
                        if (colIndex >=COL_JANUARI )
                        {
                            gridAnggaranKas.Rows[rowIndex].Cells[colIndex].Value = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Copy/paste operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void PasteClipboard()
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');

                int iRow = gridAnggaranKas.CurrentCell.RowIndex;
                int iCol = gridAnggaranKas.CurrentCell.ColumnIndex;
                // _barisKUA--;
                DataGridViewCell oCell;

                
                //int _added = 0;
                foreach (string line in lines)
                {
                    if (iRow < gridAnggaranKas.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i < this.gridAnggaranKas.ColumnCount)
                            {
                                oCell = gridAnggaranKas[iCol + i, iRow];
                                oCell.Value = Convert.ChangeType(sCells[i].Replace("\r", ""), oCell.ValueType);                               
                                
                            }
                            else
                            {
                                break;
                            }
                            
                        }
                        HitungHOrizontal(iRow);
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
        private void CetakDinas()
        {

            ParameterLaporan p = new ParameterLaporan();
            p.JenisAnggaran = 0;
            p.IDDinas = ctrlDinas1.GetID();
            p.IDProgram = m_IDProgram;
            p.IDKegiatan = m_IDKegiatan;
            p.IDSubKegiatan = m_IDSubKegiatan;
            p.IDDinas = ctrlDinas1.GetID();
            p.IDUrusan = m_IDUrusan;
            p.dTanggal = dtCetak.Value.Date;
            p.pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
            p.Bendahara = ctrlDinas1.GetBendaharaPengeluaran(dtCetak.Value);
            p.skpd = ctrlDinas1.GetSKPD();
            //p.NamaUrusan =

            //p.bKegiatanKhusus = true;

            p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            p.Tahap = m_iTahap;


            int iDinas = ctrlDinas1.GetID();
            //frmReportViewer f = new frmReportViewer();

            //if (chkGabungan.Checked == true)
            //{
            //    SKPDLogic oLogic = new SKPDLogic((int)GlobalVar.TahunAnggaran);
            //    List<SKPD> lstSKPD = oLogic.GetByParent(iDinas);
            //    f.AnggaranKasTW(p, GlobalVar.TahunAnggaran, m_IDDInas, dtCetak.Value.Date, lstSKPD);
            //}
            //else
            //{
            //    f.AnggaranKasTW(p, GlobalVar.TahunAnggaran, m_IDDInas, dtCetak.Value.Date);

            //}




            //f.Show();
        }
        private void cmdCetakSatuDinas_Click(object sender, EventArgs e)
        {
            
            //ParameterLaporan p = new ParameterLaporan();
            //p.JenisAnggaran = 0;
            //p.IDDinas = ctrlDinas1.GetID();
            //p.IDProgram = m_IDProgram;
            //p.IDKegiatan = m_IDKegiatan;
            //p.IDSubKegiatan = m_IDSubKegiatan;
            //p.IDDinas = ctrlDinas1.GetID();
            //p.IDUrusan = m_IDUrusan;
            //p.dTanggal = dtCetak.Value.Date;
            ////p.NamaUrusan =

            ////p.bKegiatanKhusus = true;

            //p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            //p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            //p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            //p.Tahap = m_iTahap;
        

            //int iDinas = ctrlDinas1.GetID();
            //frmReportViewer f = new frmReportViewer();
            //if (chkGabungan.Checked == true)
            //{
            //    SKPDLogic oLogic = new SKPDLogic((int)GlobalVar.TahunAnggaran);
            //    List<SKPD> lstSKPD = oLogic.GetByParent(iDinas);
            //    f.AnggaranKasTW(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), dtCetak.Value.Date, lstSKPD);
            //}
            //else
            //{
            //    f.AnggaranKasTW(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), dtCetak.Value.Date);

            //}


            

            //f.Show();

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            //TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            //TahapanAnggaran ta = new TahapanAnggaran();
            m_IDDInas=pIDSKPD;
            m_IDUit = pIDSKPD+pIDUK;
            m_iUnitAnggaran = ctrlDinas1.UnitAnggaran;
            m_iKodeuk = pIDUK;
          //  m_iUnitAnggaran = ctrlDinas1.UK;
            gridAnggaranKas.Rows.Clear();


            lblUrusan.Text = "";
            lblProgram.Text = "";
            lblKegiatan.Text = "";
            lblSubKegiatan.Text = "";
            int ta = ctrlDinas1.GetTahapAnggaran();
            if (ctrlDinas1.WithUnitKerja() == false && pIDUK <1)
            {
                treeProgramKegiatan1.Create(pIDSKPD, ta);
            }
            else
            {
                treeProgramKegiatan1.Create(pIDSKPD, ta, m_IDUit, pIDUK);
            }


            m_iTahap = ctrlDinas1.GetTahapAnggaran();
            //ta = oLogic.GetByDinas(m_IDDInas, (int)GlobalVar.TahunAnggaran);
            if (ta == 2)
            {
                cmdSimpanAK.Enabled = false;
            }
            else
            {
                cmdSimpanAK.Enabled = true;
            }


        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdCetakAnggaranKasSemua_Click(object sender, EventArgs e)
        {

            if(ctrlJabatan1.ID==0){
                MessageBox.Show("Penanda Tangan masih belum dipilih");
                return;
            }
            ParameterLaporan p = new ParameterLaporan();
            p.JenisAnggaran = 0;
            p.IDDinas = ctrlDinas1.GetID();

            p.IDProgram = 0; //m_IDProgram;
            p.IDKegiatan = 0;// m_IDKegiatan;
            p.IDDinas = m_IDDInas;
            p.IDUrusan = 0; //m_IDUrusan;
            p.bKegiatanKhusus = true;
            p.dTanggal = dtCetak.Value.Date;
            p.Tahap = ctrlDinas1.GetTahapAnggaran();
            p.pimpinan = ctrlDinas1.GetPimpinan(dtCetak.Value);
            p.Bendahara = ctrlDinas1.GetBendaharaPengeluaran(dtCetak.Value);
            p.skpd = ctrlDinas1.GetSKPD();
            p.IDunit = 0;




       
              CetakAnggaranKasDinas(p, GlobalVar.TahunAnggaran, m_IDDInas, dtCetak.Value);

       
        }

        private EventResponseMessage treeProgramKegiatan1_SubKegiatanChanged(long ID)
        {
            EventResponseMessage lRet = new EventResponseMessage();
            if (GlobalVar.TahunAnggaran <= 2020)
            {
                lRet.ResponseStatus=false;
          
                return lRet;
            }
                //if (m_IDKegiatan > 0)
               
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
                m_IDSubKegiatan = ID;
                if (ID != 0)
                {
                    m_IDKegiatan = DataFormat.GetInteger(m_IDSubKegiatan.ToString().Substring(0, 8));

                    m_IDUrusan = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 3));
                    m_IDProgram = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 5));
                    lblSubKegiatan.Text = "";

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
                    TProgramLogic oPLogic = new TProgramLogic(GlobalVar.TahunAnggaran, mProfile);
                    NamaUrusan=oUrusan.Nama;
                    //TPrograms oProgram = new TPrograms();
                    TProgramAPBD oProgram =new TProgramAPBD();
                    oProgram = GlobalVar.gListProgram.FirstOrDefault(x => x.Tahun == GlobalVar.TahunAnggaran && 
                                           x.IDDinas == m_IDDInas && 
                                           x.IDProgram == m_IDProgram);


                    //List<TProgramAPBD> lst = GlobalVar.gListProgram.FindAll(x => x.Tahun == GlobalVar.TahunAnggaran);
                    //if (oProgram==null)
                    //{

                    
                    ////if (oPLogic.CekProgramDinas(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram))
                    ////{
                    //    oProgram = oPLogic.GetByID (ctrlDinas1.GetID(), m_IDProgram);

                        if (oProgram == null)
                        {
                            MessageBox.Show(oPLogic.LastError());
                            lRet.ResponseStatus = false;
                            return lRet;

                        }
                        lblProgram.Text = "";
                        
                        lblProgram.Text = oProgram.KodeProgram.ToString() + " " + oProgram.Nama;
                        NamaProgram=oProgram.Nama;
                      
                        
                      TKegiatanAPBDLogic oKLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                        
                      TKegiatanAPBD oKegiatan = new TKegiatanAPBD();

                      oKegiatan = GlobalVar.gListKegiatan.FirstOrDefault(x=>x.IDDinas==m_IDDInas && 
                                                                       //   x.KodeUK==  m_iUnitAnggaran && 
                                                                            x.IDKegiatan== m_IDKegiatan);
                    
                       if (oKegiatan == null)
                        {
                            MessageBox.Show(oKLogic.LastError());
                            lRet.ResponseStatus = false;
                            return lRet;

                        }
                       string sKodeKegiatan = oKegiatan.IDKegiatan.ToKodeKegiatan();
                       lblKegiatan.Text = sKodeKegiatan.Substring(sKodeKegiatan.Length-3) + " " + oKegiatan.Nama;
                        NamaKegiatan=oKegiatan.Nama;        

                        m_oSubKegiatan = new TSubKegiatan();
                     //   TSubKegiatan m_oSubKegiatan = new TSubKegiatan();
                        TSubKegiatanLogic oSKLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, mProfile);


                        m_oSubKegiatan = oSKLogic.GetSubKegiatan((int)GlobalVar.TahunAnggaran, m_IDDInas, m_iUnitAnggaran, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan);
                      
                        if (m_oSubKegiatan != null)
                        {
                            string ssubkeg= m_IDSubKegiatan.ToString();
                            lblSubKegiatan.Text = ssubkeg.Substring(ssubkeg.Length - 2) + m_oSubKegiatan.Nama;
                            NamaSubKegiatan = m_oSubKegiatan.Nama;

                        }
                        else
                        {
                            NamaSubKegiatan = ""; 
                        }
                                           
         
                        LoadData();

                    //}
                }
            return lRet;
        }

        private void cmdCetakDinas_Click(object sender, EventArgs e)
        {
            CetakAnggaranKasTW();
          //  CetakDinas();

            //CetakAnggaranKas();


        }

        private void cmdCekRAK_Click(object sender, EventArgs e)
        {
            TAnggaranRekeningLogic oAnggaranLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            AnggaranKasLogic oAnggaranKasLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> lstAngaran= oAnggaranLogic.GetBySubKegiatan(GlobalVar.TahunAnggaran,m_IDDInas,m_IDSubKegiatan);

            int jumlahDataAggaran = lstAngaran.Count();

            List<AnggaranKas> lstAngaranKas = oAnggaranKasLogic.GetAnggaranKasByIDSubKegiatan(GlobalVar.TahunAnggaran, m_IDDInas, m_iTahap, m_IDSubKegiatan);
            int jumlahDataAggaranKas = lstAngaranKas.Count();
            if (jumlahDataAggaran != jumlahDataAggaranKas)
            {
                if (oAnggaranKasLogic.HilangkanDoble(m_IDDInas,m_IDSubKegiatan) == false)
                {
                    MessageBox.Show(oAnggaranKasLogic.LastError());
                    return;
                } 
                
            }
            MessageBox.Show("OK");


        }

        private void cmdSiapkanAK_Click(object sender, EventArgs e)
        {

            //AnggaranKasLogic oLogic = new AnggaranKasLogic(GlobalVar.TahunAnggaran);
            //if (oLogic.PersiapkanAKPerubahan(m_iTahap - 1, m_iTahap) == true)
            //{
            //    MessageBox.Show("Anggaran Kas Perubahan sudah disiapkan.... ");

            //}
            //else
            //{
            //    MessageBox.Show(oLogic.LastError());
            //}
        }

        private void treeProgramKegiatan1_Load_1(object sender, EventArgs e)
        {

        }

        private void treeProgramKegiatan1_Load_2(object sender, EventArgs e)
        {

        }

        //private EventResponseMessage treeProgramKegiatan1_KegiatanChanged_1(int ID)
        //{
        //    return default(EventResponseMessage);
        //}

        private EventResponseMessage treeProgramKegiatan1_ProgramChanged(int ID)
        {
            EventResponseMessage ret = new EventResponseMessage();
            

            gridAnggaranKas.Rows.Clear();
            lblUrusan.Text = "";
            lblProgram.Text = "";
            lblKegiatan.Text = "";
            lblSubKegiatan.Text = "";
            return ret;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ctrlHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}
