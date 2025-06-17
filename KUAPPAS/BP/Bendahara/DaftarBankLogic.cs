using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO.Bendahara ;
using System.Data.OleDb;
using DTO;
using Formatting;
using BP;
using DataAccess;

namespace BP.Bendahara
{
    public class DaftarBankLogic:BP 
       
        

    {
        public DaftarBankLogic(int thn)
            : base(thn)
        {
            Tahun = thn;
        }
        public bool Simpan(DaftarBank bank)
        {
            try
            {
                SSQL = "INSERT INTO Banks(bankCode,Nama) values (@KODE,@NAMA)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KODE", bank.bankCode));
                paramCollection.Add(new DBParameter("@NAMA", bank.Nama));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ApaBankSudahAda(DaftarBank bank)
        {
            try
            {
                SSQL = "SELECT * Banks where bankCode=@kode";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KODE", bank.bankCode));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    } else
                        return false;
                }


                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List <DaftarBank> GetBanks(){
            List<DaftarBank> lst = new List<DaftarBank>();
            try{
            SSQL = "SELECT * from Banks order by bankCode";
            DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new DaftarBank()
                                {
                                    Nama = DataFormat.GetString (dr["Nama"]),
                                    bankCode = DataFormat.GetString (dr["bankCode"]),
                                //Rekening 
                                }).ToList();
                    }
            }
            return lst;
            }catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }
        }
    }
}
