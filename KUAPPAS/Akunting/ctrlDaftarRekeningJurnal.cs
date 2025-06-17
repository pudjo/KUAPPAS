using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP.Akuntansi;
using DTO.Akuntansi;
using Formatting;

namespace KUAPPAS.Akunting
{
    public partial class ctrlDaftarRekeningJurnal : UserControl
    {
        public delegate void ValueChangedEventHandler(JurnalRekeningShow jr);
        public event ValueChangedEventHandler OnClicked;
        private string m_sJudul;
        private long NoUrutSumber;
        private bool valid;
        private bool baru;
        private bool enable;
        private long NoJurnal = 0;
        private List<JurnalRekeningShow> m_lstJrnalRekening;
        public ctrlDaftarRekeningJurnal()
        {
            InitializeComponent();
            m_sJudul = "";
            m_lstJrnalRekening = new List<JurnalRekeningShow>();
            enable = false;
        }
        public bool Enable
        {
            set
            {
                gridJurnaRekening.Columns[4].Visible = value;
                enable = value;
            }
        }
        public bool Baru
        {
            set
            {
                baru = value;
                m_lstJrnalRekening = new List<JurnalRekeningShow>();
                Displey();
            }
        }
        private void ctrlDaftarRekeningJurnal_Load(object sender, EventArgs e)
        {
            gridJurnaRekening.FormatHeader();
        }
        public List<JurnalRekeningShow> GetJurnalRekenings()
        {
            List<JurnalRekeningShow> jurnalRekenings = new List<JurnalRekeningShow>();
            for (int row = 0; row < gridJurnaRekening.Rows.Count; row++ )
            {
                if (gridJurnaRekening.Rows[row].Cells[0].Value != null)
                {
                    JurnalRekeningShow jr = new JurnalRekeningShow();
                    decimal x = DataFormat.GetDecimal(DataFormat.GetString(gridJurnaRekening.Rows[row].Cells[2].Value));
                    jr.Debet = x != 0 ? 1 : -1;
                    if (jr.Debet==1)
                    {
                        jr.Jumlah = x;
                    }
                    else
                    {
                        jr.Jumlah = DataFormat.GetDecimal(DataFormat.GetString(gridJurnaRekening.Rows[row].Cells[3].Value)); ;
                    }

                    
                    jr.IIDRekening = DataFormat.GetLong(DataFormat.GetString(gridJurnaRekening.Rows[row].Cells[0].Value).Replace(".", ""));
                    jurnalRekenings.Add(jr);
                }
            }
            return jurnalRekenings;
        }

