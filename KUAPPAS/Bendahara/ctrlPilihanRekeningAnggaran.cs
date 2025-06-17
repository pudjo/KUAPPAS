using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;
using Formatting;
using BP;
using BP.Bendahara;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlPilihanRekeningAnggaran : UserControl
    {
        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<TAnggaranRekening> _lst;
        private int m_idDinas;
        private int m_idUrusan;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private long m_idSubKegiatan;
        private int m_jenis;
        private int m_ppkd;
        private int m_iKodeUK;
      
        public ctrlPilihanRekeningAnggaran()
        {
            InitializeComponent();
            _lst = new List<TAnggaranRekening>();
        }

        private void ctrlComboAnggaran_Load(object sender, EventArgs e)
        {

        }
        public void Create(int idDinas, int IDUrusan, int IDProgram, int IDKegiatan,long IDSUbKegiatan,int kodeUK ,int tahap,int jenis = 3)
        {
            cmbRekening.Items.Clear();
            m_idDinas=idDinas;
            m_idUrusan=IDUrusan;
            m_IDProgram=IDProgram;
            m_IDKegiatan=IDKegiatan;
            m_idSubKegiatan = IDSUbKegiatan;
            m_iKodeUK = kodeUK;

            m_jenis=jenis;
          

        
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic((int)GlobalVar.TahunAnggaran);
            int idUrusan=IDUrusan;
            if (idDinas == 0)
            {
                return;
            }
            if (IDProgram == 0 )
                idUrusan = DataFormat.GetInteger(DataFormat.GetString(idDinas).Substring(0, 3));
            _lst = new List<TAnggaranRekening>();
         
            _lst = oLogic.GetfromList(
                                         GlobalVar.TahunAnggaran, 
                                         m_idDinas, 
                                         m_iKodeUK,
                                         m_idUrusan, 
                                         m_IDProgram, 
                                         m_IDKegiatan,
                                         m_idSubKegiatan,
                                         (int)jenis ,
                                         tahap);

            if (_lst != null)
            {
                foreach (TAnggaranRekening ta in _lst)
                {

                    ListItemData item = new ListItemData(ta.IDRekening.ToString() + " -  " + ta.Nama , ta.IDRekening);
                    cmbRekening.Items.Add(item);
                }
            }
        }
        public long GetID()
        {
            //ListItemData li = (ListItemData)cmbRekening.SelectedItem;
            m_SelectedID = 0;
            for (int i = 0; i < cmbRekening.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbRekening.Items[i];
                if (li.ItemText == cmbRekening.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }

            return m_SelectedID;
        }

        public void SetID(long pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbRekening.Items.Count; i++)
            {
                li = (ListItemData)cmbRekening.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbRekening.SelectedIndex = i;
                    break;
                }
            }
        }
        public TAnggaranRekening GetAnggaranRekening()
        {
            if (m_SelectedID == 0)
                GetID();
            //if (cmb.Count==0)
            //{
            //    TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic((int)GlobalVar.TahunAnggaran);
            //    mListUnit = oLogic.Get((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan, m_IDProgram, m_IDKegiatan, m_jenis, m_ppkd, 2, 2,0);

            //    //for (int i = 0; i < cmbRekening.Items.Count; i++)
            //    //{
            //    //    mListUnit.Add((TAnggaranRekening)cmbRekening.Items[i]);
            //    //}
            //}

            //ListItemData item = new ListItemData(ta.IDRekening.ToString() + " -  " + ta.Nama, ta.IDRekening);
            //cmbRekening.Items.Add(item);

            foreach (TAnggaranRekening k in _lst)
            {
                
                if (k.IDRekening== m_SelectedID)
                {
                    return k;
                }
            }
            return null;

        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                GetID();
              //  if (m_SelectedID != null && m_SelectedID !=0)
                  OnChanged(m_SelectedID);
            }
        }
        public long GetSelectedID()
        {
            return m_SelectedID;
        }



        private void cmbRekening_Click(object sender, EventArgs e)
        {
            FireChangeEvent();
        }

        private void cmbRekening_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRekening.Text.Trim().Length != 0)
            {
                FireChangeEvent();
            }
        }


    }
}
