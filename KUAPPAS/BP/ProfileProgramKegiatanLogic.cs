using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using BP;
using System.Data;


namespace BP
{
    class ProfileProgramKegiatanLogic:BP
    {
        public ProfileProgramKegiatanLogic(int _pTahun)
            : base(_pTahun)
        {
        //    Tahun = _pTahun;
            m_sNamaTabel = "ProfileProgramKegiatan";
            CekTable();
        }
        private void CekTable()
        {
            try
            {
                SSQL = "SELECT * FROM ProfileProgramKegiatan";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (_isError == true)
                {
                    SSQL = "CREATE TABLE ProfileProgramKegiatan (KodeProgram int, KodeKegiatan int)";
                    _dbHelper.ExecuteNonQuery(SSQL);
                    SSQL = "INSERT INTO ProfileProgramKegiatan (KodeProgram, KodeKegiatan) values (2,3)";
                    _dbHelper.ExecuteNonQuery(SSQL);
                }
                return;
            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE ProfileProgramKegiatan (KodeProgram int, KodeKegiatan int)";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO ProfileProgramKegiatan (KodeProgram, KodeKegiatan) values (2,3)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }

        }
        public ProfileProgramKegiatan Get()
        {
            ProfileProgramKegiatan oRet = new ProfileProgramKegiatan();
            try
            {
                SSQL = "SELECT * FROM ProfileProgramKegiatan";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                       int angkaNol;
                       angkaNol=0;
                       DataRow dr = dt.Rows[0];
                       oRet = new ProfileProgramKegiatan()
                        {
                            KodeProgram = DataFormat.GetInteger(dr["KodeProgram"]),
                            KodeKegiatan = DataFormat.GetInteger(dr["KodeKegiatan"]),
                            LENPRG = DataFormat.GetInteger(dr["KodeProgram"]),
                            LENKEG = DataFormat.GetInteger(dr["KodeProgram"])+ DataFormat.GetInteger(dr["KodeKegiatan"]),
                            FORMATPRG = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["KodeProgram"])),
                            FORMATKEG = angkaNol.IntToStringWithLeftPad(DataFormat.GetInteger(dr["KodeKegiatan"]))
                            


                        };
                    }
                }
                return oRet;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return oRet;
            }
        }

        public bool Simpan(ProfileProgramKegiatan _profile)
        {      
            try
            {

                SSQL = "DELETE ProfileProgramKegiatan";
                
               _dbHelper.ExecuteNonQuery(SSQL);

               SSQL = "INSERT INTO ProfileProgramKegiatan (KodeProgram, KodeKegiatan) values (?,?)";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KodeProgram", _profile.KodeProgram));
                paramCollection.Add(new DBParameter("@Kodekegiatan", _profile.KodeKegiatan));
                

                _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
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
