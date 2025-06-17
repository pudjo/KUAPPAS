using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using Formatting;
using DataAccess;

namespace BP
{
    public class CatatanKegiatanLogic:BP 
    {
        public CatatanKegiatanLogic(int _pTahun, int profile )
            : base(_pTahun, 0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tCatatanKegiatan";

        }
        public bool Simpan(List<CatatanKegiatan> pLst, int pTahun, int pIDUrusan,int pIDDInas, int pIDProgram, int pIDKegiatan,Single _pJenis)
        {


            SSQL = "DELETE from " + m_sNamaTabel + " WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
                 " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDKegiatan.ToString() +
                 " AND btJenis=" + _pJenis.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            foreach (CatatanKegiatan c in pLst)
            {
                SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDInas,IDUrusan,IDProgram,IDKegiatan,iNoCatatan,sCatatan,sCatatanABT, btJenis) values (" +
                    "@piTahun, @pIDDInas,@pIDUrusan,@pIDProgram,@pIDKegiatan,@piNoCatatan,@psCatatan,@psCatatanABT, @pbtJenis)";
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piTahun", pTahun)); 
                paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas));
                paramCollection.Add(new DBParameter("@pIDUrusan",pIDUrusan));
                paramCollection.Add(new DBParameter("@pIDProgram",pIDProgram));
                paramCollection.Add(new DBParameter("@pIDKegiatan",pIDKegiatan));
                paramCollection.Add(new DBParameter("@piNoCatatan", c.NoCatatan));
                paramCollection.Add(new DBParameter("@psCatatan",c.CatatanMurni));
                paramCollection.Add(new DBParameter("@psCatatanABT", c.CatatanPerubahan));
                paramCollection.Add(new DBParameter("@pbtJenis", _pJenis));



                  _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
            }
            return true;

        }
        public List<CatatanKegiatan> Get(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, Single pJenis)
        {
            List<CatatanKegiatan> _lst = new List<CatatanKegiatan>();
            try
            {
                SSQL = "SELECT *  from " + m_sNamaTabel + " WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
                     " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
                     " AND btJenis=" + pJenis.ToString() + " ORDER BY iNOCatatan";
                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new CatatanKegiatan()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    NoCatatan = DataFormat.GetInteger(dr["iNoCatatan"]),
                                    CatatanMurni= DataFormat.GetString(dr["sCatatan"]),
                                    CatatanPerubahan = DataFormat.GetString(dr["sCatatanABT"]),
                                    Jenis=DataFormat.GetSingle(dr["btJenis"])
                                }).ToList();
                    }
                    
                }
                

                return _lst;
            }
            finally
            {
                

            }


        }
        
    }
}
