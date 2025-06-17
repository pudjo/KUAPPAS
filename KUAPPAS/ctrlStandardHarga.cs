////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Drawing;
////using System.Data;
////using System.Linq;
////using System.Text;
////using System.Windows.Forms;
////using BP;
////using DTO;
////using Formatting;


////namespace KUAPPAS
////{
////    public partial class ctrlStandardHarga : UserControl
////    {
////        public delegate void ValueChangedEventHandler(StandardBiaya rek);
////        public event ValueChangedEventHandler Changed;
////        public event ValueChangedEventHandler DoubleClicking;
////        List<StandardBiaya> lstSB = new List<StandardBiaya>();
////        StandardBiaya m_oSB = new StandardBiaya();
////        public int MaxLevel;
////        Point m_iPoint;
////        private bool m_bProcessingOnDoubleClickOnly;
////        private List<StandardBiaya> m_oListSB;
////        private List<StandardBiaya> m_oListSBOnGrid;
        
////        private List<TreeNode> NodesThatMatch;

////        public ctrlStandardHarga()
////        {
////            InitializeComponent();
////            m_oListSB = new List<StandardBiaya>();
////            NodesThatMatch = new List<TreeNode>();
////        }

////        private void treeHarga_AfterSelect(object sender, TreeViewEventArgs e)
////        {
            
   
////            if (e.Node != null)
////            {

////                string brgID = e.Node.Tag.ToString().Substring(6);
////                m_oSB = SetSelectedRekening(brgID.Trim());


////                FireSelectionEvent(e.Node);

                

////                //}

////            }

////        }
////        public bool Create(Single _level)
////        {
////            StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
////            //lstSB = sbLogic.Get("");//
            
////            m_oListSB = sbLogic.Get("");//
////            TreeNode root = new TreeNode("STANDARD HARGA");
////            root.Tag = "DUMMY";
////            tvHarga.Nodes.Clear();
////            tvHarga.Nodes.Add(root);
////             //where a.IDParent== parentID
////            var query = from a in m_oListSB //lstSB  
////                        where a.Level==_level
////                        select a;

////            foreach (StandardBiaya rek in query)
////            {
                
////                    TreeNode rootx = new TreeNode( rek.Nama);
////                    //rootx.Tag = rek;
////                    rootx.Tag = "DUMMY" +rek.Level.ToString()+ rek.IDBiaya.ToString().Trim() ;

////                    if (rek.Level <= 6)
////                    {
////                        rootx.Nodes.Add("DUMMY");
////                    }
////                    root.Nodes.Add(rootx);
                    
                
////            }
////            foreach (StandardBiaya sb in m_oListSB)
////            {
////                string[] row = { sb.IDBiaya.ToString(), sb.Nama.ToString(),sb.Uraian, sb.NamaSatuan, sb.Harga.ToRupiahInReport(), ">>" };
////                gridStandardBiaya.Rows.Add(row);

////            }
            
////            return true;
////        }
        

////        public bool Create(string sLEft)
////        {
////            StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);

////            //lstSB = sbLogic.Get(sLEft);//
////            m_oListSB = sbLogic.Get(sLEft);//
////            TreeNode root = new TreeNode("Standard Harga");
////            root.Tag = "DUMMY";
////            tvHarga.Nodes.Clear();
////            tvHarga.Nodes.Add(root);
////             //where a.IDParent== parentID

////            var query = from a in m_oListSB //'lstSB                       
////                        select a;

////            foreach (StandardBiaya rek in query)
////            {
                
////                    TreeNode rootx = new TreeNode( rek.Nama);
////                    //rootx.Tag = rek;
////                    rootx.Tag = "DUMMY" +rek.Level.ToString()+ rek.IDBiaya.ToString().Replace(" ","") ;

////                    if (rek.Level <= 6)
////                    {
////                        rootx.Nodes.Add("DUMMY");
////                    }
////                    root.Nodes.Add(rootx);
                    
                
////            }
////            return true;
////        }

