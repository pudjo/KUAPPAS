using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmSPDdanRealissasi : Form
    {
        private int m_IDDINas;
        private int m_iKodeUk;
        private long m_iNoUrut;
        private int m_iJenisBelanja;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private long m_IDSUbKegiatan;
         private long m_IDRekening;
        private int m_UnitAnggaran;
        private int m_iTahapANggaaran;
        public frmSPDdanRealissasi()
        {
            InitializeComponent();
        }

        private void frmSPDdanRealissasi_Load(object sender, EventArgs e)
        {

        }
        public int Dinas
        {
            set
            {
                m_IDDINas = value;
            }

        }
        public void SetProgramKegiatan(
            int pDinas, 
            int UnitAnggaran,
            int IDUrusan, 
            int IDProgram, 
            int IDKegiatan, 
            long IDSUbKegiatan,
            long pIDRekening

            )
        {
            ctrlProgramKegiatan1.Create(pDinas, UnitAnggaran);
            m_IDDINas= pDinas; 
            m_UnitAnggaran= UnitAnggaran;
            ctrlDinas1.Create();
            ctrlDinas1.SetID(m_IDDINas, m_UnitAnggaran);
            m_IDUrusan=IDUrusan; 
            m_IDProgram=IDProgram;
            m_IDKegiatan=IDKegiatan;
            m_IDSUbKegiatan = IDSUbKegiatan;
            m_IDRekening = pIDRekening;

            ctrlProgramKegiatan1.SetValue(m_IDDINas, m_UnitAnggaran, m_IDUrusan,
                m_IDProgram, m_IDKegiatan, m_IDSUbKegiatan);//IDSUbKegiatan);
            ctrlPilihanRekeningAnggaran1.Create(m_IDDINas,
                                       m_IDUrusan,
                                       m_IDProgram,
                                       m_IDKegiatan,
                                       m_IDSUbKegiatan, m_UnitAnggaran, 2, 3);
            ctrlPilihanRekeningAnggaran1.SetID(pIDRekening);
  
        }
    }
}
