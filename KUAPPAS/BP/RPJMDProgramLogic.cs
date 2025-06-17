using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using BP;
using DataAccess;
using System.Data;

namespace BP
{
    public class RPJMDProgramLogic : BP 
    {
        public RPJMDProgramLogic(int _pTahun, int profile = 2)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            if (_pTahun <= 2020)
                m_sNamaTabel = "RPJMDProgram";
            else
            {
                m_sNamaTabel = profile == 2 ? "RPJMDProgram90" : "RPJMDProgram50";

            }
        }

        public List<RPJMDProgram> Get()
        {

            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
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
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),
                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])

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

        public List<RPJMDProgram> GetByUrusan(int pUrusan, int pSKPD = 0)
        {

            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDurusan =" + pUrusan.ToString() + " ";
                if (pSKPD > 0)
                {
                    SSQL = SSQL + " AND SKPD = " + pSKPD.ToString();

                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),
                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
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
        public List<RPJMDProgram> GetByUrusanProgram(int idUrusan, int idProgram)
        {

            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {
                SSQL = "SELECT " + m_sNamaTabel + ".*, mSKPD.sNamaSKPD as NamaSKPD FROM " + m_sNamaTabel + " INNER JOIN mSKPD ON mSKPD.ID =" + m_sNamaTabel + ".SKPD  WHERE " + m_sNamaTabel + ".IDMaster = " + idProgram.ToString() + " AND  " + m_sNamaTabel + ".IDurusanMaster =" + idUrusan.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["NamaSKPD"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),
                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
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
        public List<RPJMDProgram> GetByUrusanProgramMaster(int idUrusan, int idProgram)
        {

            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {
                SSQL = "SELECT " + m_sNamaTabel + ".*, mSKPD.sNamaSKPD as NamaSKPD FROM " + m_sNamaTabel + " INNER JOIN mSKPD ON mSKPD.ID =" + m_sNamaTabel + ".SKPD  WHERE IDMaster = " + idProgram.ToString() + " AND  IDurusanMaster =" + idUrusan.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["NamaSKPD"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),
                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
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

        public List<RPJMDProgram> GetBySKPD(int pSKPD,RemoteConnection sCOn=null)
        {

            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {

                string sKolom = GetKolomPerencanaan();                          

                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                if (sCOn == null )
                   dt = _dbHelper.ExecuteDataTable(SSQL);
                else
                    dt = _dbHelper.ExecuteDataTable(SSQL, sCOn.GetConnection());


                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),

                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr[sKolom]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
                                   
                                    
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
        public List<RPJMDProgram> GetBySKPDANdUrusan(int pSKPD, int urusan)
        {

            string sKolom = GetKolomPerencanaan();
            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " AND IDUrusan = " + urusan.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),
                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr[sKolom]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
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
        public List<RPJMDProgram> GetByUrusanAndAll(int _pUrusan)
        {
            string sKolom = GetKolomPerencanaan();
            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IDurusan=" + _pUrusan.ToString() + " or IDurusan=0 ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RPJMDProgram()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),

                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    Target1 = DataFormat.GetString(dr["Target1"]),
                                    Target2 = DataFormat.GetString(dr["Target2"]),
                                    Target3 = DataFormat.GetString(dr["Target3"]),
                                    Target4 = DataFormat.GetString(dr["Target4"]),
                                    Target5 = DataFormat.GetString(dr["Target5"]),
                                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                                    TargetRp4 = DataFormat.GetDecimal(dr[sKolom]),
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
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

        public RPJMDProgram GetByID(int SKPD, int _pID)
        {

            string sKolom = GetKolomPerencanaan();
            RPJMDProgram _o = new RPJMDProgram();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE SKPD = " + SKPD.ToString() + " AND ID=" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _o = new RPJMDProgram()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            IDProgramMaster = DataFormat.GetInteger(dr["IDMaster"]),

                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),

                            IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                            Kode = DataFormat.GetInteger(dr["Kode"]),
                            Nama = DataFormat.GetString(dr["Nama"]),
                            KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                            Outcome = DataFormat.GetString(dr["Outcome"]),
                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
                            Target1 = DataFormat.GetString(dr["Target1"]),
                            Target2 = DataFormat.GetString(dr["Target2"]),
                            Target3 = DataFormat.GetString(dr["Target3"]),
                            Target4 = DataFormat.GetString(dr["Target4"]),
                            Target5 = DataFormat.GetString(dr["Target5"]),
                            TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
                            TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
                            TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
                            TargetRp4 = DataFormat.GetDecimal(dr[sKolom]),
                            TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
                        };
                    }
                }
                return _o;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _o;
            }
        }

        public int CekByUrusanProgramSKPD(int SKPD, int idProgram, int id = 0)
        {

            List<RPJMDProgram> _lst = new List<RPJMDProgram>();
            try
            {
                SSQL = "SELECT " + m_sNamaTabel + ".*  FROM " + m_sNamaTabel + " WHERE SKPD= " + SKPD.ToString() + " AND  ID  =" + idProgram.ToString();
                if (id > 0)
                {
                    SSQL = SSQL + " AND ID= " + id.ToString();
                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    return dt.Rows.Count;

                }
                return 0;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }

        public bool Simpan(ref RPJMDProgram _pProgram)
        {
            try
            {
                int _newID;


                if (CekByUrusanProgramSKPD(_pProgram.SKPD, _pProgram.ID) == 0)
                {

                    _newID = DataFormat.GetInteger(_pProgram.IDUrusan.ToString() + _pProgram.Kode.IntToStringWithLeftPad(2));
                    SSQL = "INSERT INTO RPJMDProgram(ID,IDMaster,IDUrusan,IDUrusanMaster,Kode,SKPD,Periode, Tingkat,Nama,KondisiAwal,Outcome,Keluaran,Target1,TargetRp1,Target2,TargetRp2,Target3,TargetRp3,Target4,TargetRp4,Target5,TargetRp5) values (" +
                        "@pID,@pIDMaster,@pIDUrusan,@pIDUrusanMaster,@pKode,@pSKPD,@pPeriode, @pTingkat, @pNama,@pKondisiAwal,@pOutcome,@pKeluaran,@pTarget1,@pTargetRp1,@Target2,@pTargetRp2,@pTarget3,@pTargetRp3,@pTarget4,@pTargetRp4,@pTarget5,@pTargetRp5)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _pProgram.ID));
                    paramCollection.Add(new DBParameter("@pIDMaster", _pProgram.IDProgramMaster));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pProgram.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDUrusanMaster", _pProgram.IDUrusanMaster));

                    paramCollection.Add(new DBParameter("@pKode", _pProgram.Kode));
                    paramCollection.Add(new DBParameter("@pSKPD", _pProgram.SKPD));
                    paramCollection.Add(new DBParameter("@pPeriode", _pProgram.Perriode));
                    paramCollection.Add(new DBParameter("@pTingkat", _pProgram.Tingkat));


                    paramCollection.Add(new DBParameter("@pNama", _pProgram.Nama));
                    paramCollection.Add(new DBParameter("@pKondisiAwal", _pProgram.KondisiAwal));
                    paramCollection.Add(new DBParameter("@pOutcome", _pProgram.Outcome));
                    paramCollection.Add(new DBParameter("@pKeluaran", _pProgram.Keluaran));
                    paramCollection.Add(new DBParameter("@pTarget1", _pProgram.Target1));
                    paramCollection.Add(new DBParameter("@pTargetRp1", _pProgram.TargetRp1));
                    paramCollection.Add(new DBParameter("@Target2", _pProgram.Target2));
                    paramCollection.Add(new DBParameter("@pTargetRp2", _pProgram.TargetRp2));
                    paramCollection.Add(new DBParameter("@pTarget3", _pProgram.Target3));
                    paramCollection.Add(new DBParameter("@pTargetRp3", _pProgram.TargetRp3));
                    paramCollection.Add(new DBParameter("@pTarget4", _pProgram.Target4));
                    paramCollection.Add(new DBParameter("@pTargetRp4", _pProgram.TargetRp4));
                    paramCollection.Add(new DBParameter("@pTarget5", _pProgram.Target5));
                    paramCollection.Add(new DBParameter("@pTargetRp5", _pProgram.TargetRp5));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    _pProgram.ID = _newID;

                }
                else
                {
                    SSQL = "Update RPJMDProgram SET KondisiAwal=@pKondisiAwal,Outcome=@pOutcome,Keluaran=@pKeluaran,Target1=@pTarget1,TargetRp1=@pTargetRp1,Target2=@pTarget2," +
                             " TargetRp2=@pTargetRp2,Target3=@pTarget3,TargetRp3=@pTargetRp3,Target4=@pTarget4,TargetRp4=@pTargetRp4,Target5=@pTarget5,TargetRp5=@pTargetRp5 WHERE ID = @pID and SKPD =@pSKPD ";//) values (" +

                    DBParameterCollection paramCollection = new DBParameterCollection();


                    paramCollection.Add(new DBParameter("@pKondisiAwal", _pProgram.KondisiAwal));
                    paramCollection.Add(new DBParameter("@pOutcome", _pProgram.Outcome));
                    paramCollection.Add(new DBParameter("@pKeluaran", _pProgram.Keluaran));
                    paramCollection.Add(new DBParameter("@pTarget1", _pProgram.Target1));
                    paramCollection.Add(new DBParameter("@pTargetRp1", _pProgram.TargetRp1));
                    paramCollection.Add(new DBParameter("@pTarget2", _pProgram.Target2));
                    paramCollection.Add(new DBParameter("@pTargetRp2", _pProgram.TargetRp2));
                    paramCollection.Add(new DBParameter("@pTarget3", _pProgram.Target3));
                    paramCollection.Add(new DBParameter("@pTargetRp3", _pProgram.TargetRp3));
                    paramCollection.Add(new DBParameter("@pTarget4", _pProgram.Target4));
                    paramCollection.Add(new DBParameter("@pTargetRp4", _pProgram.TargetRp4));
                    paramCollection.Add(new DBParameter("@pTarget5", _pProgram.Target5));
                    paramCollection.Add(new DBParameter("@pTargetRp5", _pProgram.TargetRp5));
                    paramCollection.Add(new DBParameter("@pSKPD", _pProgram.SKPD));
                    paramCollection.Add(new DBParameter("@pID", _pProgram.ID));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);



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

        public void ImporDariMaster(int Periode, int Tingkat, int idUrusan = 0, int IDProgram = 0)
        {
            //try
            //{

            //    List<MasterProgram> lMaster = new List<MasterProgram>();
            //    MasterProgramLogic oMasterPLogic = new MasterProgramLogic(Tahun);

            //    lMaster = oMasterPLogic.Get(idUrusan, IDProgram);

            //    if (idUrusan == 0 && IDProgram == 0)
            //    {
            //        SSQL = "DELETE RPJMDProgram";
            //        _dbHelper.ExecuteNonQuery(SSQL);
            //    }
            //    foreach (MasterProgram mp in lMaster)
            //    {


            //        SSQL = "INSERT INTO RPJMDProgram(ID,IDMaster,IDUrusan,IDUrusanMaster,Kode,SKPD,Periode, Tingkat,Nama,KondisiAwal,Outcome,Keluaran,Target1,TargetRp1,Target2,TargetRp2,Target3,TargetRp3,Target4,TargetRp4,Target5,TargetRp5) values (" +
            //            "@pID,@pIDMaster,@pIDUrusan,@pIDUrusanMaster,@pKode,@pSKPD,@pPeriode, @pTingkat, @pNama,@pKondisiAwal,@pOutcome,@pKeluaran,@pTarget1,@pTargetRp1,@Target2,@pTargetRp2,@pTarget3,@pTargetRp3,@pTarget4,@pTargetRp4,@pTarget5,@pTargetRp5)";
            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@pID", mp.ID));
            //        paramCollection.Add(new DBParameter("@pIDMaster", mp.ID));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", mp.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pIDUrusanMaster", mp.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pKode", mp.Kode));
            //        paramCollection.Add(new DBParameter("@pSKPD", 0));
            //        paramCollection.Add(new DBParameter("@pPeriode", Periode));
            //        paramCollection.Add(new DBParameter("@pTingkat", Tingkat));


            //        paramCollection.Add(new DBParameter("@pNama", mp.Nama.ToUpper().Trim()));
            //        paramCollection.Add(new DBParameter("@pKondisiAwal", ""));
            //        paramCollection.Add(new DBParameter("@pOutcome", ""));
            //        paramCollection.Add(new DBParameter("@pKeluaran", ""));
            //        paramCollection.Add(new DBParameter("@pTarget1", ""));
            //        paramCollection.Add(new DBParameter("@pTargetRp1", 0));
            //        paramCollection.Add(new DBParameter("@Target2", ""));
            //        paramCollection.Add(new DBParameter("@pTargetRp2", 0));
            //        paramCollection.Add(new DBParameter("@pTarget3", ""));
            //        paramCollection.Add(new DBParameter("@pTargetRp3", 0));
            //        paramCollection.Add(new DBParameter("@pTarget4", ""));
            //        paramCollection.Add(new DBParameter("@pTargetRp4", 0));
            //        paramCollection.Add(new DBParameter("@pTarget5", ""));
            //        paramCollection.Add(new DBParameter("@pTargetRp5", 0));
            //        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    //  return false;

            //}

        }

        public bool Hapus(int _pIDSKPD, int _pIDProgram)
        {
            try
            {
                SSQL = "DELETE FROM RPJMDProgram WHERE SKPD = " + _pIDSKPD.ToString() + " AND ID =" + _pIDProgram.ToString();

                DBParameterCollection paramCollection = new DBParameterCollection();
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
