using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using DataAccess;
using Formatting;


namespace BP
{
    public class RegisterSPDLogic: BP
    {
        public RegisterSPDLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
        }
        public List<RegisterSPD> GetData(int _IDDInas, DateTime tanggalAwal, DateTime TanggalAkhir, Single ? bJenis=-1 )
        {
            List<RegisterSPD> _lst = new List<RegisterSPD>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                
                    SSQL = "SELECT tSPD.sNoSPD, tSPD.sKEterangan,tSPD.dtSPD, SUM(tSPDKEgiatan.cJumlah) as Jumlah, mSKPD.sNamaSKPD FROM tSPD  inner join tSPDKEgiatan on tSPD.inourut = tSPDKegiatan.inourut " +
                            " INNER JOIN mSKPD ON mSKPD.ID = tSPD.IDDInas WHERE 1>0 ";
                    if (_IDDInas> 0){
                        SSQL=SSQL + " AND  tSPD.IDDInas ="+ _IDDInas.ToString();
                    }
                    if (bJenis > -1){
                        SSQL = SSQL + " AND  tSPD.btJenis =" + bJenis.ToString();
                    }
                    SSQL = SSQL + " and dtSPD between @pdtAwal AND @pdtAkhir GROUP BY tSPD.sNoSPD, tSPD.sKEterangan,tSPD.dtSPD,mSKPD.sNamaSKPD order by dtSPD, sNoSPD ";

                    paramCollection.Add(new DBParameter("@pdtAwal", tanggalAwal));
                    paramCollection.Add(new DBParameter("@pdtAkhir", TanggalAkhir));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RegisterSPD()
                                {
                                    //'No= DataFormat.GetInteger(dr["ID"]),
                                    NoSPD = DataFormat.GetString(dr["snospd"]),
                                    Uraian = DataFormat.GetString (dr["sKeterangan"]),
                                    UnitKerja = DataFormat.GetString(dr["sNamaSKPD"]),
                                    TanggalSPD = DataFormat.FormatTanggal(DataFormat.GetDateTime (dr["dtSPD"])),                                    
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]).ToRupiahInReport()                                    
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
        public decimal GetJumlah(int _IDDInas, DateTime tanggalAwal, DateTime TanggalAkhir, Single? bJenis = -1)
        {
            decimal dRet=0;
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                SSQL = "SELECT SUM(tSPDKEgiatan.cJumlah) as Jumlah, mSKPD.sNamaSKPD FROM tSPD  inner join tSPDKEgiatan on tSPD.inourut = tSPDKegiatan.inourut " +
                        " INNER JOIN mSKPD ON mSKPD.ID = tSPD.IDDInas WHERE 1>0 ";
                if (_IDDInas > 0)
                {
                    SSQL = SSQL + " AND  tSPD.IDDInas =" + _IDDInas.ToString();
                }
                if (bJenis > -1)
                {
                    SSQL = SSQL + " AND  tSPD.btJenis =" + bJenis.ToString();
                }
                SSQL = SSQL + " and dtSPD between @pdtAwal AND @pdtAkhir GROUP BY tSPD.sNoSPD, tSPD.sKEterangan,tSPD.dtSPD,mSKPD.sNamaSKPD order by dtSPD, sNoSPD ";

                paramCollection.Add(new DBParameter("@pdtAwal", tanggalAwal));
                paramCollection.Add(new DBParameter("@pdtAkhir", TanggalAkhir));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                         DataRow dr =dt.Rows[0];
                        dRet=DataFormat.GetDecimal(dr["Jumlah"]);
                    }
                }
                return dRet;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return dRet;
            }
        }
    }
}
