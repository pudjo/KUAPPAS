using System;
using System.Collections.Generic;

using System.Windows.Forms;
using BP.Akuntansi;
using DTO.Akuntansi;
using Formatting;
using KUAPPAS.Bendahara;
using DTO;

namespace KUAPPAS.Akunting
{
    public partial class ctrlBukuBesar : UserControl
    {
        private List<BukuBesar> m_lstBukuBesar;
        private bool bexcell;

        DateTime mTanggalAkhir;
        DateTime mTanggalAwal;
        
        private List<Rekening> m_lstRekening;
        private List<Realisasi04AK> m_lstNeracaAwal;

        private int m_iSKPD;
        private long mIDRekening;
        private string FolderPath;
        private decimal m_cSaldoAwal = 0l;
        public ctrlBukuBesar()
        {
            InitializeComponent();
            Excell = false;
            FolderPath = "D:\\";
        }

        public long IDRekening
        {
            set
            {
                mIDRekening = value;
                txtIDrekening.Text = mIDRekening.ToString();

            }

        }
        public int OPD
        {
            set
            {
                m_iSKPD = value;
                ctrlSKPD1.SetID(value);
            }

        }


        public DateTime TanggalAwal
        {
            set
            {
                mTanggalAwal = value;
            }

        }
        public DateTime TanggalAkhir
        {
            set
            {
                mTanggalAkhir = value;
            }

        }
        public string NamaRekening
        {
            set
            {
                lblNamaRekening.Text = value;
            }
        }
        private void ctrlBukuBesar_Load(object sender, EventArgs e)
        {
            ctrlSKPD1.Create();
            ctrlPencarian1.setGrid(ref gridBukuBesar);
            gridBukuBesar.FormatHeader();


        }
        public bool Excell
        {
            set
            {
                bexcell = value;
            }

        }

        public bool LoadData()
        {
            try
            {
                GetSaldoAwal();
                m_lstBukuBesar = new List<BukuBesar>();
                int iLevelRekening = 6;

                List<BukuBesar> lstInRpt = new List<BukuBesar>();
                BukuBesarLogic oLogic = new BukuBesarLogic(GlobalVar.TahunAnggaran);



                m_lstBukuBesar = oLogic.GetSKPDBukuBesar(m_iSKPD, mTanggalAkhir, mIDRekening.ToString());

                if (m_lstBukuBesar == null)
                {
                    MessageBox.Show(oLogic.LastError());
                    return false;
                }


                gridBukuBesar.Rows.Clear();
                decimal saldo = m_cSaldoAwal;
                foreach (BukuBesar v in m_lstBukuBesar)
                {

                    decimal debet;
                    decimal kredit;
                    debet = v.Debet == 1 ? v.Jumlah : 0;
                    kredit = v.Debet == -1 ? v.Jumlah : 0;
                    saldo = saldo + (v.SaldoNormal * debet) - (v.SaldoNormal * kredit);
                    string[] row = { v.TanggalTransaksi.ToTanggalIndonesia(),
                        v.NamaSKPD,
                        v.NoBukti,
                                         v.Keterangan, debet.ToRupiahInReport(), kredit.ToRupiahInReport() ,
                                         saldo.ToRupiahInReport(),
                                         v.NoSumber.ToString()
                                     };
                    gridBukuBesar.Rows.Add(row);
                }


                // MessageBox.Show("Pemanggilan Selesai..");
                if (bexcell == true)
                {
                    if (Excellkan() == false)
                    {
                        // try sekalilagi

                        string NamaFile = DapatkanNamaFileYangBetul();
                        if (Excellkan(NamaFile)== false)
                        {
                            return false ;
                        }
                        return true ;

                    }
                    return true;
                }

                return false ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }

        }

        private void gridBukuBesar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GetSaldoAwal()
        {
            decimal saldoAwal = 0;
            int iLevelRekening = 6;

            SaldoAwalRehLogic oLogic = new SaldoAwalRehLogic(GlobalVar.TahunAnggaran);

            int  iSKPD = ctrlSKPD1.GetID();
            //mTanggalAkhir = ctrlTanggalBulanVertikal1.TanggalAkhir;
            if (txtIDrekening.Text.Trim().Replace(".", "").Length == 0)
            {
                MessageBox.Show("Kode Rekenig Belum di defnisikan..");
                txtIDrekening.Focus();
                return;
            }
            int tahun = GlobalVar.TahunAnggaran - 1;
            
            mIDRekening = DataFormat.GetLong(txtIDrekening.Text.Trim().Replace(".", ""));
            saldoAwal = oLogic.GetSaldoAwal(m_iSKPD, tahun, mIDRekening);
            txtSaldoAwal.Text = saldoAwal.ToRupiahInReport();
            m_cSaldoAwal = saldoAwal;
        }

