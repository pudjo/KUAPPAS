using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using System.Diagnostics;
using System.IO;

using System.Data.SqlClient;
//using MySql.Data.MySqlClient;

namespace BP
{
    public class ImportRKABapeda:BP
    {

        //    private MySqlConnection connection;
        //    private string server;
        //    private string database;
        //    private string uid;
        //    private string password;
        //    string SSQL;
        //    //Constructor
        //    public ImportRKABapeda(int _pTahun)
        //        : base(_pTahun)
        //    {
        //        Initialize();
        //    }

        public ImportRKABapeda(string _server, string _database, string _uid, string _password, int _pTahun)
            : base(_pTahun)
        {
        }
        //    public ImportRKABapeda(string _server, string _database, string _uid, string _password, int _pTahun)
        //        : base(_pTahun)
        //    {
        //        Tahun = _pTahun;
        //        Initialize();
        //        server = _server;
        //        database = _database;
        //        uid = _uid;
        //        password = _password;
        //        string connectionString;
        //        connectionString = "Server=" + server + ";port=3306;" + "database=" + database + ";" + "UID=" + uid + ";" + "password=" + password + ";";
        //        connection = new MySqlConnection(connectionString);

        //    }

        //    //Initialize values
        //    private void Initialize()
        //    {
        //        string connectionString;
        //        connectionString = "Server=" + server + ";Port=3306;" + "database=" + database + ";" + "UID=" + uid + ";" + "password=" + password + ";";
        //        connection = new MySqlConnection(connectionString);
        //    }


        //    //open connection to database
        //    public bool OpenConnection()
        //    {
        //        try
        //        {
                    
                    
        //            connection.Open();
        //            return true;
        //        }
        //        catch (MySqlException ex)
        //        {
        //            //When handling errors, you can your application's response based on the error number.
        //            //The two most common error numbers when connecting are as follows:
        //            //0: Cannot connect to server.
        //            //1045: Invalid user name and/or password.
        //            _isError = true;
        //            switch (ex.Number)
        //            {
        //                case 0:
                            
        //                    _lastError="Cannot connect to server.  Contact administrator";
                            
        //                    break;

        //                case 1045:
        //                    _lastError = "Invalid username/password, please try again";
                            
        //                    break;
        //            }
        //            return false;
        //        }
        //    }

        //    //Close connection
        //    public bool CloseConnection()
        //    {
        //        try
        //        {
        //            connection.Close();
        //            return true;
        //        }
        //        catch (MySqlException ex)
        //        {
                    
        //            _isError = true;
        //            _lastError = ex.Message;
        //            return false;
        //        }
        //    }

        //public bool TryConnect(SecondConnection _conn)
        //{
        //    //DBConnect _oConnection = new DBConnect(_conn.Server, _conn.Database, _conn.Uid, _conn.Password);
        //    //return _oConnection.OpenConnection();
        //    return true;


        //}
        //public bool Restore(SecondConnection _conn, string sFIle)
        //{
        //    //DBConnect _oConnection = new DBConnect(_conn.Server, _conn.Database, _conn.Uid, _conn.Password);
        //    //return _oConnection.Restore();
        //    return true;
        //}
        //public bool ProsesRENJA()
        //{
        //    List<Renja> mListUnit = new List<Renja>();
        //    RenjcaLogic oLogic = new RenjcaLogic(Tahun);
        //    CloseConnection();
        //    if (this.OpenConnection() == true)
        //    {

        //        //MySqlCommand cmd = new MySqlCommand(SSQL, connection);

        //        //  Musrenmbang m = new Musrenmbang();
        //        SSQL = "select skpd.id_skpd as IDDINas , renja.* from renja  inner join skpd on renja.skpd_id= skpd.skpd_id";


        //        CloseConnection();
        //        OpenConnection();
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
         
        //        MySqlDataReader dr = cmd.ExecuteReader();


