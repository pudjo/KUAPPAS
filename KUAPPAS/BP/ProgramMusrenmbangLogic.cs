using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO;
using DataAccess;
using Formatting;


namespace BP
{
    public class ProgramMusrenmbangLogic:BP
    {
        public ProgramMusrenmbangLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mprogrammusrenmbang";
        }
        public List<ProgramMusrenmbang> Get()
        {

            List<ProgramMusrenmbang> _lst = new List<ProgramMusrenmbang>();
            try
            {

                
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new ProgramMusrenmbang()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),                                    
                                    NamaProgram = DataFormat.GetString(dr["Nama"])

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
        public List<ProgramMusrenmbang> GetByIDDInasByUrusan(int pIDDInas, int pIDUrusan )
        {

            List<ProgramMusrenmbang> _lst = new List<ProgramMusrenmbang>();
            try
            {


                //SSQL = "select distinct  mProgram.ID,mProgram.IDUrusan,renja.IDProgram, mProgram.sNamaProgram  as Nama from mProgram inner join " +
                //        " renja on mProgram.ID = renja.idprogram where renja.IDDInas =" + pIDDInas.ToString() + 
                //        " AND mProgram.IDUrusan =" + pIDUrusan.ToString() +" Order BY ID " ;

                SSQL = "select distinct  musrenbang.IDProgram,musrenbang.IDUrusan,musrenbang.IDProgram, mProgram.sNamaProgram  as Nama " +
                     " from mProgram inner join  musrenbang on ( mProgram.ID = musrenbang.idprogram) or (mProgram.ID = musrenbang.idprogram %100) " +
                    " where musrenbang.IDDInas =" + pIDDInas.ToString() + " AND musrenbang.IDUrusan =" + pIDUrusan.ToString() + " Order BY musrenbang.IDProgram ";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new ProgramMusrenmbang()
                                {
                                    ID = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),                                    
                                    NamaProgram = DataFormat.GetString(dr["Nama"])

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
        public List<ProgramMusrenmbang> GetByIDDInasByUrusanFormMusrenmbang(int pIDDInas, int pIDUrusan)
        {

            List<ProgramMusrenmbang> _lst = new List<ProgramMusrenmbang>();
            try
            {


                SSQL = "  select distinct  mProgram.ID,mProgram.IDUrusan,renja.IDProgram, mProgram.sNamaProgram  as Nama from mProgram inner join " +
                        " musrenmbang on mProgram.ID = renja.idprogram where renja.IDDInas =" + pIDDInas.ToString() +
                        " AND mProgram.IDUrusan =" + pIDUrusan.ToString() + " Order BY ID ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new ProgramMusrenmbang()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    NamaProgram = DataFormat.GetString(dr["Nama"])

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
        public List<ProgramMusrenmbang> GetByUrusan(int _pUrusan)
        {

            List<ProgramMusrenmbang> _lst = new List<ProgramMusrenmbang>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDurusan=" + _pUrusan.ToString() + " ORDER BY IDProgram";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new ProgramMusrenmbang()
                                {
                                    ID= DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    NamaProgram = DataFormat.GetString(dr["Nama"])

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
        public ProgramMusrenmbang GetByID(int _pID)
        {

            ProgramMusrenmbang _o = new ProgramMusrenmbang();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE IDProgram=" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _o = new ProgramMusrenmbang()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    NamaProgram = DataFormat.GetString(dr["Nama"])
                                };
                    }
                }
                return _o;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _o;
            }
        }
        public bool Simpan(ref ProgramMusrenmbang _pProgram)
        {
            try
            {
                int _newID;
                
                    
                    //_pProgram.ID = _newID;
                    SSQL = "INSERT INTO mprogrammusrenmbang(ID,IDUrusan,iTahun ,IDProgram, Nama) values (" +
                        "@pID,@pIDUrusan,@piTahun, @pIDProgram, @pNama)";
                //}
                //else
                //{
                  //  SSQL = "UPDATE mprogrammusrenmbang SET IDUrusan=@pIDUrusan,iTahun=@piTahun, IDProgram=@pIDProgram, Nama=@pNama WHERE ID=@pID ";

                //}
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pProgram.ID));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pProgram.IDUrusan));
                paramCollection.Add(new DBParameter("@pIDProgram", _pProgram.IDProgram));
                paramCollection.Add(new DBParameter("@piTahun", _pProgram.Tahun));
                paramCollection.Add(new DBParameter("@pNama", _pProgram.NamaProgram));
                
                if (_dbHelper.ExecuteNonQuery(SSQL,paramCollection) > 0)
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
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(int _pIDKegiatan)
        {
            try
            {
                SSQL = "DELETE FROM mprogrammusrenmbang WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDKegiatan));
                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
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
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }


    }
}
