using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DTO.Akuntansi;
using Formatting;
using System.Data;
namespace BP.Akuntansi
{
    public class KorlasiLRALOLogic:BP
    {
        public KorlasiLRALOLogic(int tahun)
            : base(tahun)
        {

        }
        public bool Simpan(KorelasiLRALO k)
        {
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (CekKorelasi (k)==0){
                    SSQL="INSERT INTO KOR_LRA_LO(IidRekening,iidrekeningLO) values(@IDREKENING,@IDREKENINGLO)";
                    paramCollection.Add(new DBParameter("@IDREKENING",k.IDRekening));
                    paramCollection.Add(new DBParameter("@IDREKENINGLO",k.IDRekeningLO));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE  KOR_LRA_LO SET iidrekeningLO=@IDREKENINGLO WHERE IidRekening=@IDREKENING";

                    paramCollection.Add(new DBParameter("@IDREKENINGASET", k.IDRekening));
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
        private int CekKorelasi(KorelasiLRALO k)
        {
            try
            {
                Object obj;
                SSQL = "SELECT count(*) FROM KOR_LRA_LO WHERE IidRekening=" + k.IDRekening.ToString();
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


        public bool Hapus(KorelasiLRALO k)
        {
            try
            {
       
                SSQL = "DELETE FROM KOR_LRA_LO WHERE IidRekening=" + k.IDRekening.ToString();
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
        public KorelasiLRALO GetByIDRekening(long ID)
        {
            try
            {
                List<KorelasiLRALO> lst = new List<KorelasiLRALO>();
                SSQL = "select KOR_LRA_LO.*, a.SnamaRekening as Nama, b.SnamaRekening as NamaLO  from KOR_LRA_LO "+
                        " inner join mRekening a on KOR_LRA_LO.IidRekening= a.IIDRekening"+
                        " inner join mRekening b on KOR_LRA_LO.IIDREkeningLO = b.IIDRekening"+
                         " WHERE IidRekening=" + ID.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiLRALO()
                               {
                                   IDRekening = DataFormat.GetLong(dr["IidRekening"]),
                                   IDRekeningLO = DataFormat.GetLong(dr["iidrekeningLO"]),
                                   NamaRekening = DataFormat.GetString(dr["Nama"]),
                                   NamaRekeningLO = DataFormat.GetString(dr["NamaLO"]),


                               }).ToList();
                        return lst[0];
                    }
                    else
                    {
                        _lastError = "Korelasi " + ID.ToKodeRekening()  + " Tidak Ditemukan";
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

        public List<KorelasiLRALO> Get()
        {
            try
            {
                List<KorelasiLRALO> lst = new List<KorelasiLRALO>();
                SSQL = "select KOR_LRA_LO.*, a.SnamaRekening as Nama, b.SnamaRekening as NamaLO  from KOR_LRA_LO" +
                        " inner join mRekening a on KOR_LRA_LO.IidRekening= a.IIDRekening" +
                        " inner join mRekening b on KOR_LRA_LO.IIDREkeningLO = b.IIDRekening" +
                         " ORDER BY  IidRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiLRALO()
                               {
                                   IDRekening = DataFormat.GetLong(dr["IidRekening"]),
                                   IDRekeningLO = DataFormat.GetLong(dr["iidrekeningLO"]),
                                   NamaRekening = DataFormat.GetString(dr["Nama"]),
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
