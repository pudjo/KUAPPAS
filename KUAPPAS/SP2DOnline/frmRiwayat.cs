using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BP;
using BP.Bendahara;

using DTO.Bendahara;
using KUAPPAS.SP2DOnline;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Formatting;
using System.Security.Cryptography;
using SP2DOnline;
using DTO.SP2DOnLine.DetailTransaksiSP2DOnline57;
using DTO.SP2DOnLine;

namespace KUAPPAS.SP2DOnline
{
    public partial class frmRiwayat : Form
    {
        public frmRiwayat()
        {
            InitializeComponent();
        }

        private void frmRiwayat_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Riwayat Transaski SP2D Online");
            gridRiwayat.FormatHeader();

        }
        public void GetRiwayat(long NoUrut)
        {

            SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            gridRiwayat.Rows.Clear();
            List<SP2DOnLineLog> Lstlog = new List<SP2DOnLineLog>();
            Lstlog= oSPPLogic.GetLog(NoUrut);
            if (Lstlog != null)
            {

                foreach (SP2DOnLineLog log in Lstlog)
                {
                    string[] row = {"Tanggal "+ log.Waktu.ToString("dd MMM") + " Jam: "+ log.Waktu.ToString("HH-mm"), log.responseKode, log.pesan };
                    gridRiwayat.Rows.Add(row);
                  
                }
            }

           


        }
    }
}
