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
    public partial class ctrlSumberDanaLengkap : UserControl
    {
        private int m_iSumberDana;
        private int m_iSubSumberDana;

        public ctrlSumberDanaLengkap()
        {
            InitializeComponent();
        }

        private void ctrlSumberDanaLengkap_Load(object sender, EventArgs e)
        {

        }
        public void Create()
        {
            ctrlSumberDana1.Create();
        }

        private void ctrlSumberDana1_OnChanged(int pID)
        {
            m_iSumberDana = pID;
            ctrlSubSumberDana1.Create(pID);
        }

        private void ctrlSubSumberDana1_OnChanged(int pID)
        {
            m_iSubSumberDana = pID;
        }
        public int GetSumberDana (){
            return m_iSumberDana;
        }
        public int GetSubSumberDana()
        {
            return m_iSubSumberDana;
        }
        public string GetKeterangan()
        {
            return txtKeterangan.Text;
        }
        public void SetID(int SD, int SSD, string sKeterangan)
        {
            ctrlSumberDana1.Create();
            ctrlSumberDana1.SetID(SD);
            m_iSumberDana = SD;
            ctrlSubSumberDana1.Create(SD);
            ctrlSubSumberDana1.SetID(SSD);
            m_iSumberDana = SSD;
            txtKeterangan.Text = sKeterangan;

        }


    }
}
