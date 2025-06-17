using System;
using System.Collections.Generic;
using System.Collections;

using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP;
using DTO;
using Formatting;


namespace KUAPPAS
{
    public partial class treeRekening : UserControl
    {
        public delegate void ValueChangedEventHandler(Rekening rek);
        public event ValueChangedEventHandler Changed;
        public event ValueChangedEventHandler DoubleClicking;
        List<Rekening> lstRekening = new List<Rekening>();
        Rekening m_oRekening = new Rekening();
        public int MaxLevel;

        Point m_iPoint;

        private bool m_bProcessingOnDoubleClickOnly;
        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        private Single m_iJenis;
        private int mProfile;
        public treeRekening()
        {
            InitializeComponent();
            MaxLevel = 6;
            m_iPoint.X = 0;
            m_iPoint.Y = 0;
            m_bProcessingOnDoubleClickOnly = false;
            mProfile = 1;
        
        }
        public int Profile { set { mProfile = value; }
            get { return mProfile; }
        }
        public void DoubleClickOnLeafOnly(bool _b)
        {
            m_bProcessingOnDoubleClickOnly = _b;
        }
        public void SetMaxLevel(int pMax)
        {
            MaxLevel = pMax;
        }

        public void WithRefresh(bool _bRefresh)
        {
            cmdRefresh.Visible = _bRefresh;
        }
        public void Create(long parentID = 0)
        {
            RekeningLogic rekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13,mProfile);
          //  lstRekening = rekeningLogic.GetChildOf(parentID);  
            GlobalVar.gListRekening = rekeningLogic.Get();  
            TreeNode root = new TreeNode("Kode Rekening");
            root.Tag = "DUMMY";
            tvRekening.Nodes.Clear();
            tvRekening.Nodes.Add(root);

            var query = from a in GlobalVar.gListRekening
                        where a.IDParent== parentID
                        select a;

            foreach (Rekening rek in query)
            {
                if (rek.IDParent== parentID)
                {
                    TreeNode rootx = new TreeNode(rek.Tampilan + "  " + rek.Nama);
                    //rootx.Tag = rek;
                    rootx.Tag = "DUMMY" +rek.Leaf.ToString()+ rek.ID.ToString();
                    if (rek.Root <= MaxLevel)
                    {
                        rootx.Nodes.Add("DUMMY");
                    }
                    root.Nodes.Add(rootx);
                    
                }
            }
        
            m_iJenis = 0;
            if (parentID.ToString().Length > 2)
            {
                if (parentID.ToString().Substring(0, 1) == "4")
                {
                    m_iJenis = 1;
                }
                if (GlobalVar.PP90)
                {
                    if (parentID.ToString().Substring(0, 1) == "5")
                    {
                        m_iJenis = 3;
                    }
                }
                else
                {
                    if (parentID.ToString().Substring(0, 2) == "51")
                    {
                        m_iJenis = 2;
                    }

                    if (parentID.ToString().Substring(0, 2) == "52")
                    {
                        m_iJenis = 3;
                    }
                }
                if (parentID.ToString().Substring(0, 2) == "61")
                {
                    m_iJenis = 4;
                }
                if (parentID.ToString().Substring(0, 2) == "62")
                {
                    m_iJenis = 5;
                }
            }

        }
        public void CreateForJurnal(string parentID = "")
        {
            RekeningLogic rekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13, mProfile);
            //  lstRekening = rekeningLogic.GetChildOf(parentID);  
            GlobalVar.gListRekening = rekeningLogic.GetLike(parentID);
            TreeNode root = new TreeNode("Kode Rekening");
            root.Tag = "DUMMY";
            tvRekening.Nodes.Clear();
            tvRekening.Nodes.Add(root);



            long parent = DataFormat.GetLong(parentID);
            int lenCurrent = parentID.Length;
            for (int i = lenCurrent; i < 12; i++)
            {
                parent = parent * 10; 
            }


                foreach (Rekening rek in GlobalVar.gListRekening)
                {
                    if (rek.IDParent == parent)
                    {
                        TreeNode rootx = new TreeNode(rek.Tampilan + "  " + rek.Nama);
                        //rootx.Tag = rek;
                        rootx.Tag = "DUMMY" + rek.Leaf.ToString() + rek.ID.ToString();
                        if (rek.Root <= MaxLevel)
                        {
                            rootx.Nodes.Add("DUMMY");
                        }
                        root.Nodes.Add(rootx);

                    }
                }

