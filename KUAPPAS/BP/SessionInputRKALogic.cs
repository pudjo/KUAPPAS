using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using System.Data;

namespace BP
{
    public class SessionInputRKALogic : BP
    {
        public SessionInputRKALogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mSessionInputRKA";

        }
        public bool Simpan(SessionInputRKA oSessionInputRKA)
        {
            try
            {

                if (oSessionInputRKA.ID == 0)
                {
                    int lID = GetMaxIDNoYear();
                    lID++;
                    SSQL = "INSERT INTO mSessionInputRKA (ID,Nama,IDDInas,IDKegiatan,SessionLow,SessionUp) values ( " +
                        " @pID,@pNama,@pIDDInas,@pIDKegiatan,@pSessionLow,@pSessionUp )";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", lID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pNama", oSessionInputRKA.Nama));
                    paramCollection.Add(new DBParameter("@pIDDInas", oSessionInputRKA.IDDInas));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", oSessionInputRKA.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pSessionLow", oSessionInputRKA.SessionLow));
                    paramCollection.Add(new DBParameter("@pSessionUp", oSessionInputRKA.SessionUp));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE mSessionInputRKA SET SessionLow=@pSessionLow,SessionUp=@pSessionUp WHERE ID =@pID";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pSessionLow", oSessionInputRKA.SessionLow));
                    paramCollection.Add(new DBParameter("@pSessionUp", oSessionInputRKA.SessionUp));
                    paramCollection.Add(new DBParameter("@pID", oSessionInputRKA.ID));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }
        }

        public List<SessionInputRKA> Get(int pIDDInas, long IDKegiatan)
        {
            List<SessionInputRKA> _lst = new List<SessionInputRKA>();
            try
            {


                SSQL = "SELECT * from " + m_sNamaTabel + " WHERE IDDInas = " + pIDDInas.ToString() + " AND IDKegiatan=" + IDKegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SessionInputRKA()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDKegiatan = DataFormat.GetLong(dr["IDKegiatan"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    SessionLow = DataFormat.GetInteger(dr["SessionLow"]),
                                    SessionUp = DataFormat.GetInteger(dr["SessionUp"])

                                }).ToList();
                    }
                }
                return _lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }

        }
    }
}
        
     
