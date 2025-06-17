using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;
using Formatting;


namespace KUAPPAS.Bendahara
{
    public partial class ctrlPanjar : UserControl
    {
        public int m_idUrusan = 0;
        public int m_idProgram = 0;
        public long m_idKegiatan = 0;
        public long m_iNoUrut;
        public int m_idDinas;

        public delegate void ValueChangedEventHandler(long pID);
        public event ValueChangedEventHandler OnChanged;
        private long m_SelectedID;
        private List<Pengeluaran> m_listPengeluaran;

        public ctrlPanjar()
        {
            InitializeComponent();
            m_listPengeluaran = new List<Pengeluaran>();
        }

        private void ctrlPanjar_Load(object sender, EventArgs e)
        {

        }
        public void Create(int idDinas, int IDUrusan, int IDKegiatan, int _Jenis)
        {

            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            p.IDDInas = idDinas;
            p.IDUrusan = IDUrusan;
            p.IDKegiatan = IDKegiatan;
            p.Jenis = _Jenis;
            if (GlobalVar.gListPengeluaran == null)
            {
                GlobalVar.gListPengeluaran = new List<Pengeluaran>();

                GlobalVar.gListPengeluaran = oLogic.Get(p);
            }
            if (GlobalVar.gListPengeluaran.FindAll(x => x.IDDInas == idDinas).Count == 0)
            {
                GlobalVar.gListPengeluaran = oLogic.Get(p);
            }

            m_listPengeluaran = GlobalVar.gListPengeluaran;
       //     m_listPengeluaran = oLogic.Get(p);


        }
        public void Create(int idDinas, int KodeUK,int _Jenis, DateTime dBatas)
        {

            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            p.IDDInas = idDinas;
            p.KodeUK = KodeUK;
            p.Jenis = _Jenis;
            p.TanggalAwal = new DateTime(GlobalVar.TahunAnggaran, 1, 1);
            p.TanggalAkhir = new DateTime(GlobalVar.TahunAnggaran,12 , 31);
            cmbPanjar.Items.Clear();
            //if (GlobalVar.gListPengeluaran == null)
            //{
            //    GlobalVar.gListPengeluaran = new List<Pengeluaran>();

            GlobalVar.gListPengeluaran = oLogic.GetUntukKoreksi(p);
            ////}
            //if (GlobalVar.gListPengeluaran.FindAll(x=>x.IDDInas ==idDinas).Count==0)
            //{
            //    GlobalVar.gListPengeluaran = oLogic.Get(p);
            //}

             
                    m_listPengeluaran = GlobalVar.gListPengeluaran;//.FindAll(x => x.Tanggal <= dBatas && x.Jenis == E_JENISPENGELUARAN.PENGELUARAN_PANJAR);
                
               // if (_Jenis == 3 || _Jenis == 4)
               // m_listPengeluaran = GlobalVar.gListPengeluaran.FindAll(x => x.Tanggal <= dBatas );

            if (m_listPengeluaran != null)
            {
                foreach (Pengeluaran k in m_listPengeluaran)
                {
                    
                    ListItemData li = new ListItemData(k.NoBukti + " (" + k.Tanggal.ToTanggalIndonesia() +")" , k.NoUrut,k);
                    cmbPanjar.Items.Add(li);

                }

            }
        }
        public void CreateUntukDIpertanggungjawabkan(int idDinas)
        {

            PengeluaranLogic oLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            cmbPanjar.Items.Clear();


            m_listPengeluaran = oLogic.GetUntukDiPertanggungjawabkan(idDinas);


            if (m_listPengeluaran != null)
            {
                foreach (Pengeluaran k in m_listPengeluaran)
                {

                    ListItemData li = new ListItemData(k.NoBukti + " (" + k.Tanggal.ToTanggalIndonesia() + ")", k.NoUrut, k);
                    cmbPanjar.Items.Add(li);

                }

            }
        }
        public long GetID()
        {
            m_SelectedID = 0;
            for (int i = 0; i < cmbPanjar.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cmbPanjar.Items[i];
                if (li.ItemText == cmbPanjar.Text)
                {
                    m_SelectedID = li.lItemData;
                    break;
                }

            }
            return m_SelectedID;
        }
        public Pengeluaran GetPengeluaran(long pID)
        {
            try
            {
                if (pID == 0)
                    return null;
                GetID();
                Pengeluaran pengeluaran = new Pengeluaran();
                pengeluaran = m_listPengeluaran.First(p => p.NoUrut == pID);
                return pengeluaran;
            }
            catch (Exception ex)
            {
                return null;
            }
                    
        }
        public Pengeluaran GetPengeluaran()
        {
            try
            {
                Pengeluaran p = new Pengeluaran();
                for (int i = 0; i < cmbPanjar.Items.Count; i++)
                {
                    ListItemData li = (ListItemData)cmbPanjar.Items[i];
                    if (li.ItemText == cmbPanjar.Text)
                    {
                        p = (Pengeluaran)li.something;

                        break;
                    }

                }
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void cmbPanjar_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireChangeEvent();
        }
        private void FireChangeEvent()
        {
            if (OnChanged != null)
            {
                GetID();
                OnChanged(m_SelectedID);
            }
        }
        public decimal Jumlah
        {
            get
            {
                return GetJumlahPanjar();
            }
        }
        private decimal GetJumlahPanjar(){
            Pengeluaran p  = m_listPengeluaran.First( ob =>ob.NoUrut == m_SelectedID );
            return p.Jumlah;
    

        }
        public void SetID(long pID)
        {
            int i;
            m_SelectedID = pID;

            ListItemData li = new ListItemData("", 0);
            for (i = 0; i < cmbPanjar.Items.Count; i++)
            {
                li = (ListItemData)cmbPanjar.Items[i];
                if (li.lItemData == Convert.ToInt64(pID))
                {
                    cmbPanjar.SelectedIndex = i;
                    break;
                }
            }
        }

    }
}
