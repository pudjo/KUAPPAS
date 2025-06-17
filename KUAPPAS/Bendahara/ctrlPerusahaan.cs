using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BP;
using Formatting;
using DTO.SP2DOnLine;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace KUAPPAS.Bendahara
{
    public partial class ctrlPerusahaan : UserControl
    {

        public delegate void ValueChangedEventHandler(Perusahaan perusahaan);
        public event ValueChangedEventHandler OnFinishPencarian;
        public event ValueChangedEventHandler OnSavingPerusahaan;
        
        
        
        private int m_ID;
        public ctrlPerusahaan()
        {
            InitializeComponent();
            m_ID = 0;
        }

        private void ctrlPerusahaan_Load(object sender, EventArgs e)
        {

        }
        private void SetDisplayMode(bool bDisplay)
        {
            
            txtNoRekening.Enabled = bDisplay;

            txtNPWP.Enabled = bDisplay;
            txtNamaPimpinan.Enabled = bDisplay;

            txtNamaPerusahaan.Enabled = bDisplay;

            ctrlDaftarBank1.Enabled = bDisplay;
            cmbBentuk.Enabled = bDisplay;

            txtNoRekening.Enabled = bDisplay;

            txtNPWP.Enabled = bDisplay;
            txtNamaPimpinan.Enabled = bDisplay;

            txtAlamat.Enabled = bDisplay;

            txtNamaPerusahaan.Enabled = bDisplay;
            txtNamaDalamRekening.Enabled = bDisplay;


        }
        public void Clear()
        {
            m_ID = 0;
           txtAlamat.Text  = "";
           cmbBentuk.Items.Clear();
           cmbBentuk.Items.Add("CV");
           cmbBentuk.Items.Add("PT");
           cmbBentuk.Items.Add("Lainnya");
           ctrlDaftarBank1.Create();
            txtNamaPerusahaan.Text = "";
            txtNamaPerusahaan.Text = "";
            txtNoRekening.Text = "";
            txtNoRekening.Text = "";
            txtNPWP.Text = "";
            txtNamaPimpinan.Text = "";
            txtNamaDalamRekening.Text = "";


        }
        public void SetID(int pID)
        {

            if (pID > 0)
            {
                PerusahaanLogic oLogic = new PerusahaanLogic((int)GlobalVar.TahunAnggaran);
                Perusahaan p = new Perusahaan();
                p.IDPerusahaan = pID;
                m_ID = pID;
                cmbBentuk.Items.Clear();
                cmbBentuk.Items.Add("CV");
                cmbBentuk.Items.Add("PT");
                cmbBentuk.Items.Add("Lainnya");


                p = oLogic.GetByID(pID);

                SetPerusahaan(p);
            }
        }
        private void SetPerusahaan (Perusahaan p){
        
        if (p != null)
            {
                m_ID = p.IDPerusahaan;
                cmbBentuk.Items.Clear();
                cmbBentuk.Items.Add("CV");
                cmbBentuk.Items.Add("PT");
                cmbBentuk.Items.Add("Lainnya");

                cmbBentuk.SelectedIndex = p.Bentuk;
                txtAlamat.Text = p.Alamat;
                txtNamaPerusahaan.Text = p.NamaPerusahaan;
                txtNamaPimpinan.Text = p.Pimpinan;
                txtNamaDalamRekening.Text = p.NamaDalamRekeningBank;

                txtNPWP.Text = p.NPWP;
                txtNoRekening.Text = p.Rekening;

                cmbBentuk.Enabled = false;
                txtAlamat.Enabled = false;
                txtNamaPerusahaan.Enabled = false;
                txtNamaPimpinan.Enabled = false;
                txtAlamat.Text = p.Alamat;
            
                txtNPWP.Enabled = false;
                txtNoRekening.Enabled = false;
                ctrlDaftarBank1.Create();
                ctrlDaftarBank1.SetKode(p.KodeBank, p.Rekening.Replace(" ",""));
                ctrlDaftarBank1.KeteranganNamaBank = p.KeteranganNamaBank;
                SetDisplayMode(false);



            }

        }
        public Perusahaan GetPerusahaan()
        {
            Perusahaan p = new Perusahaan();
            p.IDPerusahaan = m_ID;
            p.Bentuk= cmbBentuk.SelectedIndex  ;
            p.Alamat= txtAlamat.Text ;
            p.NamaPerusahaan= txtNamaPerusahaan.Text  ;
            p.Pimpinan= txtNamaPimpinan.Text  ;
            p.KodeBank = ctrlDaftarBank1.Kode;
            p.Bank = ctrlDaftarBank1.NamaBank;
            p.NPWP= txtNPWP.Text  ;
            p.Rekening= txtNoRekening.Text  ;
            p.NamaDalamRekeningBank = txtNamaDalamRekening.Text;
            
            //PerusahaanLogic oLogic = new PerusahaanLogic((int)GlobalVar.TahunAnggaran);
   
            
            //int pID= oLogic.Simpan(p);
            
            //if (oLogic.IsError()== true)
            //{
            //    MessageBox.Show(oLogic.LastError());
            //    return null;
            //}
            //else
            //{
            //    m_ID = pID;

            //}
            return p;


        }
        public Perusahaan  Simpan()
        {
            Perusahaan p = new Perusahaan();
            p.IDPerusahaan = m_ID;

            p.Bentuk = cmbBentuk.SelectedIndex;
            p.Alamat = txtAlamat.Text;
            p.NamaPerusahaan = txtNamaPerusahaan.Text;
            p.Pimpinan = txtNamaPimpinan.Text.Replace("'","''");
            p.NamaDalamRekeningBank = txtNamaDalamRekening.Text.Replace("'","''");
            p.KodeBank = ctrlDaftarBank1.Kode;
            p.NPWP = txtNPWP.Text;
            p.Rekening = txtNoRekening.Text;
            p.KeteranganNamaBank = ctrlDaftarBank1.KeteranganNamaBank; 

            PerusahaanLogic oLogic = new PerusahaanLogic((int)GlobalVar.TahunAnggaran);
           int pID = oLogic.Simpan(p);

           if (pID == 0)
           {
               MessageBox.Show(oLogic.LastError());
               return null;
           }
           else
           {
               p.IDPerusahaan = pID;
               m_ID = pID;
               MessageBox.Show("Berhasil menyimpan Perusahaan.");

               return p;
           }


        }

        private void cmdCariPihakIII_Click(object sender, EventArgs e)
        {
           
        }

        private void cmdCariPerusahaan_Click(object sender, EventArgs e)
        {
            frmPengeluaranCariPerusahaan fCariPerusahaan = new frmPengeluaranCariPerusahaan();

            fCariPerusahaan.ShowDialog();
            if (fCariPerusahaan.IsOK == true)
            {
                Perusahaan p = fCariPerusahaan.Perusahaan;
                SetPerusahaan(p);

                     if (OnFinishPencarian!=null){
                         OnFinishPencarian(p);
                     }
       
            }
        }

        private void cmdTambahPerusahaan_Click(object sender, EventArgs e)
        {
            cmdTambahPerusahaan.Enabled = false;
            cmdUbahPerusahaan.Enabled = false;
            cmdSimpan.Enabled = true;
            cmdBatal.Visible = true;

            Clear();
            txtAlamat.Enabled = true;
            cmbBentuk.Enabled = true;
            cmbBentuk.Items.Clear();
            cmbBentuk.Items.Add("CV");
            cmbBentuk.Items.Add("PT");
            cmbBentuk.Items.Add("Lainnya");
            ctrlDaftarBank1.Create();

            txtNamaPerusahaan.Enabled = true;
            txtNamaPerusahaan.Enabled = true;
            txtNoRekening.Enabled = true;
            txtNoRekening.Enabled = true;
            txtNPWP.Enabled = true;
            txtNamaPimpinan.Enabled = true;
            txtNamaDalamRekening.Enabled = true;
            ctrlDaftarBank1.Enabled = true;

        }

        private void cmdCekRekening_Click(object sender, EventArgs e)
        {
            InquiryRekeningRequest requestInquiry = new InquiryRekeningRequest();
            requestInquiry.nomorRekening = txtNoRekening.Text.Replace(".", "");
            requestInquiry.sandiBank = ctrlDaftarBank1.Kode;


            string url = "";//
            DataInquiriyRekeningResponEx resp = new DataInquiriyRekeningResponEx();


            url = GlobalVar.BANK_URL + "InquiryRekening";
            //url = "http://36.92.240.142:8080/InquiryRekening";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
                //request.Headers.Set("client_secret", "");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInquiriyRekeningResponEx>(responseData);

                if (resp.error_kode != "00")
                {


                    MessageBox.Show(resp.message);
                }
                else
                {
                    txtNamaDalamRekening.Text = resp.namaPemilikRekening;
                    MessageBox.Show(resp.message);
                }
                // return resp;
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            Perusahaan p= new  Perusahaan();
            p= Simpan() ;
            if (p !=null || p.IDPerusahaan != 0 )
            {
                cmdTambahPerusahaan.Enabled = true;
                cmdUbahPerusahaan.Enabled = true;
                cmdSimpan.Enabled = false ;

                if (OnSavingPerusahaan != null)
                {
                    OnSavingPerusahaan(p);
                }

            };

        }

        private void cmdUbahPerusahaan_Click(object sender, EventArgs e)
        {
            cmdTambahPerusahaan.Enabled = false;
            cmdUbahPerusahaan.Enabled = false;
            cmdSimpan.Enabled = true;
            cmdBatal.Visible = true;
            cmbBentuk.Enabled = true;
            ctrlDaftarBank1.Enabled = true;
            SetDisplayMode(true);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            InquiryRekeningRequest requestInquiry = new InquiryRekeningRequest();
            requestInquiry.nomorRekening = txtNoRekening.Text.Replace(".", "");
            requestInquiry.sandiBank = ctrlDaftarBank1.Kode;


            string url = "";//
            DataInquiriyRekeningResponEx resp = new DataInquiriyRekeningResponEx();


            url = GlobalVar.BANK_URL + "InquiryRekening";
            //url = "http://36.92.240.142:8080/InquiryRekening";



            WebResponse objResponse = null;
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
                request.Method = "POST";


                //  url = url + "?sandiBank=" + ctrlDaftarBank1.Kode.Trim() + "&nomorRekening=" + txtNoRekening.Text.Replace(".", "");
                string JsonData = JsonConvert.SerializeObject(requestInquiry);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JsonData);
                request.ContentType = "application/Json";
                //request.Headers.Set("client_secret", "");
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                objResponse = (WebResponse)request.GetResponse();
                Stream streamdata = objResponse.GetResponseStream();
                StreamReader strReader = new StreamReader(streamdata);
                string responseData = strReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<DataInquiriyRekeningResponEx>(responseData);

                if (resp.error_kode != "00")
                {


                    MessageBox.Show(resp.message);
                }
                else
                {
                    txtNamaDalamRekening.Text = resp.namaPemilikRekening;
                    MessageBox.Show(resp.message);
                }
                // return resp;
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdBatal_Click(object sender, EventArgs e)
        {
            cmdTambahPerusahaan.Enabled = true;
            cmdUbahPerusahaan.Enabled = true;
            cmdSimpan.Enabled = false;
            cmdBatal.Visible = true;

        }

        private void txtNoRekening_TextChanged(object sender, EventArgs e)
        {

        }
        

    }
}
