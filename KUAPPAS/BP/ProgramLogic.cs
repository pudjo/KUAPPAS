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
    public class ProgramLogic:BP
    {
        public ProgramLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tProgramss_A";
        }

        public List<Programs> GetByDinasByUrusan(int pDinas, int pUrusan)
        {
            List<Programs> _lst = new List<Programs>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDDInas =" + pDinas.ToString() + " AND IDUrusan =" + pUrusan.ToString() + " ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Programs()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    UrusanPemerintahan = Convert.ToInt32(
                                            DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]).ToString("0") +
                                            DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]).ToString("00")),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    SKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Unit = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Nama = DataFormat.GetString(dr["sNamaProgram"]),
                                    Kode = DataFormat.GetInteger(dr["btIDprogram"]),

                                    Tampilan = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]).ToString("0") + "." +
                                                DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btKodeKategori"]).ToString("0") + "." +
                                                DataFormat.GetInteger(dr["btKodeURusan"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btKodeSKPD"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btKodeUK"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btIDProgram"]).ToString("00")
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

        public List<Programs> Get()
        {
            List<Programs> _lst = new List<Programs>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new Programs()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategoriPelaksana=DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana=DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    UrusanPemerintahan= Convert.ToInt32(
                                            DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]).ToString("0")+ 
                                            DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]).ToString("00")),
                                    KodeKategori= DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan= DataFormat.GetInteger(dr["btKodeURusan"]),
                                    SKPD= DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Unit = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Nama= DataFormat.GetString(dr["sNamaProgram"]),                                    
                                    Kode =DataFormat.GetInteger(dr["btIDprogram"]),

                                    Tampilan = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]).ToString("0")+ "."+
                                                DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]).ToString("00")+ "."+ 
                                                DataFormat.GetInteger(dr["btKodeKategori"]).ToString("0") + "." + 
                                                DataFormat.GetInteger(dr["btKodeURusan"]).ToString("00") + "." + 
                                                DataFormat.GetInteger(dr["btKodeSKPD"]).ToString("00") + "." + 
                                                DataFormat.GetInteger(dr["btKodeUK"]).ToString("00")+ "."+
                                                DataFormat.GetInteger(dr["btIDProgram"]).ToString("00")  
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
        public bool Simpan(ref Programs _pPrograms)
        {
                

            try
            {
                if (_pPrograms.ID== 0)
                {
                    int _newID;
                    _newID = Convert.ToInt32( 
                            _pPrograms.Tahun.ToString().Substring(2,2) +
                            _pPrograms.UrusanPemerintahan.ToString()+ 
                            DataFormat.IntToStringWithLeftPad((int)_pPrograms.KodeKategori, 1) +
                            DataFormat.IntToStringWithLeftPad((int)_pPrograms.KodeUrusan, 2) + 
                            DataFormat.IntToStringWithLeftPad(_pPrograms.SKPD, 2) +
                            DataFormat.IntToStringWithLeftPad(_pPrograms.Unit, 2) +
                            DataFormat.IntToStringWithLeftPad(_pPrograms.Kode,2));

                    _pPrograms.ID = _newID;
                    SSQL = "INSERT INTO tPrograms_A(ID, Tahun,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btKodeKategori, btKodeUrusan,btKodeSKPD, btKodeUK,btIDProgram,sNama) values (" +
                        "@pID, @pTahun,@pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtKodeKategori, @pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeUK, @pbtIDProgram,@psNama)";

                }
                else
                {

                    SSQL = "UPDATE tPrograms_A SET sNama= @psNama WHERE ID=@pID";

                }
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pPrograms.ID));
                paramCollection.Add(new DBParameter("@pTahun", _pPrograms.Tahun));
                paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pPrograms.KodeKategoriPelaksana)); 
                paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pPrograms.KodeUrusanPelaksana));
                paramCollection.Add(new DBParameter("@pbtKodeKategori", _pPrograms.KodeKategori));
                paramCollection.Add(new DBParameter("@pbtKodeUrusan", _pPrograms.KodeUrusan));
                paramCollection.Add(new DBParameter("@pbtKodeSKPD", _pPrograms.SKPD));
                paramCollection.Add(new DBParameter("@pbtKodeUK", _pPrograms.Unit));
                paramCollection.Add(new DBParameter("@pbtIDProgram", _pPrograms.Kode));
                paramCollection.Add(new DBParameter("@psNama", _pPrograms.Nama));


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
        public bool Hapus(int _pIDPrograms)
        {
            try
            {

                SSQL = "DELETE FROM tPrograms_A WHERE ID=@pID";                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDPrograms));
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
    }
}
