using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP;
using BP.Bendahara;
using DTO;
using DTO.Bendahara;

using Formatting;

namespace KUAPPAS.Bendahara
{
    public partial class frmTrxBank : Form
    {

        private int m_IdDinas;
        private int m_iKodeUK;
        private long m_ID;
        private bool m_bNew;

       
        public frmTrxBank()
        {
            InitializeComponent();
            m_ID = 0;
        }

        private void frmTrxBank_Load(object sender, EventArgs e)
        {

        }
        
        public bool Settrx(TrxBank ttx)
        {
            ctrlDinas1.Create();
            ctrlDinas1.SetID(ttx.IDDinas,ttx.KodeUK);
            m_IdDinas = ttx.IDDinas;
            m_iKodeUK = ttx.KodeUK;
            ctrlTanggal1.Tanggal = ttx.DTrx;
            ctrlJenisTrxBank1.Create();
            m_bNew = false;
            ctrlJenisTrxBank1.SetID((int)ttx.jenis);
            txtJumlah.Text = ttx.Jumlah.ToRupiahInReport();
            txtKeterangan.Text = ttx.Uraian;
            txtNoBukti.Text = ttx.NoBukti;
            m_ID = ttx.ID;

            return true;
        }
        private bool CekInput()
        {
            if (GlobalVar.Pengguna.Status == 2)
            {
                MessageBox.Show("Pengguna Tidak bisa melakukan Transaksi");
                return false;

            }
            if (ctrlTanggal1.Tanggal.Year != GlobalVar.TahunAnggaran)
            {
                MessageBox.Show("Tanggal tidak sesuai Tahun Anggaran..");
                return false;
            }
            if (txtNoBukti.Text == "")
            {
                MessageBox.Show("Belum mengisi No Bukti Pencairan. ");
                return false;

            }
            if (ctrlDinas1.PilihanValid == false)
            {
                return false;
            }
           
            if (txtKeterangan.Text == "")
            {
                MessageBox.Show("Belum mengisi Keterangan Pencairan. ");
                return false;

            }
            if (txtJumlah.Text.Trim().FormatUangReportKeDecimal() == 0)
            {
                MessageBox.Show("Belum mengisi Jumlah Transaski. ");
                return false;
            }

            if ( txtNoBukti.Text.Trim().Length== 0)
            {
                MessageBox.Show("Belum mengisi Nomor bukti ");
                return false;
            }
             



            return true;



        }
        private EventResponseMessage ctrlNavigation1_OnSave()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {

                if (CekInput() == false)
                {
                    ret.ResponseStatus = false;
                    return ret;
                }
                TrxBank trx = new TrxBank();
                trx.ID = m_ID;
                m_IdDinas = ctrlDinas1.GetID();
                trx.IDDinas = m_IdDinas;

                trx.KodeUK = ctrlDinas1.KodeUK();
                trx.Tahun = GlobalVar.TahunAnggaran;


                trx.jenis = ctrlJenisTrxBank1.GetID();
                trx.Jumlah =DataFormat.FormatUangReportKeDecimal(txtJumlah.Text);
                trx.Uraian=txtKeterangan.Text;
                trx.DTrx = ctrlTanggal1.Tanggal;
                trx.NoBukti=txtNoBukti.Text;
                trx.PPKD = 0;
                TrxBankLogic oLogic = new TrxBankLogic(GlobalVar.TahunAnggaran);
                m_ID= oLogic.Simpan(trx);
                if (m_ID > 0)
                {
                    oLogic.CatatBKU(ref trx);
                }
                m_bNew = false;
                return ret;

            }
            catch (Exception ex)
            {

                ret.ResponseStatus = false;
                ret.Message = ex.Message;
                return ret;
            }
            
        }
        public void SetNew()
        {
            ctrlNavigation1.SetNew();
        }

        private void ctrlNavigation1_Load(object sender, EventArgs e)
        {

        }

        private EventResponseMessage ctrlNavigation1_OnAdd()
        {
            EventResponseMessage ret = new EventResponseMessage();
            ret.ResponseStatus = true;

            ctrlDinas1.Create();
            ctrlJenisTrxBank1.Create();
            ctrlJenisTrxBank1.SetID(3);//hanya pencairan 

             txtJumlah.Text="0";
             txtKeterangan.Text="";
            ctrlTanggal1.Tanggal= DateTime.Now.Date;
            txtNoBukti.Text="";
            return ret;
            
        }

        private EventResponseMessage ctrlNavigation1_OnDelete()
        {
            EventResponseMessage ret = new EventResponseMessage();
            try
            {
                ret.ResponseStatus = true;
                if (MessageBox.Show("Apakah benar akan menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TrxBank trx = new TrxBank();
                    trx.ID = m_ID;

                    TrxBankLogic oLogic = new TrxBankLogic(GlobalVar.TahunAnggaran);
                    if (oLogic.Haous(m_ID) == true)
                    {

                        MessageBox.Show("Data sudah dihapus");
                    }
                    else
                    {
                        MessageBox.Show(oLogic.LastError());
                        ret.ResponseStatus = false ;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {

                ret.ResponseStatus = false;
                ret.Message = ex.Message;
                return ret;
            }
        }
    }
}
