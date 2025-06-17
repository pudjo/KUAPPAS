using BP;
using DTO;
using Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUAPPAS.Bendahara
{
    public partial class frmListPPTK : ChildForm
    {
        List<Pejabat> mlstPejabat;
        int m_IDDInas;
        int m_KodeUK;
        int m_ID;
            int CONST_IDJABATAN=50;
        public frmListPPTK()
        {
            InitializeComponent();
            mlstPejabat = new List<Pejabat>();
            m_ID=0;
        }

        private void frmListPPTK_Load(object sender, EventArgs e)
        {
            gridPPTK.FormatHeader();
            
            ctrlSKPD1.Create();
            if (GlobalVar.Pengguna.SKPD > 0)
            {
                ctrlSKPD1.SetID(GlobalVar.Pengguna.SKPD);

            }
            GetData();
        }
        private void GetData()
        {
            try
            {
                int jenis = CONST_IDJABATAN;
               PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                mlstPejabat = new List<Pejabat>();
                gridPPTK.Rows.Clear();
                mlstPejabat = oLogic.GetByJenisAndDinas(jenis, m_IDDInas, m_KodeUK);
                int i = 0;
                if (mlstPejabat != null)
                {
                    foreach (Pejabat p in mlstPejabat)
                    {
                        string[] row = { p.ID.ToString(), (++i).ToString(), p.Nama, p.NIP, p.Jabatan};
                        gridPPTK.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
           

        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
          
            m_ID = 0;
            txtNama.Text = "";
          
            txtNamaJabatan.Text = "";
            txtNIP.Text = "";
         
            return ret;
        }

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSKPD1_OnChanged(int pID)
        {
            m_IDDInas = pID;
            GetData();
            m_ID = 0;
            txtNama.Text = "";

            txtNamaJabatan.Text = "";
            txtNIP.Text = "";
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                if (MessageBox.Show("Apa benar akan menghapus PPTK ini?", "Konfirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ret.ResponseStatus = true;
                    Pejabat p = new Pejabat();
                    p.ID = m_ID;
                    p.Jenis = CONST_IDJABATAN;
                    p.Nama = txtNama.Text;
                    PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.Hapus(p) == true)
                    {
                        m_ID = 0;
                        GetData();
                        m_ID = 0;
                        txtNama.Text = "";

                        txtNamaJabatan.Text = "";
                        txtNIP.Text = "";

                    }
                    else
                    {
                        ret.ResponseStatus = false;
                        MessageBox.Show(oLogic.LastError());
                    }
                }
            }
            catch (Exception ex)
            {
                ret.ResponseStatus = false;
                MessageBox.Show(ex.Message);
            }


            return ret;
        }

         private bool CekInput()
        {
            try
            {
               
                if (txtNama.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Belum diisi.");
                    return false;
                }

                if (txtNIP.Text.Trim().Length == 0)
                {
                    MessageBox.Show("NIP Belum diisi.");
                    return false;
                }

                if (txtNamaJabatan.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Nama Jabatan Belum diisi.");
                    return false;
                }

                     


                   
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
         private EventResponseMessage ctrlNavigation1_OnSave()
         {
             EventResponseMessage ret = new EventResponseMessage();
             try
             {
                 ret.ResponseStatus = true;
                 if (CekInput() == false)
                 {
                     ret.ResponseStatus = false;
                     return ret;
                 }
                 Pejabat p = new Pejabat();
                 p.ID = m_ID;
                 p.Jenis = CONST_IDJABATAN;
                 p.IDDInas = m_IDDInas;
                 p.Unit = 0;
                 p.Nama = txtNama.Text.Replace("'","''''");
                 p.NIP = txtNIP.Text;
                 p.Jabatan = txtNamaJabatan.Text;
                 p.NamaDalamRekeningBank = "";
                 p.NPWP = "";
                 p.NoRekening = "";

                 p.NamaBank = "";
                 p.TanggalAktiv = new DateTime(GlobalVar.TahunAnggaran, 1, 1);

                 PejabatLogic oLogic = new PejabatLogic(GlobalVar.TahunAnggaran);
                 if (oLogic.Simpan(ref p) == true)
                 {
                     m_ID = p.ID;
                     GetData();
                     ret.ResponseStatus = true;
                 }
                 else
                 {
                     MessageBox.Show(oLogic.LastError());
                     ret.ResponseStatus = false;
                 }

             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
                 ret.ResponseStatus = false;
             }
             return ret;
         }

         private void gridPPTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex >= 0 && e.RowIndex < gridPPTK.Rows.Count)
             {
                 int id = DataFormat.GetInteger(gridPPTK.Rows[e.RowIndex].Cells[0].Value);
                 Pejabat p = mlstPejabat.FirstOrDefault(x => x.ID == id);
                 if (p != null)
                 {
                     txtNama.Text = p.Nama;
                     txtNIP.Text = p.NIP;
                     txtNamaJabatan.Text = p.Jabatan;
                     m_ID = p.ID;
                 }
             }
         }

         private void gridPPTK_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex >= 0 && e.RowIndex < gridPPTK.Rows.Count)
             {
                 int id = DataFormat.GetInteger(gridPPTK.Rows[e.RowIndex].Cells[0].Value);
                 Pejabat p = mlstPejabat.FirstOrDefault(x => x.ID == id);
                 if (p != null)
                 {
                     txtNama.Text = p.Nama;
                     txtNIP.Text = p.NIP;
                     txtNamaJabatan.Text = p.Jabatan;
                     m_ID = p.ID;
                 }
             }

         }

         private void ctrlNavigation1_Load(object sender, EventArgs e)
         {

         }
    }
}
