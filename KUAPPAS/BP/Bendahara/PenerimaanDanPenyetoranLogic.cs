using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using DTO;
using DTO.Bendahara;

using Formatting;
using BP;
using DataAccess;

namespace BP.Bendahara
{
    public class PenerimaanDanPenyetoranLogic:BP
    {
        public PenerimaanDanPenyetoranLogic(int tahun)
            : base(tahun)
        {

        }
        public List<PenerimaanPenerimaanPenyetoran> GetPenerimaanPenyetoran(int idDinas, DateTime tanggal)
        {
            List<PenerimaanPenerimaanPenyetoran> lst = new List<PenerimaanPenerimaanPenyetoran>();
            try
            {
                SSQL = "SELECT * from dbo.fnPenerimaanPenyetoran(@DINAS,@TANGGAL) A Order by a.dtBukukas,a.NoUrut";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@DINAS", idDinas));
               paramCollection.Add(new DBParameter("@TANGGAL", tanggal,DbType.Date));

               
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new PenerimaanPenerimaanPenyetoran()
                               {

                                   NoUrut = DataFormat.GetLong(dr["NoUrut"]),
                                   NoUrutSetor = DataFormat.GetLong(dr["NoUrutSetor"]),
                                   IDDInas = DataFormat.GetInteger(dr["IdDinas"]),
                                   Tanggal = DataFormat.GetDateTime(dr["dtBukukas"]),
                                   Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                   NoBukti = DataFormat.GetString(dr["NoBukti"]),
                                   IDRekening = DataFormat.GetLong(dr["iIDRekening"]),
                                   Debet = DataFormat.GetInteger(dr["Debet"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                   NamaRekening = DataFormat.GetString (dr["NamaRekening"]),
                                   Jenis = DataFormat.GetInteger(dr["Jenis"]),

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
