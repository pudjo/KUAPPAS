using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using Formatting;
using System.Data;
using DataAccess;

namespace BP
{
    public class PenggunaLogic:BP
    {
        public PenggunaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPengguna";
          //  CekPPKDOnTable();
           // ChangeDataType();
        }
        private void ChangeDataType()
        {
            try
            {
                SSQL = " ALTER TABLE mPengguna ALTER COLUMN isuserdinas tinyint";                
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            catch (Exception ex)
            {
                _lastError = "Gagal Rubah Type Data.." + ex.Message;
            }
        }
        private void CekPPKDOnTable()
        {
            try
            {
                SSQL = " SELECT bPPKD from mPengguna ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                return;
            }
            catch (Exception ex)
            {
                SSQL = " ALTER TABLE " + m_sNamaTabel + " ADD bPPKD tinyint ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " UPDATE " + m_sNamaTabel + " SET bPPKD =0";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                
            }

        }
        public List<Pengguna> Get(int kelompokPengguna)
        {

            List<Pengguna> _lst = new List<Pengguna>();
            try
            {
                SSQL = "SELECT ID,Nama,SKPD ,Password,Password2,NIK,Status,Level,Kelompok,UserID,bPPKD from mPengguna ";
                if (kelompokPengguna != (int)Otoritas.CON_OTORITAS_ADMIN)
                { 
                    SSQL = SSQL + "WHERE Kelompok <> 1000 ";
                } 

                SSQL=SSQL + " ORDER BY Nama";

                

                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Pengguna()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                  
                                    NIK  =DataFormat.GetString(dr["NIK"]),
                                    Status = DataFormat.GetSingle(dr["Status"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    UserID = DataFormat.GetString(dr["UserID"]),
                                    PPKD= DataFormat.GetSingle(dr["bPPKD"])>0?true:false,
                                    Kelompok =DataFormat.GetSingle(dr["Kelompok"]),
                                    SKPD =DataFormat.GetInteger(dr["SKPD"]),

                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PenggunaGroup> GetGroupByUserID(int pID)
        {
            List<PenggunaGroup> lst = new List<PenggunaGroup>();
            try
            {
                SSQL = "SELECT UserID,[Group] from PenggunaGroup where UserID =" + pID.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new PenggunaGroup()
                               {
                                   UserID = DataFormat.GetInteger(dr["UserID"]),
                                   Group = DataFormat.GetInteger(dr["Group"]),

                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }


        }
        public List<PenggunaDinas> GetDinasByUserID(int pID)
        {
            List<PenggunaDinas> lst = new List<PenggunaDinas>();
            try
            {
                SSQL = "SELECT IDDinas,Unit,UserID,PPKD from mPenggunaDinas where UserID =" + pID.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new PenggunaDinas()
                               {
                                   UserID = DataFormat.GetInteger(dr["UserID"]),
                                   SKPD = DataFormat.GetInteger(dr["IDDInas"]),
                                   Unit = DataFormat.GetInteger(dr["Unit"]),
                                   PPKD = DataFormat.GetInteger(dr["PPD"])

                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }


        }
        public Pengguna GetByID( int pID)
        {
            Pengguna _object = new Pengguna();
            try
            {
                
               SSQL = "SELECT * FROM mPengguna Where ID =" + pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    
                    
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];

                        _object = new Pengguna()
                                {
                                     ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                     Password2 = DataFormat.GetString(dr["Password2"]),
                                     Password = DataFormat.GetString(dr["Password"]),
                            
                                    NIK = DataFormat.GetString(dr["NIK"]),
                                    Status = DataFormat.GetSingle(dr["Status"]),
                                    UserID = DataFormat.GetString(dr["UserID"]),
                                    IsUserDinas = DataFormat.GetSingle(dr["IsUSerDinas"]),
                                    Level = DataFormat.GetSingle(dr["Level"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    PPKD = DataFormat.GetSingle(dr["bPPKD"]) > 0 ? true : false,
                                     Kelompok = DataFormat.GetSingle(dr["Kelompok"]),
                                     lstDinas = GetDinasByUserID(DataFormat.GetInteger(dr["ID"])),
                                     lstGroup = GetGroupByUserID(DataFormat.GetInteger(dr["ID"])),


                
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
        public Pengguna GetByUserID(string sUserID)
        {
            Pengguna _object = new Pengguna();
            _isError = false;
            try
            {

                SSQL = "SELECT * FROM mPengguna Where UserID=@USERNAME and Status<9";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@USERNAME", sUserID));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        _object = new Pengguna()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            Nama = DataFormat.GetString(dr["Nama"]),
                            Password = DataFormat.GetString(dr["Password"]),
                            NIK = DataFormat.GetString(dr["NIK"]),
                            Status = DataFormat.GetSingle(dr["Status"]),
                            UserID = DataFormat.GetString(dr["UserID"]),
                            IsUserDinas = DataFormat.GetSingle(dr["IsUSerDinas"]),
                            Level = DataFormat.GetSingle(dr["Level"]),
                            SKPD = DataFormat.GetInteger(dr["SKPD"]),
                            Kelompok =DataFormat.GetSingle(dr["Kelompok"]),
                            KodeUK = DataFormat.GetInteger(dr["KodeUK"]),
                            PPKD = DataFormat.GetSingle(dr["bPPKD"]) > 0 ? true : false,
                            lstDinas = GetDinasByUserID(DataFormat.GetInteger(dr["ID"])),
                            lstGroup = GetGroupByUserID(DataFormat.GetInteger(dr["ID"])),
                            Sumber=1,

                        };
                        return _object;
                    }  
                }
                _lastError = "Pengguna tidak ditemukan";
return null;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }


        }

       
       public bool Activate(Pengguna _pPengguna)
       {
           try
           {
               SSQL = "UPDATE mPengguna SET Status=1 WHERE ID=" + _pPengguna.ID.ToString();
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

       public bool SetPassword(Pengguna _pPengguna)
       {
           try
           {
               SSQL = "UPDATE mPengguna SET Password='" + _pPengguna.Password + "' WHERE ID=" + _pPengguna.ID.ToString();
               _dbHelper.ExecuteNonQuery(SSQL);
               return true;
           }catch(Exception ex){
               _isError = true;
               _lastError = ex.Message;
               return false;
           }
           

       }
       public bool SetNewStatus(Pengguna _pPengguna)
       {
           try
           {
               SSQL = "UPDATE mPengguna SET Status='" + _pPengguna.Status  + "' WHERE ID=" + _pPengguna.ID.ToString();

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
       public bool SetPassword2(Pengguna _pPengguna)
       {
           try
           {
               SSQL = "UPDATE mPengguna SET Password='" + _pPengguna.Password + "', Status=1 WHERE ID=" + _pPengguna.ID.ToString();
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
       public bool SetUserID(Pengguna _pPengguna)
       {
           try
           {
               SSQL = "UPDATE mPengguna SET UserID='" + _pPengguna.UserID + "' WHERE ID=" + _pPengguna.ID.ToString();
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
       
        public bool Simpan(ref Pengguna _pPengguna)
        {
            int _pID;
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (_pPengguna.ID== 0)
                {
                    _pID = GetMaxID() + 1;

                    SSQL = "INSERT INTO mPengguna(ID,Nama ,Status,IsUserDinas ,NIK,UserID,Level,SKPD, bPPKD,Kelompok,KodeUK) values (" +
                        " @pID, @pNama ,@pStatus ,@pIsUserDinas ,@pNIK,@pUserID,@pLevel,@pSKPD,@pbPPKD,@pKelompok,@KodeUK)";

                 

                    paramCollection.Add(new DBParameter("@pID", _pID,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNama", _pPengguna.Nama,DbType.String));
                    paramCollection.Add(new DBParameter("@pStatus", _pPengguna.Status,DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIsUserDinas", _pPengguna.IsUserDinas,DbType.Int16));
                    paramCollection.Add(new DBParameter("@pNIK", _pPengguna.NIK, DbType.String));
                    paramCollection.Add(new DBParameter("@pUserID", _pPengguna.UserID));
                    paramCollection.Add(new DBParameter("@pLevel", _pPengguna.Level));
                    paramCollection.Add(new DBParameter("@pSKPD", _pPengguna.SKPD));
                    paramCollection.Add(new DBParameter("@pbPPKD", _pPengguna.PPKD == true ? 1 : 0));
                    paramCollection.Add(new DBParameter("@pKelompok", _pPengguna.Kelompok));
                    paramCollection.Add(new DBParameter("@KodeUK", _pPengguna.KodeUK));

                }
                else
                {
                    _pID = _pPengguna.ID;
                    SSQL = "UPDATE mPengguna SET Nama =@pNama,Status =@pStatus ,IsUserDinas=@pIsUserDinas,UserID=@pUserID , " +
                            " NIK=@pNIK,Level=@pLevel,SKPD=@pSKPD,bPPKD=@pbPPKD,Kelompok=@pKelompok,KodeUK=@KodeUK WHERE ID=@pID";
                
                    paramCollection.Add(new DBParameter("@pNama", _pPengguna.Nama));
                    paramCollection.Add(new DBParameter("@pStatus", _pPengguna.Status));
                    paramCollection.Add(new DBParameter("@pIsUserDinas", _pPengguna.IsUserDinas));
                    paramCollection.Add(new DBParameter("@pNIK", _pPengguna.NIK));
                    paramCollection.Add(new DBParameter("@pUserID", _pPengguna.UserID));
                    paramCollection.Add(new DBParameter("@pLevel", _pPengguna.Level));
                    paramCollection.Add(new DBParameter("@pSKPD", _pPengguna.SKPD));
                    paramCollection.Add(new DBParameter("@KodeUK", _pPengguna.KodeUK));
                    paramCollection.Add(new DBParameter("@pbPPKD", _pPengguna.PPKD == true ? 1 : 0));
                    paramCollection.Add(new DBParameter("@pKelompok", _pPengguna.Kelompok));
                    
                    paramCollection.Add(new DBParameter("@pID", _pID));


                }

                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                _pPengguna.ID = _pID;
                return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public List<Pengguna> GetPengguna(int _pPengguna)
        {
            return null;
        }
        public bool Hapus(int _pIDPengguna)
        {
            try
            {
                SSQL = "DELETE mPengguna WHERE ID = @pID";
               //SSQL = "UPDATE mPengguna SET status =9 WHERE ID = @pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDPengguna));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            } catch(Exception ex){
                _lastError = ex.Message;
                _isError = true;
                return false;
            }


        }
        private int GetMaxID()
        {
            int _maxID=0;
            try
            {
                SSQL = "SELECT max(ID) as MAXID FROM mPengguna ";                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        _maxID = DataFormat.GetInteger(dr["MAXID"]);

                      
                    }
                }
                return _maxID;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }
        }
    }
}
