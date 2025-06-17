using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;

namespace KUAPPAS
{
    public partial class frmDashBoard : Form
    {
        List<RekapProgramKegiatanUmum> _lst = new List<RekapProgramKegiatanUmum>();
        List<RekapDashBoard> _lstRekap2 = new List<RekapDashBoard>();
        List<DashboardIII> _lstDashbordIII = new List<DashboardIII>();
     //   List<RekapDashBoard> _lstRekap2 = new List<RekapDashBoard>();

        public frmDashBoard()
        {
            InitializeComponent();
        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            
            this.imageStrip.ImageSize = new System.Drawing.Size(16, 16);
            this.imageStrip.TransparentColor = System.Drawing.Color.Magenta;
            this.imageStrip.ImageSize = new Size(16, 16);
            //this.imageStrip.Images.AddStrip(Properties.Resources.newGroupPostIconStrip);

            treeGridView1.ImageList = imageStrip;
            treeGridView1.ImageList = imageStrip;
            ctrlHeader1.SetCaption("Rekap Input RKA.", "");
            treeRekening1.Create(5000000);

           

        }
        private void LoadRekap()
        {

            RekapLogic oLogic = new RekapLogic(GlobalVar.TahunAnggaran);
            _lst.Clear();
            int _idDinas=0;

            if (GlobalVar.Pengguna.IsUserDinas > 0)
            {
                _idDinas = GlobalVar.Pengguna.SKPD;
            }
            _lst = oLogic.GetRekap((int)GlobalVar.TahunAnggaran, _idDinas);
            //gridRekapProgram.Rows.Clear();
            treeGridView1.Rows.Clear();
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            

            if (_lst != null)
            {
                foreach (RekapProgramKegiatanUmum r in _lst)
                {

                    if (r.KodeKegiatan == 0)
                    {
                        TreeGridNode node = treeGridView1.Nodes.Add( r.Nama,r.JumlahInput,r.JumlahPagu,"0");
                        node.DefaultCellStyle.Font = boldFont;
                        LoadKegiatan(node, r.KodeProgram);                        
                    }                    
                }
            }            
        }
        private void LoadKegiatan(TreeGridNode nodeParent, int idPrg)
        {
            Font italicFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Italic);
            

            foreach (RekapProgramKegiatanUmum r in _lst)
            {
                if (r.KodeProgram == idPrg && r.KodeKegiatan > 0 && r.KodeDinas== "0")
                {
                    TreeGridNode node = nodeParent.Nodes.Add(r.Nama, r.JumlahInput, r.JumlahPagu, "0");                  
                    LoadDinas(node, r.KodeProgram, r.KodeKegiatan); 

                }
            }

        }
        private void LoadDinas(TreeGridNode nodeParent, int idPrg, int idKeg)
        {
            foreach (RekapProgramKegiatanUmum r in _lst)
            {
                if (r.KodeProgram == idPrg && r.KodeKegiatan == idKeg && r.KodeDinas.Length>2 && r.KodeRekening.Length<3)
                {
                    TreeGridNode node = nodeParent.Nodes.Add(r.Nama, r.JumlahInput, r.JumlahPagu, "0");
                    //LoadKegiatan(node, r.KodeProgram);
                    LoadRekening(node, r.KodeProgram, r.KodeKegiatan, r.KodeDinas);

                    //node.ImageIndex = 0;

                }
            }

        }
        private void LoadRekening(TreeGridNode nodeParent, int idPrg, int idKeg,string sDinas)
        {
            foreach (RekapProgramKegiatanUmum r in _lst)
            {
                if (r.KodeProgram == idPrg && r.KodeKegiatan == idKeg && r.KodeDinas == sDinas && r.KodeRekening.Length > 1)
                {
                    TreeGridNode node = nodeParent.Nodes.Add(r.Nama, r.JumlahInput, r.JumlahPagu, "0");
                    //LoadKegiatan(node, r.KodeProgram);
                    //node.ImageIndex = 1;

                }
            }

        }

        private void cmdRefresh2_Click(object sender, EventArgs e)
        {
            int iAnggaranPerubahan = chkAnggaranPerubahan.Checked == true ? 1 : 0;

            RekapLogic oLogic = new RekapLogic(GlobalVar.TahunAnggaran);
        //    _lstRekap2.Clear();
            _lstRekap2 = oLogic.GetRekapII((int)GlobalVar.TahunAnggaran, iAnggaranPerubahan);
            //gridRekapProgram.Rows.Clear();
            treeGridView2.Rows.Clear();
            //Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);


            if (_lstRekap2 != null)
            {
                foreach (RekapDashBoard r in _lstRekap2)
                {

                    if (r.Level == 1)
                    {

                        TreeGridNode node = treeGridView2.Nodes.Add(r.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU,r.SelisihBelanja);
                        //node.DefaultCellStyle.Font = boldFont;
                        LoadUrusan2(node, r.IDDInas);                       
            

                    }

                }
            }            
        }
        private void LoadUrusan2(TreeGridNode nodeParent, int idDIna)
        {
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapDashBoard r in _lstRekap2)
                {

                    if (r.Level == 2 && r.IDDInas== idDIna)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU, r.SelisihBelanja);
                        
