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
    public class KasdaLogic:BP 
    {
        public KasdaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mKasda";
            CekIDDinasDiTabel();
        }
        private void CekIDDinasDiTabel()
        {
          
        }

        private bool IsEmpty()
        {
            try
            {
                SSQL = "SELECT * from " + m_sNamaTabel;
                DataTable dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt.Rows.Count ==0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;


            }
        }
        public bool IsPPKD(int _pDinas)
        {
            try
            {
                SSQL = "SELECT * from " + m_sNamaTabel + " WHERE IDDinas= " + _pDinas.ToString();
                DataTable dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;


            }


        }
        public bool Simpan(Kasda oKasda)
        {
            try
            {
             
                if (IsEmpty() == true)
                {

                    SSQL = "INSERT INTO " + m_sNamaTabel + " (btKodeKategori,btKodeUrusan,btKodeSKPD,btKodeUK,IDDinas "+
                    "iIDRekeningBUD,iIDRekeningSilpaTB,iiDRekeningRKPPKD,iIDRekeningSilpa,IIDRekEstimasiSAL) values ( " +
                        " @pbtKodeKategori,@pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeUK,@pIDDinas,@piIDRekeningBUD,@piIDRekeningSilpaTB," +
                        "@piiDRekeningRKPPKD,@piIDRekeningSilpa,@pIIDRekEstimasiSAL)";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", oKasda.KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", oKasda.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", oKasda.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", oKasda.KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", oKasda.IDDInas, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piIDRekeningBUD", oKasda.RekKasda, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piIDRekeningSilpaTB", oKasda.RekSILPATB, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piiDRekeningRKPPKD", oKasda.RekRKPPKD, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piIDRekeningSilpa", oKasda.RekSILPA, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pIIDRekEstimasiSAL", oKasda.KodeEstimasiPadaSAL, DbType.Int64));
                     


                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {

                    SSQL = "UPDATE " + m_sNamaTabel + "  SET btKodeKategori=@pbtKodeKategori,btKodeUrusan=@pbtKodeUrusan,btKodeSKPD=@pbtKodeSKPD,"+
                        "btKodeUK=@pbtKodeUK,IDDinas=@pIDDinas,iIDRekeningBUD=@piIDRekeningBUD,iIDRekeningSilpaTB=@piIDRekeningSilpaTB," +
                        "iiDRekeningRKPPKD=@piiDRekeningRKPPKD,iIDRekeningSilpa=@piIDRekeningSilpa,IIDRekEstimasiSAL=@pIIDRekEstimasiSAL";
                    
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", oKasda.KodeKategori, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", oKasda.KodeUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", oKasda.KodeSKPD, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", oKasda.KodeUK, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDinas", oKasda.IDDInas, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piIDRekeningBUD", oKasda.RekKasda, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piIDRekeningSilpaTB", oKasda.RekSILPATB, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piiDRekeningRKPPKD", oKasda.RekRKPPKD, DbType.Int64));
                    paramCollection.Add(new DBParameter("@piIDRekeningSilpa", oKasda.RekSILPA, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pIIDRekEstimasiSAL", oKasda.KodeEstimasiPadaSAL, DbType.Int64));
                    



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


        public bool isPPKD(int piddinas)
        {
            Kasda oKasda = new Kasda();
            oKasda = Get();
            return (oKasda.IDDInas == piddinas);

        }
        public bool SetDinasKasda(int _pIDDInas)
        {
            try
            {
                int pKategori = DataFormat.GetInteger(_pIDDInas.ToString().Trim().Substring(0, 1));
                int pUrusan = DataFormat.GetInteger(_pIDDInas.ToString().Trim().Substring(1, 2));
                int pSKPD = DataFormat.GetInteger(_pIDDInas.ToString().Trim().Substring(3, 2));
                int pUnitKerja = DataFormat.GetInteger(_pIDDInas.ToString().Trim().Substring(5, 2));



                SSQL = "UPDATE " + m_sNamaTabel + "  SET btKodeKategori=@pbtKodeKategori,btKodeUrusan=@pbtKodeUrusan,btKodeSKPD=@pbtKodeSKPD,btKodeUK=@pbtKodeUK,IDDinas=@pIDDinas";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pbtKodeKategori", pKategori, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeUrusan", pUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeSKPD", pSKPD, DbType.Int32));
                paramCollection.Add(new DBParameter("@pbtKodeUK", pUnitKerja, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDinas", _pIDDInas, DbType.Int64));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }
        public Kasda Get()
        {
            Kasda oKasda = new Kasda();
            try
            {
                SSQL = "SELECT * from " + m_sNamaTabel;
                DataTable dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    oKasda = new Kasda()
                    {
                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                        KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                        KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                        KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                        KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                        RekKasda = DataFormat.GetLong(dr["iIDRekeningBUD"]),
                        RekSILPATB = DataFormat.GetLong(dr["iIDRekeningSilpaTB"]),
                        RekRKPPKD = DataFormat.GetLong(dr["iiDRekeningRKPPKD"]),
                        RekSILPA = DataFormat.GetLong(dr["iIDRekeningSilpa"]),
                        KodeKategoriPPKD = DataFormat.GetLong(dr["btKodeKategori"]),
                        KodeUrusanPPKD = DataFormat.GetLong(dr["btKodeUrusan"]),
                        KodeSKPDPPKD = DataFormat.GetLong(dr["btKodeSKPD"]),
                        KodeUKPPKD = DataFormat.GetLong(dr["btKodeUK"]),
                        KodeEstimasiPadaSAL = DataFormat.GetLong(dr["IIDRekEstimasiSAL"]),

                    };
                                       
                }
                _isError = false;
                return oKasda;
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
