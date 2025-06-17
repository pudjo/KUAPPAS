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
    public class TimAnggaranLogic:BP
    {
        public TimAnggaranLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mTimDPA";
            CekBtTahap();
            CekIDDInas();
        }
        public bool Simpan(List<TimAnggaran> _list, int _iTahun, Single _pTahap, int pIDDinas , Single pJenis, Single _pType)
        {

            

          //  List<TimAnggaran> mListUnit = new List<TimAnggaran>();
            // return mListUnit;
            //            insert into mTIMDPA values (1,'Pudjo Isnanto','1909288199201','Anggota','')
            //insert into mTIMDPA values (2,'Arman','1909288199201','Anggota','')
            //insert into mTIMDPA values (3,'Herman','1909288199201','Anggota','')
            try
            {
                SSQL = "DELETE from " + m_sNamaTabel + " WHERE iTahun=" + _iTahun.ToString() + 
                        " AND IDDinas=" + pIDDinas.ToString() + " AND btJenis=" + pJenis.ToString() + " AND bType=" + _pType.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);

                foreach (TimAnggaran tA in _list)
                {
                    SSQL = " INSERT into " + m_sNamaTabel + " (iTahun,btIDTimDPA, btTahap,sNama,sNIP,sJabatan,bType, IDDinas, btJenis) values (" +
                        "@piTahun,@btIDTimDPA, @pbtTahap,@psNama,@psNIP,@psJabatan,@pbType,@pIDDinas, @pbtJenis) ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun",_iTahun,DbType.Int16));
                    paramCollection.Add(new DBParameter("@btIDTimDPA", tA.ID, DbType.Int32 ));
 
                    paramCollection.Add(new DBParameter("@pbtTahap",1));
                    paramCollection.Add(new DBParameter("@psNama",tA.Nama,DbType.String));
                    paramCollection.Add(new DBParameter("@psNIP",tA.NIP, DbType.String));
                    paramCollection.Add(new DBParameter("@psJabatan", tA.Jabatan, DbType.String));
                    paramCollection.Add(new DBParameter("@pbType",tA.Type , DbType.Int16));
                    paramCollection.Add(new DBParameter("@pIDDinas",tA.DInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pbtJenis", tA.Jenis, DbType.Int16));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }

                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        private void CekBtTahap()
        {
            try
            {
                SSQL = "SELECT btTahap from " + m_sNamaTabel;
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                return;
            }
            catch (Exception ex)
            {
                SSQL = " ALTER TABLE " + m_sNamaTabel + " ADD iTahun smallint, btTahap smallint,bType smallint";
                _dbHelper.ExecuteNonQuery(SSQL);

            }
        }
        private void CekIDDInas()
        {
            try
            {
                SSQL = "SELECT IDDInas from " + m_sNamaTabel;
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                return;
            }
            catch (Exception ex)
            {
                SSQL = " ALTER TABLE " + m_sNamaTabel + " ADD IDDInas int ,btJenis smallint";
                _dbHelper.ExecuteNonQuery(SSQL);

            }


        }
        public List<TimAnggaran> Get(int _iTahun, Single iTahap, int _IDDInas, int Jenis)
        {

            List<TimAnggaran> _lst = new List<TimAnggaran>();

            try{
                SSQL = "SELECT * from " + m_sNamaTabel + " WHERE iTahun=" + _iTahun.ToString() +
                    " AND btJEnis=" + Jenis.ToString() + " AND IDDinas= " + _IDDInas.ToString();// +" and  btTahap=" + iTahap.ToString();


                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new TimAnggaran()
                                {
                                    //Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    ID =DataFormat.GetInteger(dr["btIDTimDPA"]),
                                    Nama= DataFormat.GetString(dr["sNama"]),
                                    NIP = DataFormat.GetString(dr["sNIP"]),
                                    Jabatan = DataFormat.GetString(dr["sJabatan"]),
                                    Type = DataFormat.GetSingle(dr["bType"])
                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }

        public bool  Hapus(int _iTahun, int _IDDInas, int Jenis)
        {

            List<TimAnggaran> _lst = new List<TimAnggaran>();

            try
            {
                SSQL = "DELETE  from " + m_sNamaTabel + " WHERE iTahun=" + _iTahun.ToString() +
                    " AND btJEnis=" + Jenis.ToString() + " AND IDDinas= " + _IDDInas.ToString() ;

                _dbHelper.ExecuteNonQuery(SSQL);

                return true ;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public bool HapusPerID(int _iTahun, int _IDDInas, int Jenis, int _ID)
        {

            List<TimAnggaran> _lst = new List<TimAnggaran>();

            try
            {
                SSQL = "DELETE  from " + m_sNamaTabel + " WHERE iTahun=" + _iTahun.ToString() +
                    " AND btIDtimDPA =" + _ID.ToString() + " AND btJEnis=" + Jenis.ToString() + " AND IDDinas= " + _IDDInas.ToString();

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
 
        public List<TimAnggaran> GetByType(int _iTahun, Single iTahap, int _IDDInas, int Jenis, Single _type)
        {

            List<TimAnggaran> _lst = new List<TimAnggaran>();

            try
            {
                SSQL = "SELECT * from " + m_sNamaTabel + " WHERE iTahun=" + _iTahun.ToString() +
                    " AND btJEnis=" + Jenis.ToString() + " AND IDDinas= " + _IDDInas.ToString()  +" AND bType=" + _type.ToString() + " ORDER BY btIDTimDPA ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TimAnggaran()
                                {
                                    //Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    ID = 0,
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    NIP = DataFormat.GetString(dr["sNIP"]),
                                    Jabatan = DataFormat.GetString(dr["sJabatan"]),
                                    Type = DataFormat.GetSingle(dr["bType"])
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
