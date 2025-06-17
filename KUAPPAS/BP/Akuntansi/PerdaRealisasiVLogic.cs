using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Formatting;
using KUAPPAS.DataAccess9.DTO.Akuntansi;


namespace BP.Akuntansi
{
    public class PerdaRealisasiVLogic:BP
    {
        public PerdaRealisasiVLogic(int tahun) : base(tahun)
        {

        }

        public List<PerdaFungsi> GetPerdaVRealisasi050(ParameterLaporan _p)
        {
            List<PerdaFungsi> _lst = new List<PerdaFungsi>();
            try
            {
                string tanggal = "'" + _p.dTanggal.Month.ToString() + "/" + _p.dTanggal.Day.ToString() + "/" + _p.dTanggal.Year.ToString() + "'";


                GetKolom(_p.Tahap);
                _namaKolom1 = "cJumlahABT";
                _namaKolom2 = "Realisasi";

                SSQL = "";
                if (_p.Tahun < 2022)
                {
                    SSQL = "select * from vwPerda_1_V order by KOdeFungsi, KodeSUbFungsi , kodekategori ,KodeUrusan";
                }
                else
                {
                    SSQL = " select * from dbo.fnPerdaFungsi(" + _p.Tahun.ToString() + " ," + _p.TanggalRealisasi.ToSQLFormat() + ") order by KOdeFungsi, KodeSUbFungsi , kodekategori ,KodeUrusan";

                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerdaFungsi()
                                {

                                    Level = DataFormat.GetInteger(dr["Level"]),



                                    KodeFungsi = DataFormat.GetInteger(dr["KodeFungsi"]),
                                    KodeSubFungsi = DataFormat.GetInteger(dr["KodeSubFungsi"]),
                                    KodeKategori = DataFormat.GetInteger(dr["Kodekategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["KodeUrusan"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),  //DataFormat.GetInteger(dr["Level"]) < 5 ? DataFormat.GetString(dr["Nama"]).ToUpper() : DataFormat.GetString(dr["Nama"]),
                                    AnggaranOperasi = DataFormat.GetDecimal(dr["AnggaranOperasi"]),
                                    AnggaranModal= DataFormat.GetDecimal(dr["AnggaranModal"]),
                                    AnggaranTakTerduga = DataFormat.GetDecimal(dr["AnggaranTakTerduga"]),

                                    AnggaranTransfer = DataFormat.GetDecimal(dr["AnggaranTransfer"]),
                                    RealisasiOperasi= DataFormat.GetDecimal(dr["RealisasiOperasi"]),//+ DataFormat.GetDecimal(dr["BBJ"]) + DataFormat.GetDecimal(dr["BP"]) + DataFormat.GetDecimal(dr["BT"])),
                                    RealisasiModal = DataFormat.GetDecimal(dr["RealisasiModal"]),
                                    RealisasiTakTerduga = DataFormat.GetDecimal(dr["RealisasiTakTerduga"]),
                                    RealisasiTransfer = DataFormat.GetDecimal(dr["RealisasiTRANSFER"]),
                                   //BTMurni = DataFormat.GetDecimal(dr["RealisasiTransfer"]),

                                }).ToList();

                    }
                }
                return _lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
    }
}
