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
    public partial class frmSUbKegiatan : Form
    {
        private int m_idDinas;

        private int m_idUrusan;
        private int m_idProgram;
        private int m_iKodeUK;
        private int m_idKegiatan;
        public frmSUbKegiatan()
        {
            InitializeComponent();
        }

        private void frmSUbKegiatan_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            ctrlHeader1.SetCaption("Sub Kegiatan Detail");

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_idDinas = pIDSKPD;
            m_iKodeUK = pIDUK;
              ctrlUrusanPemerintahan1.Create(m_idDinas, (int)GlobalVar.TahunAnggaran);
     
            

        
        }

        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            m_idUrusan = pID;
            ctrlProgram1.Clear();
            ctrlProgram1.Create((int)GlobalVar.TahunAnggaran, m_idDinas, m_idUrusan);
        }

        private void ctrlProgram1_OnChanged(int pID)
        {
            m_idProgram = pID;
            ctrlKegiatanAPBD1.Clear();
            ctrlKegiatanAPBD1.CreateWIthUK((int)GlobalVar.TahunAnggaran, m_idDinas, m_iKodeUK, m_idProgram);

        }

        private void ctrlKegiatanAPBD1_OnChanged(int pID)
        {
            m_idKegiatan = pID;
            
        }

        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;
            try
            {
                TSubKegiatanLogic otSUbRek = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, 3);//,RekeningLogic.E_REKENING_TYPE.REKENING_13 );
                List<TSubKegiatan> _lstRek = new List<TSubKegiatan>();
                long idsubkegiatan = 0;
                if (DataFormat.GetLong(txtKode.Text) == 0)
                {
                    MessageBox.Show("Kode Masih salah");
                    ret.ResponseStatus = false;
                    return ret;

                }
                string strIDSUB = (m_idKegiatan.ToString() + "0000").ToString();
                idsubkegiatan = Convert.ToInt64(strIDSUB) + DataFormat.GetLong(txtKode.Text);

                        TSubKegiatan o = new TSubKegiatan();
                        o.Tahun = GlobalVar.TahunAnggaran;

                        o.IDDinas = m_idDinas;
                        o.IDUrusan = m_idUrusan;
                        o.IDProgram = m_idProgram;
                        o.IDUnit = (m_idDinas * 100) + DataFormat.GetInteger(txtKode.Text);
                        o.KodeUk = m_iKodeUK;
                        o.IDKegiatan = m_idKegiatan;
                        o.Nama = txtNama.Text.Trim();

                        o.IDSubKegiatan = idsubkegiatan;

                        //o.IDRekening = DataFormat.GetLong(gridData2.Rows[i].Cells[18].Value.ToString().Replace(".", ""));

                        ProgramKegiatan pk = new ProgramKegiatan();


                        pk.IDDInas = m_idDinas;
                        pk.IDUrusan = m_idUrusan;
                        pk.IDProgram = m_idProgram;
                        ;
                        //pk.IDUnit = DataFormat.GetInteger(gridData2.Rows[i].Cells[24].Value.ToString().Replace(".", ""));
                        pk.KodeUK = m_iKodeUK;
                        pk.IDKegiatan = m_idKegiatan;
                        pk.NamaKegiatan = ctrlKegiatanAPBD1.GetNamaKegiatan();
                        pk.NamaProgram = ctrlProgram1.GetNamaProgram();
                        pk.NamaSubKegiatan = txtNama.Text.Trim();
                        pk.NamaUrusan = ctrlUrusanPemerintahan1.GetNama();
                        pk.IDSubKegiatan = idsubkegiatan;
                        pk.Tahun = GlobalVar.TahunAnggaran;
                        pk.NamaDinas = ctrlDinas1.GetNamaSKPD();
                        pk.KodeDinas = ctrlDinas1.GetSKPD().KodeSIPD;
                        o.PK = pk;
                        otSUbRek.SimpanSIPD(o);//.Add(o);
                        
                        //}




                 
                MessageBox.Show("Selesai import Sub Kegiatan");
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret.ResponseStatus = false;
                return ret;
            }
            
        }
    }
}