        private void gridJurnaRekening_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (enable = true)
                    {
                        if (MessageBox.Show("Yakin akan menghapus?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            m_lstJrnalRekening.RemoveAt(e.RowIndex);
                            Displey();
                        }


                    }
                }
                if (e.ColumnIndex < 4)
                {

                    JurnalRekeningShow jr = new JurnalRekeningShow();
                    jr = m_lstJrnalRekening[e.RowIndex];
                    if (OnClicked != null)
                    {
                        OnClicked(jr);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Clear()
        {
            gridJurnaRekening.Rows.Clear();
            txtJumlahDebet.Text = "0";
            txtJumlahKredit.Text = "0";
            txtJumlahDebet.BackColor = Color.White;
            txtJumlahKredit.BackColor = Color.White;

        }
        public string Judul
        {
            set { m_sJudul = value; }
            get { return m_sJudul; }
        }
        public void SetJurnal(long noJurnal ,List<JurnalRekeningShow> lstJurnalRekening = null)
        {
            decimal JumlahDebet = 0;
            decimal JumlahKredit = 0;
            txtJumlahKredit.Text = "0";
            txtJumlahDebet.Text = "0";
            gridJurnaRekening.Rows.Clear();
            NoJurnal = noJurnal;
            if (lstJurnalRekening != null)
            {
                foreach (JurnalRekeningShow jr in lstJurnalRekening)
                {
                    m_lstJrnalRekening.Add(jr);
                    if (jr.Jumlah != 0)
                    {
                        if (jr.Debet == 0)
                        {
                            MessageBox.Show("Debet = 0 untuk Rekening " + jr.NamaRekening);

                        }
                        if (jr.Debet == -1)
                        {
                            JumlahKredit = JumlahKredit + jr.Jumlah;

                        }

                        if (jr.Debet == 1)
                        {
                            JumlahDebet = JumlahDebet + jr.Jumlah;

                        }
                        string[] row = { jr.IIDRekening ==0? "0":jr.IIDRekening.ToKodeRekening(),
                                  jr.NamaRekening,
                                  jr.Debet== 1?jr.Jumlah.ToRupiahInReport():"0.00",
                                  jr.Debet== -1?jr.Jumlah.ToRupiahInReport():"0.00"};


                        gridJurnaRekening.Rows.Add(row);
                        if (jr.IIDRekening.ToString().Substring(0, 1) == "4" || jr.IIDRekening.ToString().Substring(0, 1) == "5" ||
                            jr.IIDRekening.ToString().Substring(0, 1) == "6")
                        {
                            m_sJudul = "Jurnal Anggaran.";
                        }
                        else
                        {
                            if (jr.IIDRekening.ToString().Substring(0, 1) == "7" || jr.IIDRekening.ToString().Substring(0, 1) == "8" || jr.IIDRekening == 110102010001 || jr.IIDRekening == 110103010001)
                            {
                                m_sJudul = "Jurnal Financial.";
                            }
                            else
                            {
                                if (jr.IIDRekening == 111301010001 || jr.IIDRekening == 111301010001 || jr.PPKD==1)
                                {
                                    m_sJudul = "Jurnal PPKD";
                                }
                            }
                        }

                    }
                }

                txtJumlahKredit.Text = JumlahKredit.ToRupiahInReport();
                txtJumlahDebet.Text = JumlahDebet.ToRupiahInReport();
                if (JumlahDebet != JumlahKredit)
                {
                    txtJumlahDebet.BackColor = Color.Red;
                    txtJumlahKredit.BackColor = Color.Red;
                    valid = false;

                }
                else
                {
                    txtJumlahDebet.BackColor = Color.SkyBlue;
                    txtJumlahKredit.BackColor = Color.SkyBlue;
                    valid = true;
                }
            }
        }
        public void TambahkanKeList(JurnalRekeningShow jr)
        {
            for (int i = 0; i < m_lstJrnalRekening.Count; i++)
            {
                if (m_lstJrnalRekening[i].IIDRekening == jr.IIDRekening)
                {
                    m_lstJrnalRekening[i] = jr;
                    return;
                }
            }
            m_lstJrnalRekening.Add(jr);
        }
        public void Displey()
        {

            gridJurnaRekening.Rows.Clear();

            foreach (JurnalRekeningShow jr in m_lstJrnalRekening)
            {
                if (jr != null)
                {
                    string[] row = { jr.IIDRekening ==0? "0":jr.IIDRekening.ToKodeRekening(),
                                  jr.NamaRekening,
                                  jr.Debet== 1?jr.Jumlah.ToRupiahInReport():"0.00",
                                  jr.Debet== -1?jr.Jumlah.ToRupiahInReport():"0.00","Hapus"};


                    gridJurnaRekening.Rows.Add(row);

                }
            }

            decimal d = 0;
            decimal k = 0;
            for (int i = 0; i < gridJurnaRekening.Rows.Count; i++)
            {
                d = d + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridJurnaRekening.Rows[i].Cells[2].Value));
                k = k + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridJurnaRekening.Rows[i].Cells[3].Value));

            }
            txtJumlahDebet.Text = d.ToRupiahInReport();
            txtJumlahKredit.Text = k.ToRupiahInReport();
            if (d == k)
            {
                txtJumlahDebet.BackColor = Color.SkyBlue;
                txtJumlahKredit.BackColor = Color.SkyBlue;
                valid = true;
            }
            else
            {
                txtJumlahDebet.BackColor = Color.Red;
                txtJumlahKredit.BackColor = Color.Red;
                valid = false;
            }
        }
        public bool Valid
        {
            get
            {
                return valid;
            }
        }
    }
}
