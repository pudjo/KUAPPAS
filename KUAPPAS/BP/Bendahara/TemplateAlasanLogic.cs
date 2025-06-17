using DataAccess;
using DTO.Bendahara;
using Formatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Bendahara
{
    public class TemplateAlasanLogic:BP
    {
        public TemplateAlasanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;

        

        }




        public bool Simpan(TemplateAlasan tA)
        {
            try
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "INSERT INTO TemplateAlasan (Alasan) values (" +
                     "@Alasan)";            
            
               paramCollection.Add(new DBParameter("@Alasan", tA.Alasan));
               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                   return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;

                return false;
            }


        }

        public List<TemplateAlasan> Get()
        {
            List<TemplateAlasan> _lst = new List<TemplateAlasan>();
            try
            {
                SSQL = "SELECT * FROM TemplateAlasan order by Alasan ";                
                
              
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TemplateAlasan()
                                {

                                    Alasan = DataFormat.GetString(dr["Alasan"])
                                    
                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }

    }
}
