using BP;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmCekSPDRealisasi : Form
    {
        private long m_IDSUbKegiatan;
        private int  m_IDKegiatan;
        private int  m_IDProgram ;
        private int m_IDDInas;
        private long m_noUrutSPD;

        private int m_KodeUK;

        private long m_IDRekening;
        private DateTime m_Tanggal;

        public frmCekSPDRealisasi()
        {
            InitializeComponent();
        }

        private void frmCekSPDRealisasi_Load(object sender, EventArgs e)
        {

        }
        public long IdSubKegiatan
        {
            set
            {
                m_IDSUbKegiatan = value;

            }
        }
        public int IDKegiatan
        {
            set
            {
                m_IDKegiatan = value;
            }
        }
        public int IDProgram
        {
            set
            {
                m_IDProgram = value;

            }
        }
        public int IDDInas
        {
            set
            {
                m_IDDInas = value;

            }
        }
        public long NoUrutSPD
        {
            set
            {
                m_noUrutSPD = value;

            }
        }
        public DateTime Tanggal
        {
            set
            {
                m_Tanggal = value;

            }
        }

        public long IDrekening
        {
            set
            {
                m_IDRekening = value;
            }
        }
        public int KodeUK
        {
            set
            {
                m_KodeUK = value;
            }
        }
        private void LoadData()
        {
            SPDLogic oSPDLogic = new SPDLogic((int)GlobalVar.TahunAnggaran);
            List<SPDDetail> lstSPD = oSPDLogic.GetDetailSebelumNoUrutEx(
                m_noUrutSPD,m_IDDInas,m_IDSUbKegiatan,m_KodeUK);

            RealisasiLogic oRLogic = new RealisasiLogic((int)GlobalVar.TahunAnggaran);
            List<Realisasi> lstRealisasi = new List<Realisasi>();

            lstRealisasi = oRLogic.Get(m_IDDInas,m_KodeUK,m_IDSUbKegiatan,m_Tanggal, 0,0);



        }
    }
}
