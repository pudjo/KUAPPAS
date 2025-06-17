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
    
    public partial class frmCekDPAPlafon : Form
    {
        DataGridViewCellStyle _hilightstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _normalstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _headerstyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _lokasiStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle _errorSTyle = new DataGridViewCellStyle();
        private int m_currentStatus;
        private bool m_iStatusInput;

        public frmCekDPAPlafon()
        {
            InitializeComponent();
            

            _hilightstyle.Font = new Font(gridPlafon.Font, FontStyle.Bold);
            _hilightstyle.BackColor = Color.GreenYellow;// new Font(gridKUA.Font, FontStyle.Bold);
            _normalstyle.Font = new Font(gridPlafon.Font, FontStyle.Regular);
            _normalstyle.BackColor = Color.White;

            _errorSTyle.Font = new Font(gridPlafon.Font, FontStyle.Bold);
            _errorSTyle.BackColor = Color.Red;// new Font(gridKUA.Font, FontStyle.Bold);


            

        }

        private void frmCekDPAPlafon_Load(object sender, EventArgs e)
        {
            gridDPA.FormatHeader();
            gridDPAAnggaranKas.FormatHeader();
            gridPlafon.FormatHeader();

            ctrlDinas1.Create();
            if (GlobalVar.Pengguna.SKPD>0)
                cmdKunciAnggaran.Visible = false;

            //tabControl1.TabPages(0).Hide;
            //tabControl1.TabPages.RemoveAt(0);
            //tabControl1.TabPages.RemoveAt(1);
 
            //tabControl1.TabPages.Remove(tabCek);

        }

        private void cmdPanggilData_Click(object sender, EventArgs e)
        {
           
            PerbandinganPlafonKegiatanLogic oLogic = new PerbandinganPlafonKegiatanLogic(GlobalVar.TahunAnggaran);
            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            _lst = oLogic.GetPerbandinganPlafonKegiatan(ctrlDinas1.GetID(), (int)GlobalVar.TahunAnggaran);
            
            gridPlafon.Rows.Clear();

            if (_lst != null)
            {
                foreach (PerbandinganPlafonKegiatan p in _lst)
                {
                    string[] row = { "Hapus", "Update", p.IDUrusan.ToKodeUrusan(), p.IDProgram.ToKodeProgram(), p.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), p.IDKegiatan2.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), p.Nama, p.Nama2, p.Plafon.ToRupiahInReport(), p.DPA.ToRupiahInReport(), (p.Plafon - p.DPA).ToRupiahInReport() };
                    gridPlafon.Rows.Add(row);

                }
            }

        }

        private void cmdCekDPA_Click(object sender, EventArgs e)
        {
            PerbandinganPlafonKegiatanLogic oLogic = new PerbandinganPlafonKegiatanLogic(GlobalVar.TahunAnggaran);
            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            int pTahap;
            pTahap = 3;
            _lst = oLogic.PerbandinganRekeningPenjabaran(ctrlDinas1.GetID(), (int)GlobalVar.TahunAnggaran, pTahap);
            gridDPA.Rows.Clear();

            lblOK.Text = "OK";
            lblOK.ForeColor = Color.Green;

            if (_lst != null)
            {
                foreach (PerbandinganPlafonKegiatan p in _lst)
                {
                    if (p.IDRekening == 0)
                    {
                        string[] row = { "+", p.IDUrusan.ToKodeUrusan(), p.IDProgram.ToKodeProgram(), p.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), "0", p.Nama,p.Plafon.ToRupiahInReport(), p.DPA.ToRupiahInReport(), (p.Plafon - p.DPA).ToRupiahInReport() };
                        gridDPA.Rows.Add(row);
                        gridDPA.Rows[gridDPA.Rows.Count - 2].DefaultCellStyle = _hilightstyle;

                    }
                    else
                    {

                        string[] row = { "", p.IDUrusan.ToKodeUrusan(), p.IDProgram.ToKodeProgram(), p.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), p.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), p.Nama, p.Plafon.ToRupiahInReport(), p.DPA.ToRupiahInReport(), (p.Plafon - p.DPA).ToRupiahInReport() };

                        gridDPA.Rows.Add(row);
                    }
                    if (p.Plafon != p.DPA)
                    {
                        gridDPA.Rows[gridDPA.Rows.Count - 2].DefaultCellStyle = _errorSTyle;
                        lblOK.Text = "Ada Kesalahan";
                        lblOK.ForeColor = Color.Red;
                    }


                }
            }

        }

        private void cmdCekAnggaranKas_Click(object sender, EventArgs e)
        {
            PerbandinganPlafonKegiatanLogic oLogic = new PerbandinganPlafonKegiatanLogic(GlobalVar.TahunAnggaran);
            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            _lst = oLogic.PerbandinganDPAANggaranKas(ctrlDinas1.GetID(), (int)GlobalVar.TahunAnggaran);
            gridDPAAnggaranKas.Rows.Clear();

            lblOKAK.Text = "";
            lblOKAK.ForeColor = Color.Green;
            bool bOK = true;
            if (_lst != null)
            {
                foreach (PerbandinganPlafonKegiatan p in _lst)
                {
                    string sub = "";
                    if (p.IDSubKegiatan > 0)
                        sub = p.IDSubKegiatan.ToString().Substring(p.IDSubKegiatan.ToString().Length - 2);
                    if (p.IDRekening == 0)
                    {
                        string[] row = { "+", p.IDUrusan.ToKodeUrusan(), p.IDProgram.ToKodeProgram(), p.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), sub,"", p.Nama, p.Plafon.ToRupiahInReport(), p.DPA.ToRupiahInReport(), (p.Plafon - p.DPA).ToRupiahInReport() };
                        gridDPAAnggaranKas.Rows.Add(row);
                        gridDPAAnggaranKas.Rows[gridDPAAnggaranKas.Rows.Count - 2].DefaultCellStyle = _hilightstyle;

                    }
                    else
                    {

                        string[] row = { "", p.IDUrusan.ToKodeUrusan(), p.IDProgram.ToKodeProgram(), p.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan), sub,p.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), p.Nama, p.Plafon.ToRupiahInReport(), p.DPA.ToRupiahInReport(), (p.Plafon - p.DPA).ToRupiahInReport() };
                        gridDPAAnggaranKas.Rows.Add(row);
                    }
                    if (p.Plafon != p.DPA)
                    {
                        bOK = bOK && false;


                        gridDPAAnggaranKas.Rows[gridDPAAnggaranKas.Rows.Count - 2].DefaultCellStyle = _errorSTyle;
                        lblOKAK2.Text = "Ada Kesalahan";
                        lblOKAK2.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblOKAK2.Text = "OK";
                        lblOKAK2.ForeColor = Color.Green;
                    }


                }
            }
            if (bOK==true )
            {
                lblOKAK2.Text = "OK";
                lblOKAK2.ForeColor = Color.Green;                
            }
            else
            {
                
                lblOKAK2.Text = "Ada Kesalahan";
                lblOKAK2.ForeColor = Color.Red;
            }
        }
        private void cmdKunciAnggaran_Click(object sender, EventArgs e)
        {
            TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
            if (m_iStatusInput == true)
            {
                oLogic.KunciInput(ctrlDinas1.GetID(), GlobalVar.TahunAnggaran);
                m_iStatusInput = false  ;
            }
            else
            {
                oLogic.BukaKunciInput(ctrlDinas1.GetID(), GlobalVar.TahunAnggaran);
                m_iStatusInput = true ;

            }
            
            

        }

        private void cmdTutuAK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah benar akan mengunci data Anggaran Kas sehingga pengguna tidak bisamerubahlagi?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TahapanAnggaranLogic oLogic = new TahapanAnggaranLogic(GlobalVar.TahunAnggaran);
                if (oLogic.SetStatusAK(ctrlDinas1.GetID(), GlobalVar.TahunAnggaran, 9) == false)
                {
                    MessageBox.Show(oLogic.LastError());
                }
                else
                {
                    MessageBox.Show("Data ANggaran Kas sudah dikunci.");
                }
            }

        }

        private void ctrlDinas1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDinas1_OnChanged(int pIDSKPD, int pIDUK)
        {


            if (ctrlDinas1.GetStatusInput ()  == false )
            {
                m_iStatusInput = false;
                cmdKunciAnggaran.Text = "Buka Kunci";
            }else {
                m_iStatusInput = true ;
                cmdKunciAnggaran.Text = "Kunci Input";
            }
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
