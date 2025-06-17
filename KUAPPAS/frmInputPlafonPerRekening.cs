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
using KUAPPAS.Bendahara;

namespace KUAPPAS
{
    public partial class frmInputPlafonPerRekening : ChildForm
    {

        private int m_IDDinas;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_iTahun;
        private int m_IDDInas;
        private int m_iUnit;

        private int m_IDKegiatan;
        private long m_IDSubKegiatan;
        private int m_IDJenis;
        private int _tahap;
        private bool _gridSudahDiFormat = false;
        private int mProfile;
        private int modeForm;
        const int MODE_PLAFON = 1;
        const int MODE_REALISASI =2;
        
        public frmInputPlafonPerRekening(int pProfile )
        {
            InitializeComponent();
            modeForm = MODE_PLAFON;
            m_IDSubKegiatan = 0;
            mProfile = pProfile;
            m_iTahun = GlobalVar.TahunAnggaran;
            _tahap=1;
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
        private void frmInputPlafonPerRekening_Load(object sender, EventArgs e)
        {
            switch (mProfile)
            {
                case 1:
                    ctrlHeader1.SetCaption("Input Anggaran Per Rekening..");
                    break;
                case 3:
                    ctrlHeader1.SetCaption("Input Realisasi Per Rekening..");
                    
                        DataGridViewColumn column = gridSumberDana.Columns[3];
                        column.HeaderText = "Realisasi";
                        DataGridViewColumn column3 = gridSumberDana.Columns[3];
                        //  column3.Visible = false;
                        ctrlHeader1.SetCaption("Input Realisasi Per Kode Rekening");

                    

                    break;
                
            }
            
           if (_tahap < 3 || mProfile==3) { 
                gridSumberDana.Columns[4].Visible = false;
                gridSumberDana.Columns[5].Visible = false;
                gridSumberDana.Columns[6].Visible = false;
                gridSumberDana.Columns[7].Visible = false;
            }

          TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
          TahapanAnggaran m_TA = new TahapanAnggaran();

           m_TA = oTALogic.GetByDinas(m_IDDInas, m_iTahun);
           if (m_TA.Tahap == 2)
           {
               gridSumberDana.Columns[2].ReadOnly = false;
               gridSumberDana.Columns[3].ReadOnly = false;
               gridSumberDana.Columns[4].ReadOnly = false;
               gridSumberDana.Columns[5].ReadOnly = false;
               gridSumberDana.Columns[6].ReadOnly = false;
           }
           if (m_TA.Tahap == 3)
           {

               gridSumberDana.Columns[3].ReadOnly = true;
               gridSumberDana.Columns[4].ReadOnly = false;
               gridSumberDana.Columns[5].ReadOnly = false;
               gridSumberDana.Columns[6].ReadOnly = false;
           }

           if (m_TA.Tahap == 4)
           {

               gridSumberDana.Columns[3].ReadOnly = true;
               gridSumberDana.Columns[4].ReadOnly = true;
               gridSumberDana.Columns[5].ReadOnly = false;
               gridSumberDana.Columns[6].ReadOnly = false;
           }

           if (m_TA.Tahap == 5)
           {

               gridSumberDana.Columns[3].ReadOnly = true;
               gridSumberDana.Columns[4].ReadOnly = true;
               gridSumberDana.Columns[5].ReadOnly = true;
               gridSumberDana.Columns[6].ReadOnly = false;
           }

            ctrlDinas1.Create();
            gridSumberDana.FormatHeader();
           // this.WindowState=FormWindowState.Maximized;

            ctrlJenisAnggaran1.Create(1);
        }
        public int Profile
        {
            set{ mProfile= value;}
        }
        public void SetMode(int mode)
        {
            modeForm = mode;
            if (mProfile== 3)
            {
                DataGridViewColumn column = gridSumberDana.Columns[3];
                column.HeaderText = "Realisasi";
                DataGridViewColumn column3 = gridSumberDana.Columns[3];
              //  column3.Visible = false;
                ctrlHeader1.SetCaption("Input Realisasi Per Kode Rekening");
                
            }
        
        }
        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        //private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        //{
        //    m_IDDinas = pIDSKPD;
            
        //    treeProgramKegiatan1.Create(m_IDDinas, 1);
        //    treeProgramKegiatan1.Profile = 3;
        //    TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
        //    TahapanAnggaran m_TA = new TahapanAnggaran();

        //    m_TA = oTALogic.GetByDinas(m_IDDinas, m_iTahun);

        //    if (m_TA.Tahap == 1)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = false;
        //        gridSumberDana.Columns[4].ReadOnly = false;
        //        gridSumberDana.Columns[5].ReadOnly = false;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }
        //    if (m_TA.Tahap == 3)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = true;
        //        gridSumberDana.Columns[4].ReadOnly = false;
        //        gridSumberDana.Columns[5].ReadOnly = false;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }

        //    if (m_TA.Tahap == 4)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = true;
        //        gridSumberDana.Columns[4].ReadOnly = true;
        //        gridSumberDana.Columns[5].ReadOnly = false;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }

        //    if (m_TA.Tahap == 5)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = true;
        //        gridSumberDana.Columns[4].ReadOnly = true;
        //        gridSumberDana.Columns[5].ReadOnly = true;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }

        //    if (m_TA.StatusInput == 9)
        //    {
        //        cmdSimpan.Visible = false;

        //    }
        //    else
        //    {
        //        cmdSimpan.Visible = true ;
        //    }


    
        //}

        private void ctrlJenisAnggaran1_OnChanged(int pID)
        {
            m_IDJenis = pID;
            LoadAnggaran();
        }
        private bool LoadAnggaran()
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,3);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            int _iJenis = ctrlJenisAnggaran1.GetID();
            m_IDJenis = _iJenis;
            
