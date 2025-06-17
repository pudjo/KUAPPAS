using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using DTO;
using BP;
using Formatting;
using DataAccess;
namespace BP
{
    public class BulanLogic:BP
    {
        public BulanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;

        }
        public List<Bulan> Get()
        {
            List<Bulan> _lst = new List<Bulan>();
            try
            {


                SSQL = "SELECT * FROM Bulan Order BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Bulan()
                                {
                                    ID = DataFormat.GetSingle(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["Nama"])                                    
                                    
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
