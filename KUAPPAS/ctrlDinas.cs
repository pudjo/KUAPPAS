using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;
using DTO;

using BP;


namespace KUAPPAS
{
    public partial class ctrlDinas : UserControl
    {
        private int m_iKodeSKPD;
        private int m_iKodeUK;
        public delegate void ValueChangedEventHandler(int pIDSKPD, int pIDUK);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int m_iUnitAnggaran;// Dalam anggaran kode Unitnya bisa jdi tidak ada;
                                    //kadang ada yang pakai 1
        private bool WIthUK;

        public ctrlDinas()
        {
            InitializeComponent();
            m_iUnitAnggaran = 0;    
        }

        public int GetTahapAnggaran()
        {
            TahapanAnggaran ta = new TahapanAnggaran();
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            m_SelectedID = ctrlSKPD1.GetID ();
            m_iKodeSKPD = GetID();
            ta = oLogic.GetByDinas(m_SelectedID, (int)GlobalVar.TahunAnggaran);
            
            return ta.Tahap;

        }
        public bool GetStatusInput()
        {
            TahapanAnggaran ta = new TahapanAnggaran();
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            m_SelectedID = GetID();
            m_iKodeSKPD = GetID();
            ta = oLogic.GetByDinas(m_SelectedID, (int)GlobalVar.TahunAnggaran);
            if (ta.StatusInput ==9 )
                return false;
            return true;



        }
        public int GetTahapAnggaranKas()
        {
            TahapanAnggaran ta = new TahapanAnggaran();
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            m_SelectedID = GetID();
            m_iKodeSKPD = GetID();
            ta = oLogic.GetByDinas(m_SelectedID, (int)GlobalVar.TahunAnggaran);
            return ta.StatusAnggaranKas;

        }
        public Pejabat GetKuaaPenggunaAnggaranPenerimaan(DateTime d)
        {
            Pejabat kpendapatan = new Pejabat();

            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                m_iKodeSKPD = GetID();
                kpendapatan = oLogic.GetKuaaPenggunaAnggaranPenerimaan(m_iKodeSKPD, 0, d, m_iKodeUK);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                kpendapatan.Nama = "";
                kpendapatan.Jabatan = "";
                kpendapatan.NIP = "";

            }
            return kpendapatan;

        }
        public int KodeUk
        {
            set
            {
                m_iKodeUK = value;
            }
        }
        public  Pejabat GetPimpinan(DateTime d )
        {
            Pejabat oKepalaDinas = new Pejabat();
                
            try{
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                m_iKodeSKPD = GetID();
                oKepalaDinas = oLogic.GetKepalaDinas(m_iKodeSKPD, 0,d, m_iKodeUK);
                
            } catch(Exception ex){
                MessageBox.Show(ex.Message);
                oKepalaDinas.Nama = "";
                oKepalaDinas.Jabatan = "";
                oKepalaDinas.NIP = "";

            }
            return oKepalaDinas;

        }

