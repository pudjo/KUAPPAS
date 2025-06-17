using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Akuntansi;
using BP;
using BP.Akuntansi;
using Formatting;

namespace KUAPPAS
{
    public partial class frmMappingRekening : Form
    {
        public frmMappingRekening()
        {
            InitializeComponent();
        }

        private void frmMappingRekening_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            gridMapping.FormatHeader();

            LoadMapping();


        }
        private void LoadMapping()
        {
            List<KOR6413> lst = new List<KOR6413>();
            KOR6413Logic oLogic = new KOR6413Logic((int)GlobalVar.TahunAnggaran);
            gridMapping.Rows.Clear();
            lst = oLogic.Get();//  Get();
            if (lst != null)
            {
                foreach (KOR6413 kor in lst)
                {
                    string bdefault = kor.Default == 1 ? "true" : "false";

                    string[] row = { kor.IIDRekening13.ToString(), kor.NamaRekening13, kor.IIDRekening64.ToString(), kor.NamaRekening64, bdefault };


                    gridMapping.Rows.Add(row);
                }
            }
        }

    }
}