            m_iJenis = 0;
           

        }
        public void CreateForJurnal(long  parentID = 0)
        {
            RekeningLogic rekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13, mProfile);
            //  lstRekening = rekeningLogic.GetChildOf(parentID);  
            GlobalVar.gListRekening = rekeningLogic.GetLike(parentID.ToString());
            TreeNode root = new TreeNode("Kode Rekening");
            root.Tag = "DUMMY";
            tvRekening.Nodes.Clear();
            tvRekening.Nodes.Add(root);



            long parent = DataFormat.GetLong(parentID);
            int lenCurrent = parentID.ToString().Length;
            if (parentID == 4 || parentID == 5 )
            {
                parent = 0;
            }
            else
            {

                for (int i = lenCurrent; i < 12; i++)
                {
                    parent = parent * 10;
                }
            }


            foreach (Rekening rek in GlobalVar.gListRekening)
            {
                if (rek.IDParent == parent)
                {
                    TreeNode rootx = new TreeNode(rek.Tampilan + "  " + rek.Nama);
                    //rootx.Tag = rek;
                    rootx.Tag = "DUMMY" + rek.Leaf.ToString() + rek.ID.ToString();
                    if (rek.Root <= MaxLevel)
                    {
                        rootx.Nodes.Add("DUMMY");
                    }
                    root.Nodes.Add(rootx);

                }
            }

            m_iJenis = 0;


        }
        private void PopulateTree(long id, TreeNode pNode)
        {
            try
            {
                //RekeningLogic rekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
                //RekeningLogic rekeningLogic = new  RekeningLogic(GlobalVar.TahunAnggaran,RekeningLogic.E_REKENING_TYPE.REKENING_13,mProfile);

                ////lstRekening = rekeningLogic.GetChildOf(id);

                //var query = from a in GlobalVar.gListRekening
                //            where a.IDParent== id
                //            orderby a.ID
                //            select a;

                List<Rekening> query = GlobalVar.gListRekening.FindAll(x => x.IDParent == id);
                if (query == null) return;
                foreach (Rekening rek in query)
                {
                    string tampilan = rek.ID.ToString().ToKodeRekening (rek.Root);
                    TreeNode rootx = new TreeNode(tampilan + "  " + rek.Nama);
                    rootx.Tag = "DUMMY" + rek.Leaf.ToString() + rek.ID.ToString();
                    pNode.Nodes.Add(rootx);
                    if (rek.Root<= MaxLevel)
                    {
                        rootx.Nodes.Add("DUMMY");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void tvRekening_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FireSelectionEvent(e.Node);
        }
        private void FireSelectionEvent(TreeNode node)
        {
            try
            {
                if (node.Tag != null)
                {
                    long rekID;
                    if (node.Tag.ToString().Length > 6)
                    {

                        if (node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
                        {
                            rekID = Convert.ToInt64(node.Tag.ToString().Substring(6));
                        }
                        else
                        {
                            rekID = Convert.ToInt64(node.Tag.ToString());
                        }
                        //} else {
                        //    rekID = Convert.ToInt64(node.Tag.ToString());
                        //}
                        if (Changed != null)
                        {
                            m_oRekening = SetSelectedRekening(rekID);
                            if (m_oRekening != null)
                                Changed(m_oRekening);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        private Rekening SetSelectedRekening(long id)
        {
            //RekeningLogic rekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran);
    //        RekeningLogic rekeningLogic = new RekeningLogic(GlobalVar.TahunAnggaran, RekeningLogic.E_REKENING_TYPE.REKENING_13, mProfile);
     

    ////        RekeningLogic rekeningLogic = new 
    //        Rekening rek = rekeningLogic.GetByID(id);
            Rekening rek =  GlobalVar.gListRekening.FirstOrDefault(x => x.ID == id);
            if (rek == null)
            {
                MessageBox.Show("Rekenig salah");
            }
            return rek;

            

        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Create();

        }
        private void tvRekening_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Tag.ToString().Length > 6)
                {
                    if (e.Node.Tag.ToString().Contains("DUMMY"))
                    {
                        e.Node.Nodes.Clear();
                        long brgID = Convert.ToInt64(e.Node.Tag.ToString().Substring(6));
                        //  e.Node.Tag = e.Node.Tag.ToString().Substring(5);
                        m_oRekening = SetSelectedRekening(brgID);
                        if (m_oRekening != null)
                            PopulateTree(brgID, e.Node);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeRekening_Resize(object sender, EventArgs e)
        {
           // tvRekening.Move(tvRekening.Top,tvRekening.Left, UserControl.
        }

        private void treeRekening_Load(object sender, EventArgs e)
        {

        }

        private void tvRekening_DoubleClick(object sender, EventArgs e)
        {
            
            //m_iPoint
        }

        private void tvRekening_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                m_iPoint = new Point(e.X, e.Y);

                if (e.Button == MouseButtons.Left)
                {
                    TreeNode node = tvRekening.GetNodeAt(m_iPoint);
                    if (node == null)
                    {
                        return;
                    }
                    //int iLevel = GetLevel(node);
                    //if (iLevel < 3)
                    //{
                    //    int pID = GetID(node);

                    //    if (Changed != null)
                    //    {
                    //        Changed(pID, iLevel);

                    //    }
                    //}
                    //else
                    //{
                    //    long lID = GetLongID(node);
                    //    if (ChangedSubUnit != null)
                    //    {
                    //        ChangedSubUnit(lID);

                    //    }


                    //}

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tvRekening_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    long brgID = Convert.ToInt64(e.Node.Tag.ToString().Substring(6));
                    m_oRekening = SetSelectedRekening(brgID);
                    if (m_oRekening == null)
                    {
                        return;
                    }
                    if (m_bProcessingOnDoubleClickOnly == true)
                    {
                        if (e.Node.Tag.ToString().Substring(5, 1) == "1")
                        {
                            if (DoubleClicking != null)
                            {
                                DoubleClicking(m_oRekening);
                                //e.Node.IsVisible
                            }
                        }
                    }
                    else
                    {
                        if (DoubleClicking != null)
                        {
                            DoubleClicking(m_oRekening);
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
         private void InsertRow()
        {

            //int _idKegiatan =0;
            //_idKegiatan=DataFormat.GetInteger(gridKUA.Rows[_barisKUA].Cells[0].Value);

            //string[] row ={
                              
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[0].Value),
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[1].Value),
            //                      "Hapus",
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[3].Value),
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[4].Value),
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[5].Value),
            //                      (DataFormat.GetInteger (gridKUA.Rows[_barisKUA].Cells[6].Value)+1).ToString(),                                  
            //                      "","0",
            //                      "0","",
            //                      "","","",  // dusun
            //                      "0","0",  //valk,vald                                  
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[16].Value),
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[17].Value),
            //                      DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[18].Value),"1"
                                  
                                 

            //              };


            ////string[] row ={DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[0].Value),"Hapus",
            ////                     DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[2].Value),"",
            ////                     (DataFormat.GetInteger (gridKUA.Rows[_barisKUA].Cells[4].Value)+1).ToString(),"","","0",
            ////                     DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[8].Value),
            ////                     DataFormat.GetString(gridKUA.Rows[_barisKUA].Cells[9].Value)};
            //gridKUA.Rows.Insert(_barisKUA + 1, row);
            //_barisKUA++;
            //for (int i = _barisKUA; i < gridKUA.Rows.Count; i++)
            //{
            //    if (_idKegiatan == DataFormat.GetInteger(gridKUA.Rows[i].Cells[0].Value))
            //    {
            //        gridKUA.Rows[i].Cells[19].Value = "1";
            //    }
            //}
        }

         private void cmdCari_Click(object sender, EventArgs e)
         {
             var searchFor = txtSearch.Text.Trim().ToUpper();

             tvRekening.ExpandAll();

             Putihkan(tvRekening.Nodes);

             if (searchFor != "")
             {
                 if (tvRekening.Nodes.Count > 0)
                 {
                     if (SearchRecursive(tvRekening.Nodes, searchFor))
                     {
                         tvRekening.SelectedNode.Expand();
                         tvRekening.Focus();
                     }
                 }
             }

             
             
            
 
         }
         private bool SearchRecursive(IEnumerable nodes, string searchFor)
         {
             foreach (TreeNode node in nodes)
             {
                 if (node != null)
                 {
                     if (node.Text.ToUpper().Contains(searchFor))
                     {
                         tvRekening.SelectedNode = node;
                         node.BackColor = Color.Yellow;
                     }

                     if (SearchRecursive(node.Nodes, searchFor))
                         return true;
                 }
             }
             return false;
         }
         private bool Putihkan(IEnumerable nodes)
         {
             foreach (TreeNode node in nodes)
             {
                 if (node != null)
                 {

                     tvRekening.SelectedNode = node;
                     node.BackColor = Color.White;


                     if (Putihkan(node.Nodes))
                         return true;
                 }
             }
             return false;
         }

        // **** GABRBAGE PENCARIAN
        //string searchText = this.txtSearch.Text;

        //     if (String.IsNullOrEmpty(searchText))
        //     {
        //         return;
        //     };


        //     if (LastSearchText != searchText)
        //     {
        //         //It's a new Search
        //         CurrentNodeMatches.Clear();
        //         LastSearchText = searchText;
        //         LastNodeIndex = 0;
        //         SearchNodes(searchText, tvRekening.Nodes[0]);
        //     }

        //     if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
        //     {
        //         TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
        //         LastNodeIndex++;
        //         this.tvRekening.SelectedNode = selectedNode;
        //         this.tvRekening.SelectedNode.Expand();
        //         this.tvRekening.Select();

        //     }
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
                     SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                 };
                 StartNode = StartNode.NextNode;
             };

         }

         private void tvRekening_Click(object sender, EventArgs e)
         {

         }

         private void cmdTutup_Click(object sender, EventArgs e)
         {
            
         }

         private void tvRekening_AfterSelect_1(object sender, TreeViewEventArgs e)
         {

         }

        
    }
}
