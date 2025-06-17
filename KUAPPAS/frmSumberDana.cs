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
using DataAccess;
using Formatting;

namespace KUAPPAS
{
    public partial class frmSumberDana : Form
    {
        private int m_IDDinas;
        private int m_IDUrusan;
        private int m_IDProgram;
        private int m_IDKegiatan;
        private int m_IDJenis;
        List<RekapPerJenis> _lstRekap2;
        private bool _gridSudahDiFormat = false;
        private int mProfile;


        public frmSumberDana()
        {
            InitializeComponent();
            m_IDDinas = 0;
            m_IDUrusan = 0;
            m_IDProgram = 0;
            m_IDKegiatan = 0;
            m_IDJenis = 0;
            mProfile = 3;
        }
        public int Profile
        {
            set { mProfile = value; }

        }
        private void frmSumberDana_Load(object sender, EventArgs e)
        {
            ctrlHeader1.SetCaption("Setting APBD dan Sumber Dana.");
            ctrlDinas1.Create();
            this.WindowState = FormWindowState.Maximized;

            ctrlJenisAnggaran1.Create(4);
            
        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {
            m_IDDinas = pIDSKPD;
            treeProgramKegiatan1.Create(m_IDDinas,1);
       //     LoadData();
            //if (_gridSudahDiFormat == false)
            //{
            //    AturColumnSumberDana();
            //    _gridSudahDiFormat = true;
            //}
        }
        private void LoadRekap()
        {
            LoadData();
        }
        private void LoadData()
        {

            _lstRekap2 = new List<RekapPerJenis>();

            RekapLogic oLogic = new RekapLogic(GlobalVar.TahunAnggaran);
            //    _lstRekap2.Clear();
            //_lstRekap2 = oLogic.GetRekapPerJenisDinas((int)GlobalVar.TahunAnggaran, m_IDDinas);

            _lstRekap2 = oLogic.GetRekapPerJenisDinasDispenda((int)GlobalVar.TahunAnggaran, m_IDDinas);
            //gridRekapProgram.Rows.Clear();
            gridRekap.Rows.Clear();
            if (_lstRekap2 != null)
            {
                    foreach (RekapPerJenis r in _lstRekap2)
                    {
                        if (r.Level == 1)
                        {
                            TreeGridNode node =gridRekap.Nodes.Add(r.Kode + "  - " + r.Nama, r.Pagu);
                            LoadUrusan2(node, r.IDDInas);
                        }
                    }
            }
        }
        private void LoadDinas(TreeGridNode nodeParent)
        {
            //    Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapPerJenis r in _lstRekap2)
                {
                    if (r.Level == 1)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + "  - " + r.Nama, r.Pagu);
                        LoadUrusan2(node, r.IDDInas);
                    }
                }
            }
        }
        private void LoadUrusan2(TreeGridNode nodeParent, int idDInas)
        {
            //    Font boldFont = new Font(treeGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapPerJenis r in _lstRekap2)
                {

                    if (r.Level == 2 && r.IDDInas == idDInas)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + " - " + r.Nama, r.Pagu);
                        LoadProgram2(node, r.IDDInas, r.IDUrusan, r.Jenis);
                    }
                }
            }
        }
        private void LoadProgram2(TreeGridNode nodeParent, int idDInas, int IDUrusan, Single _jenis)
        {
            Font boldFont = new Font(gridRekap.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapPerJenis r in _lstRekap2)
                {

                    if (r.Level == 3 && r.IDDInas == idDInas && r.IDUrusan == IDUrusan )
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + " - " + r.Nama,  r.Pagu, r.Jenis);

                        //node.DefaultCellStyle.Font = boldFont;
                        LoadKegiatan2(node, r.IDDInas, r.IDUrusan, r.IDProgram, r.Jenis);
                    }

                }
            }

        }
        private void LoadKegiatan2(TreeGridNode nodeParent, int idDInas, int IDUrusan, int IDProgram, Single _jenis)
        {
            // 'Font boldFont = new Font(gridRekap.DefaultCellStyle.Font, FontStyle.Bold);
            if (_lstRekap2 != null)
            {
                foreach (RekapPerJenis r in _lstRekap2)
                {

                    if (r.Level == 4 && r.IDDInas == idDInas && r.IDUrusan == IDUrusan && r.IDProgram == IDProgram && r.Jenis == _jenis)
                    {
                        TreeGridNode node = nodeParent.Nodes.Add(r.Kode + " - " + r.Nama,  r.Pagu);

                        //LoadRekening2(node, r.IDDInas, r.IDUrusan, r.IDProgram, r.IDkegiatan, r.Jenis);
                    }

                }
            }

        }

        private bool LoadAnggaran()
        {
            TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();
            int _iJenis = ctrlJenisAnggaran1.GetID();
            m_IDJenis = _iJenis;
            if (m_IDUrusan == 0 && _iJenis == 3)
                return false;

            m_IDUrusan = m_IDUrusan == 0 ? DataFormat.GetInteger(ctrlDinas1.GetID().ToString().Substring(0, 3)) : m_IDUrusan;
            int _bPPKD = (int)ctrlDinas1.PPKD();
            lstRekening = oLogic.Get(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan, _iJenis, _bPPKD, GlobalVar.TahapAnggaran,1,0);
            
            gridSumberDana.Rows.Clear();
            foreach (TAnggaranRekening ta in lstRekening)
            {
                //              '0                       ,1   ,2       ,3   ,4   ,5   ,6   ,7  ,8  ,9                                          ,10  ,11      ,12  ,13  ,14 ,15                         ,16  ,17 ,18  ,19  ,20  ,21 ,22
                string[] rowUtkSumberDana = { ta.IDRekening.ToString(), ta.IDRekening.ToString().ToKodeRekening(), ta.Nama, ta.Plafon.ToRupiahInReport() };                
                gridSumberDana.Rows.Add(rowUtkSumberDana);
            }

        

            HitungJumlahRKA();
          //  LoadSumberDana();
            return true;
            

        }

        //private void LoadSumberDana()
        //{
        //    TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran);
        //    List<TSumberDana> mListUnit = new List<TSumberDana>();
        //    mListUnit = oLogic.Get((int)GlobalVar.TahunAnggaran, m_IDDinas, m_IDUrusan, m_IDProgram, m_IDKegiatan);
        //    if (mListUnit != null)
        //    {
        //        foreach (TSumberDana ts in mListUnit)
        //        {
        //            for (int i = 0; i < gridSumberDana.Rows.Count; i++)
        //            {
        //                if (ts.IDRekening == DataFormat.GetLong(gridSumberDana.Rows[i].Cells[0].Value))
        //                {

        //                    gridSumberDana.Rows[i].Cells[ts.IDSUmberDana.ToString()].Value = ts.Jumlah.ToRupiahInReport() ;
        //                }

        //            }
        //        }
        //    }
        //    for (int i = 0; i < gridSumberDana.Rows.Count; i++)
        //    {
        //        CekSumberDana(i);
        //    }
        //}
        private void HitungJumlahRKA()
        {
            decimal cJumlah = 0L;
           // decimal cJumlahPlafon = 0L;

            for (int i = 0; i < gridSumberDana.Rows.Count; i++)
            {

                cJumlah += DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));              
                
            }

            lblJumlah.Text = cJumlah.ToRupiahInReport();
            

        }

        private EventResponseMessage treeProgramKegiatan1_KegiatanChanged(int ID)
        {
            EventResponseMessage lRet = new EventResponseMessage();
            if (ctrlDinas1.GetID() == 0)
            {
                MessageBox.Show("Pilihan Dinasnya terlebih dahulu.");
                lRet.ResponseStatus = false;
                return lRet;
            }
            if (ctrlJenisAnggaran1.GetID() != 3)
            {
                MessageBox.Show("Pilihan jenis Aanggaran belum tepat");
                lRet.ResponseStatus = false;
                return lRet;
            }
            m_IDKegiatan = ID;
            if (m_IDKegiatan == 0)
                return lRet;
            
            m_IDUrusan = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 3));
            m_IDProgram = DataFormat.GetInteger(m_IDKegiatan.ToString().Substring(0, 5));
            Urusan oUrusan = new Urusan();
            UrusanLogic oUrusanLogic = new UrusanLogic(GlobalVar.TahunAnggaran);
            oUrusan = oUrusanLogic.GetByID(m_IDUrusan);
            if (oUrusan == null)
            {
                MessageBox.Show(oUrusanLogic.LastError());
                lRet.ResponseStatus = false;
                return lRet;

            }
            lblUrusan.Text = oUrusan.Tampilan + " " + oUrusan.Nama;

            TProgramLogic oPLogic = new TProgramLogic(GlobalVar.TahunAnggaran, mProfile);
            TPrograms oProgram = new TPrograms();
            if (oPLogic.CekProgramDinas(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram))
            {
                oProgram = oPLogic.GetByDinasAndUrusanProgram(GlobalVar.TahunAnggaran, ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram);
                if (oProgram == null)
                {
                    MessageBox.Show(oPLogic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }
                lblProgram.Text = "";
                if (oProgram != null)
                    lblProgram.Text = oProgram.KodeProgram.ToString() + " " + oProgram.Nama;
                TKegiatanLogic oKLogic = new TKegiatanLogic(GlobalVar.TahunAnggaran,3);
                TKegiatan oKegiatan = new TKegiatan();
                oKegiatan = oKLogic.GetByID(ctrlDinas1.GetID(), m_IDUrusan, m_IDProgram, m_IDKegiatan);
                if (oKegiatan == null)
                {
                    MessageBox.Show(oKLogic.LastError());
                    lRet.ResponseStatus = false;
                    return lRet;

                }
                lblKegiatan.Text = oKegiatan.KodeKegiatan.ToString() + " " + oKegiatan.Nama;
                //lblPagu.Text = oKegiatan.Pagu.FormatUang();
                TKegiatanAPBDLogic oKegiatanAPBDLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
                oKegiatanAPBDLogic.CekNAmaKegiatan(oKegiatan);

                //m_dPagu = oKegiatan.Pagu;
                LoadAnggaran();
                //HitungJumlahRKA();
            }
            return lRet;

        }
        private bool AturColumnSumberDana()
        {
            SumberDanaLogic oLogic = new SumberDanaLogic(GlobalVar.TahunAnggaran);
            List<SumberDana> _lst = new List<SumberDana>();
            try
            {
                _lst = oLogic.Get();
                if (_lst != null)
                {
                    foreach (SumberDana sd in _lst)
                    {
                        DataGridViewCellStyle celType = new DataGridViewCellStyle();
                        celType.Alignment = DataGridViewContentAlignment.MiddleRight;

                        DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
                        newCol.Name = sd.ID.ToString();
                        newCol.HeaderText = sd.Nama;
                        newCol.DefaultCellStyle = celType;
                        //DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
                        //newCol.CellType = colType;

                        //newCol.CellType=DataGridViewCell

                        newCol.ValueType = typeof(decimal);

                        gridSumberDana.Columns.Add(newCol);
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

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage treeProgramKegiatan1_KegiatanChanged_1(int ID)
        {
            m_IDKegiatan = ID;
            LoadAnggaran();
            EventResponseMessage lRet = new EventResponseMessage();
            lRet.ResponseStatus = true;

            return lRet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmImportAPBD fImport = new frmImportAPBD();
            fImport.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran);
            oLogic.BersihkanDouble((int)GlobalVar.TahunAnggaran);
        }

        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            
            TAnggaranRekeningLogic oLogicRek = new TAnggaranRekeningLogic(GlobalVar.TahunAnggaran);
            List<TAnggaranRekening> _lstRek = new List<TAnggaranRekening>();


            TSumberDanaLogic oLogic = new TSumberDanaLogic(GlobalVar.TahunAnggaran,3);

            List<TSumberDana> _lsd = new List<TSumberDana>();
            for (int i = 0; i < gridSumberDana.Rows.Count; i++)
            {
                if (m_IDJenis == 3)
                {
                    for (int c = 4; c < gridSumberDana.ColumnCount; c++)
                    {
                        if (DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[c].Value)) > 0)
                        {
                            TSumberDana oTSD = new TSumberDana();
                            oTSD.Tahun = (int)GlobalVar.TahunAnggaran;
                            oTSD.IDDinas = ctrlDinas1.GetID();
                            oTSD.IDKegiatan = m_IDKegiatan;
                            oTSD.IDProgram = m_IDProgram;
                            oTSD.IDUrusan = m_IDUrusan;
                            oTSD.IDRekening = DataFormat.GetLong(gridSumberDana.Rows[i].Cells[0].Value);
                            oTSD.IDSUmberDana = DataFormat.GetInteger(gridSumberDana.Columns[c].Name);
                            oTSD.Jumlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString( gridSumberDana.Rows[i].Cells[c].Value));
                            _lsd.Add(oTSD);

                        }
                    }
                }
                
                TAnggaranRekening o = new TAnggaranRekening();
                o.Tahun = GlobalVar.TahunAnggaran;
                o.Jenis = m_IDJenis;
                o.IDDinas = m_IDDinas;
                o.IDUrusan = m_IDUrusan;
                o.IDProgram = m_IDProgram;
                o.IDKegiatan = m_IDKegiatan;
                o.IDRekening = DataFormat.GetLong(gridSumberDana.Rows[i].Cells[0].Value);
                o.PPKD = ctrlDinas1.PPKD();
                o.StatusUpdate = 1;
                o.Plafon = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[i].Cells[3].Value));
                if (o.IDRekening> 0)
                    _lstRek.Add(o);

            }
            if (m_IDJenis == 3)
                oLogic.Simpan(_lsd, (int)GlobalVar.TahunAnggaran, m_IDUrusan, m_IDDinas, m_IDProgram, m_IDKegiatan);

            oLogicRek.SimpanPlafon(_lstRek, (int)GlobalVar.TahunAnggaran, m_IDDinas,0, m_IDUrusan, m_IDProgram, m_IDKegiatan, m_IDJenis, GlobalVar.TahapAnggaran);
            MessageBox.Show("Penyimpanan Selesai");
            LoadData();



        }

        private void ctrlJenisAnggaran1_OnChanged(int pID)
        {
            m_IDJenis = pID;
            LoadAnggaran();

        }

        private void gridSumberDana_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                gridSumberDana.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.ToRupiahInReport(DataFormat.GetDecimal(gridSumberDana.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                CekSumberDana(e.RowIndex);
                HitungJumlahRKA();
            }
        }

        private void gridSumberDana_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                gridSumberDana.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                
            }

        }

        private void gridSumberDana_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                gridSumberDana.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.ToRupiahInReport(DataFormat.GetDecimal(gridSumberDana.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                CekSumberDana(e.RowIndex);
            }
            
        }
        private void CekSumberDana(int baris)
        {
            DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
            decimal cJumlahAnggaran = 0L;
            decimal cJumlahSumberDana = 0L;
            cJumlahAnggaran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[baris].Cells[3].Value)); 

            for (int c = 4; c < gridSumberDana.Columns.Count; c++)
            {
                cJumlahSumberDana =cJumlahSumberDana +  DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[baris].Cells[c].Value)); 

            }
            if (cJumlahAnggaran != cJumlahSumberDana)
            {
                _hilightstyle.BackColor = Color.Aqua;// new Font(gridKUA.Font, FontStyle.Bold);
                _hilightstyle.Font = new Font(gridSumberDana.Font, FontStyle.Bold);
            } else {
                _hilightstyle.BackColor = Color.White;// new Font(gridKUA.Font, FontStyle.Bold);
                _hilightstyle.Font = new Font(gridSumberDana.Font, FontStyle.Regular);
                
               //' MessageBox.Show("Jumlah Anggaran berbeda dengan jumlah per sumber dana");
            }
            gridSumberDana.Rows[baris].DefaultCellStyle = _hilightstyle;


        }

        private void cmdSumbeDanaAPBD_Click(object sender, EventArgs e)
        {
            for (int baris = 0; baris < gridSumberDana.Rows.Count; baris++)
            {
                decimal cJumlahAnggaran = 0L;
                decimal cJumlahSumberDana = 0L;
                cJumlahAnggaran = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[baris].Cells[3].Value));

                for (int c = 5; c < gridSumberDana.Columns.Count; c++)
                {
                    cJumlahSumberDana = cJumlahSumberDana + DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridSumberDana.Rows[baris].Cells[c].Value));

                }
                if (cJumlahAnggaran != cJumlahSumberDana)
                {
                    gridSumberDana.Rows[baris].Cells[4].Value = (cJumlahAnggaran - cJumlahSumberDana).ToRupiahInReport();

                    
                }
            }

        }

        private void gridSumberDana_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ctrlJenisAnggaran1_Load(object sender, EventArgs e)
        {

        }

    }
}
