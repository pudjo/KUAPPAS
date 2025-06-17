using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;
namespace KUAPPAS
{
    public partial class ctrlFooter : UserControl
    {
        private int JenisDokumen;

        public ctrlFooter()
        {
            InitializeComponent();
        }

        private void ctrlFooter_Load(object sender, EventArgs e)
        {

        }
        public int IDCrt{
            set
            {
                if (GlobalVar.glistPengguna == null)
                {
                    PenggunaLogic oLogic = new PenggunaLogic(GlobalVar.TahunAnggaran);
                    GlobalVar.glistPengguna = oLogic.Get((int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD);
                }
                Pengguna p = GlobalVar.glistPengguna.FirstOrDefault(x => x.ID== value);
                if (p != null)
                {
                    lblidcrt.Text = p.Nama;

                }

            }
        }
        public int IDUpdater
        {
            set
            {
                Pengguna p = GlobalVar.glistPengguna.FirstOrDefault(x => x.ID == value);
                if (p != null)
                {
                    lblidupdate.Text = p.Nama;

                }

            }
        }
        public DateTime WaktuBuat
        {
            set
            {
                lbldcrt.Text = value.ToTanggalIndonesia();
            }
        }
        public DateTime WaktuUpdate
        {
            set
            {
                lbldupdate.Text = value.ToTanggalIndonesia();
            }
        }




    }
}
