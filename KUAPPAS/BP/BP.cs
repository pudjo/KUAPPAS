using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

using System.Data.SqlClient;
using Formatting;
using DataAccess;
using DTO;
using DTO.Bendahara;

namespace BP
{
    public class BP
    {
         
        protected DBHelper _dbHelper = new DBHelper();

        protected bool _isError;
        protected string _lastError;
        protected string m_sNamaTabel;
        protected string SSQL = String.Empty;
        protected DefaultSystem g_system;
        protected static ProfileRekening m_ProfileRekening;
        protected static ProfileProgramKegiatan m_ProfileProgKegiatan;
        protected string _namaKolom1 = "";
        protected string _namaKolom2 = "";
        protected string _namaKolomUraian1 = "";
        protected string _namaKolomUraian2 = "";
        protected string _namaKolomvolume1 = "";
        protected string _namaKolomvolume2 = "";
        protected string _namaKolomharga1 = "";
        protected string _namaKolomharga2 = "";
        protected string _namaKolomjumlahuraian1 = "";
        protected string _namaKolomjumlahuraian2 = "";
        protected int Tahun = 2024;
        protected int mkepmen;
        protected List<BKU> gListBKU;

        public enum E_KOLOM_NOURUT{
                CON_URUT_JURNAL = 1,
                CON_URUT_SPP = 2,
                CON_URUT_STS = 3,
                CON_URUT_SPJ = 4,
                CON_URUT_CP = 5,
                CON_URUT_SPD = 6,
                CON_URUT_PANJAR = 7,
                CON_URUT_PAJAK = 8,
                CON_URUT_SETOR = 9,        
                CON_URUT_SKRSKPD = 11,
                CON_URUT_BAST = 12,
                CON_URUT_KAS = 13,
                CON_URUT_PENGUJI = 14,
                CON_URUT_JURNALP = 15,
                CON_URUT_UP = 16,
                CON_URUT_TRX_ASET = 17,
                CON_URUT_INVESTASI = 22,
                CON_URUT_UTANG = 23,
                CON_URUT_Kontrak = 24,
                CON_URUT_SPMNIHIL = 25,
                CON_URUT_BLUD = 26,
                CON_URUT_KOREKSI = 27

        }


    public enum JENIS_JURNAL{
        JENIS_JURNALPENERIMAAN = 1,
        JENIS_JURNALPENGELUARAN = 2,
        JENIS_JURNALUMUM = 3,
        JENIS_JURNALPENUTUP = 4,
        JENIS_JURNALELIMINASI = 5,
        JENIS_JURNALANGGARAN = 6,
        JENIS_JURNALPENYESUAIAN = 7
    }

      public enum JENIS_SUMBERJURNAL{
        E_SUMBER_DPA = 0,
        E_SUMBER_SKR = 1,
        E_SUMBER_STS = 2,
        E_SUMBER_SETOR = 3,

