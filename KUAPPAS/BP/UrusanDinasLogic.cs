using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using DataAccess;
using System.Data;
using Formatting;
namespace BP
{
    public class UrusanDinasLogic:BP
    {
        public UrusanDinasLogic(int _pTahun, int profile)
            : base(_pTahun,0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mpelaksanaurusan";
        }
        public List<UrusanDinas> Get()
        {
            List<UrusanDinas> _lst = new List<UrusanDinas>();
            try
            {
                //SSQL = "SELECT mpelaksanaurusan.* FROM mpelaksanaurusan ";
                
                SSQL = "SELECT mpelaksanaurusan.*,mUrusan.sNamaUrusan as Nama FROM mpelaksanaurusan inner join mUrusan ON mpelaksanaurusan.idurusan= murusan.id where iTahun =" + Tahun.ToString();



                

                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanDinas()
                                {
                                    Tahun =DataFormat.GetInteger(dr["iTahun"]),
                                    IDUrusan= DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas= DataFormat.GetInteger(dr["IDDInas"]),
                                    NamaUrusan= DataFormat.GetString(dr["Nama"]),
                                    UrusanPokok = DataFormat.GetSingle (dr["IsPokok"])

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
        public List<UrusanDinas> GetByIDDinas(int pIDDinas, int _pTahun, List<SKPD> lst =  null)
        {
            List<UrusanDinas> _lst = new List<UrusanDinas>();
            try
            {

                //UrusanDinasLogic oDinasLogic = new UrusanDinasLogic(GlobalVar.TahunAnggaran, mprofile);
                //List<UrusanDinas> lstUrusanDinas = oDinasLogic.GetByIDDinas(m_pDinas, (int)GlobalVar.TahunAnggaran, m_lstSKPD);// new List<Negara>();
            
                SSQL = "SELECT mpelaksanaurusan.*,mUrusan.sNamaUrusan as Nama FROM mpelaksanaurusan inner join mUrusan ON mpelaksanaurusan.idurusan= murusan.id where mpelaksanaurusan.IDDinas =" + pIDDinas.ToString() + " AND iTahun =" +_pTahun.ToString();
                
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanDinas()
                                {
                                    KodeKategori = DataFormat.GetInteger(dr["IDUrusan"]) /100,                                   
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NamaUrusan = DataFormat.GetString(dr["Nama"]),
                                    UrusanPokok = DataFormat.GetSingle(dr["IsPokok"])
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
        private int CekAda(UrusanDinas ud ){
            int jml=0;
            SSQL = "SELECT * from mPelaksanaurusan WHERE iTahun = " + ud.Tahun.ToString() +
                   " AND IDDINAS =" + ud.IDDinas.ToString() + " AND iDUrusan =" + ud.IDUrusan.ToString();

            _dbHelper.ExecuteNonQuery(SSQL);
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    jml = dt.Rows.Count;
                }

            }
            return jml;

        }
        public bool Simpan( UrusanDinas _pUrusanDinas)
        {
            try
            {
                if (_pUrusanDinas.IDUrusan == DataFormat.GetInteger(_pUrusanDinas.IDDinas.ToString().Substring(0, 3)))
                {
                    _pUrusanDinas.UrusanPokok = 0; // hanya digunakan untuk mengurutkan laporan saja
                }
                else
                {
                    _pUrusanDinas.UrusanPokok = 1;
                }
                if (CekAda(_pUrusanDinas) == 0)
                {

                    SSQL = "INSERT INTO mPelaksanaurusan(iTahun,IDdinas, IDUrusan, IsPokok) values (" +
                            "@piTahun,@pIDDinas, @pIDUrusan, @pIsPokok)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", _pUrusanDinas.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDinas", _pUrusanDinas.IDDinas));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pUrusanDinas.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIsPokok", _pUrusanDinas.UrusanPokok));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    SSQL = "update mPelaksanaUrusan SET btKodekategori = IDDInas/1000000,btKodeUrusan= (IDDInas/10000) % 100,btKodeSKPD = (IDDInas/100) %100, " +
                         " btKodeUK =0,btKodekategoripelaksana= IDUrusan/100, btKodeUrusanpelaksana= IDUrusan %100 where iTahun = " + _pUrusanDinas.Tahun.ToString() + "  and IDDInas =" + _pUrusanDinas.IDDinas.ToString();

                    _dbHelper.ExecuteNonQuery(SSQL);

                    
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

        public bool HapusDinas(UrusanDinas _pUrusanDinas)
        {
            try
            {

                SSQL = "DELETE FROM mPelaksanaUrusan WHERE IDDInas=@pIDDInas and iTahun =@piTahun ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piTahun", _pUrusanDinas.Tahun));
                paramCollection.Add(new DBParameter("@pIDDinas", _pUrusanDinas.IDDinas));

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

        public bool Hapus(UrusanDinas _pUrusanDinas)
        {
            try
            {
                
                SSQL = "DELETE FROM mPelaksanaUrusan WHERE IDDInas=@pIDDInas AND IDUrusan=@pIDUrusan and iTahun =@piTahun ";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piTahun", _pUrusanDinas.Tahun));
                paramCollection.Add(new DBParameter("@pIDDinas", _pUrusanDinas.IDDinas));
                paramCollection.Add(new DBParameter("@pIDUrusan", _pUrusanDinas.IDUrusan));

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
        public List<UrusanDinas> GetByIDDinasAndSPD(int pIDDinas, int _pTahun, long inourutSPD)
        {
            List<UrusanDinas> _lst = new List<UrusanDinas>();
            try
            {


                SSQL = "SELECT mpelaksanaurusan.*,mUrusan.sNamaUrusan as Nama FROM mpelaksanaurusan inner join mUrusan ON mpelaksanaurusan.idurusan= murusan.id where mpelaksanaurusan.IDDinas =" + pIDDinas.ToString() + " AND iTahun =" + _pTahun.ToString();
                SSQL = SSQL + " AND ID in (Select IDUrusan from TSPDKegiatan where inourut<= " + inourutSPD.ToString() + ")";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanDinas()
                                {
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NamaUrusan = DataFormat.GetString(dr["Nama"]),
                                    UrusanPokok = DataFormat.GetSingle(dr["IsPokok"])
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
        public List<UrusanDinas> GetByIDDinasAndBAST(int pIDDinas, int _pTahun, long inourutBAST)
        {
            List<UrusanDinas> _lst = new List<UrusanDinas>();
            try
            {


                SSQL = "SELECT mpelaksanaurusan.*,mUrusan.sNamaUrusan as Nama FROM mpelaksanaurusan inner join mUrusan ON mpelaksanaurusan.idurusan= murusan.id where mpelaksanaurusan.IDDinas =" + pIDDinas.ToString() + " AND iTahun =" + _pTahun.ToString();
                SSQL = SSQL + " AND ID in (Select IDUrusan from TBAST where inourut = " + inourutBAST.ToString() + ")";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanDinas()
                                {
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NamaUrusan = DataFormat.GetString(dr["Nama"]),
                                    UrusanPokok = DataFormat.GetSingle(dr["IsPokok"])
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
        public List<UrusanDinas> GetByIDDinasAndSPJ(int pIDDinas, int _pTahun, long inourutSPJ)
        {
            List<UrusanDinas> _lst = new List<UrusanDinas>();
            try
            {


                SSQL = "SELECT mpelaksanaurusan.*,mUrusan.sNamaUrusan as Nama FROM mpelaksanaurusan inner join mUrusan ON mpelaksanaurusan.idurusan= murusan.id where mpelaksanaurusan.IDDinas =" + pIDDinas.ToString() + " AND iTahun =" + _pTahun.ToString();
                SSQL = SSQL + " AND ID in (Select IDUrusan from tSPJRekening where inourut = " + inourutSPJ.ToString() + ")";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanDinas()
                                {
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NamaUrusan = DataFormat.GetString(dr["Nama"]),
                                    UrusanPokok = DataFormat.GetSingle(dr["IsPokok"])
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
        public List<UrusanDinas> GetByIDDinasAndSP2D(int pIDDinas, int _pTahun, long inourutSP2D)// TU
        {
            List<UrusanDinas> _lst = new List<UrusanDinas>();
            try
            {


                SSQL = "SELECT mpelaksanaurusan.*,mUrusan.sNamaUrusan as Nama FROM mpelaksanaurusan inner join mUrusan ON mpelaksanaurusan.idurusan= murusan.id where mpelaksanaurusan.IDDinas =" + pIDDinas.ToString() + " AND iTahun =" + _pTahun.ToString();
                SSQL = SSQL + " AND ID in (Select IDUrusan from tSPPRekening where inourut = " + inourutSP2D.ToString() + ")";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new UrusanDinas()
                                {
                                    KodeKategori = DataFormat.GetInteger(dr["btKodekategori"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
                                    NamaUrusan = DataFormat.GetString(dr["Nama"]),
                                    UrusanPokok = DataFormat.GetSingle(dr["IsPokok"])
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
    }
}
