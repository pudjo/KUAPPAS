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
    public partial class ctrlKegiatan : UserControl
    {

        public delegate void ValueChangedEventHandler(int pID);
        public event ValueChangedEventHandler OnChanged;
        private int m_SelectedID;
        private int m_iDinas;
        private int m_iUrusan;
        private int m_IDProgram;
        private int mprofile;
        List<ListItemData> arrKegiatan;
        bool OnCreate;

        public ctrlKegiatan()
        {
            InitializeComponent();
            arrKegiatan = new List<ListItemData>();
            OnCreate = false;
            mprofile = 2;
        }

        public int Profile
        {
            set { mprofile = value; }
        }
        private void cmbKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnCreate == false)
            {
                GetID();
                FireChangeEvent();
            }


        }
        public void Create(int Tahun, int dinas, int urusan, int program)
        {
            try
            {
                cmbKegiatan.Items.Clear();
                OnCreate = true;
                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);


                arrKegiatan.Clear();// Add(p.Nama);
                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgram(Tahun, dinas, urusan, program);

                if (_lst != null)
                {
                    foreach (TKegiatanAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                        arrKegiatan.Add(new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan));
                    }
                }
                OnCreate = false;
              
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void CreateFromRenja(int Tahun, int dinas, int urusan, int program)
        {
            try
            {
                cmbKegiatan.Items.Clear();
                TKegiatanLogic oLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran, mprofile);
                List<TKegiatan> _lst = new List<TKegiatan>();
                _lst = oLogic.GetByDinasAndUrusanAndIDProgramDrRenja(dinas, urusan, program);

                if (_lst != null)
                {
                    foreach (TKegiatan p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
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

            } else 
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
                    cmbKegiatan.SelectedIndex = i;
                    cmbKegiatan.SelectedItem = li;
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

        public int KodeKategori()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length >= 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(0, 1));
            }
            else return 0;

        }
        public int KodeUrusan()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length == 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(1, 2));
            }
            else return 0;

        }
        public int KodeProgram()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length > 3)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(3, 2));
            }
            else return 0;

        }
        public int KodeKegiatan()
        {
            if (m_SelectedID == 0)
                GetID();
            if (m_SelectedID.ToString().Length > 5)
            {
                return DataFormat.GetInteger(m_SelectedID.ToString().Substring(5, 3));
            }
            else return 0;

        }
        public bool CreateMaster(int programID)
        {
            try
            {
                //cmbKegiatan.Items.Clear();
                cmbKegiatan.DataSource = null;

                int iUrusan = 0;
                iUrusan = programID<1000?0:programID/100; 
                TKegiatanLogic oLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran,mprofile);
                List<TKegiatan> _lst = new List<TKegiatan>();
                _lst = oLogic.GetByFormMaster(iUrusan, programID);
                arrKegiatan.Clear();
                if (_lst != null)
                {
                    foreach (TKegiatan p in _lst)
                    {
                        //ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        //cmbKegiatan.Items.Add(item);
                        arrKegiatan.Add(new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan));
                                            
                    }
                    cmbKegiatan.DataSource = arrKegiatan;
                    cmbKegiatan.SelectedIndex = -1;

                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        private void ctrlKegiatan_Load(object sender, EventArgs e)
        {

        }

        private void cmbKegiatan_TextUpdate(object sender, EventArgs e)
        {
            string filter_param = cmbKegiatan.Text;
            if (arrKegiatan.Count < 1)
                return;

            List<ListItemData> filteredItems = arrKegiatan.FindAll(x => x.ItemText.Contains(filter_param));
            // another variant for filtering using StartsWith:
            // List<string> filteredItems = arrProjectList.FindAll(x => x.StartsWith(filter_param));

            cmbKegiatan.DataSource = filteredItems;

            if (String.IsNullOrWhiteSpace(filter_param))
            {
                cmbKegiatan.DataSource = arrKegiatan;
            }
            cmbKegiatan.DroppedDown = true;

            // this will ensure that the drop down is as long as the list
            cmbKegiatan.IntegralHeight = true;

            // remove automatically selected first item
            cmbKegiatan.SelectedIndex = -1;

            cmbKegiatan.Text = filter_param;

            // set the position of the cursor
            cmbKegiatan.SelectionStart = filter_param.Length;
            cmbKegiatan.SelectionLength = 0;   
        }

    }
}
