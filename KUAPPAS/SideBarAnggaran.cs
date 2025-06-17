using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS
{
    public partial class SideBarAnggaran : UserControl
    {
        public delegate void ValueLeaveEventHandler(int pID, int pos);
        public delegate void ValueChangedEventHandler(int pID);



        public event ValueLeaveEventHandler OnLeaveMenu;
        public event ValueChangedEventHandler OnEnterControl;

        public event ValueChangedEventHandler OnMenuPemda;
        public event ValueChangedEventHandler OnMenuSKPD;
        public event ValueChangedEventHandler OnMenuPejabat;
        public event ValueChangedEventHandler OnMenuUrusanPemerintahan;
        public event ValueChangedEventHandler OnMenuProgramKegiatan;
        public event ValueChangedEventHandler OnMenuKodeRekening;
        public event ValueChangedEventHandler OnMenuSumberDana;

        public event ValueChangedEventHandler OnMenuDasarHukum;
        public event ValueChangedEventHandler OnMenuStandardHarga;


        public event ValueChangedEventHandler OnPaguSKPD;
        public event ValueChangedEventHandler OnPaguBTL;
        public event ValueChangedEventHandler OnPaguBL;
        public event ValueChangedEventHandler OnCetakPagu;
        //public event ValueChangedEventHandler OnMenuKodeRekening;
        //public event ValueChangedEventHandler OnMenuSumberDana;


        public event ValueChangedEventHandler OnMenuSettingTahapInput;


        public event ValueChangedEventHandler OnMenuSettingPlafonBTL;
        public event ValueChangedEventHandler OnMenuSettingPlafonBL;
        //public event ValueChangedEventHandler OnMenuDasarHukum;
        //public event ValueChangedEventHandler OnMenuStandardHarga;

        public event ValueChangedEventHandler OnMenuSettingPejabatSKPD;
        public event ValueChangedEventHandler OnMenuSettingTimTAPD;
        public event ValueChangedEventHandler OnMenuSettingRKA;
        public event ValueChangedEventHandler OnMenuSettingAnggaranKas;
        public event ValueChangedEventHandler OnMenuSettingDPA;


        public event ValueChangedEventHandler OnMenuSettingRKAPergeseran;
        public event ValueChangedEventHandler OnMenuSettingAnggaranKasPergeseran;
        public event ValueChangedEventHandler OnMenuSettingDPAPergeseran;

        public event ValueChangedEventHandler OnMenuSettingRKAPerubahan;
        public event ValueChangedEventHandler OnMenuSettingAnggaranKasPerubahan;
        public event ValueChangedEventHandler OnMenuSettingDPAPerubahan;

        public event ValueChangedEventHandler OnMenuSettingRKAPergeseranPerubahan;
        public event ValueChangedEventHandler OnMenuSettingAnggaranKasPergeseranPerubahan;
        public event ValueChangedEventHandler OnMenuSettingDPAPergeseranPerubahan;


        public event ValueChangedEventHandler OnMenuSPD;
        public event ValueChangedEventHandler OnMenuRegisterSPD;
        public event ValueChangedEventHandler OnMenuPerda;
        public event ValueChangedEventHandler OnMenuPerbub;

        public SideBarAnggaran()
        {
            InitializeComponent();
        }
        private void HideSubMenu(Panel pnSubMenu)
        {

            pnSubMenu.Height = 0;

        }
        private void ShowSubMenu(Panel pnSubMenu)
        {
            int countCB = 0;
            pnSubMenu.Height = 0;
            foreach (Control c in pnSubMenu.Controls)
            {

                if (c.GetType() == typeof(Button))
                {
                    countCB++;
                }
            }
            //if (pnSubMenu.Height < countCB * btnMasterPemda.Height)
            //{
            for (int x = 0; x < countCB; x++)
            {
                pnSubMenu.Height += btnMasterPemda.Height;
                if (pnSubMenu.Height == countCB * btnMasterPemda.Height)
                {
                    break;
                }
            }
            // }

        }
        private void btnMaster_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnMasterPemda_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnSKPD_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnPejabat_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnProgranKegiatan_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnPlafon_MouseLeave(object sender, EventArgs e)
        {

            OnMouseLeave(e);
        }

        private void SideBarAnggaran_MouseEnter(object sender, EventArgs e)
        {
           // OnMouseEnter(e);
        }

        private void btnPlafon_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnPlafonBTL_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }
        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;

            }
        }
        public int GetPosX()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            return Cursor.Position.X ;//- 50, Cursor.Position.Y - 50);

            //Cursor.Clip = new Rectangle(this.Location, this.Size);
        }
        private void btnPlafonBL_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnCetakPlafon_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnMasterPemda_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnSKPD_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnPejabat_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnProgranKegiatan_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void panelMaster_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void panelPlafon_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnMaster_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnPlafonBTL_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnPlafonBL_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnCetakPlafon_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void SideBarAnggaran_Load(object sender, EventArgs e)
        {
            if (GlobalVar.PP90 == true)
            {
                btnPaguBukanBelanjaLangsung.Text = "Pagu Belanja";

            }
        }

        private void btnMasterPemda_Click(object sender, EventArgs e)
        {
            if (OnMenuPemda != null)
            {
                OnMenuPemda(0);
            }
        }

        private void btnSKPD_Click(object sender, EventArgs e)
        {
            if (OnMenuSKPD != null)
            {
                OnMenuSKPD(0);
            }
        }

        private void btnPejabat_Click(object sender, EventArgs e)
        {
            if (OnMenuPejabat != null)
            {
                OnMenuPejabat(0);
            }

        }

        private void btnUrusanPemerintahan_Click(object sender, EventArgs e)
        {
            if (OnMenuUrusanPemerintahan != null)
            {
                OnMenuUrusanPemerintahan(0);
            }
        }

        private void btnProgranKegiatan_Click(object sender, EventArgs e)
        {
            if (OnMenuProgramKegiatan != null)
            {
                OnMenuProgramKegiatan(0);
            }

        }

        private void btnSumberDana_Click(object sender, EventArgs e)
        {
            if (OnMenuSumberDana != null)
            {
                OnMenuSumberDana(0);
            }
        }

        private void btnDasarHukum_Click(object sender, EventArgs e)
        {
            if (OnMenuDasarHukum != null)
            {
                OnMenuDasarHukum(0);
            }
        }

        private void btnKodeRekening_Click(object sender, EventArgs e)
        {
            if (OnMenuKodeRekening != null)
            {
                OnMenuKodeRekening(0);
            }
        }

        private void btnStandardHarga_Click(object sender, EventArgs e)
        {
            if (OnMenuStandardHarga != null)
            {
                OnMenuStandardHarga(0);
            }

        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
           
            ShowSubMenu(panelRKA);
            HideSubMenu(panelPlafon);
            HideSubMenu(panelCetakPerda);
            HideSubMenu(panelSPD);
            HideSubMenu(panelTransaksi);
            ShowSubMenu(panelMaster);



        }

        private void btnPaguSKPD_Click(object sender, EventArgs e)
        {
            if (OnPaguSKPD != null)
            {
                OnPaguSKPD(0);
            }

        }

        private void btnPaguBukanBelanjaLangsung_Click(object sender, EventArgs e)
        {
            if (OnPaguBTL != null)
            {
                OnPaguBTL(0);
            }

        }

        private void btnPlafonBL_Click(object sender, EventArgs e)
        {
            if (OnPaguBL != null)
            {
                OnPaguBL(0);
            }

        }

        private void btnCetakPlafon_Click(object sender, EventArgs e)
        {
            if (OnCetakPagu != null)
                OnCetakPagu(0);

        }

        private void btnProgranKegiatan_MouseEnter_1(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnProgranKegiatan_MouseLeave_1(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnPlafon_Click(object sender, EventArgs e)
        {
            
            
            ShowSubMenu(panelMaster);
            ShowSubMenu(panelRKA);
            HideSubMenu(panelCetakPerda);
            HideSubMenu(panelSPD);
            HideSubMenu(panelTransaksi);
            ShowSubMenu(panelPlafon);



        }

        private void btnSettingPejabatDinas_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnSettingTIMAnggaran_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnInputRKA_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnDPA_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnRKAPergeseran_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnAnggaranKasPergeseran_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnDPAPergeseran_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnRKAPerubahan_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void AnggaranKasPerubahan_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnDPAPerubahan_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnPaguBukanBelanjaLangsung_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnSettingPejabatDinas_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnSettingTIMAnggaran_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnInputRKA_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnAnggaranKas_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnAnggaranKas_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnDPA_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnAnggaranKasPergeseran_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnDPAPergeseran_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnRKAPerubahan_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void AnggaranKasPerubahan_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnDPAPerubahan_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnSettingPejabatDinas_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingPejabatSKPD != null)
            {
                OnMenuSettingPejabatSKPD(0);
            }
        }

        private void btnSettingTIMAnggaran_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingTimTAPD != null)
            {
                OnMenuSettingTimTAPD(0);
            }
        }

        private void btnInputRKA_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingRKA != null)
            {
                OnMenuSettingRKA(0);
            }

        }

        private void btnAnggaranKas_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingAnggaranKas != null)
            {
                OnMenuSettingAnggaranKas(0);
            }
        }

        private void btnDPA_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingDPA != null)
            {
                OnMenuSettingDPA(0);
            }
        }

        private void btnRKAPergeseran_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingRKAPergeseran !=null){
                OnMenuSettingRKAPergeseran(0);
            }
            

        }

        private void btnDPAPergeseran_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingDPAPergeseran != null)
            {
                OnMenuSettingDPAPergeseran(0);
            }

        }

        private void btnRKAPerubahan_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingRKAPerubahan != null)
            {
                OnMenuSettingRKAPerubahan(0);
            }
        }

        private void AnggaranKasPerubahan_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingAnggaranKasPerubahan != null)
            {
                OnMenuSettingAnggaranKasPerubahan(0);
            }
        }

        private void btnDPAPerubahan_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingDPAPerubahan != null)
            {
                OnMenuSettingDPAPerubahan(0);
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnRKA_Click(object sender, EventArgs e)
        {
            
            HideSubMenu(panelMaster);
            HideSubMenu(panelPlafon);
            HideSubMenu(panelCetakPerda);
            HideSubMenu(panelSPD);
            HideSubMenu(panelTransaksi);
            ShowSubMenu(panelRKA);
        }

        private void btnRKAGeserPerubahan_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingRKAPergeseranPerubahan != null)
            {
                OnMenuSettingRKAPergeseranPerubahan(5);

            }
        }

        private void btnAKGeserPerubahan_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingAnggaranKasPergeseranPerubahan!=null)
            {
                OnMenuSettingAnggaranKasPergeseranPerubahan(5);
            }
        }

        private void btnDPAGeserPerubahan_Click(object sender, EventArgs e)
        {
            if (OnMenuSettingDPAPergeseranPerubahan != null)
            {
                OnMenuSettingDPAPergeseranPerubahan(5);
            }
        }

        private void btnRKA_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnRKA_MouseLeave(object sender, EventArgs e)
        {

            OnMouseLeave(e);
        }

        private void btnRKAPergeseran_MouseLeave(object sender, EventArgs e)
        {

            OnMouseLeave(e);
        }

        private void btnRegisterSPD_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnBuatSPD_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnSPD_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnCetakPerda_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnCetakPerdaAPBD_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnCetakPergubAPBD_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void btnSPD_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnBuatSPD_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnRegisterSPD_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnCetakPerda_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnCetakPerdaAPBD_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnCetakPergubAPBD_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void btnCetakPerdaAPBD_Click(object sender, EventArgs e)
        {
            //frmCetakPerda2 f = new frmCetakPerda2();
            //f.Show();

        }

        private void btnCetakPergubAPBD_Click(object sender, EventArgs e)
        {
            //frmCetakPeraturanKepalaDaerah fPerkada = new frmCetakPeraturanKepalaDaerah();
            //fPerkada.Show();
        }

        private void btnSPD_Click(object sender, EventArgs e)
        {
           

            ShowSubMenu(panelMaster);
            ShowSubMenu(panelRKA);
            HideSubMenu(panelCetakPerda);
            HideSubMenu(panelPlafon);
            HideSubMenu(panelTransaksi);
            ShowSubMenu(panelSPD);


        }

        private void btnCetakPerda_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelCetakPerda);

            ShowSubMenu(panelMaster);
            ShowSubMenu(panelRKA);
            HideSubMenu(panelSPD);
            HideSubMenu(panelPlafon);
            HideSubMenu(panelTransaksi);
        }

        private void btnBendahara_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelTransaksi);

            ShowSubMenu(panelMaster);
            ShowSubMenu(panelRKA);
            HideSubMenu(panelCetakPerda);
            HideSubMenu(panelPlafon);
            HideSubMenu(panelSPD);
        }

        private void btnBuatSPD_Click(object sender, EventArgs e)
        {
            if (OnMenuSPD != null)
            {
                OnMenuSPD(0);
            }
        }

        private void btnRegisterSPD_Click(object sender, EventArgs e)
        {
            if (OnMenuRegisterSPD != null)
            {
                OnMenuRegisterSPD(0);
            }
        }

        private void btnTransaksi_Click(object sender, EventArgs e)
        {

        }



    }
}
//
/*
 *  public event ValueChangedEventHandler OnMenuSPD;
        public event ValueChangedEventHandler OnMenuRegisterSPD;
        public event ValueChangedEventHandler OnMenuPerda;
        public event ValueChangedEventHandler OnMenuPerbub;
 * public event ValueChangedEventHandler OnMenuPerda;
        public event ValueChangedEventHandler OnMenuPerbub;

    

       public event ValueChangedEventHandler OnMenuSettingRKAPergeseranPerubahan;
        public event ValueChangedEventHandler OnMenuSettingAnggaranKasPergeseranPerubahan;
        public event ValueChangedEventHandler OnMenuSettingDPAPergeseranPerubahan;

*/