using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using Formatting;
using KUAPPAS;
namespace DataAccess
{
     internal static class Configuration
    {
        const string DEFAULT_CONNECTION_KEY = "defaultConnection";
        public static int Tahun;
        public  static int PerbaikanAPBD;
        public static int KEPMEN050;
            
        


        public static string DefaultConnection
        {
          
            get
            {
                //  <add name="mySqlCon" connectionString="Server=localhost;Port=3306;Database=accountplus;Uid=root;Pwd=P@ssw0rd;allow user variables=true" providerName="MYSQL" />

                //return ConfigurationManager.AppSettings[DEFAULT_CONNECTION_KEY];
                return "sqlServerCon";
            }
        }
        public static string DBProvider
        {
            get
            {
                return "SQLSERVER";
                //return ConfigurationManager.ConnectionStrings[DefaultConnection].ProviderName;
                //
                //return "MYSQL";
            }
        }

        public static string ConnectionString
        {
            get
            {
               // return "Server=localhost;Port=3306;Database=accountplus;Uid=root;Pwd=P@ssw0rd;allow user variables=true";
            //    string connectionString;
                try
                {
                    string _Server="";
                    string _Database = "";
                   // string _Port = "";D:\Project\Source\KETAPANG\KUAPPAS2510KTP\KUAPPAS2510\KUAPPAS\BP\RevisiLogic.cs
                    string _Password = "";
                    string _Type = "1";
                    string _Jaringan = "1";
                    Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "KUAPPAS.ini");
                    string _userID="sa";
                    //_Server = ini.IniReadValue("KUA", "Server");
                    //_Database = ini.IniReadValue("KUA", "DATABASE");
                    //_Password = ini.IniReadValue("KUA", "PASWORD");

                    _Jaringan = ini.IniReadValue("KUA", "Jaringan");
                   
                    //_Type = ini.IniReadValue("KUA", "TYPE");
                    //if (_Type != "6")
                    //{
#if DEBUG
                        _Type = "2";
#else 
                        _Type = "2";
                      //  _Type = "4"; //pentest

#endif

                    //}

                    


                       
                        if (_Type == "2")
                        {
                          
                                   _Database = "KTP2024";//15Maret";
                              //    _Database = "KTP20242April";
                               //     _Password = "sikdaccess123!";
                           
                                    _Server = "192.168.88.199";//
                                if (_Jaringan == "2")
                                    _Server = "36.92.240.142";
                                else
                                    _Server = "192.168.88.199";
                                _userID = "sa";
                                _Password = "sikdaccess123!";
                            //_Database = "KTP2023";
                            //_Password = "unclejo";
                            //_Server = "LAPTOP-FHSK22A0\\SQLEXPRESS";

                            //_Password = "Indgafsqlsrv2021";
                            //_Server = "103.28.53.130";

                        }
                        if (_Type == "5"){
                            if (Tahun == 2023)
                                _Database = "KTP2023";
                        
                            if (Tahun == 2024)
                                _Database = "KTP202418Maret";
                            if (_Jaringan == "2")
                                _Server = "36.92.240.142";
                            else
                                _Server = "192.168.88.199";
                            _userID = "sa";
                            _Password = "sikdaccess123!";
                        }


                    
                    string ConnctionSTring = "Data Source=" + _Server + ";Initial Catalog=" + _Database + ";User ID=" + _userID + ";Password=" + _Password + ";";
                    
                    string encrypted = AesOperation.EncryptString(GlobalVar.Key, ConnctionSTring);


                     string s=AesOperation.DecryptString(GlobalVar.Key,encrypted);

                     return s;
                           // return "Data Source=" + _Server + ";Initial Catalog=" + _Database + ";User ID=" + _userID + ";Password=" + _Password + ";";
                  
                } catch(Exception ex){
                    return ex.Message;
                }
            }
        }   
    }
}
