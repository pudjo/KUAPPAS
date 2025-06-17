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
using KUAPPAS.SP2DOnline;
using BP;

namespace KUAPPAS
{
    public partial class frmSP2DOnlineDaftarBank : Form
    {
        public frmSP2DOnlineDaftarBank()
        {
            InitializeComponent();
        }

        private void froSP2DOnlineDaftarBank_Load(object sender, EventArgs e)
        {

        }
        public void SetBanks( List<Banks> lstBank ){

            gridBank.Rows.Clear();
            foreach (Banks b in lstBank)
            {
                string[] row= {"","",b.bankCode, b.Nama };
                gridBank.Rows.Add(row);

            }
        }
        //public void SetBICs(List<Kodebic> lstBIC)
        //{

        //    gridDaftarbank.Rows.Clear();
        //    foreach (Kodebic b in lstBIC)
        //    {
        //        string[] row = {b.bic, b.name, b.shortName, b.cityCode,b.postalCode,b.address}; // saccountNumber, b.bic, b.bankCode, b.participantName };
        //        gridDaftarbank.Rows.Add(row);

        //    }
        //}

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            BanksLogic ologic = new BanksLogic(GlobalVar.TahunAnggaran,0);
            List<Banks> lstBank = new List<Banks>();
            for (int r = 0; r < gridBank.Rows.Count; r++)
            {
                if (gridBank.Rows[r].Cells[0].Value != null)
                {
                    Banks oBank = new Banks();
                    oBank.bankCode = gridBank.Rows[r].Cells[2].Value.ToString().Trim();
                    oBank.Nama = gridBank.Rows[r].Cells[3].Value.ToString().Trim();
                    lstBank.Add(oBank);
                }

            }
            if (ologic.SImpanBanks(lstBank) == false)
            {
                MessageBox.Show(ologic.LastError());

            }

        }
    }
}
