using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using DataAccess;
using Formatting;
using System.Data;
using Formatting;
using System.Data;
using DataAccess;

namespace BP
{
    public class PenggunaDinasLogic: BP 
    {
        
         public PenggunaDinasLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "PenggunaDinas";
        }
        
        public List<PenggunaDinas> Get()
        {

            List<PenggunaDinas> _lst = new List<PenggunaDinas>();
            try
            {
                SSQL = "SELECT IDDinas,Unit,UserID,PPKD from PenggunaDinas";

                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new PenggunaDinas()
                                {
                                    UserID  = DataFormat.GetInteger(dr["UserID"]),
                                    SKPD  = DataFormat.GetInteger(dr["IDDInas"]),
                                    Unit = DataFormat.GetInteger(dr["Unit"]),
                                    PPKD  = DataFormat.GetInteger(dr["PPKD"])
        
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

        public List<PenggunaDinas> GetDinasByUserID( int pID)
        {
        List<PenggunaDinas> lst = new List<PenggunaDinas>();
            try
            {
                SSQL = "SELECT IDDinas,Unit,UserID,PPKD from PenggunaDinas where UserID =" + pID.ToString();

                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows                                
                                select new PenggunaDinas()
                                {
                                    UserID  = DataFormat.GetInteger(dr["UserID"]),
                                    SKPD  = DataFormat.GetInteger(dr["IDDInas"]),
                                    Unit = DataFormat.GetInteger(dr["Unit"]),
                                    PPKD  = DataFormat.GetInteger(dr["PPKD"])
        
                                }).ToList();
                    }
                }
                return lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }


        }

       
       
        public bool Simpan( PenggunaDinas _pPenggunaDinas)
        {
            
            try
            {


                SSQL = "DELETE PenggunaDinas where UserID=@pUserID AND IDDInas=@pIDDInas and Unit=@pUnit and PPKD=@pPPKD";
              
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pUserID", _pPenggunaDinas.UserID));
                paramCollection.Add(new DBParameter("@pIDDInas", _pPenggunaDinas.SKPD));
                paramCollection.Add(new DBParameter("@pUnit", _pPenggunaDinas.Unit));
                paramCollection.Add(new DBParameter("@pPPKD", _pPenggunaDinas.PPKD));


                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                


                SSQL = "INSERT INTO PenggunaDinas(UserID,IDDInas,Unit,PPKD) values (" +
                "@pUserID,@pIDDInas,@pUnit,@pPPKD )";               

                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus( PenggunaDinas _pPenggunaDinas)
        {

            try
            {


                SSQL = "DELETE PenggunaDinas where UserID=@pUserID AND IDDInas=@pIDDInas";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pUserID", _pPenggunaDinas.UserID));
                paramCollection.Add(new DBParameter("@pIDDInas", _pPenggunaDinas.SKPD));
                

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
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
