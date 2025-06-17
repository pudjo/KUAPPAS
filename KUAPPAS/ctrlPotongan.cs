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


namespace KUAPPAS

{
    public partial class ctrlPotongan : UserControl
    {
        public delegate void ValueChangedEventHandler(decimal pJumlah);
        public event ValueChangedEventHandler OnChanged;
        public decimal dRet;
        private decimal m_cJumlah;
        public ctrlPotongan()
        {
            InitializeComponent();
            dRet = 0l;
            m_cJumlah = 0;
        }

        public bool CrreateNonMpn()
        {
            try{
            PotonganLogic oLogic = new PotonganLogic (GlobalVar.TahunAnggaran);
            List <Potongan> lstPotongan = new List<Potongan>();

            gridPotongan.Rows.Clear();
            lstPotongan= oLogic.Get(0);
            
                gridPotongan.Columns[5].Visible = false;
                gridPotongan.Columns[6].Visible = false;
                gridPotongan.Columns[7].Visible = false;
                gridPotongan.Columns[8].Visible = false;
                gridPotongan.Columns[9].Visible = false;
            

                if (lstPotongan!=null){
                    foreach (Potongan p in lstPotongan)
                    {
                        string[] row = { p.IDPotongan.ToString(), p.IDPotongan.ToString(), p.Nama, "0", p.IDPotongan.ToString(), "", "", "", "", "", "Hapus" };

                        gridPotongan.Rows.Add(row);
                        

                    }
                }

            return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                
            }
        
        }
        public bool PajakSudahDisetor()
        {
            bool ret = false;
            foreach (DataGridViewRow row in gridPotongan.Rows)
            {
                if (DataFormat.GetLong(row.Cells[4].Value)> 0)
                {
                    ret = true;
                    return ret;
                }

            }
            return ret;

        }
        public bool CreateUntukSPJ()
        {
            try
            {
                gridPotongan.FormatHeader();
                gridPotongan.Columns[0].HeaderText = "No";
                gridPotongan.Columns[1].HeaderText = "Kode";
                gridPotongan.Columns[2].HeaderText = "Nama Potongan";
                gridPotongan.Columns[3].HeaderText = "Jumlah";
                gridPotongan.Columns[4].HeaderText = "NoUrutSetor";
              //  gridPotongan.Columns[4].Visible = false;
                gridPotongan.Columns[5].HeaderText = "No Bukti Setor";
                gridPotongan.Columns[6].HeaderText = "Id Billing";
                gridPotongan.Columns[7].HeaderText = "N T P N";
                for (int c = 8; c < gridPotongan.Columns.Count; c++)
                {
                    gridPotongan.Columns[c].Visible = false;
                }

                PotonganLogic oLogic = new PotonganLogic(GlobalVar.TahunAnggaran);
                List<Potongan> lstPotongan = new List<Potongan>();

                gridPotongan.Rows.Clear();
                lstPotongan = oLogic.Get(-1);          


                if (lstPotongan != null)
                {
                    foreach (Potongan p in lstPotongan)
                    {
                        string[] row = { p.IDPotongan.ToString(), p.IDPotongan.ToString(), p.Nama, "0", "", "", "", "", "", "","","","", "Hapus" };

                        gridPotongan.Rows.Add(row);


                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }
        private void gridPotongan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                if (MessageBox.Show("Apakah benar akan menghapus data potongan ini?", "Konfirmasi Penghapusan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    long noUrut = DataFormat.GetLong(gridPotongan.Rows[e.RowIndex].Cells[9].Value);
                    int idrekeningPotongan = DataFormat.GetInteger(gridPotongan.Rows[e.RowIndex].Cells[0].Value);
                    PotonganSPPLogic oLogic = new PotonganSPPLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.Hapus(noUrut, idrekeningPotongan)==true)
                    {
                        gridPotongan.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        MessageBox.Show(oLogic.LastError());

                    }
                }
            }
        }
        public decimal JumlahPotongan
        {
            get
            {
                return m_cJumlah;
            }
        }
        public void FormatTampilan()
        {
            gridPotongan.FormatHeader();

        }
        public bool TambahPotongan(Potongan p, decimal nilai =0)
        {
            try
            {

                for (int i = 0; i < gridPotongan.Rows.Count; i++)
                {
                    if (DataFormat.GetInteger(gridPotongan.Rows[i].Cells[4].Value) == p.IDPotongan)
                    {
                        MessageBox.Show("Sudah ada dalam daftar dengan nilai " + DataFormat.GetDecimal(gridPotongan.Rows[i].Cells[3].Value).ToRupiahInReport());
                        return false;
                    } 

                }
                string[] row = { p.IDPotongan.ToString(), p.IDPotongan.ToString(), p.Nama, nilai.ToRupiahInReport(), p.IDPotongan.ToString(), "", "", "" ,""};
                gridPotongan.Rows.Add(row);
                HitungJumlah();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
        }
        private void HitungJumlah()
        {
            m_cJumlah = 0;
            foreach (DataGridViewRow row in gridPotongan.Rows)
            {
                decimal nilai;
                nilai = DataFormat.GetString(row.Cells[3].Value).FormatUangReportKeDecimal();
                m_cJumlah = m_cJumlah + nilai;
            }
            
         }
      
        public void AddPotongan(PotonganSPP p)
        {

            foreach (DataGridViewRow r in gridPotongan.Rows)
            {
                if (r.Cells[0].Value != null)
                {
                    if (DataFormat.GetLong(r.Cells[0].Value) == p.IIDRekening)
                    {
                        MessageBox.Show("Pajak sudah ada dlam daftar.");
                        return;
                    }
                }
            }
            string[] row = { p.IIDRekening.ToString(), //0
                               p.IIDRekening.ToString(), //1
                               p.Nama, //2
                               p.Jumlah.ToRupiahInReport(),//3
                               p.IIDRekening.ToString(),  //4
                               p.KodeMap, //5
                               p.KodeSetor.Trim(),//6 
                               p.IDBilling, //7
                               p.NTPN,//8
                               p.NoUrut.ToString(),//9
                               p.NPWPPenyetor,//10
                               p.NoFaktur,
                               p.NIKRekeninan,
                               "Hapus" };
            gridPotongan.Rows.Add(row);
            HitungJumlah();

        }
     

        public decimal SetNoUrut(int iJenis, long noUrut)
        {
            //iJenis==1 => SPP
            if (iJenis == 0)
            {
                CrreateNonMpn();

            }
            //if (iJenis == 1)
            //{
            decimal dRet=0L;

                PotonganSPPLogic oSPLogic = new PotonganSPPLogic(GlobalVar.TahunAnggaran);
                List<long> lstNoUrut = new List<long>();
                lstNoUrut.Add(noUrut);
                List<PotonganSPP> lst = oSPLogic.Get(lstNoUrut);
                if (oSPLogic.IsError() == true)
                {
                    MessageBox.Show(oSPLogic.LastError());
                    return 0;
                }
               
                if (iJenis == 1)
                {
                    gridPotongan.Rows.Clear();
                    foreach (PotonganSPP p in lst)
                    {

                        if (p.Informasi == 0)
                        {
                            string[] row = { p.IIDRekening.ToString(), p.IIDRekening.ToString(), p.Nama, p.Jumlah.ToRupiahInReport(), p.IIDRekening.ToString(), p.KodeMap, p.KodeSetor, p.IDBilling.ToString().Trim(), p.NTPN, p.NoUrut.ToString(),p.NPWPPenyetor,p.NoFaktur,p.NIKRekeninan };
                            if (p.NoUrut == noUrut)
                            {

                                gridPotongan.Rows.Add(row);
                                dRet = dRet + p.Jumlah;
                            }
                        }
                    }
                }
                else
                {
                    foreach (PotonganSPP p in lst)
                    {
                        // jika 
                        if (p.Informasi == 1)
                        {
                            for (int row = 0; row < gridPotongan.Rows.Count; row++)
                            {
                                if (DataFormat.GetInteger (gridPotongan.Rows[row].Cells[1].Value)== p.IIDRekening){
                                    gridPotongan.Rows[row].Cells[3].Value= p.Jumlah.ToRupiahInReport();
                                }
                            }
                        }
                    }

                }
                HitungJumlah();

                return dRet;


        }
        public decimal SetNoUrutSPJ(long noUrut)
        {
            
            CreateUntukSPJ();

       
        
            decimal dRet = 0L;

            PengeluaranLogic oSPJLogic = new PengeluaranLogic(GlobalVar.TahunAnggaran);
            List<PotonganPanjar> lst = oSPJLogic.GetPotongan(noUrut);
            if (oSPJLogic.IsError() == true)
            {
                MessageBox.Show(oSPJLogic.LastError());
                return 0;
            }
            int no;
            no=0;
            decimal lRet = 0L;
            foreach (PotonganPanjar p in lst)
            { 
                foreach(DataGridViewRow row in gridPotongan.Rows){
                    if (DataFormat.GetLong(row.Cells[1].Value)== p.IIDRekening){
                        row.Cells[3].Value= p.Jumlah.ToRupiahInReport();
                        row.Cells[4].Value= p.NoUrutSetor.ToString();
                        row.Cells[5].Value= p.NoBuktiSetor;
                        row.Cells[6].Value= p.KodeBilling;
                        row.Cells[7].Value= p.NTPN;
                        lRet = lRet + p.Jumlah;
                    }
                }
            }

            return lRet;
        }

        public List<PotonganSPP> getDisplayRekening()
        {
            List<PotonganSPP> lst = new List<PotonganSPP>();
            for (int idx = 0; idx < gridPotongan.Rows.Count; idx++)
            {
                if (gridPotongan.Rows[idx].Cells[2].Value != null)
                {
                    

                    PotonganSPP sr = new PotonganSPP();
                   // MessageBox.Show(gridPotongan.Rows[idx].Cells[0].Value.ToString());
                    sr.NamaRekening = gridPotongan.Rows[idx].Cells[2].Value.ToString();
                    sr.KodeRekening = gridPotongan.Rows[idx].Cells[1].Value.ToString();
                    sr.JumlahString = gridPotongan.Rows[idx].Cells[3].Value.ToString();
                    sr.IIDRekening = DataFormat.GetLong(gridPotongan.Rows[idx].Cells[0].Value.ToString());
                    sr.IDBilling = DataFormat.GetString(gridPotongan.Rows[idx].Cells[7].Value.ToString());
                    sr.KodeMap = DataFormat.GetString(gridPotongan.Rows[idx].Cells[5].Value.ToString());
                    sr.KodeSetor = DataFormat.GetString(gridPotongan.Rows[idx].Cells[6].Value.ToString());
                    sr.Jumlah =DataFormat.GetString(gridPotongan.Rows[idx].Cells[3].Value.ToString()).FormatUangReportKeDecimal ();
                    sr.NoUrut = DataFormat.GetLong(gridPotongan.Rows[idx].Cells[9].Value);
                    sr.NPWPPenyetor = DataFormat.GetString(gridPotongan.Rows[idx].Cells[10].Value);//.ToString());
                    sr.NoFaktur = DataFormat.GetString(gridPotongan.Rows[idx].Cells[11].Value);//.ToString());
                    sr.NIKRekeninan = DataFormat.GetString(gridPotongan.Rows[idx].Cells[12].Value);//.ToString());


                    lst.Add(sr);
                }

            }
            return lst;
        }
        public List<PotonganPanjar> getPenjarPotongan()
        {
            List<PotonganPanjar> lst = new List<PotonganPanjar>();
            for (int idx = 0; idx < gridPotongan.Rows.Count; idx++)
            {
                if (gridPotongan.Rows[idx].Cells[2].Value != null)
                {
                    //gridPotongan.Columns[0].HeaderText = "No";
                    //gridPotongan.Columns[1].HeaderText = "Kode";
                    //gridPotongan.Columns[2].HeaderText = "Nama Potongan";
                    //gridPotongan.Columns[3].HeaderText = "Jumlah";
                    //gridPotongan.Columns[4].HeaderText = "NoUrutSetor";
                    //gridPotongan.Columns[4].Visible = false;
                    //gridPotongan.Columns[5].HeaderText = "No Bukti Setor";
                    //gridPotongan.Columns[6].HeaderText = "Id Billing";
                    //gridPotongan.Columns[7].HeaderText = "N T P N";


                    PotonganPanjar pp = new PotonganPanjar();
                    // MessageBox.Show(gridPotongan.Rows[idx].Cells[0].Value.ToString());
                    //pp.NamaRekening = gridPotongan.Rows[idx].Cells[2].Value.ToString();
                    pp.IIDRekening = DataFormat.GetLong(gridPotongan.Rows[idx].Cells[1].Value.ToString().Replace(".", ""));

                    pp.Jumlah = DataFormat.FormatUangReportKeDecimal(gridPotongan.Rows[idx].Cells[3].Value.ToString());
                    pp.NoUrutSetor = DataFormat.GetLong(gridPotongan.Rows[idx].Cells[4].Value.ToString());
                    pp.StatusPajak = pp.NoUrutSetor > 0 ? 1 : 0;

                    if (pp.Jumlah>0)
                    lst.Add(pp);
                }

            }
            return lst;
        }

        private void gridPotongan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                decimal h = DataFormat.GetDecimal(gridPotongan.Rows[e.RowIndex].Cells[3].Value);

                gridPotongan.Rows[e.RowIndex].Cells[3].Value = h.ToRupiahInReport();
                HitungJumlah();
                if (OnChanged != null)
                {
                    OnChanged(m_cJumlah);
                }

            }
        }
    }
}
