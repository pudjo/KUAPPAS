using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace DTO
{
    public class RemoteConnection
    {
        public string Server { set; get; }
        public string Database { set; get; }
        public string Password { set; get; }
        public string UserID { set; get; }
        
        public int Jalur{ set; get; }


        public int Jenis { set; get; }

        public IDbConnection GetConnection(){
            SqlConnection connection = null;

            string connectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";User ID=" + UserID + " ;Password=" + Password + ";";

     
           connection = new SqlConnection(connectionString);

            try
            {

                connection.Open();
                return connection;

            }
            catch (Exception )
            {
                 
                return null;
            }

            

        }

                    
    }
}
