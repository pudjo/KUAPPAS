using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using DataAccess;
using Formatting;

namespace BP
{
    class UserFungsiLogic : BP
    {
        public UserFungsiLogic(int _pTahun): base (_pTahun){

        }
        public List<UserFungsi> Get()
        {
            
            List<UserFungsi> _lst = new List<UserFungsi>();
            try
            {
                // mUserFungsi (iAplikasi smallint, Fungsi int, Nama varchar(100))

                SSQL = "SELECT * FROM muserfungsi order By iAplikasi , Fungsi ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UserFungsi()
                                {
                                    Aplikasi = DataFormat.GetInteger(dr["iAplikasi"]),
                                    Fungsi= DataFormat.GetInteger(dr["Fungsi"]),                                    
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
        public List<UserFungsi> GetByAplikasi(Single _iAplikasi)
        {
            List<UserFungsi> _lst = new List<UserFungsi>();
            try
            {
                // mUserFungsi (iAplikasi smallint, Fungsi int, Nama varchar(100))

                SSQL = "SELECT * FROM muserfungsi order By iAplikasi , Fungsi ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UserFungsi()
                                {
                                    Aplikasi = DataFormat.GetInteger(dr["ID"]),
                                    Fungsi = DataFormat.GetInteger(dr["Fungsi"]),
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
        public List<UserFungsi> GetByGroup(Single _iAplikasi, int _Group)
        {
            List<UserFungsi> _lst = new List<UserFungsi>();
            try
            {
                // mUserFungsi (iAplikasi smallint, Fungsi int, Nama varchar(100))

                SSQL = "SELECT * FROM muserfungsi WHERE FUngsi in (Select Fungsi FROM mGroupFungsi WHERE iGroup=" + _Group.ToString() +" and iAplikasi=" + _iAplikasi.ToString() +"   ) order By iAplikasi , Fungsi ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UserFungsi()
                                {
                                    Aplikasi = DataFormat.GetInteger(dr["ID"]),
                                    Fungsi = DataFormat.GetInteger(dr["Fungsi"]),
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
