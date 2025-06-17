using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP.Bendahara;
using DTO.Bendahara;
using Formatting;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlPilihanPotongan : UserControl
    {
        public int m_IDPotongan = 0;
        public int m_IDKodePusat = 0;
        public string  m_sNama;
        public Single  m_bInformasi ;

        private int lenKodePotongan = 0;
        public delegate void ValueChangedEventHandler(int pIDPotongan, int pIDKodePusat);
        public event ValueChangedEventHandler OnChanged;
        private int  m_SelectedID;
        

        public ctrlPilihanPotongan()
        {
            InitializeComponent();
        }

        private void cmbPotongan_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (OnChanged != null)
            {
                OnChanged(GetKodePotongan(),GetKodeMap());
            }
        }

        public void Create(int bInformasi)
        {

            PotonganLogic oLogic = new PotonganLogic(GlobalVar.TahunAnggaran);
            List<Potongan> lstPotongan = new List<Potongan>();
            lstPotongan = oLogic.Get();
            if (lstPotongan == null)
            {
                MessageBox.Show(oLogic.LastError());
                return;
            }

            foreach (Potongan p in lstPotongan)
                {
                    ListItemData li = new ListItemData(p.KodePusat.ToString() + "  " + p.Nama, p.KodePusat.ToString(), p.IDPotongan.ToString());
                    lenKodePotongan = p.IDPotongan.ToString().Trim().Length;
                    cmbPotongan.Items.Add(li);

                }

            
        }
        public string NamaPajak
        {
            get {
                ListItemData li = (ListItemData)cmbPotongan.SelectedItem;
                return li.ItemText.Substring(8).Trim();
            }
        }
        private int  GetKodePotongan()
        {
            if (cmbPotongan.SelectedIndex >= 0)
            {

                ListItemData li = (ListItemData)cmbPotongan.SelectedItem;
                return DataFormat.GetInteger(li.Kodetambahan);

            }
            else
            {
                return 0;
            }
        }
        public int GetKodeMap()
        {
            if (cmbPotongan.SelectedIndex >= 0)
            {

                ListItemData li = (ListItemData)cmbPotongan.SelectedItem;
                return DataFormat.GetInteger(li.Kode);

            }
            else
            {
                return 0;
            }
       }

        private void ctrlPilihanPotongan_Load(object sender, EventArgs e)
        {

        }
      
    }
}
