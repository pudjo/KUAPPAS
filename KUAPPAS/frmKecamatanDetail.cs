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
    public partial class frmKecamatanDetail : Form
    {
        private Kecamatan m_oKecamatan;
        public frmKecamatanDetail()
        {
            InitializeComponent();
            m_oKecamatan = new Kecamatan();
            m_oKecamatan.ID = 0;
            ctrlNavigation1.ToAdd();

        }

        private void frmKecamatanDetail_Load(object sender, EventArgs e)
        {

            if (m_oKecamatan.ID == 0)
            ctrlNavigation1.ToAdd();
        }

        public bool SetID(int _pID)
        {
            KecamatanLogic oLogic = new KecamatanLogic(GlobalVar.TahunAnggaran);
            m_oKecamatan = oLogic.GetByID(1, _pID);
            if (m_oKecamatan != null)
            {

                txtKode.Text = m_oKecamatan.Kode.ToString();
                txtNama.Text = m_oKecamatan.Nama;
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
            m_oKecamatan.ID = 0;
            txtKode.Text = "0";
            txtNama.Text = "";

            
            return lRet;

            
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {

            EventResponseMessage lRet = new EventResponseMessage();
            KecamatanLogic oLogic = new KecamatanLogic(GlobalVar.TahunAnggaran);
            if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Penghapusan Kecamatan", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool bRet = oLogic.Hapus(m_oKecamatan.ID);
                lRet.ResponseStatus = bRet;
                if (bRet == false)
                {
                    MessageBox.Show(oLogic.LastError());

                }
                else
                {
                    m_oKecamatan.ID = 0;

                    MessageBox.Show("Kecamatan sudah hapus");
                    ctrlNavigation1.ToAdd();

                }
            }
            return lRet;

            
        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            KecamatanLogic oLogic = new KecamatanLogic(GlobalVar.TahunAnggaran);
            
            m_oKecamatan.Nama = txtNama.Text;

            //m_oKecamatan.Kode = DataFormat.GetInteger(txtKode.Text);
            bool bRet = oLogic.Simpan( ref m_oKecamatan);
            if (bRet == false)
            {
                MessageBox.Show(oLogic.LastError());
                lRet.ResponseStatus = false;
            }
            return lRet;
            
        }

    }
}
