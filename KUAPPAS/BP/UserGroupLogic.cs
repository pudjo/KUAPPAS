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
    public class UserGroupLogic : BP
    {

        public UserGroupLogic(int _pTahun)
            : base(_pTahun)
        {

        }

        public List<UserGroup> Get()
        {

            List<UserGroup> _lst = new List<UserGroup>();
            try
            {
                // mUserGroup (iAplikasi smallint, Fungsi int, Nama varchar(100))
                //
            //    //
            //     public Single iAplikasi {set;get;}
            //public int iGroup  {set;get;}
            //public string Nama { set; get; }
                SSQL = "SELECT * FROM mUserGroup order By igROUP";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UserGroup()
                                {
                                    iAplikasi = DataFormat.GetInteger(dr["iAplikasi"]),
                                    iGroup= DataFormat.GetInteger(dr["iGroup"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Status = DataFormat.GetInteger(dr["Status"]),

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
        public List<UserGroup> GetByAplikasi(Single _iAplikasi)
        {
            List<UserGroup> _lst = new List<UserGroup>();
            try
            {
                // mUserGroup (iAplikasi smallint, Fungsi int, Nama varchar(100))

                SSQL = "SELECT * FROM mUserGroup order By iAplikasi ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UserGroup()
                                {
                                    iAplikasi = DataFormat.GetInteger(dr["iAplikasi"]),
                                    iGroup = DataFormat.GetInteger(dr["iGroup"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Status = DataFormat.GetInteger(dr["Status"]),
                                    Keterangan=DataFormat.GetInteger(dr["Status"])==1?"Aktif":(DataFormat.GetInteger(dr["Status"])==0?"Blm aktiv":"Tidak aktif")

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
        public int SImpan(UserGroup ug)
        {

            DBParameterCollection paramCollection = new DBParameterCollection();
           int iGroup  = ug.iGroup ;
            try
            {
                if (ug.iGroup == 0 ){
                iGroup = GetMaxID("iGroup");
                SSQL = "INSERT INTO mUserGroup (iAPlikasi,Nama,Status)  values (@piAPlikasi, @piGroup,@pNama,@pStatus)";


                        paramCollection.Add(new DBParameter("@piAPlikasi", ug.iAplikasi));
                        paramCollection.Add(new DBParameter("@piGroup", iGroup));
                        paramCollection.Add(new DBParameter("@pNama", ug.Nama));
                        paramCollection.Add(new DBParameter("@pStatus",ug.Status));
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                } else {
                    SSQL = "UPDATE mUserGroup SET Nama = @pNama WHERE iGroup =@piGroup";

                    paramCollection.Add(new DBParameter("@pNama", ug.Nama));
                    paramCollection.Add(new DBParameter("@piGroup", ug.iGroup));
                    
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }



                return iGroup;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return 0;
            }
        }
        public bool Acrtivate(UserGroup ug) {

        {
            try{
            DBParameterCollection paramCollection = new DBParameterCollection();
            SSQL = "UPDATE mUserGroup SET Status = 1 WHERE iGroup =@piGroup";
            paramCollection.Add(new DBParameter("@piGroup", ug.iGroup));
                    
            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
              

            return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        }
        public List<UserGroup> GetByGroup(Single _iAplikasi, int _Group)
        {
            List<UserGroup> _lst = new List<UserGroup>();
            try
            {
                // mUserGroup (iAplikasi smallint, Fungsi int, Nama varchar(100))

                SSQL = "SELECT * FROM mUserGroup WHERE FUngsi in (Select Fungsi FROM mGroupFungsi WHERE iGroup=" + _Group.ToString() + " and iAplikasi=" + _iAplikasi.ToString() + "   ) order By iAplikasi , Fungsi ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UserGroup()
                                {
                                    iAplikasi = DataFormat.GetInteger(dr["ID"]),
                                    iGroup = DataFormat.GetInteger(dr["iGroup"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    Status = DataFormat.GetInteger(dr["Status"]),

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
