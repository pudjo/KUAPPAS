using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using Formatting;
using System.Data;
using System.Data.OleDb;
using BP;


namespace BP.Bendahara
{
    public class RegisterSPPLogic:BP
    {

        public RegisterSPPLogic(int _tahun)
            : base(_tahun)
        {
            Tahun = _tahun;
        }
        public List<RegisterSPP> Get(ref ParameterLaporanBKU param,   int status)
        {
            status = 5;
            int IDDInas= param.Skpd.ID;
            int ppkd = param.PPKD;
            Periode periode = param.periode;

            List<RegisterSPP> _lst = new List<RegisterSPP>();
            try
            {

                SSQL = "Select tSPP.btJenis,tSPP.cJumlah as cJumlah " +
                     " From tSPP  WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                     " AND tSPP.btJenis=0 AND tSPP.bPPKD = " + ppkd.ToString() +
                     " AND tSPP.dtSPP >=" + periode.TanggalAwalTahun.ToSQLFormat() + " AND tSPP.dtSPP < " + periode.TanggalAwal.ToSQLFormat() + "";

                SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " UNION ALL Select tSPP.btJenis,SUM(tSPPRekening.cJumlah) as cJumlah " +
                          " From tSPP  INNER JOIN tSPPRekening ON tSPP.inourut= tSPPRekening.inourut WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                          " AND tSPP.btJenis <=5 AND tSPP.bPPKD = " + ppkd.ToString() +
                          " AND tSPP.dtSPP >=" + periode.TanggalAwalTahun.ToSQLFormat() + " AND tSPP.dtSPP < " + periode.TanggalAwal.ToSQLFormat() + "";


                SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " GROUP BY tSPP.btJenis ";
                


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RegisterSPP()
                                {
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),

                                }).ToList();
                    }
                }

                foreach (RegisterSPP r in _lst)
                {
                    param.UPPeriodeLalu = r.Jenis == 0 ? r.Jumlah : 0;
                    param.GUPeriodeLalu = r.Jenis == 1 ? r.Jumlah : 0;
                    param.TUPeriodeLalu = r.Jenis == 2 ? r.Jumlah : 0;
                    param.LSPeriodeLalu= r.Jenis == 3 ? r.Jumlah : 0;
                    param.GJPeriodeLalu = r.Jenis == 4 ? r.Jumlah : 0;
                    param.PPKDPeriodeLalu = r.Jenis == 5 ? r.Jumlah : 0;


                }

               




                SSQL = "Select tSPP.btJenis,tSPP.iNoUrut, tSPP.sNoSPP,dtSPP, tSPP.sKeteranganPekerjaan AS sPeruntukan,tSPP.sPeruntukan as sPeruntukanSPP, tSPP.iTahun, tSPP.btKodeKategori,  " +
                      "tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK, " +
                      "tSPP.btJenis, tSPP.cJumlah as cJumlah " +
                      " From tSPP  WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                      " AND tSPP.btJenis=0 AND tSPP.bPPKD = " + ppkd.ToString() +
                      " AND tSPP.dtSPP BETWEEN " + periode.TanggalAwal.ToSQLFormat() + " AND " + periode.TanggalAkhir.ToSQLFormat() + "";


                SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL +" UNION ALL Select tSPP.btJenis,tSPP.iNoUrut, tSPP.sNoSPP,dtSPP, tSPP.sKeteranganPekerjaan AS sPeruntukan,tSPP.sPeruntukan as sPeruntukanSPP, tSPP.iTahun, tSPP.btKodeKategori,  " +
                          "tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK, " +
                          "tSPP.btJenis,  SUM(tSPPRekening.cJumlah) as cJumlah " +
                          " From tSPP  INNER JOIN tSPPRekening ON tSPP.inourut= tSPPRekening.inourut WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                          " AND tSPP.btJenis <=5 AND tSPP.bPPKD = " + ppkd.ToString() +
                          " AND tSPP.dtSPP BETWEEN " + periode.TanggalAwal.ToSQLFormat() + " AND " + periode.TanggalAkhir.ToSQLFormat() + "";


                SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " GROUP BY tSPP.btJenis,tSPP.iNoUrut, tSPP.sNoSPP,dtSPP, tSPP.sKeteranganPekerjaan,tSPP.sPeruntukan , tSPP.iTahun, tSPP.btKodeKategori,  " +
                         "tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK, tSPP.btJenis ";
                SSQL = SSQL + " ORDER BY tSPP.btKodeuk,tSPP.dtSPP, tSPP.sNoSPP";



              //  DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RegisterSPP()
                                {
                                    TanggalSPP = DataFormat.GetDateTime(dr["dtSPP"]),
                                    NoSPP = DataFormat.GetString(dr["sNoSPP"]),
                                    Uraian = DataFormat.GetString(dr["sPeruntukanSPP"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),

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
        public List<RegisterSPP> GetSP2D(ref ParameterLaporanBKU param, int status)
        {
            status = 5;
            int IDDInas = param.Skpd.ID;
            int ppkd = param.PPKD;
            Periode periode = param.periode;

            List<RegisterSPP> _lst = new List<RegisterSPP>();
            try
            {

                string _kolomTanggal;
                _kolomTanggal= status ==3?"dtTerbitSP2D":"dtBukukas";
                
                
                SSQL = "Select tSPP.btJenis,tSPP.cJumlah as cJumlah " +
                     " From tSPP  WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                     " AND tSPP.btJenis=0 AND tSPP.bPPKD = " + ppkd.ToString() +
                     " AND tSPP." + _kolomTanggal + " >=" + periode.TanggalAwalTahun.ToSQLFormat() + " AND tSPP." + _kolomTanggal + " < " + periode.TanggalAwal.ToSQLFormat() + "";

                SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " UNION ALL Select tSPP.btJenis,SUM(tSPPRekening.cJumlah) as cJumlah " +
                          " From tSPP  INNER JOIN tSPPRekening ON tSPP.inourut= tSPPRekening.inourut WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                          " AND tSPP.btJenis <=5 AND tSPP.bPPKD = " + ppkd.ToString() +
                          " AND tSPP." + _kolomTanggal + " >=" + periode.TanggalAwalTahun.ToSQLFormat() + " AND tSPP." + _kolomTanggal + " < " + periode.TanggalAwal.ToSQLFormat() + "";

                if (IDDInas >0 )
                SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " GROUP BY tSPP.btJenis ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RegisterSPP()
                                {
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),

                                }).ToList();
                    }
                }

                foreach (RegisterSPP r in _lst)
                {
                    param.UPPeriodeLalu = r.Jenis == 0 ? r.Jumlah : 0;
                    param.GUPeriodeLalu = r.Jenis == 1 ? r.Jumlah : 0;
                    param.TUPeriodeLalu = r.Jenis == 2 ? r.Jumlah : 0;
                    param.LSPeriodeLalu = r.Jenis == 3 ? r.Jumlah : 0;
                    param.GJPeriodeLalu = r.Jenis == 4 ? r.Jumlah : 0;
                    param.PPKDPeriodeLalu = r.Jenis == 5 ? r.Jumlah : 0;


                }






                SSQL = "Select tSPP.btJenis,tSPP.iNoUrut, tSPP.sNoSP2D, tSPP." + _kolomTanggal + " , mskpd.sNamaSKPD, tSPP.sKeteranganPekerjaan AS sPeruntukan,tSPP.sPeruntukan as sPeruntukanSPP, tSPP.iTahun, tSPP.btKodeKategori,  " +
                      "tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK, " +
                      "tSPP.btJenis, tSPP.cJumlah as cJumlah " +
                      " From tSPP  INNER JOIN mSKPD on  tSPP.IDDINas = mSKPD.ID WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                      " AND tSPP.btJenis=0 AND tSPP.bPPKD = " + ppkd.ToString() +
                      " AND tSPP." + _kolomTanggal + " BETWEEN " + periode.TanggalAwal.ToSQLFormat() + " AND " + periode.TanggalAkhir.ToSQLFormat() + "";

                if (IDDInas > 0)
                    SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " UNION ALL Select tSPP.btJenis,tSPP.iNoUrut, tSPP.sNoSP2D,tSPP." + _kolomTanggal + ", mskpd.sNamaSKPD , tSPP.sKeteranganPekerjaan AS sPeruntukan,tSPP.sPeruntukan as sPeruntukanSPP, tSPP.iTahun, tSPP.btKodeKategori,  " +
                          "tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK, " +
                          "tSPP.btJenis,  SUM(tSPPRekening.cJumlah) as cJumlah " +
                          " From tSPP  INNER JOIN tSPPRekening ON tSPP.inourut= tSPPRekening.inourut  INNER JOIN mSKPD on  tSPP.IDDINas = mSKPD.ID  WHERE tSPP.iStatus <= " + status.ToString() + " AND tSPP.iTahun=" + Tahun.ToString() +
                          " AND tSPP.btJenis <=5 AND tSPP.bPPKD = " + ppkd.ToString() +
                          " AND tSPP." + _kolomTanggal + "  BETWEEN " + periode.TanggalAwal.ToSQLFormat() + " AND " + periode.TanggalAkhir.ToSQLFormat() + "";

                if (IDDInas > 0)
                    SSQL = SSQL + " AND tSPP.IDDInas = " + IDDInas.ToString();

                SSQL = SSQL + " GROUP BY tSPP.btJenis,tSPP.iNoUrut, tSPP.sNoSP2D, tSPP." + _kolomTanggal + " , mskpd.sNamaSKPD, tSPP.sKeteranganPekerjaan,tSPP.sPeruntukan , tSPP.iTahun, tSPP.btKodeKategori,  " +
                         "tSPP.btKodeUrusan, tSPP.btKodeSKPD, tSPP.btKodeUK, tSPP.btJenis ";
                SSQL = SSQL + " ORDER BY tSPP." + _kolomTanggal + "";



                //  DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RegisterSPP()
                                {
                                    TanggalSPP = DataFormat.GetDateTime(dr[_kolomTanggal]),
                                    Keterangan = DataFormat.GetString (dr["sNamaSKPD"]),
                                    NoSP2D= DataFormat.GetString(dr["sNoSP2D"]),
                                    Uraian = DataFormat.GetString(dr["sPeruntukanSPP"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),

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
