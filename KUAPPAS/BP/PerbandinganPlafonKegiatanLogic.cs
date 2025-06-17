using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using Formatting;
using System.Data;
using DataAccess;

namespace BP
{
    public class PerbandinganPlafonKegiatanLogic:BP
    {
        public PerbandinganPlafonKegiatanLogic(int _pTahun): base (_pTahun){

        }
        public List<PerbandinganPlafonKegiatan> GetPerbandinganPlafonKegiatan( int _idDias, int _tahun )
        {
            
            //string namaKolomPlafon;
            //string namaKolomDPA;

            //if (pTahap < 3)
            //{
            //    namaKolomPlafon = "cPlafon";
            //    namaKolomDPA= "cJumlahMurni";


            //}
            //else
            //{
            //    namaKolomPlafon = "cPlafonABT";
            //    namaKolomDPA = "cJumlahRKAP";

            //}

            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            try{
            
                SSQL="SELECT tKUA.IDKegiatan, tKUA.Usulan, tKua.JumlahPerubahan as cJumlah , tKegiatan_A.IDkegiatan as Kegiatan2, tKegiatan_A.IDProgram, tKegiatan_A.IDUrusan, tKegiatan_A.sNama, SUM(tANggaranRekening_A.cPlafon ) as cPlafon " +
                    " FROM (tKEgiatan_A INNER JOIN tAnggaranRekening_A ON tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun  " +
                    " and tAnggaranRekening_A.IDUrusan = tKegiatan_A.IDurusan AND tAnggaranRekening_A.IDProgram = tKegiatan_A.IDProgram  " +
                    " AND  tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and tAnggaranRekening_A.IDDInas = tKegiatan_A.IDDInas ) " +
                    " FULL OUTER JOIN tKUA  ON tKUA.iTahun = tKegiatan_A.iTahun  " +
                    " and tKUA.IDUrusan = tKegiatan_A.IDurusan AND tKUA.IDProgram = tKegiatan_A.IDProgram  " +
                    " AND  tKUA.IDKegiatan = tKegiatan_A.IDKegiatan and tKuA.IDDInas = tKegiatan_A.IDDInas  " +                                      
                    "  WHERE tKUA.Status < 9 and tKUA.IDLokasi = 0 and tKUA.IDDInas = "+ _idDias.ToString() +"  group by tKUA.IDKegiatan, tKUA.Usulan, tKua.JumlahOlah , tKegiatan_A.IDkegiatan, tKegiatan_A.IDProgram, tKegiatan_A.IDUrusan, tKegiatan_A.sNama";

                SSQL = "SELECT tAnggaranRekening_A.IDKegiatan, tKUA.Usulan, tKua.JumlahPerubahan as cJumlah , tKegiatan_A.IDkegiatan as Kegiatan2, tKegiatan_A.IDProgram, tKegiatan_A.IDUrusan, tKegiatan_A.sNama, SUM(tANggaranRekening_A.cPlafon ) as cPlafon " +
                    " FROM (tKEgiatan_A INNER JOIN tAnggaranRekening_A ON tAnggaranRekening_A.iTahun = tKegiatan_A.iTahun  " +
                    " and tAnggaranRekening_A.IDUrusan = tKegiatan_A.IDurusan AND tAnggaranRekening_A.IDProgram = tKegiatan_A.IDProgram  " +
                    " AND  tAnggaranRekening_A.IDKegiatan = tKegiatan_A.IDKegiatan and tAnggaranRekening_A.IDDInas = tKegiatan_A.IDDInas ) " +
                    " FULL OUTER JOIN tKUA  ON tKUA.iTahun = tKegiatan_A.iTahun  " +
                    " and tKUA.IDUrusan = tKegiatan_A.IDurusan AND tKUA.IDProgram = tKegiatan_A.IDProgram  " +
                    " AND  tKUA.IDKegiatan = tKegiatan_A.IDKegiatan and tKuA.IDDInas = tKegiatan_A.IDDInas  " +
                    "  WHERE tKUA.Status < 9 and tKUA.IDLokasi = 0 and tKUA.IDDInas = " + _idDias.ToString() + "  group by tKUA.IDKegiatan, tKUA.Usulan, tKua.JumlahOlah , tKegiatan_A.IDkegiatan, tKegiatan_A.IDProgram, tKegiatan_A.IDUrusan, tKegiatan_A.sNama";


                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new PerbandinganPlafonKegiatan ()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDKegiatan2 = DataFormat.GetInteger(dr["Kegiatan2"]),
                                    Nama = DataFormat.GetString(dr["Usulan"]),
                                    Nama2 = DataFormat.GetString(dr["sNama"]),
                                    Plafon = DataFormat.GetDecimal(dr["cJumlah"]),
                                    DPA = DataFormat.GetDecimal(dr["cPlafon"])
                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
            
           
        
        }
        public List<PerbandinganPlafonKegiatan> PerbandinganRekeningPenjabaran(int _idDinas, int Tahun, int pTahap )
        {
            string namaKolomPlafon ;
            string namaKolomDPA ;
            if (pTahap < 3)
            {
                namaKolomPlafon = "cPlafon";
                namaKolomDPA = "cJumlahMurni";
            }
            else
            {
                namaKolomPlafon = "cPlafonABT";
                namaKolomDPA = "cJumlahRKAP";
            }

            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            try{
            SSQL="select A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,0 as IDRekening, SUM(isnull(B." + namaKolomDPA + ",0)) as DPA, SUM(isnull(B." + namaKolomPlafon + ",0)) as Plafon from " +
                    " tKegiatan_A A INNER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDInas = B.IDDinas AND A.IDUrusan = B.IDUrusan and   " +
                    " A.IDProgram = B.IDProgram and A.IDKegiatan = B.IDKegiatan  " +
                    " WHERE A.iTahun = " + Tahun.ToString() + " AND A.IDDInas = " + _idDinas.ToString() +
                    " GROUP BY A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama  " +
                    " UNION ALL  " +
                    " select B.IDUrusan, B.IDProgram, B.IDKegiatan,A.sNamaRekening as sNama,A.IIDRekening,SUM(isnull(B." + namaKolomDPA + ",0)) as DPA, SUM(isnull(B." + namaKolomPlafon + ",0)) as Plafon from   " +
                    " mRekening A INNER JOIN tAnggaranRekening_A B ON A.IIDRekening= B.IIDRekening  " +
                    " WHERE B.iTahun = " + Tahun.ToString() + " AND  B.IDDInas = " + _idDinas.ToString() +
                    " group by B.IDUrusan, B.IDProgram, B.IDKegiatan,A.sNamaRekening, A.IIDRekening" +
                    " Order by IDUrusan, IDProgram, IDKegiatan,IDRekening";


                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows                                
                                select new PerbandinganPlafonKegiatan ()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),                                    
                                    Plafon = DataFormat.GetDecimal(dr["Plafon"]),
                                    DPA = DataFormat.GetDecimal(dr["DPA"])
                                }).ToList();
                    }
                }
                return _lst;
            } catch(Exception ex){
                _isError = true;
                _lastError = ex.Message;
                return _lst;
            }
            
                        

        }
        public List<PerbandinganPlafonKegiatan> PerbandinganDPAANggaranKas(int _idDinas, int Tahun)
        {

            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            try
            {
                if (Tahun <= 2020)
                {
                    SSQL = "select A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,0 as IDRekening, SUM(B.cJumlahABT) as DPA, SUM(C.cBulan1 + C.cBulan2 + C.cBulan3 + C.cBulan4 + C.cBulan5 + C.cBulan6 + C.cBulan7 + C.cBulan8 + C.cBulan9 + C.cBulan10 + C.cBulan11 + C.cBulan12  ) as Plafon from " +
                            " tKegiatan_A A FULL OUTER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDInas = B.IDDinas AND A.IDUrusan = B.IDUrusan and   " +
                            " A.IDProgram = B.IDProgram and A.IDKegiatan = B.IDKegiatan  " +
                            " FULL OUTER JOIN tANggaranKas C ON B.iTahun = C.iTahun and B.IDDInas = C.IDDinas AND B.IDUrusan = C.IDUrusan and   " +
                            " B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan AND B.IIDRekening= c.IIDRekening " +
                            " WHERE A.IDDInas = " + _idDinas.ToString() + " AND c.iTahap=4 " +
                            " GROUP BY A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama  " +
                            " UNION ALL  " +
                            " select B.IDUrusan, B.IDProgram, B.IDKegiatan,A.sNamaRekening as sNama,A.IIDRekening,SUM(B.cJumlahABT) as DPA, SUM(C.cBulan1 + C.cBulan2 + C.cBulan3 + C.cBulan4 + C.cBulan5 + C.cBulan6 + C.cBulan7 + C.cBulan8 + C.cBulan9 + C.cBulan10 + C.cBulan11 + C.cBulan12  ) as Plafon from " +
                            " mRekening A FULL OUTER  JOIN tAnggaranRekening_A B ON A.IIDRekening= B.IIDRekening  " +
                            " FULL OUTER JOIN tANggaranKas C ON B.iTahun = C.iTahun and B.IDDInas = C.IDDinas AND B.IDUrusan = C.IDUrusan and   " +
                            " B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan AND B.IIDRekening= c.IIDRekening " +
                            " WHERE B.IDDInas = " + _idDinas.ToString() + " AND c.iTahap=4 " +
                            " group by B.IDUrusan, B.IDProgram, B.IDKegiatan,A.sNamaRekening, A.IIDRekening" +
                            " Order by IDUrusan, IDProgram, IDKegiatan,IDRekening";
                }
                else
                {//Kegiatan
                    SSQL = "select A.IDUrusan, A.IDProgram, A.IDKegiatan,0 as idsubkegiatan,A.sNama,0 as IDRekening, SUM(B.cJumlahMurni) as DPA, SUM(C.cBulan1 + C.cBulan2 + C.cBulan3 + C.cBulan4 + C.cBulan5 + C.cBulan6 + C.cBulan7 + C.cBulan8 + C.cBulan9 + C.cBulan10 + C.cBulan11 + C.cBulan12  ) as Plafon from " +
                    " tKegiatan_A A FULL OUTER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDInas = B.IDDinas AND A.IDUrusan = B.IDUrusan and   " +
                    " A.IDProgram = B.IDProgram and A.IDKegiatan = B.IDKegiatan  " +
                    " FULL OUTER JOIN tANggaranKas C ON B.iTahun = C.iTahun and B.IDDInas = C.IDDinas AND B.IDUrusan = C.IDUrusan and   " +
                    " B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan " +//AND B.IIDRekening= c.IIDRekening " +
                    " WHERE A.IDDInas = " + _idDinas.ToString() + " AND B.IIDREKENING =c.IIDREKENING and C.itahap=2 " +
                    " GROUP BY A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama  " +
                    " UNION ALL  " +
                    " select A.IDUrusan, A.IDProgram, A.idkegiatan,A.idsubkegiatan ,A.Nama,0 as IDRekening, SUM(B.cJumlahMurni) as DPA, SUM(C.cBulan1 + C.cBulan2 + C.cBulan3 + C.cBulan4 + C.cBulan5 + C.cBulan6 + C.cBulan7 + C.cBulan8 + C.cBulan9 + C.cBulan10 + C.cBulan11 + C.cBulan12  ) as Plafon from " +
                    " tsubkegiatan A FULL OUTER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDInas = B.IDDinas AND A.IDUrusan = B.IDUrusan and   " +
                    " A.IDProgram = B.IDProgram and A.IDKegiatan = B.IDKegiatan and a.idsubkegiatan= B.IDsubkegiatan " +
                    " FULL OUTER JOIN tANggaranKas C ON B.iTahun = C.iTahun and B.IDDInas = C.IDDinas AND B.IDUrusan = C.IDUrusan and   " +
                    " B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan AND B.idsubkegiatan= c.idsubkegiatan AND B.IIDREKENING =c.IIDREKENING " +
                    " WHERE A.IDDInas = " + _idDinas.ToString() + " and C.itahap=2  " +
                    " GROUP BY A.IDUrusan, A.IDProgram,A.idkegiatan, A.idsubkegiatan ,A.Nama  " +

                    " UNION ALL  " +
                    " select B.IDUrusan, B.IDProgram, B.IDkegiatan,B.IDsubKEgiatan as iDkegiatan,A.sNamaRekening as sNama,A.IIDRekening,SUM(B.cJumlahMurni) as DPA, SUM(C.cBulan1 + C.cBulan2 + C.cBulan3 + C.cBulan4 + C.cBulan5 + C.cBulan6 + C.cBulan7 + C.cBulan8 + C.cBulan9 + C.cBulan10 + C.cBulan11 + C.cBulan12  ) as Plafon from " +
                    " mRekening A FULL OUTER  JOIN tAnggaranRekening_A B ON A.IIDRekening= B.IIDRekening  " +
                    " FULL OUTER JOIN tANggaranKas C ON B.iTahun = C.iTahun and B.IDDInas = C.IDDinas AND B.IDUrusan = C.IDUrusan and   " +
                    " B.IDProgram = C.IDProgram and B.IDKegiatan = C.IDKegiatan and B.IDSubKegiatan = C.IDSUbKegiatan  AND B.IIDRekening= c.IIDRekening " +
                    " WHERE B.IDDInas = " + _idDinas.ToString() + " and C.itahap=2 " +
                    " group by B.IDUrusan, B.IDProgram, B.IDKegiatan,B.IDsubKEgiatan,A.sNamaRekening, A.IIDRekening" +


                    " Order by IDUrusan, IDProgram, IDKegiatan,IDsubKEgiatan,IDRekening";


                }

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerbandinganPlafonKegiatan()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDSubKegiatan = DataFormat.GetLong(dr["IDSubKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Plafon = DataFormat.GetDecimal(dr["Plafon"]),
                                    DPA = DataFormat.GetDecimal(dr["DPA"])
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

        public List<PerbandinganPlafonKegiatan> PerbandinganRekeningPenjabaran(int _idDinas, int Tahun, int Jenis, int pTahap)
        {



            List<PerbandinganPlafonKegiatan> _lst = new List<PerbandinganPlafonKegiatan>();
            try
            {
                SSQL = "select A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama,0 as IDRekening, SUM(B.cDPA) as DPA, SUM(B.cPlafon) as Plafon from " +
                        " tKegiatan_A A INNER JOIN tAnggaranRekening_A B ON A.iTahun = B.iTahun and A.IDDInas = B.IDDinas AND A.IDUrusan = B.IDUrusan and   " +
                        " A.IDProgram = B.IDProgram and A.IDKegiatan = B.IDKegiatan  " +
                        " WHERE A.IDDInas = " + _idDinas.ToString() + " AND B.btJenis="+ Jenis.ToString() +
                        " GROUP BY A.IDUrusan, A.IDProgram, A.IDKegiatan,A.sNama  " +
                        " UNION ALL  " +
                        " select B.IDUrusan, B.IDProgram, B.IDKegiatan,A.sNamaRekening as sNama,A.IIDRekening,SUM(B.cDPA) as DPA, SUM(B.cPlafon) as Plafon from   " +
                        " mRekening A INNER JOIN tAnggaranRekening_A B ON A.IIDRekening= B.IIDRekening  " +
                        " WHERE B.IDDInas = " + _idDinas.ToString() + " AND B.btJenis=" + Jenis.ToString() +
                        " group by B.IDUrusan, B.IDProgram, B.IDKegiatan,A.sNamaRekening, A.IIDRekening" +
                        " Order by IDUrusan, IDProgram, IDKegiatan,IDRekening";


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PerbandinganPlafonKegiatan()
                                {
                                    IDUrusan = DataFormat.GetInteger(dr["IDUrusan"]),
                                    IDProgram = DataFormat.GetInteger(dr["IDProgram"]),
                                    IDKegiatan = DataFormat.GetInteger(dr["IDKegiatan"]),
                                    IDRekening = DataFormat.GetLong(dr["IDRekening"]),
                                    Nama = DataFormat.GetString(dr["sNama"]),
                                    Plafon = DataFormat.GetDecimal(dr["Plafon"]),
                                    DPA = DataFormat.GetDecimal(dr["DPA"])
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
