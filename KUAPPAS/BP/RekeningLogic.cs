using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using DataAccess;
using Formatting;
using System.Data;

namespace BP
{
    public class RekeningLogic:BP
    {
        public enum E_REKENING_TYPE : short
        {
            REKENING_13,
            REKENING_64
        };
        private E_REKENING_TYPE m_JenisRekening;
        //public RekeningLogic(int _pTahun)
        //{
        //    Tahun = _pTahun;
        //}
        private int mprofile;
        public RekeningLogic(int _pTahun, E_REKENING_TYPE _JenisRekening = E_REKENING_TYPE.REKENING_13,int profile=1)
            : base(_pTahun,0,profile)
        {
            Tahun = _pTahun;
            m_JenisRekening = _JenisRekening;
            mprofile = profile;
            if (m_JenisRekening == E_REKENING_TYPE.REKENING_13)
            {
                m_sNamaTabel = "mRekening";
            }
            else
            {
                m_sNamaTabel = "mRekeningDJPK";

            }
        }

        public List<Rekening> GetInAnggaran(int dinas)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                if (dinas == 0)
                {
                    SSQL = "SELECT * FROM dbo.fGetRekeningInANggaran() where IIDRekening > 100000000 ORDER BY IIDRekening";
                } else
                {
                    SSQL = "SELECT * FROM dbo.fGetRekeningInANggaranDinas (" + dinas.ToString() +")where IIDRekening > 100000000 ORDER BY IIDRekening";
                }
                    DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {

                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    //         Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))

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

        public List<Rekening> GetNeraca(bool eliminasiRK=false)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
              
                SSQL = "SELECT * FROM " + m_sNamaTabel + " where IIDRekening > 100000000 ORDER BY IIDRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {

                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    //         Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))

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
        public List<Rekening> Get()
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " where IIDRekening > 100000000 ORDER BY IIDRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        
                         _lst = (from DataRow dr in dt.Rows
                                select new Rekening()                        
                                {

                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                           //         Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))
                               
                                 }).ToList();                    
                    }                    
                }
                return _lst;
            }catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<Rekening> GetOnLevel(int Level)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " where IIDRekening like '5%' and btroot<=" + Level .ToString() + " order by iidrekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {

                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
                                    //         Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))

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
        public List<Rekening> GetOnRoot(Single root)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE btRoot=@ROOR ORDER BY IIDRekening";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@ROOR", root));
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {
                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))
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
        public List<Rekening> GetChildOf(long  parent)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM mRekening where IIDRekening > 100000000 and IIDPArent=@PARENT ORDER BY IIDRekening";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@PARENT", parent));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {
                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
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
        public List<Rekening> GetLike(string parent)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM mRekening where IIDRekening > 100000000 "+
                " and cast(IIDRekening  as char(12)) like '"+ parent.Trim() +"%' ORDER BY IIDRekening";

       

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {
                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLEaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Debet = DataFormat.GetInteger(dr["iDebet"]),
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
        public List<Rekening> GetByName(string sNama, Single root=0)
        {

            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE sNamaRekening like '%" + sNama.Trim()  + "%' AND btRoot=@ROOT ORDER BY IIDRekening";
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@ROOR", root));
                
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {
                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDParent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLeaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))
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
        public void ProsesImport(Rekening oRek)
        {
            //
            if (GetByID(oRek.ID) == null)
            {
                string sIDParent = oRek.ID.ToString().Substring(0, 5) + "00";
                oRek.IDParent = DataFormat.GetLong(sIDParent);

                SSQL = "INSERT INTO " + m_sNamaTabel + " (IIDRekening,IIDParent, btRoot,bLeaf,sNamaRekening, iDebet,sNama2) values (" +
                        "@pIIDRekening,@pIIDParent,5,1,@psNamaRekening, -1,@psNama2)";           

                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pIIDRekening", oRek.ID));
                        paramCollection.Add(new DBParameter("@pIIDParent", oRek.IDParent));                        
                        paramCollection.Add(new DBParameter("@psNamaRekening", oRek.Nama));
                        paramCollection.Add(new DBParameter("@psNama2", oRek.Nama));                       

                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
            } else {

                SSQL = "UPDATE " + m_sNamaTabel + " SET btRoot=5,bLeaf=1,sNama2=@psNamaRekening, iDebet=-1 WHERE IIDRekening= @pIIDRekening";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@psNamaRekening", oRek.Nama));
                paramCollection.Add(new DBParameter("@pIIDRekening", oRek.ID));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                
            }

        }
        public Rekening GetByID(long pID)
        {
     
            Rekening _rek = new Rekening();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE IIDRekening=@IDREKENING";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@IDREKENING", pID));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr =dt.Rows[0];
                        _rek = new Rekening()
                                {
                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLeaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))
                                };
                    }
                }
                return _rek;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public bool PerbaikiRoot()
        {
            try
            {

                for (int root = 1; root < 6; root++)
                {
                    List<Rekening> lstRekeningOnThisRoot = new List<Rekening>();
                    lstRekeningOnThisRoot = GetOnRoot(root);
                    if (lstRekeningOnThisRoot != null)
                    {
                        foreach (Rekening rek in lstRekeningOnThisRoot)
                        {
                            SSQL = "UPDATE mRekening set btRoot = @ROOT + 1 where iiDParent = @IDRekening";
                            DBParameterCollection paramCollection = new DBParameterCollection();
                            paramCollection.Add(new DBParameter("@ROOT", root));
                            paramCollection.Add(new DBParameter("@IDRekening", rek.ID));
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
         }
        private List<Rekening> GetOnRoot(int root)
        {
            List<Rekening> _lst = new List<Rekening>();
            try
            {
                SSQL = "SELECT * FROM mRekening where btRoot =@Root ORDER BY IIDRekening";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@Root", root));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        _lst = (from DataRow dr in dt.Rows
                                select new Rekening()
                                {
                                    ID = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDParent = DataFormat.GetLong(dr["IIDParent"]),
                                    Nama = DataFormat.GetString(dr["sNamaRekening"]),
                                    Leaf = DataFormat.GetSingle(dr["bLeaf"]),
                                    Root = DataFormat.GetSingle(dr["btRoot"]),
                                    Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))
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
        public Rekening GetParentByID(long pID)
        {

            Rekening _rek = new Rekening();
            try
            {

                
                SSQL = " Select * From " + m_sNamaTabel + "  where IIDRekening in (select IIDParent " +
                        " from " + m_sNamaTabel + " where IIDRekening= @IDREKENING)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@IDREKENING", pID));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        _rek = new Rekening()
                        {
                            ID = DataFormat.GetLong(dr["IIDRekening"]),
                            IDParent = DataFormat.GetLong(dr["IIDPArent"]),
                            Nama = DataFormat.GetString(dr["sNamaRekening"]),
                            Leaf = DataFormat.GetSingle(dr["bLeaf"]),
                            Root = DataFormat.GetSingle(dr["btRoot"]),
                            Tampilan = GetTampilan(DataFormat.GetString(dr["IIDRekening"]), DataFormat.GetSingle(dr["btRoot"]))
                        };
                    }
                }
                return _rek;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        private string GetTampilan(string _pID, Single _root)
        {
            try
            {
                
                string sRekening = _pID;
                string sRet = "";

                SetProfileRekening(mprofile);
                Rekening oRekening = new Rekening(_pID, (int)_root, m_ProfileRekening);

                switch ((int)_root)
                {
                    case 1:
                        sRet = oRekening.KodeLevel1;
                        break;
                    case 2:
                        sRet = oRekening.KodeLevel1 + "." + oRekening.KodeLevel2;
                        break;
                    case 3:
                        sRet = oRekening.KodeLevel1 + "." + oRekening.KodeLevel2 + "." + oRekening.KodeLevel3;
                        break;
                    case 4:
                        sRet = oRekening.KodeLevel1 + "." + oRekening.KodeLevel2 + "." + oRekening.KodeLevel3 + "." + oRekening.KodeLevel4;
                        break;
                    case 5:
                        sRet = oRekening.KodeLevel1 + "." + oRekening.KodeLevel2 + "." + oRekening.KodeLevel3 + "." + oRekening.KodeLevel4 + "." + oRekening.KodeLevel5;
                        break;
                    case 6:
                        sRet = oRekening.KodeLevel1 + "." + oRekening.KodeLevel2 + "." + oRekening.KodeLevel3 + "." + oRekening.KodeLevel4 + "." + oRekening.KodeLevel5 + "." + oRekening.KodeLevel6;
                        break;
                }
                return sRet;
            }
            catch (Exception ex)
            {
                return "";
            }

            

        }

        public bool Simpan(ref Rekening _pRekening)
        {
            try
            {
               // if (_pRekening.Baru== true)
                //{

                if (GetByID(_pRekening.ID).ID > 0)
                {

                    _isError = true;
                    //_lastError = "Kode Rekening sudah dipakai. Kode Rekening tidak boleh Sama..";
                    
                    SSQL = "UPDATE " + m_sNamaTabel + " SET sNamaRekening ='" + _pRekening.Nama  + "' where IIDRekening= " + _pRekening.ID.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                    

                    return true;
                    

                }
                else
                {
                    SSQL = "INSERT INTO " + m_sNamaTabel + " (IIDRekening,IIDParent, btRoot,bLeaf,sNamaRekening, iDebet) values (" +
                        "@pIIDRekening,@pIIDParent, @pbtRoot,@pbLeaf,@psNamaRekening, @piDebet)";

                    

                    
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pIIDRekening", _pRekening.ID));
                    paramCollection.Add(new DBParameter("@pIIDParent", _pRekening.IDParent));
                    paramCollection.Add(new DBParameter("@pbtRoot", _pRekening.Root));
                    paramCollection.Add(new DBParameter("@pbLeaf", _pRekening.Leaf));
                    paramCollection.Add(new DBParameter("@psNamaRekening", _pRekening.Nama));
                    paramCollection.Add(new DBParameter("@piDebet", _pRekening.Debet));

                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Simpan2024(Rekening _pRekening)
        {
            try
            {
               
                

                 _isError = true;

                 SSQL = "INSERT INTO mRekening (IIDRekening,IIDParent, btRoot,bLeaf,sNamaRekening, iDebet,Kode,NoBaris) values (" +
                        "@pIIDRekening,@pIIDParent, @pbtRoot,@pbLeaf,@psNamaRekening, @piDebet,@Kode,@NoBaris)";




                 DBParameterCollection paramCollection = new DBParameterCollection();
                 paramCollection.Add(new DBParameter("@pIIDRekening", _pRekening.ID));
                 paramCollection.Add(new DBParameter("@pIIDParent", _pRekening.IDParent));
                 paramCollection.Add(new DBParameter("@pbtRoot", _pRekening.Root));
                 paramCollection.Add(new DBParameter("@pbLeaf", _pRekening.Leaf));
                 paramCollection.Add(new DBParameter("@psNamaRekening", _pRekening.Nama));
                 paramCollection.Add(new DBParameter("@piDebet", _pRekening.Debet));
                paramCollection.Add(new DBParameter("@Kode", _pRekening.Kode));
                paramCollection.Add(new DBParameter("@NoBaris",_pRekening.NoBaris));

                 if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool SimpanRekeningDJPK(ref Rekening _pRekening)
        {
            try
            {
                

                    _isError = true;

                    SSQL = "DELETE mRekeningDJPK  where IIDRekening =" + _pRekening.ID.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);



                    SSQL = "INSERT INTO mRekeningDJPK (IIDRekening,sNamaRekening) values (" +
                        "@pIIDRekening, @psNamaRekening)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pIIDRekening", _pRekening.ID));
                    paramCollection.Add(new DBParameter("@psNamaRekening", _pRekening.Nama));
                
                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool Hapus(long pIDRekening)
        {
            try
            {
                SSQL = "DELETE FROM " + m_sNamaTabel + " WHERE IIDRekening=@REKENING";
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@REKENING", pIDRekening));

                if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
