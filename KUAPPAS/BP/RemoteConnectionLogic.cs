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
    public class RemoteConnectionLogic:BP 
    {


        public RemoteConnectionLogic(int _pTahun, int profile)
            : base(_pTahun, 0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "RemoteConnection";
        }
        public RemoteConnection GetByJenis(int Jenis, int Jalur)
        {
            RemoteConnection _object = new RemoteConnection();
            try
            {

                SSQL = "SELECT * FROM RemoteConnection  WHere Jenis = " + Jenis.ToString() + " AND Jalur=" + Jalur.ToString() ;//+ " AND Tahun ;


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;


                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        _object = new RemoteConnection()
                        {
                            Server = DataFormat.GetString(dr["sServer"]),
                            Database = DataFormat.GetString(dr["sDatabase"]),
                            Password = DataFormat.GetString(dr["sPassword"]),
                            Jenis = DataFormat.GetInteger(dr["Jenis"]),
                            UserID = DataFormat.GetString(dr["sUser"]),

                        };
                    }
                    else
                    {
                        _object = null;
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }


        }
        public RemoteConnection GetDecryptedByJenis(int Jenis, string key,int Jalur )
        {
            RemoteConnection _object = new RemoteConnection();
            try
            {

                SSQL = "SELECT * FROM RemoteConnection  WHere Jenis = " + Jenis.ToString() + " AND Jalur=" + Jalur.ToString();//+ " AND Tahun ;


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;


                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        _object = new RemoteConnection()
                        {
                            Server = DataFormat.GetString(dr["sServer"]),
                            Database = DataFormat.GetString(dr["sDatabase"]),
                            Password = DataFormat.GetString(dr["sPassword"]),
                            Jenis = DataFormat.GetInteger(dr["Jenis"]),
                            UserID = DataFormat.GetString(dr["sUser"]),
                            Jalur = DataFormat.GetInteger(dr["Jalur"]),
                            

                        };
                    }
                    else
                    {
                        _object = null;
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }


        }
        public bool Simpan(ref RemoteConnection rc)
        {
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                    
                if (GetByJenis(rc.Jenis,rc.Jalur) == null)
                {

                    SSQL = "INSERT INTO RemoteConnection(sServer, sDatabase, sPassword,sUser,Jenis,jalur) values (" +
                            "@pServer, @pDatabase, @pPassword,@pUser,@pJenis,@pjalur)";

                    paramCollection.Add(new DBParameter("@pServer", rc.Server));
                    paramCollection.Add(new DBParameter("@pDatabase", rc.Database));
                    paramCollection.Add(new DBParameter("@pPassword", rc.Password));
                    paramCollection.Add(new DBParameter("@pUser", rc.UserID));
                    paramCollection.Add(new DBParameter("@pJenis", rc.Jenis));
                    paramCollection.Add(new DBParameter("@pjalur", rc.Jalur));

                }
                else
                {

                    SSQL = "UPDATE RemoteConnection SET sServer=@pServer, sDatabase=@pDatabase, sPassword=@pPassword,sUser=@sUser WHERE Jenis=@pJenis AND Jalur =@pJalur";
                    paramCollection.Add(new DBParameter("@pServer", rc.Server));
                    paramCollection.Add(new DBParameter("@pDatabase", rc.Database));
                    paramCollection.Add(new DBParameter("@pPassword", rc.Password));
                    paramCollection.Add(new DBParameter("@sUser", rc.UserID));
                    paramCollection.Add(new DBParameter("@pJenis", rc.Jenis));
                    paramCollection.Add(new DBParameter("@pJalur", rc.Jalur));



                }
                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
    }
}
