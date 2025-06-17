using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DataAccess;
using System.Data;
using DTO;
using Formatting;

namespace BP
{
    public class TPrioritasNasionalLogic :BP 
    {
        public TPrioritasNasionalLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tPrioritasNasional";
        }

        //SSQL = "CREATE TABLE tPrioritasNasional ( iTahun smallint,IDUrusan int,IDDInas int, IDProgram int ,IDKEgiatan int , IDRekening bigint, NomorPrioritas int )";

        public List<TPrioritasNasional> Get(Single _tahun )
        {
            List<TPrioritasNasional> _lst = new List<TPrioritasNasional>();
            try
            {

                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + _tahun.ToString() + " ORDER BY NomorPrioritas, IDUrusan,IDProgram,IDKegiatan";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TPrioritasNasional()                        
                            {
                                Tahun=DataFormat.GetSingle(dr["iTahun"]),
                                IDDInas=DataFormat.GetInteger(dr["IDDInas"]),
                                IDUrusan=DataFormat.GetInteger(dr["IDUrusan"]),
                                IDProgram=DataFormat.GetInteger(dr["IDProgram"]),
                                IDKegiatan=DataFormat.GetInteger(dr["IDKegiatan"]),
                                IDRekening=DataFormat.GetLong(dr["IIDRekening"]),
                                NomorPrioritas = DataFormat.GetInteger(dr["NomorPrioritas"]),
                                Baru=1

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
        public List<TPrioritasNasional> GetChild(Single _tahun ,int nomorPrioritas)
        {

            List<TPrioritasNasional> _lst = new List<TPrioritasNasional>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE iTahun =" + _tahun.ToString() + " AND  NomorPrioritas=" + nomorPrioritas.ToString() + " OEDER BY IDDInas, IDUrusan, IDProgram, IDKegiatan ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TPrioritasNasional()
                                {
                                    Tahun = DataFormat.GetSingle(dr["iTahun"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NomorPrioritas = DataFormat.GetInteger(dr["NomorPrioritas"]),
                                    Baru = 1
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
        public bool Simpan(ref TPrioritasNasional oTPn)
        {
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (oTPn.Baru== 0)
                {
                    SSQL="INSERT INTO " + m_sNamaTabel + " (iTahun ,IDUrusan ,IDDInas , IDProgram ,IDKEgiatan , IDRekening , NomorPrioritas ) values ("+
                          "@piTahun ,@pIDUrusan ,@pIDDInas , @pIDProgram ,@pIDKEgiatan , @pIDRekening , @pNomorPrioritas)";
                    
                    paramCollection.Add(new DBParameter("@piTahun",oTPn.Tahun));
                    paramCollection.Add(new DBParameter("@pIDUrusan",oTPn.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDDInas",oTPn.IDDInas));
                    paramCollection.Add(new DBParameter("@pIDProgram",oTPn.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDKEgiatan",oTPn.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDRekening",oTPn.IDRekening));
                    paramCollection.Add(new DBParameter("@pNomorPrioritas", oTPn.NomorPrioritas));
                }
                else
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET NomorPrioritas=@pNomorPrioritas WHERE iTahun=@piTahun  AND IDUrusan=@pIDUrusan AND IDDInas =@pIDDInas, IDProgram=@pIDProgram ,IDKEgiatan=@pIDKEgiatan , IDRekening= @pIDRekening)" ;
                    paramCollection.Add(new DBParameter("@pNomorPrioritas", oTPn.NomorPrioritas));
                    paramCollection.Add(new DBParameter("@piTahun", oTPn.Tahun));
                    paramCollection.Add(new DBParameter("@pIDUrusan", oTPn.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDDInas", oTPn.IDDInas));
                    paramCollection.Add(new DBParameter("@pIDProgram", oTPn.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDKEgiatan", oTPn.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pIDRekening", oTPn.IDRekening));                   

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
