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
    public partial class treeSKPD : UserControl
    {
        private TreeNode m_OldSelectNode;
        private int m_iPemda = 0;

        public enum MAX_LEVEL
        {
            LEVEL_SKPD = 1,
            LEVEL_UNIT = 2,
            LEVEL_SUBUNIT = 3,
        };

        public delegate void ValueChangedEventHandler(object sender, EventArgs e, Single iType, Single ID);
        public delegate EventResponseMessage SelectedEventHandler(int ID, Single ObjectType);
        public delegate EventResponseMessage SelectedSubUnitEventHandler(long ID);
        public event SelectedEventHandler Changed;
        public event SelectedSubUnitEventHandler ChangedSubUnit;

        private SKPD m_oSKPD;
        private Unit m_oUnit;
        private SubUnit m_oSubUnit;

        private Point m_iPoint;
        private MAX_LEVEL m_iMaxLevel;                

        public treeSKPD()
        {

            InitializeComponent();
            m_oSKPD = null;
            m_oUnit = null;
            m_oSubUnit = null;
        }

        private void treeSKPD_Load(object sender, EventArgs e)
        {

        }
        private void treeDinas_Load(object sender, EventArgs e)
        {

        }
        public void Create(int Pemda, MAX_LEVEL maxLevel)
        {
            SKPDLogic oDinasLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            m_iPemda = Pemda;
            m_iMaxLevel = maxLevel;
            tvDinas.Nodes.Clear();
            TreeNode root = new TreeNode("DINAS..");
            root.Tag = "0DUMMY0";
            tvDinas.Nodes.Add(root);
            root.Nodes.Clear();
            List<SKPD> lstSKPD = oDinasLogic.Get((int)GlobalVar.TahunAnggaran);// new List<Negara>();
            if (lstSKPD == null)
            {
                //MessageBox.Show(oDinasLogic)
                return;
            }
            var query = from n in lstSKPD
                        orderby n.Kode
                        select n;
            foreach (SKPD skpd in query)
            {
                if (GlobalVar.Pengguna.IsUserDinas == 1)
                {
                    foreach (PenggunaDinas pd in GlobalVar.Pengguna.lstDinas)
                    {
                        if (pd.SKPD == skpd.ID)
                        {
                            TreeNode node = new TreeNode();
                            node.Tag = "1DUMMY" + skpd.ID.ToString();
                            node.Text = skpd.Tampilan;// +"  " + skpd.Nama;
                            root.Nodes.Add(node);
                            if (maxLevel > MAX_LEVEL.LEVEL_SKPD)
                            {
                                node.Nodes.Add("DUMMY");
                            }
                        }
                    }

                }
                else
                {
                    TreeNode node = new TreeNode();
                    node.Tag = "1DUMMY" + skpd.ID.ToString();
                    node.Text = skpd.Tampilan + "  " + skpd.Nama;
                    root.Nodes.Add(node);
                    if (maxLevel > MAX_LEVEL.LEVEL_SKPD)
                    {
                        node.Nodes.Add("DUMMY");
                    }
                }
                
            }

        }

        private void LoadSKPD(){

            SKPDLogic oDinasLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
          //  m_iMaxLevel = maxLevel;
            TreeNode root = tvDinas.Nodes[0];// new TreeNode("DINAS..");
            //root.Tag = "0DUMMY0";
            //tvDinas.Nodes.Add(root);
            root.Nodes.Clear();
            List<SKPD> lstSKPD = oDinasLogic.Get((int)GlobalVar.TahunAnggaran);// new List<Negara>();
            var query = from n in lstSKPD
                        orderby n.Kode
                        select n;
            foreach (SKPD skpd in query)
            {
                if (GlobalVar.Pengguna.IsUserDinas == 1)
                {
                    foreach (PenggunaDinas pd in GlobalVar.Pengguna.lstDinas)
                    {
                        if (pd.SKPD == skpd.ID)
                        {
                            TreeNode node = new TreeNode();
                            node.Tag = "1DUMMY" + skpd.ID.ToString();
                            node.Text = skpd.Tampilan;// +"  " + skpd.Nama;
                            root.Nodes.Add(node);
                            if (m_iMaxLevel > MAX_LEVEL.LEVEL_SKPD)
                            {
                                node.Nodes.Add("DUMMY");
                            }
                        }
                    }

                }
                else
                {
                    TreeNode node = new TreeNode();
                    node.Tag = "1DUMMY" + skpd.ID.ToString();
                    node.Text = skpd.Tampilan;// +" " + skpd.Nama;
                    root.Nodes.Add(node);
                    if (m_iMaxLevel > MAX_LEVEL.LEVEL_SKPD)
                    {
                        node.Nodes.Add("DUMMY");
                    }
                }

            }
        }
        private void LoadUnit(int ID, TreeNode node)
        {
            UnitKerjaLogic oUnitKerjaLogic = new UnitKerjaLogic(GlobalVar.TahunAnggaran);
            List<Unit> lstUnit = oUnitKerjaLogic.Get();// new List<Negara>();

            var query = from n in lstUnit
                        where n.SKPD== ID
                        orderby n.Kode
                        select n;
            node.Nodes.Clear();
            foreach (Unit oUnit in query)
            {
                TreeNode nodprov = new TreeNode();
                nodprov.Tag = "2DUMMY" + oUnit.ID.ToString();
                nodprov.Text = oUnit.Tampilan;// DisplayedCode + " " + oUnit.Nama;
                node.Nodes.Add(nodprov);
                if (m_iMaxLevel > MAX_LEVEL.LEVEL_UNIT)
                {
                    nodprov.Nodes.Add("DUMMY");

                }
            }
        }
        //private void LoadSubUnit(int ID, TreeNode node)
        //{
        //    SubUnitKerjaLogic oSubUnitKerjaLogic = new SubUnitKerjaLogic();
        //    List<SubUnit> lstSubUnit = oSubUnitKerjaLogic.Get();// new List<Negara>();

        //    var query = from n in lstSubUnit
        //                where n.IDUnit == ID
        //                orderby n.Kode
        //                select n;
        //    node.Nodes.Clear();
        //    foreach (SubUnit oSubUnit in query)
        //    {
        //        TreeNode nodprov = new TreeNode();
        //        nodprov.Tag = "3DUMMY" + oSubUnit.ID.ToString();
        //        nodprov.Text = oSubUnit.DisplayedCode + " " + oSubUnit.Nama;
        //        node.Nodes.Add(nodprov);
        //       // if (m_iMaxLevel > MAX_LEVEL.LEVEL_UNIT)
        //        //{
        //          //  nodprov.Nodes.Add("DUMMY");

        //        //}
        //    }
        //}

        /// <summary>
        /// Refresh after Add/Edit Delete
        /// </summary>
        /// <param name="node"></param>
        private void RefreshNode(TreeNode node)
        {
            int iLevel = GetLevel(node);
            int lID;
            lID = GetID(node);            
            node.Nodes.Clear();            
            switch (iLevel)
            {
                case 0:
                    LoadSKPD();//1, m_iMaxLevel);
                    break;
                case 1:
                    LoadUnit(lID, node);                    
                    break;
                case 2:
                    //LoadSubUnit(lID, node);
                    
                    break;
                
            }
            node.Expand();
            //}
        }

        private void tvDinas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
           // TreeNode node = tvDinas.SelectedNode;
          //  TreeNode parentNode;
            /*
            if (node.Tag != null){
                string stag = node.Tag.ToString();
                if (stag.Length < 6)
                    return;

                string sType = stag.Substring(0, 1);
                string sID = stag.Substring(6);
                int pID = Convert.ToInt32(sID);
                int type = 0;
                type = Convert.ToInt32(sType);
                if (Changed != null)
                {
                    m_oSKPD = null;
                    m_oSubUnit = null;
                    m_oUnit = null;


                    switch (type)
                    {
                        case 1:
                            m_oSKPD = GetSKPD(pID);
                            LoadUnit(pID, node);
                            break;
                        case 2:
                            m_oUnit = GetUnit(pID);
                            parentNode = node.Parent;
                            stag = parentNode.Tag.ToString();
                            sID = stag.Substring(6);
                            pID = Convert.ToInt32(sID);
                            m_oSKPD = GetSKPD(pID);
                            LoadSubUnit(pID, node);
                            
                            break;

                        case 3:
                            m_oSubUnit = GetSubUnit(pID);

                            parentNode = node.Parent;
                            stag = parentNode.Tag.ToString();
                            sID = stag.Substring(6);
                            pID = Convert.ToInt32(sID);
                            m_oUnit = GetUnit(pID);

                            parentNode = parentNode.Parent;
                            stag = parentNode.Tag.ToString();
                            sID = stag.Substring(6);
                            pID = Convert.ToInt32(sID);
                            m_oSubUnit = GetSubUnit(pID);
                            break;
                    }
                    Changed(sender, e, type, 0);
                }

            }*/

        }

        /*
         * 
         * 
         * menuTambahSKPD
menuSepDKPD
menuEditSKPD
menuHapusSKPD
menuSepUK
menuTambahUK
menuEditUK
menuHapusUnitKerja
menuSepSubUK
menuTambahSubUnit
menuEditSubUnit
menuHapusSubUnit
         * /
         */
        private void HideMenu()
        {
            menuTambahSKPD.Visible =false;
            menuSepDKPD.Visible =false;
            menuEditSKPD.Visible =false;
            menuHapusSKPD.Visible =false;
            menuSepUK.Visible =false;
            menuTambahUK.Visible =false;
            menuEditUK.Visible =false;
            menuHapusUnitKerja.Visible =false;
            menuSepSubUK.Visible =false;
            menuTambahSubUnit.Visible =false;
            menuEditSubUnit.Visible =false;
            menuHapusSubUnit.Visible = false;
        }

        private SKPD GetSKPD(int pID){
            SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            SKPD oSKPD = oSKPDLogic.GetByID(pID);
            return oSKPD;       
        }

        //private Unit GetUnit(int pID)
        //{
        //    UnitKerjaLogic oUnitKerjaLogic = new UnitKerjaLogic();
        //  //  Unit oUnit = oUnitKerjaLogic.GetByID(pID);
        //    return oUnit;
        //}
        
        public SKPD GetSKPD(){
            return m_oSKPD;
        }
        public Unit GetUnit()
        {
            return m_oUnit;
        }
        public SubUnit GetSubUnit(){
            return m_oSubUnit;
        }

        private void menuTambahSKPD_Click(object sender, EventArgs e)
        {
            frmSKPD fSKPD = new frmSKPD();
         //   fSKPD.SetID(m_oSKPD.ID);
            fSKPD.ShowDialog();
        }

        private void tvDinas_MouseUp(object sender, MouseEventArgs e)
        {
            m_iPoint = new Point(e.X, e.Y);           
            TreeNode node = tvDinas.GetNodeAt(m_iPoint);
            if (node == null)
            {
                return;
            }
            int iLevel = GetLevel(node);
  

            HideMenu();


            if (e.Button == MouseButtons.Right)
            {

                // Point where the mouse is clicked.
                

                if (node != null)
                {

                    // Select the node the user has clicked.
                    // The node appears selected until the menu is displayed on the screen.
                    m_OldSelectNode = tvDinas.SelectedNode;
                    tvDinas.SelectedNode = node;
                    switch (iLevel)
                    {
                        case 0:
                            if (GlobalVar.Pengguna.IsUserDinas == 0)
                            {
                                menuTambahSKPD.Visible = true;
                            }

                            break;

                        case 1:
                            menuEditSKPD.Visible = true;
                            menuHapusSKPD.Visible = true;
                            menuSepUK.Visible = true;
                            menuTambahUK.Visible = true;
                            break;
                        case 2:
                            menuEditUK.Visible = true;
                            menuHapusUnitKerja.Visible = true;
                            menuSepSubUK.Visible = true;
                            menuTambahSubUnit.Visible = true;
                            break;
                        case 3:
                            menuEditSubUnit.Visible = true;
                            menuHapusSubUnit.Visible = true;
                            break;

                    }


                    menuDinas.Show(tvDinas, m_iPoint);


                    // Highlight the selected node.
                    tvDinas.SelectedNode = m_OldSelectNode;
                    m_OldSelectNode = null;
                }
            }
            
        }

        private void menuTambahUK_Click(object sender, EventArgs e)
        {

        }
        private void menuTambahSubUnit_Click(object sender, EventArgs e)
        {

        }
         private int GetID(TreeNode node)
        {
            int lID;
            lID = 0;
            int iLevel = GetLevel(node);

            //2DUMMY
            if (node !=null ){
             if (node.Tag.ToString().Length > 5 && node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
            {
                if (iLevel < 3)
                {
                    lID = Convert.ToInt32(node.Tag.ToString().Substring(6));
                }
                
            }
            else
            {
                lID = Convert.ToInt32(node.Tag.ToString().Substring(1));
            }
            }
            return lID;

        }
        private long  GetLongID(TreeNode node)
        {
            long lID;
            lID = 0;
            //2DUMMY
            if (node != null)
            {
                if (node.Tag.ToString().Length > 5 && node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
                {
                    lID = Convert.ToInt64(node.Tag.ToString().Substring(6));
                }
                else
                {
                    lID = Convert.ToInt64(node.Tag.ToString().Substring(1));
                }
            }
            return lID;

        }
        private int GetLevel(TreeNode node)
        {
            int iLevel=0;
            if (node != null) { 
            if (node.Tag != null)
            {
                iLevel = Convert.ToInt32(node.Tag.ToString().Substring(0, 1));
            }
        }
            
            return iLevel;
        }

        private void tvDinas_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag.ToString().Length > 5)
            {
                if (e.Node.Tag.ToString().Substring(1, 5).Equals("DUMMY"))
                {
                    e.Node.Nodes.Clear();
                    //int lID = GetID(e.NodeConvert.ToInt32(e.Node.Tag.ToString().Substring(6));
                    int lID = GetID(e.Node);
                    int iLevel = GetLevel(e.Node);

                    //iLevel = Convert.ToInt32(e.Node.Tag.ToString().Substring(0, 1));
                    e.Node.Tag = e.Node.Tag.ToString().Replace("DUMMY", "");// e.Node.Tag.ToString().Substring(6);
                    switch (iLevel)
                    {
                        case 0:
                            LoadSKPD();
                            break;                        
                        case 1:
                            LoadUnit(lID, e.Node);
                            break;
                        case 2:
                          //  LoadSubUnit(lID, e.Node);
                            break;
                    }
                }
            }

        }

        private void tvDinas_MouseDown(object sender, MouseEventArgs e)
        {
            m_iPoint = new Point(e.X, e.Y);
            
            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = tvDinas.GetNodeAt(m_iPoint);
                if (node == null)
                {
                    return;
                }
                int iLevel = GetLevel(node);
                if (iLevel < 3)
                {
                    int pID = GetID(node);

                    if (Changed != null)
                    {
                        Changed(pID, iLevel);

                    }
                }
                else
                {
                    long lID = GetLongID(node);
                    if (ChangedSubUnit != null)
                    {
                        ChangedSubUnit(lID);

                    }
                    

                }

            }

        }

        private void menuEditSKPD_Click(object sender, EventArgs e)
        {

        }

        private void menuHapusSKPD_Click(object sender, EventArgs e)
        {

        }

        private void menuEditUK_Click(object sender, EventArgs e)
        {

        }

        private void menuHapusUnitKerja_Click(object sender, EventArgs e)
        {

        }

        private void menuEditSubUnit_Click(object sender, EventArgs e)
        {

        }

        private void menuHapusSubUnit_Click(object sender, EventArgs e)
        {

        }

        private void tvDinas_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Create(m_iPemda, m_iMaxLevel);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