////        public int KodeOn1()
////        {
////            if (m_oSB !=null)
////                if (m_oSB.IDBiaya.ToString().Length > 1)
////                return DataFormat.GetInteger(m_oSB.IDBiaya.Substring(0,1 ));
////            return 0;

////        }
////        public int KodeOn2()
////        {
////            if (m_oSB !=null)
////                if (m_oSB.IDBiaya.ToString().Length>3)
////                return DataFormat.GetInteger(m_oSB.IDBiaya.Substring(1,2 ));
////            return 0;

////        }
////        public int KodeOn3()
////        {
////            if (m_oSB != null)
////                if (m_oSB.IDBiaya.ToString().Length > 5)
////                return DataFormat.GetInteger(m_oSB.IDBiaya.Substring(3, 2));
////            return 0;

////        }
////        public int KodeOn4()
////        {
////            if (m_oSB != null)
////                if (m_oSB.IDBiaya.ToString().Length > 7)
////                return DataFormat.GetInteger(m_oSB.IDBiaya.Substring(5, 2));
////            return 0;

////        }
////        public int KodeOn5()
////        {
////            if (m_oSB != null)
////                return DataFormat.GetInteger(m_oSB.IDBiaya.Substring(7));
////            return 0;

////        }
                

////        private void PopulateTree(string sLeft, TreeNode pNode)
////        {
////            try
////            {
////                StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
////                int level = DataFormat.GetInteger( pNode.Tag.ToString().Substring(5,1));

////                lstSB = sbLogic.GetNextLevel (sLeft.Trim());//           
////                if (pNode.IsExpanded == false)
////                {
////                    foreach (StandardBiaya sb in lstSB)
////                        m_oListSB.Add(sb);

////                }
     

////                var query = from a in lstSB
////                            where a.Level == level + 1 //&& a.IDBiaya.Substring(0,sLeft.Length)== sLeft.Trim()
////                            select a;

////                foreach (StandardBiaya rek in query)
////                {
////                    TreeNode rootx = new TreeNode(rek.Nama);
////                    rootx.Tag = "DUMMY" + rek.Level.ToString() + rek.IDBiaya.ToString().Replace(" ","");
////                    pNode.Nodes.Add(rootx);
////                    //if (rek.Level<=3)
////                    //{
////                    rootx.Nodes.Add("DUMMY");
////                    //}
////                }
////            }
////            catch (Exception e)
////            {
////                MessageBox.Show(e.Message);
////            }
////        }

////        private void tvHarga_AfterSelect(object sender, TreeViewEventArgs e)
////        {
////            FireSelectionEvent(e.Node);
////        }
////        private void FireSelectionEvent(TreeNode node)
////        {
////            if (node.Tag != null)
////            {
////                string rekID;
////                if (node.Tag.ToString().Length > 6)
////                {

////                    //if (node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
////                    //{
////                    //    rekID = node.Tag.ToString().Substring(5);
////                    //}
////                    //else
////                    //{
////                    //    rekID = node.Tag.ToString();
////                    //}
////                    //} else {
////                    //    rekID = Convert.ToInt64(node.Tag.ToString());
////                    //}
////                    if (Changed != null)
////                    {
////                        string brgID = node.Tag.ToString().Substring(6);
////                        m_oSB = SetSelectedRekening(brgID.Trim());


////                       //m_oSB = SetSelectedRekening(rekID.Trim());
////                        Changed(m_oSB);
////                    }
////                }
                
////            }
////        }

////        private  StandardBiaya SetSelectedRekening(string id)
////        {


////            var items = from p in m_oListSB
////                        where p.IDBiaya.Trim()==id 
////                        select p;
////            List<StandardBiaya> lsb = items.ToList();
////            StandardBiaya sb=new StandardBiaya();
////            if (lsb.Count >0)
////                sb = lsb[0];
            
////            return sb;


////        }
////        private  StandardBiaya SetSelectedRekeningFromGrid(string id)
////        {

