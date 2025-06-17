using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using DataAccess;
using System.Data;
using BP;
using Formatting;


namespace BP
{
    public class SPDLogic:BP
    {
        IDbConnection m_connection;
        IDbTransaction m_objTrans;

        public SPDLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;

        }
        public List<DisplaySPD> GetDisplaySPD(int iddinas,long mourut){

           List<DisplaySPD> _lst = new List<DisplaySPD>();
           try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                if (mourut > 0)
                {
                    SSQL = "SELECT tSPD.btJenis, tSPDKegiatan.IDDInas,tSPDKegiatan.btKodeUK, tSPDKegiatan.IDurusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan, tSPDKegiatan.IDSUBKegiatan, tSPDKegiatan.IIDRekening , " +
                          " SUM(case when inourut < @NoURUT THEN tSPDKegiatan.cJumlah ELSE 0 END) AS JumlahLalu,  " +
                          " SUM(case when inourut = @NoURUT THEN tSPDKegiatan.cJumlah ELSE 0 END) AS JumlahKini  " +
                           " from tSPD inner join tSPDKegiatan on tSPD.inourut = tSPDKegiatan.inourut WHERE tSPDKegiatan.IDDInas =@IDDINAS  " +
                           " GROUP BY tSPD.btJenis,tSPDKegiatan.IDDInas,tSPDKegiatan.btKodeUK,tSPDKegiatan.IDurusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan, tSPDKegiatan.IDSUBKegiatan, tSPDKegiatan.IIDRekening   " +
                           " ORDER BY tSPDKegiatan.IDDInas,tSPDKegiatan.btKodeUK,tSPDKegiatan.IDurusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan, tSPDKegiatan.IDSUBKegiatan, tSPDKegiatan.IIDRekening  ";



                    paramCollection.Add(new DBParameter("@IDDINAS", iddinas));
                    paramCollection.Add(new DBParameter("@NoURUT", mourut));
                }
                else
                {
                    SSQL = "SELECT tSPD.btJenis, tSPDKegiatan.IDDInas,tSPDKegiatan.btKodeUK, tSPDKegiatan.IDurusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan, tSPDKegiatan.IDSUBKegiatan, tSPDKegiatan.IIDRekening , " +
                          " SUM(tSPDKegiatan.cJumlah) AS JumlahLalu,  " +
                          " 0 AS JumlahKini  " +
                          " from tSPD inner join tSPDKegiatan on tSPD.inourut = tSPDKegiatan.inourut WHERE tSPDKegiatan.IDDInas =@IDDINAS  " +
                           " GROUP BY tSPD.btJenis,tSPDKegiatan.IDDInas,tSPDKegiatan.btKodeUK,tSPDKegiatan.IDurusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan, tSPDKegiatan.IDSUBKegiatan, tSPDKegiatan.IIDRekening   " +
                           " ORDER BY tSPDKegiatan.IDDInas,tSPDKegiatan.btKodeUK,tSPDKegiatan.IDurusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan, tSPDKegiatan.IDSUBKegiatan, tSPDKegiatan.IIDRekening  ";

 
                



                    paramCollection.Add(new DBParameter("@IDDINAS", iddinas));
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new DisplaySPD()
                                {
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    KodeUK = DataFormat.GetInteger (dr["btKodeUK"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDurusan"]),
                                    IDProgram= DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan= DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                    IDRekening= DataFormat.GetLong(dr["IIDRekening"]),
                                    JumlahLalu= DataFormat.GetDecimal(dr["JumlahLalu"]),
                                    Jumlah = DataFormat.GetDecimal(dr["JumlahKini"]),
                                   

                                }).ToList();
                    }
                }
                return _lst;   
           } catch (Exception ex){
               _isError = true;
               _lastError= ex.Message;
               return null;
           }

        }
        public List<SPD> Get(int pIDDInas, int pTahun, int PPKD = 0)
        {
            List<SPD> _lst = new List<SPD>();
            try
            {
                SSQL = "SELECT tSPD.* FROM tSPD  WHERE tSPD.IDDInas= @IDSKPD AND tSPD.iTahun =@Tahun  ORDER BY  tSPD.iNoURut";
                
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@IDSKPD", pIDDInas));
               paramCollection.Add(new DBParameter("@Tahun", pTahun));
               //paramCollection.Add(new DBParameter("@PPKD", PPKD));

                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPD()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Bulan = DataFormat.GetInteger(dr["btBulan"]),
                                    Bulan2 = DataFormat.GetInteger(dr["btBulan2"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtSPD"]),
                                    PPKD = DataFormat.GetBoolean(dr["bPPKD"]) == true ? 1 : 0,
                                    NoSPD = DataFormat.GetString(dr["sNoSPD"]),
                                    NamaBendahara = DataFormat.GetString(dr["sBendahara"]),
                                    NamaPPTK = DataFormat.GetString(dr["sPPTK"]),
                                    KetentuanLain = DataFormat.GetString(dr["sKetentuanLain"]),
                                    Triwulan = DataFormat.GetSingle(dr["btTriwulan"]),
                                  //  ListDetail = GetDetail(DataFormat.GetLong(dr["iNoURut"])),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    IDBendahara = DataFormat.GetInteger(dr["IDBendahara"]),
                                    INoSPD = DataFormat.GetInteger(dr["iNoSPD"]),
                                    Prefix = DataFormat.GetString(dr["sPrefix"]),
                                    JenisRekening = DataFormat.GetSingle(dr["iJenisRekening"]),
                                    JenisAnggaran = DataFormat.GetInteger(dr["JenisAnggaran"])

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
        public List<SPD> GetUntukSPP(int pIDDInas, int pTahun, DateTime dBatas, long nourut=0)
        {
            List<SPD> _lst = new List<SPD>();
            try
            {
                SSQL = "SELECT tSPD.* FROM tSPD  WHERE tSPD.IDDInas= @IDSKPD AND tSPD.iTahun =@Tahun and iStatus>0 and dtSPD <= @TANGGALSPD  ";
                if (nourut > 0)
                {
                    SSQL = SSQL + " AND tSPD.inourut <= " + nourut.ToString();

                }
                SSQL = SSQL + " ORDER BY  tSPD.iNoURut";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@IDSKPD", pIDDInas));
                paramCollection.Add(new DBParameter("@Tahun", pTahun));
                paramCollection.Add(new DBParameter("@TANGGALSPD", dBatas, DbType.Date));

                //paramCollection.Add(new DBParameter("@PPKD", PPKD));


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPD()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    Bulan = DataFormat.GetInteger(dr["btBulan"]),
                                    Bulan2 = DataFormat.GetInteger(dr["btBulan2"]),
                                    Tanggal = DataFormat.GetDateTime(dr["dtSPD"]),
                                    PPKD = DataFormat.GetBoolean(dr["bPPKD"]) == true ? 1 : 0,
                                    NoSPD = DataFormat.GetString(dr["sNoSPD"]),
                                    NamaBendahara = DataFormat.GetString(dr["sBendahara"]),
                                    NamaPPTK = DataFormat.GetString(dr["sPPTK"]),
                                    KetentuanLain = DataFormat.GetString(dr["sKetentuanLain"]),
                                    Triwulan = DataFormat.GetSingle(dr["btTriwulan"]),
                                    //  ListDetail = GetDetail(DataFormat.GetLong(dr["iNoURut"])),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    IDBendahara = DataFormat.GetInteger(dr["IDBendahara"]),
                                    INoSPD = DataFormat.GetInteger(dr["iNoSPD"]),
                                    Prefix = DataFormat.GetString(dr["sPrefix"]),
                                    JenisRekening = DataFormat.GetSingle(dr["iJenisRekening"]),
                                    JenisAnggaran = DataFormat.GetInteger(dr["JenisAnggaran"])

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
        public SPD GetByID(int _pIDDinas,long inourut)
        {
            SPD oSPD =new SPD();
            try
            {
                    
                //SSQL = "SELECT inoUrut, iTahun,btJenis,IDDinas, btBulan,btBulan2, dtSPD, sNOSPD, bPPKD,sBendahara,sPPTK,sKetentuanLain,btTriwulan  * FROM tSPD WHERE IDDInas= " + _pIDDinas.ToString() + " ORDER BY iNoURut";

                SSQL = "SELECT * FROM tSPD WHERE inourut =" + inourut.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        DataRow dr = dt.Rows[0];
                        oSPD = new SPD
                        {

                            NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            Jenis = DataFormat.GetSingle(dr["btJenis"]),
                            IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                            Bulan = DataFormat.GetInteger(dr["btBulan"]),
                            Bulan2 = DataFormat.GetInteger(dr["btBulan2"]),
                            Tanggal = DataFormat.GetDateTime(dr["dtSPD"]),
                            PPKD = DataFormat.GetBoolean(dr["bPPKD"])==true?1:0,
                            NoSPD = DataFormat.GetString(dr["sNoSPD"]),
                            NamaBendahara = DataFormat.GetString(dr["sBendahara"]),
                            NamaPPTK = DataFormat.GetString(dr["sPPTK"]),
                            KetentuanLain = DataFormat.GetString(dr["sKetentuanLain"]),
                            Triwulan = DataFormat.GetSingle(dr["btTriwulan"]),
                            ListDetail=GetDetail(DataFormat.GetLong(dr["iNoURut"])),
                            Status = DataFormat.GetSingle(dr["iStatus"]),
                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                            Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                            IDBendahara = DataFormat.GetInteger(dr["IDBendahara"]),
                            INoSPD = DataFormat.GetInteger(dr["iNoSPD"]),
                            Prefix = DataFormat.GetString(dr["sPrefix"]),
                            JenisRekening= DataFormat.GetSingle(dr["iJenisRekening"]),
                            JenisAnggaran = DataFormat.GetInteger(dr["JenisAnggaran"])
                        };

                    }
                }
                return oSPD;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return null;
            }
        }

        private bool CekNoSPD(string sNoSPD)
        {
            SSQL = "SELECT * FROM tSPD WHERE sNoSPD ='" + sNoSPD.Trim() + "'";
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool Simpan(ref SPD _spd)
        {
            try
            {
                m_connection = _dbHelper.CreateCOnnection();
             

                if (_spd.NoUrut == 0)
                {
                    if (CekNoSPD(_spd.NoSPD) == true)
                    {
                        _lastError = "Nomor " + _spd.NoSPD + " Sudah ada.";
                        _isError = true;
                        return false;

                    }

                    string _newnoUrut = GetNoUrut("iNoSPD", (int)E_KOLOM_NOURUT.CON_URUT_SPD, _spd.Tahun, _spd.IDDInas);
                    m_objTrans = m_connection.BeginTransaction();
                    SSQL = "INSERT INTO tSPD (iNoURut,iTahun,btJenis,IDDInas,btBulan,btBulan2,dtSPD,bPPKD,sNoSPD,sBendahara,sPPTK,sKetentuanLain,btTriwulan, btKodekategori, btKodeurusan,btKodeSKPD,bBatal,iStatus,cJumlah, sKeterangan, IDBendahara, iNoSPD,sPrefix,iJenisRekening,JenisAnggaran) values ( " +
                        "@piNoURut,@piTahun,@pbtJenis,@pIDDInas,@pbtBulan,@pbtBulan2,@pdtSPD,@pbPPKD,@psNoSPD,@psBendahara,@psPPTK,@psKetentuanLain,@pbtTriwulan, @pbtKodekategori, @pbtKodeurusan,@pbtKodeSKPD,0, 0,@pcJumlah,@psKeterangan, @pIDBendahara, @piNoSPD,@psPrefix,@iJenisRekening,@JenisAnggaran)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piNoURut", _newnoUrut));
                    paramCollection.Add(new DBParameter("@piTahun", _spd.Tahun));
                    paramCollection.Add(new DBParameter("@pbtJenis", _spd.Jenis));
                    paramCollection.Add(new DBParameter("@pIDDInas", _spd.IDDInas));
                    paramCollection.Add(new DBParameter("@pbtBulan", _spd.Bulan));
                    paramCollection.Add(new DBParameter("@pbtBulan2", _spd.Bulan2));
                    paramCollection.Add(new DBParameter("@pdtSPD", _spd.Tanggal,DbType.Date));
                    paramCollection.Add(new DBParameter("@pbPPKD", _spd.PPKD));
                    paramCollection.Add(new DBParameter("@psNoSPD", _spd.NoSPD));
                    paramCollection.Add(new DBParameter("@psBendahara", _spd.NamaBendahara));
                    paramCollection.Add(new DBParameter("@psPPTK", _spd.NamaPPTK));
                    paramCollection.Add(new DBParameter("@psKetentuanLain", _spd.KetentuanLain));
                    paramCollection.Add(new DBParameter("@pbtTriwulan", _spd.Triwulan));
                    paramCollection.Add(new DBParameter("@pbtKodekategori", _spd.IDDInas.KodeKategori())); 
                    paramCollection.Add(new DBParameter("@pbtKodeurusan", _spd.IDDInas.KodeUrusan()));
                    paramCollection.Add(new DBParameter("@pbtKodeSKPD", _spd.IDDInas.KodeSKPD()));
                    paramCollection.Add(new DBParameter("@pcJumlah",_spd.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psKeterangan",_spd.Keterangan));
                    paramCollection.Add(new DBParameter("@pIDBendahara", _spd.IDBendahara));
                    paramCollection.Add(new DBParameter("@piNoSPD",_spd.INoSPD));
                    paramCollection.Add(new DBParameter("@psPrefix",_spd.Prefix));
                    paramCollection.Add(new DBParameter("@iJenisRekening",_spd.JenisRekening));
                    paramCollection.Add(new DBParameter("@JenisAnggaran",_spd.JenisAnggaran));

               

                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans) > 0)
                   // if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection) > 0)
                    {
                        _spd.NoUrut = DataFormat.GetLong(_newnoUrut);
                        // //DBParameterCollection paramUpdate = new DBParameterCollection();
                        //SSQL = "UPDATE tSPD set btKodekategori = " + _spd.IDDInas.ToString().Substring(0, 1) + ",btKodeUrusan =" + _spd.IDDInas.ToString().Substring(1, 2) + " , btKodeSKPD = " + _spd.IDDInas.ToString().Substring(3, 2) + " , btKodeUK = " + _spd.IDDInas.ToString().Substring(5, 2) + " WHERE inourut = " + _spd.NoUrut.ToString();
                        //_dbHelper.ExecuteNonQuery(SSQL, m_connection, m_objTrans);
                        //////, m_connection, m_objTrans
                        if (SimpanKegiatan(_spd.ListDetail, _spd.NoUrut.ToString(), m_connection, m_objTrans) == true)
                        {
                            m_objTrans.Commit();
                            return true;
                        }
                        else
                        {
                            m_objTrans.Rollback();
                            return false;
                        }
                    }
                    else
                    {
                        m_objTrans.Rollback();
                        return false;
                    }
                }
                else
                {
                    m_objTrans = m_connection.BeginTransaction();
                    SSQL = "UPDATE tSPD SET btJenis=@pbtJenis,btBulan=@pbtBulan,btBulan2=@pbtBulan2," +
                        " dtSPD=@pdtSPD,bPPKD=@pbPPKD,sNoSPD=@psNoSPD,sBendahara=@psBendahara,sPPTK=@psPPTK,sKetentuanLain=@psKetentuanLain,btTriwulan=@pbtTriwulan, " +
                        "cJumlah=@pcJumlah,sKeterangan=@psKeterangan, IDbendahara =@pIDBendahara,iJenisRekening=@piJenisRekening, JenisAnggaran= @JenisAnggaran WHERE iNoURut=@piNoURut and IDDInas=@pIDDInas and iTahun=@piTahun";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@pbtJenis", _spd.Jenis));
                    paramCollection.Add(new DBParameter("@pbtBulan", _spd.Bulan));
                    paramCollection.Add(new DBParameter("@pbtBulan2", _spd.Bulan2));
                    paramCollection.Add(new DBParameter("@pdtSPD", _spd.Tanggal,DbType.Date));
                    paramCollection.Add(new DBParameter("@pbPPKD", _spd.PPKD));
                    paramCollection.Add(new DBParameter("@psNoSPD", _spd.NoSPD));
                    paramCollection.Add(new DBParameter("@psBendahara", _spd.NamaBendahara));
                    paramCollection.Add(new DBParameter("@psPPTK", _spd.NamaPPTK));
                    paramCollection.Add(new DBParameter("@psKetentuanLain", _spd.KetentuanLain));
                    paramCollection.Add(new DBParameter("@pbtTriwulan", _spd.Triwulan));                    
                    paramCollection.Add(new DBParameter("@pcJumlah", _spd.Jumlah,DbType.Decimal));
                    paramCollection.Add(new DBParameter("@psKeterangan",_spd.Keterangan));
                    paramCollection.Add(new DBParameter("@pIDBendahara", _spd.IDBendahara));
                    paramCollection.Add(new DBParameter("@piJenisRekening", _spd.JenisRekening));
                    paramCollection.Add(new DBParameter("@JenisAnggaran", _spd.JenisAnggaran));
                    paramCollection.Add(new DBParameter("@piNoURut", _spd.NoUrut));
                    paramCollection.Add(new DBParameter("@pIDDInas", _spd.IDDInas));
                    paramCollection.Add(new DBParameter("@piTahun", _spd.Tahun));

                    if (_dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans) > 0)
                    {
                        if (SimpanKegiatan(_spd.ListDetail, _spd.NoUrut.ToString(),m_connection, m_objTrans) == true)
                        {

                            m_objTrans.Commit();
                            return true;
                        }

                        else
                        {
                            m_objTrans.Rollback();
                            return false;
                        }
                           
                    }
                    else
                    {
                        m_objTrans.Rollback();
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
        public bool Valid(long inourut)
        {
            try
            {
                SSQL = "UPDATE tSPD SET iStatus = 1 where inourut= @NoUrut";
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@NoUrut", inourut));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch(Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }
        public bool UnValid(SPD oSPD)
        {
            try
            {
                if (CekStatusDiSPP(oSPD.NoUrut) > 0)
                {

                    _isError = true;
                    _lastError = "SPD Sudah dipakai di SPP, tidak bisa dihapus";
                    return false;

                }
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "UPDATE tSPD SET iStatus = 0 where inourut= @pinourut ";
                paramCollection.Add(new DBParameter("@pinourut", oSPD.NoUrut));
                
                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                return true;

            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }
        }
        public List<SPDDetail> GetDetail( long NoUrut)
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {
                

                SSQL = "SELECT * FROM tSPDKegiatan WHERE inourut= " + NoUrut.ToString() + " ORDER BY IDDInas,IDProgram, IDKegiatan,idsubkegiatan,IIDRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPDDetail()
                                {
                                    NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSUbKegiatan"]), 
                                    IDDInas = DataFormat.GetInteger(dr["idDinas"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
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

        public List<SPDDetail> GetDetailByDinas (int iddinas)
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {


                SSQL = "SELECT tSPD.btJenis , tSPDKegiatan.* FROM tSPD INNER JOIN tSPDKegiatan ON tSPD.inourut= tSPDKegiatan.inourut  WHERE tSPDKegiatan.IDDInas = @DINAS ORDER BY tSPDKegiatan.iNoURut,tSPDKegiatan.IDUrusan, tSPDKegiatan.IDProgram, tSPDKegiatan.IDKegiatan,tSPDKegiatan.idsubkegiatan,tSPDKegiatan.IIDRekening";

                DataTable dt = new DataTable();

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@DINAS", iddinas));


               dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPDDetail()
                                {
                                    Jenis = DataFormat.GetInteger(dr["btJenis"]),
                                    NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSUbKegiatan"]),
                                    IDDInas = DataFormat.GetInteger(dr["idDinas"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),

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


        public decimal GetJumlahDetail(long NoUrut)
        {
            decimal cJumlahDetail = 0;
            try
            {
                SSQL = "SELECT SUM(cJumlah) as Jumlah FROM tSPDKegiatan WHERE inourut= " + NoUrut.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        cJumlahDetail = DataFormat.GetDecimal (dr["Jumlah"]);

                    }
                }
                return cJumlahDetail;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }

        }
        //public bool SimpanKegiatan(List<SPDDetail> _lst, string pnewnoUrut
        //    )
           public bool SimpanKegiatan(List<SPDDetail> _lst, string pnewnoUrut, IDbConnection m_connection,
        IDbTransaction m_objTrans)

        {
            bool bRet = false;            
            try
            {
                
                
                if (_lst.Count == 0)
                    return true;

              
                foreach (SPDDetail d in _lst)
                {
                    if (d.StatusUpdate == 1)
                    //if (CekRekening(d, pnewnoUrut, m_connection, m_objTrans)== true )
                    {
                        SSQL = "UPDATE  tSPDKEgiatan SET cJumlah=@pcJumlah  " +
                            "  WHERE  inourut=@pinourut and  iddinas= @piddinas AND btKodeUK=@KodeUK AND idUrusan= @pidUrusan AND  idProgram =@pidProgram and " +
                            " idsubkegiatan=@idsubkegiatan and idkegiatan = @pidkegiatan and iidRekening= @piidRekening ";


                        DBParameterCollection paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("@pcJumlah", d.Jumlah,DbType.Decimal));
                        paramCollection.Add(new DBParameter("@pinourut", pnewnoUrut));
                        paramCollection.Add(new DBParameter("@piddinas", d.IDDInas));
                        paramCollection.Add(new DBParameter("@KodeUK", d.KodeUK));
                        paramCollection.Add(new DBParameter("@pidUrusan", d.IDUrusan));
                        paramCollection.Add(new DBParameter("@pidProgram", d.IDProgram));
                        paramCollection.Add(new DBParameter("@pidkegiatan", d.IDKegiatan));
                        paramCollection.Add(new DBParameter("@idsubkegiatan", d.IDSubkegiatan));

                        paramCollection.Add(new DBParameter("@piidRekening", d.IDRekening));


                        _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection,m_objTrans);

                    }
                    else
                    {
                        if (Tahun <= 2020)
                        {

                            SSQL = "INSERT INTO xtSPDKEgiatan(inourut, iddinas, btKodeUK,idUrusan, idProgram,idkegiatan,idsubkegiatan,iidRekening,cJumlah, btKodekategoriPelaksana,btKodeUrusanPElaksana, btIDProgram,btIDkegiatan) values (" +
                                "@pinourut, @piddinas, @KodeUK,@pidUrusan, @pidProgram,@pidkegiatan,@idsubkegiatan,@piidRekening,@pcJumlah, @pbtKodekategoriPelaksana,@pbtKodeUrusanPElaksana, @pbtIDProgram,@pbtIDkegiatan)";
                            DBParameterCollection paramCollection = new DBParameterCollection();
                            paramCollection.Add(new DBParameter("@pinourut", pnewnoUrut));
                            paramCollection.Add(new DBParameter("@piddinas", d.IDDInas));
                            paramCollection.Add(new DBParameter("@KodeUK", d.KodeUK));

                            paramCollection.Add(new DBParameter("@pidUrusan", d.IDUrusan));
                            paramCollection.Add(new DBParameter("@pidProgram", d.IDProgram));
                            paramCollection.Add(new DBParameter("@pidkegiatan", d.IDKegiatan));
                            paramCollection.Add(new DBParameter("@idsubkegiatan", d.IDSubkegiatan));
                            paramCollection.Add(new DBParameter("@piidRekening", d.IDRekening));
                            paramCollection.Add(new DBParameter("@pcJumlah", d.Jumlah,DbType.Decimal));
                            //paramCollection.Add(new DBParameter("@pbtKodeUK", d.IDDInas.KodeUK()));

                            paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", DataFormat.GetInteger(d.IDUrusan.ToString().Substring(0, 1))));//d.IDUrusan.KodeKategoriPelaksana()));
                            paramCollection.Add(new DBParameter("@pbtKodeUrusanPElaksana", DataFormat.GetInteger(d.IDUrusan.ToString().Substring(1, 2))));

                            paramCollection.Add(new DBParameter("@pbtIDProgram", d.IDProgram == 0 ? 0 : DataFormat.GetInteger(d.IDProgram.ToString().Substring(3))));
                            paramCollection.Add(new DBParameter("@pbtIDkegiatan", d.IDKegiatan == 0 ? 0 : DataFormat.GetInteger(d.IDKegiatan.ToString().Substring(5))));
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);
                        }
                        else
                        {
                            SSQL = "INSERT INTO tSPDKEgiatan(inourut, iddinas, btKodeUK, idUrusan, idProgram,idkegiatan,idsubkegiatan,iidRekening,cJumlah, btKodekategoriPelaksana,btKodeUrusanPElaksana, btIDProgram,btIDkegiatan, btidsubkegiatan) values (" +
                              "@pinourut, @piddinas,@KodeUK, @pidUrusan, @pidProgram,@pidkegiatan,@idsubkegiatan,@piidRekening,@pcJumlah,@pbtKodekategoriPelaksana,@pbtKodeUrusanPElaksana, @pbtIDProgram,@pbtIDkegiatan,@pbtidsubkegiatan)";
                            DBParameterCollection paramCollection = new DBParameterCollection();
                            paramCollection.Add(new DBParameter("@pinourut", pnewnoUrut));
                            paramCollection.Add(new DBParameter("@piddinas", d.IDDInas));
                            paramCollection.Add(new DBParameter("@KodeUK", d.KodeUK));
                            paramCollection.Add(new DBParameter("@pidUrusan", d.IDUrusan));
                            paramCollection.Add(new DBParameter("@pidProgram", d.IDProgram));
                            paramCollection.Add(new DBParameter("@pidkegiatan", d.IDKegiatan));
                            paramCollection.Add(new DBParameter("@idsubkegiatan", d.IDSubkegiatan));
                            paramCollection.Add(new DBParameter("@piidRekening", d.IDRekening));
                            paramCollection.Add(new DBParameter("@pcJumlah", d.Jumlah,DbType.Decimal));
                            //paramCollection.Add(new DBParameter("@pbtKodeUK", d.IDDInas.KodeUK()));

                            paramCollection.Add(new DBParameter("@pbtKodekategoriPelaksana", DataFormat.GetInteger(d.IDUrusan.ToString().Substring(0, 1))));//d.IDUrusan.KodeKategoriPelaksana()));
                            paramCollection.Add(new DBParameter("@pbtKodeUrusanPElaksana", DataFormat.GetInteger(d.IDUrusan.ToString().Substring(1, 2))));

                            paramCollection.Add(new DBParameter("@pbtIDProgram", d.IDProgram == 0 ? 0 : DataFormat.GetInteger(d.IDProgram.ToString().Substring(3))));
                            paramCollection.Add(new DBParameter("@pbtIDkegiatan", d.IDKegiatan == 0 ? 0 : DataFormat.GetInteger(d.IDKegiatan.ToString().Substring(5))));
                            paramCollection.Add(new DBParameter("@pbtidsubkegiatan", d.IDSubkegiatan == 0 ? 0 : DataFormat.GetInteger((d.IDSubkegiatan % 100 ).ToString())));
                            _dbHelper.ExecuteNonQuery(SSQL, paramCollection, m_connection, m_objTrans);


                        }

                    }
                }
              
                return true ;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
           private bool CekRekening(SPDDetail d, string pnewnoUrut)
        {
            try
            {
                bool ret = false;
                SSQL = "SELECT * from tSPDKEgiatan " +
                                "  WHERE  inourut=@pinourut and  iddinas= @piddinas AND btKodeUK = @KodeUK AND idUrusan= @pidUrusan AND  idProgram =@pidProgram and " +
                                " idsubkegiatan=@idsubkegiatan and idkegiatan = @pidkegiatan and iidRekening= @piidRekening ";


                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@pinourut", pnewnoUrut));
                paramCollection.Add(new DBParameter("@piddinas", d.IDDInas));
                paramCollection.Add(new DBParameter("@KodeUK", d.KodeUK));
                paramCollection.Add(new DBParameter("@pidUrusan", d.IDUrusan));
                paramCollection.Add(new DBParameter("@pidProgram", d.IDProgram));
                paramCollection.Add(new DBParameter("@pidkegiatan", d.IDKegiatan));
                paramCollection.Add(new DBParameter("@idsubkegiatan", d.IDSubkegiatan));

                paramCollection.Add(new DBParameter("@piidRekening", d.IDRekening));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return false;
            }

        }
        public bool Hapus(ref SPD _spd)
        {
            try
            {
                if (CekStatus(_spd.NoUrut.ToString()) == true)
                {
                    _isError = true;
                    _lastError = "SPD Sudah valid, tidak bisa dihapus";
                    return false;

                }
                if (CekStatusDiSPP(_spd.NoUrut)>0){

                    _isError = true;
                    _lastError = "SPD Sudah dipakai di SPP, tidak bisa dihapus";
                    return false;

                }

                DBParameterCollection paramCollection = new DBParameterCollection();

                    SSQL = "DELETE FROM tSPDKegiatan WHERE iNoUrut=@pinourut";
                    paramCollection.Add(new DBParameter("@pinourut", _spd.NoUrut));
                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                    SSQL = "DELETE FROM tSPD WHERE iNoUrut=@pinourut";
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
        public bool HapusRincian(ref SPD _spd)
        {
            try
            {
                
                SSQL = "DELETE FROM tSPDKegiatan WHERE iNoUrut=" + _spd.NoUrut.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                
                return true;
            }

            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message + " " + SSQL;
                return false;
            }

        }
        private bool CekStatus(string sNoUrut)
        {
            int iStatus = 0;
            try{
                 //tSPD SET 
                SSQL = "SELECT  iStatus  from tSPD where inourut= " + sNoUrut;
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        iStatus = DataFormat.GetInteger(dr["iStatus"]);

                    }
                }
                return iStatus ==1;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return false;
            }

        }
        private int  CekStatusDiSPP(long  NoUrut)
        {
            int iSPP = 0;
            try
            {
                //tSPD SET 
                SSQL = "SELECT  count(*) as PemakaiSPP from tSPP where inourutSPD=@pinourut ";
                DBParameterCollection paramCollection = new DBParameterCollection();
           
                paramCollection.Add(new DBParameter("@pinourut", NoUrut));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        iSPP = DataFormat.GetInteger(dr["PemakaiSPP"]);

                    }
                }
                return iSPP;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return iSPP;
            }

        }

        public List<SPD> GetSPDSebelumPerJenis(int pIDDInas, DateTime dSPD, int iJenis)
        {

            List<SPD> _lst =new List<SPD>();

            int iSPP = 0;
            try
            {
                //int iJenis = 0;
                //if (JenisSPP == 4)
                //{
                //    iJenis = 2;
                //}
                //else
                //{
                //    if (JenisSPP == 3)
                //    {
                //        iJenis = 3;
                //    }
                //    else
                //        iJenis = 5;
                //}
                //tSPD SET 
                SSQL = "SELECT  *  from tSPD where iTahun = " + Tahun.ToString() + " AND  IDDInas = " + pIDDInas.ToString() + " AND dtSPD<= " + dSPD.ToSQLFormat() + " AND btJenis =" + iJenis.ToString() + 
                    " ORDER BY inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    
                        _lst = (from DataRow dr in dt.Rows
                                select new SPD()
                                {
                                    NoUrut =DataFormat.GetLong(dr["iNoURut"]),
                                    Tahun =DataFormat.GetInteger(dr["iTahun"]),
                                    Jenis =DataFormat.GetSingle(dr["btJenis"]),
                                    IDDInas =DataFormat.GetInteger(dr["IDDInas"]),
                                    Bulan =DataFormat.GetInteger(dr["btBulan"]),
                                    Bulan2 = DataFormat.GetInteger(dr["btBulan2"]),
                                    Tanggal =DataFormat.GetDateTime(dr["dtSPD"]),                                     
                                    PPKD= DataFormat.GetSingle(dr["bPPKD"]),
                                    NoSPD = DataFormat.GetString(dr["sNoSPD"]),
                                    NamaBendahara = DataFormat.GetString(dr["sBendahara"]),
                                    NamaPPTK= DataFormat.GetString(dr["sPPTK"]),
                                    KetentuanLain= DataFormat.GetString(dr["sKetentuanLain"]),            
                                    Triwulan =DataFormat.GetSingle(dr["btTriwulan"]),
                                    Status = DataFormat.GetSingle(dr["iStatus"]),
                                    Jumlah = GetJumlahDetail(DataFormat.GetLong(dr["iNoURut"])),
                                    Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                    IDBendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                    INoSPD=DataFormat.GetInteger(dr["iNoSPD"]),
                                    Prefix = DataFormat.GetString(dr["sPrefix"]),
                                    JenisRekening = DataFormat.GetSingle(dr["iJenisRekening"]),
                                    JenisAnggaran = DataFormat.GetInteger(dr["JenisAnggaran"])

                                }).ToList();
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
        public List<SPDKegiatan> GetSPDSebelumPerJenis(int pIDDInas, long iNoUrut , long pIDKegiatan ,int iJenis)
        {

            List<SPDKegiatan> _lst = new List<SPDKegiatan>();

            int iSPP = 0;
            try
            {
                //tSPD SET 
                SSQL = "SELECT  B.IIDRekening, C.sNamaRekening, sum(B.cJumlah) from tSPD A INNER JOIN tSPDKEgiatan B ON a.iNoUrut= B.inourut " +
                        " INNER JOIN mRekening C ON B.IIDRekening = C.IIDRekening where A.iTahun = " + Tahun.ToString() + " AND  IDDInas = " + pIDDInas.ToString() + " AND btJenis =" + iJenis.ToString() +
                    " ORDER BY inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {

                    _lst = (from DataRow dr in dt.Rows
                            select new SPDKegiatan()
                            {
                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                Jumlah = DataFormat.GetDecimal(dr["Jumlah"])


                            }).ToList();
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
        public List<RekeningDetail> GetSPDSPP(SPP oSPP)
        {
            List<RekeningDetail> lst = new List<RekeningDetail>();
            if (oSPP.Jenis ==1){
            //    SSQL = "SELECT ";
            }
            if ((oSPP.Jenis == 2 || oSPP.Jenis == 3) && oSPP.PPKD == 0)
            {
                SSQL = " SELECT tSPDKegiatan.IDProgram,tSPDKegiatan.IDUrusan, tSPDKegiatan.IDKegiatan, " +
                        " tSPDKegiatan.IIDRekening, sum (tSPDKegiatan.cJumlah) as Jumlah,    mRekening.sNamaRekening as NamaRekening  , " +
                         "(select sum (Debet * cJumlah) from viewTrx where  viewTrx.IDDinas = tSPD.IDDInas   AND dtBukukas < " + oSPP.dtSPP.ToSQLFormat() + " and IIDRekening = tSPDKegiatan.IIDRekening) as RealisasiSoFar ,  " +
                          " (select cJumlah from tSPPRekening where  Inourut = " + oSPP.NoUrut.ToString() + " AND IIDRekening = tSPDKegiatan.IIDRekening) as Nilai " +
                          " from tSPD INNER join tspdKegiatan ON tSPD.inourut = tSPDKegiatan.inourut   inner join mRekening ON tSPDKegiatan.IIDRekening = mRekening.IIDRekening  " +
                        " WHERE tSPD.inourut <=" + oSPP.NoUrutSPD.ToString() + " and tSPD.btJenis = 4 and tSPD.IDDinas = " + oSPP.IDDInas.ToString() + " group by tSPD.IDDInas,tSPDKegiatan.IDProgram,tSPDKegiatan.IDUrusan,  " +
                        " tSPDKegiatan.IDKegiatan,  tSPDKegiatan.IIDRekening, mRekening.sNamaRekening ";


            }
            if (oSPP.Jenis == 4  && oSPP.PPKD == 0)
            {

                SSQL = " SELECT tSPDKegiatan.IDProgram,tSPDKegiatan.IDUrusan, tSPDKegiatan.IDKegiatan, " +
                        " tSPDKegiatan.IIDRekening, sum (tSPDKegiatan.cJumlah) as Jumlah,    mRekening.sNamaRekening as NamaRekening  , " +
                         "(select sum (Debet * cJumlah) from viewTrx where  viewTrx.IDDinas = tSPD.IDDInas   AND dtBukukas < " + oSPP.dtSPP.ToSQLFormat() + " and IIDRekening = tSPDKegiatan.IIDRekening) as RealisasiSoFar ,  " +
                          " (select cJumlah from tSPPRekening where  Inourut = " + oSPP.NoUrut.ToString() + " AND IIDRekening = tSPDKegiatan.IIDRekening) as Nilai " +
                          " from tSPD INNER join tspdKegiatan ON tSPD.inourut = tSPDKegiatan.inourut   inner join mRekening ON tSPDKegiatan.IIDRekening = mRekening.IIDRekening  " +
                        " WHERE tSPD.inourut <=" + oSPP.NoUrutSPD.ToString() + " and tSPD.btJenis = 3 and tSPD.IDDinas = " + oSPP.IDDInas.ToString() + " group by tSPD.IDDInas,tSPDKegiatan.IDProgram,tSPDKegiatan.IDUrusan,  " +
                        " tSPDKegiatan.IDKegiatan,  tSPDKegiatan.IIDRekening, mRekening.sNamaRekening ";

 


            }
            if (oSPP.PPKD == 1)
            {

                SSQL = " SELECT tSPDKegiatan.IDProgram,tSPDKegiatan.IDUrusan, tSPDKegiatan.IDKegiatan, " +
                        " tSPDKegiatan.IIDRekening, sum (tSPDKegiatan.cJumlah) as Jumlah,    mRekening.sNamaRekening as NamaRekening  , " +
                         "(select sum (Debet * cJumlah) from viewTrx where  viewTrx.IDDinas = tSPD.IDDInas   AND dtBukukas < " + oSPP.dtSPP.ToSQLFormat() + " and IIDRekening = tSPDKegiatan.IIDRekening) as RealisasiSoFar ,  " +
                          " (select cJumlah from tSPPRekening where  Inourut = " + oSPP.NoUrut.ToString() + " AND IIDRekening = tSPDKegiatan.IIDRekening) as Nilai " +
                          " from tSPD INNER join tspdKegiatan ON tSPD.inourut = tSPDKegiatan.inourut   inner join mRekening ON tSPDKegiatan.IIDRekening = mRekening.IIDRekening  " +
                        " WHERE tSPD.inourut <=" + oSPP.NoUrutSPD.ToString() + " and tSPD.btJenis = 5 and tSPD.IDDinas = " + oSPP.IDDInas.ToString() + " group by tSPD.IDDInas,tSPDKegiatan.IDProgram,tSPDKegiatan.IDUrusan,  " +
                        " tSPDKegiatan.IDKegiatan,  tSPDKegiatan.IIDRekening, mRekening.sNamaRekening ";




            }
            if (SSQL=="")
                return null;
            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    lst = (from DataRow dr in dt.Rows
                            select new RekeningDetail()
                            {
        

                                IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                NamaRekening = DataFormat.GetString(dr["NamaRekening"]),
                                Sisa = DataFormat.GetDecimal(dr["Jumlah"]) - DataFormat.GetDecimal(dr["RealisasiSoFar"]),
                                Nilai=DataFormat.GetDecimal(dr["Nilai"]) 

                            }).ToList();
                }
            }
            return lst;


        }



        public List<SPDDetail> GetDetailSebelum(DateTime d, int pIDDInas, long idsubkegiatan=0, bool bpertmen77 = false )
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {

                if (bpertmen77 == false)
                {


                    DBParameterCollection paramCollection = new DBParameterCollection();
                    SSQL = "SELECT A.IDURusan,A.IDprogram, A.IDKegiatan,A.iDSubkegiatan,A.IIDRekening,C.sNamaRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut " +
                        " INNER JOIN mRekening C On A.IIDRekening= c.IIDRekening WHERE B.dtSPD <=@pDt  AND B.IDDInas = @DINAS  ";
                    paramCollection.Add(new DBParameter("@pDt", d, DbType.Date));
                    paramCollection.Add(new DBParameter("@DINAS",pIDDInas));
              


                    if (idsubkegiatan > 0)
                    {
                        SSQL = SSQL + " AND A.idsubkegiatan = @IDSUBKEGIATAN";
                        paramCollection.Add(new DBParameter("@IDSUBKEGIATAN", idsubkegiatan));
                    }

                    SSQL = SSQL + " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.iDSubkegiatan,A.IIDRekening,C.sNamaRekening" +
                            " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.iDSubkegiatan,A.IIDRekening";

                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new SPDDetail()
                                    {
                                       
                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        IDSubkegiatan = DataFormat.GetLong(dr["iDSubkegiatan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                        Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                        NamaRekening= DataFormat.GetString(dr["sNamaRekening"])

                                    }).ToList();
                        }
                    }
                    return _lst;
                }
                else
                {



                    //SSQL = "SELECT A.IDURusan,A.IDprogram, A.IDKegiatan,A.iDSubkegiatan,SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.dtSPD <=@pDt  AND B.IDDInas = " + pIDDInas.ToString() +
                    //        " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.iDSubkegiatan" +
                    //        " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.iDSubkegiatan";

                    SSQL = "SELECT 0 as IDURusan,0 as IDprogram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE  B.IDDInas = " + pIDDInas.ToString() +
                            " " +
                            "UNION SELECT A.IDURusan,0 as IDprogram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.IDDInas = " + pIDDInas.ToString() +
                            " GROUP BY A.IDURusan " +

                     " UNION SELECT A.IDURusan,A.IDprogram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE  B.IDDInas = " + pIDDInas.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram" +
                            " UNION  ALL SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.IDDInas = " + pIDDInas.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan" +
                            " UNION ALL SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE  B.IDDInas = " + pIDDInas.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan" +
                            " UNION ALL SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan,A.IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.IDDInas = " + pIDDInas.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan,A.IIDRekening";


                  //  DBParameterCollection paramCollection = new DBParameterCollection();
                   // paramCollection.Add(new DBParameter("@pDt", d, DbType.Date));
                    //paramCollection.Add(new DBParameter("@pIDDInas"m))

                    DataTable dt = new DataTable();
                    dt = _dbHelper.ExecuteDataTable(SSQL);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            _lst = (from DataRow dr in dt.Rows
                                    select new SPDDetail()
                                    {

                                        IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                        IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                        IDSubkegiatan = DataFormat.GetLong(dr["iDSubkegiatan"]),
                                        IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                        IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                        Jumlah = DataFormat.GetDecimal(dr["Jumlah"])

                                    }).ToList();
                        }
                    }
                    return _lst;

                }
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }

        }
        public List<SPDDetail> GetDetailSebelumNoUrutEx(long lNoUrut, int pIDDInas, long idSubKegiatan = 0, int KodeUK = 0)
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {
              


               DBParameterCollection paramCollection = new DBParameterCollection();
          


                if (Tahun >= 2021)
                {
                    SSQL = " SELECT A.IDURusan,A.IDprogram, A.IDKegiatan,A.IIDRekening,A.IDSUbKegiatan, c.sNamaRekening as Nama , SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B " +
                    " ON A.inourut = B.inourut INNER JOIN mRekening C ON C.IIDRekening = A.IIDRekening WHERE B.iNoUrut <= @NoUrut AND B.IDDInas = @IDDInas  AND A.IDSubKegiatan=@IDSUbKegiatan ";

                    if (pIDDInas == 1020100){
                        //SSQL=SSQL + " AND A.btKOdeUK = @UnitKerja ";
                        //paramCollection.Add(new DBParameter("@UnitKerja", KodeUK));
                    }

                    SSQL = SSQL + " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening" +
                            " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening";



                    paramCollection.Add(new DBParameter("@NoUrut", lNoUrut));
                    paramCollection.Add(new DBParameter("@IDDInas",pIDDInas));
                    paramCollection.Add(new DBParameter("@IDSUbKegiatan",idSubKegiatan));


                }
                else
                {
                    SSQL = " SELECT A.IDURusan,A.IDprogram, A.IDKegiatan,0 as IDSUbkEgiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening as Nama , SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B " +
                             " ON A.inourut = B.inourut INNER JOIN mRekening C ON C.IIDRekening = A.IIDRekening WHERE B.iNoUrut <= @NoUrut AND B.IDDInas = @IDDInas  AND B.iStatus = 1  ";
                    
                    
                    paramCollection.Add(new DBParameter("@NoUrut", lNoUrut));
                    paramCollection.Add(new DBParameter("@IDDInas",pIDDInas));
                    if (idSubKegiatan > 0)
                    {
                        SSQL = SSQL + " AND A.IDSUbKegiatan = @IDSUbKegiatan";
                         paramCollection.Add(new DBParameter("@IDSUbKegiatan",idSubKegiatan));
                    }

                    SSQL = SSQL + " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening" +
                            " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening";

                }
           
                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL,paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPDDetail()
                                {
                                    
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                  
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    IDSubkegiatan=DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    NamaRekening = DataFormat.GetString (dr["Nama"]),

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
        public List<SPDDetail> GetDetailSebelumTanggalEx(
            DateTime pTanggal, 
            int pIDDInas, 
            long idSubKegiatan = 0, 
            int KodeUK = 0)
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {



                DBParameterCollection paramCollection = new DBParameterCollection();
               
                    SSQL = " SELECT  A.IDURusan,A.IDprogram, A.IDKegiatan,A.IIDRekening,A.IDSUbKegiatan, c.sNamaRekening as Nama , SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B " +
                    " ON A.inourut = B.inourut INNER JOIN mRekening C ON C.IIDRekening = A.IIDRekening "+ 
                    " WHERE B.dtSPD <= @Tanggal AND B.IDDInas = @IDDInas  AND A.IDSubKegiatan=@IDSUbKegiatan ";

                    SSQL = SSQL + " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening" +
                            " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening";



                    paramCollection.Add(new DBParameter("@Tanggal", pTanggal,DbType.Date));
                    paramCollection.Add(new DBParameter("@IDDInas", pIDDInas));
                    paramCollection.Add(new DBParameter("@IDSUbKegiatan", idSubKegiatan));





                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPDDetail()
                                {

                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),

                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    NamaRekening = DataFormat.GetString(dr["Nama"]),

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

        public List<SPDDetail> GetSPDDanBelanjaSebelumNoUrutEx(long lNoUrut, 
            int pIDDInas, 
            long idSubKegiatan = 0, 
            int KodeUK = 0,
            long idRekening=0)
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {



                DBParameterCollection paramCollection = new DBParameterCollection();



                    SSQL = " SELECT A.IDURusan,A.IDprogram, A.IDKegiatan,A.IIDRekening,A.IDSUbKegiatan, c.sNamaRekening as Nama , SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B " +
                    " ON A.inourut = B.inourut INNER JOIN mRekening C ON C.IIDRekening = A.IIDRekening WHERE B.iNoUrut <= @NoUrut AND B.IDDInas = @IDDInas  AND A.IDSubKegiatan=@IDSUbKegiatan ";

                   

                    SSQL = SSQL + " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening" +
                            " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan,A.IDSUbKegiatan,A.IIDRekening, c.sNamaRekening";



                    paramCollection.Add(new DBParameter("@NoUrut", lNoUrut));
                    paramCollection.Add(new DBParameter("@IDDInas", pIDDInas));
                    paramCollection.Add(new DBParameter("@IDSUbKegiatan", idSubKegiatan));


                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPDDetail()
                                {

                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),

                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    NamaRekening = DataFormat.GetString(dr["Nama"]),

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

        public List<SPD> GetSPDSebelumPerJeni(int pIDDInas, DateTime dSPD, int iJenis)
        {

            List<SPD> _lst = new List<SPD>();

            int iSPP = 0;
            try
            {
                //tSPD SET 
                SSQL = "SELECT  *  from tSPD where iTahun = " + Tahun.ToString() + " AND  IDDInas = " + pIDDInas.ToString() + " AND dtSPD<= " + dSPD.ToSQLFormat() +
                    " ORDER BY inourut ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {

                    _lst = (from DataRow dr in dt.Rows
                            select new SPD()
                            {
                                NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                                Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                Jenis = DataFormat.GetSingle(dr["btJenis"]),
                                IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                Bulan = DataFormat.GetInteger(dr["btBulan"]),
                                Bulan2 = DataFormat.GetInteger(dr["btBulan2"]),
                                Tanggal = DataFormat.GetDateTime(dr["dtSPD"]),
                                PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                NoSPD = DataFormat.GetString(dr["sNoSPD"]),
                                NamaBendahara = DataFormat.GetString(dr["sBendahara"]),
                                NamaPPTK = DataFormat.GetString(dr["sPPTK"]),
                                KetentuanLain = DataFormat.GetString(dr["sKetentuanLain"]),
                                Triwulan = DataFormat.GetSingle(dr["btTriwulan"]),
                                Status = DataFormat.GetSingle(dr["iStatus"]),
                                Jumlah = GetJumlahDetail(DataFormat.GetLong(dr["iNoURut"])),
                                Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                IDBendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                INoSPD = DataFormat.GetInteger(dr["iNoSPD"]),
                                Prefix = DataFormat.GetString(dr["sPrefix"]),
                                JenisRekening = DataFormat.GetSingle(dr["iJenisRekening"]),
                                JenisAnggaran = DataFormat.GetInteger(dr["JenisAnggaran"])

                            }).ToList();
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
        public List<SPD> GetListSPDforSPP(SPP oSPP)
        {

            List<SPD> _lst = new List<SPD>();

            int iSPP = 0;
            try
            {
                switch(oSPP.Jenis){
                    case 1:
                    case 2:
                    case 3:

         
                          SSQL = "SELECT tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD, SUM(tSPDKegiatan.cJumlah) as Jumlah FROM tSPD INNER JOIN tSPDKegiatan ON tSPD.inourut = tSPDKegiatan.inourut WHERE iTahun = " + oSPP.Tahun.ToString() +
                                 " AND tSPD.IDDInas = " + oSPP.IDDInas.ToString() + " AND tSPD.dtSPD <=" + oSPP.dtSPP.ToSQLFormat() + 
                                 " AND tSPD.iStatus =1 AND tSPD.btJenis= 4 " +
                                 " GROUP BY tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD ORDER BY tSPD.inourut ";
                          if (oSPP.PPKD == 1 )
                            
                               SSQL = "SELECT tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD, SUM(tSPDKegiatan.cJumlah) as Jumlah FROM tSPD INNER JOIN tSPDKegiatan ON tSPD.inourut = tSPDKegiatan.inourut WHERE iTahun = " + oSPP.Tahun.ToString() + " AND " +
                                 " AND tSPD.IDDInas = " + oSPP.IDDInas.ToString() + "  AND dtSPD <=" + oSPP.dtSPP.ToSQLFormat() + 
                                 " AND tSPD.iStatus =1 AND tSPD.btJenis= 5 GROUP BY tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD ORDER BY tSPD.inourut ";
              
                          break;      
                   case  4:
                        SSQL = "SELECT tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD, SUM(tSPDKegiatan.cJumlah) as Jumlah FROM tSPD INNER JOIN tSPDKegiatan ON tSPD.inourut = tSPDKegiatan.inourut WHERE iTahun = " + oSPP.Tahun.ToString() +
                             " AND tSPD.IDDInas = " + oSPP.IDDInas.ToString() + " AND dtSPD <=" + oSPP.dtSPP.ToSQLFormat() + 
                             " AND tSPD.iStatus =1 AND tSPD.btJenis= 3   And dtSPD <= " + oSPP.dtSPP.ToSQLFormat() ;
                        SSQL = SSQL + " GROUP BY tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD ORDER BY tSPD.inourut ";
                        break;

                    case 5:
                        if (oSPP.JenisGaji== 62)            
                            SSQL = "SELECT tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD, SUM(tSPDKegiatan.cJumlah) as Jumlah FROM tSPD INNER JOIN tSPDKegiatan ON tSPD.inourut = tSPDKegiatan.inourut " +
                                 " WHERE iTahun = " + oSPP.Tahun.ToString() + " AND tSPD.IDDInas = " + oSPP.IDDInas.ToString() + " AND dtSPD <=" + oSPP.dtSPP.ToSQLFormat() + 
                                  " AND tSPD.btJenis= 5 and tSPD.bPPKD= 1 AND tSPD.inourut <= " + oSPP.NoUrutSPD.ToString()  + " AND tSPDKegiatan.IIDRekening like '6%' ";
                       else 
                            SSQL = "SELECT tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD, SUM(tSPDKegiatan.cJumlah) as Jumlah FROM tSPD INNER JOIN tSPDKegiatan ON tSPD.inourut = tSPDKegiatan.inourut " +
                                  " WHERE iTahun = " + oSPP.Tahun.ToString() + " AND AND tSPD.IDDInas = " + oSPP.IDDInas.ToString() + " AND dtSPD <=" + oSPP.dtSPP.ToSQLFormat() + 
                                " AND tSPD.btJenis= 5 and tSPD.bPPKD= 1  AND tSPDKegiatan.IIDRekening like '5%' ";
                            SSQL = SSQL + " GROUP BY tSPD.inourut,tSPD.sNoSPD, tSPD.dtSPD ORDER BY tSPD.sNoSPD";
                       break;

        
               }
    

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {

                    _lst = (from DataRow dr in dt.Rows
                            select new SPD()
                            {
                                NoUrut = DataFormat.GetLong(dr["iNoURut"]),
                          //      Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            //    Jenis = DataFormat.GetSingle(dr["btJenis"]),
                              //  IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                //Bulan = DataFormat.GetInteger(dr["btBulan"]),
                                //Bulan2 = DataFormat.GetInteger(dr["btBulan2"]),
                                Tanggal = DataFormat.GetDateTime(dr["dtSPD"]),
                                //PPKD = DataFormat.GetSingle(dr["bPPKD"]),
                                NoSPD = DataFormat.GetString(dr["sNoSPD"]),
                                //NamaBendahara = DataFormat.GetString(dr["sBendahara"]),
                                //NamaPPTK = DataFormat.GetString(dr["sPPTK"]),
                                //KetentuanLain = DataFormat.GetString(dr["sKetentuanLain"]),
                                //Triwulan = DataFormat.GetSingle(dr["btTriwulan"]),
                                //Status = DataFormat.GetSingle(dr["iStatus"]),
                                Jumlah = DataFormat.GetDecimal(dr["Jumlah"]),
                                //Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                //IDBendahara = DataFormat.GetInteger(dr["IDBEndahara"]),
                                //INoSPD = DataFormat.GetInteger(dr["iNoSPD"]),
                                //Prefix = DataFormat.GetString(dr["sPrefix"]),
                                //JenisRekening = DataFormat.GetSingle(dr["iJenisRekening"]),
                                //JenisAnggaran = DataFormat.GetInteger(dr["JenisAnggaran"])

                            }).ToList();
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
        public List<SPDDetail> GetDetailSebelumNoUrut(long _lNoUrut, int pIDDInas, Single  iPPKD,bool bPP77= false )
        {
            List<SPDDetail> _lst = new List<SPDDetail>();
            try
            {
                //if (bPP77 == false)
                //{

                SSQL = "SELECT 0 as IDURusan,0 as IDprogram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< " + _lNoUrut.ToString() + " AND B.IDDInas = " + pIDDInas.ToString() + " AND isnull(A.bppkd,0)=" + iPPKD.ToString() +
                            " " +
                            "UNION SELECT A.IDURusan,0 as IDprogram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< " + _lNoUrut.ToString() + " AND B.IDDInas = " + pIDDInas.ToString() + " AND isnull(A.bppkd,0)=" + iPPKD.ToString() +
                            " GROUP BY A.IDURusan " +

                            " UNION SELECT A.IDURusan,A.IDprogram, 0 as IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< " + _lNoUrut.ToString() + " AND B.IDDInas = " + pIDDInas.ToString() + " AND isnull(A.bppkd,0)=" + iPPKD.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram" +
                            " UNION  ALL SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, 0 as IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< " + _lNoUrut.ToString() + " AND B.IDDInas = " + pIDDInas.ToString() + " AND isnull(A.bppkd,0)=" + iPPKD.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan" +
                            " UNION ALL SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan,0 as IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< " + _lNoUrut.ToString() + " AND B.IDDInas = " + pIDDInas.ToString() + " AND isnull(A.bppkd,0)=" + iPPKD.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan" +
                            " UNION ALL SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan,A.IIDRekening, SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< " + _lNoUrut.ToString() + " AND B.IDDInas = " + pIDDInas.ToString() + " AND isnull(A.bppkd,0)=" + iPPKD.ToString() +
                            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan,A.IIDRekening" +

                            " ORDER BY IDURusan,IDprogram, IDKegiatan, IDSubKegiatan,IIDRekening";
                //}
                //else
                //{
                //    SSQL = "SELECT A.IDURusan,A.IDprogram, A.IDKegiatan, 0 as iidrekening ,A.IDSubKegiatan,SUM(A.cJumlah) as Jumlah FROM tSPDKegiatan A INNER JOIN tSPD B ON A.inourut = B.inourut WHERE B.iNoUrut< @pNoUrut  AND B.IDDInas = " + pIDDInas.ToString() +
                //            " GROUP BY A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan" +
                //            " ORDER BY A.IDURusan,A.IDprogram, A.IDKegiatan, A.IDSubKegiatan";

                //}

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pNoUrut", _lNoUrut));

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SPDDetail()
                                {

                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDSubkegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                    Jumlah = DataFormat.GetDecimal(dr["Jumlah"])

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
        public bool UbahDinas(int iddinaslama, int iddinasbaru)
        {
            string SSQL = "";
            SSQL = "UPDATE tKUA set idddinas = " + iddinasbaru.ToString();
return true;
        }
    }
}
