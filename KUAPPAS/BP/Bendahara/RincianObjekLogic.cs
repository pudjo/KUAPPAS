using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.Bendahara;
using BP;
using BP.Bendahara;
using DataAccess;
using System.Data;
using Formatting;


namespace BP.Bendahara
{
    public class RincianObjekLogic: BP
    {
        public RincianObjekLogic(int tahun): base(tahun){

        }
        public List<RincianObjek> GetRincianObjek(int iddinas, DateTime tanggal, long idSubKegiatan=0, long idrekening=0)
        {
             List<RincianObjek> lst= new List<RincianObjek>();
        try{
            SSQL = "select * from fnRincianObjek(@DINAS,@TANGGAL) where Tahun = @TAHUN";
             

               DBParameterCollection paramCollection = new DBParameterCollection();

               paramCollection.Add(new DBParameter("@TAHUN", Tahun));
               paramCollection.Add(new DBParameter("@DINAS", iddinas, DbType.Int32));
               paramCollection.Add(new DBParameter("@TANGGAL", tanggal, DbType.Date));

               if (idSubKegiatan > 0)
               {
                   SSQL = SSQL + " AND IDSUbKegiatan= @IDSUBKEGIATAN ";
                   paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idSubKegiatan, DbType.Int64));

               }
               if (idrekening > 0)
               {
                   SSQL = SSQL + " AND IIDrekening = @IDRekening ";
                   paramCollection.Add(new DBParameter("@IDRekening", idrekening, DbType.Int64));

               }
            
            SSQL = SSQL + " ORDER BY IDSubKegiatan, IIDRekening,NoBKUSKPD";
                
                DataTable dt = new DataTable();
            

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{ 
                        lst = (from DataRow dr in dt.Rows
                                select new RincianObjek()
                                {
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDSUbKegiatan= DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening= DataFormat.GetLong(dr["IIDRekening"]),
                                   KodeUK = DataFormat.GetInteger(dr["KodeUK"]),
                                  //  NamaRekening=DataFormat.GetString(dr["sNamaRekening"]),
                                    Tanggal = DataFormat.GetDate(dr["dtBukti"]),
                                    NoBKU= DataFormat.GetInteger(dr["NoBKUSKPD"]),
                                    NoBukti= DataFormat.GetString(dr["NoBukti"]),
                                    Uraian= DataFormat.GetString(dr["sUraian"]),
                                   UP= DataFormat.GetDecimal(dr["UP"]),
                                   LS = DataFormat.GetDecimal(dr["LS"]),
                                   TU= DataFormat.GetDecimal(dr["TU"]),
                                    JenisBeanja = DataFormat.GetInteger(dr["cJenisBelanja"])


                                    
                                }).ToList();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;

            }
            return lst;

        }
         
    }
}
