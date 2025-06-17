using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DTO.Bendahara;

using BP;
using BP.Bendahara;
using Formatting;


namespace KUAPPAS.Bendahara
{
    public partial class ctrlPotonganx : UserControl
    {
        public ctrlPotonganx()
        {
            InitializeComponent();
        }

        private void gridPotongan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void FormatTampilan()
        {
            gridPotongan.FormatHeader();

        }

        public void Create( Single informasi=0)
        {


            gridPotongan.Rows.Clear();
            gridPotongan.FormatHeader();
            List<Potongan> lst = new List<Potongan>();
            PotonganLogic oLogic = new PotonganLogic(GlobalVar.TahunAnggaran);
            lst = oLogic.Get();

            foreach (Potongan p in lst)
            {
                if (p.Informasi == informasi)
                {
                    string[] row = { p.IDPotongan.ToString(), p.IDPotongan.ToString(), p.Nama ,"0"};
                    gridPotongan.Rows.Add(row);

                }
            }
            

        }
        public bool SetNoUrut(int iJenis, long noUrut)
        {
            //iJenis==1 => SPP
            gridPotongan.FormatHeader();
            if (iJenis == 1)
            {
                PotonganSPPLogic oSPLogic = new PotonganSPPLogic(GlobalVar.TahunAnggaran);
                List<PotonganSPP> lst = oSPLogic.Get(noUrut);
                foreach (PotonganSPP p in lst)
                {
                    for (int row = 0; row < gridPotongan.Rows.Count; row++)
                    {
                        long id = DataFormat.GetLong(gridPotongan.Rows[row].Cells[0].Value);
                        if (id == p.IIDRekening)
                        {
                            gridPotongan.Rows[row].Cells[3].Value = p.Jumlah.ToRupiahInReport();

                        }
                    }

                }

                return true;

            }
            else
            {
                PotonganPanjarLogic oSPLogic = new PotonganPanjarLogic(GlobalVar.TahunAnggaran);
                List<PotonganPanjar> lst = oSPLogic.Get(noUrut);
                foreach (PotonganPanjar p in lst)
                {
                    for (int row = 0; row < gridPotongan.Rows.Count; row++)
                    {
                        long id = DataFormat.GetLong(gridPotongan.Rows[row].Cells[0].Value);
                        if (id == p.IIDRekening)
                        {
                            gridPotongan.Rows[row].Cells[3].Value = p.Jumlah.ToRupiahInReport();

                        }
                    }

                }
                return true;
            }


        }

        
    }
}
