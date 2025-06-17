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
    public class RealisasiLogic:BP

    {

        public RealisasiLogic(int pTahun)
            : base(pTahun)
        {

        }

        public List<Realisasi> Get(int idDinas,int KodeUK,long idSUbKegiatan,  DateTime tanggal, long NoUrut =0,int _statusSPP = 4)
        {
            List<Realisasi> lst = new List<Realisasi>();
            try{
                SSQL = "";
                SSQL = " Select IDSUBKegiatan, IIDRekening, SUM(debet * cJumlah) as Jumlah from ( ";

              
                SSQL = SSQL+ " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, b.cJumlah  FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut  Where a.btJenis > 1 " +
                     " And a.iStatus >= " + _statusSPP.ToString() + " and a.iStatus <> 9 AND IDDInas =" + idDinas.ToString() + " AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString();
                if (NoUrut > 0)
                {

                    SSQL = SSQL + " AND A.iNourut <= " + NoUrut.ToString();
                }

                SSQL = SSQL + " Union ALL ";
                
                SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, B.cJumlah  FROM tPanjar A INNER join   tPanjarRekening B ON a.inourut=B.inourut Where " +
                    " btJenisBelanja in (0,1,2)  AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat();


                SSQL = SSQL + "Union ALL ";
                SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  -1 as DEBET, B.cJumlah  FROM tSetor A INNER join   tSetorRekening B ON a.inourut=B.inourut Where " +
                     " A.btJenis=3 AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat();

                SSQL = SSQL + "Union ALL ";
                SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening1 as IIDRekening,  -1 as DEBET, B.cJumlah1 as cJumlah  FROM tKoreksi  A INNER join   tKoreksiDetail  B ON a.inourut=B.inourut Where " +
                    " A.IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtKoreksi <= " + tanggal.ToSQLFormat();
                ////SSQL = SSQL + "Union ALL ";
                ////SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening1 as IIDRekening,  -1* b.iDebet1 as DEBET,  B.cJumlah1 as cJumlah  FROM tKoreksi  A INNER join   tKoreksiDetail  B ON a.inourut=B.inourut Where " +
                ////     " A.IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtKoreksi <= " + tanggal.ToSQLFormat();
                


                
                SSQL = SSQL + ") A group BY IDSUBKegiatan, IIDrekening ";
                /*
                SSQL = "";
                SSQL = " Select IDSUBKegiatan, IIDRekening, SUM(debet * cJumlah) as Jumlah from ( ";


                SSQL = SSQL + " dbo.fnRealisasi(" + idDinas.ToString() + "," +
                                            KodeUK.ToString() + "," +
                                            idSUbKegiatan.ToString() + "," +
                                            tanggal.ToSQLFormat() + " ";




                SSQL = SSQL + ") A group BY IDSUBKegiatan, IIDrekening ";

 
                */
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                                select new Realisasi()
                                {
                                    
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]),
                                    IIDRekening= DataFormat.GetLong(dr["IIDRekening"]),
                                    cJumlah= DataFormat.GetDecimal(dr["Jumlah"]),
                             

                                    }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }

        }
        public List<Realisasi> GetUntukSPJ(int idDinas, int KodeUK, long idSUbKegiatan, DateTime tanggal, long NoUrut = 0, int _statusSPP = 4)
        {
            List<Realisasi> lst = new List<Realisasi>();
            try
            {
                SSQL = "";
                SSQL = " Select IDSUBKegiatan, IIDRekening, SUM(debet * cJumlah) as Jumlah from ( ";


                SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, b.cJumlah  FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut  Where a.btJenis >= 2 " +
                     " And a.iStatus >= " + _statusSPP.ToString() + " and a.iStatus <> 9 AND IDDInas =" + idDinas.ToString() + " AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString();
                if (NoUrut > 0)
                {

                    SSQL = SSQL + " AND A.iNourut <= " + NoUrut.ToString();
                }

                SSQL = SSQL + " Union ALL ";

                SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, B.cJumlah  FROM tPanjar A INNER join   tPanjarRekening B ON a.inourut=B.inourut Where " +
                     " btJenisBelanja in (0,1,2)  AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat();


                SSQL = SSQL + "Union ALL ";
                SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  -1 as DEBET, B.cJumlah  FROM tSetor A INNER join   tSetorRekening B ON a.inourut=B.inourut Where " +
                     " A.btJenis=3 AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat();

                //SSQL = SSQL + "Union ALL ";
                //SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening1 as IIDRekening,  -1 as DEBET, B.cJumlah1 as cJumlah  FROM tKoreksi  A INNER join   tKoreksiDetail  B ON a.inourut=B.inourut Where " +
                //     " A.IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtKoreksi <= " + tanggal.ToSQLFormat();
                SSQL = SSQL + "Union ALL ";
                SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening1 as IIDRekening,  -1* b.iDebet1 as DEBET,  B.cJumlah1 as cJumlah  FROM tKoreksi  A INNER join   tKoreksiDetail  B ON a.inourut=B.inourut Where " +
                     " A.IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtKoreksi <= " + tanggal.ToSQLFormat();




                SSQL = SSQL + ") A group BY IDSUBKegiatan, IIDrekening ";




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new Realisasi()
                               {

                                   IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]),
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   cJumlah = DataFormat.GetDecimal(dr["Jumlah"]),


                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }

        }
        public List<Realisasi> GetUntukSPJTU(int idDinas, int KodeUK, long idSUbKegiatan, DateTime tanggal, long NoUrutSP2D, int _statusSPP = 4)
        {
            List<Realisasi> lst = new List<Realisasi>();
            try
            {
                SSQL = "";
                SSQL = " Select IDSUBKegiatan, IIDRekening, SUM(debet * cJumlah) as Jumlah from ( ";


                
                SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, B.cJumlah  FROM tPanjar A INNER join   tPanjarRekening B ON a.inourut=B.inourut Where " +
                     " btJenisBelanja in (2)  AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat() +
                     " AND inourutspp=" + NoUrutSP2D.ToString(); 


                



                SSQL = SSQL + ") A group BY IDSUBKegiatan, IIDrekening ";




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new Realisasi()
                               {

                                   IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]),
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   cJumlah = DataFormat.GetDecimal(dr["Jumlah"]),


                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }

        }
        public List<Realisasi> GetSebelum(int idDinas, int KodeUK, long idSUbKegiatan, DateTime tanggal, long NoUrut = 0, int _statusSPP = 4)
        {
            List<Realisasi> lst = new List<Realisasi>();
            try
            {
                SSQL = "";
                SSQL = " Select IDSUBKegiatan, IIDRekening, SUM(debet * cJumlah) as Jumlah from ( ";
                SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, b.cJumlah  FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut  Where a.btJenis >= 1 " +
                     " And a.iStatus >= " + _statusSPP.ToString() + " and a.iStatus <> 9 AND IDDInas =" + idDinas.ToString() + " AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString();
                if (NoUrut > 0)
                {

                    SSQL = SSQL + " AND A.iNourut < " + NoUrut.ToString();
                }
                //SSQL = SSQL + " Union ALL ";

                //SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  1 as DEBET, B.cJumlah  FROM tPanjar A INNER join   tPanjarRekening B ON a.inourut=B.inourut Where " +
                //     " btJenisBelanja in (0,1,2)  AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat();


                //SSQL = SSQL + "Union ALL ";
                //SSQL = SSQL + " Select A.IDDInas, A.IDUrusan,A.IDProgram, A.IDKegiatan,A.IDSUBKegiatan, B.IIDRekening,  -1 as DEBET, B.cJumlah  FROM tSetor A INNER join   tSetorRekening B ON a.inourut=B.inourut Where " +
                //     " A.btJenis=3 AND IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND A.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtBukukas <= " + tanggal.ToSQLFormat();

                //SSQL = SSQL + "Union ALL ";
                //SSQL = SSQL + " Select A.IDDInas, B.IDUrusan,B.IDProgram, B.IDKegiatan,B.IDSUBKegiatan, B.IIDRekening1 as IIDRekening,  -1 as DEBET, B.cJumlah1 as cJumlah  FROM tKoreksi  A INNER join   tKoreksiDetail  B ON a.inourut=B.inourut Where " +
                //     " A.IDDInas =" + idDinas.ToString() + "  AND A.UnitAnggaran = " + KodeUK.ToString() + " AND B.IDSUbKegiatan =" + idSUbKegiatan.ToString() + " AND A.dtKoreksi <= " + tanggal.ToSQLFormat();
                SSQL = SSQL + ") A group BY IDSUBKegiatan, IIDrekening ";




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new Realisasi()
                               {

                                   IDSubKegiatan = DataFormat.GetLong(dr["IDSubkegiatan"]),
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   cJumlah = DataFormat.GetDecimal(dr["Jumlah"]),


                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }

        }
        public List<Realisasi> GetgroupedByKegiatanAndRekening(int idDinas, DateTime tanggal, int _statusSPP = 4, long inourutSPP=0)
        {
            List<Realisasi> lst = new List<Realisasi>();


               DBParameterCollection paramCollection = new DBParameterCollection();
              



            try
            {
                SSQL = "SELECT  IDDInas,IDUrusan, IDprogram, IDkegiatan,btKodeUK, IDkegiatan, idsubkegiatan,IIDRekening, SUM(DEBET * cJumlah) as jumlah from REALISASI04 WHERE " +
                    "IDDInas =@IDDInas AND dtDocument <= @TANGGAL";

                paramCollection.Add(new DBParameter("@IDDInas", idDinas));
                paramCollection.Add(new DBParameter("@TANGGAL", tanggal.Date,DbType.Date));


                if (inourutSPP > 0)
                {
                    SSQL = SSQL + " AND inourut < @NOURUTSPP";
                    SSQL = SSQL + " AND IDSubKegiatan in (select idsubkegiatan from tSPPRekening Where inourut = @NOURUTSPP)";


                    paramCollection.Add(new DBParameter("@NOURUTSPP", inourutSPP));


                }
                SSQL = SSQL + " Group by REALISASI04.IDDInas,REALISASI04.IDUrusan, REALISASI04.IDprogram, REALISASI04.IDkegiatan, REALISASI04.btKodeuk,REALISASI04.IDkegiatan,idsubkegiatan, REALISASI04.IIDRekening Order by REALISASI04.IDkegiatan, REALISASI04.IIDRekening";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new Realisasi()
                               {

                                  // TABEL = DataFormat.GetInteger(dr["TABEL"]),
                                   IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                   IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                   IDProgran = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDKegiatan = DataFormat.GetLong(dr["IDKegiatan"]),
                                   IDSubKegiatan = DataFormat.GetLong(dr["IDsubKegiatan"]),
                                   btKodeUK = DataFormat.GetInteger(dr["btKOdeUK"]),                                 
                                   IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                   cJumlah = DataFormat.GetDecimal(dr["Jumlah"]),
             

                               }).ToList();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return lst;
            }

        }
        


    }
}
