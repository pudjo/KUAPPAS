using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Text;
using System.Security;
using KUAPPAS;
//using MySql.Data.MySqlClient;


namespace DataAccess
{
    internal class ConnectionManager
    {
        //** 
        // establish the connection to database
        //888888888888
        
        public IDbConnection GetConnection(int _pTahun, int perbaikan = 1, int kepmen50 = 2)
        {
            SqlConnection connection = null;
            Configuration.Tahun = _pTahun;
            Configuration.PerbaikanAPBD = perbaikan;
            Configuration.KEPMEN050 = kepmen50;

            // some setting in ini file : KUAPPAS.iii
            Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "KUAPPAS.ini");
            

            connection = new SqlConnection();
            try
            {



                SqlConnectionStringBuilder strbldr = new SqlConnectionStringBuilder();
                SecureString securePassword;
          
                //string Type   = ini.IniReadValue("XMAN", "TYPE");
                //string DATABASE = ini.IniReadValue("XMAN", "Database");
                //string SERVER = ini.IniReadValue("XMAN", "Alamat");

           int env = 1;// server ktp
          //int env = 2;//lokal

                if (env == 1)
                {

#if DEBUG
                    
                    if (_pTahun == 2024)
                    {
                        strbldr.InitialCatalog = "KTP2024";
                   

                    }

                    //strbldr.DataSource = "36.92.240.142";// "LAPTOP-FHSK22A0\\SQL2019E";



                    strbldr.DataSource = "LAPTOP-FHSK22A0\\SQL2022";

                    strbldr.TrustServerCertificate = true;
                    strbldr.Encrypt = true;


                    strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
                    connection = new SqlConnection(strbldr.ConnectionString);

                    securePassword = new System.Net.NetworkCredential(" ",
                    AesOperation.DecryptString(GlobalVar.Key, ini.IniReadValue("XMAN", "Server"))).SecurePassword;

                    securePassword = new System.Net.NetworkCredential(" ",
                    "unclejo").SecurePassword;


                    securePassword.MakeReadOnly();
                  //  var credentials = new SqlCredential("standartEndpoints", securePassword);
                    var credentials = new SqlCredential("sa", securePassword);
                     connection.Credential = credentials;

                    connection.Open();


#else

                    //string DATABASE = ini.IniReadValue("XMAN", "Database");
                    //string SERVER = ini.IniReadValue("XMAN", "Alamat");

                    //GlobalVar.g_Connection.ConnectionString = "Provider=SQLOLEDB;Data Source=103.178.152.186;Initial Catalog=KKU2024;UID=sa;PWD=AsetKKU2024";

                   // if (Type=="6"){
                           strbldr.DataSource = "103.178.152.186";
                
               
                           strbldr.InitialCatalog = "KTP2024";
         
                            strbldr.TrustServerCertificate = true;
                            strbldr.Encrypt = true;


                            strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
                            connection = new SqlConnection(strbldr.ConnectionString);
                            securePassword = new System.Net.NetworkCredential(" ",
                                "AsetKKU2024").SecurePassword;

                                securePassword.MakeReadOnly();
                            var credentials = new SqlCredential("sa", securePassword);

            
     
                            connection.Credential = credentials;
                            connection.Open();
         //           } else{
         //                strbldr.DataSource = "36.92.240.142";
         //               if (_pTahun == 2025)
         //               {
         //                   strbldr.InitialCatalog = "KTP2025";
                        
         //               }
         //                if (_pTahun == 2024)
         //                {
         //                      strbldr.InitialCatalog = "KTP2024";
            
         //               }

         //               if (_pTahun == 2023)
         //              {
         //                 strbldr.InitialCatalog = "KTP2023Test";
         //              }
         //       strbldr.TrustServerCertificate = true;
         //       strbldr.Encrypt = true;
                        

         //       strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
         //       connection = new SqlConnection(strbldr.ConnectionString);
         //       securePassword = new System.Net.NetworkCredential(" ",
         //           AesOperation.DecryptString(GlobalVar.Key, ini.IniReadValue("XMAN", "Server"))).SecurePassword;
         //securePassword.MakeReadOnly();
         //       var credentials = new SqlCredential("standartEndpoints", securePassword);

       
     
         //       connection.Credential = credentials;
         //       connection.Open();
         //           }
#endif
                }
                else //LOKAL
                {
                    // strbldr.DataSource = "103.28.53.130";// "LAPTOP-FHSK22A0\\SQL2019E";

                    if (_pTahun == 2024)
                    {
                        strbldr.InitialCatalog = "KTP2024";
                    
                    }
                    if (_pTahun == 2023)
                    {
                        //    strbldr.InitialCatalog = "KTP2023";
                        strbldr.InitialCatalog = "KTP2023Test";

                    }

                  
                    //strbldr.DataSource =  "LAPTOP-FHSK22A0\\SQL2022";
                    strbldr.TrustServerCertificate = true;
                    strbldr.Encrypt = true;


                    strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
                    connection = new SqlConnection(strbldr.ConnectionString);


                    //securePassword = new System.Net.NetworkCredential(" ",
                    //                    "unclejo").SecurePassword;

                    securePassword = new System.Net.NetworkCredential(" ",
                    AesOperation.DecryptString(GlobalVar.Key, ini.IniReadValue("XMAN", "Server"))).SecurePassword;

                    securePassword.MakeReadOnly();
                   var credentials = new SqlCredential("standartEndpoints", securePassword);
                  //  var credentials = new SqlCredential("sa", securePassword);







                    connection.Credential = credentials;

                    connection.Open();


                }

                

                
            }
            catch (Exception err)
            {
                if (err.Source.Contains(".Net SqlClient Data Provider"))
                {
                    return null;
                }
                throw err;
            }

