using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using Formatting;


namespace KUAPPAS.Bendahara
{
    public partial class frmPengeluaranCariPerusahaan : Form
    {

        private Perusahaan moPerusahaan;
        private bool bOK;
        private int m_IDPerusahaan;
        List<Perusahaan> m_ListPerusahaan;

        public frmPengeluaranCariPerusahaan()
        {
            InitializeComponent();
            moPerusahaan = new Perusahaan();
            bOK = false;
            m_IDPerusahaan = 0;
        }

        private void frmCariPerusahaan_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Cari Data Perusahaan...");
            gridPerusahaan.FormatHeader();
           // LoadPerusahaan();
        }
        //private void LoadPerusahaan()
        //{
        //    PerusahaanLogic oLogic = new PerusahaanLogic((int)GlobalVar.TahunAnggaran);
        //    List<Perusahaan>  m_ListPerusahaan = new List<Perusahaan>(); 
        //     m_ListPerusahaan = oLogic.Get();
        //    if ( m_ListPerusahaan  != null)
        //    {
        //        foreach (Perusahaan p in  m_ListPerusahaan )
        //        {
        //            string[] row = { p.IDPerusahaan.ToString(), GetBentuk(p.Bentuk), p.NamaPerusahaan, p.Alamat, "Pilih" };
        //            gridPerusahaan.Rows.Add(row);

        //        }
        //    }

        //}
        private string GetBentuk(int b)
        {
            string sRet="";
            switch (b)
            {
                case 0:
                    sRet= "CV";
                    break;
                case 1:
                    sRet = "PT";
                    break;
                case 2:
                    sRet = "Lainnya";
                    break;

            }
            return sRet;

        }

        private void cmdCari_Click(object sender, EventArgs e)
        {
            PerusahaanLogic oLogic = new PerusahaanLogic((int)GlobalVar.TahunAnggaran);
            
             m_ListPerusahaan = new List<Perusahaan>();
             string nama = txtNama.Text;
             m_ListPerusahaan = oLogic.Get(nama);
            if (m_ListPerusahaan != null)
            {
                foreach (Perusahaan p in m_ListPerusahaan)
                {
                   
                        string[] row = { p.IDPerusahaan.ToString(), GetBentuk(p.Bentuk), p.NamaPerusahaan, p.Alamat, "Pilih" };
                        gridPerusahaan.Rows.Add(row);
                 

                    

                }
            }


        }
        public Perusahaan Perusahaan { 
            get {
        
                    return moPerusahaan;
                }
            
        }

        private void cmdPilih_Click(object sender, EventArgs e)
        {
            if (moPerusahaan == null)
            {
                MessageBox.Show("Belum Pilih Perusahaan");
                return;

            }
           
            bOK = true;
            this.Hide();
        }

        private void gridPerusahaan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0 && e.RowIndex < gridPerusahaan.Rows.Count)
                {
                    int m_IDPerusahaan = DataFormat.GetInteger(gridPerusahaan.Rows[e.RowIndex].Cells[0].Value);
                   
                        moPerusahaan = m_ListPerusahaan[e.RowIndex];
                        txtNamaPerusahaan.Text = moPerusahaan.NamaPerusahaan;
                        txtAlamatPerusahaan.Text = moPerusahaan.Alamat;
                        txtNamaPimpinan.Text = moPerusahaan.Pimpinan;

                    


                }
                else
                {
                    moPerusahaan = null;

                    txtNamaPerusahaan.Text = "";
                    txtAlamatPerusahaan.Text = "";
                    txtNamaPimpinan.Text = "";

                }    
   
        
        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            bOK= false ;
            this.Hide ();
        }
        public bool IsOK{

            get{return bOK;}
        }    
    
    }
}
