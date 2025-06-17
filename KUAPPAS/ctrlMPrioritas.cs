using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using Formatting;
using DTO;

namespace KUAPPAS
{
    public partial class ctrlMPrioritas : UserControl
    {
        public delegate void ValueChangedEventHandler(PrioritasNasional rek);
        public event ValueChangedEventHandler Changed;
        public event ValueChangedEventHandler DoubleClicking;
        List<PrioritasNasional> lstPrioritasNasional = new List<PrioritasNasional>();
        PrioritasNasional m_oPrioritasNasional = new PrioritasNasional();
        Point m_iPoint;

        private bool m_bProcessingOnDoubleClickOnly;
        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        private Single m_iJenis;

        public int MaxLevel;
        public ctrlMPrioritas()
        {
            InitializeComponent();
            
            MaxLevel = 6;
            m_iPoint.X = 0;
            m_iPoint.Y = 0;
            m_bProcessingOnDoubleClickOnly = false;

        }

        private void treePrioritas_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        public void DoubleClickOnLeafOnly(bool _b)
        {
            m_bProcessingOnDoubleClickOnly = _b;
        }
        public void SetMaxLevel(int pMax)
        {
            MaxLevel = pMax;
        }

        public void Create(long parentID = 0)
        {
            PrioritasNasionalLogic PrioritasNasionalLogic = new PrioritasNasionalLogic(GlobalVar.TahunAnggaran);
            lstPrioritasNasional = PrioritasNasionalLogic.Get (GlobalVar.TahunAnggaran);            
            TreeNode root = new TreeNode("Kode PrioritasNasional");
            root.Tag = "DUMMY";
            treePrioritas.Nodes.Clear();
            treePrioritas.Nodes.Add(root);

            var query = from a in lstPrioritasNasional                        
                        select a;

            foreach (PrioritasNasional rek in query)
            {
                
                    TreeNode rootx = new TreeNode(rek.Kode + "  " + rek.Nama);
                    //rootx.Tag = rek;
                    rootx.Tag = "DUMMY" +rek.Kode.ToString();
                    
                    root.Nodes.Add(rootx);
                    
             }
        }          

        private void PopulateTree(int  id, TreeNode pNode)
        {
            try
            {
                PrioritasNasionalLogic PrioritasNasionalLogic = new PrioritasNasionalLogic(GlobalVar.TahunAnggaran);
                lstPrioritasNasional = PrioritasNasionalLogic.GetChild(GlobalVar.TahunAnggaran, id);            

                var query = from a in lstPrioritasNasional
                            where a.Induk== id
                            orderby a.Nomor
                            select a;
                foreach (PrioritasNasional rek in query)
                {
                    TreeNode rootx = new TreeNode(rek.Kode + "  " + rek.Nama);
                    rootx.Tag = "DUMMY" + rek.Nomor.ToString();
                    pNode.Nodes.Add(rootx);
                    
                    rootx.Nodes.Add("DUMMY");
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //private void treePrioritas_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    FireSelectionEvent(e.Node);
        //}
        private void FireSelectionEvent(TreeNode node)
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
                       // m_oPrioritasNasional = SetSelectedPrioritasNasional(rekID);
                        Changed(m_oPrioritasNasional);
                    }
                }
                
            }
        }
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Create();

        }
        private void treePrioritas_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag.ToString().Length > 6)
            {
                if (e.Node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
                {
                    e.Node.Nodes.Clear();
                    int brgID = Convert.ToInt32(e.Node.Tag.ToString().Substring(6));                  
                    //m_oPrioritasNasional = SetSelectedPrioritasNasional(brgID);
                    PopulateTree(brgID, e.Node);
                }
            }
        }

        private void treePrioritasNasional_Resize(object sender, EventArgs e)
        {
           // treePrioritas.Move(treePrioritas.Top,treePrioritas.Left, UserControl.
        }

        private void treePrioritasNasional_Load(object sender, EventArgs e)
        {

        }

        private void treePrioritas_DoubleClick(object sender, EventArgs e)
        {
            
            //m_iPoint
        }

        private void treePrioritas_MouseDown(object sender, MouseEventArgs e)
        {
            m_iPoint = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = treePrioritas.GetNodeAt(m_iPoint);
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

        private void treePrioritas_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {
                long brgID = Convert.ToInt64(e.Node.Tag.ToString().Substring(6));                
                //m_oPrioritasNasional = SetSelectedPrioritasNasional(brgID);
                if (m_bProcessingOnDoubleClickOnly == true)
                {
                    if (e.Node.Tag.ToString().Substring(5,1)=="1"){
                        if (DoubleClicking !=null){
                            DoubleClicking (m_oPrioritasNasional);
                            //e.Node.IsVisible
                        }
                    }
                }
                else
                {
                    if (DoubleClicking != null)
                    {
                        DoubleClicking(m_oPrioritasNasional);
                    }

                }

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

         //private void cmdCari_Click(object sender, EventArgs e)
         //{
         //   string sCari = txtSearch.Text;
         //   PrioritasNasionalLogic PrioritasNasionalLogic = new PrioritasNasionalLogic();
         //   lstPrioritasNasional = PrioritasNasionalLogic.GetByName(sCari,5);
         //   gridPrioritasNasional.Rows.Clear();

         //   foreach (PrioritasNasional rek in lstPrioritasNasional)
         //   {
         //       bool bAdd =false;

         //       switch ((int)m_iJenis){
         //           case 1:
         //               if ( rek.ID.ToString().Substring(0,1)=="4"){
         //                   bAdd= true;
         //               }
         //               break;
         //           case 2:
         //               if ( rek.ID.ToString().Substring(0,2)=="51"){
         //                   bAdd= true;
         //               }
         //               break;
         //           case 3:
         //           if ( rek.ID.ToString().Substring(0,2)=="52"){
         //                   bAdd= true;
         //               }
         //               break;
         //           case 4:
         //           if ( rek.ID.ToString().Substring(0,2)=="61"){
         //                   bAdd= true;
         //               }
         //               break;
         //           case 5:
         //           if ( rek.ID.ToString().Substring(0,2)=="62"){
         //                   bAdd= true;
         //               }
         //           break;                }

         //       if (bAdd == true)
         //       {
         //           string[] row = { rek.ID.ToString(), rek.ID.ToKodePrioritasNasional(), rek.Nama, "Pilih" };
         //           gridPrioritasNasional.Rows.Add(row);
         //       }
         //   }
         //   splitContainer1.Panel1.Show();
         //   splitContainer1.SplitterDistance = splitContainer1.Height / 2;
 
         //}


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
        //         SearchNodes(searchText, treePrioritas.Nodes[0]);
        //     }

        //     if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
        //     {
        //         TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
        //         LastNodeIndex++;
        //         this.treePrioritas.SelectedNode = selectedNode;
        //         this.treePrioritas.SelectedNode.Expand();
        //         this.treePrioritas.Select();

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


        

    }
}
