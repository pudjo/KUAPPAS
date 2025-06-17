using DTO;
using DTO.Akuntansi;
using Formatting;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace BP.Akuntansi
{
    public class AnggaranRealisasiLogic : BP
    {
        public AnggaranRealisasiLogic(int tahun) : base(tahun)
        {

        }
        public List<AnggaranRealisasi> GetAnggaranRealisasi(int tahun, int iddinas =0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();

                SSQL = "select p.iTahun,p.btKodeKategoriPelaksana, p.idurusanprogram, p.NamaUrusanProgram , p.kode, p.Kodeuk, p.iddinas, " +
                    " p.IDProgram, p.idKegiatan, p.IDSUbkegiatan, p.Kode,p.NamaProgram,p.NamaKegiatan,p.NamaSubKegiatan,t.iidrekening, t.cJumlahABT,t.cRealisasi, " +
                    " left(t.IIDRekening,2) as KelompokRekening,left(t.IIDRekening,4) as JenisRekening," +
                    " left(t.IIDRekening,6) as ObjectRekening,left(t.IIDRekening,8) as RincianObjectRekening from ProgramKegiatan p inner join tAnggaranRekening_A t on p.iTahun= t.iTahun and  p.IDDinas = t.Iddinas and p.KodeUK= t.btKodeUK  " +
                    " and p.IDSUBkegiatan= t.IDSubKegiatan where t.iTahun = " + tahun.ToString() +
                    " and t.btJenis=3 ";
                if (iddinas > 0)
                {
                    SSQL = SSQL + "and t.IDDinas =" + iddinas.ToString();
                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new AnggaranRealisasi()
                               {
                                   Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                   IDDinas = DataFormat.GetInteger(dr["iddinas"]),
                                   KodeUK = DataFormat.GetInteger(dr["KodeUK"]),
                                   Jenis = 3,
                                   KelompokRekening = DataFormat.GetInteger(dr["KelompokRekening"]),
                                   JenisRekening = DataFormat.GetInteger(dr["JenisRekening"]),
                                   ObjectRekening = DataFormat.GetInteger(dr["ObjectRekening"]),
                                   RincianObjectRekening = DataFormat.GetInteger(dr["RincianObjectRekening"]),
                                   KodeKategori = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                   IDUrusan = DataFormat.GetInteger(dr["idurusanprogram"]),
                                   IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                   IDSUBKegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                   IDRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                   NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                   NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                   NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),
                                   Anggaran = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                   Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]),
                               }).ToList();
                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public List<AnggaranRealisasi> GetAnggaranRealisasiRinci(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();

                SSQL = "select p.iTahun,mSKPD.sNamaSKPD as namaSKPD, " +
                "p.btKodeKategoriPelaksana, p.idurusanprogram ,mUrusan.sNamaUrusan as NamaUrusanProgram, p.kode, p.Kodeuk, p.iddinas, " +
                     "p.IDProgram, p.idKegiatan, p.IDSUbkegiatan, p.Kode,p.NamaProgram,p.NamaKegiatan,p.NamaSubKegiatan,t.iidrekening, t.cJumlahABT,t.cRealisasi, " +
                     "left(t.IIDRekening, 2) as KelompokRekening,left(t.IIDRekening, 4) as JenisRekening," +
                     "left(t.IIDRekening, 6) as ObjectRekening,left(t.IIDRekening, 8) as RincianObjectRekening, p.NamaOrganisasi  as NamaUK from ProgramKegiatan p inner join tAnggaranRekening_A t on p.iTahun = t.iTahun and  p.IDDinas = t.Iddinas and p.KodeUK = t.btKodeUK " +
                     "and p.IDSUBkegiatan = t.IDSubKegiatan INNER JOIN mSKPD " +
                     " ON mSKPD.ID= p.IDDINAS INNER JOIN mURUSAN on mURUSAN.ID = p.idurusanprogram " +
                     " where t.iTahun = " + tahun.ToString() +
                   " and t.btJenis=3 ";
                
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                        select new AnggaranRealisasi()
                        {
                            Tahun = DataFormat.GetInteger(dr["iTahun"]),
                            IDDinas = DataFormat.GetInteger(dr["iddinas"]),
                                   KodeUK = DataFormat.GetInteger(dr["KodeUK"]),
                                   Jenis = 3,
                                   KelompokRekening = DataFormat.GetInteger(dr["KelompokRekening"]),
                                   JenisRekening = DataFormat.GetInteger(dr["JenisRekening"]),
                                   ObjectRekening = DataFormat.GetInteger(dr["ObjectRekening"]),
                                   RincianObjectRekening = DataFormat.GetInteger(dr["RincianObjectRekening"]),
                                   KodeKategori = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                   IDUrusan = DataFormat.GetInteger(dr["idurusanprogram"]),
                                   IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                   IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                   IDSUBKegiatan = DataFormat.GetLong(dr["IDSUBKegiatan"]),
                                   IDRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                   NamaProgram = DataFormat.GetString(dr["NamaProgram"]),
                                   NamaKegiatan = DataFormat.GetString(dr["NamaKegiatan"]),
                                   NamaSubKegiatan = DataFormat.GetString(dr["NamaSubKegiatan"]),
                                   Anggaran = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                   Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]),
                                   NamaDinas= DataFormat.GetString(dr["NamaSKPD"]),
                                   NamaUnit = dr["NamaUK"]==null? DataFormat.GetString(dr["NamaSKPD"]) 
                                                :DataFormat.GetString(dr["NamaUK"]),
                                   NamaUrusan = DataFormat.GetString(dr["NamaUrusanProgram"]),
                                   
                             
                        }).ToList();
                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public List<AnggaranRealisasi> GetAnggaranRealisasiPendapatan(int tahun, int iddinas =0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();

                SSQL = "select t.IDDinas, t.iTahun,left(t.iddinas,1)  as btKodeKategoriPelaksana," +
                    " left(t.iddinas,3) as idurusanprogram,  t.iidrekening, t.cJumlahABT,t.cRealisasi ," +
                    " left(t.IIDRekening,2) as KelompokRekening,left(t.IIDRekening,4) as JenisRekening," +
                    " left(t.IIDRekening,6) as ObjectRekening,left(t.IIDRekening,8) as RincianObjectRekening " +
                    " from tAnggaranRekening_A t where t.btJenis=1 and t.iTahun = " + tahun.ToString(); 
                if (iddinas > 0)
                {
                    SSQL = SSQL + "and t.IDDinas =" + iddinas.ToString();
                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new AnggaranRealisasi()
                               {
                                   Tahun = Tahun,
                                   IDDinas = DataFormat.GetInteger(dr["iddinas"]),
                                   KodeUK = 0,
                                   Jenis = 1,

                                   KodeKategori = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                   IDUrusan = DataFormat.GetInteger(dr["idurusanprogram"]),
                                   IDProgram = 0,
                                   IDKegiatan = 0,
                                   IDSUBKegiatan = 0,
                                   IDRekening = DataFormat.GetLong(dr["iidrekening"]),
                                   KelompokRekening = DataFormat.GetInteger(dr["KelompokRekening"]),
                                   JenisRekening = DataFormat.GetInteger(dr["JenisRekening"]),
                                   ObjectRekening = DataFormat.GetInteger(dr["ObjectRekening"]),
                                   RincianObjectRekening = DataFormat.GetInteger(dr["RincianObjectRekening"]),


                                   NamaProgram = "",
                                   NamaKegiatan = "",
                                   NamaSubKegiatan = "",

                                   Anggaran = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                   Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]),
                               }).ToList();
                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public List<AnggaranRealisasi> GetPenerimaanPembiayaan(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();

                SSQL = "select t.IDDinas, t.iTahun,left(t.iddinas,1)  as btKodeKategoriPelaksana," +
                    " left(t.iddinas,3) as idurusanprogram,  t.iidrekening, t.cJumlahABT,t.cRealisasi , " +
                     " left(t.IIDRekening,2) as KelompokRekening,left(t.IIDRekening,4) as JenisRekening," +
                    " left(t.IIDRekening,6) as ObjectRekening,left(t.IIDRekening,8) as RincianObjectRekening " +
                    " from tAnggaranRekening_A t where t.btJenis=4 and t.iTahun = " + tahun.ToString();
                if (iddinas > 0)
                {
                    SSQL = SSQL + "and t.IDDinas =" + iddinas.ToString();
                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new AnggaranRealisasi()
                               {
                                   Tahun = Tahun,
                                   IDDinas = DataFormat.GetInteger(dr["iddinas"]),
                                   KodeUK = 0,
                                   Jenis = 4,

                                   KodeKategori = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                   IDUrusan = DataFormat.GetInteger(dr["idurusanprogram"]),
                                   IDProgram = 0,
                                   IDKegiatan = 0,
                                   IDSUBKegiatan = 0,
                                   IDRekening = DataFormat.GetLong(dr["iidrekening"]),
                                   NamaProgram = "",
                                   NamaKegiatan = "",
                                   NamaSubKegiatan = "",
                                   KelompokRekening = DataFormat.GetInteger(dr["KelompokRekening"]),
                                   JenisRekening = DataFormat.GetInteger(dr["JenisRekening"]),
                                   ObjectRekening = DataFormat.GetInteger(dr["ObjectRekening"]),
                                   RincianObjectRekening = DataFormat.GetInteger(dr["RincianObjectRekening"]),

                                   Anggaran = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                   Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]),
                               }).ToList();
                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
        public List<AnggaranRealisasi> GetPengeluaranPembiayaan(int tahun, int iddinas = 0)
        {
            try
            {
                List<AnggaranRealisasi> lst = new List<AnggaranRealisasi>();

                SSQL = "select t.IDDinas, t.iTahun,left(t.iddinas,1)  as btKodeKategoriPelaksana," +
                    " left(t.iddinas,3) as idurusanprogram,  t.iidrekening, t.cJumlahABT,t.cRealisasi, " +
                     " left(t.IIDRekening,2) as KelompokRekening,left(t.IIDRekening,4) as JenisRekening," +
                    " left(t.IIDRekening,6) as ObjectRekening,left(t.IIDRekening,8) as RincianObjectRekening " +
                    " from tAnggaranRekening_A t where t.btJenis=5 and t.iTahun = " + tahun.ToString();
                if (iddinas > 0)
                {
                    SSQL = SSQL + "and t.IDDinas =" + iddinas.ToString();
                }
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lst = (from DataRow dr in dt.Rows
                               select new AnggaranRealisasi()
                               {
                                   Tahun = Tahun,
                                   IDDinas = DataFormat.GetInteger(dr["iddinas"]),
                                   KodeUK = 0,
                                   Jenis = 5,

                                   KodeKategori = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                   IDUrusan = DataFormat.GetInteger(dr["idurusanprogram"]),
                                   IDProgram = 0,
                                   IDKegiatan = 0,
                                   IDSUBKegiatan = 0,
                                   IDRekening = DataFormat.GetLong(dr["iidrekening"]),
                                   NamaProgram = "",
                                   NamaKegiatan = "",
                                   NamaSubKegiatan = "",
                                   KelompokRekening = DataFormat.GetInteger(dr["KelompokRekening"]),
                                   JenisRekening = DataFormat.GetInteger(dr["JenisRekening"]),
                                   ObjectRekening = DataFormat.GetInteger(dr["ObjectRekening"]),
                                   RincianObjectRekening = DataFormat.GetInteger(dr["RincianObjectRekening"]),

                                   Anggaran = DataFormat.GetDecimal(dr["cJumlahABT"]),
                                   Realisasi = DataFormat.GetDecimal(dr["cRealisasi"]),
                               }).ToList();
                    }
                }
                return lst;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }
    }
}
