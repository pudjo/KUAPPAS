using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    public partial class ctrlSPJFungsional : UserControl
    {
        int m_idDinas;
        public ctrlSPJFungsional()
        {
            InitializeComponent();
        }

        private void ctrlSPJFungsional_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(0);
         //   ctrlDinas1.Create();
            gridSPJ.FormatHeader();
            gridPenerimaanPengeluaran.FormatHeader();
            ctrlBulan1.Create();
        }
        public void SetDInas(int iddinas)
        {
            m_idDinas = iddinas;
        }
        private void cmdLoadData_Click(object sender, System.EventArgs e)
        {
            SPJLogic oLogic = new SPJLogic((int)GlobalVar.TahunAnggaran);
         //   m_idDinas = ctrlDinas1.GetID();
            gridSPJ.Rows.Clear();

            DateTime tanggalAwal = ctrlBulan1.GetFirstDay();// 
            DateTime tanggalAKhir = ctrlBulan1.GetLastDay();//

            decimal gl = 0L;
            decimal gk = 0L;
            decimal bl = 0L;
            decimal bk = 0L;
            decimal ul = 0L;
            decimal uk = 0L;

            decimal Tgl = 0L;
            decimal Tgk = 0L;
            decimal Tbl = 0L;
            decimal Tbk = 0L;
            decimal Tul = 0L;
            decimal Tuk = 0L;

            decimal TglK = 0L;
            decimal TgkK = 0L;
            decimal TblK = 0L;
            decimal TbkK = 0L;
            decimal TulK = 0L;
            decimal TukK = 0L;



            //    

            List<FungsionalRekening> lst = oLogic.GetAnggaran(m_idDinas);
            if (lst == null)
            {
                MessageBox.Show(oLogic.LastError());
                return;
            }

            List<FungsionalRekening> lstRealisasi = oLogic.GetRealisasiGabunganIDDInas(m_idDinas, 0, tanggalAwal, tanggalAKhir);

            if (lstRealisasi == null)
            {
                MessageBox.Show(oLogic.LastError());
                return;
            }
            if (lst != null)
            {
                foreach (FungsionalRekening s in lst)
                {
                    gl = 0L;
                    gk = 0L;
                    bl = 0L;
                    bk = 0L;
                    ul = 0L;
                    uk = 0L;



                    foreach (FungsionalRekening sp in lstRealisasi)
                    {
                        if (sp.IDUrusan == s.IDUrusan && sp.IDProgram == s.IDProgram && sp.IDKegiatan == s.IDKegiatan && s.IDRekening == sp.IDRekening)
                        {
                            gl = gl + sp.GL;
                            gk = gk + sp.GK;
                            bl = bl + sp.BL;

                            bk = bk + sp.BJk;
                            ul = ul + sp.UK;
                            uk = uk + sp.UK;

                        }

                    }


                    string[] row = { s.IDUrusan.ToKodeUrusan(), s.IDProgram.ToKodeProgram(), s.IDKegiatan.ToKodeKegiatan(GlobalVar.ProfileProgramKegiatan) , s.IDRekening.ToKodeRekening(GlobalVar.ProfileRekening), s.Uraian , s.Anggaran.ToRupiahInReport(), 
                                   gl.ToRupiahInReport(), gk.ToRupiahInReport(),(gl+gk).ToRupiahInReport(),
                                   bl.ToRupiahInReport(), bk.ToRupiahInReport(),(bl+bk).ToRupiahInReport(),
                                   ul.ToRupiahInReport(), uk.ToRupiahInReport(),(ul+uk).ToRupiahInReport(),
                                   (gl+gk+bl+bk+ul+uk).ToRupiahInReport(),
                                   (s.Anggaran-(gl+gk+bl+bk+ul+uk)).ToRupiahInReport()
                                   };





                    gridSPJ.Rows.Add(row);
                }
            }

            gridPenerimaanPengeluaran.Rows.Clear();
            List<FungsionalRekening> lstItem = oLogic.GetListPenerimaanPengeluaran();
            List<FungsionalRekening> lstPenerimaanLalu = oLogic.GetPenerimaan(m_idDinas, 0, tanggalAwal, tanggalAKhir);
            string[] rowy = { "", "P E N E R I M A A N" };
            gridPenerimaanPengeluaran.Rows.Add(rowy);


            if (lstPenerimaanLalu != null)
            {


                foreach (FungsionalRekening i in lstItem)
                {
                    gl = 0L;
                    gk = 0L;
                    bl = 0L;
                    bk = 0L;
                    ul = 0L;
                    uk = 0L;

                    foreach (FungsionalRekening s in lstPenerimaanLalu)
                    {



                        if (s.IDRekening == i.IDRekening)
                        {
                            gl = gl + s.GL;
                            gk = gk + s.GK;
                            bl = bl + s.BL;

                            bk = bk + s.BJk;
                            ul = ul + s.UL;
                            uk = uk + s.UK;

                        }
                    }
                    string[] rowx = { i.IDRekening.ToString(),i.Uraian,
                        gl.ToRupiahInReport(), gk.ToRupiahInReport(),(gl+gk).ToRupiahInReport(),
                        bl.ToRupiahInReport(), bk.ToRupiahInReport(),(bl+bk).ToRupiahInReport(),
                        ul.ToRupiahInReport(), uk.ToRupiahInReport(),(ul+uk).ToRupiahInReport(),
                                   (gl+gk+bl+bk+ul+uk).ToRupiahInReport()
                        };


                    Tgl = Tgl + gl;
                    Tgk = Tgk + gk;
                    Tbl = Tbl + bl;
                    Tbk = Tbk + bk;
                    Tul = Tul + ul;
                    Tuk = Tuk + uk;



                    gridPenerimaanPengeluaran.Rows.Add(rowx);



                }

            }
            string[] rowjP = { "","Jumlah Penerimaan",
                        Tgl.ToRupiahInReport(), Tgk.ToRupiahInReport(),(Tgl+Tgk).ToRupiahInReport(),
                        Tbl.ToRupiahInReport(), Tbk.ToRupiahInReport(),(Tbl+Tbk).ToRupiahInReport(),
                        Tul.ToRupiahInReport(), Tuk.ToRupiahInReport(),(Tul+Tuk).ToRupiahInReport(),
                                   (Tgl+Tgk+Tbl+Tbk+Tul+Tuk).ToRupiahInReport()};

            gridPenerimaanPengeluaran.Rows.Add(rowjP);

            List<FungsionalRekening> lstPengeluaran = oLogic.GetPengeluaran(m_idDinas, 0, tanggalAwal, tanggalAKhir);



            string[] rowy2 = { "", "P E N G E L U A R A N " };
            gridPenerimaanPengeluaran.Rows.Add(rowy2);


            if (lstPengeluaran != null)
            {


                foreach (FungsionalRekening i in lstItem)
                {
                    gl = 0L;
                    gk = 0L;
                    bl = 0L;
                    bk = 0L;
                    ul = 0L;
                    uk = 0L;

                    foreach (FungsionalRekening s in lstPengeluaran)
                    {



                        if (s.IDRekening == i.IDRekening)
                        {
                            gl = gl + s.GL;
                            gk = gk + s.GK;
                            bl = bl + s.BL;

                            bk = bk + s.BJk;
                            ul = ul + s.UL;
                            uk = uk + s.UK;

                        }

                    }
                    string[] rowx = { i.IDRekening.ToString(),i.Uraian,
                        gl.ToRupiahInReport(), gk.ToRupiahInReport(),(gl+gk).ToRupiahInReport(),
                        bl.ToRupiahInReport(), bk.ToRupiahInReport(),(bl+bk).ToRupiahInReport(),
                        ul.ToRupiahInReport(), uk.ToRupiahInReport(),(ul+uk).ToRupiahInReport(),
                                   (gl+gk+bl+bk+ul+uk).ToRupiahInReport()
                        };

                    gridPenerimaanPengeluaran.Rows.Add(rowx);

                    TglK = TglK + gl;
                    TgkK = TgkK + gk;
                    TblK = TblK + bl;
                    TbkK = TbkK + bk;
                    TulK = TulK + ul;
                    TukK = TukK + uk;








                }
            }
            string[] rowj = { "","Jumlah Pengeluaran",
                        TglK.ToRupiahInReport(), TgkK.ToRupiahInReport(),(TglK+TgkK).ToRupiahInReport(),
                        TblK.ToRupiahInReport(), TbkK.ToRupiahInReport(),(TblK+TbkK).ToRupiahInReport(),
                        TulK.ToRupiahInReport(), TukK.ToRupiahInReport(),(TulK+TukK).ToRupiahInReport(),
                                   (TglK+TgkK+TblK+TbkK+TulK+TukK).ToRupiahInReport()};

            gridPenerimaanPengeluaran.Rows.Add(rowj);

            string[] rowtj = { "","Saldo",
                        (Tgl-TglK).ToRupiahInReport(), (Tgk-TgkK).ToRupiahInReport(),(Tgl+Tgk -(TglK+TgkK)).ToRupiahInReport(),
                        (Tbl-TblK).ToRupiahInReport(), (Tbk-TbkK).ToRupiahInReport(),(Tbl+Tbk -(TblK+TbkK)).ToRupiahInReport(),
                        (Tul-TulK).ToRupiahInReport(), (Tuk-TukK).ToRupiahInReport(),(Tul+Tuk -(TulK+TukK)).ToRupiahInReport(),
                                   (Tgl+Tgk+Tbl+Tbk+Tul+Tuk - (TglK+TgkK+TblK+TbkK+TulK+TukK)).ToRupiahInReport()};
            gridPenerimaanPengeluaran.Rows.Add(rowtj);



            /*
}
else
{
MessageBox.Show("Kesalahan memanggil data Anggaran....");

}
*/
        }

        private void cmdCetak_Click(object sender, System.EventArgs e)
        {
          
            ParameterLaporanBKU parameter = new ParameterLaporanBKU();
            SKPDLogic oSKPDLogic = new SKPDLogic(GlobalVar.TahunAnggaran);
            PemdaLogic oPemdaLogic = new PemdaLogic(GlobalVar.TahunAnggaran);
         
            SKPD oSKPD = oSKPDLogic.GetByID(m_idDinas, true);
            Pemda oPemda = new Pemda();
            SPP oSPP = new SPP();
            //SPPLogic oSPPLogic = new SPPLogic(GlobalVar.TahunAnggaran);
            //oSPP = oSPPLogic.GetByID(m_NoUrut, true );
            oPemda = oPemdaLogic.Get();

            parameter.Skpd = oSKPD;
            parameter.Height = 8.5;
            parameter.Width = 14;

            parameter.PEMDA = oPemda;

          
        }

        private void ctrlSPJFungsional_Resize(object sender, System.EventArgs e)
        {
            gridSPJ.Left = 0;
            gridSPJ.Top = 0;
            gridSPJ.Size = tabPage2.Size;
            gridPenerimaanPengeluaran.Top = 0;
            gridPenerimaanPengeluaran.Left = 0;

            gridPenerimaanPengeluaran.Size = tabPage3.Size;


        }
    }
}
