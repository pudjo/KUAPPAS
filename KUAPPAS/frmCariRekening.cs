using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using Formatting;


namespace KUAPPAS
{
    public partial class frmCariRekening : Form
    {

        private bool isOK;
        private Rekening m_oSelectedRekening;
        string  m_iParent;
        bool bForJurnal;
        public frmCariRekening()
        {
            InitializeComponent();
            isOK = false;
            m_oSelectedRekening = new Rekening();
            m_iParent = "";
            bForJurnal = false;
        }
        public bool FoeJurnal
        {
            set
            {
                bForJurnal = value;
            }
        }
        public string Parent
        {
            set { m_iParent = value; }
        }
        private void frmCariRekening_Load(object sender, EventArgs e)
        {
            if (bForJurnal == false)
            {
                treeRekening1.Create();
            }
            else
            {
                treeRekening1.CreateForJurnal(m_iParent);
            }

        }

        private void treeRekening1_Changed(global::DTO.Rekening rek)
        {
            try
            {
                txtIDRekening.Text = rek.ID.ToString();
                txtNamaRekening.Text = rek.Nama;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Hide();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            isOK = true;
            m_oSelectedRekening.ID = DataFormat.GetLong(txtIDRekening.Text);
            m_oSelectedRekening.Nama = txtNamaRekening.Text;
            this.Hide();
        }
        public Rekening GetRekening()
        {
            return m_oSelectedRekening;
        }
        public bool IsOK()
        {
            return isOK;
        }

        private void treeRekening1_Load(object sender, EventArgs e)
        {

        }


    }
}