            return connection;
        }

        public IDbConnection GetConnection(int _pTahun)
        {
            SqlConnection connection = null;
            Configuration.Tahun = _pTahun;



            Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "KUAPPAS.ini");

            connection = new SqlConnection();
            try
            {



                SqlConnectionStringBuilder strbldr = new SqlConnectionStringBuilder();
                strbldr.DataSource = "36.92.240.141";
                strbldr.InitialCatalog = "KTP2024";
                strbldr.TrustServerCertificate = true;
                strbldr.Encrypt = true;



                // strbldr.IntegratedSecurity = true;
                strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
                connection = new SqlConnection(strbldr.ConnectionString);
                SecureString securePassword = new System.Net.NetworkCredential(" ",
                AesOperation.DecryptString(GlobalVar.Key, ini.IniReadValue("XMAN", "Server"))).SecurePassword;


                securePassword.MakeReadOnly();
                //var credentials = new SqlCredential("standardEndpoints", securePassword);
                var credentials = new SqlCredential("standardEndpoints", securePassword);

                connection.Credential = credentials;

                connection.Open();
            }
            catch (Exception err)
            {
                if (err.Source.Contains(".Net SqlClient Data Provider"))
                {
                    return null;
                }
                throw err;
            }

            return connection;
        }

        public string GetConnectionString()
        {
            // return "Server=localhost;Port=3306;Database=accountplus;Uid=root;Pwd=P@ssw0rd;allow user variables=true";
            // string connectionString;
            try
            {
                string _Server;
                string _Database;
                //   string _Port;
                string _Password;
                string _Type;
                Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "KUAPPAS.ini");

                _Server = ini.IniReadValue("KUA", "Server");
                _Database = ini.IniReadValue("KUA", "DATABASE");
                //GlobalVar.Versi= ini.IniReadValue("KUA", "CURRENTVERSION");


                _Type = ini.IniReadValue("KUA", "TYPE");
                if (_Type.Length == 0)
                {
                    _Password = "pasworssssd123!";//ini.IniReadValue("KUA", "PASSWORD");
                    _Server = "180.250.207.101";

                }
                else
                {
                    _Password = "unclejo";//ini.IniReadValue("KUA", "PASSWORD");
                }


                //return "Data Source=PUDJOISNANTO-PC\\MSSQL20//08;Initial Catalog=SimplyKDKTP;User ID=sa;Password=unclejo;";
                //GlobalVar.NamaServer = _Server;
                //GlobalVar.NamaDatabase = _Database;

                return "Data Source=" + _Server + ";Initial Catalog=" + _Database + ";User ID=sa;Password=" + _Password + ";";





                // return connectionString;// ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}
