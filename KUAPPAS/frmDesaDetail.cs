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
    public partial class frmDesaDetail : Form
    {
        Desa m_oDesa;        
        public frmDesaDetail()
        {
            InitializeComponent();
            m_oDesa = new Desa();
        }

        private void frmDesaDetail_Load(object sender, EventArgs e)
        {
            if (m_oDesa.ID == 0)
                ctrlNavigation1.SetNew();

        }

        public bool SetID(int _pID)
        {
            DesaLogic oLogic = new DesaLogic(GlobalVar.TahunAnggaran);
            ctrlNavigation1.SetNew();
            ctrlKecamatan1.Create();
            
            m_oDesa = oLogic.GetByID(_pID);
            if (m_oDesa != null)
            {
                ctrlKecamatan1.SetID(m_oDesa.Kecamatan);
                txtKode.Text = m_oDesa.Kode.ToString();
                txtNama.Text = m_oDesa.Nama;
                return true;

            }
            else
            {
                MessageBox.Show(oLogic.LastError());
                return false;
            }

        }
        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            ctrlKecamatan1.Create();
            
            m_oDesa.ID = 0;
            txtKode.Text = "0";
            txtNama.Text = "";


            return lRet;


        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {

            EventResponseMessage lRet = new EventResponseMessage();
            DesaLogic oLogic = new DesaLogic(GlobalVar.TahunAnggaran);
            if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Penghapusan Desa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool bRet = oLogic.Hapus(m_oDesa.ID);
                lRet.ResponseStatus = bRet;
                if (bRet == false)
                {
                    MessageBox.Show(oLogic.LastError());

                }
                else
                {
                    m_oDesa.ID = 0;

                }
            }
            return lRet;


        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            DesaLogic oLogic = new DesaLogic(GlobalVar.TahunAnggaran);
            m_oDesa.Kecamatan = ctrlKecamatan1.GetID();
            m_oDesa.Nama = txtNama.Text;
            m_oDesa.Kode = DataFormat.GetInteger(txtKode.Text);
            bool bRet = oLogic.Simpan(ref m_oDesa);
            if (bRet == false)
            {
                MessageBox.Show(oLogic.LastError());
                lRet.ResponseStatus = false;
            }
            return lRet;

        }
    }
}