        public Pejabat GetBendaharaPengeluaran(DateTime d)
        {
            Pejabat bendahara = new Pejabat();
                
            try{
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                m_iKodeSKPD = GetID();
                bendahara = oLogic.GetBendaharaDinas(m_iKodeSKPD, 0, d, m_iKodeUK);
                
            } catch(Exception ex){
                MessageBox.Show(ex.Message);
                bendahara.Nama ="";
                bendahara.Jabatan ="";
                bendahara.NIP ="";
                bendahara.NoRekening ="";
                bendahara.NPWP="";
                bendahara.NamaBank ="123";


            }
            return bendahara;

        }
        public Pejabat GetBendaharaPenerimaan(DateTime d)
        {
            Pejabat bendahara = new Pejabat();
                
            try{
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                m_iKodeSKPD = GetID();
                bendahara = oLogic.GetBendaharaPenerimaan (m_iKodeSKPD ,0, d,m_iKodeUK);
                
            } catch(Exception ex){
                MessageBox.Show(ex.Message);
                bendahara.Nama ="";
                bendahara.Jabatan ="";
                bendahara.NIP ="";
                bendahara.NoRekening ="";
                bendahara.NPWP="";
                bendahara.NamaBank ="123";


            }
            return bendahara;

        }
        public Pejabat GetPPK(DateTime d)
        {
            Pejabat ppk = new Pejabat();
                
            try{
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                if (m_iKodeUK <= 1)
                {
                    m_iKodeSKPD = GetID();
                    ppk = oLogic.GetPPKSKPD(m_iKodeSKPD, 0, d);
                }
                else
                {
                    m_iKodeSKPD = GetID();
                    ppk = oLogic.GetPPKSKPD(m_iKodeSKPD, 0, d, m_iKodeUK);
                }
                
                
            } catch(Exception ex){

                MessageBox.Show(ex.Message);
                ppk.Nama ="";
                ppk.Jabatan ="";
                ppk.NIP ="";
                ppk.NoRekening ="";
                ppk.NPWP="";
                ppk.NamaBank ="123";


            }
            return ppk;

        }
        public bool PilihanValid { 
          get {
              if (WIthUK == true && m_iKodeUK == 0)
              {
                  MessageBox.Show("Dinas punya Unit, Harus pilih Unitnya..");
                  return false;
              }
              return true;
          } 
        }
        private void ctrlDinas_Load(object sender, EventArgs e)
        {
           // ctrlUnitKerja1.Visible = false;
            //if (GlobalVar.TahunAnggaran >= 2021)
            //{
            //    chkPPKD.Visible = false;
            //    ctrlSKPD1.Width = this.Width- chkPPKD.Width-10;
            //    ctrlUnitKerja1.Width = this.Width;
            //}
            ctrlSKPD1.Width = this.Width ;
            ctrlUnitKerja1.Width = this.Width;
        }
        public void Create(int IDSKPD=0)
        {
            if (GlobalVar.TahunAnggaran < 2021)
            {
                if (GlobalVar.Pengguna.PPKD == true)
                {
                    chkPPKD.Visible = true;
                }
                else
                {
                    chkPPKD.Checked = false;
                    chkPPKD.Visible = true;
                }
            } else {
                   chkPPKD.Checked = false;
                chkPPKD.Visible = false;
            }
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);
                if (GlobalVar.gListOrganisasi!= null)
                {
                    if (GlobalVar.gListOrganisasi.Count > 0)
                    {
                        ctrlUnitKerja1.Visible = true;
               
                        ctrlUnitKerja1.Create(GlobalVar.Pengguna.SKPD);
                    }
                    else
                    {
                        ctrlUnitKerja1.Visible = false ;
                        Height = ctrlSKPD1.Height;
                    }
                }
                else
                {
                    if (ctrlUnitKerja1.Create(GlobalVar.Pengguna.SKPD)>0){
                        ctrlUnitKerja1.Visible = true;
                    } else{
                        ctrlUnitKerja1.Visible = false;
                        Height = ctrlSKPD1.Height;
                    }
                        
                    
                    
                }
                
            }
        }

        public void CreateWithAll()
        {

        }
        public int KodeSKPD()
        {
            return m_iKodeSKPD;
        }
        public int KodeUK()
        {
            return m_iKodeUK;

        }

        public int UK
        {
            get
            {
                return m_iKodeUK;
            }
            set 
            {
                ctrlUnitKerja1.SetID(value);
            }

        }
        public int PPKD()
        {
            if (CekIsPPKD(m_iKodeSKPD) == false)
                return 0;
            return chkPPKD.Checked == true ? 1 : 0;

        }
        public void SetPPKD( Single bPPKD)
        {
            chkPPKD.Checked = bPPKD == 1 ? true : false;// true ? 1 : 0;

        }


        private void ctrlSKPD1_OnChanged(int pID)
        {
               m_iKodeSKPD= pID;

               if (GlobalVar.TahunAnggaran < 2021)
               {
                   if (CekIsPPKD(m_iKodeSKPD) == true)
                   {
                       chkPPKD.Visible = true;
                   }
                   else
                   {
                       chkPPKD.Visible = false;
                   }
               }
               if (ctrlUnitKerja1.Create(pID) == 0)
               {  // Tidak punya Unit Kerja
                   m_iKodeUK = 0;
                   ctrlUnitKerja1.Visible = false;
                   this.Height = ctrlSKPD1.Height;

                   FireChangeEvent();
                   WIthUK = false;
               }
               else
               { // ada UnitKerja 

                   this.Height = ctrlSKPD1.Height + ctrlUnitKerja1.Height; 

                   ctrlUnitKerja1.Visible = true;
                   m_iKodeUK = 0;
                   if (GlobalVar.Pengguna.KodeUK > 1)
                   {
                       ctrlUnitKerja1.SetID(GlobalVar.Pengguna.KodeUK);

                       m_iKodeUK = GlobalVar.Pengguna.KodeUK;
                       ctrlUnitKerja1.Enabled = false;
                   }
               
                   WIthUK = true;

                   FireChangeEvent();
               }
        }
        public int GetID()
        {
            //if (m_iKodeUK == 0)
            //{
            return ctrlSKPD1.GetID();
                //return KodeSKPD();
            //} else {

            //    return KodeUK();
            //}           
        }

        private void ctrlUnitKerja1_OnChanged(int pID)
        {
            m_iKodeUK = pID ;
            
            if (CekIsPPKD(m_iKodeUK) == true)
            {
                chkPPKD.Visible = true;
            }
            else
            {
                chkPPKD.Visible = false;
            }

            FireChangeEvent();
        }
        private bool CekIsPPKD(int iDinas)
        {
            KasdaLogic oLogic = new KasdaLogic(GlobalVar.TahunAnggaran);
            return oLogic.IsPPKD(iDinas);

        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                m_iKodeSKPD = ctrlSKPD1.GetID();
                OnChanged(m_iKodeSKPD, m_iKodeUK);
            }
        }
        public void SetID(int pDinas)
        {
            DinasLogic oDinasLogic = new DinasLogic(GlobalVar.TahunAnggaran);
            Dinas oDinas = new Dinas();
            
            int pSKPD = DataFormat.GetInteger(pDinas.ToString().Substring(0, 5) +"00");
            ctrlUnitKerja1.Visible = false;
            int pUK =0;

            if (GlobalVar.Pengguna.KodeUK> 0)
            {
                pUK = pDinas;
                ctrlUnitKerja1.Visible = true ;
            }

            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlSKPD1.SetID(pSKPD);
            ctrlUnitKerja1.Create(pSKPD);
            ctrlUnitKerja1.SetID(pUK);
            m_iKodeSKPD = pSKPD;
            m_iKodeUK = pUK;
            FireChangeEvent();
        }

        public void SetID(int pSKPD, int pUK, bool bFireEvent = false )
        {
            m_iKodeSKPD = pSKPD;
            m_iKodeUK = pUK;
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            ctrlSKPD1.SetID(pSKPD);


           ctrlUnitKerja1.Create(pSKPD) ;
            //{
            //    ctrlUnitKerja1.Visible = true;
            //} else {
            //    ctrlUnitKerja1.Visible = false ;
            //}
            if (pUK>0 )
                ctrlUnitKerja1.Visible = true;
            else
                ctrlUnitKerja1.Visible = false ;

            ctrlUnitKerja1.SetID(pUK);
            m_iKodeSKPD= pSKPD;
            m_iKodeUK = pUK;
            if (bFireEvent== true)
                FireChangeEvent();
        }
        public int GetKodeKategori()
        {
            return ctrlSKPD1.KodeKategori();
        }
        public int GetKodeUrusan()
        {
            return ctrlSKPD1.KodeUrusan();
        }
        public int IDUrusan()
        {
            return ctrlSKPD1.IDUrusan();
        }

        public int GetKodeSKPD()
        {
            return ctrlSKPD1.KodeSKPD();
        }
        public int GetKodeUK()
        {
            m_iKodeUK=ctrlUnitKerja1.GetID(); 
            if (m_iKodeUK > 0)
                return m_iKodeUK % 100;

            return 0;
        }

        private void ctrlUnitKerja1_Load(object sender, EventArgs e)
        {

        }
        public string KodeUrusanPemerintahan()
        {
            return ctrlSKPD1.KodeUrusanPemerintahan();

            


        }
        public string GetNamaSKPD()
        {
            return ctrlSKPD1.GetNamaSKPD();
        }
        public string GetNamaUnit()
        {
            return ctrlUnitKerja1.GetNamaUnit();

        }
        public Unit Unit
        {
            get
            {
                return ctrlUnitKerja1.Unit;
            }
        }
        public int UnitAnggaran
        {
            get
            {
                Unit unit = new Unit();
                unit = ctrlUnitKerja1.Unit;
                if (unit != null)
                {
                    return unit.UntAnggaran;
                }
                return 0;

            }
        }
        public string KodeOrganisasi()
        {
            return ctrlSKPD1.KodeOrganisasi();
        }
        public string NamaUrusanPemerintahan()
        {

            return ctrlSKPD1.NamaUrusanPemerintahan();
        }

        private void chkPPKD_CheckedChanged(object sender, EventArgs e)
        {
            OnChkPPKDChanged();

        }
        private void OnChkPPKDChanged()
        {
            if (chkPPKD.Checked == true)
            {
                KasdaLogic oLogic = new KasdaLogic(GlobalVar.TahunAnggaran);
                Kasda oKasda = new Kasda();
                oKasda = oLogic.Get();
                if (oKasda != null)
                {
                    SetID(oKasda.IDDInas);
                }

            }
            FireChangeEvent();
        }
        public SKPD GetSKPD()
        {
            SKPD oSKPD = new SKPD();

            SKPDLogic oLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            int iDSKPD = ctrlSKPD1.GetID();
            oSKPD = oLogic.GetByID(iDSKPD);
            return oSKPD;

        }
        public int GetIDSKPD()
        {

            int iDSKPD = ctrlSKPD1.GetID();
            return iDSKPD; 
        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlUnitKerja2_Load(object sender, EventArgs e)
        {

        }

        private void ctrlUnitKerja2_OnChanged(int pID)
        {
            m_iKodeUK = pID;
        
            ctrlUnitKerja1.Visible = true;

            FireChangeEvent();
            //}
        }
        public bool WithUnitKerja()
        {
            return WIthUK;
        }
        public bool HasUK
        {
            get
            {
                return WIthUK;
            }
        }
        private void ctrlUnitKerja1_Load_1(object sender, EventArgs e)
        {

        }
        //public int GetTahapANggaran()
        //{
        //    try{
        //        TahapanAnggaranLogic taLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
        //        TahapanAnggaran ta = new TahapanAnggaran();

        //        ta = taLogic.GetByDinas(ctrlSKPD1.GetID(), GlobalVar.TahunAnggaran);
        //        return ta.Tahap;
        //    } catch(Exception ex){
        //        MessageBox.Show ("Gagal Mendapatkan Tahapan Anggaran");
        //        return 0;
        //    }
          



        //}
            
       
    }
}