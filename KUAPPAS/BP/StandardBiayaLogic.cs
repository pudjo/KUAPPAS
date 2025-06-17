//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DTO;
//using System.Data;
//using DataAccess;
//using Formatting;
//namespace BP
//{
//    public class StandardBiayaLogic:BP 
//    {
//        public StandardBiayaLogic(int _pTahun)
//            : base(_pTahun)
//        {
//            Tahun = _pTahun;
//            m_sNamaTabel = "mstandardharga";
//        }

//        public List<StandardBiaya> Get(string sLef)
//        {
//            List<StandardBiaya> mListUnit = new List<StandardBiaya>();
//            try
//            {
//                if (sLef.Length == 0)
//                {
//                    //SSQL = " SELECT A.*, B.Satuan  FROM " + m_sNamaTabel + " A Left outer join mStandardHarga_Satuan B ON A.iSatuan = B.idsatuanbarang  where iTahun= 2019 Order by btKodeBarang";
//                    //SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A Order by A.btKodeBarang";
                    
//                    SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
//                         " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
//                        " from mStandardHarga A where len(LTRIM(RTRIM(A.btKodebarang)))<4 order by A.btKodeBarang";
//                }
//                else
//                {
//                    int lenLeft = sLef.Length;
                    
//                    SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
//                         " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
//                        " from mStandardHarga A   WHERE A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "' Order by A.btKodeBarang";


//                   // SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A  WHERE A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "' Order by A.btKodeBarang";// Order by A.btKodeBarang";
//                    //SSQL = " SELECT A.*, B.Satuan  FROM " + m_sNamaTabel + " A Left outer join mStandardHarga_Satuan B ON A.iSatuan = B.idsatuanbarang  where iTahun= 2019 and btKodeBarang like '" + sLef + "%' and btKodeBarang <> '" + sLef + "' Order by btKodeBarang";
//                }

//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        mListUnit = (from DataRow dr in dt.Rows
//                                select new StandardBiaya()
//                                {
//                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
//                                    Nama = DataFormat.GetString(dr["sNamaBarang"]) ,
//                                    NamaSatuan=DataFormat.GetString(dr["NamaSatuan"]),
//                                    Harga=DataFormat.GetDecimal (dr["HargaET"]),
//                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
//                                    Level = DataFormat.GetSingle(dr["iLevel"]),
//                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
//                                    RootReport= DataFormat.GetInteger(dr["rootDisplay"])

        


