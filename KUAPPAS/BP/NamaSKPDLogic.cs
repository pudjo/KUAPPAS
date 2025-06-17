
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccess;
using DTO;
using Formatting;
using BP;
namespace BP
{
    public class NamaSKPDLogic:BP
    {
        public NamaSKPDLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "NamaSKPD";
        }
        
        public List<NamaSKPD> Get()
        {
            List<NamaSKPD> _lst = new List<NamaSKPD>();
            try
            {



                SSQL = "SELECT * from NamaSKPD ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new NamaSKPD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    TanggalAktiv = DataFormat.GetDate(dr["TanggalAktiv"]),
                                    
       

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

        

        public NamaSKPD GetNamaSKPD( int id)
        {
            NamaSKPD namaskpd = new NamaSKPD();
            try
            {
                SSQL = "SELECT * FROM NamaSKPD where id = " + id.ToString();// +(JENIS_JABATAN.ID_JENIS_KABIDPERBEND).ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr= dt.Rows[0];
                        namaskpd = new NamaSKPD()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),

                                    TanggalAktiv = DataFormat.GetDate(dr["TanggalActiv"])

                                };
                    }
                }
                return namaskpd;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
       
        
        public bool Simpan(NamaSKPD namaSKPD)
        {


            try
            {
                int _newID;

                SSQL ="insert into NamaSKPD (ID,Nama,TanggalAktiv) values (@pID,@pNama,@ptanggal)";

                 DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID",namaSKPD.ID));
                paramCollection.Add(new DBParameter("@pNama",namaSKPD.Nama.Trim()));
                paramCollection.Add(new DBParameter("@ptanggal",namaSKPD.TanggalAktiv,DbType.Date));
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
     
        public bool Hapus(int _pIDUnit)
        {
            try
            {

                SSQL = "DELETE FROM " + m_sNamaTabel + "  WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDUnit));
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
        public bool Cek(NamaSKPD namaSKPD)
        {
            try
            {

                SSQL = "Select FROM NamaSKPD WHERE ID=@pID and Nama= @Nama";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", namaSKPD.ID));
                paramCollection.Add(new DBParameter("@Nama", namaSKPD.Nama ));
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        return true ;
                    } else {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(NamaSKPD namaSKPD)
        {
            try
            {

                SSQL = "DELETE FROM NamaSKPD  WHERE ID=@pID and Nama=@Nama and TanggalAktiv=@Tanggal";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", namaSKPD.ID));
                paramCollection.Add(new DBParameter("@Nama", namaSKPD.Nama));
                paramCollection.Add(new DBParameter("@Tanggal", namaSKPD.TanggalAktiv));


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
