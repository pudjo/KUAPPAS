using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using DTO;
using BP;
using Formatting;

namespace BP
{
    public class DasarHukumLogic:BP 
    {
        public DasarHukumLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mDasarHukum";
            CekTahunDiTabel();
        }
        private void CekTahunDiTabel()
        {
            try
            {
                SSQL = "Select OnPerda from " + m_sNamaTabel;
                DataTable dt=_dbHelper.ExecuteDataTable(SSQL);
           


            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                SSQL = "ALTER TABLE " + m_sNamaTabel + " ADD OnPerda smallint";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " update mDasarHukum set onPerda= 0 where OnPerda is null";
                _dbHelper.ExecuteNonQuery(SSQL);

            }
        }
        public bool Simpan(DasarHukum dh)
        {
            try
            {

                SSQL = "SELECT *  from " + m_sNamaTabel + " WHERE IIDRekening = " + dh.IDRekening + " AND iTahun =" + dh.Tahun.ToString() + " AND iNo="+ dh.NoUrut.ToString() + " AND OnPerda = " + dh.OnPerda.ToString();
              
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        SSQL = "UPdate " + m_sNamaTabel + " SET sKeterangan ='" + dh.Keterangan + "'  WHERE IIDRekening = " + dh.IDRekening + " AND iTahun =" + dh.Tahun.ToString() + " AND iNo=" + dh.NoUrut.ToString() + " AND OnPerda = " + dh.OnPerda.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);
                    }
                    else
                    {


                        SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDInas, btKodeKategori, btKodeUrusan, btKodeSKPD,btKodeUK,IIDRekening,sKeterangan, iNo, OnPerda) values ( " +
                          " @piTahun, @pIDDInas, @pbtKodeKategori, @pbtKodeUrusan, @pbtKodeSKPD,@pbtKodeUK,@pIIDRekening,@psKeterangan, @piNo, @pOnPerda)";

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@piTahun", dh.Tahun, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pIDDInas", dh.IDDInas, DbType.Int64));
                        paramCollection.Add(new DBParameter("@pbtKodeKategori", dh.KodeKategori, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeUrusan", dh.KodeUrusan, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeSKPD", dh.KodeSKPD, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pbtKodeUK", dh.KodeUK, DbType.Int32));
                        paramCollection.Add(new DBParameter("@pIIDRekening", dh.IDRekening, DbType.Int64));
                        paramCollection.Add(new DBParameter("@psKeterangan", dh.Keterangan, DbType.String));
                        paramCollection.Add(new DBParameter("@piNo", dh.NoUrut, DbType.Int16));
                        paramCollection.Add(new DBParameter("@pOnPerda", dh.OnPerda, DbType.Int16));


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
        public List<DasarHukum> Get(int _iTahun, Single pOnPerda)
        {
            List<DasarHukum> _lst = new List<DasarHukum>();
            try
            {
                SSQL = "SELECT A.*, B.sNamaRekening as Nama FROM "+ m_sNamaTabel + " A Left  JOIN mRekening B ON A.IIDRekening= B.IIDRekening WHERE OnPerda = " + pOnPerda.ToString() + " ORDER BY IIDRekening,iNo ";
                //A.iTahun ="+ _iTahun.ToString() + " AND 
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new DasarHukum()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDInas= DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan= DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD= DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK= DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDRekening= DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["Nama"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    NoUrut = DataFormat.GetSingle(dr["iNo"]),
                                    OnPerda = DataFormat.GetSingle(dr["OnPerda"])
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
        public List<DasarHukum>GetByIDRekening(List<Rekening> lst)
        {
            List<DasarHukum> _lst = new List<DasarHukum>();
            try
            {
                string sIDRekening = "(";
                foreach(Rekening l in lst)
                {
                    string s = l.ID.ToString().Substring(0, 7);
                    if (sIDRekening.Contains(s) == false)
                    {
                        sIDRekening = sIDRekening + l.ID.ToString().Substring(0, 7) + ",";
                    }
                }
                sIDRekening = sIDRekening + "-1)";

                SSQL = "SELECT A.*, B.sNamaRekening as Nama FROM " + m_sNamaTabel + 
                    " A Left  JOIN mRekening B ON A.IIDRekening= B.IIDRekening WHERE "+
                    " A.iidrekening in " + sIDRekening + " ORDER BY IIDRekening,iNo ";
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new DasarHukum()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["Nama"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    NoUrut = DataFormat.GetSingle(dr["iNo"]),
                                    OnPerda = DataFormat.GetSingle(dr["OnPerda"])
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
        
        public DasarHukum Get(int _iTahun, long  _idRekening)
        {
            DasarHukum oDasarHukum= new DasarHukum();
            try
            {
                SSQL = "SELECT A.*,B.sNamaRekening as Nama FROM " + m_sNamaTabel + " A inner join mRekening B A.IIDRekening = B.IIDRekening WHERE A.iTahun =" + _iTahun.ToString() + " AND A.IIDRekening=" + _idRekening.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oDasarHukum= new DasarHukum()
                        {
                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                NamaRekening = DataFormat.GetString(dr["Nama"]),
                                Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                NoUrut = DataFormat.GetSingle(dr["iNo"]),
                                OnPerda = DataFormat.GetSingle(dr["OnPerda"])
                        };
                    }
                }
                return oDasarHukum;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oDasarHukum;
            }

        }
        public bool Hapus(int _iTahun, long _idRekening, int piNo)
        {
            
            try
            {
                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE iTahun =" + _iTahun.ToString() + " AND IIDRekening=" + _idRekening.ToString() + " AND iNO= " + piNo.ToString();
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
