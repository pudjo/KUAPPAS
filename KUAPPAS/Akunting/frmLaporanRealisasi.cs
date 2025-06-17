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
using Excel = Microsoft.Office.Interop.Excel;

namespace KUAPPAS.Akunting
{
    public partial class frmLaporanRealisasi : Form
    {
        int m_idDinas;
        public frmLaporanRealisasi()
        {
            InitializeComponent();
        }

        private void frmLaporanRealisasi_Load(object sender, EventArgs e)
        {
            ctrlDinas1.Create();        
            ctrlBulan1.Create();
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        //{
        //    SPJLogic oLogic = new SPJLogic((int)GlobalVar.TahunAnggaran);
        //    m_idDinas= ctrlDinas1.GetID();
        //    gridRealisasi.Rows.Clear();

        //    DateTime tanggalAwal = ctrlBulan1.GetFirstDay();// 
        //    DateTime tanggalAKhir = ctrlBulan1.GetLastDay();//

        //    decimal gl = 0L;
        //    decimal gk  =0L;
        //    decimal bl = 0L;
        //    decimal bk  = 0L;
        //    decimal ul = 0L;
        //    decimal uk = 0L;

        //    decimal Tgl = 0L;
        //    decimal Tgk = 0L;
        //    decimal Tbl = 0L;
        //    decimal Tbk = 0L;
        //    decimal Tul = 0L;
        //    decimal Tuk = 0L;

        //    decimal TglK = 0L;
        //    decimal TgkK = 0L;
        //    decimal TblK = 0L;
        //    decimal TbkK = 0L;
        //    decimal TulK = 0L;
        //    decimal TukK = 0L;



        //      //    
            
        //    List<FungsionalRekening> lst = oLogic.GetAnggaran2021(m_idDinas);
        //    if (lst == null)
        //    {
        //        MessageBox.Show(oLogic.LastError());
        //        return;
        //    }
           
        //    //List<FungsionalRekening> lstRealisasi = oLogic.GetRealisasiGabunganIDDInas(m_idDinas, 0, tanggalAwal, tanggalAKhir);
        //    List<FungsionalRekening> lstRealisasi = oLogic.GetRealisasiGabunganIDDInasBasedREALISASI(m_idDinas, 0, tanggalAwal, tanggalAKhir);

        //    if (lstRealisasi == null)
        //    {
        //        MessageBox.Show(oLogic.LastError());
        //        return;
        //    }
        //    if (lst != null)
        //    {
        //        foreach (FungsionalRekening s in lst)
        //        {
        //            gl = 0L;
        //            gk = 0L;
        //            bl = 0L;
        //            bk = 0L;
        //            ul = 0L;
        //            uk = 0L;

                   

        //            foreach (FungsionalRekening sp in lstRealisasi)
        //            {
        //                if (sp.IDUrusan == s.IDUrusan && sp.IDProgram == s.IDProgram && sp.IDKegiatan == s.IDKegiatan && sp.IDSubKegiatan== s.IDSubKegiatan && s.IDRekening == sp.IDRekening)
        //                {
        //                    gl = gl + sp.GL;
        //                    gk = gk + sp.GK;
        //                    bl = bl + sp.BL;

        //                    bk = bk + sp.BJk;
        //                    ul = ul + sp.UL;
        //                    uk = uk + sp.UK;
        //                    if (sp.IDRekening > 0 && sp.BL > 0)
        //                    {
        //                        Console.WriteLine(sp.BL.ToString());

        //                    }

        //                    if (sp.IDRekening > 0 && s.IDRekening > 0)
        //                    {

        //                        TglK = TglK + sp.GL;
        //                        TgkK = TgkK + sp.GK;
        //                        TblK = TblK + sp.BL;
        //                        TbkK = TbkK + sp.BJk;
        //                        TulK = TulK + sp.UL;
        //                        TukK = TukK + sp.UK;

        //                    }

        //                }

        //            }

        //            string kodesub = "";
        //            if (s.IDSubKegiatan > 0)
        //            {
        //                kodesub = (s.IDSubKegiatan % 100).ToString();
        //            }
        //            string[] row = { s.IDUrusan.ToKodeUrusan(), s.IDProgram.ToKodeProgram(), s.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan) ,kodesub, s.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), s.Uraian , s.Anggaran.ToRupiahInReport(), 
        //                           gl.ToRupiahInReport(), gk.ToRupiahInReport(),(gl+gk).ToRupiahInReport(),
        //                           bl.ToRupiahInReport(), bk.ToRupiahInReport(),(bl+bk).ToRupiahInReport(),
        //                           ul.ToRupiahInReport(), uk.ToRupiahInReport(),(ul+uk).ToRupiahInReport(),
        //                           (gl+gk+bl+bk+ul+uk).ToRupiahInReport(),
        //                           (s.Anggaran-(gl+gk+bl+bk+ul+uk)).ToRupiahInReport()
        //                           };


                    


        //            gridRealisasi.Rows.Add(row);
        //        }

        //        //TglK = TglK + gl;
        //        //TgkK = TgkK + gk;
        //        //TblK = TblK + bl;
        //        //TbkK = TbkK + bk;
        //        //TulK = TulK + ul;
        //        //TulK = TukK + uk;
        //        string[] rowj = { "", "", "","", "", "", "", 
        //                           TglK.ToRupiahInReport(), TgkK.ToRupiahInReport(),(TglK+TgkK).ToRupiahInReport(),
        //                           TblK.ToRupiahInReport(), TbkK.ToRupiahInReport(),(TblK+TbkK).ToRupiahInReport(),
        //                           TulK.ToRupiahInReport(), TukK.ToRupiahInReport(),(TulK+TulK).ToRupiahInReport(),
        //                           (TglK+TgkK+TblK+TbkK+TulK+TukK).ToRupiahInReport(),
        //                           ""
        //                           };
        //        gridRealisasi.Rows.Add(rowj);
            }
            
         }
    }
}