////            if (m_oListSBOnGrid == null)
////                return null;

////            var items = from p in m_oListSBOnGrid
////                        where p.IDBiaya.Trim()==id 
////                        select p;
////            List<StandardBiaya> lsb = items.ToList();
////            StandardBiaya sb=new StandardBiaya();
////            if (lsb.Count >0)
////                sb = lsb[0];
            
////            return sb;


            

////        }
        

////        private void cmdRefresh_Click(object sender, EventArgs e)
////        {
////            Create("");

////        }
////        private void tvHarga_BeforeExpand(object sender, TreeViewCancelEventArgs e)
////        {
////            if (e.Node.Tag.ToString().Length > 6)
////            {
////                if (e.Node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
////                {
////                    e.Node.Nodes.Clear();
////                    string brgID = e.Node.Tag.ToString().Substring(6);
                   

////                    //m_oSB = SetSelectedRekening(brgID);
////                    PopulateTree(brgID, e.Node);
////                }
////            }
////        }
////        private void treeRekening_Resize(object sender, EventArgs e)
////        {
////           // tvHarga.Move(tvHarga.Top,tvHarga.Left, UserControl.
////        }

////        private void treeRekening_Load(object sender, EventArgs e)
////        {

////        }

////        private void tvHarga_DoubleClick(object sender, EventArgs e)
////        {
            
////        }

////        private void tvHarga_MouseDown(object sender, MouseEventArgs e)
////        {
////            m_iPoint = new Point(e.X, e.Y);

////            if (e.Button == MouseButtons.Left)
////            {
////                TreeNode node = tvHarga.GetNodeAt(m_iPoint);
////                if (node == null)
////                {
////                    return;
////                }
                
////            }
////        }

////        private void tvHarga_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
////        {
            

////        }
////         private void InsertRow()
////        {

            
////        }

////         private void cmdCari_Click(object sender, EventArgs e)
////         {


////         }

////         private void tvHarga_MouseDoubleClick(object sender, MouseEventArgs e)
////         {

////         }

////         private void tvHarga_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
////         {
////             if (e.Node != null)
////             {
////                 if (e.Node.Tag.ToString().Length > 5) { 
////                 string brgID = e.Node.Tag.ToString().Substring(6);
////                 m_oSB = SetSelectedRekening(brgID.Trim());


////                     if (DoubleClicking != null)
////                     {
////                         DoubleClicking(m_oSB);
////                     }

////                 }

////             }
////         }

////         private void cmdCari_Click_1(object sender, EventArgs e)
////         {

////             try{
             
                 
                 
////                 string searchValue = txtCari.Text;
                
////                 StandardBiayaLogic oLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
////              //   List<StandardBiaya> mListUnit = new List<StandardBiaya>();
////                 m_oListSBOnGrid = oLogic.GetByName(searchValue);
////                 gridStandardBiaya.Rows.Clear();
////                 foreach (StandardBiaya sb in m_oListSBOnGrid)
////                 {
////                     string[] row = { sb.IDBiaya.ToString(), sb.Nama.ToString(), sb.Uraian, sb.NamaSatuan, sb.Harga.ToRupiahInReport(), ">>" };
////                     gridStandardBiaya.Rows.Add(row);

////                 }

             

////             }
////             catch (Exception exc)
////             {
////                 //      MessageBox.Show(exc.Message);
////             }

////         }

////         private void gridStandardBiaya_CellContentClick(object sender, DataGridViewCellEventArgs e)
////         {
////             if (e.ColumnIndex == 5)
////             {
                 
////                string brgID = DataFormat.GetString(gridStandardBiaya.Rows[e.RowIndex].Cells[0].Value.ToString());
////                m_oSB = SetSelectedRekeningFromGrid(brgID.Trim());


////                if (DoubleClicking != null)
////                {
////                    DoubleClicking(m_oSB);
////                }

////              }
////          }

////         private void cmdRefresh_Click_1(object sender, EventArgs e)
////         {
////             Create("");