        //        while (dr.Read())
        //        {
        //            Renja m = new Renja ();
        //            m.id= DataFormat.GetInteger(dr["renja_id"]);
        //            m.idprogram = DataFormat.GetInteger(dr["program_id"]);
        //            m.idkegiatan = DataFormat.GetInteger(dr["kegiatan_id"]);
        //            m.musrenmbang_id= DataFormat.GetInteger(dr["musrenbang_id"]);
        //            m.usulan = DataFormat.GetString(dr["musrenbang_usulan"]);
        //            m.bPegawai = DataFormat.GetDecimal(dr["bpegawai"]);
        //            m.bModal = DataFormat.GetDecimal(dr["bmodal"]);
        //            m.bbjs = DataFormat.GetDecimal(dr["bbjasa"]);
        //            m.iddinas = DataFormat.GetInteger (dr["IDDInas"]);
        //            mListUnit.Add(m);
        //        }
        //        CloseConnection();


        //    }

        //    if (oLogic.Simpan (mListUnit))
        //    {
        //        _lastError = oLogic.LastError();
        //        _isError = true;
        //        return false;
        //    }
        //    else return true;


        //}
        //public bool ProsesDesa()
        //{
        //    List<Desa> mListUnit = new List<Desa>();
        //    DesaLogic oLogic = new DesaLogic(Tahun);
        //    CloseConnection();
        //    if (this.OpenConnection() == true)
        //    {

        //     //   MySqlCommand cmd = new MySqlCommand(SSQL, connection);

        //        //  Musrenmbang m = new Musrenmbang();
        //        SSQL = "SELECT * from desa";
        //      //  MySqlDataReader dr = cmd.ExecuteReader();
        //        CloseConnection();
        //        OpenConnection();
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);

        //        MySqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            Desa m = new Desa();
        //            m.ID = DataFormat.GetInteger(dr["desa_id"]);
        //            m.Kode = DataFormat.GetInteger(dr["desa_kd"]);
        //            m.Nama = DataFormat.GetString(dr["desa_nama"]);
        //            m.Kecamatan= DataFormat.GetInteger(dr["kecamatan_id"]);
                    
        //            mListUnit.Add(m);
        //        }
        //        CloseConnection();


        //    }

        //    if (oLogic.SimpanImport(mListUnit)==false)
        //    {
        //        _lastError = oLogic.LastError();
        //        _isError = true;
        //        return false;
        //    }
        //    else return true;


        //}
        //public bool ProsesKecamatan()
        //{
        //    List<Kecamatan> mListUnit = new List<Kecamatan>();
        //    KecamatanLogic oLogic = new KecamatanLogic(Tahun);
        //    CloseConnection();
        //    if (this.OpenConnection() == true)
        //    {

                
        //        //  Musrenmbang m = new Musrenmbang();
        //        SSQL = "select * from kecamatan";
                
        //        CloseConnection();
        //        OpenConnection();
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
        //        MySqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            Kecamatan m = new Kecamatan();
        //            m.ID = DataFormat.GetInteger(dr["kecamatan_id"]);
        //            m.Nama= DataFormat.GetString(dr["kecamatan_nama"]);
                    
        //            mListUnit.Add(m);
        //        }
        //        CloseConnection();
        //    }

        //    if (oLogic.SimpanImport(mListUnit))
        //    {
        //        _lastError = oLogic.LastError();
        //        _isError = true;
        //        return false;
        //    }
        //    else return true;


        //}
        //public bool ProsesDusun()
        //{
        //    List<Dusun> mListUnit = new List<Dusun>();
        //    DusunLogic oLogic = new DusunLogic(Tahun);
        //    //CloseConnection();
        //    //if (connection != null && connection.State ==  ConnectionState.Closed)
        //    string tempCon = connection.State.ToString();
            
        //    if (connection.State.ToString() == "Closed")
        //    {
        //        if (this.OpenConnection() == false)
        //        {
        //            _lastError = "Connection Gagal";
        //            return false;

        //        } 
        //    }

           

                
        //        //  Musrenmbang m = new Musrenmbang();
        //        SSQL = "select * from dusun";
        //        CloseConnection();
        //        OpenConnection();
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
    
