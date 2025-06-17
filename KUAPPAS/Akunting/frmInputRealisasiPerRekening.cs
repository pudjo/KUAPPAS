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
using DataAccess;
using Formatting;

namespace KUAPPAS
{
    public partial class frmInputRealisasiPerRekening : Form 
    {

        private int m_IDDinas;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_iTahun;
        private int m_iKodeUK;
        private int m_iUnit;

        private int m_IDKegiatan;
        private long m_IDSubKegiatan;
        private int m_IDJenis;
        private int _tahap;
  
        const int MODE_PLAFON = 1;
        const int MODE_REALISASI =2;

        public frmInputRealisasiPerRekening()
        {
            InitializeComponent();
           
            m_IDSubKegiatan = 0;
   
            m_iTahun = GlobalVar.TahunAnggaran;
            m_iKodeUK = 0;
            _tahap=7;
            m_iUnit = 0;

        }
        public int Tahap
        {
            set
            {
                _tahap = value;
            }
            get
            {
                return _tahap;
            }
        }

        private void frmInputRealisasiPerRekening_Load(object sender, EventArgs e)
        {
            
                    ctrlHeader1.SetCaption("Input Realisasi Per Rekening..");
                    
                      
           

          TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
          TahapanAnggaran m_TA = new TahapanAnggaran();

           

           

            ctrlDinas1.Create();
            gridSumberDana.FormatHeader();
       

            ctrlJenisAnggaran1.Create(1);
        }
       
        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

       

        private void ctrlJenisAnggaran1_OnChanged(int pID)
        {
            m_IDJenis = pID;
            if (m_IDJenis != 3)
            {
                treeProgramKegiatan1.Enabled = false;
            }
            else
            {
                treeProgramKegiatan1.Enabled = true;
            }

            LoadAnggaran();
        }
        private bool LoadAnggaran()
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,3);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            int _iJenis = ctrlJenisAnggaran1.GetID();
            m_IDJenis = _iJenis;



            if (_iJenis  != 3)
            {

                m_IDKegiatan = 0;
                m_IDProgram = 0;

                m_IDSubKegiatan = 0;
                
                m_IDUrusan = DataFormat.GetInteger(m_IDDinas.ToString().Substring(0, 3));
            }

            if (m_IDUrusan == 0 && _iJenis == 3)
                return false;

            int _bPPKD = 0;
            //m_IDDinas = pIDSKPD;
           // m_iKodeUK = ctrlDinas1.UnitAnggaran;

            lstRekening = oLogic.GetANggaranDanRealisasi(GlobalVar.TahunAnggaran,
                 m_IDDinas, m_iKodeUK,m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan, _iJenis);
            
                

            gridSumberDana.Rows.Clear();
            foreach (TAnggaranRekening ta in lstRekening)
             {
                    string[] rowUtkSumberDana = { ta.IDRekening.ToString(), 
                                                    ta.IDRekening.ToString().ToKodeRekening(), 
                                                    ta.Nama, 
                                                    ta.JumlahABT.ToRupiahInReport(), 
                                                    ta.Realisasi.ToRupiahInReport() };
                    gridSumberDana.Rows.Add(rowUtkSumberDana);
             }
             
