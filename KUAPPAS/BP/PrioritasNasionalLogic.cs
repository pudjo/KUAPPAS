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
   
    public class PrioritasNasionalLogic:BP 
    {
        public PrioritasNasionalLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPrioritasNasional";            
        }
        public List<PrioritasNasional> Get(Single _tahun )
        {
            List<PrioritasNasional> _lst = new List<PrioritasNasional>();
            try
            {
                //iTahun smallint,iInduk char(10),Nomor char(12),Kode char(5),Nama varchar(200), Leaf smallint)
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + _tahun.ToString() + " ORDER BY iInduk,Nomor";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                            select new PrioritasNasional()                        
                            {
                                    
                                    Nama = DataFormat.GetString(dr["sNamaPrioritasNasional"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Kode = DataFormat.GetString(dr["Kode"]),
                                    Induk = DataFormat.GetInteger(dr["iInduk"]),
                                    Nomor = DataFormat.GetInteger(dr["Nomor"])                                    
                            }).ToList();                    
                    }                    
                }
                return _lst;
            }catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<PrioritasNasional> GetChild(Single _tahun ,int _induk)
        {

            List<PrioritasNasional> _lst = new List<PrioritasNasional>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + _tahun.ToString() + " AND  iInduk= " + _induk.ToString() + " ORDER BY iInduk,Nomor";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PrioritasNasional()
                                {
                                    Nama = DataFormat.GetString(dr["sNamaPrioritasNasional"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Kode = DataFormat.GetString(dr["Kode"]),
                                    Induk = DataFormat.GetInteger(dr["iInduk"]),
                                    Nomor = DataFormat.GetInteger(dr["Nomor"])
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
        
        
        private string GetTampilan(string _pID, Single _root)
        {
            try
            {
                string sPrioritasNasional = _pID;
                string sRet = "";
                switch ((int)_root)
                {
                    case 1:
                        sRet = sRet + sPrioritasNasional.Substring(0, 1);
                        break;
                    case 2:
                        sRet = sRet + sPrioritasNasional.Substring(0, 1) + "." + sPrioritasNasional.Substring(1, 1);
                        break;
                    case 3:
                        sRet = sRet + sPrioritasNasional.Substring(0, 1) + "." + sPrioritasNasional.Substring(1, 1) + "." + sPrioritasNasional.Substring(2, 1);
                        break;
                    case 4:
                        sRet = sRet + sPrioritasNasional.Substring(0, 1) + "." + sPrioritasNasional.Substring(1, 1) + "." + sPrioritasNasional.Substring(2, 1) + "." + sPrioritasNasional.Substring(3, 2);
                        break;
                    case 5:
                        sRet = sRet + sPrioritasNasional.Substring(0, 1) + "." + sPrioritasNasional.Substring(1, 1) + "." + sPrioritasNasional.Substring(2, 1) + "." + sPrioritasNasional.Substring(3, 2) + "." + sPrioritasNasional.Substring(5, 2);
                        break;
                    case 6:
                        sRet = sRet + sPrioritasNasional.Substring(0, 1) + "." + sPrioritasNasional.Substring(1, 1) + "." + sPrioritasNasional.Substring(2, 1) + "." + sPrioritasNasional.Substring(3, 2) + "." + sPrioritasNasional.Substring(5, 2) + "." + sPrioritasNasional.Substring(7);
                        break;
                }
                return sRet;
            }
            catch (Exception ex)
            {
                return "";
            }

            

        }
        public bool Simpan(ref PrioritasNasional oPn)
        {
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (oPn.Nomor == 0)
                {
                    int nextID = GetNoMaxPadaInduk(oPn.Tahun, oPn.Induk);
                    if (nextID == 1)
                    {
                        if (oPn.Induk==0){
                            nextID= 101;
                        } else {
                            nextID = (oPn.Induk * 100) + 1;  
                        }                        
                    }
                    

                    
                    SSQL="INSERT INTO " + m_sNamaTabel + " (iTahun,Nomor,Kode,iInduk,sNama,bLEaf) values ("+
                          "@piTahun,@pNomor,@pKode,@piInduk,@psNama,@pbLEaf)";
                    
                    
                    paramCollection.Add(new DBParameter("@piTahun",oPn.Tahun));
                    paramCollection.Add(new DBParameter("@pNomor",nextID));
                    paramCollection.Add(new DBParameter("@pKode",oPn.Kode));
                    paramCollection.Add(new DBParameter("@piInduk",oPn.Induk));
                    paramCollection.Add(new DBParameter("@psNama",oPn.Nama));
                    paramCollection.Add(new DBParameter("@pbLEaf", oPn.Leaf));


                }
                else
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET Kode=@pKode,sNama=@psNama,bLEaf=@pbLEaf WHERE iTAhun=@piTahun AND Nomor=@pNomor";
                    paramCollection.Add(new DBParameter("@pKode", oPn.Kode));
                    paramCollection.Add(new DBParameter("@psNama", oPn.Nama));
                    paramCollection.Add(new DBParameter("@pbLEaf", oPn.Leaf));
                    paramCollection.Add(new DBParameter("@piTahun", oPn.Tahun));
                    paramCollection.Add(new DBParameter("@pNomor", oPn.Nomor));
                }
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
        public bool Hapus(PrioritasNasional pN)
        {
            try
            {
                SSQL = "DELETE FROM " + m_sNamaTabel + " WHERE Nomor=" + pN.Nomor.ToString();
                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {
                    return true;
                }
                else
                {
                    _lastError = "Tidak ada data nomor tersebut.";
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
        public int  GetNoMaxPadaInduk(Single _tahun,  int induk)
        {
            SSQL = "SELECT MAX(Nomor) as N from " + m_sNamaTabel + " WHERE iInduk= " + induk.ToString();
            object objMax = _dbHelper.ExecuteScalar(SSQL, CommandType.Text);//

            if (objMax.ToString().Length == 0)
            {
                return 1;
            }
            return Convert.ToInt32 (objMax.ToString()) ;

        }

    }
}
