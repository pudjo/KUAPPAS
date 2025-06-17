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
    public class RenjcaLogic:BP
    {
        public RenjcaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "Renja";
        }
        public List<Renja> Get()
        {

            List<Renja> _lst = new List<Renja>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Renja()
                                {

                                    //id,idprogram,idkegiatan,musrenmbang_id,usulan,bPegawai,bModal,bbjs,iddinas,keyrenja,iTahun 

                                  Tahun =DataFormat.GetInteger(dr["iTahun "]),
                                   id =DataFormat.GetInteger(dr["id"]),
                                  idprogram = DataFormat.GetInteger(dr["idprogram"]),
                                  idkegiatan = DataFormat.GetInteger(dr["idkegiatan"]),
                                  musrenmbang_id = DataFormat.GetInteger(dr["musrenmbang_id"]),
                                  usulan = DataFormat.GetString(dr["usulan"]),
                                  bPegawai = DataFormat.GetDecimal(dr["bPegawai"]),
                                  bModal = DataFormat.GetDecimal(dr["bModal"]),
                                  bbjs = DataFormat.GetDecimal(dr["bbjs"]),
                                  iddinas = DataFormat.GetInteger(dr["iddinas"]),
                                  keyrenja = DataFormat.GetInteger(dr["keyrenja"])
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
        public List<Renja> GetByIDProgram(int _pID)
        {

            List<Renja> _lst = new List<Renja>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERe IDProgram =" + _pID.ToString() + " ORDER BY IDKegiatan,IDlokasi";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Renja()
                                {

                                    Tahun = DataFormat.GetInteger(dr["iTahun "]),
                                    id = DataFormat.GetInteger(dr["id"]),
                                    idprogram = DataFormat.GetInteger(dr["idprogram"]),
                                    idkegiatan = DataFormat.GetInteger(dr["idkegiatan"]),
                                    musrenmbang_id = DataFormat.GetInteger(dr["musrenmbang_id"]),
                                    usulan = DataFormat.GetString(dr["usulan"]),
                                    bPegawai = DataFormat.GetDecimal(dr["bPegawai"]),
                                    bModal = DataFormat.GetDecimal(dr["bModal"]),
                                    bbjs = DataFormat.GetDecimal(dr["bbjs"]),
                                    iddinas = DataFormat.GetInteger(dr["iddinas"]),
                                    keyrenja = DataFormat.GetInteger(dr["keyrenja"])
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
 

        public bool Simpan(List<Renja> _lst)
        {
            bool ret = true;
            Hapus(Tahun);

            foreach(Renja r in _lst){
                ret=  Simpan(r);
            }
            return ret;

        }
        public bool Simpan(Renja _pRenja)
        {


            try
            {
              //  if (_pRenja.Tanggal == 0)
               // {
                    int _tanggal;
                    _tanggal = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Month, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Day, 2));
                     SSQL = " INSERT INTO Renja (id,idprogram,idkegiatan,musrenmbang_id,usulan,bPegawai,bModal,bbjs,iddinas,keyrenja,iTahun )  VALUES (" +
                           "@pid,@pidprogram,@pidkegiatan,@pmusrenmbang_id,@pusulan,@pbPegawai,@pbModal,@pbbjs,@piddinas,@pkeyrenja,@piTahun)";
                
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pid",_pRenja.id));
                        paramCollection.Add(new DBParameter("@pidprogram",_pRenja.idprogram));
                        paramCollection.Add(new DBParameter("@pidkegiatan",_pRenja.idkegiatan));
                        paramCollection.Add(new DBParameter("@pmusrenmbang_id",_pRenja.musrenmbang_id));
                        paramCollection.Add(new DBParameter("@pusulan",_pRenja.usulan));
                        paramCollection.Add(new DBParameter("@pbPegawai",_pRenja.bPegawai));
                        paramCollection.Add(new DBParameter("@pbModal",_pRenja.bModal));
                        paramCollection.Add(new DBParameter("@pbbjs",_pRenja.bbjs));
                        paramCollection.Add(new DBParameter("@piddinas",_pRenja.iddinas));
                        paramCollection.Add(new DBParameter("@pkeyrenja", _pRenja.keyrenja));
                        paramCollection.Add(new DBParameter("@piTahun",_pRenja.Tahun ));
                    if (_dbHelper.ExecuteNonQuery(SSQL,paramCollection) > 0)
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
        public bool ProsesKUA()
        {
            return true;

        }
        public void SimpanMaterProgram(int _iTahun)
        {
            SSQL = "Select distinct  * INTO tempMasterProgram FROM mprogramRenja WHERE iTahun = " + _iTahun.ToString() ;
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM mprogramRenja WHERE iTahun =" + _iTahun.ToString() ;
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "INSERT into mprogramRenja SELECT * FROM tempMasterProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "DROP TABLE tempMasterProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

          //  _dbHelper.ExecuteNonQuery(SSQL);

        }
        public void SimpanMaterKegiatan(int _iTahun)
        {
            SSQL = "Select distinct * INTO tempMasterKeg FROM mKegiatanRenja WHERE iTahun = " + _iTahun.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM mKegiatanRenja WHERE iTahun = " + _iTahun.ToString() ;
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "INSERT into mKegiatanRenja SELECT * FROM tempMasterKeg ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "DROP TABLE tempMasterKeg ";
            _dbHelper.ExecuteNonQuery(SSQL);


            

        }
        public bool Hapus(int _tahun)
        {
            try
            {

                SSQL = "DELETE FROM Renja";
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
              
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