            if (GlobalVar.PP90 == true)
            {
                if (m_IDKegiatan != m_IDSubKegiatan / 10000)
                {
                    //  MessageBox.Show("IDKegiatan tidak Konsisten betul..");
                    m_IDKegiatan = (int)(m_IDSubKegiatan / 10000);
                }
                if (m_IDProgram != m_IDSubKegiatan / 10000000)
                {
                    //  MessageBox.Show("IDProgram tidak Konsisten betul..");
                    m_IDProgram = (int)(m_IDSubKegiatan / 10000000);
                }
                if (m_IDUrusan != (m_IDSubKegiatan / 1000000000))
                {
                    // MessageBox.Show("IDKegiatan tidak Konsisten betul..");
                    m_IDUrusan = (int)(m_IDSubKegiatan / 1000000000);



                }
            }
            else
            {

            }
            if (ctrlJenisAnggaran1.GetID() != 3)
            {

                m_IDKegiatan = 0;
                m_IDProgram = 0;

                m_IDSubKegiatan = 0;
                
                m_IDUrusan = DataFormat.GetInteger(m_IDDinas.ToString().Substring(0, 3));
            }

            if (m_IDUrusan == 0 && _iJenis == 3)
                return false;

            //m_IDUrusan = m_IDUrusan == 0 ? DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 3)) : m_IDUrusan;
            int _bPPKD = 0;// (int)ctrlDinas1.PPKD();
            //if (mProfile == 3)
            //    lstRekening = oLogic.GetANggaranDanRealisasi(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan, _iJenis);
            //else
            //{
                int kodeuk = ctrlDinas1.UnitAnggaran;
                lstRekening = oLogic.GetPlafon50(GlobalVar.TahunAnggaran, m_IDDinas, kodeuk, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDSubKegiatan, _iJenis, _bPPKD, GlobalVar.TahapAnggaran, 1);

           // }
                

            gridSumberDana.Rows.Clear();

            if (mProfile < 3)
            {
                foreach (TAnggaranRekening ta in lstRekening)
                {
                    //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                    //string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.Plafon.ToRupiahInReport(), ta.JumlahPergeseran.ToRupiahInReport(), ta.JumlahRKAP.ToRupiahInReport(), ta.PlafonABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                    
                    string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.JumlahMurni.ToRupiahInReport(), ta.JumlahPergeseran .ToRupiahInReport(),ta.JumlahRKAP.ToRupiahInReport(),ta.JumlahABT.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                    gridSumberDana.Rows.Add(rowUtkSumberDana);
                }
            }
            else
            {
                foreach (TAnggaranRekening ta in lstRekening)
                {
                    string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.Realisasi.ToRupiahInReport(), ta.Realisasi.ToRupiahInReport() };
                    gridSumberDana.Rows.Add(rowUtkSumberDana);
                }
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

