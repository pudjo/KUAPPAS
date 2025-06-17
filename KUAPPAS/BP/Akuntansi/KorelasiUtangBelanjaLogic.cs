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
    public class KorelasiUtangBelanjaLogic:BP
    {
        public KorelasiUtangBelanjaLogic(int tahun)
            : base(tahun)
        {

        }
        public bool Simpan(KorelasiUtangBelanja k)
        {
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (CekKorelasi (k)==0){
                    SSQL="INSERT INTO korpermenpiutang(IidRekening,IIDPiutang) values(@IDREKENING,@IDRekeningUtang)";
                    paramCollection.Add(new DBParameter("@IDREKENING",k.IDRekening));
                    paramCollection.Add(new DBParameter("@IDRekeningUtang",k.IDRekeningUtang));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE  korpermenpiutang SET IIDPiutang=@IDRekeningUtang WHERE IidRekening=@IDREKENING";

                    paramCollection.Add(new DBParameter("@IDREKENING", k.IDRekening));
                    paramCollection.Add(new DBParameter("@IDRekeningUtang", k.IDRekeningUtang));
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
        private int CekKorelasi(KorelasiUtangBelanja k)
        {
            try
            {
                Object obj;
                SSQL = "SELECT count(*) FROM korpermenpiutang WHERE IidRekening=" + k.IDRekening.ToString();
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


        public bool Hapus(KorelasiUtangBelanja k)
        {
            try
            {
       
                SSQL = "DELETE FROM korpermenpiutang WHERE IidRekening=" + k.IDRekening.ToString();
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
        public KorelasiUtangBelanja GetByIDRekening(long ID)
        {
            try
            {
                List<KorelasiUtangBelanja> lst = new List<KorelasiUtangBelanja>();
                SSQL = "select korpermenpiutang.*, a.SnamaRekening as Nama, b.SnamaRekening as NamaUtang  from korpermenpiutang "+
                        " inner join mRekening a on korpermenpiutang.IidRekening= a.IIDRekening"+
                        " inner join mRekening b on korpermenpiutang.iidpiutang = b.IIDRekening" +
                         " WHERE IidRekening=" + ID.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiUtangBelanja()
                               {
                                   IDRekening = DataFormat.GetLong(dr["IidRekening"]),
                                   IDRekeningUtang = DataFormat.GetLong(dr["iidpiutang"]),
                                   NamaRekening = DataFormat.GetString(dr["Nama"]),
                                   NamaRekeningUtang = DataFormat.GetString(dr["NamaUtang"]),


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

        public List<KorelasiUtangBelanja> Get()
        {
            try
            {
                List<KorelasiUtangBelanja> lst = new List<KorelasiUtangBelanja>();
                SSQL = "select korpermenpiutang.*, a.SnamaRekening as Nama, b.SnamaRekening as NamaUtang  from korpermenpiutang" +
                        " inner join mRekening a on korpermenpiutang.IidRekening= a.IIDRekening" +
                        " inner join mRekening b on korpermenpiutang.IIDPiutang = b.IIDRekening" +
                         " ORDER BY  IidRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new KorelasiUtangBelanja()
                               {
                                   IDRekening = DataFormat.GetLong(dr["IidRekening"]),
                                   IDRekeningUtang = DataFormat.GetLong(dr["IIDPiutang"]),
                                   NamaRekening = DataFormat.GetString(dr["Nama"]),
                                   NamaRekeningUtang = DataFormat.GetString(dr["NamaUtang"]),


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
