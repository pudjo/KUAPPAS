using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DataAccess;
using System.Data;
using Formatting;
namespace BP
{
    public class UrusanBaruLogic:BP
    {
        public UrusanBaruLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mUrusanBaru";
            CekTable();    
        }
        private void CekTable(){
            try
            {
                SSQL = "SELECT * from mUrusanBaru";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (_isError == true)
                {
                    SSQL = "CREATE TABLE mUrusanBaru(ID int ,btKodeKategori smallint ,btKodeUrusan smallint,btKodeFungsi smallint,sNamaUrusan varchar(100))";
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE mUrusanBaru(ID int ,btKodeKategori smallint ,btKodeUrusan smallint,btKodeFungsi smallint,sNamaUrusan varchar(100))";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }


        public List<UrusanBaru> Get()
        {
            List<UrusanBaru> _lst = new List<UrusanBaru>();
            try
            {
                SSQL = "SELECT u.* FROM mUrusanBaru  U  ORDER BY u.ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanBaru()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan= DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Nama= DataFormat.GetString(dr["sNamaUrusan"]),
                                   // 'NamaFungsi= DataFormat.GetString(dr["sNamaFungsi"]),
                                   // Fungsi= DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    Tampilan =DataFormat.GetInteger(dr["ID"]).ToString().Substring(0,1) +"." + DataFormat.GetInteger(dr["ID"]).ToString().Substring(1,2)             
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

        public List<UrusanBaru> GetDariMapping(int urusanLama)
        {
            List<UrusanBaru> _lst = new List<UrusanBaru>();
            try
            {
                SSQL = "SELECT u.* FROM mUrusanBaru  U  WHERE u.ID in (SELECT idUrusanBaru AS ID from MapUrusanBaru  WHERE idUrusan=" + urusanLama.ToString() + ") ORDER BY u.ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanBaru()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan= DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Nama= DataFormat.GetString(dr["sNamaUrusan"]),
                                   // 'NamaFungsi= DataFormat.GetString(dr["sNamaFungsi"]),
                                   // Fungsi= DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    Tampilan =DataFormat.GetInteger(dr["ID"]).ToString().Substring(0,1) +"." + DataFormat.GetInteger(dr["ID"]).ToString().Substring(1,2)             
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

        

        public UrusanBaru GetByID(int _pID )
        {
            UrusanBaru oUrusan = new UrusanBaru();
            try
            {
                SSQL = "SELECT u.* FROM mUrusanBaru as U  WHERE U.ID=" + _pID.ToString() + " ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oUrusan = new UrusanBaru()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Nama = DataFormat.GetString(dr["sNamaUrusan"]),
                                   // NamaFungsi = DataFormat.GetString(dr["sNamaFungsi"]),
                                    Fungsi = DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToString().Substring(0, 1) + "." + DataFormat.GetInteger(dr["ID"]).ToString().Substring(1, 2)
                                };
                    }
                }
                return oUrusan;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oUrusan;
            }
        }
        private int GetCountThisID(int _pID)
        {

            int count=0;
            try
            {
                SSQL = "SELECT count(*) as c FROM mUrusanBaru as U  WHERE U.ID=" + _pID.ToString() + " ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        count = DataFormat.GetInteger(dr["c"]);

                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }

        public bool Hapus(int _pID, int pUrusan)
        {
            try
            {

                SSQL = "DELETE FROM mUrusanBaru WHERE btKodekategori=@pID and btKodeUrusan=@pUrusan";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pID));
                paramCollection.Add(new DBParameter("@pUrusan", pUrusan));

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

        public List<UrusanBaru> GetByKategori(int _pID)
        {
            List<UrusanBaru> _lst = new List<UrusanBaru>();
            try
            {
                SSQL = "SELECT u.* FROM mUrusanBaru U  WHERE U.btKodeKategori=" + _pID.ToString() + " ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanBaru()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    Nama = DataFormat.GetString(dr["sNamaUrusan"]),
                                    //'NamaFungsi = DataFormat.GetString(dr["sNamaFungsi"]),
                                    //Fungsi = DataFormat.GetInteger(dr["btKodeFungsi"]),
                                    Tampilan = DataFormat.GetInteger(dr["ID"]).ToString().Substring(0, 1) + "." + DataFormat.GetInteger(dr["ID"]).ToString().Substring(1, 2)
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
        public bool Simpan(UrusanBaru ub)
        {

            try
            {
                int id = ub.KodeKategori * 100 + ub.KodeUrusan;
                UrusanBaru cUB = new UrusanBaru();
                cUB = GetByID(id);

                if (cUB == null || cUB.ID==0)
                {
                    SSQL = "INSERT INTO mUrusanBaru( ID, btKodekategori, btKodeURusan, sNamaURusan,btKodeFungsi) values (" + id.ToString() +
                        "," + ub.KodeKategori.ToString() + "," + ub.KodeUrusan.ToString() + ",'" + ub.Nama + "'," + ub.Fungsi.ToString() + ")";

               }
                else
                {
                    SSQL = "update mUrusanBaru SET sNamaURusan='" + ub.Nama + "',btKodeFungsi= " + ub.Fungsi.ToString() + " WHERE ID =" + id.ToString();

                }
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;

            }
        }
    }
}
