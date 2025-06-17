using BP.Akuntansi;
using DTO.Akuntansi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting;

namespace KUAPPAS.Akunting
{
    public partial class frmSelisihTrxBB : Form
    {
        private long m_IDRekening;
        private int m_IDDInas;
        private DateTime m_tanggal;

        public frmSelisihTrxBB()
        {
            InitializeComponent();
        }
        public long IDRekening
        {
            set
            {
                m_IDRekening = value;
                txtIDRekening.Text = value.ToString();
            }
        }
        public int Dinas
        {
            set
            {
                m_IDDInas = value;
            }
        }
        public DateTime TanggalBatas
        {
            set
            {
                m_tanggal = value;
            }
        }
        public string NamaRekening
        {
            set
            {
                label1.Text = value;

            }
        }
        private void frmSelisihTrxBB_Load(object sender, EventArgs e)
        {
            gridSelisih.FormatHeader();

        }

        private void cmdTampilkan_Click(object sender, EventArgs e)
        {
            
            gridSelisih.Rows.Clear();
            JurnalLogic logic = new JurnalLogic(GlobalVar.TahunAnggaran);
            List<SelisihTrxBB> lst = new List<SelisihTrxBB>();
            lst = logic.GetSelisihTrxBB(m_IDRekening, m_IDDInas,m_tanggal);
            if (lst != null)
            {
                int i = 0;
                foreach(SelisihTrxBB s in lst){
                    string[] row = { s.NoUrut.ToString(), 
                        s.NoBukti, 
                        s.Tanggal.ToTanggalIndonesia(), 
                        s.Trx.ToRupiahInReport(),
                        s.BB.ToRupiahInReport() ,"0"};

                    gridSelisih.Rows.Add(row);
                    if (s.Trx != s.BB)
                    {
                        gridSelisih.Rows[i].Cells[5].Value = "1";
                        gridSelisih.Rows[i].DefaultCellStyle.BackColor = Color.MediumVioletRed ;
                    }
                    i++;


                }
            }


           
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridSampah_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                
              

            }
        }

        private void cmdHapussemua_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdHapusRekeningIni_Click(object sender, EventArgs e)
        {
            ProsesJurnalLogic oLogic = new ProsesJurnalLogic(GlobalVar.TahunAnggaran, m_IDDInas);
            if (MessageBox.Show ("Apakah benar akan menghapus Rekening ini dari Buku besar?. ","Konfirmasi", MessageBoxButtons.YesNo)==DialogResult.Yes){
                long idRekening = DataFormat.GetLong(txtIDRekening.Text.Replace(".",""));
                if (oLogic.HapusJurnalBukuBesarOfRekening (idRekening, m_IDDInas))
                {
                    MessageBox.Show("Data untuk Rekening ini sudah dihapus. Sila jurnal ulang yang terhapus jurnalnya..");
                }
                else
                {
                    MessageBox.Show("Terjadi kesalahan penghapusan bukubesar untu k rekening ini");
                }

           } 
           
        }

        private void chkTampilkanYangbeda_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (chkTampilkanYangbeda.Checked == true)
                {
                    for (int row = 0; row < gridSelisih.Rows.Count; row++)
                    {
                        if (gridSelisih.Rows[row].Cells[5].Value != null)
                        {
                            if (DataFormat.GetInteger(gridSelisih.Rows[row].Cells[5].Value) == 0)
                            {
                                gridSelisih.Rows[row].Visible = false;
                            }
                        }

                    }
                } else
                {
                    for (int row = 0; row < gridSelisih.Rows.Count; row++)
                    {
                        
                            gridSelisih.Rows[row].Visible = true ;
                        

                    }

                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
