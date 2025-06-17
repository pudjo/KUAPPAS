using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using BP;
using System.Data;


namespace BP
{
    public class ProfileRekeningLogic:BP
    {
        public ProfileRekeningLogic(int _pTahun, int profile )
            : base(_pTahun,0,profile)
        {
           
            Tahun = _pTahun;
            m_sNamaTabel = "ProfileRekening";
            CekTable();
        }
        private void CekTable()
        {

            try
            {
                SSQL = "SELECT * FROM ProfileRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (_isError == true)
                {
                    SSQL = "CREATE TABLE ProfileRekening (Kode1 int, Kode2 int, Kode3 int, kode4 int, kode5 int )";
                    _dbHelper.ExecuteNonQuery(SSQL);
                    SSQL = "INSERT INTO ProfileRekening (Kode1 , Kode2 , Kode3, kode4, kode5) values (1,1,1,2,2)";
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE ProfileRekening (Kode1 int, Kode2 int, Kode3 int, kode4 int, kode5 int )";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "INSERT INTO ProfileRekening (Kode1 , Kode2 , Kode3, kode4, kode5) values (1,1,1,2,2)";
                _dbHelper.ExecuteNonQuery(SSQL);

            }

        }
        public List<ProfileRekening> Get()
        {
            List<ProfileRekening> lst = new List<ProfileRekening>();
            try
            {
                SSQL = "SELECT * FROM ProfileRekening";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                       int angkaNol;
                       angkaNol=0;
                       //DataRow dr = dt.Rows[0];                       
                       //oRet=new ProfileRekening()
                       lst = (from DataRow dr in dt.Rows                                
                                select new ProfileRekening()
                                
                                {
                                    Kode1= DataFormat.GetInteger(dr["Kode1"]),
                                    Kode2= DataFormat.GetInteger(dr["Kode2"]),
                                    Kode3= DataFormat.GetInteger(dr["Kode3"]),
                                    Kode4= DataFormat.GetInteger(dr["Kode4"]),
                                    Kode5= DataFormat.GetInteger(dr["Kode5"]),
                                    Kode6= DataFormat.GetInteger(dr["Kode6"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                  //  Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                   // Nama = DataFormat.GetString(dr["Nama"]),
                          
                           //         NumSegment = DataFormat.GetInteger(dr["NumSegment"]),
                                    LEN1 = DataFormat.GetInteger(dr["Kode1"]),
                                    LEN2 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]),
                                    LEN3 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]),
                                    LEN4 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]),
                                    LEN5 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]),
                                    LEN6 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]) + DataFormat.GetInteger(dr["Kode6"]),
                                    
                                    FORMAT1= angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode1"])),
                                    FORMAT2= angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode2"])),
                                    FORMAT3= angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode3"])),
                                    FORMAT4= angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode4"])),
                                    FORMAT5= angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode5"])),
                                    FORMAT6= angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode6"])),
                                    LEN = DataFormat.GetInteger(dr["Kode1"])+ DataFormat.GetInteger(dr["Kode2"])+ 
                                          DataFormat.GetInteger(dr["Kode3"])+DataFormat.GetInteger(dr["Kode4"])+
                                           DataFormat.GetInteger(dr["Kode5"]) + DataFormat.GetInteger(dr["Kode6"])
                                            
                                }).ToList();
                    }
                }
                return lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public ProfileRekening GetByID(int pID)
        {
            ProfileRekening oRet = new ProfileRekening();
            try
            {
                SSQL = "SELECT * FROM ProfileRekening where ID =" + pID.ToString();
                DataTable dt = new DataTable();
                if (Tahun >= 2021)
                {
                    dt = _dbHelper.ExecuteDataTable(SSQL);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            int angkaNol;
                            angkaNol = 0;
                            DataRow dr = dt.Rows[0];
                            oRet = new ProfileRekening()
                            {
                                ID = DataFormat.GetInteger(dr["ID"]),
                                //Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                //Nama = DataFormat.GetString(dr["Nama"]),

                                //NumSegment = DataFormat.GetInteger(dr["NumSegment"]),
                                Kode1 = DataFormat.GetInteger(dr["Kode1"]),
                                Kode2 = DataFormat.GetInteger(dr["Kode2"]),
                                Kode3 = DataFormat.GetInteger(dr["Kode3"]),
                                Kode4 = DataFormat.GetInteger(dr["Kode4"]),
                                Kode5 = DataFormat.GetInteger(dr["Kode5"]),
                                Kode6 = DataFormat.GetInteger(dr["Kode6"]),
                                LEN1 = DataFormat.GetInteger(dr["Kode1"]),
                                LEN2 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]),
                                LEN3 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]),
                                LEN4 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]),
                                LEN5 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]),
                                LEN6 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]) + DataFormat.GetInteger(dr["Kode6"]),

                                FORMAT1 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode1"])),
                                FORMAT2 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode2"])),
                                FORMAT3 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode3"])),
                                FORMAT4 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode4"])),
                                FORMAT5 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode5"])),
                                FORMAT6 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode6"])),
                                LEN = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) +
                                        DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]) + DataFormat.GetInteger(dr["Kode6"])




                            };
                        }

                    }
                 
                    return oRet;
                }
                else
                {
                            int angkaNol;
                            angkaNol = 0;
                            DataRow dr = dt.Rows[0];
                            oRet = new ProfileRekening()
                            {
                                ID = DataFormat.GetInteger(dr["ID"]),
                                //Keterangan = DataFormat.GetString(dr["Keterangan"]),
                                //Nama = DataFormat.GetString(dr["Nama"]),

                                //NumSegment = DataFormat.GetInteger(dr["NumSegment"]),
                                Kode1 = DataFormat.GetInteger(dr["Kode1"]),
                                Kode2 = DataFormat.GetInteger(dr["Kode2"]),
                                Kode3 = DataFormat.GetInteger(dr["Kode3"]),
                                Kode4 = DataFormat.GetInteger(dr["Kode4"]),
                                Kode5 = DataFormat.GetInteger(dr["Kode5"]),
                                Kode6 = DataFormat.GetInteger(dr["Kode6"]),
                                LEN1 = DataFormat.GetInteger(dr["Kode1"]),
                                LEN2 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]),
                                LEN3 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]),
                                LEN4 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]),
                                LEN5 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]),
                                LEN6 = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) + DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]) + DataFormat.GetInteger(dr["Kode6"]),

                                FORMAT1 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode1"])),
                                FORMAT2 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode2"])),
                                FORMAT3 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode3"])),
                                FORMAT4 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode4"])),
                                FORMAT5 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode5"])),
                                FORMAT6 = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["Kode6"])),
                                LEN = DataFormat.GetInteger(dr["Kode1"]) + DataFormat.GetInteger(dr["Kode2"]) +
                                        DataFormat.GetInteger(dr["Kode3"]) + DataFormat.GetInteger(dr["Kode4"]) + DataFormat.GetInteger(dr["Kode5"]) + DataFormat.GetInteger(dr["Kode6"])

                             };
                    }

                    return oRet;
            }
            
              
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oRet;
            }
        }
        
        public bool Simpan(ProfileRekening _profile)
        {      
            try
            {
                
               SSQL="DELETE ProfileRekening";
                
               _dbHelper.ExecuteNonQuery(SSQL);
                
                SSQL = "INSERT INTO ProfileRekening (Kode1 , Kode2 , Kode3, kode4, kode5,Kode6) values (?,?,?,?,?,?)";
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@kode1", _profile.Kode1));
                paramCollection.Add(new DBParameter("@kode2", _profile.Kode2));
                paramCollection.Add(new DBParameter("@kode3", _profile.Kode3));
                paramCollection.Add(new DBParameter("@kode4", _profile.Kode4));
                paramCollection.Add(new DBParameter("@kode5", _profile.Kode5));
                paramCollection.Add(new DBParameter("@kode6", _profile.Kode6));
                

                _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                return true;


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

