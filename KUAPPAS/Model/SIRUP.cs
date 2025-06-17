using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class SIRUP
    {

 public int Tahun {set;get;}
 public string IDPemda { set; get; }
public string  KodePemda {set;get;}//	Kode Pemda sesuai tabel DJPK
public string  KodeData	{set;get;}//Kode Data yang dikirim
public string  KodeAkun {set;get;}////	Kode Akun (1 digit, digit ke-1 dari No Rekening)
public string  UraianAku	{set;get;}// //Uraian Kode Akun sesuai uraian pada tabel rekening
public string  KodeKelompokAkun {set;get;}////	Kode Kelompok Akun (1 digit, digit ke-2 dari No Rekening)
public string  UraianKelompokAkun{set;get;}//	//Uraian Kelompok Akun sesuai uraian pada tabel rekening
public string  KodeJenisAkun{set;get;}//	//Kode Jenis Akun (1 digit, digit ke-3 dari No Rekening)
public string  	UraianJenisAkun{set;get;}//	//Uraian Jenis Akun sesuai uraian pada tabel rekening
public string  	KodeObjekAkun{set;get;}//	Kode Objek Akun (2 digit, digit ke-4 dan ke-5 dari No Rekening)
public string  UraianObjekAkun{set;get;}//	Uraian Objek Akun sesuai uraian pada tabel rekening
public string  	KodeRinciObjek{set;get;}// Akun	Kode Rinci Objek Akun (2 digit, digit ke-6 dan ke-7 dari No Rekening)
public string  	UraianRinciObjek {set;get;}//Akun	Uraian Rinci Objek Akun sesuai uraian pada tabel rekening
public string  	KodeUrusan	{set;get;}//Kode Urusan 2 digit
public string  	UraianUrusan{set;get;}//	Uraian urusan sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
public string  	KodeKelompokUrusan{set;get;}// Urusan/Kategori	Kode Kelompok Urusan 2 digit
public string  	UraianKelompok{set;get;}// Urusan/Kategori	Uraian kelompok urusan sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
public string  	KodeOrganisasi{set;get;}//	Kode Organisasi sesuai urusan 2 digit
public string  	UraianOrganisasi{set;get;}//	Uraian organisasi sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
public string  	KodeSubUnit{set;get;}// Organisasi	Kode Sub Unit Organisasi sesuai urusan 5 digit
public string  	UraianSubUnit{set;get;}// Organisasi	Uraian sub unit organisasi sesuai Kode dan Klasifikasi Urusan Pemda dan Organisasi
public string  	KodeProgram{set;get;}//	Kode Program 2 digit
public string  	UraianProgram{set;get;}//	Uraian Program sesuai Kode dan Daftar Program dan Kegiatan menurut Urusan Pemda
public string  	KodeKegiatan {set;get;}//	Kode Kegiatan 2 digit  kita 3 digit
public string  	UraianKegiatan	{set;get;}//Uraian Kegiatan sesuai Kode dan Daftar Program dan Kegiatan menurut Urusan Pemda
public string  	KodeSubRinciAkun {set;get;}//	Kode Sub Rincian Objek Akun 2 digit 
public string  	UraianSubRinci{set;get;}// Akun	Uraian Sub Rincian Objek Akun
public string  	KodeSubSubRinci {set;get;}//Akun	Kode Sub Sub Rincian Objek Akun 2 digit 
public string  	UraianSubSubRinci {set;get;}// Rinci Akun	Uraian Sub Sub Rincian Objek Akun
public string  	Volume{set;get;}//	Volume
public string  	Satuan{set;get;}//	Satuan 
public string  	HargaSatuan{set;get;}//	Harga Satuan
public string  	KodeFungsi{set;get;}//	Kode Fungsi 2 digit 
public string  	UraianFungsi{set;get;}//	Uraian fungsi sesuai Kode dan Klasifikasi Belanja Pemda menurut Fungsi Pengelolaan Keuangan Negara
public string  	KodeSubFungsi{set;get;}//	Kode Sub Fungsi 2 digit
public string  	UraianSubFungsi {set;get;}//	Uraian sub fungsi sesuai Kode dan Klasifikasi Belanja Pemda menurut Fungsi Pengelolaan Keuangan Negara
public string  	KodeDetilSubFungsi{set;get;}//	Kode Detil Sub Fungsi 2 digit
public string  	UraianDetilSubFungsi{set;get;}//	Uraian detil sub fungsi sesuai Kode dan Klasifikasi Belanja Pemda menurut Fungsi Pengelolaan Keuangan Negara
public string   JumlahRupiah { set; get; }// Jumlah rupiah (nilai) per data atau per elemen


    }
}
