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
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace KUAPPAS
{
    public partial class frmKUAPPASTerintegrasi : Form
    {
        
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();

        private int m_iDDinas = 0;
        private int m_iDUrusan = 0;

        private int _barisKUA;
        private int _KolomIDKUA;
        private DataTable dtKecamatan = new DataTable();
        private DataTable dtDesa = new DataTable();
        private Pen mypen;
        private Color mycolor = Color.Red;
        private Graphics mygraph;
        private decimal m_dPagu;
        private decimal m_currentVal;
        private int _profile;
        RemoteConnection rc;

        int x;
        int y;
        float w;
        float h;
        
        private decimal m_cJumlahRKPD=0L;
        private decimal m_cJumlahKUA =0L;
        private decimal m_cJumlahKUAP = 0L;
        //private decimal m_cJumlahKUAPerubahan = 0L;


        
        delegate void SetComboBoxCellType(int iRowIndex, int _col, int _value);
        bool bIsComboBox = false;
        bool bDesaIsComboBox = false;

        public frmKUAPPASTerintegrasi()
        {
            InitializeComponent();
            _hilightstyle.Font = new Font(gridRKPD.Font, FontStyle.Bold);
            _hilightstyle.BackColor = Color.GreenYellow;// new Font(gridRKPD.Font, FontStyle.Bold);
            _normalstyle.Font = new Font(gridRKPD.Font, FontStyle.Regular);
            _normalstyle.BackColor = Color.White;
            _profile = 1;
            m_dPagu=0L;

            _KolomIDKUA = 4;

            _barisKUA = -1;

            Point p = new Point(panel1.Left, panel1.Top);
            x = p.X;
            y = p.Y;
            w = panel1.Width;
            h = panel1.Height;
            rc = new RemoteConnection();

        }
        public int Profile
        {
            set { 
                _profile = value; 
            }
            get
            {
                return _profile;
            }
        }
        private void  DrowPanel(){
           Graphics g = panel1.CreateGraphics();  
           Pen p = new Pen(Color.Black);  
  
           SolidBrush sb = new SolidBrush(Color.Red);  
           g.DrawEllipse(p, x , y , w, h);  
           g.FillEllipse(sb, x , y , w, h);  
  
             
        }
        private void frmKUAPPASTerintegrasi_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
         //   ctrlHeader1.SetCaption("Input Data KUA PPAS", " Aplikasi harus bisa terhubung dengan Data Perencanaan/EPlanning/ Internet");
            gridRKPD.FormatHeader();
            gridRKPD.FormatGridView();
            if (GlobalVar.PP90 == true)
            {
                label5.Text = _profile == 2 ? "PERMENDAGRI 90" : "Kepmen 050";
            }
            gridRKPD.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            ctrlDinas1.Create();
            DrowPanel();
        }
        
        private void LoadPlafon(int profile= 2)
        {


            

            gridRKPD.Rows.Clear();
            RemoteConnection rCOn = new RemoteConnection();
            
          //  RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(GlobalVar.TahunAnggaran,_profile);
            RemoteConnectionLogic rcLogic = new RemoteConnectionLogic(2021, _profile);
          
            rCOn = rcLogic.GetByJenis(1,GlobalVar.JENIS_KONEKSI);
            if (rCOn == null)
            {
                MessageBox.Show("Koneksi ke database perencanaan tidak terinisialisasi.");
                return;
            }
            rCOn.Decrypt();
            m_cJumlahRKPD=0;
            if (GlobalVar.TahunAnggaran >= 2020)
            {
               
                List<RenstraKegiatan> _lst = new List<RenstraKegiatan>();
                List<RPJMDProgram> _lstPRG = new List<RPJMDProgram>();
                List<RenstraSubKegiatan> lstSubK = new List<RenstraSubKegiatan>();


                RPJMDProgramLogic pLogic = new RPJMDProgramLogic(2021, _profile);
                RenstraKegiatanLogic oKAPBDLogic = new RenstraKegiatanLogic(2021, _profile);
                RenstraSubKegiatanLogic oSubKLogic = new RenstraSubKegiatanLogic(2021, _profile);

                KUALogic kuaLogic = new KUALogic(2021, _profile);
                List<KUA> _lstKUA = new List<KUA>();
               

                _lst = oKAPBDLogic.GetBySKPD(m_iDDinas, rCOn);

                _lstPRG = pLogic.GetBySKPD(m_iDDinas, rCOn);
                lstSubK = oSubKLogic.GetBySKPD(m_iDDinas, rCOn);



                if (_lstPRG == null || _lstPRG.Count == 0)
                {
                    MessageBox.Show("Data RKPD tidak bisa diambil dari database Perencanaan. Perikesa Koneksi atau sila konfirmasi ke admin Perencanaan.");
                    return;


                }

                _lstKUA = kuaLogic.GetByIDDInas(m_iDDinas, (int)GlobalVar.TahunAnggaran, 0);

                string  target = "";
                decimal targetRP = 0L;
                if (_lst != null)
                {
                    foreach (RPJMDProgram p in _lstPRG)
                    {
                        string _kode = p.ID.ToString().Substring(0,3) + "." + p.ID.ToString().Substring(3) + "";

                        //TreeGridNode node = gridRKPD.Nodes.Add(
                                    
                        //            _kode + "  " + p.Nama,
                        //            "0",
                        //            "0",
                        //            "0", "0",p.IDUrusan.ToString(),
                        //            p.ID.ToString(),
                        //            "0",p.IDUrusanMaster.ToString(), p.IDProgramMaster.ToString(),"0", p.Nama);

                        if (GlobalVar.TahapAnggaran == 2020)
                        {
                            target = p.Target4;
                            targetRP = p.TargetRp4;
                        }

                        else
                        {
                            target = p.Target5;
                            targetRP = p.TargetRp5;
                        }
        
                        TreeGridNode node = gridRKPD.Nodes.Add(
                                    _kode + "  " + p.Nama,
                                    p.TargetRp4.ToRupiahInReport(),
                                    "0",
                                    "0", 
                                    "0",
                                    p.IDUrusan.ToString(),
                                    p.ID.ToString(),
                                    "0",
                                    p.IDUrusanMaster.ToString(), 
                                    p.IDProgramMaster.ToString(),
                                    "0", 
                                    p.Nama,"0","0",
                                    p.Keluaran,p.Outcome,
                                    "1",target,targetRP.ToRupiahInReport() );






                        foreach (RenstraKegiatan k in _lst)
                        {
                            decimal cKUA = 0L;
                            decimal cKUAPerubahan = 0L;

                            if (GlobalVar.PP90 == false)
                            {
                            
                                //if (_lstKUA != null)
                                //{
                                //    foreach (KUA kua in _lstKUA)
                                //    {
                                //        if (kua.Tahun == (int)GlobalVar.TahunAnggaran && kua.IDUrusan == k.IDUrusan && kua.IDProgram == k.IDProgram && kua.IDKegiatan == k.ID)
                                //        {
                                //            cKUA = kua.JumlahMurni;
                                //            cKUAPerubahan = kua.JumlahPerubahan;
                                //            m_cJumlahKUA = m_cJumlahKUA + cKUA;
                                //            m_cJumlahKUAP = m_cJumlahKUAP + cKUAPerubahan;

                                //        }
                                //    }
                                //}
                            }
                                
                             if ( p.IDUrusan == k.IDUrusan && p.ID== k.IDProgram )//&& k.TargetRp4>0 )
                             {
                                  string _kodek =k.IDUrusan.ToString() +"." + k.IDProgram.ToString().Substring(3) + "." +    k.ID.ToString().Substring(5) + "";
                                  decimal paguin1 = 0L;
                                  decimal paguplus1 = 0L;
                                  if (GlobalVar.TahunAnggaran== 2020)
                                  {
                                      target = k.Target4;

                                      cKUA = 0;
                                      cKUAPerubahan = 0;// kua.JumlahPerubahan;
                                      foreach (KUA kua in _lstKUA)
                                      {
                                          if (kua.Tahun == (int)GlobalVar.TahunAnggaran && kua.IDUrusan == k.IDUrusan && kua.IDProgram == k.IDProgram && kua.IDKegiatan == k.ID)
                                          {
                                              cKUA = kua.JumlahMurni;
                                              cKUAPerubahan = kua.JumlahPerubahan;
                                              m_cJumlahKUA = m_cJumlahKUA + cKUA;
                                              m_cJumlahKUAP = m_cJumlahKUAP + cKUAPerubahan;

                                          }
                                      }
                                      if (GlobalVar.TahunAnggaran == 2020)
                                      {

                                          targetRP = k.TargetRp4P;
                                          paguin1 = cKUA;// k.TargetRp3;
                                          paguplus1 = cKUAPerubahan;// k.TargetRp5;
                                      }
                                      else
                                      {
                                          targetRP = k.TargetRp5;
                                          paguin1 = cKUA;// k.TargetRp3;
                                          paguplus1 = cKUAPerubahan;// k.TargetRp5;
                                      }
                                  }

                                  else
                                  {
                                      target = k.Target5;
                                      targetRP = k.TargetRp5;
                                      paguin1 = k.TargetRp4;
                                      paguplus1 = 0;
                                  }
        
 
                                 TreeGridNode nodek = node.Nodes.Add(

                                              _kodek + "  " + k.Nama,
                                                "",
                                                targetRP.ToRupiahInReport(),

                                                paguin1.ToRupiahInReport(),
                                                paguplus1.ToRupiahInReport(), 
                                                k.IDUrusan.ToString(),
                                                k.IDProgram.ToString(),
                                                k.ID.ToString()
                                                ,k.IDUrusanMaster.ToString(), 
                                                k.IDProgramMaster.ToString(),
                                                k.IDMaster.ToString(),
                                                k.Nama,
                                                "0",
                                                "0",
                                                 k.Keluaran,k.Outcome,
                                                 "1",target,targetRP.ToRupiahInReport(),paguin1.ToRupiahInReport(), paguplus1.ToRupiahInReport() );
                                                
                                               

                                  if (lstSubK.Count > 0)
                                  {
                                      
                                          
                                      
                                      foreach (RenstraSubKegiatan rsk in lstSubK)
                                      {

                                          if (_lstKUA != null)
                                          {
                                              foreach (KUA kua in _lstKUA)
                                              {
                                                  if (kua.Tahun == (int)GlobalVar.TahunAnggaran && kua.IDUrusan == k.IDUrusan && kua.IDProgram == k.IDProgram && kua.IDKegiatan == k.ID && kua.IDSubKegiatan==rsk.ID )
                                                  {
                                                      cKUA = kua.JumlahMurni;
                                                      cKUAPerubahan = kua.JumlahPerubahan;
                                                      m_cJumlahKUA = m_cJumlahKUA + cKUA;
                                                      m_cJumlahKUAP = m_cJumlahKUAP + cKUAPerubahan;

                                                  }
                                              }
                                          }

                                          if (p.IDUrusan == rsk.IDUrusan && p.ID == rsk.ID/100000 && k.ID == rsk.ID/100)//&& k.TargetRp4>0 )
                                          {
                                              string kodeksub = rsk.ID.ToString().Substring(8) + "";
                                              TreeGridNode nodeksub = nodek.Nodes.Add(

                                              kodeksub + "  " + rsk.Nama,
                                              "",
                                              rsk.TargetRp5.ToRupiahInReport(),
                                              cKUA.ToRupiahInReport(), cKUAPerubahan.ToRupiahInReport(), k.IDUrusan.ToString(),
                                              k.IDProgram.ToString(),
                                              k.ID.ToString(), k.IDUrusanMaster.ToString(), k.IDProgramMaster.ToString(), k.IDMaster.ToString(),rsk.Nama, rsk.ID.ToString(), 
                                              rsk.IDMaster.ToString(),
                                              rsk.Outcome,
                                              rsk.Keluaran);



                                          }
                                      }
                                  }
                                 
                                 m_cJumlahRKPD = m_cJumlahRKPD+ k.TargetRp5;
                              }
                                
                            }


                        }

                    }

                }

            if (GlobalVar.PP90 == true)
            {
                int _iddinas =ctrlDinas1.GetID();
                txtJumlahRKPD.Text = m_cJumlahRKPD.ToRupiahInReport();
                txtJumlahKUA.Text = m_cJumlahKUA.ToRupiahInReport();
                txtJumlahKUAP.Text = m_cJumlahKUAP.ToRupiahInReport();
                List<PelaksanaUrusan> lstPelaksanaUrusan = new List<PelaksanaUrusan>();
                PelaksanaUrusanLogic oPelLogic = new PelaksanaUrusanLogic(2021);
                lstPelaksanaUrusan = oPelLogic.GetByIDDinas(_iddinas, rCOn);
                if (lstPelaksanaUrusan != null)
                {
                    if(oPelLogic.Bersihkan(_iddinas,2)){
                    foreach (PelaksanaUrusan pU in lstPelaksanaUrusan)
                    {
                        oPelLogic.Simpan(pU);


                    }
                    }
                }
            }
             HitungKUA();

            
        

        }
        private void ExpandAll()
        {
            foreach (TreeGridNode n in gridRKPD.Nodes)
            {
                if (n.HasChildren == true)
                {
                    n.Expand();
                    ExpandThisNode(n);
                }
            }
        }
        private void ExpandThisNode(TreeGridNode pn)
        {
            foreach (TreeGridNode n in pn.Nodes)
            {
                if (n.HasChildren)
                {
                    n.Expand();
                    ExpandThisNode(n);
                }
            }

        }
        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {

            m_iDDinas = pIDSKPD;// ctrlDinas1.GetID();
                //LoadPlafon();
            
                //PaguSKPD oPagu = new PaguSKPD();
                //PaguSKPDLogic oLogic = new PaguSKPDLogic(GlobalVar.TahunAnggaran);
                //oPagu = oLogic.GetByDinas((int)GlobalVar.TahunAnggaran, m_iDDinas, 3);

                //m_dPagu = oPagu.PaguMurni;
                //txtPaguSKPD.Text = m_dPagu.ToRupiahInReport();

            

            
            //  ExpandAll();

        }

        private void cmdCetak_Click(object sender, EventArgs e)
        {
            //int _gabungan = chkGabungan.Checked == true ? 1 : 0; 

            //frmReportViewer fW = new frmReportViewer();
            //fW.Profile = _profile;
            //if (chkDenganRKPD.Checked == false || GlobalVar.PP90== true)
            //{
            //    //bool bWitjRincian = chkDenganRincian.Checked;

            //    fW.CetakKUA(m_iDDinas, false,false, _gabungan);

            //    fW.Show();
            //}
            //else
            //{

            //    //fW.CetakRKPDKUA(m_iDDinas, false);
            //    fW.CetakRKPDKUA(0, false);
            //    fW.Show();
            //}

        }

        private bool GetTahap()
        {
            return true;
        }
       
        private void cmdSimpan_Click(object sender, EventArgs e)
        {
            ExpandAll();
            int _noUrut = 0;
            int idUrusan = 0;
            int  idKegiatan = 0;
            int idProgram = 0;
            long  idSubKegiatan = 0;
            string nama = "";

            KUALogic oLogic = new KUALogic(GlobalVar.TahunAnggaran, _profile);

            //if (oLogic.Clean(m_iDDinas) == false)
            //{
            //    MessageBox.Show(oLogic.LastError());
            //    return;
            //}

            for (int id = 0; id < gridRKPD.Rows.Count; id++)
            {
                if (gridRKPD.Rows[id].Cells[0].Value != null)
                {


                //    if (DataFormat.GetInteger(gridRKPD.Rows[id].Cells[5].Value) > 0)
                  //  {



                        idUrusan = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[id].Cells[5].Value));
                        idKegiatan = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[id].Cells[7].Value));
                        idSubKegiatan =  DataFormat.GetLong(DataFormat.GetString(gridRKPD.Rows[id].Cells[12].Value));
                        idProgram = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[id].Cells[6].Value));
                        if (idKegiatan == 0)
                        {
                            idKegiatan = 0;
                        }
                        if (idUrusan > 0 && idProgram > 0)
                        {
                            nama = DataFormat.GetString(DataFormat.GetString(gridRKPD.Rows[id].Cells[11].Value));


                            KUA o = new KUA();
                            o.Tahun = GlobalVar.TahunAnggaran;

                            o.KodeKategoriPelaksana = DataFormat.GetInteger(idUrusan.ToString().Substring(0, 1));//ok
                            o.KodeUrusanPelaksana = DataFormat.GetInteger(idUrusan.ToString().Substring(2));//ok


                            if (idKegiatan > 0)
                                o.KodeKegiatan = DataFormat.GetInteger(idKegiatan.ToString().Substring(5, 1));//ok
                            else
                                o.KodeKegiatan = 0;

                            o.KodeKategori = DataFormat.GetInteger(m_iDDinas.ToString().Substring(0, 1));
                            o.KodeUrusan = DataFormat.GetInteger(m_iDDinas.ToString().Substring(1, 2));
                            o.KodeSKPD = DataFormat.GetInteger(m_iDDinas.ToString().Substring(3, 2));
                            o.KodeUK = ctrlDinas1.KodeUK();
                            o.ID = 0;//DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[id].Cells[11].Value)); //=  DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[id].Cells[0].Value) + DataFormat.IntToStringWithLeftPad(DataFormat.GetInteger(gridRKPD.Rows[id].Cells[4].Value), 4));//+ DataFormat.IntToStringWithLeftPad(DataFormat.GetInteger(gridRKPD.Rows[id].Cells[4].Value), 3));
                            o.IDDinas = m_iDDinas;
                            o.IDUrusan = idUrusan;
                            o.IDProgram = idProgram;
                            o.IDLokasi = 0;
                            o.IDKegiatan = idKegiatan;
                            o.IDSubKegiatan = idSubKegiatan;
                            o.Jenis = 3;
                            o.Kecamatan = 0;//OK
                            o.Desa = 0;//ok
                            o.Dusun = 0;//ok

                            o.NamaUsulan = nama.Replace(idProgram.ToString(),"");//ok
//#if DEBUG

//                            o.JumlahOlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok
//                            o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok
//                            o.JumlahPerubahan = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[4].Value));//ok
//                            o.JumlahRKPD = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok
//#else

                            
                            //o.JumlahOlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok
                            //o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok
                            //o.JumlahPerubahan = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[4].Value));//ok
                            //o.JumlahRKPD = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok



                            o.JumlahOlah = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[3].Value));//ok
                            o.JumlahMurni = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[3].Value));//ok
                            o.JumlahPerubahan = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[4].Value));//ok
                            o.JumlahRKPD = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[2].Value));//ok