////         }
////     }
////}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using DTO;
using Formatting;


namespace KUAPPAS
{
    public partial class ctrlStandardHarga : UserControl
    {
        public delegate void ValueChangedEventHandler(StandardBiaya rek);
        public event ValueChangedEventHandler Changed;
        public event ValueChangedEventHandler DoubleClicking;
        List<StandardBiaya> lstSB = new List<StandardBiaya>();
        StandardBiaya m_oSB = new StandardBiaya();
        public int MaxLevel;
        Point m_iPoint;
        private bool m_bProcessingOnDoubleClickOnly;
        private List<StandardBiaya> m_oListSB;
        private List<StandardBiaya> m_oListSBOnGrid;
        List<string> lstKodeRekening = new List<string>();


        private List<TreeNode> NodesThatMatch;
        int mprofile;

        public ctrlStandardHarga()
        {
            InitializeComponent();
            m_oListSB = new List<StandardBiaya>();
            NodesThatMatch = new List<TreeNode>();
            gridStandardBiaya.FormatHeader();
            mprofile = 3;
            
        }


        private void treeHarga_AfterSelect(object sender, TreeViewEventArgs e)
        {


            if (e.Node != null && e.Node.Tag !="DUMMY")
            {

                if (e.Node.Tag != null)
                {
                    string brgID = e.Node.Tag.ToString().Substring(6);
                    m_oSB = SetSelectedRekening(brgID.Trim());


                    FireSelectionEvent(e.Node);
                }

            }

        }

        public bool Create2022(List<string> lstIDrekening)
        {


            StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran, 0);

            m_oListSB = sbLogic.Get22(lstIDrekening);//
            TreeNode root = new TreeNode("STANDARD HARGA");
            root.Tag = "DUMMY";
            tvHarga.Nodes.Clear();
            tvHarga.Nodes.Add(root);
            //where a.IDParent== parentID
      
            int jenis = 0;
            TreeNode rootjenis = new TreeNode ();
            foreach (StandardBiaya rek in m_oListSB)
            {

                if (jenis != rek.Kelompok)
                {
                    rootjenis = new TreeNode(rek.Jenis);
                    root.Nodes.Add(rootjenis);
                    jenis = rek.Kelompok;
                }

                TreeNode rootx = new TreeNode(rek.DisplayedText);
                rootx.Tag = "DUMMY" + rek.ID.ToString();
                rootjenis.Nodes.Add(rootx);


            }
            foreach (StandardBiaya sb in m_oListSB)
            {
                string[] row = { sb.IDBiaya.ToString(), sb.DisplayedText, sb.Uraian, sb.NamaSatuan, sb.Harga.ToRupiahInReport(), ">>" };
                gridStandardBiaya.Rows.Add(row);

            }

            return true;
        }

        public bool Create(Single _level, int profile)
        {

            StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran, profile);
            //lstSB = sbLogic.Get("");//
            mprofile = profile;
            m_oListSB = sbLogic.Get("");//
            TreeNode root = new TreeNode("STANDARD HARGA");
            root.Tag = "DUMMY";
            tvHarga.Nodes.Clear();
            tvHarga.Nodes.Add(root);
            //where a.IDParent== parentID
            var query = from a in m_oListSB //lstSB  
                        where a.Level == _level
                        select a;

            foreach (StandardBiaya rek in query)
            {

                TreeNode rootx = new TreeNode(rek.DisplayedText);
                //rootx.Tag = rek;
                rootx.Tag = "DUMMY" + rek.Level.ToString() + rek.IDBiaya.ToString().Trim();

                if (rek.Level <= 6)
                {
                    rootx.Nodes.Add("DUMMY");
                }
                root.Nodes.Add(rootx);


            }
            foreach (StandardBiaya sb in m_oListSB)
            {
                string[] row = { sb.IDBiaya.ToString(), sb.DisplayedText, sb.Uraian, sb.NamaSatuan, sb.Harga.ToRupiahInReport(), ">>" };
                gridStandardBiaya.Rows.Add(row);

            }