                        LoadProgram2(node, r.IDDInas,r.IDUrusan);
                    }

                }
            }            
        }
        private void LoadProgram2(TreeGridNode nodeParent, int idDInas, int IDUrusan)
        {
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapDashBoard r in _lstRekap2)
                {

                    if (r.Level == 3 && r.IDDInas == idDInas && r.IDUrusan== IDUrusan)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU, r.SelisihBelanja);
                        //node.DefaultCellStyle.Font = boldFont;
                        LoadKegiatan2(node, r.IDDInas, r.IDUrusan, r.IDProgram,r.Jenis);
                    }

                }
            }            

        }
        private void LoadKegiatan2(TreeGridNode nodeParent, int idDInas, int IDUrusan, int IDProgram, Single Jenis)
        {
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapDashBoard r in _lstRekap2)
                {

                    if (r.Level == 4 && r.IDDInas == idDInas && r.IDUrusan == IDUrusan && r.IDProgram== IDProgram && r.Jenis== Jenis)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU, r.SelisihBelanja);
                        
                        LoadRekening2(node, r.IDDInas, r.IDUrusan, r.IDProgram,r.IDkegiatan,r.Jenis);
                    }

                }
            }            

        }
        private void LoadRekening2(TreeGridNode nodeParent, int idDInas, int IDUrusan, int IDProgram, int idKegiatan, Single Jenis)
        {
            Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapDashBoard r in _lstRekap2)
                {

                    if (r.Level == 5 && r.IDDInas == idDInas && r.IDUrusan == IDUrusan && r.IDProgram == IDProgram && r.IDkegiatan== idKegiatan && r.Jenis== Jenis)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + "-" + r.Nama, r.PENDAPATANINPUT, r.BTLINPUT, r.BLINPUT, r.PENDAPATANPAGU, r.BTLPAGU, r.BLPAGU);

                        
                    }

                }
            }            
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            LoadRekap();
        }

        private void treeRekening1_Load(object sender, EventArgs e)
        {

        }

        private void treeRekening1_Changed(Rekening rek)
        {
            if (rek.Root < 5)
                return;

            
                
            _lstDashbordIII = new List<DashboardIII>();

            RekapLogic oLogic = new RekapLogic(GlobalVar.TahunAnggaran);

            _lstDashbordIII.Clear();
            _lstDashbordIII = oLogic.GetRekapIII((int)GlobalVar.TahunAnggaran,rek.ID);


            treeGridView3.Rows.Clear();
            Font boldFont = new Font(treeGridView3.DefaultCellStyle.Font, FontStyle.Bold);

            if (_lstDashbordIII  != null)
            {
                foreach (DashboardIII r in _lstDashbordIII)
                {

                    if (r.Level==1)
                    {
                        if (GlobalVar.Pengguna.IsUserDinas == 0)
                        {
                            TreeGridNode node = treeGridView3.Nodes.Add(r.Kode + " - " + r.Nama, r.IDkegiatan.ToString(), r.Input, r.Pagu, r.Selisih);
                            node.DefaultCellStyle.Font = boldFont;
                            LoadProgramIII(node, r.IDDInas);
                        }
                        else
                        {
                            if (r.IDDInas == GlobalVar.Pengguna.SKPD)
                            {
                                TreeGridNode node = treeGridView3.Nodes.Add(r.Kode + " - " + r.Nama, r.IDkegiatan.ToString(), r.Input, r.Pagu, r.Selisih);
                                node.DefaultCellStyle.Font = boldFont;
                                LoadProgramIII(node, r.IDDInas);
                            }

                        }
                        //                    node.ImageIndex = 0;

                    }                    
                }
            }            
        }
        private void LoadProgramIII(TreeGridNode nodex, int _idDinas)
        {
            if (_lstDashbordIII != null)
            {
                foreach (DashboardIII r in _lstDashbordIII)
                {

                    if (r.Level == 3 && r.IDDInas== _idDinas)
                    {
                        TreeGridNode node = nodex.Nodes.Add(r.Kode + " - "+ r.Nama, r.IDkegiatan.ToString(), r.Input, r.Pagu, r.Selisih);
                        //node.DefaultCellStyle.Font = boldFont;
                        LoadKegiatanIII(node, r.IDDInas,r.IDProgram);
                        //                    node.ImageIndex = 0;

                    }
                }
            }            
        }
        private void LoadKegiatanIII(TreeGridNode nodex, int _idDinas, int _idProgram)
        {
            if (_lstDashbordIII != null)
            {
                foreach (DashboardIII r in _lstDashbordIII)
                {

                    if (r.Level == 4 && r.IDDInas == _idDinas && r.IDProgram== _idProgram)
                    {
                        TreeGridNode node = nodex.Nodes.Add(r.Kode + " - " + r.Nama, r.IDkegiatan.ToString(), r.Input, r.Pagu, r.Selisih);
                        //node.DefaultCellStyle.Font = boldFont;
                        //LoadKegiatanIII(node, r.IDDInas, r.IDProgram);
                        //                    node.ImageIndex = 0;

                    }
                }
            }
        }
                         
    }
}
