using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using DataAccess;
using Formatting;
using System.Data;


namespace BP
{
    public class RBALogic:BP
    {
        private void CekKolomID()
        {

        }
        public RBALogic(int _unit, int _pTahun, Single _pTahap, int profile)
            : base(_pTahun, 0, profile)
        {
        
            

        }
        
        public bool Simpan(List<RBA> _lst, int _pUnit, int _pTahun, int _pTahap)
        {
            foreach (RBA  o in _lst)
            {
                
                if (o.ID == 0)
                {
                    // Auto incrment for iD 
                    SSQL = "INSERT INTO tAnggaranUraian_A (Tahun ,Unit ,Tahap,IDRekening,Uraian ,HargaSatuan ,Jumlah ,Satuan)  values ( " +
                                    "@pTahun ,@pUnit ,@pTahap ,@pUreaian ,@pHargaSatuan ,@pJumlah ,@pSatuan)";
                    
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pTahun",o.Tahun));
                    paramCollection.Add(new DBParameter("@pUnit",o.Unit));
                    paramCollection.Add(new DBParameter("@pTahap",o.Tahap));
                    paramCollection.Add(new DBParameter("@pIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@pUraian",o.Uraian));
                    paramCollection.Add(new DBParameter("@pHargaSatuan",o.HargaSatuan));
                    paramCollection.Add(new DBParameter("@pJumlah",o.Jumlah));
                    paramCollection.Add(new DBParameter("@pSatuan",o.Satuan));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                      // Auto incrment for iD 
                    SSQL = "UPDATE RBA  SET Unit=@pUnit ,Tahap=@pTahap ,IDRekening=@pIDRekening,Uraian=@pUraian,HargaSatuan =@pHargaSatuan,Jumlah=@pJumlah ,Satuan=@pSatuan WHERE ID =@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pTahun",o.Tahun));
                    paramCollection.Add(new DBParameter("@pUnit",o.Unit));
                    
                    paramCollection.Add(new DBParameter("@pTahap",o.Tahap));
                    paramCollection.Add(new DBParameter("@pIDRekening", o.IDRekening));
                    paramCollection.Add(new DBParameter("@pUraian",o.Uraian));
                    paramCollection.Add(new DBParameter("@pHargaSatuan",o.HargaSatuan));
                    paramCollection.Add(new DBParameter("@pJumlah",o.Jumlah));
                    paramCollection.Add(new DBParameter("@pSatuan",o.Satuan));
                    paramCollection.Add(new DBParameter("@pID",o.ID));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    }
                                }
            return true;
        }
        
        public bool Hapus(RBA o)
        {
            try { 
                SSQL = "DELETE from RBA  WHERE iTahun=@piTahun and ID = @pID ";

                DBParameterCollection paramCollection = new DBParameterCollection();                
                paramCollection.Add(new DBParameter("@piTahun", o.Tahun));
                paramCollection.Add(new DBParameter("@pID", o.ID ));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;
            } catch(Exception ex)
            
            {
                _isError = true;
                _lastError = ex.Message;
                return false;

            }
        }
        
    }
}
