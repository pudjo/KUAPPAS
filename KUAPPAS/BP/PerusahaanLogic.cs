using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO;
using Formatting;
using DataAccess;

namespace BP
{
    public class PerusahaanLogic:BP 
    {
   
        public PerusahaanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPerusahaan";
        }

        public int Simpan(Perusahaan p)
        {
            try
            {
                int newID = 0;
                if (p.IDPerusahaan == 0)
                { 
                    newID =GetMaxIDNoYear("IDPerusahaan");

                    SSQL = "INSERT INTO mPerusahaan ( IDPerusahaan,sNamaPerusahaan , btBentuk, sAlamat, sPimpinan,srekening, sNPWP,sBank,KodeBank,NamaDlmRekeningBank,KeteranganNamaBank) values (" +
                        newID.ToString() + ",'" + p.NamaPerusahaan + "'," + p.Bentuk.ToString() + ",'" + p.Alamat + "','" + p.Pimpinan + "','" + p.Rekening + "','" + p.NPWP + "','" + p.Bank + "','" + p.KodeBank + "','" + p.NamaDalamRekeningBank + "','" + p.KeteranganNamaBank + "')";

                  } else {
                      newID = p.IDPerusahaan;
                      SSQL = " UPDATE mPerusahaan SET  sNamaPerusahaan= '" + p.NamaPerusahaan + "' , btBentuk=" + p.Bentuk + ", sAlamat='" + p.Alamat + "', sPimpinan='" + p.Pimpinan + "',srekening='" +
                          p.Rekening + "', sNPWP='" + p.NPWP + "',sBank='" + p.Bank + "',KodeBank='" + p.KodeBank + "',NamaDlmRekeningBank='" + p.NamaDalamRekeningBank + "',KeteranganNamaBank='" + p.KeteranganNamaBank + "'  WHERE IDPerusahaan=" + p.IDPerusahaan.ToString();

                }

                _dbHelper.ExecuteNonQuery(SSQL);
                return newID;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return 0;
            }

        }

        public Perusahaan GetByID(int pIDPErusahaan)
        {
            Perusahaan oPerusahaan= new Perusahaan();
            try
            {
                SSQL = "SELECT * FROM mPerusahaan Where IDPerusahaan = " +  pIDPErusahaan.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr= dt.Rows[0];
                        oPerusahaan =new Perusahaan()
                                {
                                     IDPerusahaan = DataFormat.GetInteger(dr["IDPerusahaan"]),
                                     NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                                     Bentuk = DataFormat.GetInteger(dr["btBentuk"]),
                                     Alamat = DataFormat.GetString(dr["sAlamat"]),
                                     Pimpinan = DataFormat.GetString(dr["sPimpinan"]),
                                     Rekening = DataFormat.GetString(dr["srekening"]),
                                     NPWP = DataFormat.GetString(dr["sNPWP"]),
                                     Bank = DataFormat.GetString(dr["sBank"]),
                                     KodeBank = DataFormat.GetString(dr["KodeBank"]),
                                     NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                                     KeteranganNamaBank = DataFormat.GetString(dr["KeteranganNamaBank"]),

                                };
                    }
                }
                return oPerusahaan;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public List<Perusahaan> Get(string nama)
        {
            List<Perusahaan> _lst = new List<Perusahaan>();
            try
            {
                SSQL = "SELECT * FROM mPerusahaan where sNamaPerusahaan like '%" + nama +"%'";
              DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Perusahaan ()
                                
                        {
                            IDPerusahaan = DataFormat.GetInteger(dr["IDPerusahaan"]),
                            NamaPerusahaan = DataFormat.GetString(dr["sNamaPerusahaan"]),
                            Bentuk = DataFormat.GetInteger(dr["btBentuk"]),
                            Alamat = DataFormat.GetString(dr["sAlamat"]),
                            Pimpinan = DataFormat.GetString(dr["sPimpinan"]),
                            Rekening = DataFormat.GetString(dr["srekening"]),
                            NPWP = DataFormat.GetString(dr["sNPWP"]),
                            Bank = DataFormat.GetString(dr["sBank"]),
                            KodeBank = DataFormat.GetString(dr["KodeBank"]),
                            NamaDalamRekeningBank = DataFormat.GetString(dr["NamaDlmRekeningBank"]),
                            KeteranganNamaBank = DataFormat.GetString(dr["KeteranganNamaBank"]),
                            
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
  