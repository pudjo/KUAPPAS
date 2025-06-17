using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using DataAccess;
using Formatting;
using DTO.Bendahara;


namespace BP
{
    public class AplikasiLogic:BP
    {

        SPP m_oSPP;
        public AplikasiLogic(int _pTahun)
            : base(_pTahun)
        {
            m_oSPP = new SPP();
        }
        public List<Aplikasi> Get()
        {
            List<Aplikasi> _lst = new List<Aplikasi>();
            try
            {
                SSQL = "SELECT * FROM mUserAplikasi order By ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Aplikasi()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),                                    
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
        public Aplikasi GetByID(int pID)
        {
            Aplikasi _object = new Aplikasi();
            try
            {
                SSQL = "SELECT * FROM mUserAplikasi Where ID =" + pID.ToString();


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    //DataRow dr = null;


                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _object = new Aplikasi()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),                            
                            Nama = DataFormat.GetString(dr["Nama"])
                        };
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _object;
            }


        }
    }
}
