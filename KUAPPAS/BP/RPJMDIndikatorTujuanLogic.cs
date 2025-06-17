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
    public class RPJMDIndikatorTujuanLogic:BP 
    {
         public RPJMDIndikatorTujuanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "RPJMDIndikatorTujuan";
            CekTable();
        }
        private bool CekTable()
        {
            try
            {
                SSQL = "if OBJECT_ID('RPJMDIndikatorTujuan') IS NULL  CREATE TABLE RPJMDIndikatorTujuan(ID  int ,IDTujuan int,No int , Indikator varchar(1000),Satuan int ,sSatuan varchar(200),CapaianAwal varchar(1000),kondisiAkhir varchar(1000))";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public List<RPJMDIndikatorTujuan> GetByByUrusan(int pUrusan)
        {
            List<RPJMDIndikatorTujuan> _lst = new List<RPJMDIndikatorTujuan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE Tahun =" + Tahun.ToString() + " AND IDUrusan =" + pUrusan.ToString() + " ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDIndikatorTujuan()
                                {
                                    No= DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDTujuan = DataFormat.GetInteger(dr["IDTujuan"]),
                                    Satuan = DataFormat.GetInteger(dr["Satuan"]),
                                    SSatuan = DataFormat.GetString(dr["sSatuan"]),
                                    CapaianAwal = DataFormat.GetString(dr["CapaianAwal"]),
                                    KondisiAkhir = DataFormat.GetString(dr["KondisiAkhir"]),
                                    Indikator= DataFormat.GetString(dr["Indikator"])
                                    
                                    
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

        public List<RPJMDIndikatorTujuan> Get()
        {
            List<RPJMDIndikatorTujuan> _lst = new List<RPJMDIndikatorTujuan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY No";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDIndikatorTujuan()
                                {
                                    No = DataFormat.GetInteger(dr["No"]),
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDTujuan = DataFormat.GetInteger(dr["IDTujuan"]),
                                    Satuan = DataFormat.GetInteger(dr["Satuan"]),
                                    SSatuan = DataFormat.GetString(dr["sSatuan"]),
                                    CapaianAwal = DataFormat.GetString(dr["CapaianAwal"]),
                                    KondisiAkhir = DataFormat.GetString(dr["KondisiAkhir"]),
                                    Indikator = DataFormat.GetString(dr["Indikator"])
                                    
                                    
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
        public bool Simpan(ref RPJMDIndikatorTujuan _pTujuan)
        {
                

            try
            {
                if (_pTujuan.ID== 0)
                {
                    int _newID;
                    _newID = GetMaxID();

                    _pTujuan.ID = _newID;
                    SSQL = "INSERT INTO RPJMDIndikatorTujuan(ID  ,IDTujuan ,No , Indikator ,Satuan ,sSatuan ,CapaianAwal ,kondisiAkhir ) values (" +
                        "@pID  ,@pIDTujuan ,@pNo , @pIndikator ,@pSatuan ,@psSatuan ,@pCapaianAwal ,@pkondisiAkhir )";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID",_pTujuan.ID));
                    paramCollection.Add(new DBParameter("@pIDTujuan",_pTujuan.IDTujuan));
                    paramCollection.Add(new DBParameter("@pNo",_pTujuan.No));
                    paramCollection.Add(new DBParameter("@pIndikator",_pTujuan.Indikator));
                    paramCollection.Add(new DBParameter("@pSatuan",_pTujuan.Satuan));
                    paramCollection.Add(new DBParameter("@psSatuan",_pTujuan.SSatuan));
                    paramCollection.Add(new DBParameter("@pCapaianAwal",_pTujuan.CapaianAwal));
                    paramCollection.Add(new DBParameter("@pkondisiAkhir", _pTujuan.KondisiAkhir));
                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    SSQL = "Update RPJMDIndikatorTujuan SET IDTujuan =@pIDTujuan,No=@pNo , Indikator =@pIndikator, " +
                            " Satuan=@pSatuan ,sSatuan=@psSatuan ,CapaianAwal=@pCapaianAwal ,kondisiAkhir=@pkondisiAkhir where ID =@pID ";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    
                    paramCollection.Add(new DBParameter("@pIDTujuan", _pTujuan.IDTujuan));
                    paramCollection.Add(new DBParameter("@pNo", _pTujuan.No));
                    paramCollection.Add(new DBParameter("@pIndikator", _pTujuan.Indikator));
                    paramCollection.Add(new DBParameter("@pSatuan", _pTujuan.Satuan));
                    paramCollection.Add(new DBParameter("@psSatuan", _pTujuan.SSatuan));
                    paramCollection.Add(new DBParameter("@pCapaianAwal", _pTujuan.CapaianAwal));
                    paramCollection.Add(new DBParameter("@pkondisiAkhir", _pTujuan.KondisiAkhir));
                    paramCollection.Add(new DBParameter("@pID", _pTujuan.ID));
                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(int _pIDPermasalahanPembangunan)
        {
            try
            {

                SSQL = "DELETE FROM RPJMDIndikatorTujuan WHERE ID=" + _pIDPermasalahanPembangunan.ToString();                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDPermasalahanPembangunan));

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
