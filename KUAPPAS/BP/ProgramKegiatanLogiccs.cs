using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using System.Data;
using DataAccess;

namespace BP
{
    public class ProgramKegiatanLogiccs:BP
    {

        public ProgramKegiatanLogiccs(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
      

        }
        
        public ProgramKegiatan GetByIDSub(long idSubKegiatan)
        {

            ProgramKegiatan pk = new ProgramKegiatan();
            try
            {
                SSQL = "SELECT * FROM ProgramKegiatan  Where idsubkegiatan =@IDSUBKEGIATAN";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idSubKegiatan));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        pk = new ProgramKegiatan()
                        {

                            StrIDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]).ToKodeUrusan(),
                            StrIDProgram = DataFormat.GetInteger(dr["IdProgram"]).ToKodeProgram(),
                            StrIDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]).ToKodeKegiatan (),
                            StrIDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]).ToKodeSubKegiatan (),

                            IDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]),
                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                            IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                            IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),


                            NamaUrusan = DataFormat.GetString(dr["NamaUrusanProgram"]),
                            NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                            NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                            NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"])
                            
                        };
                    }
                }
                return pk ;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return pk;
            }

        }
        public List<ProgramKegiatan> GetByDInas(int iddinas, int Kodeunit = 0)
        {

            List<ProgramKegiatan> lst = new List<ProgramKegiatan>();
            try
            {
                SSQL = "SELECT * FROM ProgramKegiatan  Where IDDINAS =@Dinas and KodeUK =@KodeUK order by IDUrusanProgram, IDProgram , IDkegiatan, IDSUbKegiatan";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Dinas", iddinas));
                paramCollection.Add(new DBParameter("@KodeUK", Kodeunit));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);

                if (dt != null)
                {


                    if (dt.Rows.Count > 0)
                    {

                        lst = (from DataRow dr in dt.Rows
                               select new ProgramKegiatan()
                               {

                                   StrIDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]).ToKodeUrusan(),
                                   StrIDProgram = DataFormat.GetInteger(dr["IDProgram"]).ToKodeProgram(),
                                   StrIDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]).ToKodeKegiatan(),
                                   StrIDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]).ToKodeSubKegiatan(),
                                   IDUrusan = DataFormat.GetInteger(dr["IDUrusanProgram"]),
                                   IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                   IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),

                                   NamaUrusan = DataFormat.GetString(dr["NamaUrusanProgram"]),
                                   NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                   NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                   NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"])

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
        public bool Simpan (ProgramKegiatan pk){

                try{

                    SSQL="INSERT INTO ProgramKegiatan (NamaOrganisasi,KodeUK,Kode,iTahun,IDDInas,iDurusanProgram," +
                                "NamaUrusanProgram,btKodekategoriPElaksana,btKodeUrusan," +
                                "IDProgram, NamaProgram, IDKegiatan , NamaKegiatan, IDSUbKegiatan, NamaSubKegiatan) values( " +
                                "@NamaOrganisasi,@KodeUK,@Kode,@Tahun,@IDDInas,@DurusanProgram," +
                                "@NamaUrusanProgram,@btKodekategoriPElaksana,@btKodeUrusan," +
                                "@IDProgram, @NamaProgram, @IDKegiatan , @NamaKegiatan, @IDSUbKegiatan, @NamaSubKegiatan)";
                    int KKP = DataFormat.GetInteger(pk.IDUrusan.ToString().Substring(0,1));
                    int KKU = DataFormat.GetInteger(pk.IDUrusan.ToString().Substring(1));

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@NamaOrganisasi",pk.NamaDinas));
                    paramCollection.Add(new DBParameter("@KodeUK",pk.KodeUK));
                    paramCollection.Add(new DBParameter("@Kode",pk.KodeDinas ));
                    paramCollection.Add(new DBParameter("@Tahun",pk.Tahun));
                    paramCollection.Add(new DBParameter("@IDDInas",pk.IDDInas));
                    paramCollection.Add(new DBParameter("@DurusanProgram",pk.IDUrusan));
                    paramCollection.Add(new DBParameter("@NamaUrusanProgram", pk.NamaUrusan));
                    paramCollection.Add(new DBParameter("@btKodekategoriPElaksana",KKP));
                    paramCollection.Add(new DBParameter("@btKodeUrusan",KKU));
                    paramCollection.Add(new DBParameter("@IDProgram",pk.IDProgram));
                    paramCollection.Add(new DBParameter("@NamaProgram",pk.NamaProgram));
                    paramCollection.Add(new DBParameter("@IDKegiatan",pk.IDKegiatan));
                    paramCollection.Add(new DBParameter("@NamaKegiatan",pk.NamaKegiatan));
                    paramCollection.Add(new DBParameter("@IDSUbKegiatan",pk.IDSubKegiatan));
                    paramCollection.Add(new DBParameter("@NamaSubKegiatan",pk.NamaSubKegiatan));
                    
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    return true ;
                } catch(Exception ex){
                    return false ;

                }
             }            
        
    }
}
