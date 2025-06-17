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
    public class PotonganLogic:BP 
    {
        public PotonganLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPotongan";
        }
        
        public bool Simpan(Potongan dh)
        {
            try
            {

                SSQL = "SELECT *  from " + m_sNamaTabel + " WHERE IIDRekeningPotongan  = @IDrekening";

                DBParameterCollection paramCCek = new DBParameterCollection();
                paramCCek.Add(new DBParameter("@IDrekening", dh.IDPotongan));
              
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCCek);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        //SSQL = "UPdate " + m_sNamaTabel + " SET sNamaPotongan ='" + dh.Nama + "' , btInformasi= " + dh.Informasi.ToString()+ " WHERE iidRekeningPotongan = " + dh.IDPotongan.ToString();//
                        //_dbHelper.ExecuteNonQuery(SSQL);

                        SSQL = "UPDATE " + m_sNamaTabel + " SET sNamaPotongan=@psNamaPotongan, btInformasi=@tInformasi,idKodePusat=@idKodePusat  WHERE iidRekeningPotongan =@piidRekeningPotongan";

                        DBParameterCollection paramCUpdate  = new DBParameterCollection();
                        paramCUpdate.Add(new DBParameter("@psNamaPotongan", dh.Nama, DbType.String));
                        paramCUpdate.Add(new DBParameter("@tInformasi", dh.Informasi, DbType.Single));
                        paramCUpdate.Add(new DBParameter("@idKodePusat", dh.KodePusat, DbType.Int32));
                        paramCUpdate.Add(new DBParameter("@piidRekeningPotongan", dh.IDPotongan, DbType.UInt32));

                        _dbHelper.ExecuteNonQuery(SSQL, paramCUpdate);


                    }
                    else
                    {


                        SSQL = "INSERT INTO " + m_sNamaTabel + " (iidRekeningPotongan, sNamaPotongan, btInformasi,idKodePusat ) values ( " +
                          " @piidRekeningPotongan,@psNamaPotongan,@tInformasi,@idKodePusat ) ";//, @piNo, @pOnPerda)";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@piidRekeningPotongan", dh.IDPotongan, DbType.UInt32));
                        paramCollection.Add(new DBParameter("@psNamaPotongan", dh.Nama, DbType.String));
                        paramCollection.Add(new DBParameter("@tInformasi", dh.Informasi, DbType.Single));
                        paramCollection.Add(new DBParameter("@idKodePusat", dh.KodePusat , DbType.Int32 ));

                        
                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    }
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
        public List<Potongan> Get(int iMPN=1)
        {
            //1-> MPN
            //2-? Non MPN
            List<Potongan> _lst = new List<Potongan>();
            try
            {
                DataTable dt = new DataTable();
                if (iMPN >= 0)
                {
                    SSQL = "SELECT * FROM " + m_sNamaTabel + " where bPajak = @iPajak Order by IIDRekeningPotongan ";//
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@iPajak", iMPN));
                    dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                }
                else
                {
                    SSQL = "SELECT * FROM " + m_sNamaTabel + " Order by IIDRekeningPotongan ";//
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                }
               

                
                
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Potongan()
                                {
                                    IDPotongan = DataFormat.GetInteger(dr["IIDRekeningPotongan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekeningPotongan"]),
                                    Nama = DataFormat.GetString(dr["sNamaPotongan"]),
                                    Informasi= DataFormat.GetSingle(dr["bInformasi"]),
                                    KodePusat=DataFormat.GetInteger(dr["idKodePusat"]),
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
        public Potongan GetPotongan(int _idRekeningPotongan)
        {
            Potongan oPotongan= new Potongan();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IIDRekeningPotongan  = @IDrekening";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@IDrekening", _idRekeningPotongan));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection );
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oPotongan= new Potongan()
                        {
                            IDPotongan = DataFormat.GetInteger(dr["IIDRekeningPotongan"]),
                            IDRekening = DataFormat.GetInteger(dr["IIDRekeningPotongan"]),
                            Nama = DataFormat.GetString(dr["sNamaPotongan"]),
                            Informasi = DataFormat.GetSingle(dr["bInformasi"]),
                            KodePusat = DataFormat.GetInteger(dr["idKodePusat"]),
                               
                        };
                    }
                }
                return oPotongan;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oPotongan;
            }

        }
        public bool Hapus(int  _idRekeningPotongan)
        {
            
            try
            {
                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE WHERE IIDRekeningPotongan = @IDrekening";
              
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@IDrekening", _idRekeningPotongan));

               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
                
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public List<KodeSetor> GetKodeSetor(int KodeMap)
        {
                 List<KodeSetor> _lst = new List<KodeSetor>();
            try
            {

                SSQL = "Select * FROM Refsetor WHERE KodeMap = @KodeMap Order by KodeSetor";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KodeMap", KodeMap ));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new KodeSetor()
                                {
                                    KodeMap = DataFormat.GetInteger(dr["KodeMap"]),
                                    NamaSetor = DataFormat.GetString(dr["NamaSetor"]),
                                    Kode= DataFormat.GetString(dr["KodeSetor"]),
                                }).ToList();
                    }
                }
                return _lst;



            }
            catch (Exception ex) {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }
    }
}