//#endif
                            o.IDUrusanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[8].Value);//OK
                            o.IDProgramMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[9].Value);//OK
                            o.IDKegiatanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[10].Value);//OK
                            o.PPKD = 0;

                            o.NoUrut = _noUrut;
                            o.UserID = GlobalVar.Pengguna.ID;

                            if (GlobalVar.PP90 == false)
                            {
                                if ((idKegiatan > 0))
                                {
                                    if (oLogic.Simpan(ref o, m_iDDinas) == true)
                                    {


                                        TKegiatanAPBDLogic oKegiatanAPBD = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran, _profile);
                                        TKegiatanAPBD tk = new TKegiatanAPBD();
                                        tk.Tahun = GlobalVar.TahunAnggaran;
                                        tk.IDDinas = m_iDDinas;
                                        tk.IDUrusan = idUrusan;
                                        tk.IDProgram = idProgram;
                                        tk.IDKegiatan = idKegiatan;
                                        tk.Jenis = 3;

                                        tk.Nama = nama.Replace(idProgram.ToString(), "");//ok
                                        tk.Pagu = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[3].Value));//ok
                                        tk.PaguABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[4].Value));//ok
                                       
                                        tk.Jenis = 3;

                                        tk.IDUrusanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[8].Value);//OK
                                        tk.IDProgramMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[9].Value);//OK
                                        tk.IDKegiatanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[10].Value);//OK
                                        oKegiatanAPBD.Simpan(tk, true);

                                    }
                                }
                                else
                                {
                                    if (o.JumlahMurni > 0 || o.JumlahPerubahan > 0)
                                    {
                                        if (oLogic.Simpan(ref o, m_iDDinas) == true)
                                        {
                                            TProgramAPBDLogic oorgAPBD = new TProgramAPBDLogic(GlobalVar.TahunAnggaran, _profile);
                                            TProgramAPBD pr = new TProgramAPBD();
                                            pr.Tahun = GlobalVar.TahunAnggaran;
                                            pr.IDDinas = m_iDDinas;
                                            pr.IDUrusan = idUrusan;
                                            pr.IDProgram = idProgram;
                                            pr.Keluaran = DataFormat.GetString(gridRKPD.Rows[id].Cells[19].Value);

                                            pr.Outcome = DataFormat.GetString(gridRKPD.Rows[id].Cells[20].Value);

                                            pr.PrioritasNasional = 0;// DataFormat.GetInteger(gridRKPD.Rows[id].Cells[21].Value);
                                            pr.Target = 0;// DataFormat.GetString(gridRKPD.Rows[id].Cells[22].Value);
                                            pr.RPJMD = 0;// DataFormat.FormatUangReportKeDecimal(gridRKPD.Rows[id].Cells[23].Value);


                                            pr.Jenis = 3;

                                            pr.Nama = nama.Replace(idProgram.ToString(), "");//ok

                                            //pr.IDUIDUrusanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[8].Value);//OK
                                            //pr.IDProgramMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[9].Value);//OK
                                            oorgAPBD.Simpan(pr);
                                        }
                                    }
                                }
                            }
                            else
                            {// On PP 90
                        
                                //if (idSubKegiatan == 7010120402)
                                //{
                                //    idSubKegiatan = 7010120402;
                                //}
                          
                                if ((idSubKegiatan >0))
                                {
                                    if (oLogic.Simpan(ref o, m_iDDinas) == true)
                                    {

                                        nama = DataFormat.GetString(DataFormat.GetString(gridRKPD.Rows[id].Cells[11].Value));
                                        TSubKegiatanLogic oSubKegiatanAPBD = new TSubKegiatanLogic(GlobalVar.TahunAnggaran,_profile);
                                        TSubKegiatan tk = new TSubKegiatan();
                                        tk.Tahun = GlobalVar.TahunAnggaran;
                                        tk.IDDinas = m_iDDinas;
                                        tk.IDUrusan = idUrusan;
                                        tk.IDProgram = idProgram;
                                        tk.IDKegiatan = idKegiatan;
                                        tk.IDSubKegiatan= idSubKegiatan;
                                        //tk.Jenis = 3;

                                        tk.Nama = nama.Replace(idProgram.ToString(), "");//ok
                                        tk.Pagu = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[3].Value));//ok
                                        tk.PaguABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[4].Value));//ok
                                       // tk.Jenis = 3;
                                        tk.Keluaran = DataFormat.GetString(gridRKPD.Rows[id].Cells[14].Value);
                                        tk.Outcome = DataFormat.GetString(gridRKPD.Rows[id].Cells[15].Value);
                                        tk.Lokasi = "";

                                        tk.IDUrusanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[8].Value);//OK
                                        tk.IDProgramMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[9].Value);//OK
                                        tk.IDKegiatanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[10].Value);//OK
                                        oSubKegiatanAPBD.Simpan(tk);



                                    }
                                }
                                    else
                                    {
                                        if (idKegiatan == 10401207)
                                        {
                                            idKegiatan = 10401207;
                                        }
                                    if (idKegiatan > 0 && idSubKegiatan == 0)
                                    {
                                        TKegiatanAPBDLogic oKegiatanAPBD = new TKegiatanAPBDLogic(GlobalVar.TahunAnggaran, _profile);
                                        TKegiatanAPBD tk = new TKegiatanAPBD();
                                        tk.Tahun = GlobalVar.TahunAnggaran;
                                        tk.IDDinas = m_iDDinas;
                                        tk.IDUrusan = idUrusan;
                                        tk.IDProgram = idProgram;
                                        tk.IDKegiatan = idKegiatan;
                                        tk.Jenis = 3;

                                        tk.Nama = nama.Replace(idProgram.ToString(), "");//ok
                                        tk.Pagu = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[3].Value));//ok
                                        tk.PaguABT = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[4].Value));//ok
                                        tk.Jenis = 3;
                                        tk.AnggaranTahunLalu= DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[19].Value));//ok
                                        tk.AnggaranTahunDepan = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[id].Cells[20].Value));//ok
                                        tk.IDUrusanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[8].Value);//OK
                                        tk.IDProgramMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[9].Value);//OK
                                        tk.IDKegiatanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[10].Value);//OK
                                        tk.Keluaran = DataFormat.GetString(gridRKPD.Rows[id].Cells[14].Value);
                                        tk.Outcome = DataFormat.GetString(gridRKPD.Rows[id].Cells[15].Value);
                                        oKegiatanAPBD.Simpan(tk, true);



                                    }
                                }

                                if (idProgram == 40103)
                                {
                                    idProgram = 40103;
                                }
                                    if ( idProgram>0 && idKegiatan==0 ) // &&  ( o.JumlahMurni > 0 || o.JumlahPerubahan > 0))
                                    {
                                        TProgramAPBDLogic oorgAPBD = new TProgramAPBDLogic(GlobalVar.TahunAnggaran,_profile);
                                        TProgramAPBD pr = new TProgramAPBD();
                                        pr.Tahun = GlobalVar.TahunAnggaran;
                                        pr.IDDinas = m_iDDinas;
                                        pr.IDUrusan = idUrusan;
                                        pr.IDProgram = idProgram;
                                        pr.Jenis = 3;

                                          //pr.IDUIDUrusanMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[8].Value);//OK
                                        //pr.IDProgramMaster = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[9].Value);//OK
                                   
                                      pr.Nama = nama.Replace(idProgram.ToString(), "");//ok

                                        pr.Keluaran = DataFormat.GetString(gridRKPD.Rows[id].Cells[14].Value);
                                        pr.Outcome = DataFormat.GetString(gridRKPD.Rows[id].Cells[15].Value);
                                        pr.PrioritasNasional = DataFormat.GetInteger(gridRKPD.Rows[id].Cells[16].Value);
                                        pr.Target = DataFormat.GetDecimal(gridRKPD.Rows[id].Cells[17].Value);
                                        pr.RPJMD = DataFormat.FormatUangReportKeDecimal(gridRKPD.Rows[id].Cells[18].Value);
                                        
                                        if (idProgram == 40103)
                                        {
                                            idProgram = 40103;
                                        }
                                        if (oorgAPBD.Simpan(pr) == false)
                                        {
                                            MessageBox.Show("Ada maslaah menyimpan program \n" + oorgAPBD.LastError());
                                        }
                                   }

                                
                             }
                        }
                    }
                //}
            }
            MessageBox.Show("Penyimpanan Selesai.");
        }

        private void gridRKPD_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 3|| e.ColumnIndex == 4)
              //  if (e.ColumnIndex == 4)

                gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DataFormat.FormatUangReportKeDecimal(DataFormat.GetString(gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
            else
                e.Cancel = true;

        }

        private void gridRKPD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
               if (e.ColumnIndex == 3){


                decimal _jumlahKUA  = DataFormat.GetDecimal(gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                decimal _jumlahRKPD = DataFormat.GetDecimal(gridRKPD.Rows[e.RowIndex].Cells[1].Value.ToString().FormatUangReportKeDecimal ());
                

                //if (_jumlahRKPD < _jumlahKUA)
                //{
                //    MessageBox.Show("KUA  Tidak Boleh Melebihi RKPD.");
                //    gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                //    return;

                //}

                //if (CekJumlahProgram(e.RowIndex) == false)
                //{
                //    MessageBox.Show("Jumlah Untuk program ini melebihi RKPD Program");
                //    gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";//.ToRupiahInReport();
                //    return;
                //} 


                HitungKUA();

               

                gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _jumlahKUA.ToRupiahInReport();     
                //HitungJumlah();
                //HitungJumlahKegiatan(e.RowIndex);
                //HitungJumlahProgram(e.RowIndex);


              //  gridSPDKegiatan.Rows[e.RowIndex].Cells[19].Value = "1";
                //HitungJumlahBL();
            }
               if (e.ColumnIndex == 4)
               {


                   decimal _jumlahKUA = DataFormat.GetDecimal(gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                   decimal _jumlahRKPD = DataFormat.GetDecimal(gridRKPD.Rows[e.RowIndex].Cells[1].Value.ToString().FormatUangReportKeDecimal());

                   HitungKUA();



                   gridRKPD.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _jumlahKUA.ToRupiahInReport();
                   
               }
        }

        private decimal HitungKUA ()
        {

            m_cJumlahKUA = 0;
            m_cJumlahRKPD = 0;
            m_cJumlahKUAP = 0;
            int idProgram = -1;
            int rowProgram = 0;

            int rowKegiaran = 0;
            int idKegiatan = 0;
            decimal cJumlahPerProgram =0L;
            decimal cJumlahRKPDPerProgram = 0L;
            decimal cJumlahPerProgramP = 0L;
            decimal cJulahPerKegiatan = 0L;
            decimal cJumlahPerKegiatanP = 0L;
           
            // decimal cJumlahRKPDPerProgramP = 0L;
            
            ExpandAll();

            for (int row = 0; row < gridRKPD.Rows.Count; row++)
            {
                if (gridRKPD.Rows[row].Cells[7].Value != null){
                
                
                if (GlobalVar.PP90 == true)
                {
                    int  idKegiatanRow = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[row].Cells[7].Value));
                    long idSubKegiatan = DataFormat.GetLong(DataFormat.GetString(gridRKPD.Rows[row].Cells[12].Value));

                 //   int idKegiatan = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[row].Cells[11].Value));

                    if (idSubKegiatan > 0)
                    {
                        decimal _jumlahKUA = gridRKPD.Rows[row].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                        decimal _jumlahKUAP = gridRKPD.Rows[row].Cells[4].Value.ToString().FormatUangReportKeDecimal();
                        decimal _jumlahRKPD = gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();

                        m_cJumlahKUA = m_cJumlahKUA + _jumlahKUA;
                        m_cJumlahKUAP = m_cJumlahKUAP + _jumlahKUAP;
                        m_cJumlahRKPD = m_cJumlahRKPD + _jumlahRKPD;


                        cJumlahPerProgram = cJumlahPerProgram + _jumlahKUA;
                        cJumlahRKPDPerProgram = cJumlahRKPDPerProgram + _jumlahRKPD;
                        cJumlahPerProgramP = cJumlahPerProgramP + _jumlahKUAP;

                        cJulahPerKegiatan = cJulahPerKegiatan + _jumlahKUA;
                        cJumlahPerKegiatanP = cJumlahPerKegiatanP + _jumlahKUA;
                    
                    } else {
                           // /**************************************************
                        if (idKegiatanRow > 0)
                        {
                           // int kodeKegiatanOnThisRow = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[row].Cells[6].Value));
                            if (idKegiatan != idKegiatanRow)
                            {
                                
                                gridRKPD.Rows[row].DefaultCellStyle = _hilightstyle;
                                gridRKPD.Rows[rowKegiaran].Cells[3].Value = cJulahPerKegiatan.ToRupiahInReport();
                                gridRKPD.Rows[rowKegiaran].Cells[4].Value = cJumlahPerKegiatanP.ToRupiahInReport();
                                idKegiatan = idKegiatanRow;
                                rowKegiaran = row;
                                cJulahPerKegiatan = 0;
                                cJumlahPerKegiatanP = 0;
                    

                            }

                        }
                        else
                        {
                            int kodeProgramOnThisRow = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[row].Cells[6].Value));
                            if (idProgram != kodeProgramOnThisRow)
                            {

                                gridRKPD.Rows[row].DefaultCellStyle = _hilightstyle;
                                gridRKPD.Rows[rowProgram].Cells[3].Value = cJumlahPerProgram.ToRupiahInReport();
                                gridRKPD.Rows[rowProgram].Cells[4].Value = cJumlahPerProgramP.ToRupiahInReport();

                                decimal jumlahRKPDProgram = 0;// gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();

                                if (gridRKPD.Rows[row].Cells[1].Value != null)
                                    jumlahRKPDProgram = gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();


                                idProgram = kodeProgramOnThisRow;
                                rowProgram = row;
                                cJumlahPerProgram = 0L;
                                cJumlahPerProgramP = 0L;
                                cJumlahRKPDPerProgram = 0;
                            }
                        }






                         
                        // ***************************************************************


                    }

                }
                else

                { // Bukan Permendagri 90
                     idKegiatan = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[row].Cells[7].Value));
