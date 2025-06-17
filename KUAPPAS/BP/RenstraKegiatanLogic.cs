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
    public class RenstraKegiatanLogic : BP
    {
        public RenstraKegiatanLogic(int _pTahun, int profile=2 )
            : base(_pTahun)
        {
            Tahun = _pTahun;
            if (_pTahun <= 2020)
                m_sNamaTabel = "RenstraKegiatan";
            else
            {

                m_sNamaTabel = profile == 2 ? "RenstraKegiatan90" : "RenstraKegiatan50";
            }

        }
        public List<RenstraKegiatan> Get(RemoteConnection rCon=null)
        {

            List<RenstraKegiatan> _lst = new List<RenstraKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                if (rCon== null)
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                else
                    dt = _dbHelper.ExecuteDataTable(SSQL, rCon.GetConnection());


                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RenstraKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDMaster = DataFormat.GetInteger(dr["IDmaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    SKPD = DataFormat.GetInteger(dr["SKPD"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),

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



        public List<RenstraKegiatan> GetBySKPD(int pSKPD, RemoteConnection rCon = null)
        {

            string sKolom = GetKolomPerencanaan();
            // Gunakan kolom ini untuk Target RP 4 

            List<RenstraKegiatan> _lst = new List<RenstraKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                if (rCon ==null)
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                else
                    dt = _dbHelper.ExecuteDataTable(SSQL, rCon.GetConnection());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RenstraKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),

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
                                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"]),
                                    TargetRp4P = DataFormat.GetDecimal(dr["TargetRp4P"]),
                                    TargetRp5P = DataFormat.GetDecimal(dr["TargetRp5P"])
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

        public List<RenstraKegiatan> GetBySKPDUrusanProgram(int pSKPD, int iUrusan, int iProgram)
        {

            List<RenstraKegiatan> _lst = new List<RenstraKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " and IDUrusan =" + iUrusan.ToString() + " AND IDProgram= " + iProgram.ToString() + "  ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RenstraKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
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

        public List<RenstraKegiatan> GetBySKPD(int pSKPD, int IDProgram)
        {

            List<RenstraKegiatan> _lst = new List<RenstraKegiatan>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " AND IDProgram = " + IDProgram.ToString() + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RenstraKegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDMaster = DataFormat.GetInteger(dr["IDMaster"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
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



        public RenstraKegiatan GetByID(int _pID)
        {

            RenstraKegiatan _o = new RenstraKegiatan();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE ID=" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _o = new RenstraKegiatan()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            IDMaster = DataFormat.GetInteger(dr["IDMaster"]),
                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
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
                            TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
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
        public RenstraKegiatan GetByID(int iSKPD, int _pID)
        {

            RenstraKegiatan _o = new RenstraKegiatan();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE SKPD = " + iSKPD.ToString() + " AND  ID=" + _pID.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        _o = new RenstraKegiatan()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                            IDMaster = DataFormat.GetInteger(dr["IDMaster"]),
                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
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
                            TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
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

        private int CekKebarandaan(RenstraKegiatan r)
        {
            SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE SKPD = " + r.SKPD.ToString() + " AND  ID=" + r.ID.ToString();
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                return dt.Rows.Count;

            }
            else return 0;


        }

        public bool Simpan( KUA kua)
        {
            try
            {

                switch ((int)Tahun)
                {
                    
                    case 2017:
                       SSQL = "Update RenstraKegiatan SET KUA1= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2018:
                        SSQL = "Update RenstraKegiatan SET KUA2= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2019:
                        SSQL = "Update RenstraKegiatan SET KUA3= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2020:
                        SSQL = "Update RenstraKegiatan SET KUA4= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2021:
                        SSQL = "Update RenstraKegiatan SET KUA5= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;

                }

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@pKUA", kua.JumlahMurni));
                    paramCollection.Add(new DBParameter("@pID", kua.IDKegiatan));
                    paramCollection.Add(new DBParameter("@pSKPD", kua.IDDinas));
                    RemoteConnection rCOn = new RemoteConnection();

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection,rCOn.GetConnection());



                

                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanDariRPJMD(ref RenstraKegiatan _pProgram)
        {
            try
            {
                if (CekKebarandaan(_pProgram) == 0)
                {



                    SSQL = "INSERT INTO RenstraKegiatan(Tahun,ID,IDProgram,IDUrusan,IDProgramMaster," +
                            "IDUrusanMaster,IDMaster,Kode,SKPD,Periode," +
                            "Tingkat,Nama,KondisiAwal,Outcome,Keluaran," +
                        "Target1,TargetRp1,Target2,TargetRp2,Target3," +
                            "TargetRp3,Target4,TargetRp4,Target5,TargetRp5) values (" +
                        Tahun.ToString() + ",@pID,@pIDProgram,@pIDUrusan,@pIDProgramMaster," +
                        "@pIDUrusanMaster,@pIDMaster,@pKode,@pSKPD,@pPeriode," +
                        "@pTingkat, @pNama,@pKondisiAwal,@pOutcome,@pKeluaran," +
                        "@pTarget1,@pTargetRp1,@Target2,@pTargetRp2,@pTarget3," +
                        "@pTargetRp3,@pTarget4,@pTargetRp4,@pTarget5,@pTargetRp5)";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _pProgram.ID));
                    paramCollection.Add(new DBParameter("@pIDProgram", _pProgram.IDProgram));
                    paramCollection.Add(new DBParameter("@pIDUrusan", _pProgram.IDUrusan));
                    paramCollection.Add(new DBParameter("@pIDProgramMaster", _pProgram.IDProgramMaster));
                    paramCollection.Add(new DBParameter("@pIDUrusanMaster", _pProgram.IDUrusanMaster));


                    paramCollection.Add(new DBParameter("@pIDMaster", _pProgram.IDMaster));
                    paramCollection.Add(new DBParameter("@pKode", _pProgram.Kode));
                    paramCollection.Add(new DBParameter("@pSKPD", _pProgram.SKPD));
                    paramCollection.Add(new DBParameter("@pPeriode", _pProgram.Perriode));

                    paramCollection.Add(new DBParameter("@pTingkat", _pProgram.Tingkat));
                    paramCollection.Add(new DBParameter("@pNama", _pProgram.Nama));
                    paramCollection.Add(new DBParameter("@pOutcome", _pProgram.Outcome == null ? "" : _pProgram.Outcome));
                    paramCollection.Add(new DBParameter("@pKeluaran", _pProgram.Keluaran == null ? "" : _pProgram.Keluaran));
                    paramCollection.Add(new DBParameter("@pKondisiAwal", _pProgram.KondisiAwal == null ? "" : _pProgram.KondisiAwal));


                    paramCollection.Add(new DBParameter("@pTarget1", _pProgram.Target1 == null ? "" : _pProgram.Target1));
                    paramCollection.Add(new DBParameter("@pTargetRp1", _pProgram.TargetRp1));
                    paramCollection.Add(new DBParameter("@Target2", _pProgram.Target2 == null ? "" : _pProgram.Target2));
                    paramCollection.Add(new DBParameter("@pTargetRp2", _pProgram.TargetRp2));
                    paramCollection.Add(new DBParameter("@pTarget3", _pProgram.Target3 == null ? "" : _pProgram.Target3));

                    paramCollection.Add(new DBParameter("@pTargetRp3", _pProgram.TargetRp3));
                    paramCollection.Add(new DBParameter("@pTarget4", _pProgram.Target4 == null ? "" : _pProgram.Target4));
                    paramCollection.Add(new DBParameter("@pTargetRp4", _pProgram.TargetRp4));
                    paramCollection.Add(new DBParameter("@pTarget5", _pProgram.Target5 == null ? "" : _pProgram.Target5));
                    paramCollection.Add(new DBParameter("@pTargetRp5", _pProgram.TargetRp5));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }
                else
                {
                    SSQL = "Update RenstraKegiatan SET Kode=@pKode,Nama=@pNama WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@pKode", _pProgram.Kode));
                    paramCollection.Add(new DBParameter("@pNama", _pProgram.Nama));
                    paramCollection.Add(new DBParameter("@pID", _pProgram.ID));
                    paramCollection.Add(new DBParameter("@pSKPD", _pProgram.SKPD));
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

        public void ImporDariMaster(int Periode, int Tingkat)
        {
            try
            {

                List<MasterProgram> lMaster = new List<MasterProgram>();
                MasterProgramLogic oMasterPLogic = new MasterProgramLogic(Tahun);
                lMaster = oMasterPLogic.Get();
                SSQL = "DELETE RenstraKegiatan";
                _dbHelper.ExecuteNonQuery(SSQL);
                foreach (MasterProgram mp in lMaster)
                {
                    int _newID = 0;
                    _newID = GetMaxID() + 1;


                    SSQL = "INSERT INTO RenstraKegiatan(ID,IDUrusan,Kode,SKPD,Periode, Tingkat,Nama,KondisiAwal,Outcome,Keluaran,Target1,TargetRp1,Target2,TargetRp2,Target3,TargetRp3,Target4,TargetRp4,Target5,TargetRp5) values (" +
                        "@pID,@pIDUrusan,@pKode,@pSKPD,@pPeriode, @pTingkat, @pNama,@pKondisiAwal,@pOutcome,@pKeluaran,@pTarget1,@pTargetRp1,@Target2,@pTargetRp2,@pTarget3,@pTargetRp3,@pTarget4,@pTargetRp4,@pTarget5,@pTargetRp5)";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", _newID));
                    paramCollection.Add(new DBParameter("@pIDUrusan", mp.IDUrusan));


                    paramCollection.Add(new DBParameter("@pKode", mp.Kode));
                    paramCollection.Add(new DBParameter("@pSKPD", 0));
                    paramCollection.Add(new DBParameter("@pPeriode", Periode));
                    paramCollection.Add(new DBParameter("@pTingkat", Tingkat));


                    paramCollection.Add(new DBParameter("@pNama", mp.Nama.ToUpper().Trim()));
                    paramCollection.Add(new DBParameter("@pKondisiAwal", ""));
                    paramCollection.Add(new DBParameter("@pOutcome", ""));
                    paramCollection.Add(new DBParameter("@pKeluaran", ""));
                    paramCollection.Add(new DBParameter("@pTarget1", ""));
                    paramCollection.Add(new DBParameter("@pTargetRp1", 0));
                    paramCollection.Add(new DBParameter("@Target2", ""));
                    paramCollection.Add(new DBParameter("@pTargetRp2", 0));
                    paramCollection.Add(new DBParameter("@pTarget3", ""));
                    paramCollection.Add(new DBParameter("@pTargetRp3", 0));
                    paramCollection.Add(new DBParameter("@pTarget4", ""));
                    paramCollection.Add(new DBParameter("@pTargetRp4", 0));
                    paramCollection.Add(new DBParameter("@pTarget5", ""));
                    paramCollection.Add(new DBParameter("@pTargetRp5", 0));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                //  return false;

            }

        }

        public bool Hapus(long _pIDKegiatan)
        {
            try
            {
                SSQL = "DELETE FROM RenstraKegiatan WHERE ID=@pID";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pIDKegiatan));
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
    }
}
