using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using Formatting;
using DataAccess;
using System.Data;

namespace BP
{
    public class BanksLogic:BP
    {
       
       public BanksLogic(int _pTahun, int profile )
            : base(_pTahun, 0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "Banks";

        }
                public bool SImpanBanks(List<Banks> _banks){
            try{
                SSQL="DELETE Banks";
                _dbHelper.ExecuteNonQuery(SSQL);
                //Banks (KodeBIC,Nama,NoAKun,Kode char(20))
                foreach(Banks b in _banks){
                    SSQL = "INSERT INTO Banks(Nama,BankCode) values(" +
                        "@pNama,@pKode)";

                     DBParameterCollection paramCollection = new DBParameterCollection();
                     
                    paramCollection.Add(new DBParameter("@pNama",b.Nama));
                    paramCollection.Add(new DBParameter("@pKode",b.bankCode));

                    
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                 

                }
                return true;

            } catch (Exception ex){
                _lastError = ex.Message;
                return false;
            }
        }
        public List<Banks> Get(){




            List<Banks> _lst = new List<Banks>();
            try
            {
                SSQL = "SELECT *  from " + m_sNamaTabel ;
                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                
                if (dt != null)
                {

               
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Banks()
                                {


                



                                    Nama  = DataFormat.GetString(dr["Nama"]),

                                    bankCode = DataFormat.GetString(dr["bankCode"]),


                                }).ToList();
                    }
                    
                }
                

                return _lst;
            } catch(Exception ex){
                _lastError = ex.Message;
                return null;
            }

           


        }




       
    }
}
