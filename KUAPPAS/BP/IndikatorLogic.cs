//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DTO;
//using DataAccess;
//using System.Data;
//using Formatting;


//namespace BP
//{
//    public class IndikatorLogic:BP
//    {
//        public IndikatorLogic(int _pTahun)
//            : base(_pTahun)
//        {
//            Tahun = _pTahun;
//            m_sNamaTabel = "tIndikator";
//        }
//        public List<string> LoadJenisIndikator()
//        {
//            List<string> mListUnit = new List<string>();
//            mListUnit.Add("Capaian Program");
//            mListUnit.Add("Masukan");
//            mListUnit.Add("Keluaran");
//            mListUnit.Add("Hasil");

//            return mListUnit;
            

//        }
//        public bool Simpan(List<Indikator> pLst, int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan)
//        {
            
//          // _dbHelper.ExecuteNonQuery(SSQL);
//            SSQL = "DELETE " + m_sNamaTabel + " WHERE iTahun=" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
//                            " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString();


//            //                " AND btJenisIndikator=@pJenisIndikator AND iIndikator=@piIndikator";

//            _dbHelper.ExecuteNonQuery(SSQL);

//            foreach (Indikator i in pLst)
//            {
//                //if (GetIndikator(i) == 0)
//                //{
//                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun, IDDInas, IDUrusan, IDProgram,IDKegiatan,btJenisIndikator,iIndikator,sIndikator,sTarget ) values (" +
//                        "@piTahun, @pIDDInas, @pIDUrusan, @pIDProgram,@pIDKegiatan,@pJenisIndikator,@piIndikator,@psIndikator,@psTarget)";

//                    DBParameterCollection paramCollection = new DBParameterCollection();
//                    paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
//                    paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
//                    paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
//                    paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
//                    paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int32));
//                    paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
//                    paramCollection.Add(new DBParameter("@piIndikator", i.iIndikator, DbType.Int16));
//                    paramCollection.Add(new DBParameter("@psIndikator", i.sIndikator, DbType.String));
//                    paramCollection.Add(new DBParameter("@psTarget", i.Target, DbType.String));
//                 //   paramCollection.Add(new DBParameter("@psIndikatorGeser",  i.sIndikator, DbType.String));
//                  //  paramCollection.Add(new DBParameter("@psTargetGeser", i.TargetPerubahan, DbType.String));



//                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
//                //}
//                //else
//                //{
//                //    SSQL = "UPDATE " + m_sNamaTabel + " SET sIndikator=@psIndikator,sTarget=@psTarget,sIndikatorGeser=@psIndikatorGeser,sTargetGeser=@psTargetGeser  WHERE iTahun=@piTahun AND IDDInas=@pIDDInas " +
//                //            " AND IDUrusan=@pIDUrusan AND IDProgram=@pIDProgram AND IDKegiatan=@pIDKegiatan AND btJenisIndikator=@pJenisIndikator AND iIndikator=@piIndikator";


//                //    DBParameterCollection paramCollection = new DBParameterCollection();
                  
//                //    paramCollection.Add(new DBParameter("@psIndikator", i.sIndikator, DbType.String));
//                //    paramCollection.Add(new DBParameter("@psTarget", i.Target, DbType.String));

//                //    paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
//                //    paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
//                //    paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
//                //    paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
//                //    paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int32));
//                //    paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
//                //    paramCollection.Add(new DBParameter("@piIndikator", i.iIndikator, DbType.Int16));
//                //    paramCollection.Add(new DBParameter("@psIndikatorGeser", i.sIndikatorMurni, DbType.String));
//                //    paramCollection.Add(new DBParameter("@psTargetGeser", i.TargetPerubahan, DbType.String));
                   
//                //    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

//                //}
//            }
//            return true;

//        }
//        private int GetIndikator(Indikator i)
//        {
//            int lRet = 0;

//            SSQL = "Select count(*) from tIndikator WHERE iTahun =" + i.Tahun.ToString() + " AND IDDInas=" + i.IDDInas.ToString() +
//                 " AND IDUrusan=" + i.IDUrusan.ToString() + " AND IDProgram=" + i.IDProgram.ToString() + " AND IDKegiatan=" + i.IDKegiatan.ToString() + " AND btJenisIndikator=" + i.iJenis.ToString() + " AND iIndikator=" + i.iIndikator.ToString() ;

