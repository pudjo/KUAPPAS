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
    public partial class TreeSumberDana : UserControl
    {

        public delegate void ValueChangedEventHandler(SumberDana rek);
        public event ValueChangedEventHandler Changed;
        public event ValueChangedEventHandler DoubleClicking;
        List<SumberDana> lstSumberDana = new List<SumberDana>();
        SumberDana m_oSumberDana = new SumberDana();
        public int MaxLevel;

        Point m_iPoint;

        private bool m_bProcessingOnDoubleClickOnly;
        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;

        public TreeSumberDana()
        {
            InitializeComponent();
        }

        private void tvSumberDana_AfterSelect(object sender, TreeViewEventArgs e)
        {

            FireSelectionEvent(e.Node);
        }
        private void FireSelectionEvent(TreeNode node)
        {
            if (node.Tag != null)
            {
                long sID;
                if (node.Tag.ToString().Length > 6)
                {

                    if (node.Tag.ToString().Substring(1, 6).Equals("DUMMY"))
                    {
                        sID = 0;//Convert.ToInt64(node.Tag.ToString().Substring(1,);
                    }
                    else
                    {
                      //  sID = Convert.ToInt64(node.Tag.ToString().Substring(6);
                        sID = Convert.ToInt64(node.Tag.ToString().Substring(6));
                    }

                    if (Changed != null)
                    {
                        m_oSumberDana = SetSelectedSumberDana(sID);
                        Changed(m_oSumberDana);
                    }
                }

            }
        }
        public void Create()
        {


            SumberDanaLogic sumberDanaLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
            lstSumberDana = new List<SumberDana>();

            lstSumberDana = sumberDanaLogic.Get();            
            TreeNode root = new TreeNode("Sumber Dana");
            root.Tag = "DUMMY";
            tvSumberDana.Nodes.Clear();
            tvSumberDana.Nodes.Add(root);

            var query = from a in lstSumberDana
                        where a.IIDParent== 0
                        select a;

            foreach (SumberDana sd in query)
            {
                   TreeNode rootx = new TreeNode(sd.IDRekening.ToString() + "  " + sd.Nama);
                    //rootx.Tag = rek;
                    rootx.Tag = "DUMMY" +sd.Root.ToString()+ sd.IDRekening.ToString();
                    if (sd.Root <=6)
                    {
                        rootx.Nodes.Add("DUMMY");
                    }
                    root.Nodes.Add(rootx);
             }        
        }
           private SumberDana SetSelectedSumberDana(long id)
        {
            SumberDanaLogic sbLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran) ;//, RekeningLogic.E_REKENING_TYPE.REKENING_13, mProfile);
            SumberDana sb = sbLogic.GetByIIDRekening(id);

            return sb;

            

        }

        private void PopulateTree(long id, TreeNode pNode)
        {
            try
            {
                SumberDanaLogic sbLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
        

                var query = from a in lstSumberDana
                            where a.IIDParent == id
                            orderby a.ID
                            select a;
                foreach (SumberDana sd in query)
                {
                    //TreeNode rootx = new TreeNode(rek.Tampilan + "  " + rek.Nama);
                    TreeNode rootx = new TreeNode(sd.IDRekening.ToString() + "  " + sd.Nama);

                    rootx.Tag = "DUMMY" + sd.Root.ToString() + sd.IDRekening.ToString();
                    pNode.Nodes.Add(rootx);
                    if (sd.Root <= 6)
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

        private void tvSumberDana_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag.ToString().Length > 6)
            {
                if (e.Node.Tag.ToString().Substring(0, 5).Equals("DUMMY"))
                {
                    e.Node.Nodes.Clear();
                    long brgID = Convert.ToInt64(e.Node.Tag.ToString().Substring(6));
                    //  e.Node.Tag = e.Node.Tag.ToString().Substring(5);
                   // m_oRekening = SetSelectedRekening(brgID);
                    PopulateTree(brgID, e.Node);
                }
            }
        }
     

        private void tvSumberDana_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node != null)
            {
                try
                {
                    long brgID = Convert.ToInt64(e.Node.Tag.ToString().Substring(6));
                    m_oSumberDana = SetSelectedSumberDana(brgID);

                    if (DoubleClicking != null)
                    {
                        DoubleClicking(m_oSumberDana);
                    }

                }
                catch (Exception ex)
                {

                }

            }

        
        }
    }
}
