using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DataAccess;
using System.Data;
using Formatting;
using BP;

namespace BP
{
    public class PaguSKPDLogic:BP
    {
         public PaguSKPDLogic(int _pTahun, int profile)
            : base(_pTahun,0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "PaguSKPD";
        }
        //public List<string> LoadJenisPaguSKPD()
        //{
            

        //}
        public bool Simpan(List<PaguSKPD> pLst, int pTahun, int iJenis)
        {

            SSQL = "DELETE PaguSKPD where itahun =" + pTahun.ToString() + " AND Jenis=" + iJenis.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);


            
            foreach (PaguSKPD i in pLst)
            {



            
                    SSQL = "INSERT INTO PaguSKPD (iTahun,IDDInas, Jenis,PaguMurni, PaguPerubahan) values (" +
                        "@piTahun, @pIDDInas,@pJenis, @pPaguMurni, @pPaguPerubahan)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    
                paramCollection.Add(new DBParameter("@piTahun",i.Tahun,DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas",i.IDDInas,DbType.Int32));
                paramCollection.Add(new DBParameter("@pJenis", i.Jenis, DbType.Int32)); 
                paramCollection.Add(new DBParameter("@pPaguMurni",i.PaguMurni,DbType.Decimal));
                paramCollection.Add(new DBParameter("@pPaguPerubahan",i.PaguPerubahan,DbType.Decimal));
                 _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
            }
            return true;

        }

        
        public List<PaguSKPD> Get(int pTahun, int iJenis )
        {
            List<PaguSKPD> _lst = new List<PaguSKPD>();
            try
            {
                SSQL = "SELECT PaguSKPD.* from PaguSKPD WHERE PaguSKPD.iTahun =" + pTahun.ToString() + " and Jenis =" + iJenis.ToString() + " ORDER BY PaguSKPD.IDDInas";
                //  _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PaguSKPD()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    PaguMurni = DataFormat.GetDecimal(dr["PaguMurni"]),
                                    Jenis = DataFormat.GetInteger(dr["Jenis"]),
                                    PaguPerubahan = DataFormat.GetDecimal(dr["PaguPerubahan"]),

                                }).ToList();
                    }
                }
                return _lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;

            }
        }
        public PaguSKPD GetByDinas(int pTahun,int _pIDDInas, int iJenis )
        {
            PaguSKPD oPagu = new PaguSKPD();
            try
            {
                SSQL = "SELECT PaguSKPD.* from PaguSKPD WHERE PaguSKPD.IDDInas = " + _pIDDInas.ToString()+ " and  PaguSKPD.iTahun =" + pTahun.ToString() + "  AND Jenis = " + iJenis.ToString() + " ORDER BY PaguSKPD.IDDInas";
                //  _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        oPagu = new PaguSKPD()
                          {
                              Tahun = DataFormat.GetInteger(dr["iTahun"]),
                              IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                              PaguMurni = DataFormat.GetDecimal(dr["PaguMurni"]),
                              Jenis = DataFormat.GetInteger(dr["Jenis"]),
                              PaguPerubahan = DataFormat.GetDecimal(dr["PaguPerubahan"]),

                          };
                    }
                }
                return oPagu;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;

            }
        }

    }
}
