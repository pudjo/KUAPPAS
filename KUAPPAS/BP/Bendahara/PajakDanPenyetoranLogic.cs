using DTO.Bendahara;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

using Formatting;
using BP;
using System.Data;
using System.Data.OleDb;
namespace BP.Bendahara
{
    public class PajakDanPenyetoranLogic:BP
    {

        public PajakDanPenyetoranLogic(int tahun)
            : base(tahun)
        {

        }
        public List<PajakdanPenyetoran> GetPajakPenyetoran(int iddinas, DateTime tanggalAwal, DateTime tanggalAkhir, int iStatus)
        {
            List<PajakdanPenyetoran> lst = new List<PajakdanPenyetoran>();
            try
            {
                SSQL = "select vwpanjarPotongan.IDDinas,vwpanjarPotongan.inourut as NoUrutBelanja, vwpanjarPotongan.sNoBukti as NoBuktiBelanja," +
                    " vwpanjarPotongan.btKOdeUK as KodeUK,vwpanjarPotongan.btIDBank as bank,vwpanjarPotongan.IIDRekening as RekeningPungut," +
                    " vwpanjarPotongan.dtBukukas as TanggalBelanja,vwpanjarPotongan.sUraian as KeteranganBelanja,vwpanjarPotongan.IIDRekening as RekeningPungut," +
                    " vwpanjarPotongan.sNamaPotongan as NamaRekeningPungut,vwpanjarPotongan.cJumlah as JumlahPungut, " +
                    " vwSetorPajak.iNoUrut as NoUrutSetor,vwSetorPajak.sNoBukti as NoBuktiSetor, " +
                    " vwSetorPajak.dtBukuKas as TanggalSetor,vwSetorPajak.cJumlah as JumlahSetor," +
                    " vwSetorPajak.sKeterangan as KeteranagSetor,vwSetorPajak.sNamaPotongan as NamaRekeningSetor," +
                    " vwSetorPajak.iIDRekening as IdRekeningSetor,vwSetorPajak.sNoNTPN, vwSetorPajak.KodeBilling from vwpanjarPotongan INNER JOIN vwSetorPajak  on vwpanjarPotongan.iNoSetorPajak = vwSetorPajak.iNoUrut " +
                    " and vwpanjarPotongan.iddinas = vwSetorPajak.IDDInas and vwpanjarPotongan.IIDRekening = vwSetorPajak.iIDRekening ";
                SSQL = SSQL + " WHERE vwpanjarPotongan.IDDInas =@DINAS and vwpanjarPotongan.dtBukukas between @TANGGALAWAL AND @TANGGALAKHIR ";
                
                if (iStatus > 0)
                {
                    if (iStatus == 1)
                    {
                        SSQL = SSQL + " AND isnull(vwSetorPajak.inourut,0)=0";
                    }
                    else
                    {
                        SSQL = SSQL + " AND isnull(vwSetorPajak.inourut,0)>0";
                    }


                }
                //belum setor 
                SSQL = SSQL + " UNION select vwpanjarPotongan.IDDinas,vwpanjarPotongan.inourut as NoUrutBelanja, vwpanjarPotongan.sNoBukti as NoBuktiBelanja," +
                    " vwpanjarPotongan.btKOdeUK as KodeUK,vwpanjarPotongan.btIDBank as bank,vwpanjarPotongan.IIDRekening as RekeningPungut," +
                    " vwpanjarPotongan.dtBukukas as TanggalBelanja,vwpanjarPotongan.sUraian as KeteranganBelanja,vwpanjarPotongan.IIDRekening as RekeningPungut," +
                    " vwpanjarPotongan.sNamaPotongan as NamaRekeningPungut,vwpanjarPotongan.cJumlah as JumlahPungut, " +
                    " vwSetorPajak.iNoUrut as NoUrutSetor,vwSetorPajak.sNoBukti as NoBuktiSetor, " +
                    " vwSetorPajak.dtBukuKas as TanggalSetor,vwSetorPajak.cJumlah as JumlahSetor," +
                    " vwSetorPajak.sKeterangan as KeteranagSetor,vwSetorPajak.sNamaPotongan as NamaRekeningSetor," +
                    " vwSetorPajak.iIDRekening as IdRekeningSetor,vwSetorPajak.sNoNTPN, vwSetorPajak.KodeBilling from vwpanjarPotongan LEFT JOIN vwSetorPajak  on vwpanjarPotongan.iNoSetorPajak = vwSetorPajak.iNoUrut " +
                    " and vwpanjarPotongan.iddinas = vwSetorPajak.IDDInas and vwpanjarPotongan.IIDRekening = vwSetorPajak.iIDRekening ";
                SSQL = SSQL + " WHERE vwpanjarPotongan.IDDInas =@DINAS and vwpanjarPotongan.dtBukukas between @TANGGALAWAL AND @TANGGALAKHIR and vwSetorPajak.inourut is null ";

                if (iStatus > 0)
                {
                    if (iStatus == 1)
                    {
                        SSQL = SSQL + " AND isnull(vwSetorPajak.inourut,0)=0";
                    }
                    else
                    {
                        SSQL = SSQL + " AND isnull(vwSetorPajak.inourut,0)>0";
                    }


                }
                SSQL = SSQL + " UNION select vwpanjarPotongan.IDDinas,vwpanjarPotongan.inourut as NoUrutBelanja, vwpanjarPotongan.sNoBukti as NoBuktiBelanja," +
                    " vwpanjarPotongan.btKOdeUK as KodeUK,vwpanjarPotongan.btIDBank as bank,vwpanjarPotongan.IIDRekening as RekeningPungut," +
                    " vwpanjarPotongan.dtBukukas as TanggalBelanja,vwpanjarPotongan.sUraian as KeteranganBelanja,vwpanjarPotongan.IIDRekening as RekeningPungut," +
                    " vwpanjarPotongan.sNamaPotongan as NamaRekeningPungut,vwpanjarPotongan.cJumlah as JumlahPungut, " +
                    " vwSetorPajak.iNoUrut as NoUrutSetor,vwSetorPajak.sNoBukti as NoBuktiSetor, " +
                    " vwSetorPajak.dtBukuKas as TanggalSetor,vwSetorPajak.cJumlah as JumlahSetor," +
                    " vwSetorPajak.sKeterangan as KeteranagSetor,vwSetorPajak.sNamaPotongan as NamaRekeningSetor," +
                    " vwSetorPajak.iIDRekening as IdRekeningSetor,vwSetorPajak.sNoNTPN, vwSetorPajak.KodeBilling from vwpanjarPotongan RIGHT JOIN vwSetorPajak  on vwpanjarPotongan.iNoSetorPajak = vwSetorPajak.iNoUrut " +
                    " and vwpanjarPotongan.iddinas = vwSetorPajak.IDDInas and vwpanjarPotongan.IIDRekening = vwSetorPajak.iIDRekening ";
                SSQL = SSQL + " WHERE vwSetorPajak.IDDInas =@DINAS and vwSetorPajak.dtBukukas between @TANGGALAWAL AND @TANGGALAKHIR and vwpanjarPotongan.inourut is null ";

                if (iStatus > 0)
                {
                    if (iStatus == 1)
                    {
                        SSQL = SSQL + " AND isnull(vwSetorPajak.inourut,0)=0";
                    }
                    else
                    {
                        SSQL = SSQL + " AND isnull(vwSetorPajak.inourut,0)>0";
                    }


                }


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", iddinas));
                paramCollection.Add(new DBParameter("@TANGGALAWAL", tanggalAwal));
                paramCollection.Add(new DBParameter("@TANGGALAKHIR", tanggalAkhir));




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {

                    
                    if (dt.Rows.Count > 0)
                        {
                            lst = (from DataRow dr in dt.Rows
                                    select new PajakdanPenyetoran() 
                        {
                            idDinas = DataFormat.GetInteger(dr["IDDinas"]),
                            KodeUK = DataFormat.GetInteger(dr["KodeUK"]),
                            iBank = DataFormat.GetInteger(dr["bank"]),

                            inourutPanjar = DataFormat.GetLong(dr["NoUrutBelanja"]),
                            inourutSetor = DataFormat.GetLong(dr["NoUrutSetor"]),

                            NoBuktiBelanja = DataFormat.GetString(dr["NoBuktiBelanja"]),
                            NoBuktiSetor = DataFormat.GetString(dr["NoBuktiSetor"]),
                            TanggalBelanja = DataFormat.GetDateTime(dr["TanggalBelanja"]),
                            TanggalSetor = DataFormat.GetDateTime(dr["TanggalSetor"]),
                            KeteranganBelanja = DataFormat.GetString(dr["KeteranganBelanja"]),
                            KeterangabSetor = DataFormat.GetString(dr["KeteranagSetor"]),
                            idRekeningBelanja = DataFormat.GetInteger(dr["RekeningPungut"]),
                            idrekeningSetor=  DataFormat.GetInteger(dr["IdRekeningSetor"]),
                            NamaPungut = DataFormat.GetString(dr["NamaRekeningPungut"]),
                            JumlahPungut = DataFormat.GetDecimal(dr["JumlahPungut"]),
                            JumlahSetor = DataFormat.GetDecimal(dr["JumlahSetor"]),
                            NTPN = DataFormat.GetString(dr["sNoNTPN"]),
                            KodeBIlling = DataFormat.GetString(dr["KodeBilling"]),

                        }).ToList();
                    }
                }



                return lst;

            }
            catch (Exception ex)
            {
                return null;
            }

            
        }
    
    }
}
