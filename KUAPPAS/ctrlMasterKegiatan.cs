using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using Formatting;
using BP;

namespace KUAPPAS
{
    public partial class ctrlMasterKegiatan : UserControl
    {
        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int m_iDinas;
        private int m_iUrusan;
        private int m_IDProgram;
        List<ListItemData> arrKegiatan;
        bool OnCreate;

        public ctrlMasterKegiatan()
        {
            InitializeComponent();
            arrKegiatan = new List<ListItemData>();
            OnCreate = false;
        }

        private void ctrlMasterKegiatan_Load(object sender, EventArgs e)
        {

        }
        public void Create(int Tahun, int dinas, int urusan, int program)
        {
            try
            {
                cmbKegiatan.Items.Clear();
                OnCreate = true;
                MasterKegiatanLogic oLOgic = new MasterKegiatanLogic(GlobalVar.TahunAnggaran);


                arrKegiatan.Clear();// Add(p.Nama);
                List<MasterKegiatan> _lst = new List<MasterKegiatan>();
                _lst = oLOgic.GetByProgram( program );

                if (_lst != null)
                {
                    foreach (MasterKegiatan p in _lst)
                    {
                        ListItemData item = new ListItemData(p.ID.ToSimpleKodeKegiatan() + " " + p.Nama, p.ID );
                        cmbKegiatan.Items.Add(item);
                        arrKegiatan.Add(new ListItemData(p.ID.ToSimpleKodeKegiatan() + " " + p.Nama, p.ID));
                    }
                }
                OnCreate = false;

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void Clear()
        {
            if (cmbKegiatan.DataSource != null)
            {
                cmbKegiatan.DataSource = null;

            }
            else
                cmbKegiatan.Items.Clear();
            cmbKegiatan.SelectedIndex = -1;

        }
        public string NamaKegiatan()
        {
            if (cmbKegiatan.Text.Length > 4)
            {
                return cmbKegiatan.Text.Substring(4);
            }
            else
                return "";
        }
        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbKegiatan.SelectedItem;
            if (cmbKegiatan.SelectedIndex < 0)
                return 0;
            m_SelectedID = 0;
            for (int i = 0; i < cmbKegiatan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbKegiatan.Items[i];
                if (li.ItemText == cmbKegiatan.Text)
                {
                    m_SelectedID = li.Itemdata;
                    break;
                }

            }
            return m_SelectedID;
        }
        public void SetID(int pID)
        {
            int i;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbKegiatan.Items.Count; i++)
            {
                li = (ListItemData)cmbKegiatan.Items[i];
                if (li.Itemdata == Convert.ToInt32(pID))
                {
                    cmbKegiatan.SelectedItem = li;
                    cmbKegiatan.SelectedIndex = i;
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                OnChanged(m_SelectedID);
            }
        }
        public int GetSelectedID()
        {
            return m_SelectedID;
        }
    }
}