//                                }).ToList();
//                    }
//                }
//                return mListUnit;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return mListUnit;
//            }
//        }
//        public List<StandardBiaya> Get(RemoteConnection rCon)
//        {
//            List<StandardBiaya> mListUnit = new List<StandardBiaya>();
//            try
//            {
//                SSQL = "select *  from mStandardHarga ";
//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL,rCon.GetConnection());
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        mListUnit = (from DataRow dr in dt.Rows
//                                select new StandardBiaya()
//                                {
//                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
//                                    Nama = DataFormat.GetString(dr["sNamaBarang"]) ,
//                                    NamaSatuan=DataFormat.GetString(dr["NamaSatuan"]),
//                                    Harga=DataFormat.GetDecimal (dr["HargaET"]),
//                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
//                                    Level = DataFormat.GetSingle(dr["iLevel"]),
//                                    RootReport= DataFormat.GetInteger(dr["rootDisplay"]),
//                                    Parent =DataFormat.GetString(dr["Parent"]),
//                                    Kelompok =DataFormat.GetInteger(dr["Kelompok"])

        


//                                }).ToList();
//                    }
//                }
//                return mListUnit;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return mListUnit;
//            }
//        }

//        public List<StandardBiaya> GetNextLevel(string sLef)
//        {
//            List<StandardBiaya> mListUnit = new List<StandardBiaya>();
//            try
//            {

//                int lenNext = sLef.Trim().Length + 2;
//                SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
//                         " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
//                        " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
//                        " ORDER BY A.btKodebarang";
                
//                //order by A.btKodeBarang";



//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        mListUnit = (from DataRow dr in dt.Rows
//                                select new StandardBiaya()
//                                {
//                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
//                                    Nama = DataFormat.GetString(dr["sNamaBarang"]) ,
//                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
//                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
//                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
//                                    Level = DataFormat.GetSingle(dr["iLevel"]),
//                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
//                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"])




//                                }).ToList();
//                    }
//                }
//                return mListUnit;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return mListUnit;
//            }
//        }

//        public int GetMaxOnLevel(string sLef)
//        {
//            List<StandardBiaya> mListUnit = new List<StandardBiaya>();
//            try
//            {
//                string realCode = sLef.Replace(" ","").Trim();
//                int lenNext = realCode.Length + 2;
//                SSQL = "select * from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + realCode + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
//                        " ORDER BY A.btKodebarang";

//                //order by A.btKodeBarang";
//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                string sLastCode = "";

//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        mListUnit = (from DataRow dr in dt.Rows
//                                select new StandardBiaya()
//                                {
//                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"])
                                
//                                }).ToList();
//                    }
//                }
//                for (int i = 0; i < mListUnit.Count; i++)
//                {
//                    sLastCode = mListUnit[i].IDBiaya.Replace(" ","");

//                }
//                sLastCode= sLastCode.Replace(" ", "");

//                    if (sLastCode.Trim().Length == lenNext)
//                    {
//                        return DataFormat.GetInteger(sLastCode.Trim().Substring(lenNext - 2, 2));

//                    }
//                return 0;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return 0;
//            }
//        }
   

//        public List<StandardBiaya> GetByName(string sLef)
//        {
//            List<StandardBiaya> mListUnit = new List<StandardBiaya>();
//            try
//            {
                
//              //  m_oListSBOnGrid = new List<StandardBiaya>();

//                SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
//                        " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
//                        " from mStandardHarga A where A.sNamaBarang like '%" + sLef.Trim() + "%'";
                

//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        mListUnit = (from DataRow dr in dt.Rows
//                                select new StandardBiaya()
//                                {
//                                    ID = DataFormat.GetInteger(dr["ID"]),
//                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
//                                    Nama = DataFormat.GetString(dr["sNamaBarang"]) ,
//                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
//                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
//                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
//                                    Level = DataFormat.GetSingle(dr["iLevel"]),
//                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
//                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"])




//                                }).ToList();
//                    }
//                }
//                return mListUnit;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return mListUnit;
//            }
//        }

//        public bool HialangkanDouble ()
//        {
//           try
//            {
//                List <StandardBiaya> lstDouble = new List<StandardBiaya>();

//                 SSQL = "select btKodeBarang, count(*) from mStandardHarga group by btKodeBarang having COUNT(*)>1";
//                 DataTable dt = new DataTable();
//                 dt = _dbHelper.ExecuteDataTable(SSQL);
//                 if (dt != null)
//                 {
                    
//                     if (dt.Rows.Count > 0)
//                    {
//                        lstDouble = (from DataRow dr in dt.Rows
//                                select new StandardBiaya()
//                                {
//                                     IDBiaya = DataFormat.GetString(dr["btKodeBarang"]).Replace (" ","")
//                                }).ToList();
                                                             
//                    }
//                 }
//                 foreach(StandardBiaya sb in lstDouble ){
//                        SSQL = "select * from mStandardHarga where btKodeBarang='" + sb.IDBiaya.ToString().Trim() + "' ORDER BY ID DESC";
//                        DataTable dtd = new DataTable();
//                        dtd = _dbHelper.ExecuteDataTable(SSQL);
//                        if (dtd != null)
//                        {
//                            if (dtd.Rows.Count > 0)
//                            { 
//                                DataRow dr= dtd.Rows[0];
//                                SSQL="DELETE from mStandardHarga where btKodeBarang = '" + sb.IDBiaya.ToString().Trim() + "' AND ID = " + DataFormat.GetInteger(dr["ID"]);
//                                _dbHelper.ExecuteNonQuery (SSQL);
//                            }
//                        }
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return false;
//            }
//        }
//        private string GetKode(string sKodeBarang)
//        {
//            if (sKodeBarang.Trim().Length == 0)
//            {
//                return "";
//            } 
//            switch (sKodeBarang.Trim().Length){
//                case 1:
//                    return sKodeBarang.Trim();
//                    break;
//                case 2:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1);
//                    break;
//                case 3:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1,2) + "." + sKodeBarang.Trim().Substring(3) ; 
//                    break;
//                case 5:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3,2) +
//                            sKodeBarang.Trim().Substring(5) ; 

//                    break;
//                case 7:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
//                            sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7) ; 
//                    break;
//                case 9:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
//                            sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7);
//                    break;

//                case 11:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
//                            sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7);
//                    break;
//                default:
//                    return sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
//                            sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7);
//                    break;                
//            }
//        }
//        public StandardBiaya  GetByID(string sLef)
//        {
//            StandardBiaya oSB = new StandardBiaya();
//            try
//            {
//                //SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A where A.btKodeBarang = '" + sLef.Trim() + "' ";

//                SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
//                        " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
//                       " from mStandardHarga A  where A.btKodeBarang = '" + sLef.Trim() + "' ";



//                DataTable dt = new DataTable();
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        DataRow dr = dt.Rows[0];
//                        oSB = new StandardBiaya()
//                                {
//                                    ID = DataFormat.GetInteger(dr["ID"]),
//                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
//                                    //Nama = DataFormat.GetString(dr["sNamaBarang"]) + " " ,//+ DataFormat.GetString(dr["sUraian"]),
//                                    Nama =  DataFormat.GetString(dr["sNamaBarang"]) ,
//                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
//                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
//                                    Satuan =0,// DataFormat.GetInteger(dr["iSatuan"]),
//                                    Level = DataFormat.GetSingle(dr["iLevel"]),
//                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
//                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"])

//                                };
//                    }
//                }
//                return oSB;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return oSB;
//            }
//        }
//        public bool Hapus(string _idRekening)
//        {

//            try
//            {
//                if (CekDiAnggaran(_idRekening) > 0)
//                {
//                    _isError = true;
//                    _lastError = "Kode Sudah dipakai di penganggaran. Tidak bisa dihapus.";
//                    return false;

//                }

//                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE btKodeBarang=" + _idRekening.ToString().Replace(" ","").Trim();
//                _dbHelper.ExecuteNonQuery(SSQL);
//                return true;


//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return false;
//            }

//        }

//        private int CekDiAnggaran(string cKode)
//        {
//            StandardBiaya oSB = new StandardBiaya();
//            try
//            {
//                //SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A where A.btKodeBarang = '" + sLef.Trim() + "' ";

//                SSQL = "select count(*) as J from tAnggaranUraian_A A where  A.IDStandardHarga = '" + cKode.Trim().Replace(" ","") + "' ";
                


//                DataTable dt = new DataTable();
//                int c = 0;
//                dt = _dbHelper.ExecuteDataTable(SSQL);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        DataRow dr = dt.Rows[0];
//                        c = DataFormat.GetInteger(dr["J"]);

//                    }
//                }
//                return c;
//            }
//            catch (Exception ex)
//            {
//                _isError = true;
//                _lastError = ex.Message;
//                return 0;
//            }

//        }
//        public bool Update(StandardBiaya sb)
//        {
//            try
//            {
//                if (sb.ID == 0)
//                {
//                    int maxID = GetMaxIDNoYear();
//                    string sPaent = "";
//                    if (sb.IDBiaya.Length > 2)
//                    {
//                        sPaent = sb.IDBiaya.Substring(0, sb.IDBiaya.Length - 2);

//                    } else
//                        sPaent = sb.IDBiaya.Substring(0,1);


//                    SSQL = "INSERT INTO mStandardHarga (ID,snamaBarang, btKodeBarang,HargaET ,NamaSatuan,rootDisplay,Kelompok,Parent, iLevel) values " + //(= where btKodeBarang =@pKode";
//                                " ( @pID,@psnamaBarang, @pbtKodeBarang,@pHargaET ,@pNamaSatuan,@prootDisplay,@pKelompok,@pParent, @piLevel)";

//                    DBParameterCollection paramCollection = new DBParameterCollection();
//                    paramCollection.Add(new DBParameter("@pID",maxID+1));
//                    paramCollection.Add(new DBParameter("@psnamaBarang", sb.Nama)); 
//                    paramCollection.Add(new DBParameter("@pbtKodeBarang", sb.IDBiaya));
//                    paramCollection.Add(new DBParameter("@pHargaET", sb.Harga ));
//                    paramCollection.Add(new DBParameter("@pNamaSatuan", sb.NamaSatuan ));
//                    paramCollection.Add(new DBParameter("@prootDisplay", 0));
//                    paramCollection.Add(new DBParameter("@pKelompok", DataFormat.GetInteger(sb.IDBiaya.Substring(0,1))));
//                    paramCollection.Add(new DBParameter("@pParent", sPaent));
//                    paramCollection.Add(new DBParameter("@piLevel", sb.Level)); 



//                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

//                }
//                else
//                {
//                    SSQL = "update mStandardHarga SET snamaBarang= @pNama, HargaET =@pHarga, NamaSatuan=@pNamaSatuan where btKodeBarang =@pKode";
//                    DBParameterCollection paramCollection = new DBParameterCollection();
//                    paramCollection.Add(new DBParameter("@pNama", sb.Nama));
//                    paramCollection.Add(new DBParameter("@pHarga", sb.Harga));
//                    paramCollection.Add(new DBParameter("@pNamaSatuan", sb.NamaSatuan));

//                    paramCollection.Add(new DBParameter("@pKode", sb.IDBiaya.Trim()));

//                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
//                }

//                return true;

//            }
//            catch (Exception ex)
//            {
//                _lastError = ex.Message;

//                return false;

//            }
//        }
//        public bool SimpanDariPerencanaan(List<StandardBiaya> _lsb)
//        {
//            try
//            {
//                SSQL = "DELETE from mStandardHarga";

//                _dbHelper.ExecuteNonQuery(SSQL);
//                foreach (StandardBiaya sb in _lsb)
//                {
                  
//                        SSQL = "INSERT INTO mStandardHarga (ID,snamaBarang, btKodeBarang,HargaET ,NamaSatuan,rootDisplay,Kelompok,Parent, iLevel) values " + //(= where btKodeBarang =@pKode";
//                                     " ( @pID,@psnamaBarang, @pbtKodeBarang,@pHargaET ,@pNamaSatuan,@prootDisplay,@pKelompok,@pParent, @piLevel)";

//                        DBParameterCollection paramCollection = new DBParameterCollection();
//                        paramCollection.Add(new DBParameter("@pID", sb.ID));
//                        paramCollection.Add(new DBParameter("@psnamaBarang", sb.Nama));
//                        paramCollection.Add(new DBParameter("@pbtKodeBarang", sb.IDBiaya));
//                        paramCollection.Add(new DBParameter("@pHargaET", sb.Harga));
//                        paramCollection.Add(new DBParameter("@pNamaSatuan", sb.NamaSatuan));
//                        paramCollection.Add(new DBParameter("@prootDisplay", 0));
//                        paramCollection.Add(new DBParameter("@pKelompok", sb.Kelompok));
//                        paramCollection.Add(new DBParameter("@pParent", sb.Parent));
//                        paramCollection.Add(new DBParameter("@piLevel", sb.Level));



//                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                    
//                }

//                return true;

//            }
//            catch (Exception ex)
//            {
//                _lastError = ex.Message;

//                return false;

//            }
//        }
        
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using DataAccess;
using Formatting;
namespace BP
{
    public class StandardBiayaLogic : BP
    {
        public StandardBiayaLogic(int _pTahun, int profile=2)
            : base(_pTahun, 0,profile)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mstandardharga";
        }

        public List<StandardBiaya> Get(string sLef)
        {
            List<StandardBiaya> _lst = new List<StandardBiaya>();
            try
            {
               //403. 20 006

                if (sLef.Length == 0)
                {
                    //SSQL = " SELECT A.*, B.Satuan  FROM " + m_sNamaTabel + " A Left outer join mStandardHarga_Satuan B ON A.iSatuan = B.idsatuanbarang  where iTahun= 2019 Order by btKodeBarang";
                    //SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A Order by A.btKodeBarang";
                    if (Tahun >= 2020)
                    {
                        SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                                " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                                " from mStandardHarga A where iLevel<3 AND len(LTRIM(RTRIM(A.btKodebarang)))<4 ";

                        SSQL =SSQL+ "UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                                " (LTRIM(RTRIM(A.btKodebarang)),1,1 + LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                                " from mStandardHarga A where iLevel=3 AND  len(LTRIM(RTRIM(A.btKodebarang)))<4 ";

                        SSQL = SSQL + "UNION  select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                                " (LTRIM(RTRIM(A.btKodebarang)),1,2 + LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                                " from mStandardHarga A where iLevel>3 AND  len(LTRIM(RTRIM(A.btKodebarang)))<4 ";
                        SSQL = SSQL + " order by A.btKodeBarang";
                    }
                    else
                    {

                        SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                                " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                                " from mStandardHarga A where len(LTRIM(RTRIM(A.btKodebarang)))<4 order by A.btKodeBarang";


                    }





                }
                else
                {
                    int lenLeft = sLef.Length;

                    if (Tahun >= 2020)
                    {

                        SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                             " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                            " from mStandardHarga A   WHERE iLevel < 3 and A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "'";


                        SSQL = SSQL+ " UNION ALL select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                             " (LTRIM(RTRIM(A.btKodebarang)),1,1 + LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                            " from mStandardHarga A   WHERE iLevel = 3 A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "'";

                        SSQL = SSQL+ " UNION ALL select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                             " (LTRIM(RTRIM(A.btKodebarang)),1,2 + LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                            " from mStandardHarga A   WHERE iLevel > 3 A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "'";
                        
                        
                        SSQL =SSQL + " Order by A.btKodeBarang";

                    } else
                    {
                        SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                         " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                        " from mStandardHarga A   WHERE A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "' Order by A.btKodeBarang";


                    }


                    // SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A  WHERE A.btKodeBarang like '" + sLef.Trim() + "%' and A.btKodeBarang <> '" + sLef.Trim() + "' Order by A.btKodeBarang";// Order by A.btKodeBarang";
                    //SSQL = " SELECT A.*, B.Satuan  FROM " + m_sNamaTabel + " A Left outer join mStandardHarga_Satuan B ON A.iSatuan = B.idsatuanbarang  where iTahun= 2019 and btKodeBarang like '" + sLef + "%' and btKodeBarang <> '" + sLef + "' Order by btKodeBarang";
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new StandardBiaya()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
                                    Nama =  DataFormat.GetString(dr["sNamaBarang"]),
                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
                                    Level = DataFormat.GetSingle(dr["iLevel"]),
                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"]),
                                    DisplayedText = GetKode(DataFormat.GetString(dr["btKodebarang"])) + " " + DataFormat.GetString(dr["sNamaBarang"]) + " ( " + DataFormat.GetDecimal(dr["HargaET"]).ToRupiahInReport() + ")",
                                    PPN =  DataFormat.GetInteger(dr["bPPN"]),




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

        public List<StandardBiaya> Get22(List<string> lstIDrekening )
        {
            List<StandardBiaya> _lst = new List<StandardBiaya>();
            try
            {
                //403. 20 006

                string sIDRekening="(";
                foreach(string r in lstIDrekening){
                    sIDRekening= sIDRekening + "'" +  r + "'" + ",";

                }
                sIDRekening = sIDRekening  + "'99')";

                SSQL = "SELECT * from mStandardHarga ";//where (iidrekening in " + sIDRekening +") or iidrekening= '0'";
                SSQL = SSQL + " ORDER BY Kelompok,btKodebarang ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new StandardBiaya()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
                                    Nama = DataFormat.GetString(dr["sNamaBarang"]),
                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
                                    Level = DataFormat.GetSingle(dr["iLevel"]),
                                    Uraian = DataFormat.GetString(dr["sNamaBarang"])+ " - " + DataFormat.GetString(dr["Spec"]),
                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"]),
                                    DisplayedText = GetKode(DataFormat.GetString(dr["btKodebarang"])) + " " + DataFormat.GetString(dr["sNamaBarang"]) + " ( " + DataFormat.GetDecimal(dr["HargaET"]).ToRupiahInReport() + ")",
                                    PPN = DataFormat.GetInteger(dr["bPPN"]),
                                    Jenis = DataFormat.GetString(dr["Jenis"]),
                                    Kelompok = DataFormat.GetInteger(dr["Kelompok"]),



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
        public List<StandardBiaya> GetNextLevel(string sLef, int iLevel)
        {
            List<StandardBiaya> _lst = new List<StandardBiaya>();
            try
            {
               int panjang = 2;

               if ((iLevel == 2 || iLevel == 3) && Tahun >= 2020){
                        panjang = 3;
                }
                //1--> 2 
                //2 -- 2 *2
                //3 -- 2 * 3
                // pengurang 

              int lenNext = sLef.Trim().Length + panjang;
                
              if (Tahun >= 2020)
                {
                   
                    if (iLevel<2){
                
                        SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                             " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                            " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                            " ORDER BY A.btKodebarang";
              

                   
//                        SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
  //                           " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- ((A.rootDisplay * 2)))) as displlayedText  " +
   //                         " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
    //                        " and A.rootDisplay = 0 ";

              } else{     
                  SSQL = "select *, sNamaBarang as displlayedText  " +
                            " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                            " and A.rootDisplay = 0 ";
                  
                 

                   if (iLevel == 2)
                   {
                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang)))- 3)) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel =3 AND A.rootDisplay=1  ";

                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang)))- 5)) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel =3 AND A.rootDisplay=2  ";
                        SSQL = SSQL + " ORDER BY A.btKodebarang";

                   }
                   if (iLevel == 3)
                   {
                  
                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang)))- 3)) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel =4 AND A.rootDisplay=1  ";

                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang)))- 6)) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel =4 AND A.rootDisplay=2  ";
                        SSQL = SSQL + " ORDER BY A.btKodebarang";
                   }
                   if (iLevel == 4)
                   {
                  
                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang)))- 2)) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel =5 AND A.rootDisplay=1  ";

                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang)))- 5)) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel =5 AND A.rootDisplay=2  ";

                        SSQL = SSQL + " ORDER BY A.btKodebarang";
                   }
                   if (iLevel > 5)
                   {
                  
                        SSQL = SSQL + " UNION select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                          " (LTRIM(RTRIM(A.btKodebarang)),1, LEN(LTRIM(RTRIM(A.btKodebarang))) - (rootDisplay * 2))) as displlayedText  " +
                          " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                          " AND A.iLevel >5  ";
                        SSQL = SSQL + " ORDER BY A.btKodebarang";
                   
                   }


                } 
              }else 
                {
                  SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                         " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                        " from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + sLef + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                        " ORDER BY A.btKodebarang";

              }
            
                    //order by A.btKodeBarang";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new StandardBiaya()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
                                    Nama = DataFormat.GetString(dr["sNamaBarang"]) ,
                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
                                    Level = DataFormat.GetSingle(dr["iLevel"]),
                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"]),
                                    DisplayedText = GetKode(DataFormat.GetString(dr["btKodebarang"]))  + DataFormat.GetInteger(dr["rootDisplay"]).ToString() + "->" + DataFormat.GetString(dr["sNamaBarang"]) + " ( " + DataFormat.GetDecimal(dr["HargaET"]).ToRupiahInReport() + ")",
                                    PPN = DataFormat.GetInteger(dr["bPPN"]),



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


        public List<StandardBiaya> GetByName(string sLef)
        {
            List<StandardBiaya> _lst = new List<StandardBiaya>();
            
            try
            {
                if (Tahun < 2020)
                {


                    SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                                   " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                                   " from mStandardHarga A where A.RootDisplay > 0 AND iLevel<3 AND len(LTRIM(RTRIM(A.btKodebarang)))<4  AND  A.sNamaBarang like '%" + sLef.Trim() + "%'";

                    SSQL = SSQL + "UNION select *, (select distinct sNamaBarang from mStandardHarga  B where btKodeBarang = substring " +
                            " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (3 + ((A.rootDisplay -1)* 2)))) as displlayedText  " +
                            " from mStandardHarga A where iLevel >= 3 AND  A.RootDisplay > 0 AND  A.sNamaBarang like '%" + sLef.Trim() + "%'";


                    SSQL = SSQL + "UNION select *, sNamaBarang  as displlayedText  " +
                            " from mStandardHarga A where  A.RootDisplay = 0 AND  A.sNamaBarang like '%" + sLef.Trim() + "%'";

                    SSQL = SSQL + " order by A.btKodeBarang";
                }
                else
                {
                    SSQL = "select *, sNamaBarang as displlayedText, 1 as rootDisplay from mStandardHarga A where A.sNamaBarang like '%" + sLef.Trim() + "%'";


                    SSQL = SSQL + " order by A.btKodeBarang";


                } 


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new StandardBiaya()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
                                    Nama = DataFormat.GetString(dr["sNamaBarang"]),
                                    NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
                                    Harga = DataFormat.GetDecimal(dr["HargaET"]),
                                    Satuan = 0, //DataFormat.GetInteger(dr["iSatuan"]),
                                    Level = DataFormat.GetSingle(dr["iLevel"]),
                                    Uraian = DataFormat.GetString(dr["displlayedText"]),
                                    RootReport = DataFormat.GetInteger(dr["rootDisplay"]),
                                    DisplayedText= GetKode(DataFormat.GetString(dr["btKodebarang"])) + " " + DataFormat.GetString(dr["sNamaBarang"]) + " ( " + DataFormat.GetDecimal(dr["HargaET"]).ToRupiahInReport() + ")",
                                    PPN = DataFormat.GetInteger(dr["bPPN"]),



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

        public bool HialangkanDouble()
        {
            try
            {
                List<StandardBiaya> lstDouble = new List<StandardBiaya>();

                SSQL = "select btKodeBarang, count(*) from mStandardHarga group by btKodeBarang having COUNT(*)>1";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        lstDouble = (from DataRow dr in dt.Rows
                                     select new StandardBiaya()
                                     {
                                         IDBiaya = DataFormat.GetString(dr["btKodeBarang"])
                                     }).ToList();

                    }
                }
                foreach (StandardBiaya sb in lstDouble)
                {
                    SSQL = "select * from mStandardHarga where btKodeBarang='" + sb.IDBiaya.ToString().Trim() + "' ORDER BY ID DESC";
                    DataTable dtd = new DataTable();
                    dtd = _dbHelper.ExecuteDataTable(SSQL);
                    if (dtd != null)
                    {
                        if (dtd.Rows.Count > 0)
                        {
                            DataRow dr = dtd.Rows[0];
                            SSQL = "DELETE from mStandardHarga where btKodeBarang = '" + sb.IDBiaya.ToString().Trim() + "' AND ID = " + DataFormat.GetInteger(dr["ID"]);
                            _dbHelper.ExecuteNonQuery(SSQL);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }
        private string GetKode(string sKodeBarang)
        {
            if (sKodeBarang.Trim().Length == 0)
            {
                return "";
            }
            string sret;
            if (Tahun < 2020) {
                switch (sKodeBarang.Trim().Length)
                {
                    case 1:
                        sret=sKodeBarang.Trim();
                        break;

                    case 3:
                        sret= sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2);//+ "." + sKodeBarang.Trim().Substring(3);
                        break;
                    case 5:
                        sret= sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2);// +
          
                        break;
                    case 7:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                 "." + sKodeBarang.Trim().Substring(5);//, 2) + sKodeBarang.Trim().Substring(7);
                        break;

                    case 9:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                "." + sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7);
                        break;

                    case 11:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                "." + sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7, 2) + "." + sKodeBarang.Trim().Substring(9);
                        break;

                    case 13:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7, 2) + "." + sKodeBarang.Trim().Substring(9, 2) + "." + sKodeBarang.Trim().Substring(11);
                        break;

                    default:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                sKodeBarang.Trim().Substring(5, 2) + sKodeBarang.Trim().Substring(7, 2) + "." + sKodeBarang.Trim().Substring(9, 2) + "." + sKodeBarang.Trim().Substring(11, 2) + "." + sKodeBarang.Trim().Substring(13);
                        
                        break;
                }
                } else {
                switch (sKodeBarang.Trim().Length)
                {
                    case 1:
                        sret = sKodeBarang.Trim();
                        break;

                    case 3:
                        sret= sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2);//+ "." + sKodeBarang.Trim().Substring(3);
                        break;
                    case 5:
                        sret= sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2);// +
                        //sKodeBarang.Trim().Substring(5);

                        break;
                    case 8:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                 "." + sKodeBarang.Trim().Substring(5);//, 2) + sKodeBarang.Trim().Substring(7);
                        break;

                 
                    case 11:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                "." + sKodeBarang.Trim().Substring(5, 3) + sKodeBarang.Trim().Substring(8);
                        break;

                    case 13:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                sKodeBarang.Trim().Substring(5, 3) + sKodeBarang.Trim().Substring(8, 3) + "." + sKodeBarang.Trim().Substring(11);
                        break;

                    default:
                        sret = sKodeBarang.Trim().Substring(0, 1) + "." + sKodeBarang.Trim().Substring(1, 2) + "." + sKodeBarang.Trim().Substring(3, 2) +
                                sKodeBarang.Trim().Substring(5, 3) + sKodeBarang.Trim().Substring(8, 3) + "." + sKodeBarang.Trim().Substring(11, 2) +
                                "." + sKodeBarang.Trim().Substring(13);
                        break;
                }
                
            }
            return sret;

        }
        public StandardBiaya GetByID(string sLef)
        {
            StandardBiaya oSB = new StandardBiaya();
            try
            {
                //SSQL = " SELECT A.* FROM " + m_sNamaTabel + " A where A.btKodeBarang = '" + sLef.Trim() + "' ";

                SSQL = "select *, (select distinct sNamaBarang from mStandardHarga  B where LTRIM(RTRIM(btKodeBarang)) = substring " +
                        " (LTRIM(RTRIM(A.btKodebarang)),1,LEN(LTRIM(RTRIM(A.btKodebarang)))- (A.rootDisplay * 2))) as displlayedText  " +
                       " from mStandardHarga A  where A.btKodeBarang = '" + sLef.Trim() + "' ";



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        oSB = new StandardBiaya()
                        {
                            ID = DataFormat.GetInteger(dr["ID"]),
                            IDBiaya = DataFormat.GetString(dr["btKodeBarang"]),
                            //Nama = DataFormat.GetString(dr["sNamaBarang"]) + " " ,//+ DataFormat.GetString(dr["sUraian"]),
                            Nama = DataFormat.GetString(dr["sNamaBarang"]) ,
                            NamaSatuan = DataFormat.GetString(dr["NamaSatuan"]),
                            Harga = DataFormat.GetDecimal(dr["HargaET"]),
                            Satuan = 0,// DataFormat.GetInteger(dr["iSatuan"]),
                            Level = DataFormat.GetSingle(dr["iLevel"]),
                            Uraian = DataFormat.GetString(dr["displlayedText"]),
                            RootReport = DataFormat.GetInteger(dr["rootDisplay"]),
                            DisplayedText= GetKode(DataFormat.GetString(dr["btKodebarang"])) + " " + DataFormat.GetString(dr["sNamaBarang"]) + " ( " + DataFormat.GetDecimal(dr["HargaET"]).ToRupiahInReport() + ")",
                            PPN = DataFormat.GetInteger(dr["bPPN"]),
                        };
                    }
                }
                return oSB;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oSB;
            }
        }
        public bool Hapus(string _idRekening)
        {

            try
            {
                SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE  btKodeBarang='" + _idRekening.ToString().Trim()+"'";

                _dbHelper.ExecuteNonQuery(SSQL);
                return true;


            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        public StandardBiaya Update(StandardBiaya sb)
        {
            try
            {
                if (sb.ID == 0)
                {
                    int maxID = GetMaxIDNoYear();
                    string sPaent = "";
                    if (sb.IDBiaya.Length > 2)
                    {
                        sPaent = sb.IDBiaya.Substring(0, sb.IDBiaya.Length - 2);

                    }
                    else
                        sPaent = sb.IDBiaya.Substring(0, 1);

                    sb.ID = maxID + 1;

                    SSQL = "INSERT INTO mStandardHarga (ID,snamaBarang, btKodeBarang,HargaET ,NamaSatuan,rootDisplay,Kelompok,Parent, iLevel) values " + //(= where btKodeBarang =@pKode";
                                " ( @pID,@psnamaBarang, @pbtKodeBarang,@pHargaET ,@pNamaSatuan,@prootDisplay,@pKelompok,@pParent, @piLevel)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pID", maxID + 1));
                    paramCollection.Add(new DBParameter("@psnamaBarang", sb.Nama));
                    paramCollection.Add(new DBParameter("@pbtKodeBarang", sb.IDBiaya));
                    paramCollection.Add(new DBParameter("@pHargaET", sb.Harga));
                    paramCollection.Add(new DBParameter("@pNamaSatuan", sb.NamaSatuan));
                    paramCollection.Add(new DBParameter("@prootDisplay", sb.RootReport));
                    paramCollection.Add(new DBParameter("@pKelompok", DataFormat.GetInteger(sb.IDBiaya.Substring(0, 1))));
                    paramCollection.Add(new DBParameter("@pParent", sPaent));
                    paramCollection.Add(new DBParameter("@piLevel", sb.Level));



                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                else
                {
                    SSQL = "update mStandardHarga SET snamaBarang= @pNama, rootDisplay=@prootDisplay,HargaET =@pHarga, NamaSatuan=@pNamaSatuan where btKodeBarang =@pKode";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pNama", sb.Nama));
                    paramCollection.Add(new DBParameter("@prootDisplay", sb.RootReport));
                    paramCollection.Add(new DBParameter("@pHarga", sb.Harga));

                    paramCollection.Add(new DBParameter("@pNamaSatuan", sb.NamaSatuan));

                    paramCollection.Add(new DBParameter("@pKode", sb.IDBiaya.Trim()));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                }

                return sb;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;

                return null;

            }
        }

        public int GetMaxOnLevel(string sLef , int pLevel)
        {
            List<StandardBiaya> _lst = new List<StandardBiaya>();
            try
            {
                string realCode = sLef.Replace(" ", "").Trim();
                int lenNext;

                if (pLevel ==2 || pLevel==3)
                    lenNext = realCode.Length + 3;
                else
                    lenNext = realCode.Length + 2;

                SSQL = "select * from mStandardHarga A where LTRIM(RTRIM(A.btKodebarang)) like '" + realCode + "%' and LEN(LTRIM(RTRIM(A.btKodebarang))) = " + lenNext.ToString() +
                        " ORDER BY A.btKodebarang";

                //order by A.btKodeBarang";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                string sLastCode = "";

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new StandardBiaya()
                                {
                                    IDBiaya = DataFormat.GetString(dr["btKodeBarang"])

                                }).ToList();
                    }
                }
                for (int i = 0; i < _lst.Count; i++)
                {
                    sLastCode = _lst[i].IDBiaya.Replace(" ", "");

                }
                sLastCode = sLastCode.Replace(" ", "");

                if (sLastCode.Trim().Length == lenNext)
                {
                    if (pLevel == 2 || pLevel == 3)
                        return DataFormat.GetInteger(sLastCode.Trim().Substring(lenNext - 3));

                    else
                    return DataFormat.GetInteger(sLastCode.Trim().Substring(lenNext - 2, 2));

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
    }
}

