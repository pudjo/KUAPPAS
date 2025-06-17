using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DTO;
using DTO.Bendahara;

using Formatting;
using BP;

namespace BP.Bendahara
{
    public class LaporanBendaharaLogic : BP 
    {
        public LaporanBendaharaLogic(int pTahun)
            : base(pTahun)
        {


        }

        public List <LaporanBendahara> GetBukuKas(ref ParameterLaporanBKU p , int Jenis )
        {
            int IDDInas= p.Skpd.ID;
            int ppkd= p.PPKD;
            int jenisBendahara = p.JenisBendahara;
            Periode periode = p.periode;




            List<LaporanBendahara> lst = new List<LaporanBendahara>();
            try
            {
                SSQL = "SELECT isnull(SUM(cJumlah*iDebet),0) AS Jumlah FROM tBKU WHERE iTahun=" + p.Tahun.ToString() + " And btIDBank =" + Jenis.ToString() +
                        " AND dtBukti >= " + periode.TanggalAwalTahun.ToSQLFormat() + "  AND dtBukti < " + periode.TanggalAwal.ToSQLFormat() +
                        " AND IDDInas = " + IDDInas.ToString();
                SSQL = SSQL + " AND btJenisBEndahara = " + jenisBendahara.ToString();
                SSQL = SSQL + " AND bPPKD=" + ppkd.ToString();
                SSQL = SSQL + " AND btIDBank= " + Jenis.ToString();
                object saldo = _dbHelper.ExecuteScalar(SSQL);
                p.SaldoAwal = (decimal)saldo;



                SSQL = "SELECT NoBukti, dtBukti, sUraian, cJumlah, iDebet,inobku FROM tBKU" +
                    " WHERE iTahun=" + p.Tahun.ToString() + " And btIDBank =" + Jenis.ToString() + " And dtBukti BETWEEN " + periode.TanggalAwal.ToSQLFormat() +
                    " AND " + periode.TanggalAkhir.ToSQLFormat() +
                     " AND IDDINAS =  " + IDDInas.ToString();


                SSQL = SSQL + " AND btJenisBEndahara = " + jenisBendahara.ToString();
                SSQL = SSQL + " AND bPPKD=" + ppkd.ToString();
                SSQL = SSQL + " AND btIDBank= " + Jenis.ToString();
                SSQL = SSQL + " and btJenisBendahara>0 ORDER BY tbku.inobku, iDebet DESC";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new LaporanBendahara()
                                {
                                    NoBKU = DataFormat.GetInteger(dr["INOBKU"]),
                                    Tanggal= DataFormat.GetDateTime(dr["dtBukti"]).FormatTanggal(),
                                    Uraian = DataFormat.GetString(dr["sUraian"]),
                                    NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"])
                                    
                                    
                                }).ToList();

                    }

                }
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;


                return null;

            }
        }
    }
}
