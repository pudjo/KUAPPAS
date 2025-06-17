using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class MenuBendahara : UserControl
    {
        public delegate void ValueLeaveEventHandler(int pID, int pos);
        public delegate void ValueChangedEventHandler();

        public event ValueChangedEventHandler OnKontrak;
        public event ValueChangedEventHandler OnBAST;
        public event ValueChangedEventHandler OnSPP;
        public event ValueChangedEventHandler OnBKUSP2D;
        public event ValueChangedEventHandler OnPanjar;
        public event ValueChangedEventHandler OnTanggungJawabPanjar;
        public event ValueChangedEventHandler OnBPK;
        public event ValueChangedEventHandler OnTrxBank;
        public event ValueChangedEventHandler OnPengembalianBelanja;
        public event ValueChangedEventHandler OnKoreksi;
        public event ValueChangedEventHandler OnSetorPajak;
        public event ValueChangedEventHandler OnSPJUP;
       

        public event ValueChangedEventHandler OnSPM;
        public event ValueChangedEventHandler OnRegisterSPM;


        public event ValueChangedEventHandler OnSKRSKPD;
        public event ValueChangedEventHandler OnSTS;
        public event ValueChangedEventHandler OnSetorkeKasda;

        public event ValueChangedEventHandler OnKASDAPenerimaan;
        public event ValueChangedEventHandler OnKASDATanggalCair;
        public event ValueChangedEventHandler OnKASDABKU;





        public event ValueChangedEventHandler OnSPJ;
        public event ValueChangedEventHandler OnKartuKendali;
        public event ValueChangedEventHandler OnBKU;
        
        public event ValueChangedEventHandler OnTransaksi;
        public event ValueChangedEventHandler OnRegisterSPP;
        public event ValueChangedEventHandler OnRegisterSP2D;
        public event ValueChangedEventHandler OnKasTunai;
        public event ValueChangedEventHandler OnKasBank;
        public event ValueChangedEventHandler OnKasBukuPajak;
        public event ValueChangedEventHandler OnKasBukuPaanjar;
        public event ValueChangedEventHandler OnKasBukuKartuKendali;


        public event ValueChangedEventHandler OnJurnalTrx;
        public event ValueChangedEventHandler OnJurnalPenyesuaian;
        public event ValueChangedEventHandler OnJurnalManual;
        public event ValueChangedEventHandler OnTrxAset;
        public event ValueChangedEventHandler OnPosting;



        public event ValueChangedEventHandler OnLaporanLRA;
        public event ValueChangedEventHandler OnLaporanLO;
       

    

        public MenuBendahara()
        {
            InitializeComponent();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            panelSetting.Expand(0, 86);
        }

        private void btnPerbendaharaan_Click(object sender, EventArgs e)
        {
            panelBendahara.Expand(0, 80);
        }

        private void cmdSpj_Click(object sender, EventArgs e)
        {
            if (OnSPJ != null)
                OnSPJ();
        }

        private void cmdbku_Click(object sender, EventArgs e)
        {
            if (OnBKU != null)
                OnBKU();
        }

        private void cmdRegisterSPP_Click(object sender, EventArgs e)
        {
            if (OnRegisterSPP != null)
                OnRegisterSPP();
        }

        private void cmdbukutunai_Click(object sender, EventArgs e)
        {
            if (OnKasTunai != null)
                OnKasTunai();

        }

        private void cmdbukuBank_Click(object sender, EventArgs e)
        {
            if (OnKasBank != null)
                OnKasBank();
        }

        private void cmbBukuPajak_Click(object sender, EventArgs e)
        {
            if (OnKasBukuPajak != null)
            {
                OnKasBukuPajak();

            }
        }

        private void cmdRegisterSP2d_Click(object sender, EventArgs e)
        {
            if (OnRegisterSP2D != null)
            {
                OnRegisterSP2D();

            }
        }

        private void cmdlaporanbendahara_Click(object sender, EventArgs e)
        {
            panelLaporanBendaharaPengeluaran.Expand(0, 80);
            //pa .Expand(0, 80);
        }

        private void MenuBendahara_Load(object sender, EventArgs e)
        {

        }

        private void cmdTransaksi_Click(object sender, EventArgs e)
        {
            if (OnKontrak != null)
            {
                OnKontrak();
            }
        }

        private void cmdBAST_Click(object sender, EventArgs e)
        {
            if (OnBAST != null)
            {
                OnBAST();
            }
        }

        private void cmdSPP_Click(object sender, EventArgs e)
        {
            if (OnSPP != null)
            {
                OnSPP();
            }
        }

        private void cmdCatatBKUSP2D_Click(object sender, EventArgs e)
        {
            if (OnBKUSP2D !=null)
            {
                OnBKUSP2D();
            }
        }

        private void cmdSKR_Click(object sender, EventArgs e)
        {
            if (OnSKRSKPD != null)
            {
                OnSKRSKPD();
            }
        }

        private void cmdSTS_Click(object sender, EventArgs e)
        {
            if (OnSTS != null)
            {
                OnSTS();
            }
        }

        private void cmdSetor_Click(object sender, EventArgs e)
        {
            if (OnSetorkeKasda != null) {
                OnSetorkeKasda();
            }
        }

        private void cmdSPM_Click(object sender, EventArgs e)
        {
            if (OnSPM != null)
                OnSPM();
        }

        private void cmdSPJFungsional_Click(object sender, EventArgs e)
        {
            if (OnSPJ != null)
            {
                OnSPJ();
            }

        }

        private void cmdRegisterSPM_Click(object sender, EventArgs e)
        {
            if (OnRegisterSPM != null)
                OnRegisterSPM();

        }

        private void cmdProsesjurnalTransaksi_Click(object sender, EventArgs e)
        {
            if (OnJurnalTrx != null)
            {
                OnJurnalTrx();
            }
        }

        private void cmdInputJUrnalPenyesuaian_Click(object sender, EventArgs e)
        {
            if (OnJurnalPenyesuaian != null)
                OnJurnalPenyesuaian();

        }

        private void cmdJurnalManual_Click(object sender, EventArgs e)
        {
            if (OnJurnalManual != null)
            {
                OnJurnalManual();
            }
        }

        private void cmdPosting_Click(object sender, EventArgs e)
        {
            if (OnPosting != null)
            {
                OnPosting();
            }
        }

        private void cmdBendaharaPennerimaan_Click(object sender, EventArgs e)
        {
            panelBendaharaPenerimaan.Expand(0,5);

        }

        private void btnPPK_Click(object sender, EventArgs e)
        {
            panelPPK.Expand(0, 5);

        }

        private void cmdProsesAkuntansi_Click(object sender, EventArgs e)
        {
            panelProsesAkuntansi.Expand(0, 0);

        }

        private void cmdLaporanAKuntansi_Click(object sender, EventArgs e)
        {
            panelLaporanAkuntansi.Expand(0, 0);
        }

        private void cmdTrxBank_Click(object sender, EventArgs e)
        {
            if (OnTrxBank != null)
                OnTrxBank();
        }

        private void cmdPanjar_Click(object sender, EventArgs e)
        {
            if (OnPanjar != null)
            {
                OnPanjar();
            }
        }

        private void cmdPertanggungJawabanPanjar_Click(object sender, EventArgs e)
        {
            if (OnPanjar != null)
            {
                OnPanjar();
            }
        }

        private void cmdPengeluaran_Click(object sender, EventArgs e)
        {
            if (OnBPK != null)
                OnBPK();
        }

        private void cmdPengembalianBelanja_Click(object sender, EventArgs e)
        {
            if (OnPengembalianBelanja != null)
                OnPengembalianBelanja();
        }

        private void cmdSetorPajak_Click(object sender, EventArgs e)
        {
            if (OnSetorPajak != null)
                OnSetorPajak();
        }

        private void cmdKoreksi_Click(object sender, EventArgs e)
        {
            if (OnKoreksi != null)
            {
                OnKoreksi();
            }
        }

        private void cmdSPJUP_Click(object sender, EventArgs e)
        {
            if (OnSPJUP != null)
                OnSPJUP();

        }



    }
}
