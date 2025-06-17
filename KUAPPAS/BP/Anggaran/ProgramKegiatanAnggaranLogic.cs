using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Anggaran;
using BP;
using Formatting;
using System.Data;
using DataAccess;
namespace BP.Anggaran
{
    public class ProgramKegiatanAnggaranLogic:BP 
    {
        public ProgramKegiatanAnggaranLogic(int thn)
            : base (thn)
        {
            Tahun = thn;
        }
        //public ProgramKegiatan GetByIDSub(long idSubKegiatan)
        //{
        //    ProgramKegiatan pk = new ProgramKegiatan();
        //    try
        //    {
        //        SSQL = "SELECT * FROM ProgramKegiatan  Where idsubkegiatan =@IDSUBKEGIATAN";

        //       DBParameterCollection paramCollection = new DBParameterCollection();
        //       paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idSubKegiatan));



        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
        //        if (dt != null)
        //        {
                    

        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];

        //                pk = new ProgramKegiatan()
        //                {

        //                    StrIDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]).ToKodeUrusan(),
        //                    StrIDProgram = DataFormat.GetInteger(dr["IdProgram"]).ToKodeProgram(),
        //                    StrIDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]).ToKodeKegiatan (),
        //                    StrIDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]).ToKodeSubKegiatan (),

        //                    IDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]),
        //                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
        //                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),


        //                    NamaUrusan = DataFormat.GetString(dr["NamaUrusanProgram"]),
        //                    NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
        //                    NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
        //                    NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"])
                            
        //                };
        //            }
        //        }
        //        return pk ;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return pk;
        //    }

        //}
        public List<ProgramKegiatanAnggaran> GetByDInas(int iddinas, int Kodeunit=0)
        {

            List<ProgramKegiatanAnggaran> lst = new List<ProgramKegiatanAnggaran>();
            try
            {
                SSQL = "SELECT A.* ,mRekening.sNamaRekening, B.IIDRekening,B.cJumlahMurni,B.cJumlahGeser,B.cJumlahRKAP," +
                        " B.cJumlahABT  from ProgramKegiatan A inner join tAnggaranRekening_A B ON A.IDDInas = B.IDDInas and A.KodeUK= B.btKOdeUK and A.Jenis= B.btJenis  " +
                        " AND A.IDSUbKegiatan= B.IDSUbKegiatan INNER JOIN mRekening on B.IIDrekening= mRekening.IIDrekening " +
                        " Where A.iTahun =@Tahun and B.IIDRekening like '5%'";
                DBParameterCollection paramCollection = new DBParameterCollection();
                if (iddinas > 0)
                {
                    SSQL = SSQL + " AND A.IDDINAS =@Dinas";
                    paramCollection.Add(new DBParameter("@Dinas", iddinas));
                }
                               
                
                paramCollection.Add(new DBParameter("@Tahun", Tahun));
                if (Kodeunit > 1)
                {
                    SSQL = SSQL + " and A.KodeUK =@KodeUK ";
                                  paramCollection.Add(new DBParameter("@KodeUK", Kodeunit));
                }

                SSQL = SSQL + " Order by A.Jenis,A.KodeUK,A.IDUrusanProgram,A.IDProgram, A.IDkegiatan,A.IDSubKegiatan, B.IIDRekening ";

               // SSQL = SSQL + " order by A.IDUrusanProgram, A.IDProgram , A.IDkegiatan, A.IDSUbKegiatan,B.IIDRekening";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                
                if (dt != null)
                {


                    if (dt.Rows.Count > 0)
                    {

                        lst = (from DataRow dr in dt.Rows
                               select new ProgramKegiatanAnggaran()
                                {
                                    IDDInas =  DataFormat.GetInteger(dr["IDDinas"]),
                                   Jenis =   DataFormat.GetInteger(dr["Jenis"]),
                                    NamaUK = DataFormat.GetString(dr["NamaOrganisasi"]),
                                    KodeUK = DataFormat.GetInteger(dr["KodeUK"]),
                                    KeyKegiatan = (DataFormat.GetInteger(dr["IDKegiatan"]) * 100) + DataFormat.GetInteger(dr["KodeUK"]),

                                    StrIDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]).ToKodeUrusan(),
                                    StrIDProgram = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                    StrIDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]).ToKodeKegiatan(),
                                    StrIDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]).ToKodeSubKegiatan(),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    KeySubKegiatan =(DataFormat.GetLong(dr["IDSubKegiatan"]) * 100)+ DataFormat.GetInteger(dr["KodeUK"]),
                                    IIDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                    
                                    NamaUrusan = DataFormat.GetString(dr["NamaUrusanProgram"]),
                                    NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                    NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                    NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),
                                    AnggaranMurni = DataFormat.GetDecimal(dr["cJumlahMurni"]),
                                    AnggaranGeser = DataFormat.GetDecimal(dr["cJumlahGeser"]),
                                    AnggaranRKAP = DataFormat.GetDecimal(dr["cJumlahRKAP"]),
                                    AnggaranABT = DataFormat.GetDecimal(dr["cJumlahABT"]),

                                }).ToList();
                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }

        }

    }
}
