using DataAccess;
using DTO.Bendahara;
using Formatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Bendahara
{
    public class SPJPenerimaanLogic: BP 
    {
        public SPJPenerimaanLogic(int Tahun)
            : base(Tahun)
        {

        }
        public List<SPJPenerimaan> Get(int iddinas, DateTime tanggal)
        {
            List<SPJPenerimaan> lst = new List<SPJPenerimaan>();
            try
            {

                SSQL = "SELECT * from dbo.fnSPJPenerimaan (@IDDINas,@Tanggal) where iidrekening>0 ";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@IDDINas", iddinas));
                paramCollection.Add(new DBParameter("@Tanggal", tanggal,DbType.Date));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new SPJPenerimaan()
                               {
                                   IdDinas = DataFormat.GetInteger(dr["IDDinas"]),
                                   Tanggal = DataFormat.GetDateTime(dr["Tanggal"]),
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   Penerimaan = DataFormat.GetDecimal(dr["Penerimaan"]),
                                   Penyetoran = DataFormat.GetDecimal(dr["Penyetoran"]),

                               }).ToList();


                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }
        }

    }
}


/*

 * CREATE FUNCTION [dbo].[fnSPJPenerimaan] 
(	
	-- Add the parameters for the function here
	@pdinas as int , 
	@ptanggal as DateTime 
)
RETURNS TABLE 
AS
RETURN 
(
select tSTS.IDDInas,tSTS.dtSTS as tanggal , tSTSRekening.iIDRekening, 
tSTSRekening.cJumlah as Penerimaan, 0 as penyetoran
from tSTS inner join tSTSRekening ON tSTS .inourut= tSTSRekening.inourut
WHERE tSTS.IDDInas =@pdinas and tsts.dtSTS <= @ptanggal
UNION 
select tSetor.IDDInas,tSetor.dtBukuKas  as tanggal , tSetorRekening.iIDRekening, 
0 as Penerimaan, tSetorRekening.cJumlah as penyetoran
from tSetor inner join tSetorRekening ON tSetor.inourut= tSetorRekening.inourut
WHERE tSetor.IDDInas =@pdinas and tSetor.btJenis = 1 and tSetor.dtBukuKas  <= @ptanggal


)

*/