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
    public partial class ctrlPanelPencarian : UserControl
    {
        public delegate void ValueChangedEventHandler();
        public event ValueChangedEventHandler OnDisplay;
        public event ValueChangedEventHandler OnAdd;
        public event ValueChangedEventHandler OnEdit;
        public event ValueChangedEventHandler OnDelete;
        public event ValueChangedEventHandler TanggalBerubah;
        public event ValueChangedEventHandler DinasBerubah;


        private bool bMustLoad;
        private int m_idDinas;
        private int mIKodeUK;
        public ctrlPanelPencarian()
        {
            InitializeComponent();
            bMustLoad = false;
            m_idDinas = 0;
            mIKodeUK = 0;
        }

        private void ctrlPanelPencarian_Load(object sender, EventArgs e)
        {

        }

        public void SetVisible(int idx, bool bVisible)
        {
            switch (idx)
            {
                case 1:
                    cmdTampilkan.Visible = bVisible;
                    break;
                case 2:
                    cmdtambah.Visible = bVisible;
                    break;
                case 3:
                    cmdUbah.Visible = bVisible;
                    break;

                case 4:
                    cmdHapus.Visible = bVisible;
                    break;

            }
        }
        public void SetCaption(int idx, string str)
        {
            switch (idx)
            {
                case 1:
                    cmdTampilkan.Text = str;
                    break;
                case 2:
                    cmdtambah.Text = str;
                    break;
                case 3:
                    cmdUbah.Text = str;
                    break;

                case 4:
                    cmdHapus.Text = str;
                    break;

            }
        }
        public void Create( int dinas=0)
        {
            ctrlDinas1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                m_idDinas = GlobalVar.Pengguna.SKPD;
                ctrlDinas1.SetID(m_idDinas);
            }
            else
            {
                m_idDinas = ctrlDinas1.GetID();
            }
         
            DateTime hariIni = DateTime.Now.Date;
            DateTime tanggalAwalBulan = new DateTime(GlobalVar.TahunAnggaran, hariIni.Month, 1);

            dtAwal.Value = tanggalAwalBulan;// new DateTime(GlobalVar.TahunAnggaran, hariIni.Month, 1);
            dtAkhir.Value = hariIni;
           
        }
        public bool MustLoad
        {
            set { bMustLoad = value; } 
        }
        public int Dinas
        {
            
            get {
                //m_idDinas = ctrlDinas1.GetIDSKPD();
                return m_idDinas; 
            }
        }
        public int UnitKerja
        {

            get
            {
                 return mIKodeUK ;

            }
        }

        public DateTime TanggalAwal
        {
            get { return dtAwal.Value; }
            set
            {
                dtAwal.Value = value;
                
            }
        }
        public DateTime TanggalAkhir
        {
            get { return dtAkhir.Value ; }
            set
            {
                dtAkhir.Value = value;
            }
        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            if (OnDisplay != null)
            {
                OnDisplay();
            }
        }

        private void cmdtambah_Click(object sender, EventArgs e)
        {
            if (OnAdd!= null)
            {
                OnAdd();
            }
        }

        private void cmdUbah_Click(object sender, EventArgs e)
        {
            if (OnEdit != null)
            {
                OnEdit();
            }
        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            if (OnDelete != null)
            {
                OnDelete();
            }
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
              m_idDinas=pIDSKPD;
              mIKodeUK = pIDUK;
              if (DinasBerubah != null)
              {
                  DinasBerubah();
              }
        }

        private void dtAwal_ValueChanged(object sender, EventArgs e)
        {
            if (TanggalBerubah != null){
                TanggalBerubah();
            }
        }

        private void dtAkhir_ValueChanged(object sender, EventArgs e)
        {
            if (TanggalBerubah != null)
            {
                TanggalBerubah();
            }
        }

    }
}
