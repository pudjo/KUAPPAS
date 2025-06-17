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


namespace KUAPPAS
{
    public partial class frmPerbaikiKodeProgramKegiatan : Form
    {
        private int m_iDinas;
        private int m_iTahun;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private int m_IDUrusan;
        private decimal m_dPagu = 0L;

        public frmPerbaikiKodeProgramKegiatan()
        {
            InitializeComponent();

            m_iDinas = 0;
            m_iTahun = (int)GlobalVar.TahunAnggaran;
        }

        private void ctrlKegiatanAPBD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {

            m_iDinas = pIDSKPD;
            ctrlUrusan1.Create();

            treeProgramKegiatan1.Create(m_iDinas, 0);
            ctrlUrusanPemerintahan1.Create(m_iDinas, m_iTahun);
        
        }

        private void frmPerbaikiKodeProgramKegiatan_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();

        }

        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            ctrlProgram1.Create(m_iTahun, m_iDinas, ctrlUrusanPemerintahan1.GetID());

        }

        private void ctrlProgram1_OnChanged(int pID)
        {
            ctrlKegiatanAPBD2.Create(m_iTahun, m_iDinas, ctrlUrusanPemerintahan1.GetID(), ctrlProgram1.GetID());

          //  .Create(m_iTahun, m_iDinas, ctrlUrusanPemerintahan1.GetID());
        }

        private void cmdPerbaiki_Click(object sender, EventArgs e)
        {
            int iDProgramLama = m_IDProgram;
            int idUrusanLama = m_IDUrusan;
            int idUrusanBaru = ctrlUrusanPemerintahan1.GetID();
            long idKegiatanLama = m_IDKegiatan;
            int iDProgramBaru = ctrlProgram1.GetID();
            int idKegiatanBaru = ctrlKegiatanAPBD2.GetID();
            KUALogic oLogic = new KUALogic(m_iTahun);
            if (oLogic.PerbaikiKodeProgram(m_iDinas, m_IDUrusan, iDProgramLama, idKegiatanLama, idUrusanBaru, iDProgramBaru, idKegiatanBaru)== true )
                MessageBox.Show("Slesai"); 
            else
                MessageBox.Show("Gagal"); 





        }

        private EventResponseMessage treeProgramKegiatan1_KegiatanChanged(int ID)
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            m_IDKegiatan = ID;
            m_IDUrusan = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 3));
            m_IDProgram = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 5));
            Urusan oUrusan = new Urusan();
            UrusanLogic oUrusanLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
            oUrusan = oUrusanLogic.GetByID(m_IDUrusan);
    
            lblUrusan.Text = oUrusan.Tampilan + " " + oUrusan.Nama;
            TProgramAPBD oPAPBD = new TProgramAPBD();
            TProgramAPBDLogic pLogic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);
            oPAPBD = pLogic.GetByID(GlobalVar.TahunAnggaran, m_IDUrusan, m_iDinas, m_IDProgram);
            if (oPAPBD != null)
            {

                lblProgram.Text = "";
                lblProgram.Text = oPAPBD.KodeProgram.ToString() + " " + oPAPBD.Nama;
            }


            //TKegiatanLogic oKLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran);
            TKegiatanAPBDLogic oKAPBDLOgic = new TKegiatanAPBDLogic((int)GlobalVar.TahunAnggaran);
            TKegiatanAPBD oKegiatan = new TKegiatanAPBD();
            oKegiatan = oKAPBDLOgic.GetKegiatan((int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, 3, 2);

            if (oKegiatan == null)
            {
                MessageBox.Show(oKAPBDLOgic.LastError());
                ret.ResponseStatus = false;
                return ret;

            }



            return ret;
        }

        private void ctrlUrusan1_OnChanged(int pID)
        {
           
        }
    }
}