        S_SUMBER_BAST = 4,
        E_SUMBER_SP2D = 5,
        E_SUMBER_PANJAR = 6,
        E_SUMBER_MANUAL = 7,
        E_JURNAL_PENYESUAIAN = 9,
        E_JURNAL_PENYUSUTAN = 10,
        E_SUMBER_TRXASET = 11,
        E_SUMBER_INVESTASI = 12,
        E_SUMBER_UTANG = 13,
        E_SUMBER_KOREKSI = 14,
        E_SUMBER_PENUTUP = 20
        }
      public BP()
      {

      }
      public BP(List<BKU> lstBKU)
      {
          gListBKU = new List<BKU>();
          gListBKU = lstBKU;
      }
        public long   ReadNo(E_KOLOM_NOURUT nIndex , int iddinas, bool bUrutNyaSaja = false, int pKodeUK =-1) 
        {
            try
            {
                int iNo = 1;
                string SQLUPDATE = "";
                string colName = "";
                SSQL = "SELECT * FROM mAutoNumber where iTahun = " + Tahun.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        iNo = DataFormat.GetInteger(dr[(int)nIndex]);

                        colName = dt.Columns[(int)nIndex].ColumnName;


                        SQLUPDATE = "UPDATE mAutoNumber set " + colName + "=" + colName + " + 1 Where  iTahun = " + Tahun.ToString() +
                                " AND " + colName + " is not null";
                        _dbHelper.ExecuteNonQuery(SQLUPDATE);

                        SQLUPDATE = "UPDATE mAutoNumber set " + colName + "= 2 Where  iTahun = " + Tahun.ToString() +
                                   " AND " + colName + " is null";
                        _dbHelper.ExecuteNonQuery(SQLUPDATE);

                    }
                    else
                    {
                        SSQL = "INSERT INTO mAutoNumber (iTahun) values ( " + Tahun.ToString() + ")";
                        _dbHelper.ExecuteNonQuery(SSQL);
                        SSQL = "SELECT * FROM mAutoNumber where iTahun = " + Tahun.ToString();

                        DataTable dt2 = new DataTable();
                        dt2 = _dbHelper.ExecuteDataTable(SSQL);


                        for (int col = 1; col < dt2.Columns.Count; col++)
                        {
                            SSQL = "UPDATE mAutoNumber SET " + dt2.Columns[col].ColumnName + " = 1";
                            _dbHelper.ExecuteNonQuery(SSQL);

                        }
                        iNo = 1;




                    }
                }
                char pad = '0';
                long retVal=DataFormat.GetLong(GetPrefixNourut(nIndex, iddinas) + iNo.ToString().PadLeft(5, pad));
                return retVal;
            }
            catch (Exception ex)
            {
                return 0;

            }
        }
        public   int  ReadNoPenguji(int Tahun)
        {
            try
            {
                int iNo = 1;
      
                
                SSQL = "SELECT iNoPenguji FROM mAutoNumber where iTahun = " + Tahun.ToString();

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        iNo = DataFormat.GetInteger(dr["iNoPenguji"]);

                    }
                    SSQL = "UPDATE mAutoNumber Set iNoPenguji=iNoPenguji+1  where iTahun = " + Tahun.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
                return iNo;
                
            }
            catch (Exception ex)
            {
                SSQL = "UPDATE mAutoNumber Set iNoPenguji=2  where iTahun = " + Tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);

                return 1;

            }
        }

       private string  GetPrefixNourut(E_KOLOM_NOURUT nIndex , int iddinas) {        
           char pad= '0';
           string prefix = Tahun.ToString().Substring(2, 2) + ((int)nIndex).ToString().PadLeft(2, pad) + iddinas.ToString(); 
           return prefix;
        }

       protected string GetCurrentDate()
       {
           return System.DateTime.Today.ToSQLFormat();
       }



        //public BP()
        //{

        //    _dbHelper.SetTahun(Tahun);
        //    if (m_ProfileRekening == null)
        //    {
        //        m_ProfileRekening = new ProfileRekening();
        //        ProfileRekeningLogic oLogic = new ProfileRekeningLogic(Tahun);
        //        m_ProfileRekening = oLogic.Get();
        //        if (m_ProfileRekening == null)
        //        {
        //            m_ProfileRekening.Kode1 = 1;
        //            m_ProfileRekening.Kode2 = 1;
        //            m_ProfileRekening.Kode3 = 1;
        //            m_ProfileRekening.Kode4 = 2;
        //            m_ProfileRekening.Kode5 = 2;
        //            oLogic.Simpan(m_ProfileRekening);
        //            m_ProfileRekening = oLogic.Get();


        //        }
        //    }
        //    if (m_ProfileProgKegiatan == null)
        //    {
        //        m_ProfileProgKegiatan = new ProfileProgramKegiatan();
        //        ProfileProgramKegiatanLogic oLogicPK = new ProfileProgramKegiatanLogic(Tahun);
        //        m_ProfileProgKegiatan = oLogicPK.Get();
        //        if (m_ProfileRekening == null)
        //        {
        //            m_ProfileProgKegiatan.KodeProgram = 2;
        //            m_ProfileProgKegiatan.KodeKegiatan = 3;
        //            oLogicPK.Simpan(m_ProfileProgKegiatan);
        //            m_ProfileProgKegiatan = oLogicPK.Get();


        //        }
        //    }


        //}

        public BP( int pTahun , int _perbaikan=0, int kepmen= 2 )
        {
            
            Tahun = pTahun;
            _dbHelper.SetTahun(Tahun, _perbaikan, kepmen);

            mkepmen = kepmen;
          
            m_ProfileProgKegiatan = new ProfileProgramKegiatan();
            if (Tahun <= 2020)
            {
                m_ProfileProgKegiatan.KodeProgram = 2;
                m_ProfileProgKegiatan.KodeKegiatan = 3;
                m_ProfileProgKegiatan.LENKEG = 5;
                m_ProfileProgKegiatan.LENPRG = 2;
            }
            else
            {
                m_ProfileProgKegiatan.KodeProgram = 2;
                m_ProfileProgKegiatan.KodeKegiatan = 3;
                m_ProfileProgKegiatan.LENKEG = 5;
                m_ProfileProgKegiatan.LENPRG = 2;
            }


        }
        protected bool SetProfileRekening(int profile)
        {

            ProfileRekeningLogic oProfileRekeningLogic = new ProfileRekeningLogic(Tahun, profile);
            m_ProfileRekening = oProfileRekeningLogic.GetByID(1);
            if (m_ProfileRekening == null)
                return false;
            else
                return true;

        }
        protected bool HapusView(string sNamaView)
        {
            try
            {
                SSQL = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + sNamaView + "]') and OBJECTPROPERTY(id, N'IsView') = 1) " +
                    " DROP view " + sNamaView;

                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }

        }
        
        protected void GetKolom(int tahap)
        {
            ProfileReportPerda oRptPerda = new ProfileReportPerda(tahap);
            _namaKolom1 = oRptPerda.KolomKiri;
            _namaKolom2 = oRptPerda.KolomKanan;


            
            _namaKolomUraian1 = oRptPerda._namaKolomUraian1;
            _namaKolomUraian2 = oRptPerda._namaKolomUraian2;
            _namaKolomvolume1 = oRptPerda._namaKolomvolume1;
            _namaKolomvolume2 = oRptPerda._namaKolomvolume2;
            _namaKolomharga1 = oRptPerda._namaKolomharga1;
            _namaKolomharga2 = oRptPerda._namaKolomharga2;
            _namaKolomjumlahuraian1 = oRptPerda._namaKolomjumlah1;
            _namaKolomjumlahuraian2 = oRptPerda._namaKolomjumlah2;



        }


        public bool IsError()
        {
            return _isError;
        }
        public string LastError()
        {
            return _lastError;
        }
        public int GetMaxID(string sNamaKolom ="ID")
        {
            SSQL = "SELECT MAX(" + sNamaKolom + ") from " + m_sNamaTabel;// +" WHERE iTahun = " + Tahun.ToString();
            object  objMax= _dbHelper.ExecuteScalar(SSQL,CommandType.Text);//
            if (objMax == null)
            {
                return 1;
            }
            if (objMax.ToString().Length == 0)
            {
                return 1;
            }
            return Convert.ToInt32(objMax.ToString()) + 1;

        }

        public int GetMaxIDNoYear(string sNamaKolom = "ID")
        {
            SSQL = "SELECT MAX(" + sNamaKolom + ") from " + m_sNamaTabel;
            object objMax = _dbHelper.ExecuteScalar(SSQL, CommandType.Text);//

            if (objMax.ToString().Length == 0)
            {
                return 1;
            }
            return Convert.ToInt32(objMax.ToString()) + 1;

        }
        public long GetMaxLongID()
        {
            SSQL = "SELECT MAX(ID) from " + m_sNamaTabel;

            object objMax = _dbHelper.ExecuteScalar(SSQL, CommandType.Text);//

            if (objMax.ToString().Length == 0)
            {
                return 1;
            }
            return Convert.ToInt64(objMax.ToString()) + 1;

        }
        public string GetNoUrut(E_KOLOM_NOURUT iKolom, int _iTahun, int _IDDinas)
        {
           int idxKolom = (int)iKolom;
           string _sNamaKolom = GetColumnName(idxKolom);

           SSQL = "SELECT * from mAutoNumber WHERE iTahun =" + _iTahun.ToString() ;
           DataTable dt = new DataTable();

           dt = _dbHelper.ExecuteDataTable(SSQL, CommandType.Text);
           int xID;
           if (dt.Rows.Count > 0){               
               DataRow dr = dt.Rows[0];
              
               if (dr[idxKolom] == null)
               {
                   SSQL = "UPDATE mAutoNumber SET " + _sNamaKolom + "=1 WHERE iTahun =" + _iTahun.ToString();
                   _dbHelper.ExecuteNonQuery (SSQL, CommandType.Text);
                   xID =1;
               } else {
                   xID = DataFormat.GetInteger(dr[(int)iKolom]);
                   SSQL = "UPDATE mAutoNumber SET " + _sNamaKolom + "=" + _sNamaKolom + "+1 WHERE iTahun =" + _iTahun.ToString();
                   _dbHelper.ExecuteNonQuery(SSQL, CommandType.Text);
                   //'xID += 1;
               }
           }
           else
           {
               SSQL = "INSERT INTO mAutoNumber (iTahun," + _sNamaKolom + " ) values (" + _iTahun + ",2)";
              _dbHelper.ExecuteNonQuery(SSQL);
              xID =1;

           }
           string val = _iTahun.ToString().Substring(2, 2) + idxKolom.IntToStringWithLeftPad(2) + _IDDinas.ToString();
           string retVal = DataFormat.GetString((DataFormat.GetLong(val) * 1000000) +  xID);

           return retVal;
        }
        public string GetNoUrut(string sNamaKolom,int idxKolom, int _iTahun, int _IDDinas)
        {
            //int idxKolom = (int)iKolom;
            string _sNamaKolom = sNamaKolom;
            SSQL = "SELECT * from mAutoNumber WHERE iTahun =" + _iTahun.ToString();
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL, CommandType.Text);
            int xID;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                if (dr[sNamaKolom] == null)
                {
                    SSQL = "UPDATE mAutoNumber SET " + sNamaKolom + "= 2 WHERE iTahun =" + _iTahun.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL, CommandType.Text);
                    xID = 1;
                }
                else
                {
                    xID = DataFormat.GetInteger(dr[sNamaKolom]);
                    SSQL = "UPDATE mAutoNumber SET " + sNamaKolom + "=" + sNamaKolom + "+1 WHERE iTahun =" + _iTahun.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL, CommandType.Text);
                    //'xID += 1;
                }
            }
            else
            {
                SSQL = "INSERT INTO mAutoNumber (iTahun," + sNamaKolom + " ) values (" + _iTahun + ",2)";
                _dbHelper.ExecuteNonQuery(SSQL);
                xID = 1;

            }
            string val = _iTahun.ToString().Substring(2, 2) + idxKolom.IntToStringWithLeftPad(2) + _IDDinas.ToString();
            string retVal = DataFormat.GetString((DataFormat.GetLong(val) * 1000000) + xID);

            return retVal;
        }
        public string ToKodeRekening(long _pIDRekening)
        {
            string sKode = _pIDRekening.ToString();
            string sRet;
            sRet = sKode.Substring(0, 1);
            if (sKode.Substring(1, 1) != "0")
            {
                sRet = sRet + "." + sKode.Substring(1, 1);
                if (sKode.Substring(2, 1) != "0")
                {
                    sRet = sRet + "." + sKode.Substring(1, 1);
                    if (sKode.Substring(3, 2) != "00")
                    {
                        sRet = sRet + "." + sKode.Substring(3, 2);
                        if (sKode.Substring(5, 2) != "00")
                        {
                            sRet = sRet + "." + sKode.Substring(5, 2);
                        }
                    }
                }
            }
            return sRet;
        }
        private string GetColumnName(int idx)
        {
            string _namaKolom="";
            
            SSQL = "  SELECT TOP 1 COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'mAutoNumber' AND ORDINAL_POSITION = " + (idx+1).ToString();
            
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    _namaKolom = DataFormat.GetString(dr["COLUMN_NAME"]);

                    };
                }
            return _namaKolom;
         }


        public enum JENIS_JABATAN
        {
            ID_JENIS_KADA = 1,
            ID_JENIS_SEKDA = 2,
            ID_JENIS_KEPALAPPKD = 3,
            ID_JENIS_KABIDPERBEND = 4,
            ID_JENIS_BENDAHARAPPKD = 5,
            ID_JENIS_KEPALADINAS = 6,
            ID_JENIS_PPK = 7,
            ID_JENIS_BEMDAHARAPENGELUARAN = 8,
            ID_JENIS_BEMDAHARAPENERIMAAN = 9,
            ID_JENIS_BEMDAHARAPENGELUARANPEMBANTU = 10,
            ID_JENIS_KUAASAPENGGUNAANGGARANPENDAPATAN = 11,


            
        }
        protected string GetKolomPerencanaan()
        {
            string sRet;
            sRet = "TargetRp1";
            
            switch (Tahun)
            {
                case 2016:
                    sRet = "TargetRp1";
                    break;
                case 2017:
                    sRet = "TargetRp1";
                    break;
                case 2018:
                    sRet = "TargetRp2";
                    break;
                case 2019:
                    sRet = "TargetRp3";
                    break;
                case 2020:
                    sRet = "TargetRp4";
                    break;
                case 2021:
                    sRet = "TargetRp5";
                    break;


            }


            return sRet;
        }
        
    }
}
