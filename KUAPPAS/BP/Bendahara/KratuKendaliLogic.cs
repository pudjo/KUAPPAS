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
    public class KartuKendaliLogic:BP
    {

        public KartuKendaliLogic(int tahun)
            : base(tahun)
        {

         }
        public List<KartuKendali> GetDataKendali(int iddinas, DateTime tanggal)
        {
            List<KartuKendali> lst = new List<KartuKendali>();
            try
            {

                SSQL = "select * from fnKartuKendali(@DINAS,@TANGGAL)";

               DBParameterCollection paramCollection = new DBParameterCollection();

               paramCollection.Add(new DBParameter("@DINAS", iddinas, DbType.Int32));
               paramCollection.Add(new DBParameter("@TANGGAL", tanggal, DbType.Date));
                
                DataTable dt = new DataTable();
            

                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // if (_p.Tahap == 0 || _p.Tahap == 2)
                        //{ 
                        lst = (from DataRow dr in dt.Rows
                                select new KartuKendali()
                                {
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),                            
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    SP2DUP = DataFormat.GetDecimal(dr["SP2DUP"]),
                                    SP2DGU = DataFormat.GetDecimal(dr["SP2DGU"]),
                                    SP2DTU = DataFormat.GetDecimal(dr["SP2DTU"]),
                                    SP2DLS = DataFormat.GetDecimal(dr["SP2DLS"]),
                                    
                                    RSP2DGU = DataFormat.GetDecimal(dr["RSP2DGU"]),
                                    RSP2DTU = DataFormat.GetDecimal(dr["RSP2DTU"]),
                                    RSP2DLS = DataFormat.GetDecimal(dr["RSP2DLS"]),

                                    
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
        public List<clsObject> GetJumlahSP2D(int idDinas, int JenisSumber, int Jenis ,DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {
                List<clsObject> lst = new List<clsObject>();
                SSQL = "SELECT IDSUbkegiatan, IIDrekening,SP2DUP, SP2DGU,SP2DTU,SP2DTULS,RSP2DGU,RSP2DTU,RSP2DTULS  from dbo.fnKartuKendali (" + idDinas.ToString() + "," + tanggalakhir.ToSQLFormat() + ")" +
                       " Order by IDSUbkegiatan, IIDrekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new clsObject()
                               {
                                   NoUrut = DataFormat.GetLong(dr["inourut"]),
                                   NoBukti = DataFormat.GetString(dr["sNoBukti"]),

                                   Jumlah = DataFormat.GetLong(dr["Jumlah"]),
                                   Tanggal = DataFormat.GetDate(dr["dtBukukas"]),
                               }).ToList();

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;

            }
        }
        public decimal GetJumlahDetail(int idDinas, int JenisSumber, DateTime tanggalAwal, DateTime tanggalakhir)
        {
            decimal dRet = 0;
            try
            {

                SSQL = "SELECT sum(tPanjarRekening.cJumlah) as Jumlah from tPanjar   inner join tPanjarRekening " +
                    " ON tPanjar.inourut= tPanjarRekening.inourut WHERE tPanjar.IDDInas =" + idDinas.ToString() +
                     " AND tPanjar.dtBukukas >=" + tanggalAwal.ToSQLFormat() + " AND tPanjar.dtBukukas <=" + tanggalakhir.ToSQLFormat();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        dRet = DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return dRet;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return dRet;

            }
        }

    }
}
