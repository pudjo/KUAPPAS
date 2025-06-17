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
    public partial class frmMapUrusanBaru : Form
    {
        public frmMapUrusanBaru()
        {
            InitializeComponent();
        }

        private void frmMapUrusanBaru_Load(object sender, EventArgs e)
        {
            ctrlUrusan1.Create();
            ctrlUrusanBaru1.Create();
            LoadMappingSemua();

        }
        private void LoadMapping()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MapUrusanBaruLogic oLOgic = new MapUrusanBaruLogic(GlobalVar.TahunAnggaran);
            List<int> lstUrusanBaru = new List<int>();
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                lstUrusanBaru.Add(DataFormat.GetInteger(grid1.Rows[i].Cells[0].Value));

            }
            oLOgic.Simpan(lstUrusanBaru, ctrlUrusan1.GetID());
            LoadMappingSemua();

        }

        private void cmdTambah_Click(object sender, EventArgs e)
        {

            UrusanBaruLogic oLogic = new UrusanBaruLogic(GlobalVar.TahunAnggaran);

            UrusanBaru ub = new UrusanBaru();
            ub = oLogic.GetByID(ctrlUrusanBaru1.GetID());

            string[] row  ={ub.ID.ToString(),ub.Nama} ;
            grid1.Rows.Add(row);

        }

        private void ctrlUrusan1_OnChanged(int pID)
        {
            LoadMappingx();

        }
        private void LoadMappingx()
        {
            MapUrusanBaruLogic oLogic = new MapUrusanBaruLogic(GlobalVar.TahunAnggaran);
            List<MapUrusanUrusanBaru> _lst = new List<MapUrusanUrusanBaru>();
            _lst = oLogic.GetByUrusanLama(ctrlUrusan1.GetID());
            grid1.Rows.Clear();

            foreach (MapUrusanUrusanBaru m in _lst)
            {
                string[] row = { m.UrusanBaru.ToString(), m.Nama };
                grid1.Rows.Add(row);

            }
        }

        private void cmdHapus_Click(object sender, EventArgs e)
        {
            MapUrusanBaruLogic oLogic = new MapUrusanBaruLogic(GlobalVar.TahunAnggaran);
            if (grid1.SelectedRows.Count > 0)
            {

                foreach (DataGridViewRow r in grid1.SelectedRows)
                {
                    MapUrusanUrusanBaru map = new MapUrusanUrusanBaru();
                    map.UrusanBaru = DataFormat.GetInteger(grid1.Rows[r.Index].Cells[0].Value);
                    map.UrusanLama = ctrlUrusan1.GetID();
                    oLogic.Hapus(map);

                }
            }
            MessageBox.Show("Penghapusan Berhasil");
            LoadMappingx();
            LoadMappingSemua();
        }

        private void ctrlUrusan1_Load(object sender, EventArgs e)
        {

        }
        private void LoadMappingSemua()
        {
            MapUrusanBaruLogic oLogic = new MapUrusanBaruLogic(GlobalVar.TahunAnggaran);
            List<MapUrusanUrusanBaru> _lst = new List<MapUrusanUrusanBaru>();
            _lst = oLogic.GetByUrusanLama(0);
            dataGridView1.Rows.Clear();

            int oldID = 0;
            foreach (MapUrusanUrusanBaru m in _lst)
            {
                if (oldID != m.UrusanLama)
                {
                    string[] row = { m.UrusanLama.ToString(), m.NamaLama, m.UrusanBaru.ToString(), m.Nama };
                    dataGridView1.Rows.Add(row);
                }
                else
                {
                    string[] row = { "", "", m.UrusanBaru.ToString(), m.Nama };
                    dataGridView1.Rows.Add(row);
                }

            }


        }
    }
}
