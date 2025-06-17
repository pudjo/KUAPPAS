using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Formatting;
using DTO;
using BP;
 
namespace KUAPPAS.Anggaran
{
    public partial class ctrlProgramKegiatanSub : UserControl
    {
        private long m_IdSubKegiatan ;
        private int m_IDKegiatan;
        private int m_IDProgram;
        private int m_IDUrusan ;

        public ctrlProgramKegiatanSub()
        {
            InitializeComponent();
        }

        private void ctrlProgramKegiatanSub_Load(object sender, EventArgs e)
        {

        }
        public long IDSubKegiatan
        { 
            set {
                m_IdSubKegiatan = value;
                LoadData();
             }
            get { 
                return m_IdSubKegiatan; 
            } 
                  
        }
        public void SetProgramKegiatan(ProgramKegiatan pk)
        {
            if (pk != null)
            {
                txtIDurusan.Text = pk.StrIDUrusan;
                txtIDProgram.Text = pk.StrIDProgram;
                txtIDKegiatan.Text = pk.StrIDKegiatan;
                txtIDSubKegiatan.Text = pk.StrIDSubKegiatan;
                m_IDKegiatan = DataFormat.GetInteger(txtIDKegiatan.Text.Replace(".", ""));
                m_IDProgram = DataFormat.GetInteger(txtIDProgram.Text.Replace(".", ""));
                m_IDUrusan = DataFormat.GetInteger(txtIDurusan.Text.Replace(".", ""));
                txtNamaUrusan.Text = pk.NamaUrusan;
                txtNamaProgram.Text = pk.NamaProgram;
                txtNamaKegiatan.Text = pk.NamaKegiatan;
                txtNamaSUbKegiatan.Text = pk.NamaSubKegiatan;
            }

        }
        private void LoadData()
        {
            ProgramKegiatan pk = new ProgramKegiatan();
            ProgramKegiatanLogiccs oLogic = new ProgramKegiatanLogiccs(GlobalVar.TahunAnggaran);
            pk = oLogic.GetByIDSub(m_IdSubKegiatan);
            if (pk != null)
            {
             
                txtIDurusan.Text = pk.StrIDUrusan;
                txtIDProgram.Text = pk.StrIDProgram;
                txtIDKegiatan.Text = pk.StrIDKegiatan;
                txtIDSubKegiatan.Text = pk.StrIDSubKegiatan;
                m_IDKegiatan = DataFormat.GetInteger(txtIDKegiatan.Text.Replace(".", ""));
                m_IDProgram = DataFormat.GetInteger(txtIDProgram.Text.Replace(".", ""));
                m_IDUrusan = DataFormat.GetInteger(txtIDurusan.Text.Replace(".", ""));
                txtNamaUrusan.Text = pk.NamaUrusan;
                txtNamaProgram.Text = pk.NamaProgram;
                txtNamaKegiatan.Text = pk.NamaKegiatan;
                txtNamaSUbKegiatan.Text = pk.NamaSubKegiatan;

            }
        }

        public int IDKegiatan
        {
            get { return m_IDKegiatan; }
        }
        public int IDProgram
        {
            get
            {
                return m_IDProgram;
            }
        }
        public int IDUrusan
        {
            get
            {
                return m_IDUrusan;
            }
        }

        private void txtNamaProgram_StyleChanged(object sender, EventArgs e)
        {
           
 
        }

        private void txtNamaProgram_SizeChanged(object sender, EventArgs e)
        {
            txtNamaKegiatan.Top = txtNamaProgram.Top + txtNamaProgram.Height + 2;
            txtIDKegiatan.Top = txtNamaKegiatan.Top ;

            txtNamaSUbKegiatan.Top = txtNamaKegiatan.Top + txtNamaKegiatan.Height + 2;
            txtIDSubKegiatan.Top = txtNamaSUbKegiatan.Top ;
        }

        private void txtNamaKegiatan_SizeChanged(object sender, EventArgs e)
        {
           

            txtNamaSUbKegiatan.Top = txtNamaKegiatan.Top + txtNamaKegiatan.Height + 2;
            txtIDSubKegiatan.Top = txtNamaSUbKegiatan.Top + 3;
        }

        private void ctrlProgramKegiatanSub_Load_1(object sender, EventArgs e)
        {

        }


        
    
    }
}
