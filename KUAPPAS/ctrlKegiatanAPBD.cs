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


namespace KUAPPAS
{
    public partial class ctrlKegiatanAPBD : UserControl
    {
        public delegate void ValueChangedEventHandler(int   pID);
        public event ValueChangedEventHandler OnChanged;
        private int  m_SelectedID;
        private int m_iDinas;
        private int m_iUrusan;
        private int m_IDProgram;
        private int m_profile;
        private bool OnCreate;

        public ctrlKegiatanAPBD()
        {
            InitializeComponent();
            if (GlobalVar.PP90 == true) 
            m_profile = 2;
            else 
            m_profile=1;

            OnCreate = false;
        }

        private void ctrlKegiatanAPBD_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            cmbKegiatan.Text = "";
            cmbKegiatan.SelectedIndex = -1;

        }
        public int Profile
        {
            set { m_profile = value; }
            get { return m_profile; }
        }
        public List<RekeningDetail> GetSPDRekening(DateTime dBatas)
        {
            List<RekeningDetail> lRet = new List<RekeningDetail>();

            if (m_SelectedID == 0)
            {
                m_SelectedID = GetID();
            }
            TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
            lRet = oLogic.GetRekeningBasedSPD(m_SelectedID, m_iDinas, dBatas);
            return lRet;


        }
        private void cmbKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }


        public void Show(List<SPPRekening> lst)
        {
            try
            {
                cmbKegiatan.Items.Clear();
                foreach (SPPRekening sp in lst)
                {
                    ListItemData item = new ListItemData(sp.IDKegiatan.ToSimpleKodeKegiatan() +"   " + sp.NamaKegiatan, sp.IDKegiatan);
                    if (cmbKegiatan.Items.Contains(item)== false )
                    cmbKegiatan.Items.Add(item);


                }
                cmbKegiatan.SelectedIndex = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void Create( int Tahun, int dinas, int urusan, int program, int idKegiatan)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgram (Tahun, dinas, urusan, program);


                if (_lst != null)
                {
                    {
                        foreach (TKegiatanAPBD p in _lst)
                        {
                            ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);

                            if (idKegiatan == 0)
                            {
                                cmbKegiatan.Items.Add(item);
                            }
                            else
                            {
                                if (idKegiatan == p.IDKegiatan)
                                {
                                    cmbKegiatan.Items.Add(item);
                                }
                            }
                        }
                    }
                    if (cmbKegiatan.Items.Count > 0)
                        cmbKegiatan.SelectedIndex = 0;

                }
                OnCreate = false ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void Create(int Tahun, int dinas, int urusan, int program, List<SPPRekening> lstSPPRekening = null)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgram(Tahun, dinas, urusan, program);


                if (_lst != null)
                {
                    if (lstSPPRekening != null)
                    {
                        var query = from k in _lst
                                    join kegs in lstSPPRekening
                                    on k.IDKegiatan equals kegs.IDKegiatan

                                    select new { IDKeg = k.IDKegiatan, Nama = k.Nama };

                        int olddIDKegiatan = 0;
                        foreach (var kg in query)
                        {
                            if (kg.IDKeg != olddIDKegiatan)
                            {
                                ListItemData item = new ListItemData(kg.IDKeg.ToKodeKegiatan() + " " + kg.Nama, kg.IDKeg);
                                cmbKegiatan.Items.Add(item);
                                olddIDKegiatan = kg.IDKeg;
                            }
                        }
                        if (cmbKegiatan.Items.Count > 0)
                            cmbKegiatan.SelectedIndex = 0;

                    }
                    else
                    {
                        foreach (TKegiatanAPBD p in _lst)
                        {
                            ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                            cmbKegiatan.Items.Add(item);
                            
                        }
                    }
                    

                }
                OnCreate = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public string GetNamaKegiatan()
        {
            return  DataFormat.RemoveDigits(cmbKegiatan.Text).Trim();
        }
        public void CreateWIthUK(int Tahun, int dinas, int KodeUK , int program, List<SPPRekening> lstSPPRekening = null)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();

                _lst = oLOgic.GetKegiatanByDInasUkProgram (Tahun, dinas, KodeUK,program);


                if (_lst != null)
                {
                    if (lstSPPRekening != null)
                    {
                        var query = from k in _lst
                                    join kegs in lstSPPRekening
                                    on k.IDKegiatan equals kegs.IDKegiatan

                                    select new { IDKeg = k.IDKegiatan, Nama = k.Nama };

                        int olddIDKegiatan = 0;
                        foreach (var kg in query)
                        {
                            if (kg.IDKeg != olddIDKegiatan)
                            {
                                ListItemData item = new ListItemData(kg.IDKeg.ToKodeKegiatan() + " " + kg.Nama, kg.IDKeg);
                                cmbKegiatan.Items.Add(item);
                                olddIDKegiatan = kg.IDKeg;
                            }
                        }
                        if (cmbKegiatan.Items.Count > 0)
                            cmbKegiatan.SelectedIndex = 0;

                    }
                    else
                    {
                        foreach (TKegiatanAPBD p in _lst)
                        {
                            ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                            cmbKegiatan.Items.Add(item);

                        }
                    }
      

                }
                else
                {
                    MessageBox.Show("Kesalahan Mengambil data Kegiatan.");
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
                OnCreate = true;
                cmbKegiatan.Items.Clear();
                TKegiatanLogic oLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran,3);
                List<TKegiatan> _lst = new List<TKegiatan>();
                _lst = oLogic.GetByDinasAndUrusanAndIDProgramDrRenja( dinas, urusan, program);

                if (_lst != null)
                {
                    foreach (TKegiatan p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
                OnCreate = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public int GetID()
        {
            //ListItemData li = (ListItemData)cmbKegiatan.SelectedItem;
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

        public void SetID(int  pID)
        {
            int i;
            OnCreate = true;
            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbKegiatan.Items.Count; i++)
            {
                li = (ListItemData)cmbKegiatan.Items[i];
                if (li.Itemdata== pID)
                {
                    cmbKegiatan.SelectedIndex = i;
                    break;
                }
            }
            OnCreate = false;
        }
        private void FireChangeEvent()
        {
            if (OnCreate == false)
            {
                if (OnChanged != null)
                {
                    OnChanged(m_SelectedID);
                }
            }
        }
        public int   GetSelectedID()
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
                cmbKegiatan.Items.Clear();
                TKegiatanLogic oLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran,m_profile);
                List<TKegiatan> _lst = new List<TKegiatan>();
                _lst = oLogic.GetByFormMaster(programID / 100, programID);

                if (_lst != null)
                {
                    foreach (TKegiatan p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public void CreateBasedSPD(int Tahun, int dinas, int urusan, int program, long inourutSPD)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);



                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgramAndSPD(Tahun, dinas, urusan, program, inourutSPD);

                if (_lst != null)
                {
                    foreach (TKegiatanAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
                OnCreate = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void CreateBasedSP2D(int Tahun, int dinas, int urusan, int program, long inourutSP2D)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);



                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgramAndSP2D(Tahun, dinas, urusan, program, inourutSP2D);

                if (_lst != null)
                {
                    foreach (TKegiatanAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
                OnCreate = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedSPJ(int Tahun, int dinas, int urusan, int program, long inourutSPJ)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);



                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgramAndSPJ(Tahun, dinas, urusan, program, inourutSPJ);

                if (_lst != null)
                {
                    foreach (TKegiatanAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
                OnCreate = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateBasedBAST(int Tahun, int dinas, int urusan, int program, long inourutBAST)
        {
            try
            {
                OnCreate = true;
                cmbKegiatan.Items.Clear();

                TKegiatanAPBDLogic oLOgic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                List<TKegiatanAPBD> _lst = new List<TKegiatanAPBD>();
                _lst = oLOgic.GetKegiatanByProgramAndBAST(Tahun, dinas, urusan, program, inourutBAST);

                if (_lst != null)
                {
                    foreach (TKegiatanAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
                        cmbKegiatan.Items.Add(item);
                    }
                }
                OnCreate = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


    }
}
