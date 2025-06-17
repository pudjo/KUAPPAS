using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using DataAccess;
using System.Data;
using Formatting;


namespace BP
{
    public class TSumberDanaLogic:BP 
    {
        public TSumberDanaLogic(int _pTahun, int profile)
            : base(_pTahun, 0, profile )
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSumberDanaRekening";

        }
        public List<TSumberDana> Get()
        {
            List<TSumberDana> _lst = new List<TSumberDana>();
            try
            {
                SSQL = "SELECT tSumberDanaRekening.*, mSumberdana.sNama  as Nama FROM tSumberDanaRekening inner join mSumberDana on tSumberDanaRekening.iSumberDana = mSumberDana.ID   Order BY IDDInas, IDUrusan, IdKegiatan , IIDRekneing";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSumberDana()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    IDSUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    JumlahMurni = DataFormat.GetInteger(dr["cJumlahMurni"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    TahapInput = DataFormat.GetSingle(dr["btTahapInput"])

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

        //public List<TSumberDana> Get(int _Tahun, int _IDDInas, int _idUrusan, int _idProgram, int _idKegiatan )
        //{
        //    List<TSumberDana> mListUnit = new List<TSumberDana>();
        //    try
        //    {
        //        SSQL = "SELECT tSumberDanaRekening.*, mSumberdana.sNama  as Nama FROM tSumberDanaRekening  inner join mSumberDana on tSumberDanaRekening.iSumberDana = mSumberDana.ID  " + 
        //            " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas =" + _IDDInas.ToString()+ " AND IDUrusan =" + _idUrusan.ToString() + " AND IDProgram =" + _idProgram.ToString() + 
        //            " AND IDKegiatan =" + _idKegiatan.ToString() + " Order BY IIDRekneing";

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new TSumberDana()
        //                        {
        //                            Tahun = DataFormat.GetSingle(dr["iTahun"]),
        //                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                            IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
        //                            IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
        //                            IDSUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
        //                            JumlahMurni = DataFormat.GetInteger(dr["cJumlahMurni"]),
        //                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
        //                            Nama = DataFormat.GetString(dr["Nama"]),
        //                            TahapInput = DataFormat.GetSingle(dr["btTahapInput"])

        //                        }).ToList();
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}
        public List<TSumberDana> Get(int _Tahun, int _IDDInas, int _idUrusan, int _idProgram, int _idKegiatan, long idsubkegiatan)
        {
            List<TSumberDana> _lst = new List<TSumberDana>();
            try
            {
                SSQL = "SELECT tSumberDanaRekening.*, mSumberdana.sNama  as Nama FROM tSumberDanaRekening  inner join mSumberDana on tSumberDanaRekening.iSumberDana = mSumberDana.iidrekening  " +
                    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas =" + _IDDInas.ToString() + " AND IDUrusan =" + _idUrusan.ToString() + " AND IDProgram =" + _idProgram.ToString() +
                    " AND IDKegiatan =" + _idKegiatan.ToString() + "  AND isnull( IDSubKegiatan,0) =" + idsubkegiatan.ToString() + " Order BY IIDRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                  

                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSumberDana()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    IDSUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    JumlahMurni = DataFormat.GetInteger(dr["cJumlahMurni"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    TahapInput = DataFormat.GetSingle(dr["btTahapInput"])

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
        public List<TSumberDana> Get(int _Tahun, int _IDDInas, int _idKegiatan)
        {
            List<TSumberDana> _lst = new List<TSumberDana>();
            try
            {
                SSQL = "SELECT tSumberDanaRekening.*, mSumberdana.sNama  as Nama FROM tSumberDanaRekening  inner join mSumberDana on tSumberDanaRekening.iSumberDana = mSumberDana.ID  " +
                    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas =" + _IDDInas.ToString() + "  AND IDKegiatan =" + _idKegiatan.ToString() + " Order BY IIDRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {


                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSumberDana()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    IDSUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    JumlahMurni = DataFormat.GetInteger(dr["cJumlahMurni"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    TahapInput = DataFormat.GetSingle(dr["btTahapInput"])

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
        public List<TSumberDana> Get(int _Tahun, int _IDDInas, long  _idSubKegiatan)
        {
            List<TSumberDana> _lst = new List<TSumberDana>();
            try
            {
                SSQL = "SELECT tSumberDanaRekening.*, mSumberdana.sNama  as Nama FROM tSumberDanaRekening  inner join mSumberDana on tSumberDanaRekening.iSumberDana = mSumberDana.IIDrekening  " +
                    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas =" + _IDDInas.ToString() + "  AND IDSubKegiatan =" + _idSubKegiatan.ToString() + " Order BY IIDRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {


                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TSumberDana()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDkegiatan"]),
                                    IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                    IDSUmberDana = DataFormat.GetInteger(dr["iSumberDana"]),
                                    JumlahMurni = DataFormat.GetInteger(dr["cJumlahMurni"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    TahapInput = DataFormat.GetSingle(dr["btTahapInput"])

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



        public bool Simpan(List<TSumberDana>  _lstSumberDana, int _TAhun, int _idUrusan, int _dinas, int _program, int _kegiatan, long idsubkegiatan=0)
        {


            try
            {
                
                SSQL = "DELETE " + m_sNamaTabel + " WHERE iTAhun =" + _TAhun.ToString() + " AND IDDInas =" + _dinas.ToString() +
                    " AND IdUrusan = " + _idUrusan.ToString() + " AND IDPRogram=" + _program.ToString() + " AND IDKegiatan =" + _kegiatan.ToString() + " AND IDSubKegiatan =" + idsubkegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                
                foreach(TSumberDana tsd in _lstSumberDana){



                    SSQL = " INSERT INTO " + m_sNamaTabel + " (iTahun,iDDInas, IDUrusan, IDProgram, IDKegiatan,IDSubKegiatan,IIDRekening,iSumberDana, cJumlah,cJumlahMurni, btTahapInput) values ( " +
                       _TAhun.ToString() + "," + _dinas.ToString() + "," + _idUrusan.ToString() + "," +
                        _program.ToString() + "," + _kegiatan.ToString() + "," + idsubkegiatan.ToString() + "," + tsd.IDRekening.ToString() +
                        "," + tsd.IDSUmberDana.ToString() + "," + tsd.Jumlah.ToString() + "," + tsd.JumlahMurni.ToString() + ",0)";
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
                
                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        
        public bool Hapus(int _pIDDesa)
        {
            try
            {

                SSQL = "DELETE FROM mDesa WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDDesa));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
    }
}