            return true;
        }


        public bool Create(string sLEft)
        {
            StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);

            //lstSB = sbLogic.Get(sLEft);//
            m_oListSB = sbLogic.Get(sLEft);//
            TreeNode root = new TreeNode("Standard Harga");
            root.Tag = "DUMMY";
            tvHarga.Nodes.Clear();
            tvHarga.Nodes.Add(root);
            //where a.IDParent== parentID

            var query = from a in m_oListSB //'lstSB                       
                        select a;

            foreach (StandardBiaya rek in query)
            {

                TreeNode rootx = new TreeNode(rek.DisplayedText);
                //rootx.Tag = rek;
                rootx.Tag = "DUMMY" + rek.Level.ToString() + rek.IDBiaya.ToString();

                if (rek.Level <= 6)
                {
                    rootx.Nodes.Add("DUMMY");
                }
                root.Nodes.Add(rootx);


            }
            return true;
        }
        private void PopulateTree(string sLeft, TreeNode pNode)
        {
            try
            {
                StandardBiayaLogic sbLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran,mprofile);
                int level = DataFormat.GetInteger(pNode.Tag.ToString().Substring(5, 1));

                lstSB = sbLogic.GetNextLevel(sLeft.Trim(), level);//           
                if (pNode.IsExpanded == false)
                {
                    foreach (StandardBiaya sb in lstSB)
                        m_oListSB.Add(sb);

                }
                ////int len = sLeft.Trim().Length-1;

                ////var items = from p in m_oListSB
                ////            where p.IDBiaya.Trim().Substring(0, len) == sLeft.Trim()  && p.Level == level + 1
                ////            select p;
                ////List<StandardBiaya> lsb = items.ToList();
                //////StandardBiaya sb = new StandardBiaya();
                //////if (lsb.Count > 0)
                //////    sb = lsb[0];

                //////////return sb;

                ////////  // lstSB 

                var query = from a in lstSB
                            where a.Level == level + 1 //&& a.IDBiaya.Substring(0,sLeft.Length)== sLeft.Trim()
                            select a;

                foreach (StandardBiaya rek in query)
                {
                    TreeNode rootx = new TreeNode(rek.DisplayedText);
                    rootx.Tag = "DUMMY" + rek.Level.ToString() + rek.IDBiaya.ToString();
                    pNode.Nodes.Add(rootx);
                    //if (rek.Level<=3)
                    //{
                    rootx.Nodes.Add("DUMMY");
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void tvHarga_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FireSelectionEvent(e.Node);
        }
        private void FireSelectionEvent(TreeNode node)
        {
            if (node.Tag != null)
            {
                string rekID;
                if (node.Tag.ToString().Length > 6)
                {

                    //if (node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
                    //{
                    //    rekID = node.Tag.ToString().Substring(5);
                    //}
                    //else
                    //{
                    //    rekID = node.Tag.ToString();
                    //}
                    //} else {
                    //    rekID = Convert.ToInt64(node.Tag.ToString());
                    //}
                    if (Changed != null)
                    {
                        string brgID = node.Tag.ToString().Substring(6);
                        m_oSB = SetSelectedRekening(brgID.Trim());


                        //m_oSB = SetSelectedRekening(rekID.Trim());
                        Changed(m_oSB);
                    }
                }

            }
        }
        private StandardBiaya SetSelectedRekening(string id)
        {


            var items = from p in m_oListSB
                        where p.ID == DataFormat.GetInteger(id)
                        select p;
            List<StandardBiaya> lsb = items.ToList();
            StandardBiaya sb = new StandardBiaya();
            if (lsb.Count > 0)
                sb = lsb[0];

            return sb;


        }
        
        private StandardBiaya SetSelectedRekeningFromGrid(string id)
        {

            if (m_oListSB == null)
                return null;

            var items = from p in m_oListSB
                        where p.ID == DataFormat.GetInteger(id)
                        select p;
            List<StandardBiaya> lsb = items.ToList();
            StandardBiaya sb = new StandardBiaya();
            if (lsb.Count > 0)
                sb = lsb[0];

            return sb;




        }


        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Create("");

        }
        private void tvHarga_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag.ToString().Length > 6)
                {
                    if (e.Node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
                    {
                        e.Node.Nodes.Clear();
                        string brgID = e.Node.Tag.ToString().Substring(6);


                        //m_oSB = SetSelectedRekening(brgID);
                        PopulateTree(brgID, e.Node);
                    }
                }
            }
        }
        private void treeRekening_Resize(object sender, EventArgs e)
        {
            // tvHarga.Move(tvHarga.Top,tvHarga.Left, UserControl.
        }

        private void treeRekening_Load(object sender, EventArgs e)
        {

        }

        private void tvHarga_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tvHarga_MouseDown(object sender, MouseEventArgs e)
        {
            m_iPoint = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = tvHarga.GetNodeAt(m_iPoint);
                if (node == null)
                {
                    return;
                }

            }
        }

        private void tvHarga_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {


        }
        private void InsertRow()
        {


        }

        private void cmdCari_Click(object sender, EventArgs e)
        {


        }

        private void tvHarga_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void tvHarga_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {

            
                string brgID = e.Node.Tag.ToString().Substring(5);
                m_oSB = SetSelectedRekening(brgID.Trim());


                if (DoubleClicking != null)
                {
                    DoubleClicking(m_oSB);
                }

                //}

            }
        }

        private void cmdCari_Click_1(object sender, EventArgs e)
        {

            try
            {



                string searchValue = txtCari.Text;

                StandardBiayaLogic oLogic = new StandardBiayaLogic(GlobalVar.TahunAnggaran);
                //   List<StandardBiaya> mListUnit = new List<StandardBiaya>();
                //m_oListSBOnGrid = oLogic.GetByName(searchValue);
                //foreach()


                //var result = from d in lstSB where d.Uraian.Contains(searchValue);


                gridStandardBiaya.Rows.Clear();
                foreach (StandardBiaya sb in m_oListSB)
                {
                    if (sb.Uraian.ToUpper().Trim().Contains(searchValue.ToUpper().Trim()))
                    {
                        string[] row = { sb.IDBiaya.ToString(), sb.Nama.ToString(), sb.Uraian, sb.NamaSatuan, sb.Harga.ToRupiahInReport(), ">>",sb.ID.ToString() };
                        gridStandardBiaya.Rows.Add(row);
                    }

                }


                //var items = from p in m_oListSB
                //                                          where p.Nama.ToUpper().Contains(searchValue.ToUpper()) ||
                //                                              p.Uraian.ToUpper().Contains(searchValue.ToUpper())
                //                                          select p;

                //                                   ;
                //                               //    select p;
                //                              //select p;

                //                                   List<StandardBiaya> lsb = items.ToList();
                //                                   gridStandardBiaya.Rows.Clear();

                //                                   foreach (StandardBiaya sb in lsb)
                //                                   {
                //                                       string[] row = { sb.IDBiaya.ToString(), sb.Nama.ToString(), sb.Uraian, sb.NamaSatuan, sb.Harga.ToRupiahInReport(), ">>" };
                //                                       gridStandardBiaya.Rows.Add(row);

                //                                   }

            }
            catch (Exception exc)
            {
                //      MessageBox.Show(exc.Message);
            }

        }

        private void gridStandardBiaya_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {

                string brgID = DataFormat.GetString(gridStandardBiaya.Rows[e.RowIndex].Cells[6].Value.ToString());
                m_oSB = SetSelectedRekeningFromGrid(brgID.Trim());


                if (DoubleClicking != null)
                {
                    DoubleClicking(m_oSB);
                }

            }
        }

        private void cmdRefresh_Click_1(object sender, EventArgs e)
        {
            Create(0, mprofile);

        }
    }
}
