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

namespace KUAPPAS.Bendahara
{
    public partial class ctrlKodeSetor : UserControl
    {
        public delegate void ValueChangedEventHandler(RefPajak refPajak);
        public event ValueChangedEventHandler OnChanged;
        private RefPajak mRefPajak;
        private int m_iKodeMap;
        private int m_SelectedID;
        public ctrlKodeSetor()
        {
            InitializeComponent();
            mRefPajak = new RefPajak();
            m_iKodeMap = 0;
        }

        private void KodeSetor_Load(object sender, EventArgs e)
        {

        }
        public int KodeMap
        {
            set
            {
                m_iKodeMap=value;
            }
        }

        public void Create(int KodeMap)
        {

            //PotonganLogic oLogic = new PotonganLogic(GlobalVar.TahunAnggaran);

            //List<KodeSetor> lstKodeSetor = new List<KodeSetor>();
            //lstKodeSetor = oLogic.GetKodeSetor(KodeMap);

            //if (lstKodeSetor == null)
            //{
            //    MessageBox.Show(oLogic.LastError());
            //    return;
            //}

            //foreach (KodeSetor p in lstKodeSetor)
            //{
            //    ListItemData li = new ListItemData(p.Kode  + "  " + p.NamaSetor, p.Kode);
            //    cmbKodeSetor.Items.Add(li);

            //}

            if (GlobalVar.gListRefPajak == null)
            {
                RefPajakLogic oKSLogic = new RefPajakLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListRefPajak = oKSLogic.Get();

            }
            List<RefPajak> lst =GlobalVar.gListRefPajak.FindAll(x => x.kd_map == KodeMap);
            cmbKodeSetor.Items.Clear();
            foreach (RefPajak p in lst)
            {
                ListItemData li = new ListItemData( p.kd_setor.ToString() + "  " + p.desc_setor, p.kd_setor);
                cmbKodeSetor.Items.Add(li);

            }
        }
        public string GetKode()
        {
            if (cmbKodeSetor.SelectedIndex >= 0)
            {
                ListItemData li = (ListItemData)cmbKodeSetor.SelectedItem;
                return li.Itemdata.ToString();//.Kode;
            }
            else return "";


        }
        public int GetID()
        {
            m_SelectedID = 0;
            for (int i = 0; i < cmbKodeSetor .Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbKodeSetor.Items[i];
                if (li.ItemText == cmbKodeSetor.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;

        }
        public void Clear()
        {
            cmbKodeSetor.Text = "";
        }
        private void cmbKodeSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kodesetor = GetID();
            mRefPajak = GlobalVar.gListRefPajak.FirstOrDefault(x => x.kd_map == m_iKodeMap && x.kd_setor == kodesetor);
            if (mRefPajak != null)
            {
                if (OnChanged != null)
                {
                    OnChanged(mRefPajak);
                }
            }
        }
    }
}
