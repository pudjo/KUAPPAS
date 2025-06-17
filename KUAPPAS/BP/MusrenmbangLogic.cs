using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using DataAccess;
using System.Data;
using Formatting;
using Excel = Microsoft.Office.Interop.Excel;

namespace BP
{
    public class MusrenmbangLogic : BP
    {
        public MusrenmbangLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "musrenbang";
        }
        public List<Musrenmbang> Get()
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),
                                
                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["keterangan"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
                               
                                   IDSUbKegiatan= DataFormat.GetInteger(dr["IDsubKegiatan"])

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
        public Musrenmbang GetByID(int id)
        {

            Musrenmbang m = new Musrenmbang();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " where id= " + id.ToString();// ORDER BY ID";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        m = new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),
                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["keterangan"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDSUbKegiatan = DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    
                                 
                                };
                    }
                }
                return m;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }
        public List<Musrenmbang> GetTanpaKegiatanByIDDInas(int _idDInas, int IDUrusan, int IDProgram, int IDKegiatan)
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {
                //if (IDUrusan == 0 &&  IDProgram ==0 &&  IDKegiatan ==0)
                //SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
                //     " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
                //     " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
                //    " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " + 
                //     " where IDDInas =" + _idDInas.ToString() +
                //     " ORDER BY ID";
                //else
                //    SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
                //     " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
                //     " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
                //    " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
                //     " where IDDInas =" + _idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram = " + IDProgram.ToString() + " AND IDKegiatan= " + IDKegiatan.ToString() +
                //     " ORDER BY ID";

                if (IDUrusan == 0 && IDProgram == 0 && IDKegiatan == 0)
                    SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
                         " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
                         " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
                        " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
                         " where IDDInas =" + _idDInas.ToString() +
                         " ORDER BY ID";
                else
                    SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
                     " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
                     " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
                    " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
                     " where IDDInas =" + _idDInas.ToString() + " AND IDUrusan= " + IDUrusan.ToString() + " AND IDProgram = " + IDProgram.ToString() + " AND IDKegiatan= " + IDKegiatan.ToString() +
                     " ORDER BY ID";




                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),
                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["lokasi"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
                                    IDSUbKegiatan= DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    iidrekening = DataFormat.GetLong(dr["IIDrekening"]),
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
        public bool SImpanSUbKegiatan(List<Musrenmbang> lst,RemoteConnection rCon)
        {
            foreach (Musrenmbang m in lst)
            {
                SSQL = "UPDATE Musrenbang set IDSUBKegiatan= @idSUb  WHERE ID= @id";
             //   _dbHelper.ExecuteNonQuery(SSQL);
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@idSUb", m.IDSUbKegiatan));
                paramCollection.Add(new DBParameter("@id", m.id));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, rCon.GetConnection());
            }
            return true;


        }

        public bool SImpanSUbKegiatan50(List<Musrenmbang> lst, RemoteConnection rCon)
        {
            foreach (Musrenmbang m in lst)
            {
                SSQL = "UPDATE Musrenbang set IDSUBKegiatan50= @idSUb  WHERE ID= @id";
                //   _dbHelper.ExecuteNonQuery(SSQL);
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@idSUb", m.IDSUbKegiatan));
                paramCollection.Add(new DBParameter("@id", m.id));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, rCon.GetConnection());
            }
            return true;


        }

        public List<Musrenmbang> GetAssignedByIDDInas2021(int _idDInas, int idUrusan, int idProgram, int idKegiatan, long idSUbKegiatan, RemoteConnection rCon, int profile = 2)
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {


                if (Tahun >= 2021)
                {

                    //SSQL = "SELECT vwMusrenbang90.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi " +
                    //    " FROM vwMusrenbang90 " +
                    //    " left join dbo.mDusun  on vwMusrenbang90.dusun_id = mDusun.ID and vwMusrenbang90.desa_id = mDusun.Desa " +
                    //    " and vwMusrenbang90.kecamatan_id= mDusun.KEcamatan " +
                    //    " left join dbo.mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND " +
                    //    " vwMusrenbang90.desa_id= mDesa.ID " +
                    //    " left join dbo.mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
                    //    " where ISNULL(IDSUbKegiatan,0)=0 and iTahun = " + Tahun.ToString() +
                    //    " AND  IDDInas =" + _idDInas.ToString() + " AND statusbapeda= 4 and vwMusrenbang90.Status<9  AND  isnull(idsubkegiatansk,0) = " + idSUbKegiatan.ToString() +
                    //    " ORDER BY ID";
                    if (profile == 2)
                    {
                        SSQL = "SELECT m.*, ' ' + isnull(mDusun.Nama,'') + ', ' + isnull(mDesa.Nama,'') + ' ' + isnull(mKecamatan.Nama,'')  as lokasi " +
                               "   from musrenbang m " +
                         " left join dbo.mDusun  on m.dusun_id = mDusun.ID " +
                           " left join dbo.mDesa on " +
                            " m.desa_id= mDesa.ID  left join dbo.mKecamatan on m.Kecamatan_id = mKecamatan.ID " +
                         " inner join vwSKPD2 vs ON m.IDDInas = vs.Parent where m.idkegiatan in (    " +
                                " select  renstraSUbKegiatan.idkegiatan13  from renstraSUbKegiatan inner join " +
                              " subkegiatan on subkegiatan.idkegiatan13= renstrasubkegiatan.id where  subkegiatan.id=" + idSUbKegiatan.ToString() +
                              "  AND m.iTahun =" + Tahun.ToString() + " AND m.statusbapeda= 4 and vs.id =" + _idDInas.ToString() + ") and (isnull(m.idsubkegiatan,0)=0 OR m.idsubkegiatan=" + idSUbKegiatan.ToString() + ")";
                    }
                    else
                    {
                        //SSQL = "SELECT m.*, ' ' + isnull(mDusun.Nama,'') + ', ' + isnull(mDesa.Nama,'') + ' ' + isnull(mKecamatan.Nama,'')  as lokasi " +
                        //       "   from musrenbang m " +
                        // " left join dbo.mDusun  on m.dusun_id = mDusun.ID " +
                        //   " left join dbo.mDesa on " +
                        //    " m.desa_id= mDesa.ID  left join dbo.mKecamatan on m.Kecamatan_id = mKecamatan.ID " +
                        // " inner join vwSKPD2 vs ON m.IDDInas = vs.Parent where m.idkegiatan in (    " +
                        //        " select  renstrasubkegiatan50.idkegiatan13  from renstrasubkegiatan50 inner join " +
                        //      " subkegiatan50 on subkegiatan50.idmaster= renstrasubkegiatan50.idmaster where  subkegiatan50.id=" + idSUbKegiatan.ToString() +
                        //      "  AND m.iTahun =" + Tahun.ToString() + " AND m.statusbapeda= 4 and vs.id =" + _idDInas.ToString() + ") and (isnull(m.idsubkegiatan50,0)=0 OR m.idsubkegiatan50=" + idSUbKegiatan.ToString() + ")";

                        SSQL = "SELECT m.*, ' ' + isnull(mDusun.Nama,'') + ', ' + isnull(mDesa.Nama,'') + ' ' + isnull(mKecamatan.Nama,'')  as lokasi " +
                               "   from musrenbang m " +
                         " left join dbo.mDusun  on m.dusun_id = mDusun.ID " +
                           " left join dbo.mDesa on " +
                            " m.desa_id= mDesa.ID  left join dbo.mKecamatan on m.Kecamatan_id = mKecamatan.ID " +
                         " inner join vwSKPD2 vs ON m.IDDInas = vs.Parent where m.idkegiatan in (    " +
                                " select  renstrasubkegiatan50.idkegiatan13  from renstrasubkegiatan50 inner join " +
                              " subkegiatan50 on subkegiatan50.idmaster= renstrasubkegiatan50.idmaster where  subkegiatan50.id=" + idSUbKegiatan.ToString() +
                              "  AND m.iTahun =" + Tahun.ToString() + " AND m.statusbapeda= 4 and vs.id =" + _idDInas.ToString() + ")";// and (isnull(m.idsubkegiatan50,0)=0 OR m.idsubkegiatan50=" + idSUbKegiatan.ToString() + ")";


                    }

                }
                else
                {
                    SSQL = "SELECT m.*, ' ' + isnull(mDusun.Nama,'') + ', ' + isnull(mDesa.Nama,'') + ' ' + isnull(mKecamatan.Nama,'')  as lokasi " +
                          "   from musrenbang m " +
                    " left join dbo.mDusun  on m.dusun_id = mDusun.ID " +
                      " left join dbo.mDesa on " +
                       " m.desa_id= mDesa.ID  left join dbo.mKecamatan on m.Kecamatan_id = mKecamatan.ID " +
                    " where m.idkegiatan =    " + idKegiatan.ToString() +
                         "  AND m.iTahun =" + Tahun.ToString() + " AND m.statusbapeda= 4 and m.Iddinas =" + _idDInas.ToString();

                }


                DataTable dt = new DataTable();
                //dt = _dbHelper.ExecuteDataTable(SSQL);//, rCon.GetConnection());
                dt = _dbHelper.ExecuteDataTable(SSQL, rCon.GetConnection());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),
                                  
                                    nama = DataFormat.GetString(dr["namaRKA"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["Pagubapeda"]),
                                  
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
                                  
                                }).ToList();
                    }
                }
                //}
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public List<Musrenmbang> GetPaket2022(int _idDInas, long  idSUbKegiatan )
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {

                SSQL = "SELECT * from Musrenbang WHERE IDDINAS =" + _idDInas.ToString() + " AND IDSUBKEGIATAN =" + idSUbKegiatan.ToString();
                 



                    DataTable dt = new DataTable();
                    //dt = _dbHelper.ExecuteDataTable(SSQL);//, rCon.GetConnection());
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new Musrenmbang()
                                    {
                                        id = DataFormat.GetInteger(dr["id"]),
                                        nama = DataFormat.GetString(dr["Nama"]),
                                        volume = DataFormat.GetDecimal(dr["volume"]),
                                        satuan = DataFormat.GetString(dr["satuan"]),
                                        pagu = DataFormat.GetDecimal(dr["Pagu"]),
                                        keterangan = "",
                                     
                                     
                                    }).ToList();
                        }
                    }
                //}
                return _lst;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
        }
        public bool  UpdatePagu(int id, decimal  nilai, RemoteConnection rCon)
        {

            

                try{

                   

                    SSQL = "update  musrenbang " +
                     " set cRKA=@pRKA where id = @pid";

                      DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pRKA", nilai,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@pid", id,DbType.Int32));


                                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection, rCon.GetConnection());


                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;

                return false ;
            }
        }
        public List<Musrenmbang> GetUnAssignedByIDDInas(int _idDInas)
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {

                if (_idDInas > 0)
                {

                    SSQL = "SELECT musrenbang.*, ' - ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
                             " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
                             " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
                            " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
                             " where IDDInas =" + _idDInas.ToString() + " AND IDProgram = 0  AND IDKegiatan= 0 " +
                             " ORDER BY nama";
                }
                else
                {

                }



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),
                                 
                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["lokasi"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),

                                  
                                    IDSUbKegiatan= DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    iidrekening = DataFormat.GetLong(dr["IIDRekening"]),

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


        public bool SimpanStatusAPBD(int IDDInas, long IDKEgiatan)
        {
           
            try
            {

                SSQL ="UPDATE musrenbang set statusAPBD=0, nilaiAPBD=0 where IDDInas =" + IDDInas.ToString() +" and IDKegiatan ="+ IDKEgiatan.ToString() + " AND TahunAPBD= "+ Tahun.ToString();
                RemoteConnection rCOn = new RemoteConnection();

                _dbHelper.ExecuteNonQuery(SSQL,rCOn.GetConnection());
                

                    DBParameterCollection paramCollection = new DBParameterCollection();

                    //paramCollection.Add(new DBParameter("@pKUA", kua.JumlahMurni));
                    //paramCollection.Add(new DBParameter("@pID", kua.IDKegiatan));
                    //paramCollection.Add(new DBParameter("@pSKPD", kua.IDDinas));
                

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


        public bool SimpanNilaiMusrenbang(int IDDInas, long IDKEgiatan, int IDM, decimal NilaiRKA)
        {

            try
            {

                SSQL = "UPDATE musrenbang set cRKA=@pcRKA,statusRKABappeda =0 where  ID =  @pID  and " +
                    "IDDInas =@pIDDinas  and IDKegiatan =@pIDKegiatan";

                RemoteConnection rCOn = new RemoteConnection();
    

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pcRKA", NilaiRKA));
                paramCollection.Add(new DBParameter("@pID",IDM));
                paramCollection.Add(new DBParameter("@pIDDinas",IDDInas));
                paramCollection.Add(new DBParameter("@pIDKegiatan", IDKEgiatan));
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection, rCOn.GetConnection());
                return true;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }

        public List<Musrenmbang> GetVerifiedByBapeda(RemoteConnection rConn)
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {
                SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi, satuan.nama as namasatuan FROM musrenbang " +
                         " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
                         " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
                        " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
                         "  left join satuan on satuan.id = musrenbang.satuan where iTahun = " + Tahun.ToString() +
                         "  AND statusbapeda= 4 and musrenbang.Status<9  and statusRKABappeda=3 ORDER BY ID";

        //public List<Musrenmbang> GetVerifiedByBapeda(int _idDInas, int IDUrusan, int IDProgram, int IDKegiatan, RemoteConnection rConn)
        //{

        //    List<Musrenmbang> mListUnit = new List<Musrenmbang>();
        //    try
        //    {

        //            SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi, satuan.nama as namasatuan FROM musrenbang " +
        //                 " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
        //                 " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
        //                " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
        //                 "  left join satuan on satuan.id = musrenbang.satuan where IDDInas =" + _idDInas.ToString() + " AND IDProgram =" + IDProgram.ToString()  +
        //                 "  AND IDKegiatan =" + IDKegiatan.ToString() + " AND statusbapeda= 4 and  and musrenbang.statusRKABappeda=3  and musrenbang.Status<9  ORDER BY ID";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, rConn.GetConnection ());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),

                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["lokasi"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),


                                    IDSUbKegiatan = DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    iidrekening = DataFormat.GetLong(dr["IIDRekening"]),

                                    
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



        public List<Musrenmbang> GetByIDProgram(int _pID)
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERe IDProgram =" + _pID.ToString() + " ORDER BY IDKegiatan,IDlokasi";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),

                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["lokasi"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),


                                    IDSUbKegiatan = DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    iidrekening = DataFormat.GetLong(dr["IIDRekening"]),

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

        public List<Musrenmbang> GetByIDProgramIDKegiatanFromRenja(int _pIDProgram, int _pIDKegiatan)
        {

            List<Musrenmbang> _lst = new List<Musrenmbang>();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERe IDProgram =" + _pIDProgram.ToString() + " ORDER BY IDKegiatan,IDlokasi";

                DataTable dt = new DataTable();

                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new Musrenmbang()
                                {
                                    id = DataFormat.GetInteger(dr["id"]),

                                    nama = DataFormat.GetString(dr["nama"]),
                                    volume = DataFormat.GetDecimal(dr["volume"]),
                                    satuan = DataFormat.GetString(dr["satuan"]),
                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
                                    keterangan = DataFormat.GetString(dr["lokasi"]),
                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),


                                    IDSUbKegiatan = DataFormat.GetInteger(dr["IDSubKegiatan"]),
                                    iidrekening = DataFormat.GetLong(dr["IIDRekening"]),

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


        public bool Simpan(List<Musrenmbang> _lst)
        {
            bool lRet = true;
            Hapus(Tahun);

            foreach (Musrenmbang m in _lst)
            {
                lRet = Simpan(m);

            }
            return lRet;


        }

        
        public bool Simpan(Musrenmbang _pMusrenmbang)
        {

            try
            {
                int _tanggal;
                if (_pMusrenmbang.id == 0) { }
               // _tanggal = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Month, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Day, 2));

                SSQL = " INSERT INTO musrenbang (iddinas, idsubkegiatan,Type, nama,volume,satuan, pagu )  VALUES (" +
                       "@piddinas, @pidsubkegiatan,1, @pnama, @pvolume,@psatuan ,@ppagu)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@piddinas",_pMusrenmbang.IDDInas)); 
                paramCollection.Add(new DBParameter("@pidsubkegiatan",_pMusrenmbang.IDSUbKegiatan));
                paramCollection.Add(new DBParameter("@pnama",_pMusrenmbang.nama));
                paramCollection.Add(new DBParameter("@pvolume",_pMusrenmbang.volume));
                paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan));
                paramCollection.Add(new DBParameter("@ppagu",_pMusrenmbang.pagu));

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

        public int SimpanEx(Musrenmbang _pMusrenmbang)
        {
            return 0;

            //try
            //{
            //    //int _tanggal;
            //    if (_pMusrenmbang.id == 0)
            //    {
            //        int newID = GetMaxID();// _pMusrenmbang.id;
                    
            //        SSQL = " INSERT INTO musrenbang (id ,desa_id ,dusun_id ,kecamatan_id ,skpd_id ,program_id , " +
            //                " kegiatan_id ,prioritas ,nama ,volume ,sumberdana , " +
            //               " satuan ,pagu ,keterangan ,iTahun,IDUrusan ,IDDInas ,IDProgram ,IDKegiatan,Pagu2, TahunAPBD,TahapAPBD,idUrusanMaster, idProgramMaster,idKegiatanMaster,volSKPD, paguskpd, keteranganskpd, statusskpd,volbapeda, pagubapeda ,keteranganbapeda, statusbapeda,levelmusrenbang,prioritasdesa, prioritaskecamatan, prioritasskpd, prioritasbapeda ,cRKA,statusRKABappeda)  VALUES (" +
            //               "@pid ,@pdesa_id ,@pdusun_id ,@pkecamatan_id ,@pskpd_id ,@pprogram_id , " +
            //                " @pkegiatan_id ,@pprioritas ,@pnama ,@pvolume , @pSumberdana," +
            //               " @psatuan ,@ppagu ,@pketerangan ,@piTahun,@pIDUrusan ,@pIDDInas ,@pIDProgram ,@pIDKegiatan,@pPagu2, @pTahunAPBD,@pTahapAPBD ,@pidUrusanMaster, @pidProgramMaster,@pidKegiatanMaster,@pvolskpd, @ppaguskpd, @pketeranganskpd,@pstatusskpd,@pvolbapeda, @ppagubapeda, @pketeranganbapeda, @pstatusbapeda,@plevelmusrenbang ,@pprioritasdesa, @pprioritaskecamatan, @pprioritasskpd, @pprioritasbapeda,@pcRKA,@pstatusRKABappeda )";

            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@pid", newID));
            //        paramCollection.Add(new DBParameter("@pdesa_id", _pMusrenmbang.desa_id));
            //        paramCollection.Add(new DBParameter("@pdusun_id", _pMusrenmbang.dusun_id));
            //        paramCollection.Add(new DBParameter("@pkecamatan_id ", _pMusrenmbang.kecamatan_id));
            //        paramCollection.Add(new DBParameter("@pskpd_id", _pMusrenmbang.DewanID ));
            //        paramCollection.Add(new DBParameter("@pprogram_id", _pMusrenmbang.program_id));
            //        paramCollection.Add(new DBParameter("@pkegiatan_id", _pMusrenmbang.kecamatan_id));
            //        paramCollection.Add(new DBParameter("@pprioritas", _pMusrenmbang.prioritas));
            //        paramCollection.Add(new DBParameter("@pnama", _pMusrenmbang.nama));
            //        paramCollection.Add(new DBParameter("@pvolume", _pMusrenmbang.volume));
            //        paramCollection.Add(new DBParameter("@pSumberdana", _pMusrenmbang.sumberDana));
            //        paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan));
            //        paramCollection.Add(new DBParameter("@ppagu", _pMusrenmbang.pagu));
            //        paramCollection.Add(new DBParameter("@pketerangan", _pMusrenmbang.keterangan));
            //        paramCollection.Add(new DBParameter("@piTahun", _pMusrenmbang.iTahun));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", _pMusrenmbang.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pIDDInas", _pMusrenmbang.IDDInas));
            //        paramCollection.Add(new DBParameter("@pIDProgram", _pMusrenmbang.IDProgram));
            //        paramCollection.Add(new DBParameter("@pIDKegiatan", _pMusrenmbang.IDKegiatan));
            //        paramCollection.Add(new DBParameter("@pPagu2", _pMusrenmbang.Pagu2));
            //        paramCollection.Add(new DBParameter("@pTahunAPBD", _pMusrenmbang.TahunAPBD));
            //        paramCollection.Add(new DBParameter("@pTahapAPBD", _pMusrenmbang.TahapAPBD));
            //        paramCollection.Add(new DBParameter("@pidUrusanMaster", _pMusrenmbang.IDUrusanMaster )); 
            //        paramCollection.Add(new DBParameter("@pidProgramMaster", _pMusrenmbang.IDProgramMaster ));
            //        paramCollection.Add(new DBParameter("@pidKegiatanMaster", _pMusrenmbang.IDKegiatanMaster));
            // //       paramCollection.Add(new DBParameter("@pType", _pMusrenmbang.Type));
            //        paramCollection.Add(new DBParameter("@pvolskpd", _pMusrenmbang.volskpd));
            //        paramCollection.Add(new DBParameter("@ppaguskpd", _pMusrenmbang.paguSKPD));
            //        paramCollection.Add(new DBParameter("@pketeranganskpd", _pMusrenmbang.keteranganskpd == null ? "" : _pMusrenmbang.keteranganskpd));
            //        paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusskpd));
            //        paramCollection.Add(new DBParameter("@pvolbapeda", _pMusrenmbang.volbapeda)); 
            //        paramCollection.Add(new DBParameter("@ppagubapeda", _pMusrenmbang.pagubapeda));
            //        paramCollection.Add(new DBParameter("@pketeranganbapeda", _pMusrenmbang.keteranganbapeda == null ? "" : _pMusrenmbang.keteranganbapeda));
            //        paramCollection.Add(new DBParameter("@pstatusbapeda", _pMusrenmbang.statusbapeda));
            //        paramCollection.Add(new DBParameter("@plevelmusrenbang", _pMusrenmbang.levelmusrenbang));
            //        paramCollection.Add(new DBParameter("@pprioritasdesa", _pMusrenmbang.PrioritasDesa )); 
            //        paramCollection.Add(new DBParameter("@pprioritaskecamatan", _pMusrenmbang.PrioritasKecamatan ));
            //        paramCollection.Add(new DBParameter("@pprioritasskpd", _pMusrenmbang.PrioritasSKPD ));
            //        paramCollection.Add(new DBParameter("@pprioritasbapeda", _pMusrenmbang.PrioritasBapeda ));
            //        paramCollection.Add(new DBParameter("@pcRKA", _pMusrenmbang.cRKA));
            //        paramCollection.Add(new DBParameter("@pstatusRKABappeda", _pMusrenmbang.statusRKABappeda));

            //        //  paramCollection.Add(new DBParameter("@pidUpdate", _pMusrenmbang.idUpdate ));
            //        //paramCollection.Add(new DBParameter("@pidDelete", _pMusrenmbang.idDelete ));
            //        //paramCollection.Add(new DBParameter("@pstatus", _pMusrenmbang.Status));

                 


            //        if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
            //        {
            //            return newID;
            //        }
                
            //    }

            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    return 0;
            //}
        
        }
        public int SimpanImport(Musrenmbang _pMusrenmbang)
        {
return 0;

            //try
            //{
            //    //int _tanggal;
            //   // if (_pMusrenmbang.id == 0)
            //    //{
            //      //  int newID = GetMaxID();// _pMusrenmbang.id;

            //        SSQL = " INSERT INTO musrenbang (id ,desa_id ,dusun_id ,kecamatan_id ,skpd_id ,program_id , " +
            //                " kegiatan_id ,prioritas ,nama ,volume ,sumberdana , " +
            //               " satuan ,pagu ,keterangan ,iTahun,IDUrusan ,IDDInas ,IDProgram ,IDKegiatan,Pagu2, TahunAPBD,TahapAPBD,idUrusanMaster, idProgramMaster,idKegiatanMaster,volSKPD, paguskpd, keteranganskpd, statusskpd,volbapeda, pagubapeda ,keteranganbapeda, statusbapeda,levelmusrenbang,prioritasdesa, prioritaskecamatan, prioritasskpd, prioritasbapeda ,cRKA,statusRKABappeda)  VALUES (" +
            //               "@pid ,@pdesa_id ,@pdusun_id ,@pkecamatan_id ,@pskpd_id ,@pprogram_id , " +
            //                " @pkegiatan_id ,@pprioritas ,@pnama ,@pvolume , @pSumberdana," +
            //               " @psatuan ,@ppagu ,@pketerangan ,@piTahun,@pIDUrusan ,@pIDDInas ,@pIDProgram ,@pIDKegiatan,@pPagu2, @pTahunAPBD,@pTahapAPBD ,@pidUrusanMaster, @pidProgramMaster,@pidKegiatanMaster,@pvolskpd, @ppaguskpd, @pketeranganskpd,@pstatusskpd,@pvolbapeda, @ppagubapeda, @pketeranganbapeda, @pstatusbapeda,@plevelmusrenbang ,@pprioritasdesa, @pprioritaskecamatan, @pprioritasskpd, @pprioritasbapeda,@pcRKA,@pstatusRKABappeda )";

            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@pid", _pMusrenmbang.id ));
            //        paramCollection.Add(new DBParameter("@pdesa_id", _pMusrenmbang.desa_id));
            //        paramCollection.Add(new DBParameter("@pdusun_id", _pMusrenmbang.dusun_id));
            //        paramCollection.Add(new DBParameter("@pkecamatan_id ", _pMusrenmbang.kecamatan_id));
            //        paramCollection.Add(new DBParameter("@pskpd_id", _pMusrenmbang.DewanID));
            //        paramCollection.Add(new DBParameter("@pprogram_id", _pMusrenmbang.program_id));
            //        paramCollection.Add(new DBParameter("@pkegiatan_id", _pMusrenmbang.kecamatan_id));
            //        paramCollection.Add(new DBParameter("@pprioritas", _pMusrenmbang.prioritas));
            //        paramCollection.Add(new DBParameter("@pnama", _pMusrenmbang.nama));
            //        paramCollection.Add(new DBParameter("@pvolume", _pMusrenmbang.volume));
            //        paramCollection.Add(new DBParameter("@pSumberdana", _pMusrenmbang.sumberDana));
            //        paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan));
            //        paramCollection.Add(new DBParameter("@ppagu", _pMusrenmbang.pagu));
            //        paramCollection.Add(new DBParameter("@pketerangan", _pMusrenmbang.keterangan));
            //        paramCollection.Add(new DBParameter("@piTahun", _pMusrenmbang.iTahun));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", _pMusrenmbang.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pIDDInas", _pMusrenmbang.IDDInas));
            //        paramCollection.Add(new DBParameter("@pIDProgram", _pMusrenmbang.IDProgram));
            //        paramCollection.Add(new DBParameter("@pIDKegiatan", _pMusrenmbang.IDKegiatan));
            //        paramCollection.Add(new DBParameter("@pPagu2", _pMusrenmbang.Pagu2));
            //        paramCollection.Add(new DBParameter("@pTahunAPBD", _pMusrenmbang.TahunAPBD));
            //        paramCollection.Add(new DBParameter("@pTahapAPBD", _pMusrenmbang.TahapAPBD));
            //        paramCollection.Add(new DBParameter("@pidUrusanMaster", _pMusrenmbang.IDUrusanMaster));
            //        paramCollection.Add(new DBParameter("@pidProgramMaster", _pMusrenmbang.IDProgramMaster));
            //        paramCollection.Add(new DBParameter("@pidKegiatanMaster", _pMusrenmbang.IDKegiatanMaster));
            //        //       paramCollection.Add(new DBParameter("@pType", _pMusrenmbang.Type));
            //        paramCollection.Add(new DBParameter("@pvolskpd", _pMusrenmbang.volskpd));
            //        paramCollection.Add(new DBParameter("@ppaguskpd", _pMusrenmbang.paguSKPD));
            //        paramCollection.Add(new DBParameter("@pketeranganskpd", _pMusrenmbang.keteranganskpd == null ? "" : _pMusrenmbang.keteranganskpd));
            //        paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusskpd));
            //        paramCollection.Add(new DBParameter("@pvolbapeda", _pMusrenmbang.volbapeda));
            //        paramCollection.Add(new DBParameter("@ppagubapeda", _pMusrenmbang.pagubapeda));
            //        paramCollection.Add(new DBParameter("@pketeranganbapeda", _pMusrenmbang.keteranganbapeda == null ? "" : _pMusrenmbang.keteranganbapeda));
            //        paramCollection.Add(new DBParameter("@pstatusbapeda", _pMusrenmbang.statusbapeda));
            //        paramCollection.Add(new DBParameter("@plevelmusrenbang", _pMusrenmbang.levelmusrenbang));
            //        paramCollection.Add(new DBParameter("@pprioritasdesa", _pMusrenmbang.PrioritasDesa));
            //        paramCollection.Add(new DBParameter("@pprioritaskecamatan", _pMusrenmbang.PrioritasKecamatan));
            //        paramCollection.Add(new DBParameter("@pprioritasskpd", _pMusrenmbang.PrioritasSKPD));
            //        paramCollection.Add(new DBParameter("@pprioritasbapeda", _pMusrenmbang.PrioritasBapeda));
            //        paramCollection.Add(new DBParameter("@pcRKA", _pMusrenmbang.cRKA));
            //        paramCollection.Add(new DBParameter("@pstatusRKABappeda", _pMusrenmbang.statusRKABappeda));

            //        //  paramCollection.Add(new DBParameter("@pidUpdate", _pMusrenmbang.idUpdate ));
            //        //paramCollection.Add(new DBParameter("@pidDelete", _pMusrenmbang.idDelete ));
            //        //paramCollection.Add(new DBParameter("@pstatus", _pMusrenmbang.Status));




            //        if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
            //        {
            //         //   return newID;
            //        }

            //    //}

            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    return 0;
            //}

        }

        public bool SImpanImportPerencanaan(List<Musrenmbang> lst)
        {
            try
            {
                SSQL = "DELETE musrenbang ";
                _dbHelper.ExecuteNonQuery(SSQL);
                int i = 0;
                foreach (Musrenmbang m in lst)
                {
                    SimpanImport(m);
                    i++;

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
        public bool Update(Musrenmbang _pMusrenmbang)
        {
            return true;

            //try
            //{

            //    SSQL = "UPDATE  musrenbang SET desa_id=@pdesa_id ,dusun_id=@pdusun_id ,kecamatan_id=@pkecamatan_id, " +
            //               "nama =@pnama,volume=@pvolume  , " +
            //               " satuan=@psatuan ,pagu2 =@ppagu ,keterangan=@pketerangan,IDurusan= @pIDurusan, IDProgram =@pIDProgram, IDKegiatan=@pIDKegiatan   where id = @pid ";

            //    DBParameterCollection paramCollection = new DBParameterCollection();

            //    paramCollection.Add(new DBParameter("@pdesa_id", _pMusrenmbang.desa_id));
            //    paramCollection.Add(new DBParameter("@pdusun_id", _pMusrenmbang.dusun_id));
            //    paramCollection.Add(new DBParameter("@pkecamatan_id ", _pMusrenmbang.kecamatan_id));
            //    paramCollection.Add(new DBParameter("@pnama", _pMusrenmbang.nama));
            //    paramCollection.Add(new DBParameter("@pvolume ", _pMusrenmbang.volume));
            //    paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan));
            //    paramCollection.Add(new DBParameter("@ppagu", _pMusrenmbang.pagu));
            //    paramCollection.Add(new DBParameter("@pketerangan", _pMusrenmbang.keterangan));
            //    paramCollection.Add(new DBParameter("@pIDurusan", _pMusrenmbang.IDUrusan));
            //    paramCollection.Add(new DBParameter("@pIDProgram", _pMusrenmbang.IDProgram));
            //    paramCollection.Add(new DBParameter("@pIDKegiatan", _pMusrenmbang.IDKegiatan));

            //    paramCollection.Add(new DBParameter("@pid", _pMusrenmbang.id));

            //    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    return false;
            //}
        }

        public bool UpdateKegiatan(List<Musrenmbang> _lst)
        {
            return true;
            //try
            //{
            //    foreach (Musrenmbang m in mListUnit)
            //    {

            //        SSQL = " update musrenbang SET iTahun=@piTahun,IDUrusan=@pIDUrusan  ,IDDInas=@pIDDInas ,IDProgram =@pIDProgram,IDKegiatan =@pIDKegiatan, pagu2= @ppagu2,tahunAPBD=@tahunAPBD, tahapAPBD=@tahapAPBD,IDProgramMaster =@pIDProgramMaster,IDKegiatanMaster =@pIDKegiatanMaster, IDUrusanMaster= @pIDUrusanMaster  where id = @pid";

            //        DBParameterCollection paramCollection = new DBParameterCollection();
            //        paramCollection.Add(new DBParameter("@piTahun", m.iTahun));
            //        paramCollection.Add(new DBParameter("@pIDUrusan", m.IDUrusan));
            //        paramCollection.Add(new DBParameter("@pIDDInas", m.IDDInas));
            //        paramCollection.Add(new DBParameter("@pIDProgram", m.IDProgram));
            //        paramCollection.Add(new DBParameter("@pIDKegiatan", m.IDKegiatan));
            //        paramCollection.Add(new DBParameter("@ppagu2", m.Pagu2));
            //        paramCollection.Add(new DBParameter("@tahunAPBD", m.TahunAPBD));
            //        paramCollection.Add(new DBParameter("@tahapAPBD", m.TahapAPBD));
            //        paramCollection.Add(new DBParameter("@pIDProgramMaster", m.IDProgramMaster));
            //        paramCollection.Add(new DBParameter("@pIDKegiatanMaster", m.IDKegiatanMaster));
            //        paramCollection.Add(new DBParameter("@pIDUrusanMaster", m.IDUrusanMaster));



            //        paramCollection.Add(new DBParameter("@pid", m.id));


            //        _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
            //    }
            //    return true;


            //}
            //catch (Exception ex)
            //{
            //    _isError = true;
            //    _lastError = ex.Message + " " + SSQL;
            //    return false;
            //}
        }

        public bool LepasKegiatan(List<Musrenmbang> _lst)
        {
            try
            {
                foreach (Musrenmbang m in _lst)
                {
                    LepasMusrenbangKegiatan(m);

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
        public bool LepasMusrenbangKegiatan(Musrenmbang _mrb)
        {
            try
            {

                SSQL = " update musrenbang SET iTahun=0,IDUrusan=0,IDProgram =0,IDKegiatan =0, pagu2= 0,tahunAPBD=0, tahapAPBD=0, IDkegiatanmaster=0, idprogrammaster=0, idurusanmaster=0  where id = @pid";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pid", _mrb.id));
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
        public void SimpanMaterProgram(int _iTahun)
        {
            SSQL = "Select distinct  * INTO tempMasterProgram FROM mprogrammusrenmbang WHERE iTahun = " + _iTahun.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM mprogrammusrenmbang WHERE iTahun =" + _iTahun.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "INSERT into mprogrammusrenmbang SELECT * FROM tempMasterProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "DROP TABLE tempMasterProgram ";
            _dbHelper.ExecuteNonQuery(SSQL);

            //  _dbHelper.ExecuteNonQuery(SSQL);

        }
        public void SimpanMaterKegiatan(int _iTahun)
        {
            SSQL = "Select distinct * INTO tempMasterKeg FROM mKegiatanMusrenmbang WHERE iTahun = " + _iTahun.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);
            SSQL = "DELETE FROM mKegiatanMusrenmbang WHERE iTahun = " + _iTahun.ToString();
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "INSERT into mKegiatanMusrenmbang SELECT * FROM tempMasterKeg ";
            _dbHelper.ExecuteNonQuery(SSQL);

            SSQL = "DROP TABLE tempMasterKeg ";
            _dbHelper.ExecuteNonQuery(SSQL);




        }
        public bool Hapus(int _tahun)
        {
            try
            {

                SSQL = "DELETE FROM musrenbang WHERE iTahun =" + _tahun.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                // {
                //SSQL = "DELETE FROM mprogrammusrenmbang WHERE iTahun =" + _tahun.ToString();
                //_dbHelper.ExecuteNonQuery(SSQL);
                //SSQL = "DELETE FROM mKegiatanMusrenmbang WHERE iTahun =" + _tahun.ToString();
                //_dbHelper.ExecuteNonQuery(SSQL);
                return true;
                //}
                //else
                //{
                //    return false;
                // }
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;

            }

        }
        public bool ExportToExcell(int idDInas, string ExcelFilePath)
        {
            try
            {

                //  SSQL = "Select * from Musrenbang where IDDInas =" + idDInas.ToString();


                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Excel._Worksheet workSheet = excelApp.ActiveSheet;

                SSQL = "select mprogram.id, mprogram.sNamaProgram, " +
                    " mKegiatan.ID ,mKegiatan.sNamaKegiatan , musrenbang.id, musrenbang.nama ,musrenbang.volume,musrenbang.satuan, musrenbang.pagu2 " +
                    " from musrenbang  INNER JOIN mPRogram on musrenbang.idProgramMaster = mProgram.ID   inner join mKegiatan  " +
                    " on mKegiatan.id = musrenbang.idkegiatanmaster  and  musrenbang.idprogramMaster = mKegiatan.idProgram  where musrenbang.IDDInas =" + idDInas.ToString() + " AND  idKegiatanMaster >0 ";



                DataTable Tbl = new DataTable();
                Tbl = _dbHelper.ExecuteDataTable(SSQL);



                //// column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excelApp.Visible = true;
                }
                return true;
            }
            catch (Exception ex)
            {

               // throw new Exception("ExportToExcel: \n" + ex.Message);
                _lastError = ex.Message;
                return false;

            }


        }
    }
}


////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using DTO;
////using BP;
////using DataAccess;
////using System.Data;
////using Formatting;
////using Excel = Microsoft.Office.Interop.Excel;

////namespace BP
////{
////    public class MusrenmbangLogic : BP
////    {
////        private DateTime DEFAULTDATEUPDATE;
////        private DateTime DEFAULTDATEDELETE;

////        public MusrenmbangLogic(int _pTahun)
////            : base(_pTahun)
////        {
////            Tahun = _pTahun;
////            m_sNamaTabel = "musrenbang";
////            DEFAULTDATEUPDATE = new DateTime(1950, 1, 1);
////            DEFAULTDATEDELETE = new DateTime(1950, 1, 1);


////        }
////        public List<Musrenmbang> Get()
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {
////                SSQL = "SELECT * FROM " + m_sNamaTabel + " where Status<9 ORDER BY ID";
////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["keterangan"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),

////                                    dtUpdate = DataFormat.GetDateTime(dr["dtUpdate"]),
////                                    dtDelete = DataFormat.GetDateTime(dr["dtDelete"]),
////                                    idUpdate = DataFormat.GetInteger(dr["idUpdate"]),
////                                    idDelete = DataFormat.GetInteger(dr["idDelete"])



////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }
////        public Musrenmbang GetByID(int id)
////        {

////            Musrenmbang m = new Musrenmbang();
////            try
////            {
////                SSQL = "SELECT * FROM " + m_sNamaTabel + " where id= " + id.ToString();// ORDER BY ID";
////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        DataRow dr = dt.Rows[0];

////                        m = new Musrenmbang()
////                        {
////                            id = DataFormat.GetInteger(dr["id"]),
////                            desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                            dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                            kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                            skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                            program_id = DataFormat.GetInteger(dr["program_id"]),
////                            kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                            prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                            nama = DataFormat.GetString(dr["nama"]),
////                            volume = DataFormat.GetDecimal(dr["volume"]),
////                            satuan = DataFormat.GetInteger(dr["satuan"]),
////                            pagu = DataFormat.GetDecimal(dr["pagu"]),
////                            Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                            keterangan = DataFormat.GetString(dr["keterangan"]),
////                            iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                            IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                            IDKegiatanMaster = DataFormat.GetInteger(dr["IDKegiatanMaster"]),
////                            IDProgramMaster = DataFormat.GetInteger(dr["IDProgramMaster"]),
////                            IDUrusanMaster = DataFormat.GetInteger(dr["IDUrusanMaster"]),
////                            pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                            paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                            outcome = DataFormat.GetString(dr["outcome"]),
////                            keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                            latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                            sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                            idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                            dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                            dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                            idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                            levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),
////                            Type = DataFormat.GetInteger(dr["Type"]),
////                            keteranganskpd = DataFormat.GetString(dr["Keteranganskpd"]),
////                            keteranganbapeda = DataFormat.GetString(dr["Keteranganbapeda"]),
////                            statusskpd = DataFormat.GetInteger(dr["statusskpd"]),
////                            statusbapeda = DataFormat.GetInteger(dr["statusbapeda"]),
////                            volskpd = DataFormat.GetDecimal(dr["volskpd"]),
////                            volbapeda = DataFormat.GetDecimal(dr["volbapeda"]),
////                            PrioritasDesa = DataFormat.GetInteger(dr["prioritasdesa"]),
////                            PrioritasKecamatan = DataFormat.GetInteger(dr["prioritaskecamatan"]),
////                            PrioritasSKPD = DataFormat.GetInteger(dr["prioritasskpd"]),
////                            PrioritasBapeda = DataFormat.GetInteger(dr["prioritasbapeda"]),
////                            DewanID = DataFormat.GetInteger(dr["skpd_id"])


////                        };
////                    }
////                }
////                return m;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return null;
////            }
////        }


////        public List<Musrenmbang> GetTanpaKegiatanByIDDInas(int _idDInas, int IDUrusan, int IDProgram, int IDKegiatan)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {
////                //if (IDUrusan == 0 &&  IDProgram ==0 &&  IDKegiatan ==0)
////                //SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                //     " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                //     " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                //    " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " + 
////                //     " where IDDInas =" + _idDInas.ToString() +
////                //     " ORDER BY ID";
////                //else
////                //    SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                //     " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                //     " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                //    " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                //     " where IDDInas =" + _idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram = " + IDProgram.ToString() + " AND IDKegiatan= " + IDKegiatan.ToString() +
////                //     " ORDER BY ID";

////                if (IDUrusan == 0 && IDProgram == 0 && IDKegiatan == 0)
////                    SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                         " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         " where IDDInas =" + _idDInas.ToString() +
////                         " and Status<9 ORDER BY ID";
////                else
////                    SSQL = "SELECT musrenbang.*, 'Dusun: ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                     " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                     " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                    " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                     " where IDDInas =" + _idDInas.ToString() + " AND IDUrusan= " + IDUrusan.ToString() + " AND IDProgram = " + IDProgram.ToString() + " AND IDKegiatan= " + IDKegiatan.ToString() +
////                     " and Status<9 ORDER BY ID";




////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["lokasi"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    Type = DataFormat.GetInteger(dr["Type"]),
////                                    DewanID = DataFormat.GetInteger(dr["skpd_id"])
////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }
////        public List<Musrenmbang> GetAssignedByIDDInas(int _idDInas, int IDUrusan, int IDProgram, int IDKegiatan)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {

////                if (IDUrusan == 0 && IDProgram == 0 && IDKegiatan == 0)
////                    SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                         " inner join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " inner join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " inner join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         " where IDDInas =" + _idDInas.ToString() + " AND IDProgram >0 " +
////                         " and Status<9 ORDER BY ID";

////                else
////                {
////                    if (IDUrusan > 0 && IDProgram > 0 && IDKegiatan > 0)
////                    {
////                        SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                         " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         " where IDDInas =" + _idDInas.ToString() + " AND statusskpd= 3 AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram= " + IDProgram.ToString() + " AND IDKegiatan= " + IDKegiatan.ToString() +
////                         " and Status<9 ORDER BY ID";
////                    }
////                    else
////                    {
////                        if (IDUrusan > 0 && IDProgram > 0)
////                        {
////                            SSQL = "SELECT musrenbang.*, ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                             " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                             " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                            " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                             " where IDDInas =" + _idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram= " + IDProgram.ToString() +
////                             " and Status<9 ORDER BY ID";
////                        }
////                        else
////                        {
////                            SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                             " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                             " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                            " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                             " where IDDInas =" + _idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() +
////                             " and Status<9 ORDER BY ID";

////                        }
////                    }
////                }



////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["lokasi"]).Contains("egiatan") ? "" : DataFormat.GetString(dr["lokasi"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    TahunAPBD = DataFormat.GetInteger(dr["TahunAPBD"]),
////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"])
////                                  //  Type = DataFormat.GetInteger(dr["Type"])

////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }
////        public List<Musrenmbang> GetVerifiedByBapeda(int _idDInas, int IDUrusan, int IDProgram, int IDKegiatan)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {

////                if (IDUrusan == 0 && IDProgram == 0 && IDKegiatan == 0)
////                    SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi, satuan.nama as namasatuan FROM musrenbang " +
////                         " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         "  left join satuan on satuan.id = musrenbang.satuan where IDDInas =" + _idDInas.ToString() + " AND IDProgram >0 " +
////                         "  AND statusbapeda= 4 and and musrenbang.Status<9  ORDER BY ID";

////                else
////                {
////                    if (IDUrusan > 0 && IDProgram > 0 && IDKegiatan > 0)
////                    {
////                        SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi, satuan.nama as namasatuan FROM musrenbang " +
////                         " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         "   left join satuan on satuan.id = musrenbang.satuan where IDDInas =" + _idDInas.ToString() + "  AND statusbapeda= 4 AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram= " + IDProgram.ToString() + " AND IDKegiatan= " + IDKegiatan.ToString() +
////                         " and musrenbang.Status<9 ORDER BY ID";
////                    }
////                    else
////                    {
////                        if (IDUrusan > 0 && IDProgram > 0)
////                        {
////                            SSQL = "SELECT musrenbang.*, ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi, satuan.nama as namasatuan FROM musrenbang " +
////                             " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                             " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                            " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                             "   left join satuan on satuan.id = musrenbang.satuan where IDDInas =" + _idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() + " AND IDProgram= " + IDProgram.ToString() +
////                             "  AND statusbapeda= 4 and musrenbang.Status<9  ORDER BY ID";
////                        }
////                        else
////                        {
////                            SSQL = "SELECT musrenbang.*, ' ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi, satuan.nama as namasatuan FROM musrenbang " +
////                             " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                             " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                            " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                             "   left join satuan on satuan.id = musrenbang.satuan where IDDInas =" + _idDInas.ToString() + " AND IDUrusan = " + IDUrusan.ToString() +
////                             "  AND statusbapeda= 4 and musrenbang.Status<9  ORDER BY ID";

////                        }
////                    }
////                }



////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["lokasi"]).Contains("egiatan") ? "" : DataFormat.GetString(dr["lokasi"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    TahunAPBD = DataFormat.GetInteger(dr["TahunAPBD"]),
////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),
////                                    Type = DataFormat.GetInteger(dr["Type"]),
////                                    namasatuan = DataFormat.GetString(dr["namasatuan"]),
////                                    DewanID = DataFormat.GetInteger(dr["skpd_id"])

////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }
////        public List<Musrenmbang> GetUnAssignedByIDDInas(int _idDInas)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {

////                SSQL = "SELECT musrenbang.*, ' - ' + mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi FROM musrenbang " +
////                         " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         " where IDDInas =" + _idDInas.ToString() + " AND IDProgram = 0  AND IDKegiatan= 0 " +
////                         " and musrenbang.Status<9  ORDER BY nama";




////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["lokasi"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    TahunAPBD = DataFormat.GetInteger(dr["TahunAPBD"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),
////                                    DewanID = DataFormat.GetInteger(dr["skpd_id"])

////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }

////        public List<Musrenmbang> GetUnAssignedByTeritory(int pTahun, int pKecamatan = 0, int pDesa = 0, int pDusun = 0, int _idDInas = 0, int pType = 0, int pUrusan = 0, int pProgram = 0, int pIDKegiatan = 0, int _pStatusSKPD = -1, int _pStatusBapeda = -1, int _pDewan = -1)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {


////                SSQL = "SELECT musrenbang.*, mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi , satuan.nama as namasatuan , stskpd.Nama as namastatusSKPD, stbapeda.Nama as namaStatusBapeda, mSKPD.sNamaSKPD FROM musrenbang " +
////                         " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////                         " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////                        " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////                         " left join satuan on satuan.id= musrenbang.satuan left Join status as stskpd on stskpd.id= musrenbang.statusskpd " +
////                         " left Join status as stbapeda on stbapeda.id= musrenbang.statusbapeda " +
////                         " Left Join mSKPD on mSKPD.ID = musrenbang.IDDInas  where  musrenbang.Status<9   ";
////                //if (pType>0){ stskpd.Nama as namastatusSKPD, stbapeda.Nama as namaStatusBapeda
////                //    SSQL = SSQL+ "  AND Type=  " + pType.ToString() ;
////                //}
////                if (pTahun > 0)
////                    SSQL = SSQL + "  AND musrenbang.iTahun=  " + pTahun.ToString().ToString();

////                if (pKecamatan > 0)
////                {
////                    SSQL = SSQL + " AND musrenbang.kecamatan_ID= " + pKecamatan.ToString();
////                    if (pDesa > 0)
////                    {
////                        SSQL = SSQL + " AND musrenbang.desa_ID= " + pDesa.ToString();
////                        if (pDusun > 0)
////                            SSQL = SSQL + " AND musrenbang.dusun_ID= " + pDusun.ToString();

////                    }
////                }
////                if (_idDInas > 0)

////                    SSQL = SSQL + " AND IDDInas =" + _idDInas.ToString();

////                if (pUrusan > 0)
////                {
////                    SSQL = SSQL + " AND musrenbang.IDUrusan  =" + pUrusan.ToString();
////                    if (pProgram > 0)
////                    {
////                        SSQL = SSQL + " AND musrenbang.IDProgram =" + pProgram.ToString();
////                        if (pIDKegiatan > 0)
////                        {
////                            SSQL = SSQL + " AND musrenbang.IDKegiatan =" + pIDKegiatan.ToString();


////                        }

////                    }

////                }

////                if (_pStatusSKPD > -1)
////                {

////                    SSQL = SSQL + "AND musrenbang.statusskpd= " + _pStatusSKPD.ToString();

////                }

////                if (_pStatusBapeda > -1)
////                {

////                    SSQL = SSQL + "AND musrenbang.statusbapeda= " + _pStatusBapeda.ToString();

////                }
////                if (_pDewan > 0)
////                {
////                    SSQL = SSQL + " AND levelmusrenbang = 4 and musrenbang.skpd_id= " + _pDewan.ToString();

////                }


////                SSQL = SSQL + " ORDER BY nama";




////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["keterangan"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    TahunAPBD = DataFormat.GetInteger(dr["TahunAPBD"]),
////                                    keteranganlokasi = DataFormat.GetString(dr["lokasi"]),

////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),
////                                    Type = DataFormat.GetInteger(dr["Type"]),
////                                    volskpd = DataFormat.GetDecimal(dr["volskpd"]),
////                                    volbapeda = DataFormat.GetDecimal(dr["volbapeda"]),

////                                    keteranganskpd = DataFormat.GetString(dr["keteranganskpd"]),
////                                    keteranganbapeda = DataFormat.GetString(dr["keteranganbapeda"]),
////                                    namasatuan = DataFormat.GetString(dr["namasatuan"]),
////                                    statusskpd = DataFormat.GetInteger(dr["statusskpd"]),
////                                    statusbapeda = DataFormat.GetInteger(dr["statusbapeda"]),
////                                    namastatusSKPD = DataFormat.GetString(dr["namastatusSKPD"]),
////                                    namastatusBapeda = DataFormat.GetString(dr["namaStatusBapeda"]),
////                                    namaDinas = DataFormat.GetString(dr["sNamaSKPD"]),
////                                    PrioritasDesa = DataFormat.GetInteger(dr["prioritasdesa"]),
////                                    PrioritasKecamatan = DataFormat.GetInteger(dr["prioritaskecamatan"]),
////                                    PrioritasSKPD = DataFormat.GetInteger(dr["prioritasskpd"]),
////                                    PrioritasBapeda = DataFormat.GetInteger(dr["prioritasbapeda"]),
////                                    DewanID = DataFormat.GetInteger(dr["skpd_id"])




////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }

////        //public List<RptMusrenbang> GetReportMusrenbang(int pTahun, int pKecamatan = 0, int pDesa = 0, int pDusun = 0, int _idDInas = 0, int pType = 0, int pUrusan = 0, int pProgram = 0, int pIDKegiatan = 0, int pDewan = -1)
////        //{

////        //    List<RptMusrenbang> mListUnit = new List<RptMusrenbang>();
////        //    try
////        //    {




////        //        SSQL = "SELECT musrenbang.*, mDusun.Nama + ', ' + mDesa.Nama + ' ' + mKecamatan.Nama  as lokasi , satuan.nama as namasatuan , stskpd.Nama as namastatusSKPD, stbapeda.Nama as namaStatusBapeda, mSKPD.sNamaSKPD,mSumberDana.snama as namasumberdana FROM musrenbang " +
////        //                 " left join mDusun  on musrenbang.dusun_id = mDusun.ID and musrenbang.desa_id = mDusun.Desa and musrenbang.kecamatan_id= mDusun.KEcamatan " +
////        //                 " left join mDesa on mDusun.Desa = mDesa.ID and mDesa.Kecamatan = mDusun.Kecamatan AND musrenbang.desa_id= mDesa.ID " +
////        //                " left join mKecamatan on mDesa.Kecamatan = mKecamatan.ID and mDusun.Kecamatan = mKecamatan.ID " +
////        //                 " left join satuan on satuan.id= musrenbang.satuan left Join status as stskpd on stskpd.id= musrenbang.statusskpd " +
////        //                 " left Join status as stbapeda on stbapeda.id= musrenbang.statusbapeda " +
////        //                 " Left Join mSKPD on mSKPD.ID = musrenbang.IDDInas Left Join mSumberDana on msumberdana.ID = musrenbang.sumberdana where  musrenbang.Status<9  ";
////        //        //if (pType>0){ stskpd.Nama as namastatusSKPD, stbapeda.Nama as namaStatusBapeda
////        //        //    SSQL = SSQL+ "  AND Type=  " + pType.ToString() ;
////        //        //}
////        //        if (pTahun > 0)
////        //            SSQL = SSQL + "  AND iTahun=  " + pTahun.ToString().ToString();

////        //        if (pKecamatan > 0)
////        //        {
////        //            SSQL = SSQL + " AND musrenbang.kecamatan_ID= " + pKecamatan.ToString();
////        //            if (pDesa > 0)
////        //            {
////        //                SSQL = SSQL + " AND musrenbang.desa_ID= " + pDesa.ToString();
////        //                if (pDusun > 0)
////        //                    SSQL = SSQL + " AND musrenbang.dusun_ID= " + pDusun.ToString();

////        //            }
////        //        }
////        //        if (_idDInas > 0)

////        //            SSQL = SSQL + " AND IDDInas =" + _idDInas.ToString();

////        //        if (pUrusan > 0)
////        //        {
////        //            SSQL = SSQL + " AND musrenbang.IDUrusan  =" + pUrusan.ToString();
////        //            if (pProgram > 0)
////        //            {
////        //                SSQL = SSQL + " AND musrenbang.IDProgram =" + pProgram.ToString();
////        //                if (pIDKegiatan > 0)
////        //                {
////        //                    SSQL = SSQL + " AND musrenbang.IDKegiatan =" + pIDKegiatan.ToString();


////        //                }

////        //            }

////        //        }
////        //        if (pDewan > -1)
////        //        {
////        //            SSQL = SSQL + " AND levelmusrenbang = 4 and musrenbang.skpd_id= " + pDewan.ToString();


////        //        }
////        //        SSQL = SSQL + " ORDER BY nama";




////        //        DataTable dt = new DataTable();
////        //        dt = _dbHelper.ExecuteDataTable(SSQL);
////        //        if (dt != null)
////        //        {
////        //            if (dt.Rows.Count > 0)
////        //            {
////        //                mListUnit = (from DataRow dr in dt.Rows
////        //                        select new RptMusrenbang()
////        //                        {
////        //                            Pagu = DataFormat.GetDecimal(dr["pagu"]).ToRupiahInReport(),
////        //                            PaguDec = DataFormat.GetDecimal(dr["pagu"]),
////        //                            Usulan = DataFormat.GetString(dr["nama"]),
////        //                            Volume = DataFormat.GetDecimal(dr["volume"]).ToRupiahInReportNoSen(),

////        //                            Satuan = DataFormat.GetString(dr["NamaSatuan"]),
////        //                            Dinas = DataFormat.GetString(dr["sNamaSKPD"]),
////        //                            Desa = DataFormat.GetString(dr["lokasi"]),
////        //                            SumberDana = DataFormat.GetString(dr["namasumberdana"]),
////        //                            PaguBappeda = DataFormat.GetDecimal(dr["pagubapeda"]).ToRupiahInReport(),
////        //                            PaguSKPD = DataFormat.GetDecimal(dr["paguskpd"]).ToRupiahInReport(),
////        //                            PaguBappedaDec = DataFormat.GetDecimal(dr["pagubapeda"]),
////        //                            PaguSKPDDec = DataFormat.GetDecimal(dr["paguskpd"]),
////        //                            StatusBappeda = DataFormat.GetString(dr["namastatusbapeda"]),
////        //                            StatusSKPD = DataFormat.GetString(dr["namastatusskpd"]),
////        //                            //'PrioritasDesa = DataFormat.GetInteger(dr["prioritasdesa"]),
////        //                            Prioritas = DataFormat.GetInteger(dr["prioritaskecamatan"]).ToString("##"),
////        //                            //PrioritasSKPD = DataFormat.GetInteger(dr["prioritasskpd"]),
////        //                            //PrioritasBapeda = DataFormat.GetInteger(dr["prioritasbapeda"]),




////        //                        }).ToList();
////        //            }
////        //        }
////        //        return mListUnit;
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        _isError = true;
////        //        _lastError = ex.Message;
////        //        return mListUnit;
////        //    }
////        //}


////        public List<Musrenmbang> GetByIDProgram(int _pID)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {
////                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERe IDProgram =" + _pID.ToString() + " ORDER BY IDKegiatan,IDlokasi";
////                DataTable dt = new DataTable();
////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["kecamatan"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),
////                                    Type = DataFormat.GetInteger(dr["Type"]),
////                                    DewanID = DataFormat.GetInteger(dr["skpd_id"])
////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }

////        public List<Musrenmbang> GetByIDProgramIDKegiatanFromRenja(int _pIDProgram, int _pIDKegiatan)
////        {

////            List<Musrenmbang> mListUnit = new List<Musrenmbang>();
////            try
////            {
////                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERe IDProgram =" + _pIDProgram.ToString() + " ORDER BY IDKegiatan,IDlokasi";

////                DataTable dt = new DataTable();

////                dt = _dbHelper.ExecuteDataTable(SSQL);
////                if (dt != null)
////                {
////                    if (dt.Rows.Count > 0)
////                    {
////                        mListUnit = (from DataRow dr in dt.Rows
////                                select new Musrenmbang()
////                                {
////                                    id = DataFormat.GetInteger(dr["id"]),
////                                    desa_id = DataFormat.GetInteger(dr["desa_id"]),
////                                    dusun_id = DataFormat.GetInteger(dr["dusun_id"]),
////                                    kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]),
////                                    skpd_id = DataFormat.GetInteger(dr["skpd_id"]),
////                                    program_id = DataFormat.GetInteger(dr["program_id"]),
////                                    kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]),
////                                    prioritas = DataFormat.GetInteger(dr["prioritas"]),
////                                    nama = DataFormat.GetString(dr["nama"]),
////                                    volume = DataFormat.GetDecimal(dr["volume"]),
////                                    satuan = DataFormat.GetInteger(dr["satuan"]),
////                                    pagu = DataFormat.GetDecimal(dr["pagu"]),
////                                    keterangan = DataFormat.GetString(dr["kecamatan"]),
////                                    iTahun = DataFormat.GetInteger(dr["iTahun"]),
////                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
////                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
////                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
////                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
////                                    Pagu2 = DataFormat.GetDecimal(dr["pagu2"]),
////                                    pagubapeda = DataFormat.GetDecimal(dr["pagubapeda"]),
////                                    paguSKPD = DataFormat.GetDecimal(dr["paguSKPD"]),
////                                    outcome = DataFormat.GetString(dr["outcome"]),
////                                    keteranganRTRW = DataFormat.GetString(dr["keteranganRTRW"]),
////                                    latarbelakang = DataFormat.GetString(dr["latarbelakang"]),
////                                    sumberDana = DataFormat.GetInteger(dr["sumberDana"]),
////                                    idcrt = DataFormat.GetInteger(dr["idcrt"]),
////                                    dtcrt = DataFormat.GetDateTime(dr["dtcrt"]),
////                                    dtverifikasi = DataFormat.GetDateTime(dr["dtverifikasi"]),
////                                    idverifikasi = DataFormat.GetInteger(dr["idverifikasi"]),
////                                    levelmusrenbang = DataFormat.GetInteger(dr["levelmusrenbang"]),
////                                    Type = DataFormat.GetInteger(dr["Type"]),
////                                    DewanID = DataFormat.GetInteger(dr["skpd_id"])
////                                }).ToList();
////                    }
////                }
////                return mListUnit;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message;
////                return mListUnit;
////            }
////        }


////        //public bool Simpan(List<Musrenmbang> mListUnit)
////        //{
////        //    //bool lRet = true;
////        //    //Hapus(Tahun);

////        //    //foreach (Musrenmbang m in mListUnit)
////        //    //{
////        //    //    lRet =  Simpan(m);

////        //    //}
////        //    //return lRet;


////        //}

////        public bool Simpan(Musrenmbang _pMusrenmbang)
////        {
////            try
////            {
////                int _tanggal;
////                if (_pMusrenmbang.id == 0) { }
////                _tanggal = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Month, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Day, 2));




////                SSQL = " INSERT INTO musrenbang (id ,Type,desa_id ,dusun_id ,kecamatan_id ,skpd_id ,program_id , " +
////                            " kegiatan_id ,prioritas ,nama ,volume , " +
////                           " satuan ,pagu ,keterangan ,iTahun,IDUrusan ,IDDInas ,IDProgram ,IDKegiatan,Pagu2, TahunAPBD,TahapAPBD ," +
////                        "pagubapeda ,outcome ,keteranganRTRW ,latarbelakang ,sumberDana ,idcrt ,dtcrt ,dtverifikasi ,idverifikasi ,levelmusrenbang,paguSKPD,dtDelete,dtUpdate,idUpdate,idDelete )  VALUES (" +
////                           "@pid ,@pType,@pdesa_id ,@pdusun_id ,@pkecamatan_id ,@pskpd_id ,@pprogram_id , " +
////                            " @pkegiatan_id ,@pprioritas ,@pnama ,@pvolume , " +
////                           " @psatuan ,@ppagu ,@pketerangan ,@piTahun,@pIDUrusan ,@pIDDInas ,@pIDProgram ,@pIDKegiatan,@pPagu2, @pTahunAPBD,@pTahapAPBD," +
////                        "@ppagubapeda ,@poutcome ,@pketeranganRTRW ,@platarbelakang ,@psumberDana ,@pidcrt ,@pdtcrt ,@pdtverifikasi ,@pidverifikasi ,@plevelmusrenbang,@ppaguSKPD, " + DEFAULTDATEDELETE.ToSQLFormat() + " ," + DEFAULTDATEUPDATE.ToSQLFormat() + ",0,0 )";

////                DBParameterCollection paramCollection = new DBParameterCollection();
////                paramCollection.Add(new DBParameter("@pid", _pMusrenmbang.id));
////                paramCollection.Add(new DBParameter("@pType", _pMusrenmbang.Type));
////                paramCollection.Add(new DBParameter("@pdesa_id", _pMusrenmbang.desa_id));
////                paramCollection.Add(new DBParameter("@pdusun_id", _pMusrenmbang.dusun_id));
////                paramCollection.Add(new DBParameter("@pkecamatan_id ", _pMusrenmbang.kecamatan_id));
////                paramCollection.Add(new DBParameter("@pskpd_id", _pMusrenmbang.skpd_id));
////                paramCollection.Add(new DBParameter("@pprogram_id", _pMusrenmbang.program_id));
////                paramCollection.Add(new DBParameter("@pkegiatan_id", _pMusrenmbang.kecamatan_id));
////                paramCollection.Add(new DBParameter("@pprioritas", _pMusrenmbang.prioritas));
////                paramCollection.Add(new DBParameter("@pnama", _pMusrenmbang.nama));
////                paramCollection.Add(new DBParameter("@pvolume ", _pMusrenmbang.volume));
////                paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan));
////                paramCollection.Add(new DBParameter("@ppagu", _pMusrenmbang.pagu));
////                paramCollection.Add(new DBParameter("@pketerangan", _pMusrenmbang.keterangan));
////                paramCollection.Add(new DBParameter("@piTahun", _pMusrenmbang.iTahun));
////                paramCollection.Add(new DBParameter("@pIDUrusan", _pMusrenmbang.IDUrusan));
////                paramCollection.Add(new DBParameter("@pIDDInas", _pMusrenmbang.IDDInas));
////                paramCollection.Add(new DBParameter("@pIDProgram", _pMusrenmbang.IDProgram));
////                paramCollection.Add(new DBParameter("@pIDKegiatan", _pMusrenmbang.IDKegiatan));
////                paramCollection.Add(new DBParameter("@pPagu2", _pMusrenmbang.Pagu2));
////                paramCollection.Add(new DBParameter("@pTahunAPBD", _pMusrenmbang.TahunAPBD));
////                paramCollection.Add(new DBParameter("@pTahapAPBD", _pMusrenmbang.TahapAPBD));
////                paramCollection.Add(new DBParameter("@ppagubapeda", _pMusrenmbang.pagubapeda));
////                paramCollection.Add(new DBParameter("@poutcome", _pMusrenmbang.outcome));
////                paramCollection.Add(new DBParameter("@pketeranganRTRW", _pMusrenmbang.keteranganRTRW));
////                paramCollection.Add(new DBParameter("@platarbelakang", _pMusrenmbang.latarbelakang));
////                paramCollection.Add(new DBParameter("@psumberDana", _pMusrenmbang.sumberDana));
////                paramCollection.Add(new DBParameter("@pidcrt", _pMusrenmbang.idcrt));
////                paramCollection.Add(new DBParameter("@pdtcrt", _pMusrenmbang.dtcrt));
////                paramCollection.Add(new DBParameter("@pdtverifikasi", _pMusrenmbang.dtverifikasi));
////                paramCollection.Add(new DBParameter("@pidverifikasi", _pMusrenmbang.idverifikasi));
////                paramCollection.Add(new DBParameter("@plevelmusrenbang", _pMusrenmbang.levelmusrenbang));
////                paramCollection.Add(new DBParameter("@ppaguSKPD", _pMusrenmbang.paguSKPD));


////                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
////                {
////                    return true;
////                }
////                else
////                {
////                    return false;
////                }

////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }
////        public int SimpanEx(Musrenmbang _pMusrenmbang)
////        {
////            try
////            {
////                int _tanggal;
////                if (_pMusrenmbang.id == 0)
////                {
////                    int newID = GetMaxID();
////                    if (newID < 1)
////                    {
////                        return 0;
////                    }
////                    _tanggal = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Month, 2) + DataFormat.IntToStringWithLeftPad(DateTime.Now.Day, 2));

////                    SSQL = " INSERT INTO musrenbang (id ,desa_id ,dusun_id ,kecamatan_id ,skpd_id ,program_id , " +
////                            " kegiatan_id ,prioritas ,nama ,volume ,sumberdana , " +
////                           " satuan ,pagu ,keterangan ,iTahun,IDUrusan ,IDDInas ,IDProgram ,IDKegiatan,Pagu2, TahunAPBD,TahapAPBD,idUrusanMaster, idProgramMaster,idKegiatanMaster,Type,volSKPD, paguskpd, keteranganskpd, statusskpd,volbapeda, pagubapeda ,keteranganbapeda, statusbapeda,levelmusrenbang,prioritasdesa, prioritaskecamatan, prioritasskpd, prioritasbapeda,dtDelete,dtUpdate,idUpdate,idDelete, status )  VALUES (" +
////                           "@pid ,@pdesa_id ,@pdusun_id ,@pkecamatan_id ,@pskpd_id ,@pprogram_id , " +
////                            " @pkegiatan_id ,@pprioritas ,@pnama ,@pvolume , @pSumberdana," +
////                           " @psatuan ,@ppagu ,@pketerangan ,@piTahun,@pIDUrusan ,@pIDDInas ,@pIDProgram ,@pIDKegiatan,@pPagu2, @pTahunAPBD,@pTahapAPBD ,@pidUrusanMaster, @pidProgramMaster,@pidKegiatanMaster,@pType,@pvolskpd, @ppaguskpd, @pketeranganskpd,@pstatusskpd,@pvolbapeda, @ppagubapeda, @pketeranganbapeda, @pstatusbapeda,@plevelmusrenbang ,@pprioritasdesa, @pprioritaskecamatan, @pprioritasskpd, @pprioritasbapeda, " + DEFAULTDATEDELETE.ToSQLFormat() + " ," + DEFAULTDATEUPDATE.ToSQLFormat() + ",0,0, 0 )";

////                    DBParameterCollection paramCollection = new DBParameterCollection();
////                    paramCollection.Add(new DBParameter("@pid", newID));
////                    paramCollection.Add(new DBParameter("@pdesa_id", _pMusrenmbang.desa_id));
////                    paramCollection.Add(new DBParameter("@pdusun_id", _pMusrenmbang.dusun_id));
////                    paramCollection.Add(new DBParameter("@pkecamatan_id ", _pMusrenmbang.kecamatan_id));
////                    paramCollection.Add(new DBParameter("@pskpd_id", _pMusrenmbang.DewanID));
////                    paramCollection.Add(new DBParameter("@pprogram_id", _pMusrenmbang.program_id));
////                    paramCollection.Add(new DBParameter("@pkegiatan_id", _pMusrenmbang.kecamatan_id));
////                    paramCollection.Add(new DBParameter("@pprioritas", _pMusrenmbang.prioritas));
////                    paramCollection.Add(new DBParameter("@pnama", _pMusrenmbang.nama));
////                    paramCollection.Add(new DBParameter("@pvolume", _pMusrenmbang.volume));
////                    paramCollection.Add(new DBParameter("@pSumberdana", _pMusrenmbang.sumberDana));
////                    paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan));
////                    paramCollection.Add(new DBParameter("@ppagu", _pMusrenmbang.pagu));
////                    paramCollection.Add(new DBParameter("@pketerangan", _pMusrenmbang.keterangan));
////                    paramCollection.Add(new DBParameter("@piTahun", _pMusrenmbang.iTahun));
////                    paramCollection.Add(new DBParameter("@pIDUrusan", _pMusrenmbang.IDUrusan));
////                    paramCollection.Add(new DBParameter("@pIDDInas", _pMusrenmbang.IDDInas));
////                    paramCollection.Add(new DBParameter("@pIDProgram", _pMusrenmbang.IDProgram));
////                    paramCollection.Add(new DBParameter("@pIDKegiatan", _pMusrenmbang.IDKegiatan));
////                    paramCollection.Add(new DBParameter("@pPagu2", _pMusrenmbang.Pagu2));
////                    paramCollection.Add(new DBParameter("@pTahunAPBD", _pMusrenmbang.TahunAPBD));
////                    paramCollection.Add(new DBParameter("@pTahapAPBD", _pMusrenmbang.TahapAPBD));
////                    paramCollection.Add(new DBParameter("@pidUrusanMaster", _pMusrenmbang.IDUrusanMaster));
////                    paramCollection.Add(new DBParameter("@pidProgramMaster", _pMusrenmbang.IDProgramMaster));
////                    paramCollection.Add(new DBParameter("@pidKegiatanMaster", _pMusrenmbang.IDKegiatanMaster));
////                    paramCollection.Add(new DBParameter("@pType", _pMusrenmbang.Type));
////                    paramCollection.Add(new DBParameter("@pvolskpd", _pMusrenmbang.volskpd));
////                    paramCollection.Add(new DBParameter("@ppaguskpd", _pMusrenmbang.paguSKPD));
////                    paramCollection.Add(new DBParameter("@pketeranganskpd ", _pMusrenmbang.keteranganskpd));
////                    paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusskpd));
////                    paramCollection.Add(new DBParameter("@pvolbapeda", _pMusrenmbang.volbapeda));
////                    paramCollection.Add(new DBParameter("@ppagubapeda", _pMusrenmbang.pagubapeda));
////                    paramCollection.Add(new DBParameter("@pketeranganbapeda", _pMusrenmbang.keteranganbapeda));
////                    paramCollection.Add(new DBParameter("@pstatusbapeda", _pMusrenmbang.statusbapeda));
////                    paramCollection.Add(new DBParameter("@plevelmusrenbang", _pMusrenmbang.levelmusrenbang));
////                    paramCollection.Add(new DBParameter("@pprioritasdesa", _pMusrenmbang.PrioritasDesa));
////                    paramCollection.Add(new DBParameter("@pprioritaskecamatan", _pMusrenmbang.PrioritasKecamatan));
////                    paramCollection.Add(new DBParameter("@pprioritasskpd", _pMusrenmbang.PrioritasSKPD));
////                    paramCollection.Add(new DBParameter("@pprioritasbapeda", _pMusrenmbang.PrioritasBapeda));





////                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
////                    {
////                        return newID;
////                    }
////                    else
////                    {
////                        return 0;
////                    }
////                }
////                else
////                {
////                    Update(_pMusrenmbang);
////                    return _pMusrenmbang.id;
////                }

////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return 0;
////            }
////        }

////        public bool Update(Musrenmbang _pMusrenmbang)
////        {
////            try
////            {

////                SSQL = "UPDATE  musrenbang SET desa_id=@pdesa_id ,dusun_id=@pdusun_id ,kecamatan_id=@pkecamatan_id " +
////                          ", nama =@pnama,  satuan=@psatuan ,pagu =@ppagu,pagu2 =@ppagu ,keterangan=@pketerangan,IDurusan= @pIDurusan, IDProgram =@pIDProgram, IDKegiatan=@pIDKegiatan, " +
////                          "IDurusanMaster= @pIDurusanMaster, IDProgramMaster =@pIDProgramMaster, IDKegiatanMaster=@pIDKegiatanMaster  " +
////                         ",volume=@pvolume,pagubapeda=@ppagubapeda ,outcome=@poutcome ,keteranganRTRW=@pketeranganRTRW ,latarbelakang=@platarbelakang,sumberDana=@psumberDana,volSKPD=@pvolskpd, paguskpd=@ppaguskpd, keteranganskpd=@pketeranganskpd,statusskpd=@pstatusskpd, " +
////                          " volbapeda=@pvolbapeda, keteranganbapeda=@pketeranganbapeda, statusbapeda=@pstatusbapeda, IDDInas =@pSKPD,prioritasdesa=@pprioritasdesa, prioritaskecamatan=@pprioritaskecamatan, prioritasskpd=@pprioritasskpd, prioritasbapeda=@pprioritasbapeda,dtUpdate=@pdtUpdate,idUpdate=@pIDUpdate " +
////                           " ,idverifikasi=@pidverifikasi,dtverifikasi=@dtVerifikasi,idVerifikasiBappeda =@pidVerifikasiBappeda,dtVerifikasiBappeda =@pdtVerifikasiBappeda  where id = @pid ";

////                DBParameterCollection paramCollection = new DBParameterCollection();

////                paramCollection.Add(new DBParameter("@pdesa_id", _pMusrenmbang.desa_id, DbType.Int32));
////                paramCollection.Add(new DBParameter("@pdusun_id", _pMusrenmbang.dusun_id, DbType.Int32));
////                paramCollection.Add(new DBParameter("@pkecamatan_id ", _pMusrenmbang.kecamatan_id, DbType.Int32));
////                paramCollection.Add(new DBParameter("@pnama", _pMusrenmbang.nama, DbType.String));
////                paramCollection.Add(new DBParameter("@pvolume ", _pMusrenmbang.volume, DbType.Decimal));
////                paramCollection.Add(new DBParameter("@psatuan", _pMusrenmbang.satuan, DbType.Int32));
////                paramCollection.Add(new DBParameter("@ppagu", _pMusrenmbang.pagu, DbType.Decimal));
////                paramCollection.Add(new DBParameter("@pketerangan", _pMusrenmbang.keterangan, DbType.String));
////                paramCollection.Add(new DBParameter("@pIDurusan", _pMusrenmbang.IDUrusan, DbType.Int32));
////                paramCollection.Add(new DBParameter("@pIDProgram", _pMusrenmbang.IDProgram, DbType.Int32));
////                paramCollection.Add(new DBParameter("@pIDKegiatan", _pMusrenmbang.IDKegiatan, DbType.Int64));
////                paramCollection.Add(new DBParameter("@pIDurusanMaster", _pMusrenmbang.IDUrusanMaster, DbType.Int64));
////                paramCollection.Add(new DBParameter("@pIDProgramMaster", _pMusrenmbang.IDProgramMaster, DbType.Int64));
////                paramCollection.Add(new DBParameter("@pIDKegiatanMaster", _pMusrenmbang.IDKegiatanMaster, DbType.Int64));



////                paramCollection.Add(new DBParameter("@ppagubapeda", _pMusrenmbang.pagubapeda, DbType.Decimal));
////                paramCollection.Add(new DBParameter("@poutcome", _pMusrenmbang.outcome == null ? "" : _pMusrenmbang.outcome));
////                paramCollection.Add(new DBParameter("@pketeranganRTRW", _pMusrenmbang.keteranganRTRW == null ? "" : _pMusrenmbang.keteranganRTRW));
////                paramCollection.Add(new DBParameter("@platarbelakang", _pMusrenmbang.latarbelakang == null ? "" : _pMusrenmbang.latarbelakang));
////                paramCollection.Add(new DBParameter("@psumberDana", _pMusrenmbang.sumberDana, DbType.Int32));
////                paramCollection.Add(new DBParameter("@pvolskpd", _pMusrenmbang.volskpd, DbType.Decimal));
////                paramCollection.Add(new DBParameter("@ppaguskpd", _pMusrenmbang.paguSKPD, DbType.Decimal));
////                paramCollection.Add(new DBParameter("@pketeranganskpd ", _pMusrenmbang.keteranganskpd == null ? "" : _pMusrenmbang.keteranganskpd));
////                paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusskpd));
////                paramCollection.Add(new DBParameter("@pvolbapeda", _pMusrenmbang.volbapeda));
////                paramCollection.Add(new DBParameter("@pketeranganbapeda", _pMusrenmbang.keteranganbapeda));
////                paramCollection.Add(new DBParameter("@pstatusbapeda", _pMusrenmbang.statusbapeda));
////                paramCollection.Add(new DBParameter("@pSKPD", _pMusrenmbang.IDDInas));
////                paramCollection.Add(new DBParameter("@pprioritasdesa", _pMusrenmbang.PrioritasDesa));
////                paramCollection.Add(new DBParameter("@pprioritaskecamatan", _pMusrenmbang.PrioritasKecamatan));
////                paramCollection.Add(new DBParameter("@pprioritasskpd", _pMusrenmbang.PrioritasSKPD));
////                paramCollection.Add(new DBParameter("@pprioritasbapeda", _pMusrenmbang.PrioritasBapeda));
////                paramCollection.Add(new DBParameter("@pdtUpdate", DateTime.Now.Date));
////                paramCollection.Add(new DBParameter("@pIDUpdate", _pMusrenmbang.idcrt));
////                paramCollection.Add(new DBParameter("@pidverifikasi", _pMusrenmbang.idverifikasi));
////                paramCollection.Add(new DBParameter("@dtVerifikasi", _pMusrenmbang.dtverifikasi));
////                paramCollection.Add(new DBParameter("@pidVerifikasiBappeda", _pMusrenmbang.idverifikasibapeda));
////                paramCollection.Add(new DBParameter("@pdtVerifikasiBappeda", _pMusrenmbang.dtverifikasibapeda));


////                //paramCollection.Add(new DBParameter("@pidverifikasi", _pMusrenmbang.idverifikasi));
////                //paramCollection.Add(new DBParameter("@plevelmusrenbang", _pMusrenmbang.levelmusrenbang));
////                //paramCollection.Add(new DBParameter("@pType", _pMusrenmbang.Type));

////                //paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusskpd));
////                paramCollection.Add(new DBParameter("@pid", _pMusrenmbang.id));

////                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
////                {
////                    return true;
////                }
////                else
////                {
////                    return false;
////                }

////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }
////        public bool VerifikasiSKPD(Musrenmbang _pMusrenmbang)
////        {
////            try
////            {

////                SSQL = "UPDATE  musrenbang SET volSKPD= @pvolskpd, paguskpd=@ppaguskpd, keteranganskpd=@pketeranganskpd, statusskpd=@pstatusskpd ,idverifikasi=@pidverifikasi,dtverifikasi=@dtVerifikasi  where id = @pid ";

////                DBParameterCollection paramCollection = new DBParameterCollection();

////                paramCollection.Add(new DBParameter("@pvolskpd", _pMusrenmbang.volskpd));
////                paramCollection.Add(new DBParameter("@ppaguskpd", _pMusrenmbang.paguSKPD));
////                paramCollection.Add(new DBParameter("@pketeranganskpd ", _pMusrenmbang.keteranganskpd));
////                paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusskpd));
////                //paramCollection.Add(new DBParameter("@pdtverifikasi ", _pMusrenmbang.dtverifikasi));
////                paramCollection.Add(new DBParameter("@pidverifikasi", _pMusrenmbang.idverifikasi));
////                paramCollection.Add(new DBParameter("@dtVerifikasi", DateTime.Now.Date));

////                paramCollection.Add(new DBParameter("@pid", _pMusrenmbang.id));


////                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
////                {
////                    return true;
////                }
////                else
////                {
////                    return false;
////                }

////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }

////        public bool VerifikasiBapeda(Musrenmbang _pMusrenmbang)
////        {
////            try
////            {//,idverifikasibapeda=@pidverifikasi 

////                SSQL = "UPDATE  musrenbang SET volbapeda= @pvol, pagubapeda=@ppaguskpd, keteranganbapeda=@pketeranganskpd, statusbapeda=@pstatusskpd ,idVerifikasiBappeda =@pidVerifikasiBappeda,dtVerifikasiBappeda =@pdtVerifikasiBappeda where id = @pid ";

////                DBParameterCollection paramCollection = new DBParameterCollection();

////                paramCollection.Add(new DBParameter("@pvol", _pMusrenmbang.volbapeda));
////                paramCollection.Add(new DBParameter("@ppaguskpd", _pMusrenmbang.pagubapeda));
////                paramCollection.Add(new DBParameter("@pketeranganskpd ", _pMusrenmbang.keteranganbapeda));
////                paramCollection.Add(new DBParameter("@pstatusskpd", _pMusrenmbang.statusbapeda));
////                paramCollection.Add(new DBParameter("@pidVerifikasiBappeda", _pMusrenmbang.idcrt));
////                paramCollection.Add(new DBParameter("@pdtVerifikasiBappeda", DateTime.Now.Date));



////                //paramCollection.Add(new DBParameter("@pidverifikasi", _pMusrenmbang.idverifikasibapeda));
////                paramCollection.Add(new DBParameter("@pid", _pMusrenmbang.id));

////                if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
////                {
////                    return true;
////                }
////                else
////                {
////                    return false;
////                }

////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }

////        public bool UpdateKegiatan(List<Musrenmbang> mListUnit)
////        {
////            try
////            {
////                foreach (Musrenmbang m in mListUnit)
////                {

////                    SSQL = " update musrenbang SET iTahun=@piTahun,IDUrusan=@pIDUrusan  ,IDDInas=@pIDDInas ,IDProgram =@pIDProgram,IDKegiatan =@pIDKegiatan, pagu2= @ppagu2,tahunAPBD=@tahunAPBD, tahapAPBD=@tahapAPBD,IDProgramMaster =@pIDProgramMaster,IDKegiatanMaster =@pIDKegiatanMaster, IDUrusanMaster= @pIDUrusanMaster  where id = @pid";

////                    DBParameterCollection paramCollection = new DBParameterCollection();
////                    paramCollection.Add(new DBParameter("@piTahun", m.iTahun));
////                    paramCollection.Add(new DBParameter("@pIDUrusan", m.IDUrusan));
////                    paramCollection.Add(new DBParameter("@pIDDInas", m.IDDInas));
////                    paramCollection.Add(new DBParameter("@pIDProgram", m.IDProgram));
////                    paramCollection.Add(new DBParameter("@pIDKegiatan", m.IDKegiatan));
////                    paramCollection.Add(new DBParameter("@ppagu2", m.Pagu2));
////                    paramCollection.Add(new DBParameter("@tahunAPBD", m.TahunAPBD));
////                    paramCollection.Add(new DBParameter("@tahapAPBD", m.TahapAPBD));
////                    paramCollection.Add(new DBParameter("@pIDProgramMaster", m.IDProgramMaster));
////                    paramCollection.Add(new DBParameter("@pIDKegiatanMaster", m.IDKegiatanMaster));
////                    paramCollection.Add(new DBParameter("@pIDUrusanMaster", m.IDUrusanMaster));



////                    paramCollection.Add(new DBParameter("@pid", m.id));


////                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
////                }
////                return true;


////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }

////        public bool LepasKegiatan(List<Musrenmbang> mListUnit)
////        {
////            try
////            {
////                foreach (Musrenmbang m in mListUnit)
////                {
////                    LepasMusrenbangKegiatan(m);

////                }
////                return true;


////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }
////        public bool LepasMusrenbangKegiatan(Musrenmbang _mrb)
////        {
////            try
////            {

////                SSQL = " update musrenbang SET iTahun=0,IDUrusan=0,IDProgram =0,IDKegiatan =0, pagu2= 0,tahunAPBD=0, tahapAPBD=0, IDkegiatanmaster=0, idprogrammaster=0, idurusanmaster=0  where id = @pid";

////                DBParameterCollection paramCollection = new DBParameterCollection();
////                paramCollection.Add(new DBParameter("@pid", _mrb.id));
////                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
////                return true;


////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;
////            }
////        }
////        public void SimpanMaterProgram(int _iTahun)
////        {
////            SSQL = "Select distinct  * INTO tempMasterProgram FROM mprogrammusrenmbang WHERE iTahun = " + _iTahun.ToString();
////            _dbHelper.ExecuteNonQuery(SSQL);
////            SSQL = "DELETE FROM mprogrammusrenmbang WHERE iTahun =" + _iTahun.ToString();
////            _dbHelper.ExecuteNonQuery(SSQL);

////            SSQL = "INSERT into mprogrammusrenmbang SELECT * FROM tempMasterProgram ";
////            _dbHelper.ExecuteNonQuery(SSQL);

////            SSQL = "DROP TABLE tempMasterProgram ";
////            _dbHelper.ExecuteNonQuery(SSQL);

////            //  _dbHelper.ExecuteNonQuery(SSQL);

////        }
////        public void SimpanMaterKegiatan(int _iTahun)
////        {
////            SSQL = "Select distinct * INTO tempMasterKeg FROM mKegiatanMusrenmbang WHERE iTahun = " + _iTahun.ToString();
////            _dbHelper.ExecuteNonQuery(SSQL);
////            SSQL = "DELETE FROM mKegiatanMusrenmbang WHERE iTahun = " + _iTahun.ToString();
////            _dbHelper.ExecuteNonQuery(SSQL);

////            SSQL = "INSERT into mKegiatanMusrenmbang SELECT * FROM tempMasterKeg ";
////            _dbHelper.ExecuteNonQuery(SSQL);

////            SSQL = "DROP TABLE tempMasterKeg ";
////            _dbHelper.ExecuteNonQuery(SSQL);




////        }
////        //public bool Hapus(int _tahun)
////        //{
////        //    try
////        //    {

////        //        SSQL = "DELETE FROM musrenbang WHERE iTahun =" + _tahun.ToString();
////        //        _dbHelper.ExecuteNonQuery(SSQL);
////        //       // {
////        //            //SSQL = "DELETE FROM mprogrammusrenmbang WHERE iTahun =" + _tahun.ToString();
////        //            //_dbHelper.ExecuteNonQuery(SSQL);
////        //            //SSQL = "DELETE FROM mKegiatanMusrenmbang WHERE iTahun =" + _tahun.ToString();
////        //            //_dbHelper.ExecuteNonQuery(SSQL);
////        //            return true;
////        //        //}
////        //        //else
////        //        //{
////        //        //    return false;
////        //       // }
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        _isError = true;
////        //        _lastError = ex.Message + " " + SSQL;
////        //        return false;

////        //    }

////        //}
////        public bool Hapus(int id, int idHapus)
////        {
////            try
////            {

////                SSQL = "Update  musrenbang set status = 9 ,dtDelete= " + DateTime.Now.Date.ToSQLFormat() + ", idDelete=" + idHapus.ToString() + " WHERE id =" + id.ToString();
////                _dbHelper.ExecuteNonQuery(SSQL);
////                return true;
////            }
////            catch (Exception ex)
////            {
////                _isError = true;
////                _lastError = ex.Message + " " + SSQL;
////                return false;

////            }

////        }
////        public bool ExportToExcell(int idDInas, string ExcelFilePath)
////        {
////            try
////            {

////                //  SSQL = "Select * from Musrenbang where IDDInas =" + idDInas.ToString();


////                Excel.Application excelApp = new Excel.Application();
////                excelApp.Workbooks.Add();

////                // single worksheet
////                Excel._Worksheet workSheet = excelApp.ActiveSheet;

////                SSQL = "select mprogram.id, mprogram.sNamaProgram, " +
////                    " mKegiatan.ID ,mKegiatan.sNamaKegiatan , musrenbang.id, musrenbang.nama ,musrenbang.volume,musrenbang.satuan, musrenbang.pagu2 " +
////                    " from musrenbang  INNER JOIN mPRogram on musrenbang.idProgramMaster = mProgram.ID   inner join mKegiatan  " +
////                    " on mKegiatan.id = musrenbang.idkegiatanmaster  and  musrenbang.idprogramMaster = mKegiatan.idProgram  where musrenbang.IDDInas =" + idDInas.ToString() + " AND  idKegiatanMaster >0 ";



////                DataTable Tbl = new DataTable();
////                Tbl = _dbHelper.ExecuteDataTable(SSQL);



////                //// column headings
////                for (int i = 0; i < Tbl.Columns.Count; i++)
////                {
////                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
////                }

////                // rows
////                for (int i = 0; i < Tbl.Rows.Count; i++)
////                {
////                    // to do: format datetime values before printing
////                    for (int j = 0; j < Tbl.Columns.Count; j++)
////                    {
////                        workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
////                    }
////                }

////                // check fielpath
////                if (ExcelFilePath != null && ExcelFilePath != "")
////                {
////                    try
////                    {
////                        workSheet.SaveAs(ExcelFilePath);
////                        excelApp.Quit();

////                    }
////                    catch (Exception ex)
////                    {
////                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
////                            + ex.Message);
////                    }
////                }
////                else    // no filepath is given
////                {
////                    excelApp.Visible = true;
////                }
////                return true;
////            }
////            catch (Exception ex)
////            {
////                throw new Exception("ExportToExcel: \n" + ex.Message);
////                return false;

////            }


////        }
////    }
////}
