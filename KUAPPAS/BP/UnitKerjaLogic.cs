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
    public class UnitKerjaLogic:BP
    {
        public UnitKerjaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mUnitKerja";
        }
        public List<Unit> Get()
        {
            List<Unit> _lst = new List<Unit>();
            try
            {
                SSQL = "SELECT * FROM mUnitKerja ORDER BY SKPD,btKodeUK ";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Unit()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaUK"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeUK"]),
                                    UntAnggaran = DataFormat.GetInteger(dr["UntAnggaran"]),
                               
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
        public List<Unit> GetBySKPD( int _pSKPD)
        {
            List<Unit> _lst = new List<Unit>();
            try
            {
                SSQL = "SELECT * FROM mUnitKerja where SKPD= " + _pSKPD.ToString() + "  ORDER BY btKodeuk";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Unit()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaUK"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeUK"]),
                                    UntAnggaran = DataFormat.GetInteger(dr["UntAnggaran"]),
                            
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
        public Unit GetByID(int _pID)
        {
            Unit _oUnit = new Unit();
            try
            {
                SSQL = "SELECT * FROM mUnitKerja WHERE ID =" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        _oUnit = new Unit()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Nama = DataFormat.GetString(dr["sNamaUK"]),
                                    Kode = DataFormat.GetInteger(dr["btKodeUK"]),
                                    UntAnggaran = DataFormat.GetInteger(dr["UntAnggaran"]),



                                };
                    }
                }
                return _oUnit;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public int  CekID(int _pID)
        {
            Unit _oUnit = new Unit();
            try
            {
                SSQL = "SELECT * FROM mUnitKerja WHERE ID =" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows.Count;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
        public bool Simpan(ref Unit _pUnit)
        {
                

            try
            {
                int _newID;
                int _pSKPD;
                if (_pUnit.ID== 0)
                {

                    _newID = _pUnit.SKPD + _pUnit.Kode; ;// Convert.ToInt32(DataFormat.IntToStringWithLeftPad(_pUnit.KodeKategori, 1) + DataFormat.IntToStringWithLeftPad(_pUnit.KodeUrusan, 2) + DataFormat.IntToStringWithLeftPad(_pUnit.KodeSKPD, 2) + DataFormat.IntToStringWithLeftPad(_pUnit.Kode, 2));
                  //  _pSKPD = _pUnit.SKPD + _pUnit.Kode; // Convert.ToInt32(DataFormat.IntToStringWithLeftPad(_pUnit.KodeKategori, 1) + DataFormat.IntToStringWithLeftPad(_pUnit.KodeUrusan, 2) + DataFormat.IntToStringWithLeftPad(_pUnit.KodeSKPD, 2) + "00");
                    
                    _pUnit.ID = _newID;
                    
                    SSQL = "INSERT INTO mUnitKerja(ID, SKPD,btKodeKategori, btKodeUrusan,btKodeSKPD, btKodeUK,sNamaUK,UntAnggaran) values (" +
                        "@pID, @pSKPD,@pbtKodeKategori, @pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeUK, @psNamaUK,@UntAnggaran)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pSKPD", _pUnit.SKPD ));
                    paramCollection.Add(new DBParameter("@pbtKodeKategori", _pUnit.KodeKategori));
                    paramCollection.Add(new DBParameter("@pbtKodeUrusan", _pUnit.KodeUrusan));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _pUnit.KodeSKPD));
                    paramCollection.Add(new DBParameter("@pbtKodeUK", _pUnit.Kode));
                    paramCollection.Add(new DBParameter("@psNamaUK", _pUnit.Nama));
                    paramCollection.Add(new DBParameter("@UntAnggaran", _pUnit.UntAnggaran));
                    _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                }
                else
                {
                    _newID = _pUnit.ID;
                    SSQL = "UPDATE mUnitKerja SET sNamaUK = @psNamaUK, SKPD= @pSKPD,UntAnggaran=@UntAnggaran WHERE ID=@pID";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@psNamaUK", _pUnit.Nama));
                    paramCollection.Add(new DBParameter("@pSKPD", _pUnit.SKPD));
                    paramCollection.Add(new DBParameter("@UntAnggaran", _pUnit.UntAnggaran));

                    paramCollection.Add(new DBParameter("@pID", _newID));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

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
        public bool Hapus(int _pIDUnit)
        {
            try
            {
                
                SSQL = "DELETE FROM mUNitKerja WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDUnit));
                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
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