        //    MySqlDataReader dr = cmd.ExecuteReader();


        //        while (dr.Read())
        //        {
        //            Dusun m = new Dusun();
        //            m.ID= DataFormat.GetInteger(dr["dusun_id"]);
        //            m.Desa = DataFormat.GetInteger(dr["desa_id"]);
        //            m.Kecamatan = DataFormat.GetInteger(dr["kecamatan_id"]);
        //            m.Nama = DataFormat.GetString(dr["dusun_nama"]);
                    
        //            mListUnit.Add(m);
        //        }
        //        CloseConnection();


            

        //    if (oLogic.SimpanImport(mListUnit))
        //    {
        //        _lastError = oLogic.LastError();
        //        _isError = true;
        //        return false;
        //    }
        //    else return true;


        //}
        //public bool ProsesMusrenmbang()
        //{
        // //   List<Musrenmbang> mListUnit = new List<Musrenmbang>();
        // //   MusrenmbangLogic oLogic = new MusrenmbangLogic(Tahun);
        // //   CloseConnection();
        // //   if (this.OpenConnection() == true)
        // //   {

                
        // //     //  Musrenmbang m = new Musrenmbang();
        // //      SSQL = "select  skpd.id_skpd as IDDInas, musrenbang.* from musrenbang inner join skpd  on musrenbang.skpd_id= skpd.skpd_id";

        // ////       SSQL = "select  skpd.id_skpd as IDDInas, musrenbang.* from musrenbang inner join skpd  on musrenbang.skpd_id= skpd.skpd_id";

        // //       CloseConnection();
        // //       OpenConnection();
        // //       MySqlCommand cmd = new MySqlCommand(SSQL, connection);
         
        // //       MySqlDataReader dr = cmd.ExecuteReader();


        // //       while (dr.Read())
        // //       {
        // //           Musrenmbang m = new Musrenmbang();
        // //           m.id = DataFormat.GetInteger(dr["musrenbang_id"]);
        // //           m.IDDInas = DataFormat.GetInteger(dr["IDDInas"]);
        // //           m.IDKegiatan = DataFormat.GetInteger(dr["kegiatan_id"]);
        // //           m.IDProgram = DataFormat.GetInteger(dr["program_id"]);
        // //           m.IDUrusan = DataFormat.GetInteger(dr["program_id"]) / 100;
        // //           m.iTahun = Tahun;
        // //           m.kecamatan_id = DataFormat.GetInteger(dr["kecamatan_id"]);
        // //           m.keterangan = DataFormat.GetString(dr["musrenbang_keterangan"]);
        // //           m.kegiatan_id = DataFormat.GetInteger(dr["kegiatan_id"]);
        // //           m.nama = DataFormat.GetString(dr["musrenbang_usulan"]);
        // //           m.desa_id = DataFormat.GetInteger(dr["desa_id"]);
        // //           m.dusun_id = DataFormat.GetInteger(dr["dusun_id"]);
        // //           m.pagu = DataFormat.GetDecimal(dr["musrenbang_pagu"]);
        // //           m.Pagu2 = DataFormat.GetDecimal(dr["musrenbang_pagu"]);
        // //           m.TahunAPBD = Tahun;
        // //           m.TahapAPBD = 1;


        // //        //   m.status = DataFormat.GetInteger(dr["status"]);

                    
        // //           mListUnit.Add(m);
        // //       }
        // //       CloseConnection();

                
        // //   }

        // //   if (oLogic.Simpan(mListUnit))
        // //   {
        // //       _lastError = oLogic.LastError();
        // //       _isError = true;
        // //       return false;
        // //   }
        // //   else 
        //    return true;
 

        //}
        //public bool SimpanKUA()
        //{
        //    // data diambil dari renja 
        //    // 
        //  //  SSQL " SELECT * from renja INNER JOIN musrenmbang ON renja.idMusrenmbang = musrenmbang.ID "
            
