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
    public class TahapKUALogic:BP 
    {
        public TahapKUALogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mtahapkua";
        }
        public TahapKUA Create(int _Tahun)
        {
            TahapKUA oTahap;
            try
            {
                   SSQL = "INSERT INTO mTahapKUA (iTahun,IDDinas,Tahap,IsCurrent) values {" + _Tahun.ToString() + ",0,0,1)";
                    _dbHelper.ExecuteNonQuery(SSQL);
                    oTahap = new TahapKUA()
                   {
                        Tahun = _Tahun,
                        IDDinas = 0,
                        Tahap = 1,
                        IsCurrent = 1
                   };
                return oTahap;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

                
            }
            

        }
        public TahapKUA Get(int _iTahun)
        {

            TahapKUA oTahap= new TahapKUA();
            try
            {
                SSQL = "select iTahun,IDDInas,Tahap,IsCurrent from mTahapKUA WHERE iTahun =" + _iTahun.ToString();
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        oTahap = new TahapKUA()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            Tahap = DataFormat.GetInteger(dr["Tahap"]),
                            IsCurrent = DataFormat.GetSingle(dr["IsCurrent"])
                        };
                    }
                   else
                   {
                       oTahap.Tahap = 1;
                       oTahap.Tahun = _iTahun;
                       oTahap.IDDinas = 0;
                       oTahap.IsCurrent = 1;
                       SSQL = "INSERT INTO mTahapKUA (iTahun,Tahap,IDDInas, isCurrent) values (" + oTahap.Tahun.ToString() + "," + oTahap.Tahap.ToString() + "," +oTahap.IDDinas.ToString () + ",1)";// =" + _pTahapKUA.Tahap.ToString() + " WHERE iTahun= " + _pTahapKUA.Tahun.ToString();

                       _dbHelper.ExecuteNonQuery(SSQL);

                     //  Simpan(ref oTahap);
                   }

                   
                }
                return oTahap;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }

        public TahapKUA GetCurrent()
        {

            TahapKUA oTahap = new TahapKUA();
            try
            {
                SSQL = "select iTahun,IDDInas,Tahap,IsCurrent from mTahapKUA WHERE IsCurrent =1";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];


                        oTahap = new TahapKUA()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                            Tahap = DataFormat.GetInteger(dr["Tahap"]),
                            IsCurrent = DataFormat.GetSingle(dr["IsCurrent"])
                        };
                    }
                }
                return oTahap;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public TahapKUA GetByTahunDInas(int _tahun, int _dinas)
        {
            TahapKUA _object = new TahapKUA();
            try
            {
                
               
               SSQL = "select iTahun,IDDInas,Tahap from mTahapKUA WHERE iTahun =" + _tahun.ToString() + " AND IDDinas=" +_dinas.ToString();
               

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr= dt.Rows[0];
                        _object = new TahapKUA()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Tahap = DataFormat.GetInteger(dr["Tahap"])
                                    
                
                                };
                    }
                }
                return _object;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _object;
            }


        }
       
        public bool Simpan(ref TahapKUA _pTahapKUA)
        {
            try
            {
                SSQL = "UPDATE mTahapKUA SET Tahap =" + _pTahapKUA.Tahap.ToString() + " WHERE iTahun= " + _pTahapKUA.Tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                // simpan kegiatan di tKegiatan_A
                
                SSQL = "DELETE from tKegiatan_A   WHERE iTahun= " + _pTahapKUA.Tahun.ToString();

                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "insert into tKegiatan_A (iTahun, IDDinas, IDUrusan, IDProgram, IDkegiatan,sNama, cPlafon, btJenis) " +
                    " select distinct iTahun, IDDinas, IDUrusan, IDProgram, IDkegiatan,Usulan as sNAma, JumlahOlah as cPlafon, btJenis " +
                    " from tKUA where idlokasi =0  and Status < 9";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "Delete  from tPrograms_A WHERE iTahun= " + _pTahapKUA.Tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " insert into tPrograms_A (IDProgram,iTahun, IDUrusan, IDDInas, sNamaProgram,btJenis) Select distinct tKUA.IDProgram, tKUA.ITahun,tKUA.IDUrusan,tKUA.IDDInas,mProgram.sNamaProgram , tKUA.btJenis FROM tKUA INNER JOIN mProgram ON tKUA.IDUrusanMaster= mProgram.IDUrusan and tKUA.IDProgramMaster= mProgram.ID ";
                _dbHelper.ExecuteNonQuery(SSQL);                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

            return true;
       
        }
        public bool SetCurrentTahun(ref TahapKUA _pTahapKUA)
        {
            try
            {
                SSQL = "UPDATE mTahapKUA SET IsCurrent=0 ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE mTahapKUA SET IsCurrent=1 WHERE iTahun= " + _pTahapKUA.Tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

            return true;

        }

        public List<TahapKUA> GetTahapKUA(int _pTahapKUA)
        {
            return null;

        }
        public bool Hapus(int _pIDTahapKUA)
        {
            return true;            

        }
    }
}