//                    long idSubKegiatan = DataFormat.GetLong(DataFormat.GetString(gridRKPD.Rows[row].Cells[12].Value));

                    
                    if (idKegiatan > 0 ) {
                            decimal _jumlahKUA = gridRKPD.Rows[row].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                            decimal _jumlahKUAP = gridRKPD.Rows[row].Cells[4].Value.ToString().FormatUangReportKeDecimal();
                            decimal _jumlahRKPD = gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();
                            
                            m_cJumlahKUA = m_cJumlahKUA + _jumlahKUA;

                            m_cJumlahKUAP = m_cJumlahKUAP + _jumlahKUAP;
                            m_cJumlahRKPD = m_cJumlahRKPD + _jumlahRKPD;

                            cJumlahPerProgram =cJumlahPerProgram+ _jumlahKUA;
                            cJumlahRKPDPerProgram = cJumlahRKPDPerProgram + _jumlahRKPD;
                            cJumlahPerProgramP = cJumlahPerProgramP + _jumlahKUAP;
                  
                    }
                    else 
                    {
                           int kodeProgramOnThisRow = DataFormat.GetInteger(DataFormat.GetString(gridRKPD.Rows[row].Cells[6].Value));
                           if (idProgram != kodeProgramOnThisRow)
                            {

                                    gridRKPD.Rows[row].DefaultCellStyle = _hilightstyle;
                                    gridRKPD.Rows[rowProgram].Cells[3].Value = cJumlahPerProgram.ToRupiahInReport();
                                    gridRKPD.Rows[rowProgram].Cells[4].Value = cJumlahPerProgramP.ToRupiahInReport();

                                    decimal jumlahRKPDProgram = 0;// gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();

                                    if (gridRKPD.Rows[row].Cells[1].Value !=null)
                                        jumlahRKPDProgram = gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();

                        
                                    idProgram = kodeProgramOnThisRow;
                                    rowProgram = row;
                                    cJumlahPerProgram = 0L;
                                    cJumlahPerProgramP = 0L;                        
                                    cJumlahRKPDPerProgram = 0;
                            }
                       }
                   }
                }               
            }
            gridRKPD.Rows[rowProgram].Cells[3].Value = cJumlahPerProgram.ToRupiahInReport();
            gridRKPD.Rows[rowProgram].Cells[4].Value = cJumlahPerProgramP.ToRupiahInReport();
            gridRKPD.Rows[rowKegiaran].Cells[3].Value = cJulahPerKegiatan.ToRupiahInReport();
            gridRKPD.Rows[rowKegiaran].Cells[4].Value = cJumlahPerKegiatanP.ToRupiahInReport();

            CekKuaProgram();

            txtJumlahKUA.Text = m_cJumlahKUA.ToRupiahInReport();
            txtJumlahKUAP.Text = m_cJumlahKUAP.ToRupiahInReport();

            return m_cJumlahKUA;

        }


        private bool CekKuaProgram()
        {
            bool bRet = true;

            for (int row = 0; row < gridRKPD.Rows.Count; row++)
            {
                if (gridRKPD.Rows[row].Cells[7].Value != null)
                {
                    long idKegiatan = DataFormat.GetLong(DataFormat.GetString(gridRKPD.Rows[row].Cells[7].Value));

                    if (idKegiatan == 0)
                    {
                        decimal JumlahRKPD = gridRKPD.Rows[row].Cells[1].Value.ToString().FormatUangReportKeDecimal();
                        decimal JumlahKUA = gridRKPD.Rows[row].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                        if (JumlahRKPD < JumlahKUA)
                        {
                            bRet = bRet && false; 
                          }
                    }

                }
            }
            return bRet;

        }

        private bool CekJumlahProgram(int row)
        {
            decimal cJumlahRKPDProgram = 0L;
            decimal cJumlahKUAProgram = 0L;
            int idProgram = DataFormat.GetInteger(gridRKPD.Rows[row].Cells[6].Value);

            for (int i = 0; i < gridRKPD.Rows.Count; i++)
            {
                if (gridRKPD.Rows[row].Cells[6].Value != null)
                {
                    // hanya untuk yang sama program
                    if (DataFormat.GetInteger(gridRKPD.Rows[i].Cells[6].Value) == idProgram)
                    {

                        long idKegiatan = DataFormat.GetLong(gridRKPD.Rows[i].Cells[7].Value);

                        if (idKegiatan == 0)
                        {
                            cJumlahRKPDProgram = gridRKPD.Rows[i].Cells[1].Value.ToString().FormatUangReportKeDecimal();

                        }
                        else
                        {
                            cJumlahKUAProgram = cJumlahKUAProgram + gridRKPD.Rows[i].Cells[3].Value.ToString().FormatUangReportKeDecimal();
                        }
                    }
                }
            }

            if (cJumlahRKPDProgram < cJumlahKUAProgram)
                return false;
            else
                return true;

            

        }
        private void gridRKPD_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                //gridSPDKegiatan.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                //gridSPDKegiatan.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
              //  SendKeys.Send("{F2}");
            }
        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void cmdTastKonekso_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (InternetAvailability.IsInternetAvailable() == false)
            {
                panel1.BackColor = Color.Red;
                lblCOnnection.Text = "Tak Terhubung Perencanaan";
             //   lblCOnnection.Left = panel1.Left + 10;
            }
            else
            {
                panel1.BackColor = Color.Green;
                lblCOnnection.Text = "Terhubung Perencanaan";
               // lblCOnnection.Left = panel1.Left + 10;
                
            }
            lblCOnnection.BackColor = panel1.BackColor;
            
        }

        private void gridRKPD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            LoadPlafon();

            PaguSKPD oPagu = new PaguSKPD();
            PaguSKPDLogic oLogic = new PaguSKPDLogic(GlobalVar.TahunAnggaran, _profile);
            oPagu = oLogic.GetByDinas((int)GlobalVar.TahunAnggaran, m_iDDinas, 3);
            if (oPagu != null)
            {
                m_dPagu = oPagu.PaguMurni;
                txtPaguSKPD.Text = m_dPagu.ToRupiahInReport();
            }
        }

        private void cmdSmaakanDenganKUAAWAL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah benar akan menyamakan nilai KUA Perubahan dengan Murni? ", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                KUALogic oLogic = new KUALogic((int)GlobalVar.TahunAnggaran,_profile );
                if (oLogic.SamakanPerubahanDenganMurni(m_iDDinas) == true)
                {
                    MessageBox.Show("Nilai Pagu Perubahan sama dengan nilai pagu murni");
                }
                else
                {
                    MessageBox.Show("Gagal melakukan penyimpanan...");
                }

            }
        }

        private void cmdCetakSemua_Click(object sender, EventArgs e)
        {
            int _gabungan = chkGabungan.Checked == true ? 1 : 0; 

            //frmReportViewer fW = new frmReportViewer();
            //fW.Profile = _profile;
            //    fW.CetakKUA(0, false, false, _gabungan);

            //    fW.Show();
            
        }
    }
}
