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
    public partial class frmTerimaSPM : Form
    {
        private int m_IDDInas;
        public frmTerimaSPM()
        {
            InitializeComponent();
            m_IDDInas = 0;
        }

        private void frmTerimaSPM_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            m_IDDInas = ctrlDinas1.GetID();
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDInas = pIDSKPD;
            LoadSPM();
        }
        private bool LoadSPM()
        {
            bool bRet = true;
            ctrlSPP1.Create(m_IDDInas, 0, 1);


            return bRet;
        }
    }
}
