using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class TreeProgramKegiatan : UserControl
    {
        private int mprofile;
        private int m_pDinas;
        private int m_iUrusan;
        private int m_iProgram;
        private int m_iKegiatan;
        private long m_iSubKegiatan;
        private int m_iTahun;
    
            private int m_iKodeUk;
        public delegate void ValueChangedEventHandler(object sender, EventArgs e, Single iType, Single ID);

        public delegate EventResponseMessage SelectedEventHandler(int ID, Single ObjectType);
        public delegate EventResponseMessage SelectedUrusanEventHandler(int ID);
        
        public delegate EventResponseMessage SelectedProgramEventHandler(int ID);
        public delegate EventResponseMessage SelectedKegiatanEventHandler(int ID);
        public delegate EventResponseMessage SelectedSubKegiatanEventHandler(long  ID);

        public event SelectedEventHandler Changed;
        public event SelectedUrusanEventHandler UrusanChanged;
        public event SelectedProgramEventHandler ProgramChanged;
        public event SelectedKegiatanEventHandler KegiatanChanged;
        public event SelectedSubKegiatanEventHandler SubKegiatanChanged;
        private string NaneByText;

        private TPrograms m_oProgram;
        private TKegiatan m_oKegiatan;
        private TSubKegiatan m_oSUbKegiatan;
        private bool bOnCreating;

        

        private Point m_iPoint;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        TreeNode m_previousSelectedNode;
        List<SKPD> m_lstSKPD = new List<SKPD>();


        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;
        private string LastSearchText;
        private Single m_iTahap;
        Font boldFont ;
        public TreeProgramKegiatan()
        {
            InitializeComponent();
            m_pDinas=0;
            m_iTahap = 0;
            m_iUrusan = 0;
            m_iProgram = 0;
            m_iKegiatan = 0;
            m_iSubKegiatan = 0;
            m_iKodeUk = 0;
            mprofile = 2;
            bOnCreating = true;
            m_iTahun=(int)GlobalVar.TahunAnggaran;
            m_previousSelectedNode = null;

        }
        public int Profile {
            set { mprofile = value; }
            get { return mprofile; }
        
        }
        private void tvProgramKegiatan_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (m_previousSelectedNode != null)
            //{
            //    m_previousSelectedNode.BackColor = tvProgramKegiatan.BackColor;
            //    m_previousSelectedNode.ForeColor = tvProgramKegiatan.ForeColor;
            //}

        }


        public bool Create(int _pDInas, Single pTahap, int idUnit =0, int kodeuk=0 )
        {
            bool lRet = true;

            boldFont = new Font(tvProgramKegiatan.Font, FontStyle.Bold);
  

            m_iTahap = pTahap;
           
            m_iKodeUk = idUnit;
            bOnCreating = true;

            //SKPDLogic oLogicSKPD = new SKPDLogic((int)GlobalVar.TahunAnggaran);

            //m_lstSKPD = oLogicSKPD.GetByParent(_pDInas);
            m_lstSKPD = GlobalVar.gListSKPD;
            if (m_lstSKPD.Count == 0)
                m_lstSKPD = null;

            UrusanDinasLogic oDinasLogic = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mprofile);
            m_pDinas = _pDInas;
            tvProgramKegiatan.Nodes.Clear();
            TreeNode root = new TreeNode("Urusan Pemerintah, Program,Kegiatan");
            root.Tag = "0DUMMY0";
            tvProgramKegiatan.Nodes.Add(root);
            root.Nodes.Clear();


            List<UrusanDinas> lstUrusanDinas = GlobalVar.gListUrusanDinas.FindAll(x => x.Tahun == (int)GlobalVar.TahunAnggaran && x.IDDinas == _pDInas);
            if (lstUrusanDinas == null)
            {
                if (oDinasLogic.IsError())
                    MessageBox.Show(oDinasLogic.LastError());

                return false;
            }
            var query = from n in lstUrusanDinas
                        orderby n.IDUrusan
                        select n;
            
            foreach (UrusanDinas ud in query)
            {

                TreeNode node = new TreeNode();
                node.Tag = "1DUMMY" + ud.IDUrusan.ToString();
                node.Text = ud.IDUrusan + "  " + ud.NamaUrusan;
                node.NodeFont = boldFont;
                node.Nodes.Add("DUMMY");  
                root.Nodes.Add(node);

              
                node.ExpandAll();// Expand();
            }

            bOnCreating = false;
            return lRet;
        }
        private void LoadUrusan(TreeNode node)
        {

            UrusanDinasLogic oDinasLogic = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mprofile);
            //List<UrusanDinas> lstUrusanDinas = oDinasLogic.GetByIDDinas(m_pDinas, (int)GlobalVar.TahunAnggaran, m_lstSKPD);// new List<Negara>();
            List<UrusanDinas> lstUrusanDinas = GlobalVar.gListUrusanDinas.FindAll(x => x.Tahun == (int)GlobalVar.TahunAnggaran && x.IDDinas == m_pDinas);
            

            if (lstUrusanDinas == null)
            {
                if (oDinasLogic.IsError())
                    MessageBox.Show(oDinasLogic.LastError());

                return;
            }
            var query = from n in lstUrusanDinas
                        orderby n.IDUrusan
                        select n;
            foreach (UrusanDinas ud in query)
            {

                TreeNode nodurs = new TreeNode();
                nodurs.Tag = "1DUMMY" + ud.IDUrusan.ToString();
                nodurs.Text = ud.IDUrusan + "  " + ud.NamaUrusan;
                nodurs.Nodes.Add("DUMMY");
                nodurs.NodeFont = boldFont;
                node.Nodes.Add(nodurs);

                
               // node.Expand();// Expand();
            }

            TProgramLogic oPrgLogic = new TProgramLogic(GlobalVar.TahunAnggaran,mprofile);
            
        }
        private void LoadProgram( TreeNode node)
        {
            if (bOnCreating == true)
                return;

            bOnCreating = true;
            TProgramLogic oPrgLogic = new TProgramLogic(GlobalVar.TahunAnggaran, mprofile);
            //List<TPrograms> lstProgram = oPrgLogic.GetByDinasAndUrusan(m_pDinas, m_iUrusan, m_lstSKPD);// new List<Negara>();

            List<TProgramAPBD> lstProgram = GlobalVar.gListProgram.FindAll(x => x.IDDinas == m_pDinas &&
                x.IDUrusan== m_iUrusan && x.KodeUK== m_iKodeUk);   
            node.Nodes.Clear();

            foreach (TProgramAPBD oProgram in lstProgram)
                {
                    TreeNode nodprog = new TreeNode();
                    nodprog.Tag = "2DUMMY" + oProgram.IDProgram.ToString();
                    nodprog.Text = oProgram.IDProgram.ToString("00") + " " + oProgram.Nama;// DisplayedCode + " " + oUnit.Nama;
                    nodprog.NodeFont = boldFont;
                
                    node.Nodes.Add(nodprog);
                    nodprog.Nodes.Add("DUMMY");
                }
                bOnCreating = false;
        }

        private void LoadKegiatan(TreeNode node)
        {
            if (bOnCreating == true)
                return;

            bOnCreating = true;

            TKegiatanAPBDLogic oKAPBDLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran, mprofile);
            List<TKegiatanAPBD> lst = oKAPBDLogic.GetKegiatanByProgramEx((int)GlobalVar.TahunAnggaran, m_pDinas, m_iUrusan, m_iProgram,m_iKodeUk, m_lstSKPD);

                node.Nodes.Clear();

                foreach (TKegiatanAPBD o in lst)
                {
                    TreeNode nodkeg = new TreeNode();
                    nodkeg.Tag = "3DUMMY" + o.IDKegiatan.ToString();

                    nodkeg.Text = o.TampilanKode + " " + o.Nama.Trim();
                    NaneByText = o.Nama;
                    node.Nodes.Add(nodkeg);
                    if (GlobalVar.PP90== true)
                        nodkeg.Nodes.Add("DUMMY");
                }

                bOnCreating = false;
        }
        private void LoadSubKegiatan(TreeNode node)
        {
            if (GlobalVar.PP90 == false)
                return;
            //m_iKodeUk = ctrl
            TSubKegiatanLogic oKAPBDLogic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, mprofile);
            List<TSubKegiatan> lst = oKAPBDLogic.GetSubKegiatanByKegiatanWithUnit((int)GlobalVar.TahunAnggaran, m_pDinas,
                m_iKodeUk, m_iUrusan, m_iProgram, m_iKegiatan);
            node.Nodes.Clear();

            if (lst != null)
            {
                foreach (TSubKegiatan o in lst)
                {
                    TreeNode nodkeg = new TreeNode();
                    nodkeg.Tag = "4DUMMY" + o.IDSubKegiatan.ToString();
                    nodkeg.Text = o.IDSubKegiatan.TampilanSubKegiatan() + " " + o.Nama.Trim();
                    NaneByText = o.Nama;

                    node.Nodes.Add(nodkeg);
                }
            }
            
        }

        public string GetNamInText (){

            return NaneByText;
        }

        private void tvProgramKegiatan_MouseUp(object sender, MouseEventArgs e)
        {
            m_iPoint = new Point(e.X, e.Y);
            TreeNode node = tvProgramKegiatan.GetNodeAt(m_iPoint);
            if (node == null)
            {
                return;
            }
            int iLevel = GetLevel(node);
             if (e.Button == MouseButtons.Right)
            {

                // Point where the mouse is clicked.


                if (node != null)
                {

                   tvProgramKegiatan.SelectedNode = node;
                    switch (iLevel)
                    {
                        case 0:
                            if (GlobalVar.Pengguna.IsUserDinas == 0)
                            {
                //                menuTambahSKPD.Visible = true;
                            }

                            break;

                        case 1:
                            //menuEditSKPD.Visible = true;
                            //menuHapusSKPD.Visible = true;
                            //menuSepUK.Visible = true;
                            //menuTambahUK.Visible = true;
                            break;
                        case 2:
                            //menuEditUK.Visible = true;
                            //menuHapusUnitKerja.Visible = true;
                            //menuSepSubUK.Visible = true;
                            //menuTambahSubUnit.Visible = true;
                            break;
                        case 3:
                            //menuEditSubUnit.Visible = true;
                            //menuHapusSubUnit.Visible = true;
                            break;

                    }


                   // menuDinas.Show(tvDinas, m_iPoint);


                    // Highlight the selected node.
                    //tvDinas.SelectedNode = m_OldSelectNode;
                    //m_OldSelectNode = null;
                }
            }
        }
        private int GetLevel(TreeNode node)
        {
            int iLevel = 0;
            if (node != null)
            {
                if (node.Tag != null)
                {
                    iLevel = Convert.ToInt32(node.Tag.ToString().Substring(0, 1));
                }
            }

            return iLevel;
        }

        private void tvProgramKegiatan_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag.ToString().Length > 5)
            {
                if (e.Node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
                {
                    e.Node.Nodes.Clear();
                    //int lID = GetID(e.NodeConvert.ToInt32(e.Node.Tag.ToString().Substring(6));
                    int lID = GetID(e.Node);
                    int iLevel = GetLevel(e.Node);
                    long LongID = 0;
                    switch (iLevel)
                    {
                        case 0:
                            //m_iUrusan=lID;
                            LoadUrusan(e.Node);

                            m_iProgram = 0;
                            m_iKegiatan = 0;
                            break;
                        case 1:
                            lID = GetID(e.Node);
                            //  e.Node.Tag = e.Node.Tag.ToString().Replace("DUMMY", "");// e.Node.Tag.ToString().Substring(6);
                            m_iUrusan = lID;

                            LoadProgram(e.Node);

                            m_iProgram = 0;

                            m_iKegiatan = 0;
                            break;
                        case 2:
                            lID = GetID(e.Node);
                            m_iProgram = lID;
                            LoadKegiatan(e.Node);
                            m_iKegiatan = 0;
                            break;
                        case 3:
                            lID = GetID(e.Node);
                            m_iKegiatan = lID;
                            if (GlobalVar.PP90 == true)
                            {
                                LoadSubKegiatan(e.Node);
                            }
                            m_iSubKegiatan = 0;
                            break;

                        case 4:
                            LongID = GetLongID(e.Node);
                            m_iSubKegiatan = LongID;
          

                            break;


                    }

                    //iLevel = Convert.ToInt32(e.Node.Tag.ToString().Substring(0, 1));
                    //e.Node.Tag = e.Node.Tag.ToString().Replace("DUMMY", "");// e.Node.Tag.ToString().Substring(6);
                    //switch (iLevel)
                    //{
                    //    case 0:
                    //        //m_iUrusan=lID;
                    //        LoadUrusan(e.Node);                            
                    //        m_iProgram=0;
                    //        m_iKegiatan=0;
                    //        break;
                    //    case 1:
                    //        m_iUrusan=lID;                            
                    //        LoadProgram(e.Node);
                    //        m_iProgram=0;
                    //        m_iKegiatan=0;


                           
                    //        break;
                    //    case 2:
                    //        //  LoadSubUnit(lID, e.Node);

                    //        m_iProgram = lID;
                    //        LoadKegiatan(e.Node);
                    //        m_iKegiatan = 0;
                    //        break;
                    //}
                }
            }
        }
        
        private int GetID(TreeNode node)
        {
            int lID;
            lID = 0;
            int iLevel = GetLevel(node);

            //2DUMMY
            if (node != null)
            {
                if (node.Tag != null)
                {
                    if (node.Tag.ToString().Length > 5 && node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
                    {


                        lID = Convert.ToInt32(node.Tag.ToString().Substring(6));


                    }
                }
                //else
                //{
                //    lID = Convert.ToInt32(node.Tag.ToString().Substring(1));
                //}
            }
            return lID;

        }
        private long  GetLongID(TreeNode node)
        {
            long lID;
            lID = 0;
            int iLevel = GetLevel(node);

            //2DUMMY
            if (node != null)
            {
                if (node.Tag != null)
                {
                    if (node.Tag.ToString().Length > 5 && node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
                    {


                        lID = Convert.ToInt64(node.Tag.ToString().Substring(6));


                    }
                }
                //else
                //{
                //    lID = Convert.ToInt32(node.Tag.ToString().Substring(1));
                //}
            }
            return lID;

        }

        private void tvProgramKegiatan_MouseDown(object sender, MouseEventArgs e)
        {
            m_iPoint = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = tvProgramKegiatan.GetNodeAt(m_iPoint);
                if (node == null)
                {
                    return;
                }
                int iLevel = GetLevel(node);
                int pID=0; //= GetID(node);
                switch (iLevel) { 
                    case 0:

                        break;
                    case 1:
                  //      ProgramChanged(pID);
                        pID = GetID(node);
                        if (UrusanChanged != null)
                            UrusanChanged(pID);
                        break;


                        break;
                    case 2:
                        if (ProgramChanged != null)
                            pID = GetID(node);
                        if (pID>0)
                            ProgramChanged(pID);
                        break;

                    case 3:

                        if (KegiatanChanged != null)
                        {
                            EventResponseMessage res = new EventResponseMessage();
                            pID = GetID(node);
                        
                            res = KegiatanChanged(pID);
                            m_iKegiatan = pID;
                            LoadSubKegiatan(node);

                            if (res.ResponseStatus == true)
                            {
                                //if (m_previousSelectedNode != null)
                                //{
                                //    m_previousSelectedNode.BackColor = tvProgramKegiatan.BackColor;
                                //    m_previousSelectedNode.ForeColor = tvProgramKegiatan.ForeColor;
                                //    for (int x = 0; x < 1000; x++)
                                //    {
                                //        string z;
                                //        z = "";
                                //    }
                                //        m_previousSelectedNode = node;

                                //    node.BackColor = SystemColors.Highlight;
                                //    node.ForeColor = Color.White;

                                //}
                            }
                        }


                        break;
                    case 4:
                        if (SubKegiatanChanged != null)
                        {
                            long pLID = GetLongID(node);

                            EventResponseMessage res = new EventResponseMessage();
                            res = SubKegiatanChanged(pLID);
                            if (res.ResponseStatus == true)
                            {
                                if (m_previousSelectedNode != null)
                                {
                                   
                                }
                            }
                        }


                        break;
                }
                
                

            }
        }
        //private long GetLongID(TreeNode node)
        //{
        //    long lID;
        //    lID = 0;
        //    int iLevel = GetLevel(node);

        //    //2DUMMY
        //    if (node != null)
        //    {
        //        if (node.Tag != null)
        //        {
        //            if (node.Tag.ToString().Length > 8 && node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
        //            {

        //                lID = Convert.ToInt64(node.Tag.ToString().Substring(6));
        //            }
        //        }
        //    }
        //    return lID;

        //}
     
        private void tvProgramKegiatan_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                if (tvProgramKegiatan.Focused)
                    e.Graphics.FillRectangle(greenBrush, e.Bounds);
                else
                    e.Graphics.FillRectangle(redBrush, e.Bounds);
            }
            else
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            e.Graphics.DrawRectangle(SystemPens.Control, e.Bounds);

            TextRenderer.DrawText(e.Graphics,
                                   e.Node.Text,
                                   e.Node.TreeView.Font,
                                   e.Node.Bounds,
                                   e.Node.ForeColor);
        }

        private void tvProgramKegiatan_Validating(object sender, CancelEventArgs e)
        {
            if (tvProgramKegiatan.SelectedNode != null)
            {
                tvProgramKegiatan.SelectedNode.BackColor = SystemColors.Highlight;
                tvProgramKegiatan.SelectedNode.ForeColor = Color.White;
                m_previousSelectedNode = tvProgramKegiatan.SelectedNode;
            }
        }

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    foreach (TreeNode nd in StartNode.Nodes)
                    {
                        //SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                        SearchNodes(SearchText, nd);//Recursive Search 
                    }
                };
                StartNode = StartNode.NextNode;
            };

        }

        private bool SearchRecursive(IEnumerable nodes, string searchFor)
        {
            foreach (TreeNode node in nodes)
            {
                if (node != null)
                {
                    if (node.Text.ToUpper().Contains(searchFor))
                    {
                        tvProgramKegiatan.SelectedNode = node;
                        node.BackColor = Color.Yellow;
                    }

                    if (SearchRecursive(node.Nodes, searchFor))
                        return true;
                }
            }
            return false;
        }
        private bool Putihkan (IEnumerable nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node != null)
                {
                    
                        tvProgramKegiatan.SelectedNode = node;
                        node.BackColor = Color.White;


                        if (Putihkan(node.Nodes))
                        return true;
                }
            }
            return false;
        }

 
        private void cmdCari_Click(object sender, EventArgs e)
        {
            var searchFor = txtCari.Text.Trim().ToUpper();

            tvProgramKegiatan.ExpandAll();
            //foreach (TreeNode node in tvProgramKegiatan.Nodes)
            //{
            //    if (node != null)
            //    {
            //            node.BackColor = Color.White ;
            //    }
            //}

            Putihkan(tvProgramKegiatan.Nodes);

            if (searchFor != "")
            {
                if (tvProgramKegiatan.Nodes.Count > 0)
                {
                    if (SearchRecursive(tvProgramKegiatan.Nodes, searchFor))
                    {
                        tvProgramKegiatan.SelectedNode.Expand();
                        tvProgramKegiatan.Focus();
                    }
                }
            }
            //TKegiatanLogic oKegiatanLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran);

            //string sSeacrh = txtCari.Text.Trim();

            //List<TKegiatan> lst = oKegiatanLogic.GetByDinasAndName(m_pDinas, sSeacrh);// new List<Negara>();


            //gridResultCari.Rows.Clear();
            //foreach (TKegiatan o in lst)
            //{
            //    string[] row = { o.IDUrusan.ToString(), o.IDProgram.ToString(), o.IDKegiatan.ToString(), o.IDKegiatan.ToString(), o.Nama };
            //    gridResultCari.Rows.Add(row);
            //}

            //splitContainer1.Panel1.Show();
     
            //splitContainer1.SplitterDistance = 500;

            
        }
        //private void FindNodesByString()
        //{
        //    foreach (TreeNode currentNode in tvProgramKegiatan.Nodes)
        //    {
        //        FindNodeByString(currentNode);
        //    }
        //}


        //private void FindNodeByString(TreeNode parentNode)
        //{
        //    FindMatch(parentNode);
        //    foreach (TreeNode currentNode in parentNode.Nodes)
        //    {
        //        FindMatch(currentNode);
        //        FindNodeByString(currentNode);
        //    }
        //}


        //private void FindMatch(TreeNode currentNode)
        //{
        //    if (currentNode.Text.ToUpper().Contains(txtCari.Text.ToUpper()))
        //    {
        //        currentNode.Expand();

        //        //currentNode.ShowCheckBox = true;
        //    }
        //    else
        //    {
        //                  currentNode.Collapse();
        //        //currentNode.ShowCheckBox = false;
        //    }
        //}

        private void cmdTutup_Click(object sender, EventArgs e)
        {

        }

     

        private void TreeProgramKegiatan_Load(object sender, EventArgs e)
        {

        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
