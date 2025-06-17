using BP.Bendahara;
using DTO.Bendahara;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;
using DTO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using NPOI.SS.UserModel;


namespace KUAPPAS.KasDaerah
{
    public partial class frmCatatTanggalCair : ChildForm
    {


        DataGridViewCellStyle _merah = new DataGridViewCellStyle();
        DataGridViewCellStyle _pink = new DataGridViewCellStyle();
        DataGridViewCellStyle _ijo = new DataGridViewCellStyle();

        DataGridViewCellStyle _ditolakStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _didiskusikanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTerimaStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _diTangguhkanStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _baruStyle = new DataGridViewCellStyle();
        private readonly Random _random = new Random();

        List<SPP> m_lstspp;
 
        private int _InternalCounter;

        public frmCatatTanggalCair()
        {
            InitializeComponent();
        }

        private void frmCatatTanggalCair_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Pencatatan Tanggal Cair SP2D");
            gridSP2D.FormatHeader();
            ctrlTanggal1.Tanggal = DateTime.Now.Date;
            ctrlTanggal2.Tanggal = DateTime.Now.Date;

            ctrlTanggal3.Tanggal = DateTime.Now.Date;

        }

        private void chkCair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCair.Checked)
            {
                lblStatus.Text = "Tanggal Cair";
            }
            else
            {
                lblStatus.Text = "Tanggal Terbt SP2D";
            }
        }
        private void BersihkanKeterangan()
        {
            txtKeterangan.Text = "";
            lblNoSP2D.Text = "";
            lblJumlah.Text = "";
            lblTanggalTerbit.Text  = "";
            lblTanggalCair.Text = "";

        }
        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            try
            {
                
            SPPLogic ologic = new SPPLogic(GlobalVar.TahunAnggaran);
            ParameterBendahara p = new ParameterBendahara(GlobalVar.TahunAnggaran);
            p.TanggalAwal = ctrlTanggal1.Tanggal ;
            p.TanggalAkhir = ctrlTanggal2.Tanggal;
            p.NoSP2D = txtNoSP2D.Text;
            p.NoSPP = "";
            p.NoSPM = "";

            p.Status = chkCair.Checked == true ? 4 : 3;

            p.WithPotongan = true;
            p.LstStatus = new List<int>();
            m_lstspp = new List<SPP>();
            p.Jenis = -1;// DataFormat.GetInteger(txtJenis.Text);
            p.OrderBy = " tSPP.iNoSP2D, tSPP.dtTerbitSP2D, tSPP.inoUrut";

                m_lstspp = ologic.GetSPP(p).FindAll(spp=>spp.Status== p.Status);

            if (ologic.IsError() == true)
            {
                MessageBox.Show(ologic.LastError());
                return;
            }

                

                int iRow = 0;
            gridSP2D.Rows.Clear();
            BersihkanKeterangan();




            

       

            foreach (SPP spp in m_lstspp)
            {

                //jumlahdibayar = 0;
                //jumlahdibayar = spp.Jumlah - spp.JumlahPotongan;
                string namaSKPD="";
                SKPD oSKPD = GlobalVar.gListSKPD.FirstOrDefault(skpd => skpd.ID == spp.IDDInas);
                if (oSKPD != null)
                {
                    namaSKPD = oSKPD.Nama;
                }
                string[] row = { 
                                   spp.NoUrut.ToString(), //0
                                   "false", //2
                                   spp.NoUrutKasda.ToString(),
                                   spp.NoSP2D, //3

                                    namaSKPD, //5
                                   spp.Keterangan  , 
                                   spp.Jumlah.ToRupiahInReport(),//10 
                                   spp.JumlahPotongan.ToRupiahInReport(),//12
                                   spp.dtTerbit.ToString("MM/dd/yyyy"),//13
                                   spp.dtCair.ToString("dd MMM"),spp.Status.ToString()};
                                   
                    gridSP2D.Rows.Add(row);

                    iRow++;

                    if (spp.Status == 3)
                        gridSP2D.Rows[iRow - 1].DefaultCellStyle.BackColor = Color.AntiqueWhite;// Red;
                    if (spp.Status == 4)
                    {
                        gridSP2D.Rows[iRow - 1].DefaultCellStyle.BackColor = Color.LightSalmon;// PaleVioletRed;// IndianRed;// Red;
                        
                       
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            
        }

        private void gridSP2D_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < gridSP2D.Rows.Count && e.RowIndex > -1)
            {
                BersihkanKeterangan();
                DataGridViewRow row= gridSP2D.Rows[e.RowIndex];
                long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                if (NoUrut > 0)
                {
                    SPP spp = m_lstspp.FirstOrDefault(s => s.NoUrut == NoUrut);
                    if (spp != null)
                    {
                        lblNoSP2D.Text = spp.NoSP2D;
                        lblTanggalCair.Text = spp.dtCair.ToString("dd MMM yyyy");
                        lblTanggalTerbit.Text = spp.dtTerbit.ToString("dd MMM yyyy");
                        txtKeterangan.Text = spp.Keterangan;
                        lblJumlah.Text = spp.Jumlah.ToRupiahInReport();

                    }

                }

            }
        }

        private void cmdpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                txtDikirmKeSilakan.Text="";
                List<SPP> lstSPPToCair = new List<SPP>();        
                foreach (DataGridViewRow row in gridSP2D.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        long NoUrut = DataFormat.GetLong(row.Cells[0].Value);
                        bool bDipilih = Convert.ToBoolean(row.Cells[1].Value);
                        if (bDipilih == true)
                        {
                            DateTime tanggalTerbit = DataFormat.GetDate(row.Cells[8].Value);
                            if (tanggalTerbit > ctrlTanggal3.Tanggal)
                            {


                                MessageBox.Show("Tanggal cair tidak boleh mendahului tanggal terbit " +
                                               " Untuk SPdD " + DataFormat.GetString(row.Cells[3].Value) + " Dinas " + 
                                                  DataFormat.GetString(row.Cells[4].Value)  );


                            }
                            else
                            {

                                SPP spp = new SPP();
                                spp = m_lstspp.FirstOrDefault(s => s.NoUrut == NoUrut);

                                if (spp != null)
                                {
                                    spp.NoUrutKasda = DataFormat.GetInteger(row.Cells[2].Value);
                                    spp.dtCair = ctrlTanggal3.Tanggal;
                                    lstSPPToCair.Add(spp);

                                }
                            }
                            

                        }

                    }
                   
                }
                List<string> lst = new List<string>();
                if (lstSPPToCair.Count > 0)
                {   
                    SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                    lst=oLogic.SetTanggalCair(lstSPPToCair);//, lstSPPRekening, lstBKU, lstMaxNoBKU);
                }
                if (lst != null)
                {
                    foreach (string s in lst)
                    {
                        txtDikirmKeSilakan.Text= txtDikirmKeSilakan.Text + "," + s;
                    }
                }
                MessageBox.Show("Tanggal Pembukuan SP2D Sudah diberi nilai.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

       
        private void cmdBKU_Click(object sender, EventArgs e)
        {
            foreach (SPP spp in m_lstspp)
            {
                SPPLogic oLogic = new SPPLogic(GlobalVar.TahunAnggaran);
                oLogic.CatatBKUKasda(spp);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
