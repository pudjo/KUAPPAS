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
    public class RenstraSubKegiatanLogic : BP
    {
        public RenstraSubKegiatanLogic(int _pTahun, int profile=2)
            : base(_pTahun)
        {
            Tahun = _pTahun;

            m_sNamaTabel = profile == 2 ? "SubKegiatan" : "SubKegiatan50";
        }
        //public List<RenstraSubKegiatan> Get(RemoteConnection rCon=null)
        //{

        //    List<RenstraSubKegiatan> mListUnit = new List<RenstraSubKegiatan>();
        //    try
        //    {
        //        SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
        //        DataTable dt = new DataTable();
        //        if (rCon== null)
        //            dt = _dbHelper.ExecuteDataTable(SSQL);
        //        else
        //            dt = _dbHelper.ExecuteDataTable(SSQL, rCon.GetConnection());


        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new RenstraSubKegiatan()
        //                        {
        //                            ID = DataFormat.GetLong(dr["ID"]),
        //                            IDMaster = DataFormat.GetLong(dr["IDmaster"]),
                                    
        //                            IDKegiatan = DataFormat.GetInteger("IDKegiatan"),
        //                            IDKegiatanMaster = DataFormat.GetInteger("IDKegiatanMaster"),

        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
        //                            IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
        //                            SKPD = DataFormat.GetInteger(dr["SKPD"]),
        //                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),

        //                            Kode = DataFormat.GetInteger(dr["Kode"]),
        //                            Nama = DataFormat.GetString(dr["Nama"]),
        //                            KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
        //                            Outcome = DataFormat.GetString(dr["Outcome"]),
        //                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
        //                            Target1 = DataFormat.GetString(dr["Target1"]),
        //                            Target2 = DataFormat.GetString(dr["Target2"]),
        //                            Target3 = DataFormat.GetString(dr["Target3"]),
        //                            Target4 = DataFormat.GetString(dr["Target4"]),
        //                            Target5 = DataFormat.GetString(dr["Target5"]),
        //                            TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
        //                            TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
        //                            TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
        //                            TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
        //                            TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])

        //                        }).ToList();
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}



        public List<RenstraSubKegiatan> GetBySKPD(int pSKPD, RemoteConnection rCon = null)
        {

            string sKolom = GetKolomPerencanaan();
            // Gunakan kolom ini untuk Target RP 4 

            List<RenstraSubKegiatan> _lst = new List<RenstraSubKegiatan>();
            try
            {

                SSQL = "SELECT distinct ID,IDMaster, IDKegiatan, IDKegiatanMaster, IDProgram, IDProgramMaster, IDUrusan, IDUrusanMaster, Kode,Nama,Keluaran,Outcome, Sum(TargetRp5) as TargetRp5, Sum(TargetRp5P) as TargetRp5P ";
                SSQL = SSQL + " FROM  " + m_sNamaTabel + "  WHERE SKPD=" + pSKPD.ToString();
                SSQL = SSQL + " group BY ID,IDMaster, IDKegiatan, IDKegiatanMaster, IDProgram, IDProgramMaster, IDUrusan, IDUrusanMaster, Kode, Nama,Keluaran,Outcome ORDER BY ID";
                
                
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
                                select new RenstraSubKegiatan()
                                {
                                    ID = DataFormat.GetLong(dr["ID"]),
                                    IDMaster = DataFormat.GetLong(dr["IDmaster"]),

                                    IDKegiatan = DataFormat.GetInteger("IDKegiatan"),
                                    IDKegiatanMaster = DataFormat.GetInteger("IDKegiatanMaster"),

                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
                                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),

                                    Kode = DataFormat.GetInteger(dr["Kode"]),
                                    Nama = DataFormat.GetString(dr["Nama"]),
                                    //KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
                                    Outcome = DataFormat.GetString(dr["Outcome"]),
                                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
                                    //Target5 = DataFormat.GetString(dr["Target5"]),
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

        //public List<RenstraSubKegiatan> GetBySKPDUrusanProgram(int pSKPD, int iUrusan, int IDProgram, int idKegiatan)
        //{

        //    List<RenstraSubKegiatan> mListUnit = new List<RenstraSubKegiatan>();
        //    try
        //    {
        //        SSQL = "SELECT ID,IDMaster, IDKegiatan, IDKegiatanMaster, IDProgram, IDProgramMaster, IDUrusan, IDUrusanMaster, Nama, Keluaran, Outcome, Sum(Targer5Rp) as Targer5Rp, Sum(Targer5RpP) as Targer5RpP ";
        //        SSQL = SSQL + " FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " AND IDProgram = " + IDProgram.ToString() + "  AND IDKegiatan= " + idKegiatan.ToString() ;
        //        SSQL= SSQL + " group BY ID,IDMaster, IDKegiatan, IDKegiatanMaster, IDProgram, IDProgramMaster, IDUrusan, IDUrusanMaster, Nama, Keluaran, Outcome  ORDER BY ID";
                

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new RenstraSubKegiatan()
        //                        {
        //                            ID = DataFormat.GetLong(dr["ID"]),
        //                            IDMaster = DataFormat.GetLong(dr["IDmaster"]),
        //                            IDKegiatan = DataFormat.GetInteger("IDKegiatan"),
        //                            IDKegiatanMaster = DataFormat.GetInteger("IDKegiatanMaster"),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
        //                            IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
        //                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                         //   Kode = DataFormat.GetInteger(dr["Kode"]),
        //                            Nama = DataFormat.GetString(dr["Nama"]),
        //                           // KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
        //                            Outcome = DataFormat.GetString(dr["Outcome"]),
        //                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
        //                           // Target5 = DataFormat.GetString(dr["Target5"]),
        //                            TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
        //                        }).ToList();
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}

        //public List<RenstraSubKegiatan> GetBySKPD(int pSKPD, int IDProgram , int idKegiatan )
        //{

        //    List<RenstraSubKegiatan> mListUnit = new List<RenstraSubKegiatan>();
        //    try
        //    {
        //        //SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " AND IDProgram = " + IDProgram.ToString() + " ORDER BY ID";
        //        SSQL = "SELECT ID,IDMaster, IDKegiatan, IDKegiatanMaster, IDProgram, IDProgramMaster, IDUrusan, IDUrusanMaster, Nama, Keluaran, Outcome, Sum(Targer5Rp) as Targer5Rp, Sum(Targer5RpP) as Targer5RpP ";
        //        SSQL = SSQL + " FROM " + m_sNamaTabel + " WHERE SKPD=" + pSKPD.ToString() + " AND IDProgram = " + IDProgram.ToString() + "  AND IDKegiatan= " + idKegiatan.ToString() ;
        //        SSQL= SSQL + " group BY ID,IDMaster, IDKegiatan, IDKegiatanMaster, IDProgram, IDProgramMaster, IDUrusan, IDUrusanMaster, Nama, Keluaran, Outcome  ORDER BY ID";
                
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new RenstraSubKegiatan()
        //                        {
        //                            ID = DataFormat.GetLong(dr["ID"]),
        //                            IDMaster = DataFormat.GetLong(dr["IDmaster"]),

        //                            IDKegiatan = DataFormat.GetInteger("IDKegiatan"),
        //                            IDKegiatanMaster = DataFormat.GetInteger("IDKegiatanMaster"),

        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
        //                            IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),

        //                            Kode = DataFormat.GetInteger(dr["Kode"]),
        //                            Nama = DataFormat.GetString(dr["Nama"]),
        //                            //KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
        //                            Outcome = DataFormat.GetString(dr["Outcome"]),
        //                            Keluaran = DataFormat.GetString(dr["Keluaran"]),
        //                            //Target5 = DataFormat.GetString(dr["Target5"]),
        //                            TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
        //                        }).ToList();
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}



        //public RenstraSubKegiatan GetByID(int _pID)
        //{

        //    RenstraSubKegiatan _o = new RenstraSubKegiatan();
        //    try
        //    {
        //        SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE ID=" + _pID.ToString();
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];

        //                _o = new RenstraSubKegiatan()
        //                {
        //                    ID = DataFormat.GetLong(dr["ID"]),
        //                    IDMaster = DataFormat.GetLong(dr["IDmaster"]),

        //                    IDKegiatan = DataFormat.GetInteger("IDKegiatan"),
        //                    IDKegiatanMaster = DataFormat.GetInteger("IDKegiatanMaster"),

        //                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
        //                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),

        //                    Kode = DataFormat.GetInteger(dr["Kode"]),
        //                    Nama = DataFormat.GetString(dr["Nama"]),
        //                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
        //                    Outcome = DataFormat.GetString(dr["Outcome"]),
        //                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
        //                    Target1 = DataFormat.GetString(dr["Target1"]),
        //                    Target2 = DataFormat.GetString(dr["Target2"]),
        //                    Target3 = DataFormat.GetString(dr["Target3"]),
        //                    Target4 = DataFormat.GetString(dr["Target4"]),
        //                    Target5 = DataFormat.GetString(dr["Target5"]),
        //                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
        //                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
        //                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
        //                    TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
        //                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
        //                };
        //            }
        //        }
        //        return _o;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return _o;
        //    }
        //}
        //public RenstraSubKegiatan GetByID(int iSKPD, int _pID)
        //{

        //    RenstraSubKegiatan _o = new RenstraSubKegiatan();
        //    try
        //    {
        //        SSQL = "SELECT * FROM " + m_sNamaTabel + "  WHERE SKPD = " + iSKPD.ToString() + " AND  ID=" + _pID.ToString();
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];

        //                _o = new RenstraSubKegiatan()
        //                {
        //                    ID = DataFormat.GetLong(dr["ID"]),
        //                    IDMaster = DataFormat.GetLong(dr["IDmaster"]),

        //                    IDKegiatan = DataFormat.GetInteger("IDKegiatan"),
        //                    IDKegiatanMaster = DataFormat.GetInteger("IDKegiatanMaster"),

        //                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                    IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
        //                    IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),

        //                    Kode = DataFormat.GetInteger(dr["Kode"]),
        //                    Nama = DataFormat.GetString(dr["Nama"]),
        //                    KondisiAwal = DataFormat.GetString(dr["KondisiAwal"]),
        //                    Outcome = DataFormat.GetString(dr["Outcome"]),
        //                    Keluaran = DataFormat.GetString(dr["Keluaran"]),
        //                    Target1 = DataFormat.GetString(dr["Target1"]),
        //                    Target2 = DataFormat.GetString(dr["Target2"]),
        //                    Target3 = DataFormat.GetString(dr["Target3"]),
        //                    Target4 = DataFormat.GetString(dr["Target4"]),
        //                    Target5 = DataFormat.GetString(dr["Target5"]),
        //                    TargetRp1 = DataFormat.GetDecimal(dr["TargetRp1"]),
        //                    TargetRp2 = DataFormat.GetDecimal(dr["TargetRp2"]),
        //                    TargetRp3 = DataFormat.GetDecimal(dr["TargetRp3"]),
        //                    TargetRp4 = DataFormat.GetDecimal(dr["TargetRp4"]),
        //                    TargetRp5 = DataFormat.GetDecimal(dr["TargetRp5"])
        //                };
        //            }
        //        }
        //        return _o;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return _o;
        //    }
        //}

        private int CekKebarandaan(RenstraSubKegiatan r)
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
                       SSQL = "Update RenstraSubKegiatan SET KUA1= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2018:
                        SSQL = "Update RenstraSubKegiatan SET KUA2= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2019:
                        SSQL = "Update RenstraSubKegiatan SET KUA3= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2020:
                        SSQL = "Update RenstraSubKegiatan SET KUA4= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
                        break;
                    case 2021:
                        SSQL = "Update RenstraSubKegiatan SET KUA5= @pKUA WHERE ID = @pID AND SKPD = @pSKPD";//) values (" +
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
    }
}

