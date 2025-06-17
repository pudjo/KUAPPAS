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
    public class BICBankLogic:BP
    {
        public BICBankLogic(int _pTahun, int profile)
            : base(_pTahun, 0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "BICBank";

        }
        
     



        public bool SImpanBanks(List<BICBank> _banks){
            try{
                SSQL="DELETE BICBank";
                _dbHelper.ExecuteNonQuery(SSQL);

                foreach(BICBank bb in _banks){
                    SSQL="INSERT INTO BICBank(KodeBIC ,Nama  ShortName ,Kode ) value("+
                        "@pKodeBIC,@pNama,@pShortName,@pKode)";

                     DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pKodeBIC",bb.KodeBIC));
                    paramCollection.Add(new DBParameter("@pNama",bb.Nama));
                    paramCollection.Add(new DBParameter("@pShortName",bb.ShortName));
                    paramCollection.Add(new DBParameter("@pKode",bb.Kode));

                    
                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                 

                }
                return true;

            } catch (Exception ex){
                _lastError = ex.Message;
                return false;
            }
        }
        public List<BICBank> Get(){




            List<BICBank> _lst = new List<BICBank>();
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
                                select new BICBank()
                                {
                                    KodeBIC = DataFormat.GetString (dr["KodeBIC"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    ShortName = DataFormat.GetString(dr["ShortName"]),
                                    Kode = DataFormat.GetString(dr["Kode"]),
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
