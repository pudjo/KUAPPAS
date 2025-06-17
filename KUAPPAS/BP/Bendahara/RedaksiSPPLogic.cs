using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using DTO;
using DTO.Bendahara;
using BP;
using Formatting;


namespace BP.Bendahara
{
    public class RedaksiSPPLogic: BP 
    {
        public RedaksiSPPLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mRedaksiSPP";
        }

        public bool Simpan(RedaksiSPP bUP)
        {
            try
            {

                SSQL = "DELETE from mRedaksiSPP where iJenisLaporan=" + bUP.JenisLaporan.ToString() + " AND btJenis = " + bUP.Jenis.ToString() + " AND No = " + bUP.No.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);



                SSQL = "INSERT INTO mRedaksiSPP (iJenisLaporan, btJenis, No,sPernyataan) values (" +
                      bUP.JenisLaporan.ToString() + "," + bUP.Jenis.ToString() + "," + bUP.No.ToString() + ",'" + bUP.Redaksi+ "')";
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

        public List<RedaksiSPP> Get()
        {
            List<RedaksiSPP> _lst = new List<RedaksiSPP>();
            try
            {

                SSQL = "SELECT mRedaksiSPP.* FROM mRedaksiSPP ORDER BY mRedaksiSPP.iJenisLaporan, mRedaksiSPP.btJenis";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RedaksiSPP()
                                {

                                    JenisLaporan= DataFormat.GetInteger(dr["iJenisLaporan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    No = DataFormat.GetInteger(dr["No"]),
                                    Redaksi= DataFormat.GetString(dr["sPernyataan"]),

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

        public RedaksiSPP GetByJenis (int JenisLaporan, int Jenis, int No)
        {
            RedaksiSPP oRedaksiSPP = new RedaksiSPP();
            try
            {

                SSQL = "SELECT * FROM mRedaksiSPP where iJenisLaporan = " + JenisLaporan.ToString() + 
                    " AND  btJenis=" + Jenis.ToString() + " AND No=" + No.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //nk (btKodeRedaksiSPP,sNamaRedaksiSPP,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) 
                        oRedaksiSPP = new RedaksiSPP()
                        {
                            JenisLaporan = DataFormat.GetInteger(dr["iJenisLaporan"]),
                            Jenis = DataFormat.GetInteger(dr["btJenis"]),
                            No = DataFormat.GetInteger(dr["No"]),
                            Redaksi = DataFormat.GetString(dr["sPernyataan"]),
                        };
                    }
                }
                return oRedaksiSPP;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oRedaksiSPP;
            }

        }
        public RedaksiSPP GetByJenis(int JenisLaporan, int Jenis)
        {
            RedaksiSPP oRedaksiSPP = new RedaksiSPP();
            try
            {

                SSQL = "SELECT * FROM mRedaksiSPP where iJenisLaporan = " + JenisLaporan.ToString() +
                    " AND  btJenis=" + Jenis.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //nk (btKodeRedaksiSPP,sNamaRedaksiSPP,sNoRekening, btKodekategori, btKodeUrusan, btKodeSKPD, btKodeUK,btJenisBendahara,IIDRekening) 
                        oRedaksiSPP = new RedaksiSPP()
                        {
                            JenisLaporan = DataFormat.GetInteger(dr["iJenisLaporan"]),
                            Jenis = DataFormat.GetInteger(dr["btJenis"]),
                            No = DataFormat.GetInteger(dr["No"]),
                            Redaksi = DataFormat.GetString(dr["sPernyataan"]),
                        };
                    }
                }
                return oRedaksiSPP;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oRedaksiSPP;
            }

        }
        public List<RedaksiSPP> GetByJenisSPP(int JenisLaporan, int Jenis)
        {
            List<RedaksiSPP> lst = new List<RedaksiSPP>();
            try
            {

                SSQL = "SELECT * FROM mRedaksiSPP where iJenisLaporan = " + JenisLaporan.ToString() +
                    " AND  btJenis=" + Jenis.ToString() + " ORDER BY No";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new RedaksiSPP()
                                {

                                    JenisLaporan = DataFormat.GetInteger(dr["iJenisLaporan"]),
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    No = DataFormat.GetInteger(dr["No"]),
                                    Redaksi = DataFormat.GetString(dr["sPernyataan"]),

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

        }
        public bool Hapus(RedaksiSPP bUP)
        {

            try
            {


                SSQL = "DELETE from mRedaksiSPP where iJenisLaporan=" + bUP.JenisLaporan.ToString() + " AND btJenis = " + bUP.Jenis.ToString() + " AND No = " + bUP.No.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
    }
}