        private bool  Excellkan(string namaFile="")
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // Make Excel invisible and disable alerts.
                excel.Visible = false;
                // excel.DisplayAlerts = false;

                // Create a new Workbook.
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Create a Worksheet.
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;


                // header
                excelSheet.Cells[1, 2] = "Buku besar";
                excelSheet.Cells[2, 1] = "Rekening";
                excelSheet.Cells[2, 2] = txtIDrekening.Text.ToKodeRekening(6);
                excelSheet.Cells[2, 3] = lblNamaRekening.Text;

                excelSheet.Cells[3, 1] = "Tanggal ";
                excelSheet.Cells[3, 2] = mTanggalAkhir.ToTanggalIndonesiaLengkap();

                excelSheet.Cells[3, 1] = "Saldo Awal ";
                excelSheet.Cells[3, 2] = m_cSaldoAwal.ToString();


                int jumlahkolom = gridBukuBesar.ColumnCount;

                for (int i = 1; i < jumlahkolom; i++)
                {
                    excelSheet.Cells[4, i] = gridBukuBesar.Columns[i - 1].HeaderText;
                }
                //gridLRA.Columns.Count + 1
                for (int row = 0; row < gridBukuBesar.Rows.Count; row++)
                {
                    for (int col = 0; col < jumlahkolom - 1; col++)
                    {
                        if (col >= 4)
                        {
                            string s = DataFormat.GetString(gridBukuBesar.Rows[row].Cells[col].Value);
                            excelSheet.Cells[row + 5, col + 1] = s.FormatUangReportKeDecimal().ToString().ReplaceUnicode();
                        }
                        else
                        {
                            excelSheet.Cells[row + 5, col + 1] = DataFormat.GetString(gridBukuBesar.Rows[row].Cells[col].Value).ReplaceUnicode();
                        }




                    }
                }


                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[excelSheet.Rows.Count, excelSheet.Columns.Count]];
                if (namaFile.Length == 0)
                {
                    if (FolderPath.Substring(FolderPath.Length - 1, 1) != "\\")
                    {
                        FolderPath = FolderPath + "\\";
                    }


                    namaFile = FolderPath + txtIDrekening.Text.ToKodeRekening(6) + "-" + lblNamaRekening.Text.Trim().ReplaceUnicode() + ".xlsx";
                }
                //if (namaFile.Trim().Length == 0)
                //{
                //    MessageBox.Show("Nama File Masih Kosong ");
                //    return;
                //}

                excelworkBook.SaveAs(namaFile);
                
                MessageBox.Show("File sudah disimpan di " + namaFile);
                

                excelworkBook.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke excell" + ex.Message);
                return false;
            }
        }
        private string DapatkanNamaFileYangBetul() {
            if (FolderPath.Substring(FolderPath.Length - 1, 1) != "\\")
            {
                FolderPath = FolderPath + "\\";
            }

            string NamaFile = FolderPath  + txtIDrekening.Text.ToKodeRekening(6) + ".xlsx";
            return NamaFile;
        }
        private void cmdExcell_Click(object sender, EventArgs e)
        {

            string NamaFile = DapatkanNamaFileYangBetul();


            Excellkan(NamaFile);

            
        }
        public string Direktori
        {
            set
            {
                FolderPath = value;
            }
        }
        //private string BuatFile()
        //{

        //    string sRet = "";
        //    SaveFileDialog fdlg = new SaveFileDialog();
        //    fdlg.Filter = "Excel|*.xlsx;*.xls";
        //    fdlg.Title = "Save an Image File";
        //    fdlg.ShowDialog();

        //    fdlg.Title = "Buat File file";
        //    fdlg.InitialDirectory = @"c:\";

        //    fdlg.FileName = txtIDrekening.Text + "_"+ lblNamaRekening.Text  ;
        //    fdlg.Filter = "Excel|*.xlsx;*.xls";
        //    fdlg.RestoreDirectory = true;

        //    fdlg.FileName = txtIDrekening.Text + "_" + lblNamaRekening.Text;



        //    //if (fdlg.FileName != "")
        //    //{
        //    // Saves the Image via a FileStream created by the OpenFile method.  

        //    sRet = fdlg.FileName;


        //    //  }
        //    return sRet;
        //}

        private void ctrlSKPD1_Load(object sender, EventArgs e)
        {

        }

        private void txtSaldoAwal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
