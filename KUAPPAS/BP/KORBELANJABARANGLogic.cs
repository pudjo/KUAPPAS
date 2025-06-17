using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using DataAccess;
using System.Data;
using System.Data.Sql;
using Formatting;
namespace BP
{
    public class KORBELANJABARANGLogic:BP
    {

        public KORBELANJABARANGLogic(int tahun)
            : base(tahun)
        {

        }

        public List<KORBELANJABARANG> Get()
        {
            List<KORBELANJABARANG> _lst = new List<KORBELANJABARANG>();
            try
            {
                SSQL = "SELECT KORBELANJABARANG.*, mRekening.sNamaRekening FROM KORBELANJABARANG INNER JOIN mRekening ON " +
                    "KORBELANJABARANG.IDRekening= mRekening.IIDRekening";
               
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KORBELANJABARANG()
                                {
                                    IDBarang= DataFormat.GetLong(dr["IDBarang"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Jenis = DataFormat.GetInteger(dr["Jenis"]),
                                    NamaRekening = DataFormat.GetString(dr["sNamaRekening"])

                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
    }
}