            refreshJumah();
            return true;
        }

        private EventResponseMessage treeProgramKegiatan1_KegiatanChanged(int ID)
        {
            EventResponseMessage lRet = new EventResponseMessage();
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
            if (m_IDKegiatan == 0)
                return lRet;

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
                oProgram = oPLogic.GetByID(ctrlDinas1.GetID(), m_IDProgram);
                if (oProgram == null)
                {
                    MessageBox.Show(oPLogic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }
                lblProgram.Text = "";
                if (oProgram != null)
                    lblProgram.Text = oProgram.KodeProgram.ToString().Trim() + " " + oProgram.Nama;
                TKegiatanLogic oKLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran,3);
                TKegiatan oKegiatan = new TKegiatan();
                oKegiatan = oKLogic.GetByID(ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan);
                if (oKegiatan == null)
                {
                    MessageBox.Show(oKLogic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }
                lblKegiatan.Text = oKegiatan.KodeKegiatan.ToString() + " " + oKegiatan.Nama;
                //lblPagu.Text = oKegiatan.Pagu.FormatUang();
                TKegiatanAPBDLogic oKegiatanAPBDLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                oKegiatanAPBDLogic.CekNAmaKegiatan(oKegiatan);
                gridSumberDana.Rows.Clear();

               
            }
            return lRet;
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,3);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();


            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran,3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            for (int i = 0; i < gridSumberDana.Rows.Count; i++)
            {
                
                TAnggaranRekening o = new TAnggaranRekening();
                o.Tahun = GlobalVar.TahunAnggaran;
                o.Jenis = m_IDJenis;
                o.IDDinas = m_IDDinas;
                o.IDUrusan = m_IDUrusan;
                o.IDProgram = m_IDProgram;
                o.IDKegiatan = m_IDKegiatan;
                o.IDSubKegiatan = m_IDSubKegiatan;
                o.IDUnit = m_iUnit;
                o.IDRekening = DataFormat.GetLong(gridSumberDana.Rows[i].Cells[0].Value);
                o.PPKD = 0;// ctrlDinas1.PPKD();
                o.StatusUpdate = 1;
                _tahap = 3;

                o.Tahap = _tahap;


               
                o.Realisasi= DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
               o.KodeUK = ctrlDinas1.UnitAnggaran;
                //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                o.IDSubKegiatan = m_IDSubKegiatan;
                if (o.IDRekening> 0)
                    _lstRek.Add(o);

            }
            
            oLogicRek.SimpanRealisasi (_lstRek, (int)GlobalVar.TahunAnggaran, 
                m_IDDinas, 
                m_iKodeUK,
                m_IDUrusan, 
                m_IDProgram, 
                m_IDKegiatan, 
                m_IDSubKegiatan,
                m_IDJenis);

            MessageBox.Show("Penyimpanan Selesai");
           



        

        }

        private void ctrlJenisAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage treeProgramKegiatan1_SubKegiatanChanged(long ID)
        {

            EventResponseMessage resp = new EventResponseMessage();
            resp.ResponseStatus = true;
            gridSumberDana.Rows.Clear();
            EventResponseMessage lRet = new EventResponseMessage();
            lRet.ResponseStatus = true;

            if (GlobalVar.PP90 == false)
                return lRet;

            m_IDSubKegiatan = ID;
            if (ID == 0)
                return lRet;
             m_IDKegiatan = DataFormat.GetInteger(ID.ToString().Substring(0, 8));

            //if (m_IDKegiatan != m_IDSubKegiatan / 100)
            //{
            //    //  MessageBox.Show("IDKegiatan tidak Konsisten betul..");
            //    m_IDKegiatan = (int)(m_IDSubKegiatan / 10000);
            //}
            m_IDProgram = DataFormat.GetInteger(ID.ToString().Substring(0, 5));

            //if (m_IDProgram != m_IDSubKegiatan / 100000)
            //{
            //    //    MessageBox.Show("IDProgram tidak Konsisten betul..");
            //    m_IDProgram = (int)(m_IDSubKegiatan / 100000);
            //}
            m_IDUrusan = DataFormat.GetInteger(ID.ToString().Substring(0, 3));


            //if (m_IDUrusan != m_IDSubKegiatan / 10000000)
            //{
            //    //    MessageBox.Show("IDKegiatan tidak Konsisten betul..");
            //    m_IDUrusan = (int)(m_IDSubKegiatan / 10000000);
            //}
            ShowSubKegiatan();
            LoadAnggaran();
            return resp;

            
        }

        private void gridSumberDana_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridSumberDana_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
               cmdSimpan.Enabled = true;
               if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
               {
                        refreshJumah();
               }
                
            
        }
        private void refreshJumah(){
            decimal jumlah = 0L;
            decimal jumlahPerubahan =0;

           
                for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                {
                    if (gridSumberDana.Rows[i].Cells[3].Value != null)
                    {
                        decimal d = gridSumberDana.Rows[i].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                        jumlah = jumlah + d;
                    }
                }

           
                txtJumlahMurni.Text = jumlah.ToRupiahInReport();
         
           
                for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                {
                    if (gridSumberDana.Rows[i].Cells[4].Value != null)
                    {
                        decimal d = gridSumberDana.Rows[i].Cells[4].Value.ToString().FormatUangReportKeDecimal();
                        jumlahPerubahan = jumlahPerubahan + d;
                    }
                }




                txtJumlahPergeseran.Text = jumlahPerubahan.ToRupiahInReport();
               
               
       

        
        }

        private void treeProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }
        private void ShowSubKegiatan()
        {

          TProgramAPBD m_oProgram;
          TKegiatanAPBD m_oKegiatan;
          TSubKegiatan m_oSubKegiatan;

          TSubKegiatanLogic oKAPBDLOgic = new TSubKegiatanLogic(m_iTahun, 3);
          
 
            m_oSubKegiatan = new TSubKegiatan();
          int kodeuk = ctrlDinas1.UnitAnggaran;
          m_oSubKegiatan = oKAPBDLOgic.GetSubKegiatan(m_iTahun, m_IDDinas, kodeuk, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan);
                    

          if (m_oSubKegiatan == null)
          {
              MessageBox.Show("Sub Kegiatan tidak ditemukan");
              return;
          }
            //DisplaySubKegiatan(m_oSubKegiatan);
            string sKode = m_oSubKegiatan.IDSubKegiatan.ToString();
            lblSubKegiatan.Text = sKode.Substring(sKode.Length - 2);//" " + m_oSubKegiatan.Nama ;//lblKodeKegiatan.Text = oKeg.TampilanKode;
            lbllNamaSubKegiatan.Text = m_oSubKegiatan.Nama;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CetakDPA22(_tahap);
        }
        private void CetakDPA22(int pTahap, List<SKPD> lstSKPD = null)
        {
            
        }
       
        private void cmdCetakDPASKPD_Click(object sender, EventArgs e)
        {
        }

       

        private void ctrlDinas1_Load_1(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDinas = pIDSKPD;
            m_iKodeUK = ctrlDinas1.UnitAnggaran;

            m_iUnit =  pIDUK;

            if (ctrlDinas1.WithUnitKerja() == false)
            {
                treeProgramKegiatan1.Create(m_IDDinas, 1);
            }
            else
            {
                treeProgramKegiatan1.Create(m_IDDinas, 1, m_iUnit);
            }
            treeProgramKegiatan1.Profile = 3;
            TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            TahapanAnggaran m_TA = new TahapanAnggaran();

            m_TA = oTALogic.GetByDinas(m_IDDinas, m_iTahun);

            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            


            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,3);
            //List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();
            //TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran,3);

            //List<TSumberDana> _lsd = new List<TSumberDana>();
                
            // TAnggaranRekening o = new TAnggaranRekening();
            // o.Tahun = GlobalVar.TahunAnggaran;
            // o.Jenis = 3;
            // o.IDDinas = m_IDDinas;
            // o.IDUrusan = m_IDUrusan;
            // o.IDProgram = 10202;

            // o.IDKegiatan = DataFormat.GetInteger(txtIDSUBKegiatan.Text.Replace(".","").Replace(" ","").Substring(0,8));
            // o.IDSubKegiatan = DataFormat.GetLong(txtIDSUBKegiatan.Text.Replace(".", "").Replace(" ", "").Trim());
            // o.IDUnit = m_iUnit;
            // o.IDRekening = DataFormat.GetLong (txtidrekening.Text.Replace(".",""));

            //    o.PPKD = 0;// ctrlDinas1.PPKD();
            //    o.StatusUpdate = 1;
            //    o.Tahap = 3;


            //    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
            //    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
            //        o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);

            //        o.JumlahABT = DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);

            
            //    //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
            //   // o.IDSubKegiatan = m_IDSubKegiatan;

               oLogicRek.Perbaikandinkes();
            //oLogicRek.PerbaikiKesehatan();
            MessageBox.Show("Penyimpanan Selesai");
           



        

        }

        private void cmdTambahRekening_Click(object sender, EventArgs e)
        {
            frmInputKodeRekening fInputRek = new frmInputKodeRekening();
            fInputRek.ShowDialog();
            if (fInputRek.IsOK)
            {
                long idRekening = fInputRek.IDrekening;
                TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,3);
                List<TAnggaranRekening> lstRek = new List<TAnggaranRekening>();

                
                TAnggaranRekening o = new TAnggaranRekening();
                o.Tahun = GlobalVar.TahunAnggaran;
                o.Jenis = m_IDJenis;
                o.IDDinas = m_IDDinas;
                o.IDUrusan = m_IDUrusan;
                o.IDProgram = m_IDProgram;
                o.IDKegiatan = m_IDKegiatan;
                o.IDSubKegiatan = m_IDSubKegiatan;
                o.IDUnit = m_iUnit;
                o.IDRekening = idRekening;
                o.PPKD = 0;// ctrlDinas1.PPKD();
                o.StatusUpdate = 1;
                _tahap = 3;

                o.Tahap = _tahap;
                o.Plafon = 0;
                o.JumlahMurni = 0;
                o.JumlahPergeseran = 0;
                o.JumlahRKAP = 0;
                o.JumlahABT = 0;
                o.IDSubKegiatan = m_IDSubKegiatan;
                lstRek.Add(o);

             

                int kodeuk = ctrlDinas1.UnitAnggaran;
                oLogicRek.SimpanPlafon(lstRek, (int)GlobalVar.TahunAnggaran, m_IDDinas, kodeuk, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDJenis, _tahap);
                LoadAnggaran();
            }
        }

    }
}