                //m_dPagu = oKegiatan.Pagu;
                LoadAnggaran();
                //HitungJumlahRKA();
            }
            return lRet;
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,mProfile);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();


            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran,3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            _tahap = ctrlDinas1.GetTahapAnggaran();
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
                

                o.Tahap = _tahap;


                o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                if (_tahap == 2)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                }
                if (_tahap == 3)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                }
                if (_tahap == 4)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[5].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[5].Value));
                }
                if (_tahap == 5)
                {
                    o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                    o.JumlahPergeseran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                    o.JumlahRKAP = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[5].Value));
                    o.JumlahABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[6].Value));
                }
                o.KodeUK = ctrlDinas1.UnitAnggaran;
                //o.Realisasi = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[4].Value));
                o.IDSubKegiatan = m_IDSubKegiatan;
                if (o.IDRekening> 0)
                    _lstRek.Add(o);

            }
            //if (m_IDJenis == 3)
            //    oLogic.SimpanPlafon(_lsd, (int)GlobalVar.TahunAnggaran, m_IDUrusan, m_IDDinas, m_IDProgram, m_IDKegiatan);

            if (mProfile == 1)
            {

                int kodeuk = ctrlDinas1.UnitAnggaran;  
                    oLogicRek.SimpanPlafon(_lstRek, (int)GlobalVar.TahunAnggaran, m_IDDinas, kodeuk, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDJenis, GlobalVar.TahapAnggaran);

              
            }
            
            MessageBox.Show("Penyimpanan Selesai");
           



        

        }

        private void ctrlJenisAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage treeProgramKegiatan1_SubKegiatanChanged(long ID)
        {

            EventResponseMessage resp = new EventResponseMessage();
            resp.ResponseStatus = true;
            //m_IDSubkegiatan = ID;

            //TSubKegiatan oSUbKEgiatan = new TSubKegiatan();
            //TSubKegiatanLogic oLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran,1);
            ////m_IDUrusan = m_IDSubkegiatan / 100000;
            ////m_IDProgram = m_IDSubkegiatan / 10000;
            ////m_IDKegiatan = m_IDSubkegiatan / 10;

            //oSUbKEgiatan = oLogic.GetSubKegiatanEx(GlobalVar.TahunAnggaran, m_IDDinas,m_IDSubkegiatan);

            //txtpaguSub.Text = oSUbKEgiatan.Pagu.ToRupiahInReport();

            EventResponseMessage lRet = new EventResponseMessage();
            lRet.ResponseStatus = true;

            if (GlobalVar.PP90 == false)
                return lRet;

            m_IDSubKegiatan = ID;
            if (ID == 0)
                return lRet;
            int idKegeiatan = DataFormat.GetInteger(ID.ToString().Substring(0, 8));

            if (m_IDKegiatan != m_IDSubKegiatan / 100)
            {
                //  MessageBox.Show("IDKegiatan tidak Konsisten betul..");
                m_IDKegiatan = (int)(m_IDSubKegiatan / 100);
            }
            if (m_IDProgram != m_IDSubKegiatan / 100000)
            {
                //    MessageBox.Show("IDProgram tidak Konsisten betul..");
                m_IDProgram = (int)(m_IDSubKegiatan / 100000);
            }
            if (m_IDUrusan != m_IDSubKegiatan / 10000000)
            {
                //    MessageBox.Show("IDKegiatan tidak Konsisten betul..");
                m_IDUrusan = (int)(m_IDSubKegiatan / 10000000);
            }
            ShowSubKegiatan();
            LoadAnggaran();
            return resp;

            //return default(EventResponseMessage);
        }

        private void gridSumberDana_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridSumberDana_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (mProfile < 3)
            {
                if (gridSumberDana.Rows[e.RowIndex].Cells[6].Value.ToString().FormatUangReportKeDecimal() <
                    gridSumberDana.Rows[e.RowIndex].Cells[7].Value.ToString().FormatUangReportKeDecimal())
                {
                    MessageBox.Show("Tidak boleh lebih kecil dari Realisasi..");
                    cmdSimpan.Enabled = true;
                    // return;

                }
                else
                {
                    cmdSimpan.Enabled = true;
                    if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                    {
                        refreshJumah();
                    }
                }
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
               
            jumlahPerubahan = 0;
                for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                {
                    if (gridSumberDana.Rows[i].Cells[5].Value != null)
                    {
                        decimal d = gridSumberDana.Rows[i].Cells[5].Value.ToString().FormatUangReportKeDecimal();
                        jumlahPerubahan = jumlahPerubahan + d;
                    }
                }




                txtJumlahPerubahan.Text = jumlahPerubahan.ToRupiahInReport();
            jumlahPerubahan = 0;

                for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                {
                    if (gridSumberDana.Rows[i].Cells[6].Value != null)
                    {
                        decimal d = gridSumberDana.Rows[i].Cells[6].Value.ToString().FormatUangReportKeDecimal();
                        jumlahPerubahan = jumlahPerubahan + d;
                    }
                }




                txtJumlahPergeseranPerubahan.Text = jumlahPerubahan.ToRupiahInReport();


       

        
        }

        private void treeProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }
        private void ShowSubKegiatan()
        {

          TProgramAPBD m_oProgram;
          TKegiatanAPBD m_oKegiatan;
          TSubKegiatan m_oSubKegiatan;
          TSubKegiatanLogic oKAPBDLOgic = new TSubKegiatanLogic(m_iTahun, mProfile);
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
            //ParameterLaporan p = new ParameterLaporan();
            //p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            //p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            //p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            //p.NamaDinas = ctrlDinas1.GetNamaSKPD();
            
            //p.Tanggal = DateTime.Now.Date.ToString("dd MMM yyyy");
            //p.Tahap = pTahap;
            //p.dTanggal = DateTime.Now.Date;

            //if (ctrlDinas1.GetID() == 0)
            //{
            //    MessageBox.Show("Dinas Belum dipilih");
            //    return;
            //}

            //frmReportViewer f = new frmReportViewer();
            //if (pTahap < 3)
            //{
            //    f.CetakDPA22(p, GlobalVar.TahunAnggaran, m_IDDinas, lstSKPD);
            //}
            //else
            //{
            //    f.CetakDPA22ABT(p, GlobalVar.TahunAnggaran, m_IDDinas, lstSKPD);
            //}
            //f.Show();

        }
        private void CetakRekap(bool bSampul, int pTahap, List<SKPD> lstSKPD = null)
        {

            ParameterLaporan p = new ParameterLaporan();
            p.KodeUrusan = ctrlDinas1.KodeUrusanPemerintahan();
            p.KodeOrganisasi = ctrlDinas1.KodeOrganisasi();
            p.NamaUrusan = ctrlDinas1.NamaUrusanPemerintahan();
            p.NamaDinas = ctrlDinas1.GetNamaSKPD();
            p.Tanggal = DateTime.Now.Date.ToString("dd MMM yyyy");
            p.Tahap = pTahap;
            p.dTanggal = DateTime.Now.Date;

            //frmReportViewer f = new frmReportViewer();
            //// f.CetakDPARekapBersampul(p, GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), false);
            //if (pTahap < 3)
            //{
            //    f.CetakDPARekap(p, GlobalVar.TahunAnggaran, m_IDDinas, bSampul, lstSKPD);
            //}
            //else
            //{
            //    f.CetakDPARekapPerubahan(p, GlobalVar.TahunAnggaran, m_IDDinas, false, lstSKPD);
            //}



            //f.Show();
        }

        private void cmdCetakDPASKPD_Click(object sender, EventArgs e)
        {
            CetakRekap(false, _tahap, null);
        }

        //private void ctrlSKPD1_OnChanged(int pID)
        //{
        //    m_IDDinas = pID;

        //    treeProgramKegiatan1.Create(m_IDDinas, 1);
        //    treeProgramKegiatan1.Profile = 3;
        //    TahapanAnggaranLogic oTALogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
        //    TahapanAnggaran m_TA = new TahapanAnggaran();

        //    m_TA = oTALogic.GetByDinas(m_IDDinas, m_iTahun);

        //    if (m_TA.Tahap == 1)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = false;
        //        gridSumberDana.Columns[4].ReadOnly = false;
        //        gridSumberDana.Columns[5].ReadOnly = false;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }
        //    if (m_TA.Tahap == 3)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = true;
        //        gridSumberDana.Columns[4].ReadOnly = false;
        //        gridSumberDana.Columns[5].ReadOnly = false;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }

        //    if (m_TA.Tahap == 4)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = true;
        //        gridSumberDana.Columns[4].ReadOnly = true;
        //        gridSumberDana.Columns[5].ReadOnly = false;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }

        //    if (m_TA.Tahap == 5)
        //    {

        //        gridSumberDana.Columns[3].ReadOnly = true;
        //        gridSumberDana.Columns[4].ReadOnly = true;
        //        gridSumberDana.Columns[5].ReadOnly = true;
        //        gridSumberDana.Columns[6].ReadOnly = false;
        //    }

        //    if (m_TA.StatusInput == 9)
        //    {
        //        cmdSimpan.Visible = false;

        //    }
        //    else
        //    {
        //        cmdSimpan.Visible = true;
        //    }

        //}

        private void ctrlDinas1_Load_1(object sender, EventArgs e)
        {
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlDinas1.Create();
                ctrlDinas1.SetID(GlobalVar.Pengguna.SKPD);
                m_IDDinas = GlobalVar.Pengguna.SKPD;
                m_iUnit = GlobalVar.Pengguna.SKPD + GlobalVar.Pengguna.KodeUK;
                RereshGrid();
            }
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDinas = pIDSKPD;

            m_iUnit = pIDSKPD + pIDUK;
            RereshGrid();
        }

        private void RereshGrid(){
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

            if (m_TA.Tahap == 2)
            {

                gridSumberDana.Columns[3].ReadOnly = false;
                gridSumberDana.Columns[3].Visible = true;
                gridSumberDana.Columns[4].ReadOnly = true;
                gridSumberDana.Columns[4].Visible = false;
                gridSumberDana.Columns[5].ReadOnly = true;
                gridSumberDana.Columns[5].Visible = false ;
                gridSumberDana.Columns[6].ReadOnly = true;
                gridSumberDana.Columns[6].Visible = false;
            }
            if (m_TA.Tahap == 3)
            {

                gridSumberDana.Columns[3].ReadOnly = true;
                gridSumberDana.Columns[4].ReadOnly = false;
                gridSumberDana.Columns[4].Visible= true ;

                gridSumberDana.Columns[5].ReadOnly = false;
                gridSumberDana.Columns[6].ReadOnly = false;
            }

            if (m_TA.Tahap == 4)
            {

                gridSumberDana.Columns[3].ReadOnly = true;
                gridSumberDana.Columns[4].ReadOnly = true;
                gridSumberDana.Columns[5].ReadOnly = false;
                gridSumberDana.Columns[6].ReadOnly = true;

                gridSumberDana.Columns[4].Visible = true;
                gridSumberDana.Columns[5].Visible = true;
                gridSumberDana.Columns[6].Visible = false;
   
            }

            if (m_TA.Tahap == 5)
            {

                gridSumberDana.Columns[3].ReadOnly = true;
                gridSumberDana.Columns[4].ReadOnly = true;
                gridSumberDana.Columns[5].ReadOnly = true;
                gridSumberDana.Columns[6].ReadOnly = false;

                gridSumberDana.Columns[4].Visible = true;
                gridSumberDana.Columns[5].Visible = true;
                gridSumberDana.Columns[6].Visible = true;
            }

            if (m_TA.StatusInput == 9)
            {
                cmdSimpan.Visible = false;

            }
            else
            {
                cmdSimpan.Visible = true;
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            


            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,mProfile);
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
            if (ctrlJenisAnggaran1.GetID() == 1) {
                fInputRek.ParentRekening= 4;
            }

            if (ctrlJenisAnggaran1.GetID() == 2 || ctrlJenisAnggaran1.GetID() == 3 )
            {
                fInputRek.ParentRekening = 5;
            }
            if (ctrlJenisAnggaran1.GetID() ==4 )
            {
                fInputRek.ParentRekening = 61;
            }
            if (ctrlJenisAnggaran1.GetID() == 5)
            {
                fInputRek.ParentRekening = 62;
            }
            fInputRek.ShowDialog();
            if (fInputRek.IsOK)
            {
                long idRekening = fInputRek.IDrekening;
                TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran,0,mProfile);
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

             //   _tahap = 3;
                _tahap = ctrlDinas1.GetTahapAnggaran();

                o.Tahap = _tahap;

                o.KodeUK = ctrlDinas1.UnitAnggaran;
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

        private void cmdTambahSubKegiatan_Click(object sender, EventArgs e)
        {
            frmSUbKegiatan fSub= new frmSUbKegiatan();
            fSub.Show();
        }

    }
}
