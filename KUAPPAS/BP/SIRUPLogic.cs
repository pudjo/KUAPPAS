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
    public class SIRUPLogic:BP
    {
        public SIRUPLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            //m_sNamaTabel = "mSIRUP",
        }

        public List<SIRUP> Get(int Tahun)
        {
            List<SIRUP> _lst = new List<SIRUP>();
            try
            {
                SSQL = "SELECT * from vwSIRUP";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SIRUP()
                                {
                                 Tahun = Tahun,
                                    IDPemda	= DataFormat.GetString(""),
                                    KodePemda =DataFormat.GetString(""),
                                    KodeData	=DataFormat.GetString(""),//Kode Data yang dikirim
                                    KodeAkun =DataFormat.GetString(""),////	Kode Akun (1 digit, digit ke-1 dari No Rekening)
                                    UraianAku	=DataFormat.GetString(""),// //Uraian Kode Akun sesuai uraian pada tabel rekening
                                    KodeKelompokAkun =DataFormat.GetString(""),////	Kode Kelompok Akun (1 digit, digit ke-2 dari No Rekening)
                                    UraianKelompokAkun=DataFormat.GetString(""),//	//Uraian Kelompok Akun sesuai uraian pada tabel rekening
                                    KodeJenisAkun=DataFormat.GetString(""),//	//Kode Jenis Akun (1 digit, digit ke-3 dari No Rekening)
                                    UraianJenisAkun=DataFormat.GetString(""),//	//Uraian Jenis Akun sesuai uraian pada tabel rekening
                                    KodeObjekAkun=DataFormat.GetString(""),//	Kode Objek Akun (2 digit, digit ke-4 dan ke-5 dari No Rekening)
                                    UraianObjekAkun=DataFormat.GetString(""),//	Uraian Objek Akun sesuai uraian pada tabel rekening
                                    KodeRinciObjek=DataFormat.GetString(""),// Akun	Kode Rinci Objek Akun (2 digit, digit ke-6 dan ke-7 dari No Rekening)
                                    UraianRinciObjek =DataFormat.GetString(""),//Akun	Uraian Rinci Objek Akun sesuai uraian pada tabel rekening
                                    KodeUrusan	=DataFormat.GetString(""),//Kode Urusan 2 digit
                                    UraianUrusan=DataFormat.GetString(""),//	Uraian urusan sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
                                    KodeKelompokUrusan=DataFormat.GetString(""),// Urusan/Kategori	Kode Kelompok Urusan 2 digit
                                    UraianKelompok=DataFormat.GetString(""),// Urusan/Kategori	Uraian kelompok urusan sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
                                    KodeOrganisasi=DataFormat.GetString(""),//	Kode Organisasi sesuai urusan 2 digit
                                    UraianOrganisasi=DataFormat.GetString(""),//	Uraian organisasi sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
                                    KodeSubUnit=DataFormat.GetString(""),// Organisasi	Kode Sub Unit Organisasi sesuai urusan 5 digit
                                    UraianSubUnit=DataFormat.GetString(""),// Organisasi	Uraian sub unit organisasi sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
                                    KodeProgram=DataFormat.GetString(""),//	Kode Program 2 digit
                                    UraianProgram=DataFormat.GetString(""),//	Uraian Program sesuai Kode dan Daftar Program dan Kegiatan menurut Urusan Pemda
                                    KodeKegiatan =DataFormat.GetString(""),//	Kode Kegiatan 2 digit  kita 3 digit
                                    UraianKegiatan	=DataFormat.GetString(""),//Uraian Kegiatan sesuai Kode dan Daftar Program dan Kegiatan menurut Urusan Pemda
                                    KodeSubRinciAkun =DataFormat.GetString(""),//	Kode Sub Rincian Objek Akun 2 digit 
                                    UraianSubRinci=DataFormat.GetString(""),// Akun	Uraian Sub Rincian Objek Akun
                                    KodeSubSubRinci =DataFormat.GetString(""),//Akun	Kode Sub Sub Rincian Objek Akun 2 digit 
                                    UraianSubSubRinci =DataFormat.GetString(""),// Rinci Akun	Uraian Sub Sub Rincian Objek Akun
                                    Volume=DataFormat.GetString(""),//	Volume
                                    Satuan=DataFormat.GetString(""),//	Satuan 
                                    HargaSatuan=DataFormat.GetString(""),//	Harga Satuan
                                    KodeFungsi=DataFormat.GetString(""),//	Kode Fungsi 2 digit 
                                    UraianFungsi=DataFormat.GetString(""),//	Uraian fungsi sesuai Kode dan Klasifikasi Belanja Pemda menurut Fungsi Pengelolaan Keuangan Negara
                                    KodeSubFungsi=DataFormat.GetString(""),//	Kode Sub Fungsi 2 digit
                                    UraianSubFungsi =DataFormat.GetString(""),//	Uraian sub fungsi sesuai Kode dan Klasifikasi Belanja Pemda menurut Fungsi Pengelolaan Keuangan Negara
                                    KodeDetilSubFungsi=DataFormat.GetString(""),//	Kode Detil Sub Fungsi 2 digit
                                    UraianDetilSubFungsi=DataFormat.GetString(""),//	Uraian detil sub fungsi sesuai Kode dan Klasifikasi Belanja Pemda menurut Fungsi Pengelolaan Keuangan Negara
                                 JumlahRupiah = DataFormat.GetString("")// Jumlah rupiah (nilai) per data atau per elemen

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

    }
}
