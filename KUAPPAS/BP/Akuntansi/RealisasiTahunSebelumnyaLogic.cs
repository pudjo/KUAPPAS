using DataAccess;
using DTO;
using DTO.Akuntansi;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BP.Akuntansi
{
    public class RealisasiTahunSebelumnyaLogic:BP
    {
        public RealisasiTahunSebelumnyaLogic(int tahun) : base(tahun)
        {

        }
        public bool Simpan(List<RealisasiTahunSebelumnya> lst)
        {
            try
            {
                SSQL = " DELETE RealisasiTahunsebelumnya  where iTahun= " + Tahun.ToString();
                _dbHelper.ExecuteDataTable(SSQL);
                DBParameterCollection paramCollection = new DBParameterCollection();
                foreach (RealisasiTahunSebelumnya rb in lst)
                {
                    SSQL = "INSERT INTO RealisasiTahunsebelumnya  (iTahun,iidrekening,Jumlah) values " +
                         "(@Tahun,@IDRekening,@Jumlah)";
                    paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@Tahun", rb.Tahun));
                    paramCollection.Add(new DBParameter("@IDRekening", rb.IDRekening));
                    paramCollection.Add(new DBParameter("@Jumlah", rb.Jumlah));
                    _dbHelper.ExecuteDataTable(SSQL, paramCollection);




                }

                return true;
            }
            catch(Exception ex)
            {
                _lastError = ex.Message;
                return false ;

            }
        }

    }
}
