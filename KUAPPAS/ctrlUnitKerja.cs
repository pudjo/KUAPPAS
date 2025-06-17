using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;


namespace KUAPPAS
{
    public partial class ctrlUnitKerja : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;

        private int m_SelectedID;
        private int m_IdSKPD;
        private bool bOnLoad;
        public ctrlUnitKerja()
        {
            InitializeComponent();
            bOnLoad = false;
        }

        private void ctrlUnitKerja_Load(object sender, EventArgs e)
        {

        }
        private void cmbUnitKerja_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        public int Create(int _pSKPD)
        {
            try
            {
                m_IdSKPD = _pSKPD;
                cmbUnitKerja.Items.Clear();
               

                UnitKerjaLogic o = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListOrganisasi = new List<Unit>();
                GlobalVar.gListOrganisasi = o.GetBySKPD(_pSKPD);
                
                if (GlobalVar.gListOrganisasi.Count > 0)
                {
                    ListItemData item = new ListItemData("  " + "Semua Bagian/Unit", 0,null);
                    cmbUnitKerja.Items.Add(item);
                }

                foreach (Unit p in GlobalVar.gListOrganisasi)
                {
                    ListItemData item = new ListItemData(p.KodeSIPD  + "  " + p.Nama, p.Kode,(Unit)p );
                    cmbUnitKerja.Items.Add(item);
                }
            
                return cmbUnitKerja.Items.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }

        }
        public int CreateWitAll(int _pSKPD)
        {
            try
            {
                cmbUnitKerja.Items.Clear();
                m_IdSKPD = _pSKPD;
               
             
              
                UnitKerjaLogic o = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
                GlobalVar.gListOrganisasi = new List<Unit>();
                GlobalVar.gListOrganisasi = o.GetBySKPD(_pSKPD);
                
                ListItemData _item = new ListItemData("Semua Uit Kerja", 0,null);
                cmbUnitKerja.Items.Add(_item);

                foreach (Unit p in GlobalVar.gListOrganisasi)
                {
                    ListItemData item = new ListItemData(p.Tampilan, p.ID,p);
                    cmbUnitKerja.Items.Add(item);
                }
              //  lblUK.Visible =cmbUnitKerja.Items.Count == 0?false:true;
                cmbUnitKerja.Visible = cmbUnitKerja.Items.Count == 0 ? false : true;
                return cmbUnitKerja.Items.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }

        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbUnitKerja.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbUnitKerja.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbUnitKerja.Items[i];
                if (li.ItemText == cmbUnitKerja.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }

            return m_SelectedID;
        }

        public Unit Unit {
           
             get{
                  ListItemData li = new ListItemData("", 0);

                  for (int i = 0; i < cmbUnitKerja.Items.Count; i++)
                  {
                      
                      li = (ListItemData)cmbUnitKerja.Items[i];
                      if (li.ItemText == cmbUnitKerja.Text)
                      {
                          int id = li.Itemdata;
                          Unit oUnit = GlobalVar.gListOrganisasi.FirstOrDefault(x => x.Kode == id);
                          return oUnit;
                      
                      }
                  }
                  return null;
            }
        }
        public void SetID(int pID)
        {
            int i;
            bOnLoad = true;
            m_SelectedID = pID;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbUnitKerja.Items.Count; i++)
            {
                li = (ListItemData)cmbUnitKerja.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbUnitKerja.SelectedIndex = i;
                    break;
                }
            }
            bOnLoad = false;
        }
        private void FireChangeEvent()
        {
            if (bOnLoad == false)
            {
                GetID();
                if (OnChanged != null)
                {
                    OnChanged(m_SelectedID);
                }

            }
        }
        public string GetNamaUnit()
        {
            return cmbUnitKerja.Text.Trim();
        }
        public int GetSelectedID()
        {
            return m_SelectedID;
        }
        private void cmbUnitKerja_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        public string Nama
        {
            get
            {
                return cmbUnitKerja.Text.Trim();
            }
        }
        private void ctrlUnitKerja_Resize(object sender, EventArgs e)
        {
            this.Height = cmbUnitKerja.Height;
        }
        public Pejabat GetPPK(DateTime date)
        {
            Pejabat ppk = new Pejabat();
            GetID();
            try
            {
                PejabatLogic oLogic = new PejabatLogic((int)GlobalVar.TahunAnggaran);
                ppk = oLogic.GetPPKSKPD(m_IdSKPD,0, date,m_SelectedID);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                ppk.Nama = "";
                ppk.Jabatan = "";
                ppk.NIP = "";
                ppk.NoRekening = "";
                ppk.NPWP = "";
                ppk.NamaBank = "123";


            }
            return ppk;
        }
    }
}
