using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using BP;
using Formatting;
using DataAccess;
using System.Data;

namespace BP
{
    public class PerdaLogic:BP
    {
        public PerdaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            
        }
        
        public List<RingkasanPerda> GetRingkasanPerda (int _iTahun,Single _iTahap, ParameterLaporan _p ){
            
            List<RingkasanPerda> _lsttemp = new List<RingkasanPerda>();
            List<RingkasanPerda> _lst = new List<RingkasanPerda>();
            try
            {

                GetKolom(_p.Tahap);
                ProfileReportPerda oRptPerda = new ProfileReportPerda(_p.Tahap);


                string _namaKolom1 = oRptPerda.KolomKiri;
                string _namaKolom2 = oRptPerda.KolomKanan;

                List<RingkasanPerda> _lstPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlPendapatan = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaTidakLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _JmlBelanjaLangsung = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel1 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel2 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel3 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel4 = new List<RingkasanPerda>();
                List<RingkasanPerda> _lstBelanjaLevel5 = new List<RingkasanPerda>();



                if (_p.LastLevel == 3)
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<4  AND IIDrekening > 4000000 GROUP BY Root,IIDrekening, sNamaRekening ";
                else
                    SSQL = "SELECT 1 as Kelompok,0 as Level, Root,IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun=  " + _iTahun.ToString() + "  AND  btJEnis= 1 and Root<6  AND IIDrekening > 4000000 GROUP BY Root,IIDrekening, sNamaRekening ";

                _lstPendapatan = GetBagianRingkasanPerda(SSQL, oRptPerda);
                _lst = _lstPendapatan;

                SSQL = " SELECT 2 as Kelompok,1 as Level,6  as Root, 0 as IIDRekening, 'JUMLAH PENDAPATAN' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis= 1  and Root=1 AND IIDrekening > 4000000  ";
                _JmlPendapatan = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _JmlPendapatan)
                {
                    _lst.Add(p);
                }
                SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1  = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                 {
                        _lst.Add(p);
                  }

                  SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL, oRptPerda);

                  foreach (RingkasanPerda p in _lstBelanjaLevel2)
                  {
                        _lst.Add(p);
                  }
                  SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =2  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                  _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                  foreach (RingkasanPerda p in _lstBelanjaLevel3)
                  {
                        _lst.Add(p);
                    }

                  if (_p.LastLevel == 5)
                    {
                        SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                        _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL, oRptPerda);

                        foreach (RingkasanPerda p in _lstBelanjaLevel4)
                        {
                            _lst.Add(p);
                        }

