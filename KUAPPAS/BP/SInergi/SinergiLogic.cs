using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Data;
using DTO.Laporan;
using Formatting;
namespace BP.Sinergi
{
    public class SinergiLogic:BP
    {

        public SinergiLogic(int _pTahun)
            : base(_pTahun)
        {
          
        }
        public DataTable ExportDTA(int bulan)
        {
            try
            {
                SSQL = "select *  from vwRTHUntukSinergi2024TabBelnja";

                DataTable dt = new DataTable();
                int clumnCount = 0;
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        public DataTable ExportPajak(int bulan)
        {
            try
            {
                SSQL = "SELECT A.sNoBukti as spmNumber,A.sNoSP2D as sp2dNumber ," +
                        "B.IIDRekeningPotongan as kodeAkunPajak,mPotongan.sNamaPotongan as namaAkunPajak," +
                        "mPotongan.idKodePusat as jenisPajak,'' as ntpn,b.cJumlah as nilaiPotongan " +
                        " from tSPP A INNER JOIN tSPPPotongan B On A.inourut = B.iNoUrut " +
                         "INNER JOIN mPotongan  ON B.iIDRekeningPotongan = mPotongan.iIDRekeningPotongan " +
                        " where  A.iTahun = 2024 and A.iStatus in (1,3,4)   and btJenis >=1";
                        
                DataTable dt = new DataTable();
     
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;

            }
           
        }
         public List<CetakanDTH>GetCetakanDTH(){
                try{
                    List<CetakanDTH> lst = new List<CetakanDTH>();
                    SSQL="select kodeSKPD, NamaSKPD , count( distinct sp2dNumber) as JumlahSPM,"+
                        " sum(nilai) as Belanja, count( distinct sp2dNumber) as JumlahSP2D,"+
                        " sum(nilai) as BelanjaTotal, sum(nilaiToTalPajak) as TotalPotongan from tDTH group by kodeSKPD,NamaSKPD "+
                        " order by kodeSKPD ";

           
                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            lst = (from DataRow dr in dt.Rows
                                    select new CetakanDTH()
                                    {

                                        KodeSKPD = DataFormat.GetString(dr["kodeSKPD"]),
                                        NamaSKPD = DataFormat.GetString(dr["NamaSKPD"]),
                                        JumlahSPM = DataFormat.GetInteger(dr["JumlahSPM"]),
                                        Belanja = DataFormat.GetDecimal(dr["Belanja"]),
                                        JumlahSP2D = DataFormat.GetInteger(dr["JumlahSP2D"]),
                                        BelanjaTotal = DataFormat.GetDecimal(dr["BelanjaTotal"]),
                                        TotalPotongan = DataFormat.GetDecimal(dr["TotalPotongan"]),
                                        Keterangan = "",

                        

                                    }).ToList();
                        }
                    }
                    return lst;
                  

                }
                catch (Exception ex)
                {
                    _isError = true;
                    _lastError = ex.Message;
                    return null;
                }
            }
         public bool BetulkanBTBT()
         {
             try
             {
                 SSQL=" update tJurnalRekening set btKodekategori= substring(Cast (iddinas as varchar(7)),1,1) ,"+
                " btKodeUrusan = substring(Cast (iddinas as varchar(7)),2,2)  ,"+
                " btKodeSKPD = substring(Cast (iddinas as varchar(7)),4,2)  , "+
                " btKodeKategoriPelaksana=substring(Cast (idurusan   as varchar(12)),1,1)  ,"+
                " btKodeUrusanPelaksana=substring(Cast (idurusan  as varchar(12)),2,2)  ,"+
                " btIdProgram=substring(Cast (idprogram  as varchar(12)),3,2)  ,"+
                " btIDKegiatan=substring(Cast (IDSubKegiatan   as varchar(12)),6,3)  ,"+
                " btIDSubKegiatan=substring(Cast (IDSubKegiatan  as varchar(12)),9,34) "+
                    " from tJurnalRekening "+
                " where btKodekategori=0 or btKodekategori is null and iidrekening like '5%' or iidrekening like '6%' or iidrekening like '8%' ";

                 _dbHelper.ExecuteNonQuery(SSQL);
                 return true;
             }
             catch (Exception ex)
             {
                 _isError = true;
                 _lastError = ex.Message;
                 return false;
             }
         }
    }
}
