using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;
using Formatting;
namespace KUAPPAS
{
    public partial class frmUK : Form
    {
        private int m_ID;
        private int mProfile;
        private List<Unit> mListUnit = new List<Unit>();
        public frmUK()
        {
            InitializeComponent();
            m_ID = 0;
            mProfile = 3;
        }
        public List<Unit> UnitYangSudahAda
        {
            set
            {
                mListUnit = value;
            }

        }
        public int Prorfile
        {
            set { mProfile = value; }
        }
        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage lRet = new EventResponseMessage();

            lRet.ResponseStatus = true;
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            txtNama.Text = "";
            txtKode.Text = "0";
            m_ID = 0;

            return lRet;

            
        }
        private bool CekInput(){

            foreach(Unit u in mListUnit){
                if (txtNama.Text.Trim() == u.Nama.Trim())
                {
                    MessageBox.Show("Nama Sudah ada... ");
                    return false;
                }
                if (u.Kode == DataFormat.GetInteger(txtKode.Text.Trim()))
                {
                    MessageBox.Show("Kode sudah dipakai ada... ");
                    return false;
                }
            }
            return true;

        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            try
            {
           
                lRet.ResponseStatus = true;
                UnitKerjaLogic oLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                Unit oUnit = new Unit();

                oUnit.ID = m_ID;

                oUnit.Nama = txtNama.Text;
                oUnit.SKPD = ctrlSKPD1.GetID();
                oUnit.Kode = DataFormat.GetInteger(txtKode.Text);

                if (oLogic.Simpan(ref oUnit) == false)
                {
                    MessageBox.Show(oLogic.LastError());
                    lRet.ResponseStatus = false;
                }
                else
                {
                    m_ID = oUnit.ID;
                    // MessageBox.Show("Penyimpanan Berhasil.");

                }

                return lRet;
            }
            catch (Exception ex)
            {
                lRet.ResponseStatus = false;
                MessageBox.Show(ex.Message);
                return lRet;

            }
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }
        public void SetNew()
        {
            ctrlNavigation1.SetNew();
        }
        public void SetID(int _pID)
        {
            ctrlSKPD1.Create(GlobalVar.Pengguna.SKPD);
            UnitKerjaLogic oUKLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
            Unit oUnit = new Unit();
            oUnit = oUKLogic.GetByID(_pID);
            if (oUnit != null)
            {
                ctrlSKPD1.SetID(oUnit.SKPD);
                txtKode.Text = oUnit.Kode.ToString();
                txtNama.Text = oUnit.Nama;
                m_ID = _pID;

               
            }
            else
            {
                MessageBox.Show(oUKLogic.LastError());
            }
        }
        private void frmUK_Load(object sender, EventArgs e)
        {
        }
        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmdSImpanUrusanDinas_Click(object sender, EventArgs e)
        {
            
        
        }

        private void chkYangDipilih_CheckedChanged(object sender, EventArgs e)
        {
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage lRet = new EventResponseMessage();
            try
            {

                if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lRet.ResponseStatus = true;
                    UnitKerjaLogic oLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                    Unit oUnit = new Unit();

                    oUnit.ID = m_ID;

                    oUnit.Nama = txtNama.Text;
                    oUnit.SKPD = ctrlSKPD1.GetID();
                    oUnit.Kode = DataFormat.GetInteger(txtKode.Text);

                    if (oLogic.Hapus(m_ID) == false)
                    {
                        MessageBox.Show(oLogic.LastError());
                        lRet.ResponseStatus = false;
                    }
                    else
                    {
                        MessageBox.Show("Data Unit Kerja Sudah dihapus.");
                        SetNew();
                    }
                }

                return lRet;
            }
            catch (Exception ex)
            {
                lRet.ResponseStatus = false;
                MessageBox.Show(ex.Message);
                return lRet;

            }
        }
        
        
    }
}
