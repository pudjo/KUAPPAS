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
    public partial class frmDusunDetail : Form
    {
        Dusun m_oDusun;
        public frmDusunDetail()
        {
            InitializeComponent();
            m_oDusun = new Dusun();

        }

        private void frmDusunDetail_Load(object sender, EventArgs e)
        {
            if (m_oDusun.ID == 0)
                ctrlNavigation1.SetNew();

        }
        public bool SetID(int _pID)
        {
            DusunLogic oLogic = new DusunLogic(GlobalVar.TahunAnggaran);
            ctrlKecamatan1.Create();
            m_oDusun = oLogic.GetByID(1, _pID);
            if (m_oDusun != null)
            {

                ctrlKecamatan1.SetID(m_oDusun.Kecamatan);
                ctrlDesa1.Create(m_oDusun.Kecamatan);
                ctrlDesa1.SetID(m_oDusun.Desa);

                txtKode.Text = m_oDusun.Kode.ToString();
                txtNama.Text = m_oDusun.Nama;
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

            m_oDusun.ID = 0;
            txtKode.Text = "0";
            txtNama.Text = "";


            return lRet;

        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            DusunLogic oLogic = new DusunLogic(GlobalVar.TahunAnggaran);
            if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Penghapusan Dusun", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool bRet = oLogic.Hapus(m_oDusun.ID);
                lRet.ResponseStatus = bRet;
                if (bRet == false)
                {
                    MessageBox.Show(oLogic.LastError());

                }
                else
                {
                    m_oDusun.ID = 0;

                }
            }
            return lRet;
        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            DusunLogic oLogic = new DusunLogic(GlobalVar.TahunAnggaran);
            m_oDusun.Kecamatan = ctrlKecamatan1.GetID();
            m_oDusun.Desa = ctrlDesa1.GetID();
            m_oDusun.Nama = txtNama.Text;
            m_oDusun.Kode = DataFormat.GetInteger(txtKode.Text);
            bool bRet = oLogic.Simpan(ref m_oDusun);
            if (bRet == false)
            {
                MessageBox.Show(oLogic.LastError());
                lRet.ResponseStatus = false;
            }
            return lRet;
        }

        private void ctrlKecamatan1_OnChanged(int pID)
        {
            ctrlDesa1.Create(pID);
        }

        private void ctrlDesa1_Load(object sender, EventArgs e)
        {

        }
    }
}
