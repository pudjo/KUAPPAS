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
    public partial class frmMapUrusanBAruProgram : Form
    {
        private int m_idDInas;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_IDUrusanBaru;

        public frmMapUrusanBAruProgram()
        {
            InitializeComponent();
        }

        private void frmMapUrusanBAruProgram_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();
            ctrlUrusanBaru1.Create();


        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            ctrlUrusanPemerintahan1.Create(ctrlDinas1.GetID(), (int)GlobalVar.TahunAnggaran);
            m_idDInas= ctrlDinas1.GetID();
        

        }

        private void ctrlUrusanPemerintahan1_OnChanged(int pID)
        {
            m_IDUrusan=ctrlUrusanPemerintahan1.GetID();
            ctrlUrusanBaru1.CreateByIUrusanLama(pID);
            ctrlProgram1.Create((int)GlobalVar.TahunAnggaran, m_idDInas, m_IDUrusan);
            LoadMappingProgram();

        }

        private void cmdSImpan_Click(object sender, EventArgs e)
        {
            TProgramAPBDLogic oLogic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);
            if (oLogic.SimpanUrusanBaru(m_IDUrusan, m_idDInas, m_IDProgram, ctrlUrusanBaru1.GetID()) == true)
            {
                MessageBox.Show("Penyimpanan Berhasil");
                LoadMappingProgram();


            }
            else
            {
                MessageBox.Show("Penyimpanan Gagal");
            }
        }

        private void ctrlProgram1_OnChanged(int pID)
        {
            m_IDProgram = pID;

        }
        private void LoadMappingProgram()
        {

            try
            {

                TProgramAPBDLogic oLOgic = new TProgramAPBDLogic(GlobalVar.TahunAnggaran);
                List<TProgramAPBD> _lst = new List<TProgramAPBD>();
                _lst = oLOgic.GetByDinasAndUrusan((int)GlobalVar.TahunAnggaran, m_idDInas, m_IDUrusan);
                gridProgram.Rows.Clear();
                if (_lst != null)
                {
                    foreach (TProgramAPBD p in _lst)
                    {
                        ListItemData item = new ListItemData(p.IDProgram.ToKodeProgram() + " " + p.Nama, p.IDProgram);
                        string[] row = { p.IDUrusan.ToKodeUrusan(), p.IDProgram.ToKodeProgram(), p.Nama, p.IDurusanBaru.ToKodeUrusan(), p.NamaUrusanBaru };

                        gridProgram.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }
    }
}