//            lRet = (int)_dbHelper.ExecuteScalar(SSQL);
//            return lRet;

//        }

//        public bool Refresh(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan)
//        {
//            try
//            {
//                SSQL = "DELETE from tIndikator WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
//                     " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString();                     
//                _dbHelper.ExecuteNonQuery(SSQL);
//                return true;

//            }
//            catch (Exception ex)
//            {
//                _lastError = ex.Message;
//                return false;
//            }
//        }

//        public bool Hapus(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, int Jenis, int no)
//        {
//            try
//            {
//                SSQL = "DELETE from tIndikator WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
//                     " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
//                     " AND btJenisIndikator =" + Jenis.ToString() + " AND iIndikator=" + no.ToString();

//                _dbHelper.ExecuteNonQuery(SSQL);
//                return true;

//            }
//            catch (Exception ex)
//            {
//                _lastError = ex.Message;
//                return false;
//            }
//        }

//        public List<Indikator> Get(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan)
//        {
//            List<Indikator> mListUnit = new List<Indikator>();
//            try
//            {
//                SSQL = "SELECT tIndikator.* , JenisIndikator.Nama from JenisIndikator LEFT OUTER JOIN tIndikator ON tIndikator.btJenisIndikator= JenisIndikator.iJenis WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
//                     " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
//                     " ORDER BY btJenisIndikator, iIndikator";
//                _dbHelper.ExecuteNonQuery(SSQL);
//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
                
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        mListUnit = (from DataRow dr in dt.Rows
//                                select new Indikator()
//                                {
//                                    Tahun= DataFormat.GetInteger(dr["iTahun"]),
//                                    IDDInas= DataFormat.GetInteger(dr["IDDinas"]),
//                                    IDUrusan= DataFormat.GetInteger(dr["IDUrusan"]),
//                                    IDProgram= DataFormat.GetInteger(dr["IDProgram"]),
//                                    IDKegiatan= DataFormat.GetInteger(dr["IDKegiatan"]),
//                                    iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
//                                    iIndikator= DataFormat.GetInteger(dr["iIndikator"]),
//                                    sIndikator= DataFormat.GetString(dr["sIndikator"]),
//                                    Target = DataFormat.GetString(dr["sTarget"]),
//                                    sIndikatorMurni= DataFormat.GetString(dr["sIndikatorGeser"]), //Idnikator Perubahan 

//                                    TargetPerubahan= DataFormat.GetString(dr["sTargetGeser"]),
//                                    //TargetMurni = DataFormat.GetString(dr["sTargetGeseri"]),
//                                    NamaJenis = DataFormat.GetString(dr["Nama"])

//                    //                        paramCollection.Add(new DBParameter("@psIndikatorGeser", i.sIndikatorMurni, DbType.String));
//                    //paramCollection.Add(new DBParameter("@psTargetGeser", i.TargetPerubahan, DbType.String));
//                                     // paramCollection.Add(new DBParameter("@psIndikatorGeser", i.sIndikatorMurni, DbType.String));
//                                    //paramCollection.Add(new DBParameter("@psTargetGeser", i.TargetPerubahan, DbType.String));

                
//                                }).ToList();
//                    }
//                    else
//                    {
//                        mListUnit.Add(new Indikator()
//                        {
//                            Tahun = 0,
//                            IDDInas = 0,
//                            IDUrusan = 0,
//                            IDProgram = 0,
//                            IDKegiatan = 0,
//                            iJenis = 1,
//                            iIndikator = 0,
//                            sIndikator = "",
//                            Target = "",
//                            NamaJenis = "Capaian Program"

//                        });

//                        mListUnit.Add(new Indikator()
//                        {
//                            Tahun = 0,
//                            IDDInas = 0,
//                            IDUrusan = 0,
//                            IDProgram = 0,
//                            IDKegiatan = 0,
//                            iJenis = 2,
//                            iIndikator = 0,
//                            sIndikator = "",
//                            Target = "",
//                            NamaJenis = "Masukan"

//                        });

//                        mListUnit.Add(new Indikator()
//                        {
//                            Tahun = 0,
//                            IDDInas = 0,
//                            IDUrusan = 0,
//                            IDProgram = 0,
//                            IDKegiatan = 0,
//                            iJenis = 3,
//                            iIndikator = 0,
//                            sIndikator = "",
//                            Target = "",
//                            NamaJenis = "Keluaran"

//                        });

//                        mListUnit.Add(new Indikator()
//                        {
//                            Tahun = 0,
//                            IDDInas = 0,
//                            IDUrusan = 0,
//                            IDProgram = 0,
//                            IDKegiatan = 0,
//                            iJenis = 4,
//                            iIndikator = 0,
//                            sIndikator = "",
//                            Target = "",
//                            NamaJenis = "Hasil"

//                        });

//                    }
//                }
                

//                return mListUnit;
//            }
//            finally
//            {
                

//            }


//        }
//    }
//}

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
    public class IndikatorLogic : BP
    {
        public IndikatorLogic(int _pTahun, int profile)
            : base(_pTahun,0, profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tIndikator";
        }
        public List<string> LoadJenisIndikator()
        {
            List<string> _lst = new List<string>();
            _lst.Add("Capaian Program");
            _lst.Add("Masukan");
            _lst.Add("Keluaran");
            _lst.Add("Hasil");

            return _lst;


        }
        public bool Simpan(List<Indikator> pLst, int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, int pTahap)
        {

            // _dbHelper.ExecuteNonQuery(SSQL);
            //   SSQL = " DELETE " + m_sNamaTabel + " WHERE iTahun=" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() + " " +
            //                 " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString();
            // _dbHelper.ExecuteNonQuery(SSQL);

            foreach (Indikator i in pLst)
            {



                if (i.ID == 0)
                {
                    long maxID = GetMaxLongID ();

                    if (pTahap==1 )
                    SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun,ID, IDDInas, IDUrusan, IDProgram,IDKegiatan,btJenisIndikator,iIndikator,sIndikator,sTarget ,sIndikatorGeser,sTargetGeser,sIndikatorMurni,sTargetMurni ) values (" +
                        "@piTahun,@pID, @pIDDInas, @pIDUrusan, @pIDProgram,@pIDKegiatan,@pJenisIndikator,@piIndikator,@psIndikator,@psTarget,@psIndikator,@psTarget,@psIndikator,@psTarget )";

                    if (pTahap==3 )
                        SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun,ID, IDDInas, IDUrusan, IDProgram,IDKegiatan,btJenisIndikator,iIndikator,sIndikator,sTarget ,sIndikatorGeser,sTargetGeser,sIndikatorMurni,sTargetMurni ) values (" +
                        "@piTahun,@pID, @pIDDInas, @pIDUrusan, @pIDProgram,@pIDKegiatan,@pJenisIndikator,@piIndikator,@psIndikator,@psTarget,@psIndikator,@psTarget,'','' )";

                    if (pTahap>3 )
                        SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun,ID, IDDInas, IDUrusan, IDProgram,IDKegiatan,btJenisIndikator,iIndikator,sIndikator,sTarget ,sIndikatorGeser,sTargetGeser,sIndikatorMurni,sTargetMurni ) values (" +
                        "@piTahun,@pID, @pIDDInas, @pIDUrusan, @pIDProgram,@pIDKegiatan,@pJenisIndikator,@piIndikator,@psIndikator,@psTarget,'','','','' )";
                    

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", ++maxID, DbType.Int32));
                    paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int64));
                    paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@piIndikator", i.iIndikator, DbType.Int16));
                    paramCollection.Add(new DBParameter("@psIndikator", i.sIndikator, DbType.String));
                    paramCollection.Add(new DBParameter("@psTarget", i.Target, DbType.String));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    if (pTahap==1 )
                        SSQL = "UPDATE " + m_sNamaTabel + " SET sIndikatorMurni='" + i.sIndikator + "' ,sTargetMurni='" + i.Target + "' , sIndikator='" + i.sIndikator + "' ,sTarget='" + i.Target + "' , sIndikatorGeser='" + i.sIndikator + "' ,sTargetGeser='" + i.Target + "' WHERE iTahun=" + i.Tahun.ToString() + " AND  IDDInas=" + i.IDDInas.ToString() + " AND  IDUrusan=" + i.IDUrusan.ToString() +
                            " AND IDProgram=" + i.IDProgram.ToString() + " AND IDKegiatan=" + i.IDKegiatan.ToString() + " AND btJenisIndikator=" + i.iJenis.ToString() + " AND ID = " + i.ID.ToString();



                    if (pTahap == 3)
                        SSQL = "UPDATE " + m_sNamaTabel + " SET sIndikator='" + i.sIndikator + "' ,sTarget='" + i.Target + "' , sIndikatorGeser='" + i.sIndikator + "' ,sTargetGeser='" + i.Target + "' WHERE iTahun=" + i.Tahun.ToString() + " AND  IDDInas=" + i.IDDInas.ToString() + " AND  IDUrusan=" + i.IDUrusan.ToString()  +
                            " AND IDProgram=" + i.IDProgram.ToString() + " AND IDKegiatan=" + i.IDKegiatan.ToString() + " AND btJenisIndikator=" + i.iJenis.ToString() + " AND ID = " + i.ID.ToString() ;

                    if (pTahap >3 )
                        SSQL = "UPDATE " + m_sNamaTabel + " SET sIndikator='" + i.sIndikator + "' ,sTarget='" + i.Target + "'  WHERE iTahun=" + i.Tahun.ToString() + " AND  IDDInas=" + i.IDDInas.ToString() + " AND  IDUrusan=" + i.IDUrusan.ToString() +
                            " AND IDProgram=" + i.IDProgram.ToString() + " AND IDKegiatan=" + i.IDKegiatan.ToString() + " AND btJenisIndikator=" + i.iJenis.ToString() + " AND ID = " + i.ID.ToString();



                  //  DBParameterCollection paramCollection = new DBParameterCollection();
                  //  //paramCollection.Add(new DBParameter("@psIndikator", i.sIndikator, DbType.String));
                  //  //paramCollection.Add(new DBParameter("@psTarget", i.Target, DbType.String));

                  //  //paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
                  //  //paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
                  //  //paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
                  //  //paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
                  //  //paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int64));
                  //  //paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
                  //  paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
                  //  paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
                  //  paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
                  //  paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
                  //  paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int64));
                  //  paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
                  ////  paramCollection.Add(new DBParameter("@piIndikator", i.iIndikator, DbType.Int16));
                  //  paramCollection.Add(new DBParameter("@psIndikator", i.sIndikator, DbType.String));


                  //  paramCollection.Add(new DBParameter("@pID", i.ID, DbType.Int32));
                    _dbHelper.ExecuteNonQuery(SSQL);

                }


            }
            return true;

        }
        public bool Hapus(Indikator i, int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan)
        {

            try
            {
                if (i.NamaJenis.Trim().Length == 0)
                {
                    SSQL = "DELETE " + m_sNamaTabel + " WHERE iTahun=@piTahun AND  IDDInas=@pIDDInas AND  IDUrusan=@pIDUrusan " +
                            " AND IDProgram=@pIDProgram AND IDKegiatan=@pIDKegiatan AND btJenisIndikator=@pJenisIndikator AND ID = @pID ";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
                    paramCollection.Add(new DBParameter("@pID", i.ID, DbType.Int32));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }
                else
                {
                    SSQL = "UPDATE  " + m_sNamaTabel + "  SET sIndikator='',sTarget=''  WHERE iTahun=@piTahun AND  IDDInas=@pIDDInas AND  IDUrusan=@pIDUrusan " +
                            " AND IDProgram=@pIDProgram AND IDKegiatan=@pIDKegiatan AND btJenisIndikator=@pJenisIndikator AND ID = @pID ";


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun", pTahun, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDDInas", pIDDInas, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDUrusan", pIDUrusan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDProgram", pIDProgram, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pIDKegiatan", pIDkegiatan, DbType.Int32));
                    paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int32));//Int16));
                    paramCollection.Add(new DBParameter("@pID", i.ID, DbType.Int16));
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
        public bool SimpanJumlahDana(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, string sTarget)
        {

            // _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = " update  tIndikator SET sTarget ='Rp. " + sTarget + "'  WHERE iTahun=" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() + " " +
                          " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() + " and btJenisIndikator= 2";
            _dbHelper.ExecuteNonQuery(SSQL);

            return true;

        }
        private int GetIndikator(Indikator i)
        {
            int lRet = 0;

            SSQL = "Select count(*) from tIndikator WHERE iTahun =" + i.Tahun.ToString() + " AND IDDInas=" + i.IDDInas.ToString() +
                 " AND IDUrusan=" + i.IDUrusan.ToString() + " AND IDProgram=" + i.IDProgram.ToString() + " AND IDKegiatan=" + i.IDKegiatan.ToString() + " AND btJenisIndikator=" + i.iJenis.ToString() + " AND iIndikator=" + i.iIndikator.ToString() + " AND ID =" + i.ID.ToString();

            lRet = (int)_dbHelper.ExecuteScalar(SSQL);
            return lRet;

        }
        private int GetMaxID()
        {

            int  lRet = 0;

            SSQL = "Select max(ID) from tIndikator WHERE iTahun =" + Tahun.ToString();

            lRet = (int )_dbHelper.ExecuteScalar(SSQL);
            return lRet;

        }

        public bool Refresh(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan)
        {
            try
            {
                SSQL = "DELETE from tIndikator WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
                     " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public bool SignID()
        {
            List<Indikator> lst = Get(Tahun);
            int pID = 0;
            foreach (Indikator i in lst)
            {
                ++pID;

                SSQL = "UPDATE tIndikator SET ID= @pID WHERE iTahun=@piTahun AND  IDDInas=@pIDDInas AND  IDUrusan=@pIDUrusan " +
                            " AND IDProgram=@pIDProgram AND IDKegiatan=@pIDKegiatan AND btJenisIndikator=@pJenisIndikator AND iIndikator=@piIndikator ";


                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", pID, DbType.Int32));

                paramCollection.Add(new DBParameter("@piTahun", i.Tahun, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDDInas", i.IDDInas, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDUrusan", i.IDUrusan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDProgram", i.IDProgram, DbType.Int32));
                paramCollection.Add(new DBParameter("@pIDKegiatan", i.IDKegiatan, DbType.Int32));
                paramCollection.Add(new DBParameter("@pJenisIndikator", i.iJenis, DbType.Int16));
                paramCollection.Add(new DBParameter("@piIndikator", i.iIndikator, DbType.Int16));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


            }
            return true;




        }

        public List<Indikator> Get(int pTahun)
        {
            List<Indikator> _lst = new List<Indikator>();
            try
            {
                SSQL = "SELECT tIndikator.* from tIndikator WHERE tIndikator.iTahun =" + pTahun.ToString() + " ORDER BY tIndikator.IDDInas,tIndikator.IDUrusan,tIndikator.IDProgram,tIndikator.IDKegiatan,tIndikator.btJenisIndikator, tIndikator.iIndikator";

                //  _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Indikator()
                                {
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
                                    iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
                                    sIndikator = DataFormat.GetString(dr["sIndikator"]),

                                    Target = DataFormat.GetString(dr["sTarget"]),
                                    sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
                                    TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
                                    ID = DataFormat.GetInteger(dr["ID"])
                                    // NamaJenis = DataFormat.GetString(dr["Nama"])

                                }).ToList();
                    }
                }
                return _lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;

            }
        }

        //public TKegiatanAPBD GetJumlah(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, int _tahap, bool bGabungan = false)
        //{
        //    List<Indikator> mListUnit = new List<Indikator>();
        //    try
        //    {

        //        if (bGabungan == false)
        //        {
        //            SSQL = "SELECT tIndikator.* , JenisIndikator.Nama from JenisIndikator LEFT OUTER JOIN tIndikator ON tIndikator.btJenisIndikator= JenisIndikator.iJenis WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
        //                 " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
        //                 " ORDER BY btJenisIndikator, iIndikator";

        //        }
        //        else
        //        {

        //            SSQL = "SELECT tIndikator.* , JenisIndikator.Nama from JenisIndikator LEFT OUTER JOIN tIndikator ON tIndikator.btJenisIndikator= JenisIndikator.iJenis WHERE iTahun =" + pTahun.ToString() +
        //                 " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
        //                 " AND IDDInas in ( SELECT ID as IDDinas from mSKPD WHERE Parent = (Select Parent from mSKPD WHERE ID = " + pIDDInas.ToString() + "))" +
        //                 " ORDER BY btJenisIndikator, iIndikator";


        //        }



        //        _dbHelper.ExecuteNonQuery(SSQL);
        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);

        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {

        //                if (_tahap < 3)
        //                {
        //                    mListUnit = (from DataRow dr in dt.Rows
        //                            select new Indikator()
        //                            {
        //                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                                IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
        //                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
        //                                iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
        //                                iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
        //                                sIndikator = DataFormat.GetString(dr["sIndikatorMurni"]),
        //                                Target = DataFormat.GetString(dr["sTargetMurni"]),
        //                                sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
        //                                TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
        //                                NamaJenis = DataFormat.GetString(dr["Nama"]),
        //                                ID = DataFormat.GetInteger(dr["ID"])

        //                            }).ToList();
        //                }
        //                if (_tahap == 3)
        //                {
        //                    mListUnit = (from DataRow dr in dt.Rows
        //                            select new Indikator()
        //                            {
        //                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                                IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
        //                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
        //                                iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
        //                                iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
        //                                sIndikator = DataFormat.GetString(dr["sIndikatorGeser"]),
        //                                Target = DataFormat.GetString(dr["sTargetGeser"]),
        //                                sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
        //                                TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
        //                                NamaJenis = DataFormat.GetString(dr["Nama"]),
        //                                ID = DataFormat.GetInteger(dr["ID"])

        //                            }).ToList();
        //                }
        //                if (_tahap > 3)
        //                {
        //                    mListUnit = (from DataRow dr in dt.Rows
        //                            select new Indikator()
        //                            {
        //                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
        //                                IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
        //                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
        //                                iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
        //                                iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
        //                                sIndikator = DataFormat.GetString(dr["sIndikator"]),
        //                                Target = DataFormat.GetString(dr["sTarget"]),
        //                                sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
        //                                TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
        //                                NamaJenis = DataFormat.GetString(dr["Nama"]),
        //                                ID = DataFormat.GetInteger(dr["ID"])

        //                            }).ToList();
        //                }
        //            }
        //            else
        //            {
        //                mListUnit.Add(new Indikator()
        //                {
        //                    Tahun = 0,
        //                    IDDInas = 0,
        //                    IDUrusan = 0,
        //                    IDProgram = 0,
        //                    IDKegiatan = 0,
        //                    iJenis = 1,
        //                    iIndikator = 0,
        //                    sIndikator = "",
        //                    Target = "",
        //                    NamaJenis = "Capaian Program"

        //                });

        //                mListUnit.Add(new Indikator()
        //                {
        //                    Tahun = 0,
        //                    IDDInas = 0,
        //                    IDUrusan = 0,
        //                    IDProgram = 0,
        //                    IDKegiatan = 0,
        //                    iJenis = 2,
        //                    iIndikator = 0,
        //                    sIndikator = "",
        //                    Target = "",
        //                    NamaJenis = "Masukan"

        //                });

        //                mListUnit.Add(new Indikator()
        //                {
        //                    Tahun = 0,
        //                    IDDInas = 0,
        //                    IDUrusan = 0,
        //                    IDProgram = 0,
        //                    IDKegiatan = 0,
        //                    iJenis = 3,
        //                    iIndikator = 0,
        //                    sIndikator = "",
        //                    Target = "",
        //                    NamaJenis = "Keluaran"

        //                });

        //                mListUnit.Add(new Indikator()
        //                {
        //                    Tahun = 0,
        //                    IDDInas = 0,
        //                    IDUrusan = 0,
        //                    IDProgram = 0,
        //                    IDKegiatan = 0,
        //                    iJenis = 4,
        //                    iIndikator = 0,
        //                    sIndikator = "",
        //                    Target = "",
        //                    NamaJenis = "Hasil"

        //                });

        //            }
        //        }


        //        return mListUnit;
        //    }
        //    finally
        //    {


        //    }


        //}

        public List<Indikator> Get(int pTahun, int pIDDInas, int pIDUrusan, int pIDProgram, int pIDkegiatan, int _tahap, List<SKPD> lst =null)
        {
            List<Indikator> _lst = new List<Indikator>();
            try
            {

                if (lst == null)
                {
                    SSQL = "SELECT tIndikator.* , JenisIndikator.Nama, '' AS NAMASKPD from JenisIndikator LEFT OUTER JOIN tIndikator ON tIndikator.btJenisIndikator= JenisIndikator.iJenis WHERE iTahun =" + pTahun.ToString() + " AND IDDInas=" + pIDDInas.ToString() +
                         " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
                         " ORDER BY btJenisIndikator, iIndikator";

                }
                else
                {
                    int idx = 0;
                    foreach (SKPD s in lst){
                        if (idx == 0)
                        {
                            SSQL = "SELECT tIndikator.* , JenisIndikator.Nama , '' AS NAMASKPD from JenisIndikator LEFT OUTER JOIN tIndikator ON tIndikator.btJenisIndikator= JenisIndikator.iJenis WHERE iTahun =" + pTahun.ToString() +
                             " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
                             " AND IDDInas =" + s.ID.ToString();// +" and sIndikatorMurni <> '' ";
                        }
                        else
                        

                            {

                                SSQL = SSQL + " UNION ALL SELECT tIndikator.* , JenisIndikator.Nama , ' (" + s.Nama + ")' AS NAMASKPD from JenisIndikator LEFT OUTER JOIN tIndikator ON tIndikator.btJenisIndikator= JenisIndikator.iJenis WHERE iTahun =" + pTahun.ToString() +
                                     " AND IDUrusan=" + pIDUrusan.ToString() + " AND IDProgram=" + pIDProgram.ToString() + " AND IDKegiatan=" + pIDkegiatan.ToString() +
                                     " AND IDDInas =" + s.ID.ToString() + " AND btJenisIndikator >2 ";//and sIndikatorMurni <> '' ";
                                     
                            }
                        //RKA gabungan 
                        idx++;
                        }
                    SSQL = SSQL + " ORDER BY btJenisIndikator, IDDinas, iIndikator";

                        
                               
                }

                
                
                _dbHelper.ExecuteNonQuery(SSQL);
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        if (_tahap < 3)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new Indikator()
                                    {
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
                                        iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
                                        sIndikator = DataFormat.GetString(dr["sIndikatorMurni"]) + DataFormat.GetString(dr["NAMASKPD"])  ,
                                        Target = DataFormat.GetString(dr["sTargetMurni"]),
                                        sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
                                        TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
                                        NamaJenis = DataFormat.GetString(dr["Nama"]),
                                        ID = DataFormat.GetInteger(dr["ID"])

                                    }).ToList();
                        }
                        if (_tahap == 3)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new Indikator()
                                    {
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
                                        iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
                                        sIndikator = DataFormat.GetString(dr["sIndikatorGeser"]) + DataFormat.GetString(dr["NAMASKPD"]),
                                        Target = DataFormat.GetString(dr["sTargetGeser"]),
                                        sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
                                        TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
                                        NamaJenis = DataFormat.GetString(dr["Nama"]),
                                        ID = DataFormat.GetInteger(dr["ID"])

                                    }).ToList();
                        }
                        if (_tahap > 3)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new Indikator()
                                    {
                                        Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDinas"]),
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        iJenis = DataFormat.GetSingle(dr["btJenisIndikator"]),
                                        iIndikator = DataFormat.GetInteger(dr["iIndikator"]),
                                        sIndikator = DataFormat.GetString(dr["sIndikator"]) + DataFormat.GetString(dr["NAMASKPD"]),
                                        Target = DataFormat.GetString(dr["sTarget"]),
                                        sIndikatorMurni = DataFormat.GetString(dr["sIndikatorMurni"]),
                                        TargetMurni = DataFormat.GetString(dr["sTargetMurni"]),
                                        NamaJenis = DataFormat.GetString(dr["Nama"]),
                                        ID = DataFormat.GetInteger(dr["ID"])

                                    }).ToList();
                        } 
                    }
                    else
                    {
                        _lst.Add(new Indikator()
                        {
                            Tahun = 0,
                            IDDInas = 0,
                            IDUrusan = 0,
                            IDProgram = 0,
                            IDKegiatan = 0,
                            iJenis = 1,
                            iIndikator = 0,
                            sIndikator = "",
                            Target = "",
                            NamaJenis = "Capaian Kegiatan"

                        });

                        _lst.Add(new Indikator()
                        {
                            Tahun = 0,
                            IDDInas = 0,
                            IDUrusan = 0,
                            IDProgram = 0,
                            IDKegiatan = 0,
                            iJenis = 2,
                            iIndikator = 0,
                            sIndikator = "",
                            Target = "",
                            NamaJenis = "Masukan"

                        });

                        _lst.Add(new Indikator()
                        {
                            Tahun = 0,
                            IDDInas = 0,
                            IDUrusan = 0,
                            IDProgram = 0,
                            IDKegiatan = 0,
                            iJenis = 3,
                            iIndikator = 0,
                            sIndikator = "",
                            Target = "",
                            NamaJenis = "Keluaran"

                        });

                        _lst.Add(new Indikator()
                        {
                            Tahun = 0,
                            IDDInas = 0,
                            IDUrusan = 0,
                            IDProgram = 0,
                            IDKegiatan = 0,
                            iJenis = 4,
                            iIndikator = 0,
                            sIndikator = "",
                            Target = "",
                            NamaJenis = "Hasil"

                        });

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