                        SSQL = " SELECT 3 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2)  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                        _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                        foreach (RingkasanPerda p in _lstBelanjaLevel5)
                        {
                            _lst.Add(p);
                        }
                    }

                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                    foreach (RingkasanPerda p in _lstBelanjaLevel1)
                    {
                        _lst.Add(p);
                    }

                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL, oRptPerda);

                    foreach (RingkasanPerda p in _lstBelanjaLevel2)
                    {
                        _lst.Add(p);
                    }

                    SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =3  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";

                    _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                    foreach (RingkasanPerda p in _lstBelanjaLevel3)
                    {
                        _lst.Add(p);
                    }
                    if (_p.LastLevel == 5)
                    {
                        SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                        _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                        foreach (RingkasanPerda p in _lstBelanjaLevel4)
                        {
                            _lst.Add(p);
                        }
                        SSQL = " SELECT 4 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =3  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                        _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                        foreach (RingkasanPerda p in _lstBelanjaLevel5)
                        {
                            _lst.Add(p);
                        }
                    }

                 SSQL = " SELECT 5 as Kelompok,1 as Level,6 as Root, 0 as IIDRekening, 'JUMLAH BELANJA' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(cDPA) as Jumlah from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis in (2,3) and Root=1 AND IIDrekening > 4000000  ";
                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }
                SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =4  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =4  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }


                SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =4  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel3 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel3)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {


                    SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =4  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }


                    SSQL = " SELECT 6 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =4  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }
                }

                SSQL = " SELECT 7 as Kelompok,1 as Level,6 as Root, 0 as IIDRekening, 'JUMLAH PENERIMAAN PEMBIAYAAN' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(cDPA) as Jumlah from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =4  and Root=1 AND IIDrekening > 4000000  ";
                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }


                SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=1  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel1 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel1)
                {
                    _lst.Add(p);
                }
                SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=2  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }

                SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis  =5  and Root=3  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                _lstBelanjaLevel2 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _lstBelanjaLevel2)
                {
                    _lst.Add(p);
                }

                if (_p.LastLevel == 5)
                {


                    SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=4  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel4 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                    foreach (RingkasanPerda p in _lstBelanjaLevel4)
                    {
                        _lst.Add(p);
                    }

                    SSQL = " SELECT 8 as Kelompok,0 as Level, Root, IIDrekening-3000000 as IIDrekening, sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni ,SUM(" + _namaKolom2 + ") as Jumlah  from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=5  AND IIDrekening > 4000000  GROUP BY Root, IIDrekening, sNamaRekening ";
                    _lstBelanjaLevel5 = GetBagianRingkasanPerda(SSQL, oRptPerda);
                    foreach (RingkasanPerda p in _lstBelanjaLevel5)
                    {
                        _lst.Add(p);
                    }

                }


                SSQL = " SELECT 9 as Kelompok,1 as Level,6 as Root, 0 as IIDRekening, 'JUMLAH PENGELUARAN PEMBIAYAAN' as sNamaRekening,SUM(" + _namaKolom1 + ") as JumlahMurni,SUM(cDPA) as Jumlah from vwAnggaranAllLevel WHERE iTahun= " + _iTahun.ToString() + " AND  btJEnis =5  and Root=1 AND IIDrekening > 4000000  ";
                _JmlBelanjaLangsung = GetBagianRingkasanPerda(SSQL, oRptPerda);
                foreach (RingkasanPerda p in _JmlBelanjaLangsung)
                {
                    _lst.Add(p);
                }



                var cats = from c in _lst
                             .OrderBy(i => i.Kelompok).ThenBy(i => i.IDRekening)
                           select c;
                //.ThenBy(i => i.IDRekening)
                _lsttemp = cats.ToList<RingkasanPerda>();
                return _lsttemp;
/*
                SSQL = SSQL + " ORDER BY Kelompok, IIDRekening ";

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       mListUnit = (from DataRow dr in dt.Rows
                               select new RingkasanPerda()
                               {
                                   
                                   Root = DataFormat.GetInteger(dr["Root"]),
                                   Level = DataFormat.GetSingle(dr["Level"]),
                                   Kelompok= DataFormat.GetSingle(dr["Kelompok"]),
                                   Kode= DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["sNamaRekening"]),                               
                                   JumlahMurni = DataFormat.GetDecimal(dr[_namaKolom1]),
                                   Jumlah = DataFormat.GetDecimal(dr[_namaKolom2])
                                   
                                   
                               }).ToList();
                        } 

                }

                return mListUnit;*/
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }

        }

        public List<RingkasanPerda> GetBagianRingkasanPerda(string SSQL, ProfileReportPerda oRptPerda)
        {

            List<RingkasanPerda> _lst = new List<RingkasanPerda>();            
            string _namaKolom1 = oRptPerda.KolomKiri;
            string _namaKolom2 = oRptPerda.KolomKanan;
            try{
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                       _lst = (from DataRow dr in dt.Rows
                               select new RingkasanPerda()
                               {
                                   IDRekening = DataFormat.GetInteger(dr["IIDRekening"]),
                                   Root = DataFormat.GetInteger(dr["Root"]),
                                   Level = DataFormat.GetInteger(dr["Level"]),
                                   Kelompok= DataFormat.GetInteger(dr["Kelompok"]),
                                   Kode= DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                   Nama = DataFormat.GetString(dr["sNamaRekening"]),                               
                                   JumlahMurni = DataFormat.GetDecimal(dr["JumlahMurni"]),
                                   Jumlah = DataFormat.GetDecimal(dr["Jumlah"])
                                   
                                   
                               }).ToList();
                        } 

                }

                return _lst;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return null;
            }
        }
        
    }
}
