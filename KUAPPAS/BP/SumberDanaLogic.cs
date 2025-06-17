using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using System.Data;
using DataAccess;
using Formatting;
namespace BP
{
    public class SumberDanaLogic:BP 
    {
        public SumberDanaLogic(int _pTahun): base (_pTahun)

        {
            Tahun = _pTahun;
            m_sNamaTabel = "mSumberDana";
        }
        public List<SumberDana> Get()
        {
            List<SumberDana> _lst = new List<SumberDana>();
            try
            {
            
                SSQL = "SELECT mSumberDana.* from mSumberDana where isnull(iidrekening,0)>0 ORDER BY ID";
                 DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SumberDana()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),                          
                                    IDRekening= DataFormat.GetLong(dr["IIDRekening"]), 
                                    IIDParent = DataFormat.GetLong(dr["IIDParent"]) ,
                                    Root = DataFormat.GetInteger(dr["Root"]),
                                    Leaf = DataFormat.GetInteger(dr["Leaf"]),
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
        public SumberDana GetByID(long IId)
        {
            SumberDana sb = new SumberDana();
            try
            {

                SSQL = "SELECT mSumberDana.* from mSumberDana where ID=" + IId.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        sb = new SumberDana()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    IIDParent = DataFormat.GetLong(dr["IIDParent"]),
                                    Root = DataFormat.GetInteger(dr["Root"]),
                                    Leaf = DataFormat.GetInteger(dr["Leaf"]),
                                };
                    } else {
                        sb=null;
                    }
                }
                return sb;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }


        }
        public SumberDana GetByIIDRekening(long IId)
        {
            SumberDana sb = new SumberDana();
            try
            {

                SSQL = "SELECT mSumberDana.* from mSumberDana where iidrekening =" + IId.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        sb = new SumberDana()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            Nama = DataFormat.GetString(dr["sNama"]),
                            IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                            IIDParent = DataFormat.GetLong(dr["IIDParent"]),
                            Root = DataFormat.GetInteger(dr["Root"]),
                            Leaf = DataFormat.GetInteger(dr["Leaf"]),
                        };
                    }
                    else
                    {
                        sb = null;
                    }
                }
                return sb;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }


        }
        public bool Simpan(SumberDana oSD)
        {
            
            try
            {
                
                if ( GetByID(oSD.ID)== null)
                {
                            SSQL = "INSERT into mSumberDana (IIDRekening, sNama, IIDparent ,Root,Leaf )values( " +
                                oSD.IDRekening.ToString() + ",'" + oSD.Nama + "'," + oSD.IIDParent.ToString() + "," + oSD.Root.ToString() + ","+oSD.Leaf.ToString() +")";
                 }
                 else
                 {
                        SSQL = "UPDATE mSumberDana SET sNama='" + oSD.Nama + "', Leaf = " +  oSD.Leaf.ToString() + " WHERE  ID =" + oSD.ID.ToString();
                 }
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
        public bool Hapus(SumberDana sd)
        {
            try
            {
                if (sd.ID == 0)
                {
                    SSQL = "INSERT into mSumberDana (ID,sNama, IIDRekening)values " +
                        sd.ID.ToString() + ",'" + sd.Nama + "'," + sd.IDRekening.ToString() + ")";
                }
                else
                {
                    SSQL = "UPDATE mSumberDana SET sNama='" + sd.Nama + "', IIDRekening=" + sd.IDRekening.ToString() + " WHERE ID=" + sd.ID.ToString();
                }
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
