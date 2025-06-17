using DataAccess;
using DTO.Akuntansi;
using Formatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Akuntansi
{
    public class KorelasiPenyesuaianLogic:BP
    {
        public KorelasiPenyesuaianLogic(int tahun)
            : base(tahun)
        {

        }
        public bool Simpan(KorelasiPenyesuaian k)
        {
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (CekKorelasi (k)==0){
                    SSQL="INSERT INTO KorelasiPenyesuaian(IidRekeningAset,iidrekeningLO) values(@IDREKENINGASET,@IDREKENINGLO)";
                    paramCollection.Add(new DBParameter("@IDREKENINGASET",k.IDRekeningAset));
                    paramCollection.Add(new DBParameter("@IDREKENINGLO",k.IDRekeningLO));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE  KorelasiPenyesuaian SET iidrekeningLO=@IDREKENINGLO WHERE IidRekeningAset=@IDREKENINGASET";

                    paramCollection.Add(new DBParameter("@IDREKENINGASET", k.IDRekeningAset));
                    paramCollection.Add(new DBParameter("@IDREKENINGLO", k.IDRekeningLO));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }
        private int CekKorelasi(KorelasiPenyesuaian k)
        {
            try
            {
                Object obj;
                SSQL = "SELECT count(*) FROM KorelasiPenyesuaian WHERE IIDREKENINGASET=" + k.IDRekeningAset.ToString();
                obj = _dbHelper.ExecuteScalar(SSQL);

                return (int)obj;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }
        }

        
        public  bool Hapus(KorelasiPenyesuaian k)
        {
            try
            {
       
                SSQL = "DELETE FROM KorelasiPenyesuaian WHERE IIDREKENINGASET=" + k.IDRekeningAset.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }

        }
        public KorelasiPenyesuaian GetByIDASET(long IDASET)
        {
            try
            {
                List<KorelasiPenyesuaian> lst= new List<KorelasiPenyesuaian>();
                SSQL = "select KorelasiPenyesuaian.*, a.SnamaRekening as NamaASet, b.SnamaRekening as NamaLO  from KorelasiPenyesuaian"+
                        " inner join mRekening a on KorelasiPenyesuaian.iidRekeningAset= a.IIDRekening"+
                        " inner join mRekening b on KorelasiPenyesuaian.IIDREkeningLO = b.IIDRekening"+
                         " WHERE IIDREKENINGASET=" + IDASET.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiPenyesuaian()
                               {
                                   IDRekeningAset = DataFormat.GetLong(dr["iidrekeningAset"]),
                                   IDRekeningLO = DataFormat.GetLong(dr["iidrekeningLO"]),
                                   NamaRekeningAset = DataFormat.GetString(dr["NamaASet"]),
                                   NamaRekeningLO = DataFormat.GetString(dr["NamaLO"]),


                               }).ToList();
                        return lst[0];
                    }
                    else
                    {
                        _lastError = "Korelasi " + IDASET.ToKodeRekening()  + " Tidak Ditemukan";
                        _isError = true;
                        return null;
                    }
                }
                return null;
            
               
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;
            }

        }
        public KorelasiPenyesuaian GetByIDLO(long IDLO)
        {
            try
            {
                List<KorelasiPenyesuaian> lst = new List<KorelasiPenyesuaian>();
                SSQL = "select KorelasiPenyesuaian.*, a.SnamaRekening as NamaASet, b.SnamaRekening as NamaLO  from KorelasiPenyesuaian" +
                        " inner join mRekening a on KorelasiPenyesuaian.iidRekeningAset= a.IIDRekening" +
                        " inner join mRekening b on KorelasiPenyesuaian.IIDREkeningLO = b.IIDRekening" +
                         " WHERE IIDREKENINGLO=" + IDLO.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiPenyesuaian()
                               {
                                   IDRekeningAset = DataFormat.GetLong(dr["iidrekeningAset"]),
                                   IDRekeningLO = DataFormat.GetLong(dr["iidrekeningLO"]),
                                   NamaRekeningAset = DataFormat.GetString(dr["NamaASet"]),
                                   NamaRekeningLO = DataFormat.GetString(dr["NamaLO"]),


                               }).ToList();
                        return lst[0];
                    }
                    else
                    {
                        _lastError = "Korelasi " + IDLO.ToKodeRekening() + " Tidak Ditemukan";
                        _isError = true;
                        return null;
                    }
                }
                return null;


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;
            }

        }
        public List<KorelasiPenyesuaian> Get()
        {
            try
            {
                List<KorelasiPenyesuaian> lst = new List<KorelasiPenyesuaian>();
                SSQL = "select KorelasiPenyesuaian.*, a.SnamaRekening as NamaASet, b.SnamaRekening as NamaLO  from KorelasiPenyesuaian" +
                        " inner join mRekening a on KorelasiPenyesuaian.iidRekeningAset= a.IIDRekening" +
                        " inner join mRekening b on KorelasiPenyesuaian.IIDREkeningLO = b.IIDRekening" +
                         " ORDER BY  IIDREKENINGASET";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiPenyesuaian()
                               {
                                   IDRekeningAset = DataFormat.GetLong(dr["iidrekeningAset"]),
                                   IDRekeningLO = DataFormat.GetLong(dr["iidrekeningLO"]),
                                   NamaRekeningAset = DataFormat.GetString(dr["NamaASet"]),
                                   NamaRekeningLO = DataFormat.GetString(dr["NamaLO"]),


                               }).ToList();
                        return lst;
                    }
                    else
                    {
                        _lastError = "Korelasi  Tidak Ditemukan";
                        _isError = true;
                        return null;
                    }
                }
                return null;


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;
            }

        }
    }
}