        //    return true;

        //}
        //public void ProcessMasterKegiatan()
        //{
        //    SSQL = "DELETE  FROM mKegiatan ";
        //    _dbHelper.ExecuteNonQuery(SSQL);
        //    SSQL = "SELECT * FROM mkegiatan";
        //    connection.Close();
        //    if (this.OpenConnection() == true)
        //    {
                
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);                
        //        MySqlDataReader dataReader = cmd.ExecuteReader();
        //        MasterKegiatan mstKegiatan = new MasterKegiatan();
        //        MasterKegiatanLogic oLogic = new MasterKegiatanLogic(Tahun);

        //        //Read the data and store them in the list
                
        //        while (dataReader.Read())
        //        {
        //            mstKegiatan.ID = DataFormat.GetInteger(dataReader["id"]);
        //            mstKegiatan.IDProgram = DataFormat.GetInteger(dataReader["idprogram"]);
        //            mstKegiatan.IDUrusan = DataFormat.GetInteger(dataReader["idurusan"]);
        //            mstKegiatan.Nama= DataFormat.GetString(dataReader["snamakegiatan"]);
        //            mstKegiatan.KategoriPelaksana = DataFormat.GetInteger(dataReader["btkodekategoripelaksana"]);
        //            mstKegiatan.UrusanPelaksana = DataFormat.GetInteger(dataReader["btkodeurusanpelaksana"]);
        //            mstKegiatan.Kode = DataFormat.GetInteger(dataReader["btidkegiatan"]);
        //            mstKegiatan.Program = DataFormat.GetInteger(dataReader["btidprogram"]);



        //            oLogic.SimpanImportBapeda( ref mstKegiatan);

        //        }
        //        //close Data Reader
        //        dataReader.Close();
        //        //close Connection
        //        this.CloseConnection();

        //        //return list to be displayed
                
        //    }
        //    else
        //    {
        //        _isError = true;
        //        _lastError = "Koneksi belum terhubung";
        //    }


        //}
        //public void ProcessMasterProgram()
        //{
        //    SSQL = "DELETE  FROM mprogram ";
        //    _dbHelper.ExecuteNonQuery(SSQL);


        //    if (this.OpenConnection() == true)
        //    {

        //        SSQL = "SELECT * FROM mprogram";

        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();
        //        MasterProgram mstprogram = new MasterProgram();

        //        MasterProgramLogic oLogic = new MasterProgramLogic(Tahun);

        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            mstprogram.ID = DataFormat.GetInteger(dataReader["id"]);
        //            mstprogram.IDUrusan = DataFormat.GetInteger(dataReader["idurusan"]);
        //            mstprogram.Nama = DataFormat.GetString(dataReader["snamaprogram"]);
        //            mstprogram.KategoriPelaksana = DataFormat.GetInteger(dataReader["btkodekategoripelaksana"]);
        //            mstprogram.UrusanPelaksana= DataFormat.GetInteger(dataReader["btkodeurusanpelaksana"]);
        //            mstprogram.Kode = DataFormat.GetInteger(dataReader["btidprogram"]);
                    

        //            //mstKegiatan.KategoriPelaksana = DataFormat.GetInteger(dataReader["btkodekategoripelaksana"]);
        //            //mstKegiatan.UrusanPelaksana = DataFormat.GetInteger(dataReader["btKodeurusdanpelaksana"]);
        //            //mstKegiatan.Kode = DataFormat.GetInteger(dataReader["btidprogram"]);



        //            oLogic.SimpanImport(ref mstprogram);


        //        }
        //        //close Data Reader
        //        dataReader.Close();
        //        //close Connection
        //        this.CloseConnection();
        //    }
        //    else
        //    {
        //        _isError = true;
        //        _lastError = "Koneksi belum tersambung";
        //    }

        //}

        //public void ProcessProgram(int _pTahun )
        //{

        //    SSQL = "DELETE FROM tPrograms_A where iTahun =" + _pTahun.ToString();
        //    _dbHelper.ExecuteNonQuery(SSQL);

        //    if (this.OpenConnection() == true)
        //    {
        //        //SSQL = "SELECT * FROM tPrograms_A where iTahun =" + _pTahun.ToString();
        //        SSQL = "SELECT * FROM tPrograms_A ";
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();
        //        TProgramAPBDLogic oLogic = new TProgramAPBDLogic(Tahun);

        //        TProgramAPBD program = new TProgramAPBD();
        //        List<TProgramAPBD> lst = new List<TProgramAPBD>();
                

        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            program.Tahun = _pTahun;
        //            program.IDDinas = DataFormat.GetInteger(dataReader["iddinas"]);
        //            program.IDUrusan = DataFormat.GetInteger(dataReader["idurusan"]);
        //            program.Nama = DataFormat.GetString(dataReader["snamaprogram"]);
        //            program.IDProgram = DataFormat.GetInteger(dataReader["idprogram"]);
        //            program.KodeKategoriPelaksana = DataFormat.GetInteger(dataReader["btkodekategoripelaksana"]);
        //            program.KodeUrusanPelaksana= DataFormat.GetInteger(dataReader["btkodeurusanpelaksana"]);
        //            program.KodeKategori = DataFormat.GetInteger(dataReader["btkodekategori"]);
        //            program.KodeUrusan = DataFormat.GetInteger(dataReader["btkodeurusan"]);
        //            program.KodeSKPD = DataFormat.GetInteger(dataReader["btkodeskpd"]);
        //            program.KodeUK = DataFormat.GetInteger(dataReader["btkodeuk"]);
        //            program.Jenis = DataFormat.GetInteger(dataReader["btjenis"]);                    
        //            lst.Add(program);


                    


        //        }
        //        oLogic.SimpanImport(lst);
        //        //close Data Reader
        //        dataReader.Close();
        //        //close Connection
        //        this.CloseConnection();
        //    }
        //    else
        //    {
        //        _isError = true;
        //        _lastError = "Koneksi belum tersambung";
        //    }


        //}
        //public void ProcessKegiatan(int _pTahun)
        //{
        //    SSQL = "DELETE FROM tKegiatan_A where iTahun =" + _pTahun.ToString();
        //    _dbHelper.ExecuteNonQuery(SSQL);

        //    if (this.OpenConnection() == true)
        //    {
        //        //SSQL = "SELECT * FROM tPrograms_A where iTahun =" + _pTahun.ToString();
        //        SSQL = "SELECT * FROM tKegiatan_A ";
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();
        //        TKegiatanAPBDLogic oLogic = new TKegiatanAPBDLogic(Tahun);

        //        TKegiatanAPBD kegiatan = new TKegiatanAPBD();
        //        List<TProgramAPBD> lst = new List<TProgramAPBD>();


        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            kegiatan.Tahun = _pTahun;
        //            kegiatan.IDDinas = DataFormat.GetInteger(dataReader["iddinas"]);
        //            kegiatan.IDUrusan = DataFormat.GetInteger(dataReader["idurusan"]);
        //            kegiatan.Nama = DataFormat.GetString(dataReader["snama"]);
        //            kegiatan.IDProgram = DataFormat.GetInteger(dataReader["idprogram"]);

        //            int idKegiatan = DataFormat.GetInteger(dataReader["idkegiatan"]);
        //            if (idKegiatan > 0)
        //            {
        //                if (idKegiatan.ToString().Length < 8)
        //                {
        //                    idKegiatan = DataFormat.GetInteger(idKegiatan.ToString().Substring(0, 5) + '0' + idKegiatan.ToString().Substring(5, 2));

        //                }
        //            }
                    
        //            kegiatan.IDKegiatan = idKegiatan;//


        //            kegiatan.KodeKategoriPelaksana = DataFormat.GetInteger(dataReader["btkodekategoripelaksana"]);
        //            kegiatan.KodeUrusanPelaksana = DataFormat.GetInteger(dataReader["btkodeurusanpelaksana"]);
        //            kegiatan.KodeKategori = DataFormat.GetInteger(dataReader["btkodekategori"]);
        //            kegiatan.KodeUrusan = DataFormat.GetInteger(dataReader["btkodeurusan"]);
        //            kegiatan.KodeSKPD = DataFormat.GetInteger(dataReader["btkodeskpd"]);
        //            kegiatan.KodeUK = DataFormat.GetInteger(dataReader["btkodeuk"]);
        //            kegiatan.Jenis = idKegiatan>0? 3:2;
        //            kegiatan.KelompokSasaran = DataFormat.GetString(dataReader["skelompoksasaran"]);
        //            kegiatan.Plafon= DataFormat.GetDecimal(dataReader["cplafon"]);
        //            kegiatan.AnggaranTahunLalu= DataFormat.GetDecimal(dataReader["canggarantahunlalu"]);
        //            kegiatan.AnggaranTahunDepan= DataFormat.GetDecimal(dataReader["canggarantahundepan"]);
        //            kegiatan.Lokasi= DataFormat.GetString(dataReader["slokasi"]);
        //            kegiatan.TanggalPembahasan= DataFormat.GetDateTime(dataReader["dtpembahasan"]);                    
        //            oLogic.SimpanImport(kegiatan);



        //        }
                
        //        //close Data Reader
        //        dataReader.Close();
        //        //close Connection
        //        this.CloseConnection();
        //    }
        //    else
        //    {
        //        _isError = true;
        //        _lastError = "Koneksi belum tersambung";
        //    }


        //}
        //public void ProcessRincian()
        //{

        //}

        //public void ProcessRekening(int _pTahun)
        //{

        //    SSQL = "DELETE FROM tANggaranRekening_A where iTahun =" + _pTahun.ToString();
        //    _dbHelper.ExecuteNonQuery(SSQL);

        //    if (this.OpenConnection() == true)
        //    {
        //        //SSQL = "SELECT * FROM tPrograms_A where iTahun =" + _pTahun.ToString();
        //        SSQL = "SELECT * FROM rkarincian_objek ";
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        List<TAnggaranRekening> lstRekening = new List<TAnggaranRekening>();

        //        TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(Tahun);
        //        TAnggaranRekening rekening = new TAnggaranRekening();
        //        List<TProgramAPBD> lst = new List<TProgramAPBD>();
        //        int iddinas = 0;
        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {                   
        //                TAnggaranRekening o = new TAnggaranRekening();
        //                o.Tahun = _pTahun;
        //                iddinas = DataFormat.GetInteger(dataReader["skpd_id"]);
        //                if (iddinas==0)
        //                    iddinas= DataFormat.GetInteger(dataReader["skpd_id"]);
        //                o.IDDinas = DataFormat.GetInteger(dataReader["skpd_id"]);
        //                //o.Jenis = ctrlJenisAnggaran1.GetID();
        //                int idKegiatan = DataFormat.GetInteger(dataReader["kegiatan_id"]);
        //                if (idKegiatan > 0 && idKegiatan.ToString().Length < 8)
        //                {
        //                    idKegiatan = DataFormat.GetInteger(idKegiatan.ToString().Substring(0, 5) + '0' + idKegiatan.ToString().Substring(5, 2));

        //                }
        //                if (idKegiatan == 0)
        //                {
        //                    o.Jenis = 2;
        //                }
        //                else
        //                {
        //                    o.Jenis = 3;
        //                }

        //                o.IDKegiatan = idKegiatan;
        //                o.IDProgram = DataFormat.GetInteger(dataReader["program_id"]); 
        //                o.IDUrusan =  DataFormat.GetInteger(dataReader["urusan_id"]); 
        //                o.KodeKegiatan = 0;
        //                o.KodeKegiatan = 0;
        //                o.KodeKategoriPelaksana = 0;
        //                o.KodeUrusanPelaksana = 0;                        
                        
        //                //o.PPKD = ctrlDinas1.PPKD();

        //                o.IDRekening = DataFormat.GetLong(dataReader["objek_id"]); 
        //                o.JumlahOlah = 0;
        //                o.Plafon = 0;
        //                o.JumlahYAD = 0;                        
        //                o.StatusUpdate = 0;

        //                o.rincian_ID = DataFormat.GetInteger(dataReader["rkarincianobjek_id"]);
        //                if ((iddinas>0) && o.IDRekening>0)

        //                    lstRekening.Add(o);
                       

        //            }
                    
        //            dataReader.Close();
        //            //close Connection

        //           this.CloseConnection();
                    
        //           //for(int idx = 0; idx<5;idx++)//lstRekening.Count(); idx++)
        //           //{
        //           //    TAnggaranRekening ta = (TAnggaranRekening)lstRekening[idx];
        //           //    lstRekening[idx].ListUraian = GetUraian(ta);
        //           //}
        //           oLogic.SimpanImportBapeda(lstRekening, _pTahun, 0);
                    

        //    }
        //    else
        //    {
        //        _isError = true;
        //        _lastError = "Koneksi belum tersambung";
        //    }


        //}
        //public bool RefreshJumlahImport(int _pTahun){

        //    TAnggaranRekeningLogic oLOgic = new TAnggaranRekeningLogic(_pTahun);
        //    return oLOgic.RefreshJumlahImport(_pTahun);


        //}
        //public  bool ProsesImportUraian(int _pTahun)
        //{
        //    //SSQL = "DELETE FROM tANggaranRekening_A where iTahun =" + _pTahun.ToString();
        //    SSQL = "DELETE FROM  tAnggaranUraian_A where iTahun =" + _pTahun.ToString();
        //    _dbHelper.ExecuteNonQuery(SSQL);
        //    List<TAnggaranUraian> lstUraian = new List<TAnggaranUraian>();
        //    if (this.OpenConnection() == true)
        //    {
        //        //SSQL = "SELECT * FROM tPrograms_A where iTahun =" + _pTahun.ToString();
        //        SSQL = "SELECT rkarincian_uraian.* ,rkarincian_objek.objek_id as rekening, satuan.satuan_simbol as namasatuan FROM rkarincian_uraian inner join rkarincian_objek on rkarincian_objek.rkarincianobjek_id=rkarincian_uraian.rkarincianobjek_id left outer join satuan on rkarincian_uraian.rkarincianuraian_satuan2 = satuan.satuan_id  ";
        //        //SSQL = "SELECT rkarincian_uraian.* FROM rkarincian_uraian  ";
        //        //SSQL = "SELECT rkarincian_uraian.* ,rkarincian_objek.objek_id as rekening FROM rkarincian_uraian inner join rkarincian_objek on rkarincian_objek.rkarincianobjek_id=rkarincian_uraian.rkarincianobjek_id ";
        //        MySqlCommand cmd = new MySqlCommand(SSQL, connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        TAnggaranUraianLogic oLogic = new TAnggaranUraianLogic(Tahun);
        //        TAnggaranUraian rekening = new TAnggaranUraian();                
        //        int iddinas = 0;
        //        //Read the data and store them in the list
        //        decimal jumlah = 0L;
        //        int i = 0;
        //        while (dataReader.Read())
        //        {                   
        //                TAnggaranUraian o = new TAnggaranUraian();
        //                i++;
        //                iddinas = DataFormat.GetInteger(dataReader["skpd_id"]);
        //                if (iddinas==0)
        //                    iddinas= DataFormat.GetInteger(dataReader["skpd_id"]);
        //                o.IDDinas = DataFormat.GetInteger(dataReader["skpd_id"]);
        //                //o.Jenis = ctrlJenisAnggaran1.GetID();
        //                int idKegiatan = DataFormat.GetInteger(dataReader["kegiatan_id"]);
        //                if (idKegiatan > 0 && idKegiatan.ToString().Length < 8)
        //                {

        //                    idKegiatan = DataFormat.GetInteger(idKegiatan.ToString().Substring(0, 5) + '0' + idKegiatan.ToString().Substring(5, 2));

        //                }

        //                o.IDKegiatan =  DataFormat.GetInteger(dataReader["kegiatan_id"]);;
        //                o.IDProgram = DataFormat.GetInteger(dataReader["program_id"]); 
        //                o.IDUrusan =  DataFormat.GetInteger(dataReader["urusan_id"]);                        
        //                o.Tahun = 2018;                                                                  
        //                o.IDLokasi = 0;
        //                o.Level = 1;
        //                o.NoUrut = i;// DataFormat.GetInteger(gridRekening.Rows[i].Cells[COL_NO].Value);
        //                o.PPKD = 0;
        //                o.VolOlah = DataFormat.GetDouble(dataReader["rkarincianuraian_volume"]);
        //                o.Satuan = "";//DataFormat.GetString(dataReader["namasatuan"]);
        //                o.HargaOlah = DataFormat.GetDecimal(dataReader["rkarincianuraian_hargasatuan"]);
        //                o.IDRekening = DataFormat.GetLong(dataReader["rekening"]);
        //                o.IDUraian = DataFormat.GetInteger(dataReader["rkarincianuraian_id"]);
        //                o.Uraian = DataFormat.GetString(dataReader["rkarincianuraian_nama"]);
        //                o.JumlahOlah = DataFormat.GetDecimal(dataReader["rkarincianuraian_jumlah"]);
        //                o.StatusUpdate = 0;
        //                o.Jenis = 3;
        //                o.Label = "";
        //                o.ShowInReport = 0;
        //                o.Plafon = 0;
        //                o.JumlahYAD = 0;
        //                o.rincianID = DataFormat.GetInteger(dataReader["rkarincianrincian_id"]);
        //                o.uraianID = DataFormat.GetInteger(dataReader["rkarincianuraian_id"]);                        
        //                o.ShowInReport = 0;
                  

        //                if ((iddinas>0) && o.IDRekening>0)
        //                //if ((iddinas > 0) )
        //                    lstUraian.Add(o);
                       

        //            }
        //            //'SSQL = "UPDATE tAnggaranRekening_A set cJumlahRKA= " + jumlah.ToString() + ", cJumlahRKABapeda = " + jumlah.ToString() + ", cJumlahOlah = " + jumlah.ToString() + "  where IDRincianBapeda = " + ta.rincian_ID.ToString();
        //            //_dbHelper.ExecuteNonQuery(SSQL); 
        //            dataReader.Close();
        //            CloseConnection();

        //            TAnggaranUraianLogic oLogicUraian = new TAnggaranUraianLogic(Tahun);
        //            oLogicUraian.SimpanUraianImport(lstUraian);
        //            return true;
                    
        //    }
        //    else
        //    {
        //        _isError = true;
        //        _lastError = "Koneksi belum tersambung";
        //        return false;
        //    }


        //}
        //public void ProcessUraian()
        //{

        //}

        ////Select statement
        //public List<string>[] Select()
        //{
        //    string query = "SELECT * FROM tableinfo";

        //    //Create a list to store the result
        //    List<string>[] list = new List<string>[3];
        //    list[0] = new List<string>();
        //    list[1] = new List<string>();
        //    list[2] = new List<string>();

        //    //Open connection
        //    if (this.OpenConnection() == true)
        //    {
        //        //Create Command
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        //Create a data reader and Execute the command
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        //Read the data and store them in the list
        //        while (dataReader.Read())
        //        {
        //            list[0].Add(dataReader["id"] + "");
        //            list[1].Add(dataReader["name"] + "");
        //            list[2].Add(dataReader["age"] + "");
        //        }

        //        //close Data Reader
        //        dataReader.Close();

        //        //close Connection
        //        this.CloseConnection();

        //        //return list to be displayed
        //        return list;
        //    }
        //    else
        //    {
        //        return list;
        //    }
        //}



        

    }
}
