using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DTO;
using BP;
using Formatting;

namespace KUAPPAS
{
    public partial class treeMasterProgramKegiatan : UserControl
    {
        private int m_pDinas;
        private int m_iUrusan;
        private int m_iProgram;
        private int m_iKegiatan;
        private long  m_iSubKegiatan;
        private int mProfile;
        private int m_iTahun;
        public delegate void ValueChangedEventHandler(object sender, EventArgs e, Single iType, Single ID);
        public delegate EventResponseMessage SelectedEventHandler(int ID, Single ObjectType);
        public delegate EventResponseMessage SelectedProgramEventHandler(int ID);
        public delegate EventResponseMessage SelectedKegiatanEventHandler(int ID);
        public delegate EventResponseMessage SelectedSubKegiatanEventHandler(long ID);

        public event SelectedEventHandler Changed;
        public event SelectedProgramEventHandler ProgramChanged;
        public event SelectedKegiatanEventHandler KegiatanChanged;
        public event SelectedSubKegiatanEventHandler SubKegiatanChanged;

        private string NaneByText;

        private MasterProgram m_oProgram;
        private MasterKegiatan m_oKegiatan;
        private SubKegiatan m_oSubKegiatan;



        private Point m_iPoint;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        TreeNode m_previousSelectedNode;
        List<SKPD> m_lstSKPD = new List<SKPD>();


        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        private Single m_iTahap;

        public treeMasterProgramKegiatan()
        {
            InitializeComponent();
            m_pDinas = 0;
            m_iTahap = 0;
            m_iUrusan = 0;
            m_iProgram = 0;
            m_iKegiatan = 0;
            m_iKegiatan = 0;
            m_iSubKegiatan = 0;
            
            //m_iTahun = (int)GlobalVar.TahunAnggaran;
            m_previousSelectedNode = null;
        
        }
        public int Profile
        {
        
            set{ mProfile = value;}

        }
        private void treeView1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void treeMasterProgramKegiatan_Load(object sender, EventArgs e)
        {

        }
        public bool Create(int _pDInas, Single pTahap)
        {
            bool lRet = true;





            UrusanLogic oUrusanLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
            m_pDinas = _pDInas;
            tvProgramKegiatan.Nodes.Clear();
            TreeNode root = new TreeNode("Urusan Pemerintah, Program,Kegiatan");
            root.Tag = "0DUMMY0";
            tvProgramKegiatan.Nodes.Add(root);
            root.Nodes.Clear();
            List<Urusan> lstUrusanDinas = oUrusanLogic.Get();
            if (lstUrusanDinas == null)
            {
                if (oUrusanLogic.IsError())
                    MessageBox.Show(oUrusanLogic.LastError());

                return false;
            }
            var query = from n in lstUrusanDinas
                        orderby n.ID 
                        select n;
            foreach (Urusan ud in query)
            {

                TreeNode node = new TreeNode();
                node.Tag = "1DUMMY" + ud.ID.ToString();
                node.Text = ud.ID + "  " + ud.Nama;
                node.Nodes.Add("DUMMY");
                root.Nodes.Add(node);


                node.ExpandAll();// Expand();
            }

            return lRet;
        }
        private void LoadProgram(TreeNode node)
        {
            //if (GlobalVar.TahunAnggaran== 2020)
            //{
            //    RPJMDProgramLogic oPrgLogic = new RPJMDProgramLogic(GlobalVar.TahunAnggaran);
            //    List<RPJMDProgram> lstProgram = oPrgLogic.GetBySKPD(m_pDinas);// new List<Negara>();



            //    node.Nodes.Clear();

            //    foreach (RPJMDProgram oProgram in lstProgram)
            //    {
            //        if (oProgram.IDUrusan == m_iUrusan)
            //        {
            //            TreeNode nodprog = new TreeNode();
            //            nodprog.Tag = "2DUMMY" + oProgram.ID.ToString();
            //            nodprog.Text = oProgram.ID.ToString("00") + " " + oProgram.Nama;// DisplayedCode + " " + oUnit.Nama;
            //            node.Nodes.Add(nodprog);
            //            nodprog.Nodes.Add("DUMMY");
            //        }
            //    }

            //}
            //else
            //{

            TProgramLogic oPrgLogic = new TProgramLogic(GlobalVar.TahunAnggaran, mProfile);
            List<TPrograms> lstProgram = oPrgLogic.GetByDinasAndUrusan(m_pDinas, m_iUrusan, m_lstSKPD);// new List<Negara>();
            node.Nodes.Clear();

            foreach (TPrograms oProgram in lstProgram)
            {
                TreeNode nodprog = new TreeNode();
                nodprog.Tag = "2DUMMY" + oProgram.IDProgram.ToString();
                nodprog.Text = oProgram.IDProgram.ToString("00") + " " + oProgram.Nama;// DisplayedCode + " " + oUnit.Nama;
                node.Nodes.Add(nodprog);
                nodprog.Nodes.Add("DUMMY");
            }
            // }
        }

        private void LoadKegiatan(TreeNode node)
        {

        //    MasterProgramLogic oKAPBDLogic = new MasterProgramLogic(GlobalVar.TahunAnggaran);
        //    List<MasterProgram> lst = oKAPBDLogic.GetByUrusan();


        //    ///List<TKegiatan> lst = oKegiatanLogic.GetByDinasAndUrusanAndIDProgram(m_pDinas,m_iUrusan,m_iProgram);// new List<Negara>();

        //    node.Nodes.Clear();

        //    foreach (TKegiatanAPBD o in lst)
        //    {
        //        TreeNode nodkeg = new TreeNode();
        //        nodkeg.Tag = "3DUMMY" + o.IDKegiatan.ToString();
        //        if (m_iTahap == 0)
        //        {
        //            nodkeg.Text = o.TampilanKode + " " + o.Nama.Trim() + "->(" + o.JumlahDiInput + ")";
        //        }
        //        else
        //            nodkeg.Text = o.TampilanKode + " " + o.Nama.Trim() + "->(" + o.JumlahPagu + ")";
        //        NaneByText = o.Nama;

        //        node.Nodes.Add(nodkeg);
        //    }
        //    // }

        }
    }
}
