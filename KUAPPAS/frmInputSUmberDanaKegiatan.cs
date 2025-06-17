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
    public partial class frmInputSUmberDanaKegiatan : Form
    {
      
       


        List<string> lstKodeRekening = new List<string>();

        private bool mbstatusinput;
        private int m_IDDInas;
        private List<StandardBiaya> _lstSB;
        List<StandardBiaya> lSB = new List<StandardBiaya>();

        List<KORBELANJABARANG> lstKorBarangRekening;


        private int m_IDProgram;
        private int m_IDKegiatan;
        private long m_IDSubKegiatan;
        private int m_IDUrusan;
        private int m_ijenis;
        private decimal m_dPagu = 0L;
        private Urusan m_oUrusan;
        private TProgramAPBD m_oProgram;
        private TKegiatanAPBD m_oKegiatan;
        private TSubKegiatan m_oSubKegiatan;
        private int m_iTahun;
        private int m_iUbit;


        delegate void SetComboBoxCellType(int iRowIndex, int _col, int _value);
        //bool bIsComboBox = false;
        //bool bDesaIsComboBox = false;
        private int m_iCurrentRow;
        private int m_iCurrentRowSB;
        private int m_iRowJustAdded;
        List<DataGridViewCell> containingCells = new List<DataGridViewCell>();
        int currentContainingCellListIndex;

        int mProfile;
        public frmInputSUmberDanaKegiatan()
        {
            InitializeComponent();
        }

        private void frmInputSUmberDanaKegiatan_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ctrlDinas1.Create();
            m_IDDInas = ctrlDinas1.GetID();
            treeSumberDana1.Create();

          

           
        }
        private bool SimpanSumberDana()
        {
            try
            {
                List<TSumberDana> _lsd = new List<TSumberDana>();
                TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 1);
                //if (GlobalVar.PP90 = false)
                   // m_IDSubKegiatan = 0;

                for (int i = 0; i < gridSumberDana.Rows.Count; i++)
                {
                    DataGridViewRow row = gridSumberDana.Rows[i];
                    DataGridViewCheckBoxCell chk = row.Cells[1] as DataGridViewCheckBoxCell;
                    if (chk.Value != null)
                    {
                        if (DataFormat.GetBoolean(row.Cells[1].Value) == true)
                        {
                            TSumberDana oTSD = new TSumberDana();
                            oTSD.Tahun = (int)GlobalVar.TahunAnggaran;
                            oTSD.IDDinas = ctrlDinas1.GetID();
                            oTSD.IDKegiatan = m_IDKegiatan;
                            oTSD.IDProgram = m_IDProgram;
                            oTSD.IDUrusan = m_IDUrusan;
                            oTSD.IDSubKegiatan = m_IDSubKegiatan;
                            oTSD.IDRekening = DataFormat.GetInteger(gridSumberDana.Rows[i].Cells[0].Value);
                            oTSD.IDSUmberDana = DataFormat.GetInteger(gridSumberDana.Rows[i].Cells[0].Value);
                            oTSD.Jumlah = DataFormat.GetDecimal(gridSumberDana.Rows[i].Cells[3].Value); ;
                            _lsd.Add(oTSD);


                        }
                    }
                }
                oLogic.Simpan(_lsd, GlobalVar.TahunAnggaran, m_IDUrusan, ctrlDinas1.GetID(), m_IDProgram, m_IDKegiatan, m_IDSubKegiatan);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penyimpanan sumber dana gagal" + ex.Message);

                return false;
            }

        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            //LoadSumberMasterDana();
            m_IDDInas= pIDSKPD;
            TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran, 1);
            // oLogic.CekKUAdanKegiatan(GlobalVar.TahunAnggaran, ctrlDinas1.GetID());
            treeProgramKegiatan1.Profile = 1 ;

            treeProgramKegiatan1.Create(pIDSKPD, 0);

        }
        private void LoadSumberDana()
        {
            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran, 1);
            List<TSumberDana> _lst = new List<TSumberDana>();
            if (GlobalVar.PP90 == false)
            {
                m_IDSubKegiatan = 0;

            }
            _lst = oLogic.Get((int)GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDSubKegiatan);
            if (_lst != null)
            {
                foreach (TSumberDana ts in _lst)
                {
                    //for (int j = 0; j < gridSumberDana.Rows.Count; j++)
                    //{
                        //if (DataFormat.GetInteger(gridSumberDana.Rows[j].Cells[0].Value) == ts.IDSUmberDana)
                        //{
                            //DataGridViewRow row = gridSumberDana.Rows[j];
                            //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[1];

                            //chk.Selected = true;
                            //chk.Value = true;

                     //   }
                    //}
                    string[] row = { ts.IDRekening.ToString(), "true", ts.Nama, ts.Jumlah.ToRupiahInReport() };
                    gridSumberDana.Rows.Add(row);


                }
            }
            RefreshJumlah();

        }

        private void treeSumberDana1_Load(object sender, EventArgs e)
        {

        }

        private void treeSumberDana1_DoubleClicking(SumberDana rek)
        {
            if (rek.Leaf  ==0)
            {
                MessageBox.Show("Sila Pilih yang paling rinci");
                return;

            }
            string[] row = { rek.IDRekening.ToString(), "true", rek.Nama, "0" };
            gridSumberDana.Rows.Add(row);


        }

        private EventResponseMessage treeProgramKegiatan1_SubKegiatanChanged(long ID)
        {
            EventResponseMessage lRet = new EventResponseMessage();
            lRet.ResponseStatus = true;

            if (GlobalVar.PP90 == false)
                return lRet;

            //m_IDSubKegiatan = ID;
            if (ID == 0)
                return lRet;
            int idKegeiatan = DataFormat.GetInteger(ID.ToString().Substring(0, 8));

            if (m_IDKegiatan != m_IDSubKegiatan / 100)
            {
                //  MessageBox.Show("IDKegiatan tidak Konsisten betul..");
                m_IDKegiatan = (int)(m_IDSubKegiatan / 100);
            }
            if (m_IDProgram != m_IDSubKegiatan / 100000)
            {
                //    MessageBox.Show("IDProgram tidak Konsisten betul..");
                m_IDProgram = (int)(m_IDSubKegiatan / 100000);
            }
            if (m_IDUrusan != m_IDSubKegiatan / 10000000)
            {
                //    MessageBox.Show("IDKegiatan tidak Konsisten betul..");
                m_IDUrusan = (int)m_IDSubKegiatan / 10000000;
            }
            // if (GlobalVar.TahunAnggaran < 2021) { 



            RefreshSubKegiatan(ID);
            gridSumberDana.Rows.Clear();
                DisplaySubKegiatan(m_oSubKegiatan);

               
                LoadSumberDana();
               

           


            return lRet;


        }
        private bool  RefreshSubKegiatan( long idSubKegiatan)
        {
                if (GlobalVar.PP90 == true && idSubKegiatan > 0)
                {
                    if (m_oSubKegiatan != null)
                    {
                        if (m_oSubKegiatan.IDSubKegiatan != m_IDSubKegiatan || m_IDSubKegiatan != idSubKegiatan)
                        {
                            TSubKegiatanLogic oKAPBDLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, mProfile);
                            m_oSubKegiatan = new TSubKegiatan();
                            if (ctrlDinas1.WithUnitKerja() == false)
                            {
                                m_oSubKegiatan = oKAPBDLOgic.GetSubKegiatan(GlobalVar.TahunAnggaran, m_IDDInas, 0, m_IDUrusan, m_IDProgram, m_IDKegiatan, idSubKegiatan);

                            } else {
                                m_oSubKegiatan = oKAPBDLOgic.GetSubKegiatan(GlobalVar.TahunAnggaran, m_IDDInas, m_iUbit, m_IDUrusan, m_IDProgram, m_IDKegiatan, idSubKegiatan);
                        }


                            if (m_oSubKegiatan == null)
                            {
                                return false;

                            }
                            m_IDSubKegiatan = idSubKegiatan;
                            int lenSubKed = m_oSubKegiatan.IDSubKegiatan.ToString().Length;
                            if (lenSubKed> 2)
                                lblSubKegiatan.Text = m_oSubKegiatan.IDSubKegiatan.ToString().Substring(lenSubKed - 2);

                            lbllNamaSubKegiatan.Text = m_oSubKegiatan.Nama;
                            

                        }
                    }
                    else
                    {
                        TSubKegiatanLogic oKAPBDLOgic = new TSubKegiatanLogic(GlobalVar.TahunAnggaran, mProfile);
                            m_oSubKegiatan = new TSubKegiatan();
                            m_oSubKegiatan = oKAPBDLOgic.GetSubKegiatan(GlobalVar.TahunAnggaran, m_IDDInas,0,m_IDUrusan, m_IDProgram, m_IDKegiatan, idSubKegiatan);

                            if (m_oSubKegiatan == null)
                            {
                                return false;

                            }
                            m_IDSubKegiatan = idSubKegiatan;
                            lblSubKegiatan.Text = m_oSubKegiatan.KodePendek;
                            lbllNamaSubKegiatan.Text = m_oSubKegiatan.Nama;
                            txtDPA.Text = m_oSubKegiatan.Pagu.ToRupiahInReport();

                        
                       

                    }


                }
                return true;

        
        }
        private void DisplaySubKegiatan(TSubKegiatan oKeg)
        {

            // LoadSumberMasterDana();
            ClearSumberDana();
        
        

            if (m_oSubKegiatan != null)
            {
                string sKode = m_oSubKegiatan.IDSubKegiatan.ToString();
                lblSubKegiatan.Text = sKode.Substring(sKode.Length - 2);//" " + m_oSubKegiatan.Nama ;//lblKodeKegiatan.Text = oKeg.TampilanKode;
                lbllNamaSubKegiatan.Text = m_oSubKegiatan.Nama;
                txtDPA.Text = m_oSubKegiatan.Pagu.ToRupiahInReport();
        

            }

        }
        private void ClearSumberDana()
        {
            for (int id = 0; id < gridSumberDana.Rows.Count; id++)
            {
                DataGridViewCell cell = gridSumberDana.Rows[id].Cells[1];
                cell.Value = false;


            }
        }

        private void cmdSimpanSUmberDana_Click(object sender, EventArgs e)
        {
            SimpanSumberDana();

        }

        private void gridSumberDana_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
                //if (gridSumberDana.Rows[e.RowIndex].Cells[0].Value.ToString().FormatUangReportKeDecimal() <
                //    gridSumberDana.Rows[e.RowIndex].Cells[7].Value.ToString().FormatUangReportKeDecimal())
                //{
                //    MessageBox.Show("Tidak boleh lebih kecil dari Realisasi..");
                //    cmdSimpan.Enabled = true;
                //    // return;

                //}
                //else
                //{
            if (e.ColumnIndex== 3){

                RefreshJumlah();
                    
                }
        
            
        }
        private void RefreshJumlah()
        {
            decimal jumlah = 0L;
      


            for (int i = 0; i < gridSumberDana.Rows.Count; i++)
            {
                if (gridSumberDana.Rows[i].Cells[3].Value != null)
                {
                    decimal d = gridSumberDana.Rows[i].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                    jumlah = jumlah + d;
                }
            }
            txtJumlah.Text = jumlah.ToRupiahInReport();
            if (txtDPA.Text.FormatUangReportKeDecimal() < jumlah)
            {
                MessageBox.Show("Melebihi nilai Anggaran");
                cmdSimpanSUmberDana.Enabled = false;
            } else 
            cmdSimpanSUmberDana.Enabled = true ;

         


        }

        private void treeProgramKegiatan1_Load(object sender, EventArgs e)
        {

        }

    }
}
