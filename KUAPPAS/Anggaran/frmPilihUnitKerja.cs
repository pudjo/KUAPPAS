using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Anggaran
{
    public partial class frmPilihUnitKerja : Form
    {
        int m_IDDinas;
        long m_IDSubKegitan;
        private bool m_bOK;
        int m_iKodeUK;
        public frmPilihUnitKerja()
        {
            InitializeComponent();
            m_IDDinas=0;
            m_bOK = false;
            m_iKodeUK = 0;
        }

        public int Dinas
        {
            set
            {
                m_IDDinas = value;

            }
        }
        public string NamaUnit
        {
            get
            {
                return ctrlUnitKerja1.GetNamaUnit();
            }
        }
        public long IdSubKegiatan
        {
            set
            {
                m_IDSubKegitan = value;
            }
        }
        private void frmPilihUnitKerja_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Pili Unit Kerja");
            ctrlUnitKerja1.Create(m_IDDinas);
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            m_bOK = true;
            this.Hide();
        }
        public bool OK
        {
            get
            {
                m_iKodeUK = ctrlUnitKerja1.GetID();
                return m_bOK;
            }
        }
        
        public int KodeUK
        {
            get
            {
                return m_iKodeUK;
            }
        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            
            m_bOK = false ;
            this.Hide();
        }
    }
}
