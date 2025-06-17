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
    public partial class frmIputTimTAPD : Form
    {
        private int m_iJenisAnggaran =0;
        public frmIputTimTAPD()
        {
            InitializeComponent();
            m_iJenisAnggaran = 0;

        }

        private void cmdSimpanTIMTAPD_Click(object sender, EventArgs e)
        {
           
            TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
            List<TimAnggaran> _lst = new List<TimAnggaran>();
            int _idDInas = 0;

            if (chkSemuaDinas.Checked == false)
                _idDInas = ctrlDinas1.GetID();

            if (m_iJenisAnggaran == 0)
            {
                MessageBox.Show("Jenis RKA / DPA belum dipilih.");
                return;
            }

            for (int i = 0; i < gridTimDPA.Rows.Count; i++)
            {
                if (DataFormat.GetString(gridTimDPA.Rows[i].Cells[1].Value).Length > 0)
                {
                    TimAnggaran ta = new TimAnggaran();
                    ta.ID = i;
                    ta.Nama = DataFormat.GetString(gridTimDPA.Rows[i].Cells[1].Value);
                    ta.NIP = DataFormat.GetString(gridTimDPA.Rows[i].Cells[2].Value);
                    ta.Jabatan = DataFormat.GetString(gridTimDPA.Rows[i].Cells[3].Value);
                    ta.Type = 1;
                    ta.DInas = _idDInas;
                    ta.Jenis = m_iJenisAnggaran;

                    _lst.Add(ta);
                }
            }
            if (_idDInas == 0)
            {

                if (MessageBox.Show("Apakah benar akan berlaku untuk semua dinas?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (taLogic.Simpan(_lst, GlobalVar.TahunAnggaran, 1, _idDInas, m_iJenisAnggaran, 1) == true)
                    {
                        MessageBox.Show("Penyimpanan TIM DPA berhasil.");
                    }
                    else
                    {
                        MessageBox.Show("Kesalahan Penyimpanan TIM DPA." + taLogic.LastError());
                    }
                }

            }
            else
            {
                if (taLogic.Simpan(_lst, GlobalVar.TahunAnggaran, 1, ctrlDinas1.GetID(), m_iJenisAnggaran, 1) == true)
                {
                    MessageBox.Show("Penyimpanan TIM DPA berhasil.");
                }
                else
                {
                    MessageBox.Show("Kesalahan Penyimpanan TIM DPA." + taLogic.LastError());
                }
            }
        }



        private void chkSemuaDinas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSemuaDinas.Checked== true)// Semua Dinas
            {
                if (GlobalVar.Pengguna.SKPD > 0)
                    cmdSimpanTIMTAPD.Enabled = false;
                else
                    cmdSimpanTIMTAPD.Enabled = true;

                LoadTimDPA();


            }
            else
            {
                //if (GlobalVar.Pengguna.SKPD > 0)
                //    cmdSimpanTIMTAPD.Enabled = false;
                
                LoadTimDPA();

            }
        }

        private void frmInoutTimDPA_Load(object sender, EventArgs e)
        {
            this.Text = "Set Tim TAPD";
            ctrlDinas1.Create();
            ctrlHeader1.SetCaption("Setting Tim TAPD.", "");

            //if (GlobalVar.Pengguna.SKPD == 0)
            //{
            //    chkSemuaDinas.Checked = true;
            //    cmdSimpanTIMTAPD.Enabled = true;


            //}
            //else
            //{
                chkSemuaDinas.Checked = false;
                chkSemuaDinas.Enabled = false;
                

          //  }
            ctrlDinas1.Create();
            ctrlJenisAnggaran1.Create(1);
            m_iJenisAnggaran = ctrlJenisAnggaran1.GetID() ;
            LoadTimDPA();

        }
        private void LoadTimDPA()
         {
             TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
             List<TimAnggaran> _lst = new List<TimAnggaran>();
             int _idDInas = 0;
             if (chkSemuaDinas.Checked == false)
                 _idDInas = ctrlDinas1.GetID();
            
             _lst = taLogic.Get(GlobalVar.TahunAnggaran, 1,_idDInas,m_iJenisAnggaran );

             gridTimDPA.Rows.Clear();
             gridTimViewer.Rows.Clear();

             foreach (TimAnggaran ta in _lst)
             {
                 string[] row = { "Hapus", ta.Nama, ta.NIP, ta.Jabatan , ta.ID.ToString()};
                 if (ta.Type == 1)
                 {
                     gridTimDPA.Rows.Add(row);
                 }
                 else
                 {
                     gridTimViewer.Rows.Add(row);
                 }

                
             }
         }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            LoadTimDPA();
            
        }

        private void cmdHapusTimTAPD_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Apakah benar akan menghapu data tim Anggaran ? ", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
            List<TimAnggaran> _lst = new List<TimAnggaran>();
            int _idDinas =ctrlDinas1.GetID();
            if (_idDinas > 0)
            {

                if (taLogic.Hapus(GlobalVar.TahunAnggaran, _idDinas,ctrlJenisAnggaran1.GetID()) == true)
                {
                    MessageBox.Show("Tim TAPD sudah Dihapus");
                    LoadTimDPA();
                }
            } else
                MessageBox.Show("Tim TAPD utk semua dinas tidak bisa dihapus");
        }

        private void ctrlJenisAnggaran1_OnChanged(int pID)
        {
            
            m_iJenisAnggaran = pID;
            LoadTimDPA();


        }

        private void ctrlJenisAnggaran1_Load(object sender, EventArgs e)
        {

        }

        private void cmdSimpanTimViewer_Click(object sender, EventArgs e)
        {
            TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
            List<TimAnggaran> _lst = new List<TimAnggaran>();
            int _idDInas = 0;

            if (chkSemuaDinas.Checked == false)
                _idDInas = ctrlDinas1.GetID();

            if (m_iJenisAnggaran == 0)
            {
                MessageBox.Show("Jenis RKA / DPA belum dipilih.");
                return;
            }

            for (int i = 0; i < gridTimViewer.Rows.Count; i++)
            {
                if (DataFormat.GetString(gridTimViewer.Rows[i].Cells[1].Value).Length > 0)
                {
                    TimAnggaran ta = new TimAnggaran();
                    ta.ID = i;
                    ta.Nama = DataFormat.GetString(gridTimViewer.Rows[i].Cells[1].Value);
                    ta.NIP = DataFormat.GetString(gridTimViewer.Rows[i].Cells[2].Value);
                    ta.Jabatan = DataFormat.GetString(gridTimViewer.Rows[i].Cells[3].Value);
                    ta.Type = 2;
                    ta.DInas = _idDInas;
                    ta.Jenis = m_iJenisAnggaran;

                    _lst.Add(ta);
                }
            }
            if (taLogic.Simpan(_lst, GlobalVar.TahunAnggaran, 1, ctrlDinas1.GetID(), m_iJenisAnggaran, 2) == true)
            {
                MessageBox.Show("Penyimpanan TIM DPA berhasil.");
            }
            else
            {
                MessageBox.Show("Kesalahan Penyimpanan TIM DPA." + taLogic.LastError());
            }
        }

        private void gridTimDPA_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Apakah benar akan menghapus data tim Anggaran ? ", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                TimAnggaranLogic taLogic = new TimAnggaranLogic(GlobalVar.TahunAnggaran);
                List<TimAnggaran> _lst = new List<TimAnggaran>();
                int _idDinas = ctrlDinas1.GetID();
                if (_idDinas > 0)
                {
                    int id = DataFormat.GetInteger(gridTimDPA.Rows[e.RowIndex].Cells[4].Value.ToString() );

                    if (taLogic.HapusPerID(GlobalVar.TahunAnggaran, _idDinas, ctrlJenisAnggaran1.GetID(),id) == true)
                    {
                        MessageBox.Show("Tim TAPD sudah Dihapus");
                        LoadTimDPA();
                    }
                }
                else
                    MessageBox.Show("Tim TAPD utk semua dinas tidak bisa dihapus");

            }


        }

        

    }

}
