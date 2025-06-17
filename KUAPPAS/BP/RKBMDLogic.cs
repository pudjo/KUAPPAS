using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DTO;
using DataAccess;
using Formatting;
using System.Data;
using System.Data.Sql;


namespace BP
{
    public class RKBMDLogic:BP 
    {
        RemoteConnection _connection;
        public RKBMDLogic(int Tahun, RemoteConnection rk): base(Tahun)

        {

            _connection = rk;

        }

        public bool DeActivateRKBMD(int id, int idrKBMDBARANG)
        {
            List<RKBMD> _lst = new List<RKBMD>();
            try
            {
                SSQL = "UPDATE RKBMDBarang set statusrka=9 where IDRKBMD=" + id.ToString() + " and ID=" + idrKBMDBARANG.ToString();

                _dbHelper.ExecuteNonQuery(SSQL, _connection.GetConnection());
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;

            }
        }

        public List<RKBMD> GetByIDDInas(int iddinas)
        {
            List<RKBMD> _lst = new List<RKBMD>();
            try
            {
                //SSQL = "select RKBMD.ID as ID,RKBMDBarang.ID as IDRKBMDBArang,RKBMD.Tahun,  mskpd.ID90 as IDDInasKeuangan , RKBMD.SKPD , RKBMD.IDUrusan, " +
                //    " RKBMD.IDProgram, RKBMD.IDkegiatan, RKBMD.IDSubkegiatan, RKBMD.Jenis," +
                //    " RKBMDBarang.IDBarang, RKBMDBarang.NamaBarang, RKBMDBarang.Jumlah, " +
                //    " RKBMDBarang.JumlahPerubahan,  mRekening.IIDRekening, mRekening.sNamaRekening,RKBMDBarang.No,RKBMDBarang.CaraPemenuhan FROM " +
                //    " RKBMD INNER JOIN RKBMDBarang ON RKBMD.ID= RKBMDBarang.IDRKBMD  INNER JOIN KORBELANJABARANG ON " +
                //    " KORBELANJABARANG.IDBarang = cast((RKBMDBArang.IDBarang)/1000 as bigint) * 1000 INNER JOIN mSKPD ON mSKPD.ID=RKBMD.SKPD " +
                //    " INNER JOIN mRekening ON " +
                //    " mRekening.IIDRekening=KORBELANJABARANG.IDRekening and KORBELANJABARANG.Jenis= RKBMD.Jenis WHERE isnull(RKBMDBarang.StatusRKA,0)<9 and  RKBMD.Jenis = 1 AND " +
                //    " mskpd.id90 =  " + iddinas.ToString() + " AND Tahun = " + Tahun.ToString() + " ORDER BY RKBMD.IDUrusan, RKBMD.IDPRogram, RKBMD.IDKegiatan," +
                //    " RKBMD.IDSUbKegiatan, KORBELANJABARANG.IDRekening, RKBMDBarang.IDBarang , RKBMDBarang.No";



                SSQL = "select RKBMD.ID as ID,RKBMDBarang.ID as IDRKBMDBArang,RKBMD.Tahun,  mskpd.ID90 as IDDInasKeuangan , RKBMD.SKPD , RKBMD.IDUrusan, " +
                    " RKBMD.IDProgram, RKBMD.IDkegiatan, RKBMD.IDSubkegiatan, RKBMD.Jenis," +
                    " RKBMDBarang.IDBarang, RKBMDBarang.NamaBarang, RKBMDBarang.Jumlah, " +
                    " RKBMDBarang.JumlahPerubahan,  mRekening.IIDRekening, mRekening.sNamaRekening,RKBMDBarang.No,RKBMDBarang.CaraPemenuhan FROM " +
                    " RKBMD INNER JOIN RKBMDBarang ON RKBMD.ID= RKBMDBarang.IDRKBMD  INNER JOIN KORBELANJABARANG ON " +
                    " KORBELANJABARANG.IDBarang/1000 = RKBMDBArang.IDBarang/1000  INNER JOIN mSKPD ON mSKPD.ID=RKBMD.SKPD " +
                    " INNER JOIN mRekening ON " +
                    " mRekening.IIDRekening=KORBELANJABARANG.IDRekening and KORBELANJABARANG.Jenis= RKBMD.Jenis WHERE isnull(RKBMDBarang.StatusRKA,0)<9 and  RKBMD.Jenis = 1 AND " +
                    " mskpd.id90 =  " + iddinas.ToString() + " AND Tahun = " + Tahun.ToString() + " ORDER BY RKBMD.IDUrusan, RKBMD.IDPRogram, RKBMD.IDKegiatan," +
                    " RKBMD.IDSUbKegiatan, KORBELANJABARANG.IDRekening, RKBMDBarang.IDBarang , RKBMDBarang.No";







                DataTable dt;
                dt = _dbHelper.ExecuteDataTable(SSQL, _connection.GetConnection());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RKBMD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDDInasKeuangan = DataFormat.GetInteger(dr["IDDInasKeuangan"]),
                                    Tahun = DataFormat.GetInteger(dr["Tahun"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDsubKegiatan"]),
                                    IDBarang = DataFormat.GetLong(dr["IDBarang"]),
                                    NamaBarang =  DataFormat.GetInteger(dr["No"])>0 ? DataFormat.GetString(dr["CaraPemenuhan"]): DataFormat.GetString(dr["NamaBarang"]),
                                    JumlahUsulan = DataFormat.GetInteger(dr["Jumlah"]),
                                    JumlahUsulanPerubahan = DataFormat.GetInteger(dr["JumlahPerubahan"]),
                                    IDrekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                    IDRKBMDBArang =  DataFormat.GetInteger(dr["IDRKBMDBArang"])

                                }).ToList();
                    }
                }
                return _lst;



            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return null;
            }
        }
        
    }
}
