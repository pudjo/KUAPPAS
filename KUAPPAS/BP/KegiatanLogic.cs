using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using System.Data;
using DataAccess;
using Formatting;

namespace BP
{
    public class KegiatanLogic:BP
        // Tidak digunakan 
        //
    {
        public KegiatanLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tKegiatan_A";
        }
        public List<Kegiatan> Get()
        {

            List<Kegiatan> _lst = new List<Kegiatan>();
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
                                select new Kegiatan()
                                {
                                    ID = DataFormat.GetInteger(dr["ID"]),
                                    KodeKategoriPelaksana = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]),
                                    KodeUrusanPelaksana = DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]),
                                    KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                    KodeUrusan = DataFormat.GetInteger(dr["btKodeURusan"]),
                                    SKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                    Unit = DataFormat.GetInteger(dr["btKodeUK"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Program = DataFormat.GetInteger(dr["btIDprogram"]),
                                    Kode = DataFormat.GetInteger(dr["btIDKegiatan"]),
                                    Tampilan = DataFormat.GetInteger(dr["btKodeKategoriPelaksana"]).ToString("0") + "." +
                                                DataFormat.GetInteger(dr["btKodeUrusanPelaksana"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btKodeKategori"]).ToString("0") + "." +
                                                DataFormat.GetInteger(dr["btKodeURusan"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btKodeSKPD"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btKodeUK"]).ToString("00") + "." +
                                                DataFormat.GetInteger(dr["btIDProgram"]).ToString("00")+ "." +
                                                DataFormat.GetInteger(dr["btIDKegiatan"]).ToString("000")
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
        //public List<TKegiatanAPBD> GetBySPJ(int _pITahun, int _pDinas, DateTime tanggalAwal, DateTime tanggalAkhir, string sNoUrut, long noUrutSPJUP = 0)
        //{
        //    List<TProgramAPBD> mListUnit = new List<TProgramAPBD>();
        //    try
        //    {

        //        if (noUrutSPJUP == 0)
        //        {
        //            SSQL = "SELECT IDDInas, idurusan,IDProgram,sNamaProgram, SUM(Jumlah) as Jumlah from ( ";
        //            SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram, A.sNamaProgram, SUM(C.cJumlah) as Jumlah FROM tPrograms_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
        //                 " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
        //                  " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
        //                  " AND B.inourut in ( " + sNoUrut + ")" +
        //                  " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.btIDProgram,A.sNamaProgram " +
        //                  " UNION ";
        //            SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram, " +
        //                 " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tPrograms_A A  INNER JOIN tKoreksi B " +
        //                 "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
        //                 " AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
        //                 " AND B.inourut in ( " + sNoUrut + ")" +
        //                  " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram";
        //            SSQL = SSQL + ") A Group by A.IDDInas, A.IDProgram order by A.IDDInas, A.IDProgram ";





        //        }
        //        else
        //        {


        //            SSQL = " SELECT IDDInas, idurusan,IDProgram,sNamaProgram, SUM(Jumlah) as Jumlah from ( ";
        //            SSQL = SSQL + "Select A.IDDInas,A.IDUrusan,A.IDProgram, A.sNamaProgram, SUM(C.cJumlah) as Jumlah FROM tPrograms_A A  INNER JOIN tPanjar B ON B.IDDINas= A.IDDInas and " +
        //                  " A.IDUrusan = B.IDurusan and A.IDProgram = B.IDPRogram INNER JOIN tPanjarRekening C on B.iNourut= C.inourut " +
        //                  " where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
        //                  " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
        //                  " group by A.IDDInas, A.IDUrusan, A.IDProgram, A.btIDProgram,A.sNamaProgram " +
        //                   " UNION ";
        //            SSQL = SSQL + " Select A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram, " +
        //                    " SUM(-1 * C.iDebet1 * C.cJumlah1 )  as Jumlah FROM tPrograms_A A  INNER JOIN tKoreksi B " +
        //                    "  ON A.iTahun= b.iTahun and B.IDDINas= A.IDDInas INNER jOIN  tKoreksiDetail C on A.IDUrusan = C.IDurusan and A.IDProgram = C.IDPRogram " +
        //                    " AND B.iNourut= C.inourut where A.iTahun =" + _pITahun.ToString() + " AND A.IDDInas= " + _pDinas.ToString() +
        //                     " AND B.inourutspjup  =  " + noUrutSPJUP.ToString() +
        //                     " group by A.IDDInas,A.IDUrusan, A.IDProgram, A.sNamaProgram";
        //            SSQL = SSQL + ") A Group by A.IDDInas, A.IDProgram order by A.IDDInas, A.IDProgram ";

        //        }

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                mListUnit = (from DataRow dr in dt.Rows
        //                        select new TProgramAPBD()
        //                        {
        //                            IDDinas = DataFormat.GetInteger(dr["IDDInas"]),
        //                            IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
        //                            IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
        //                            Nama = DataFormat.GetString(dr["sNamaProgram"]),
        //                            Realisasi = DataFormat.GetDecimal(dr["Jumlah"]),
        //                        }).ToList();
        //            }
        //        }
        //        return mListUnit;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return mListUnit;
        //    }
        //}


        public bool Simpan(ref Kegiatan _pKegiatan)
        {


            try
            {
                if (_pKegiatan.ID == 0)
                {
                    int _newID;
                    _newID = Convert.ToInt32(
                            _pKegiatan.Tahun.ToString().Substring(2, 2) +
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.KodeKategoriPelaksana, 1) +
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.KodeUrusanPelaksana, 2) +
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.KodeKategori, 1) +
                            DataFormat.IntToStringWithLeftPad((int)_pKegiatan.KodeUrusan, 2) +
                            DataFormat.IntToStringWithLeftPad(_pKegiatan.SKPD, 2) +
                            DataFormat.IntToStringWithLeftPad(_pKegiatan.Unit, 2) +
                            DataFormat.IntToStringWithLeftPad(_pKegiatan.Program, 2) +
                            DataFormat.IntToStringWithLeftPad(_pKegiatan.Kode, 2));


                    _pKegiatan.ID = _newID;
                    SSQL = "INSERT INTO tKegiatan_A(ID, Tahun,btKodeKategoriPelaksana, btKodeUrusanPelaksana, btKodeKategori, btKodeUrusan,btKodeSKPD, btKodeUK,btIDProgram,sNama) values (" +
                        "@pID, @pTahun,@pbtKodeKategoriPelaksana, @pbtKodeUrusanPelaksana, @pbtKodeKategori, @pbtKodeUrusan,@pbtKodeSKPD,@pbtKodeUK, @pbtIDProgram,@psNama)";

                }
                else
                {

                    SSQL = "UPDATE tKegiatan_A SET sNama= @psNama WHERE ID=@pID";

                }
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", _pKegiatan.ID));
                paramCollection.Add(new DBParameter("@pTahun", _pKegiatan.Tahun));
                paramCollection.Add(new DBParameter("@pbtKodeKategoriPelaksana", _pKegiatan.KodeKategoriPelaksana));
                paramCollection.Add(new DBParameter("@pbtKodeUrusanPelaksana", _pKegiatan.KodeUrusanPelaksana));
                paramCollection.Add(new DBParameter("@pbtKodeKategori", _pKegiatan.KodeKategori));
                paramCollection.Add(new DBParameter("@pbtKodeUrusan", _pKegiatan.KodeUrusan));
                paramCollection.Add(new DBParameter("@pbtKodeSKPD", _pKegiatan.SKPD));
                paramCollection.Add(new DBParameter("@pbtKodeUK", _pKegiatan.Unit));
                paramCollection.Add(new DBParameter("@pbtIDProgram", _pKegiatan.Kode));
                paramCollection.Add(new DBParameter("@psNama", _pKegiatan.Nama));


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
        //public bool Hapus(int _pIDKegiatan)
        //{
        //    try
        //    {

        //        SSQL = "DELETE FROM tKegiatan_A WHERE ID=@pID";
        //        DBParameterCollection paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("@pID", _pIDKegiatan));
        //        if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message + " " + SSQL;
        //        return false;

        //    }

        //}
    }
}
