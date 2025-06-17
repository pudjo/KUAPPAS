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
    public partial class ctrlSubKegiatan : UserControl
    {

        public delegate void ValueChangedEventHandler(long  pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private int m_iDinas;
        private int m_iUrusan;
        private int m_IDProgram;
        private int m_profile;
        private bool OnLoad;

        public ctrlSubKegiatan()
        {
            InitializeComponent();
                if (GlobalVar.PP90 == true) 
            m_profile = 2;
            else 
            m_profile=1;
        
        }

        private void ctrlSubKegiatan_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            cmbSubKegiatan.Text = "";
            cmbSubKegiatan.SelectedIndex = -1;

        }
        public int Profile
        {
            set { m_profile = value; }
            get { return m_profile; }
        }
        public List<SPDDetail> GetSPDRekening(DateTime dBatas)
        {
            List<SPDDetail> lRet = new List<SPDDetail>();

            if (m_SelectedID == 0)
            {
                m_SelectedID = GetID();
            }
            TSubKegiatanLogic oLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran,m_profile);
            lRet = oLogic.GetRekeningBasedSPD(m_SelectedID, m_iDinas, dBatas);
            return lRet;


        }

        public List<TAnggaranRekening> GetAnggaran(DateTime dBatas)
        {
            List<TAnggaranRekening> lRet = new List<TAnggaranRekening>();

            if (m_SelectedID == 0)
            {
                m_SelectedID = GetID();
            }
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran, m_profile);
            lRet = oLogic.GetBySubKegiatan(GlobalVar.TahunAnggaran, m_iDinas, m_SelectedID);
            return lRet;


        }

        public void Show(List<SPPRekening> lst)
        {
            try
            {
                cmbSubKegiatan.Items.Clear();
                foreach (SPPRekening sp in lst)
                {
                    ListItemData item = new ListItemData(sp.IDSubKegiatan.TampilanSubKegiatan() + "   " + sp.NamaSubKegiatan, sp.IDSubKegiatan);
                    if (cmbSubKegiatan.Items.Contains(item)== false)
                        cmbSubKegiatan.Items.Add(item);


                }
                cmbSubKegiatan.SelectedIndex = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void Create( int Tahun, int dinas, int urusan, int program, int idKEgiatan, long idsubkegiatan=0,List<SPPRekening> lstSPPRekening=null)
        {
            try
            {
                cmbSubKegiatan.Items.Clear();

                TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran,m_profile);
                List<TSubKegiatan> _lst = new List<TSubKegiatan>();
                
                
                _lst = oLOgic.GetSubKegiatanByKegiatan (Tahun, dinas, urusan, program,idKEgiatan);
                OnLoad = true;
                if (_lst != null)
                {
                    if (lstSPPRekening != null)
                    {
                        var query = from k in _lst
                                    join kegs in lstSPPRekening
                                    on k.IDSubKegiatan equals kegs.IDSubKegiatan 

                                    select new { IDSUbKegiatan = k.IDSubKegiatan, Nama = k.Nama};

                        long  olddIDSubKegiatan = 0;
                        foreach (var skg in query)
                        {
                            if (skg.IDSUbKegiatan != olddIDSubKegiatan)
                            {
                                ListItemData item = new ListItemData(skg.IDSUbKegiatan.ToKodeSubKegiatan() + " " + skg.Nama, skg.IDSUbKegiatan);
                                cmbSubKegiatan.Items.Add(item);
                                olddIDSubKegiatan = skg.IDSUbKegiatan;
                            }
                        }

                    }
                    else
                    {

                        foreach (TSubKegiatan p in _lst)
                        {
                            ListItemData item = new ListItemData(p.IDSubKegiatan.TampilanSubKegiatan() + " " + p.Nama, p.IDSubKegiatan);
                            if (idsubkegiatan == 0)
                            {
                                cmbSubKegiatan.Items.Add(item);
                            }
                            else
                            {
                                if (idsubkegiatan == p.IDSubKegiatan)
                                {
                                    cmbSubKegiatan.Items.Add(item);
                                }
                            }
                        }
                    }
                    cmbSubKegiatan.SelectedIndex = 0;

                }
                OnLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void CreateWithUK(int Tahun, int dinas, int KodeUK, int idKEgiatan, List<SPPRekening> lstSPPRekening = null)
        {
            try
            {
                cmbSubKegiatan.Items.Clear();
               
                TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, m_profile);
                List<TSubKegiatan> _lst = new List<TSubKegiatan>();

                //if (GlobalVar.gSubKegiatan == null )
                //{
                    GlobalVar.gSubKegiatan = new List<TSubKegiatan>();
                    GlobalVar.gSubKegiatan = oLOgic.GetSubKegiatanByDinas(GlobalVar.TahunAnggaran, dinas);

              //  }

                //if (GlobalVar.gSubKegiatan.Count==0){

                //    GlobalVar.gSubKegiatan = oLOgic.GetSubKegiatanByDinas(GlobalVar.TahunAnggaran, dinas);

                //}

                //if (GlobalVar.gSubKegiatan.FindAll(sk=>sk.IDDinas==dinas ).Count==0)
                //{
                //    // JIka tidak ada data untuk dinas ini... 
                //    GlobalVar.gSubKegiatan = oLOgic.GetSubKegiatanByDinas(GlobalVar.TahunAnggaran, dinas);

                //}

                if (KodeUK == 1)
                {
                    _lst = GlobalVar.gSubKegiatan.FindAll(sk => (sk.KodeUk == KodeUK || sk.KodeUk == 0) && sk.IDKegiatan == idKEgiatan);

                }
                else
                {
                    //foreach(TSubKegiatan sk in GlobalVar.gSubKegiatan ){
                     //   if (sk.KodeUk == 3)
                     //   {
                      //     // MessageBox.Show(sk.Nama);
                        //}
                   // }
                    _lst = GlobalVar.gSubKegiatan.FindAll(sk => sk.KodeUk == KodeUK && sk.IDKegiatan == idKEgiatan);
                }

                OnLoad = true;
                if (_lst != null)
                {
                    if (lstSPPRekening != null)
                    {
                        var query = from k in _lst
                                    join kegs in lstSPPRekening
                                    on k.IDSubKegiatan equals kegs.IDSubKegiatan

                                    select new { IDSUbKegiatan = k.IDSubKegiatan, Nama = k.Nama };

                        long olddIDSubKegiatan = 0;
                        foreach (var skg in query)
                        {
                            if (skg.IDSUbKegiatan != olddIDSubKegiatan)
                            {
                                ListItemData item = new ListItemData(skg.IDSUbKegiatan.ToKodeSubKegiatan() + " " + skg.Nama, skg.IDSUbKegiatan);
                                cmbSubKegiatan.Items.Add(item);
                                olddIDSubKegiatan = skg.IDSUbKegiatan;
                            }
                        }

                    }
                    else
                    {

                        foreach (TSubKegiatan p in _lst)
                        {
                            ListItemData item = new ListItemData(p.IDSubKegiatan.TampilanSubKegiatan() + " " + p.Nama, p.IDSubKegiatan);
                            
                                cmbSubKegiatan.Items.Add(item);
                            
                           
                        }
                    
                    }
                    if (cmbSubKegiatan.Items.Count>0 )
                    cmbSubKegiatan.SelectedIndex = 0;

                }
                OnLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void CreateFromBidangAnggaran(int Tahun, int dinas, int KodeUK, 
               int idKEgiatan, 
               int bidangAnggaran,
                List<SPPRekening> lstSPPRekening = null)
        {
            try
            {
                cmbSubKegiatan.Items.Clear();

                TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, m_profile);
             

                if (GlobalVar.gSubKegiatan == null)
                {
                    GlobalVar.gSubKegiatan = new List<TSubKegiatan>();
                    GlobalVar.gSubKegiatan = oLOgic.GetByDInasDanBidangAnggaran(dinas, KodeUK, bidangAnggaran);

                }

                if (GlobalVar.gSubKegiatan.Count == 0)
                {

                    GlobalVar.gSubKegiatan = oLOgic.GetByDInasDanBidangAnggaran(dinas, KodeUK, bidangAnggaran);

                }

                

                OnLoad = true;
                if (GlobalVar.gSubKegiatan != null)
                {
                    if (lstSPPRekening != null)
                    {
                        var query = from k in GlobalVar.gSubKegiatan
                                    join kegs in lstSPPRekening
                                    on k.IDSubKegiatan equals kegs.IDSubKegiatan

                                    select new { IDSUbKegiatan = k.IDSubKegiatan, Nama = k.Nama };

                        long olddIDSubKegiatan = 0;
                        foreach (var skg in query)
                        {
                            if (skg.IDSUbKegiatan != olddIDSubKegiatan)
                            {
                                ListItemData item = new ListItemData(skg.IDSUbKegiatan.ToKodeSubKegiatan() + " " + skg.Nama, skg.IDSUbKegiatan);
                                cmbSubKegiatan.Items.Add(item);
                                olddIDSubKegiatan = skg.IDSUbKegiatan;
                            }
                        }

                    }
                    else
                    {

                        foreach (TSubKegiatan p in GlobalVar.gSubKegiatan)
                        {
                            ListItemData item = new ListItemData(p.IDSubKegiatan.ToKodeSubKegiatan() + " " + p.Nama, p.IDSubKegiatan);

                            cmbSubKegiatan.Items.Add(item);


                        }

                    }
                    if (cmbSubKegiatan.Items.Count > 0)
                        cmbSubKegiatan.SelectedIndex = 0;

                }
                OnLoad = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public string GetNamaSubKegiatan()
        {
            return DataFormat.RemoveDigits( cmbSubKegiatan.Text).Trim();
        }
        public long  GetID()
        {
            //ListItemData li = (ListItemData)cmbKegiatan.SelectedItem;
            m_SelectedID = 0;

            for (int i = 0; i < cmbSubKegiatan.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbSubKegiatan.Items[i];
                if (li.ItemText == cmbSubKegiatan.Text)
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

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbSubKegiatan.Items.Count; i++)
            {
                li = (ListItemData)cmbSubKegiatan.Items[i];
                if (li.lItemData == pID)
                {
                    cmbSubKegiatan.SelectedIndex = i;
                    m_SelectedID = pID;
                 //   FireChangeEvent();
                    break;
                }
            }
        }
        private void FireChangeEvent()
        {
            if (OnChanged != null && OnLoad==false )
            {
                OnChanged(m_SelectedID);
            }
        }
        public long  GetSelectedID()
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

        private void cmbSubKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetID();
            FireChangeEvent();

        }
        //public bool CreateMaster(int programID)
        //{
        //    try
        //    {
        //        cmbSubKegiatan.Items.Clear();
        //        TKegiatanLogic oLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran,m_profile);
        //        List<TKegiatan> mListUnit = new List<TKegiatan>();
        //        mListUnit = oLogic.GetByFormMaster(programID / 100, programID);

        //        if (mListUnit != null)
        //        {
        //            foreach (TKegiatan p in mListUnit)
        //            {
        //                ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
        //                cmbSubKegiatan.Items.Add(item);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return false;
        //    }
        //}

        //public void CreateBasedSPD(int Tahun, int dinas, int urusan, int program, long inourutSPD)
        //{
        //    try
        //    {
        //        cmbSubKegiatan.Items.Clear();

        //        TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran);



        //        List<TSubKegiatan> mListUnit = new List<TSubKegiatan>();
        //        mListUnit = oLOgic.GetKegiatanByProgramAndSPD(Tahun, dinas, urusan, program, inourutSPD);

        //        if (mListUnit != null)
        //        {
        //            foreach (TSubKegiatan p in mListUnit)
        //            {
        //                ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
        //                cmbSubKegiatan.Items.Add(item);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}

        //public void CreateBasedSP2D(int Tahun, int dinas, int urusan, int program, long inourutSP2D)
        //{
        //    try
        //    {
        //        cmbSubKegiatan.Items.Clear();

        //        TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, m_profile);



        //        List<TSubKegiatan> mListUnit = new List<TSubKegiatan>();
        //        mListUnit = oLOgic.GetKegiatanByProgramAndSP2D(Tahun, dinas, urusan, program, inourutSP2D);

        //        if (mListUnit != null)
        //        {
        //            foreach (TSubKegiatan p in mListUnit)
        //            {
        //                ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
        //                cmbSubKegiatan.Items.Add(item);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}
        //public void CreateBasedSPJ(int Tahun, int dinas, int urusan, int program, long inourutSPJ)
        //{
        //    try
        //    {
        //        cmbSubKegiatan.Items.Clear();

        //        TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, m_profile);



        //        List<TSubKegiatan> mListUnit = new List<TSubKegiatan>();
        //        mListUnit = oLOgic.GetKegiatanByProgramAndSPJ(Tahun, dinas, urusan, program, inourutSPJ);

        //        if (mListUnit != null)
        //        {
        //            foreach (TSubKegiatan p in mListUnit)
        //            {
        //                ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
        //                cmbSubKegiatan.Items.Add(item);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}
        //public void CreateBasedBAST(int Tahun, int dinas, int urusan, int program, long inourutBAST)
        //{
        //    try
        //    {
        //        cmbSubKegiatan.Items.Clear();

        //        TSubKegiatanLogic oLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, m_profile);
        //        List<TSubKegiatan> mListUnit = new List<TSubKegiatan>();
        //        mListUnit = oLOgic.GetKegiatanByProgramAndBAST(Tahun, dinas, urusan, program, inourutBAST);

        //        if (mListUnit != null)
        //        {
        //            foreach (TSubKegiatan p in mListUnit)
        //            {
        //                ListItemData item = new ListItemData(p.IDKegiatan.ToSimpleKodeKegiatan() + " " + p.Nama, p.IDKegiatan);
        //                cmbSubKegiatan.Items.Add(item);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //}

    }
}
