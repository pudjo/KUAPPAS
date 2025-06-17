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
    public class PK04_01Logic:BP
    {
        public PK04_01Logic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            CekView();
        }
        private void CekView()
        {
            try
            {
                SSQL = "SELECT * from vwRekening2";
                _dbHelper.ExecuteDataTable(SSQL);
                return;
            }
            catch (Exception ex)
            {
                SSQL=" create view vwRekening2 as Select * from mRekening where btRoot=2";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL=" create view vwRekening3 as Select * from mRekening where btRoot=3";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL=" create view vwRekening4 as Select * from mRekening where btRoot=4";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " create view vwRekening5 as Select * from mRekening where btRoot=5";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public List<PMK04_01> Get(int _Tahun, Single _Type)
        {
            List<PMK04_01> _lst = new List<PMK04_01>();
            try
            {
                SSQL = "select mFUngsi.btKodeFungsi, mFUngsi.sNamaFungsi as NamaFungsi,mUrusan.btKodeKategori, ";
                SSQL = SSQL + " case when mUrusan.btKodeKategori=1 Then 'Wajib' else 'Pilihan' end as TypeUrusan, ";
                SSQL = SSQL + " mUrusan.btKodeUrusan  as KodeUrusan, mUrusan.sNamaUrusan as NamaUrusan,  ";
                SSQL = SSQL + " mSKPD.sNamaSKPD, tPrograms_A.sNamaProgram as NamaProgram, ";
                SSQL = SSQL + " tKegiatan_A.sNama  as NamaKegiatan, ";
                SSQL = SSQL + " tAnggaranRekening_A.IIDRekening/1000000 as KodeAkun,mRekening.sNamaRekening as NamaAkun, ";
                SSQL = SSQL + " tAnggaranRekening_A.IIDRekening/100000 as KodeKelompok,vwRekening2.sNamaRekening as NamaKelompok, ";
                SSQL = SSQL + " tAnggaranRekening_A.IIDRekening/10000 as KodeJenis,vwRekening3.sNamaRekening as NamaJenis, ";
                SSQL = SSQL + " tAnggaranRekening_A.IIDRekening/100 as KodeObyek,vwRekening4.sNamaRekening as NamaObyek, ";
                SSQL = SSQL + " tAnggaranRekening_A.IIDRekening as KodeRincian,vwRekening5.sNamaRekening as NamaRincian, ";
                SSQL = SSQL + " tAnggaranRekening_A.cJumlah  ";
                SSQL = SSQL + " from mFungsi INNER JOIN mUrusan on mFUngsi.btKodeFungsi = mUrusan.btKodeFungsi ";
                SSQL = SSQL + " inner join tAnggaranRekening_A ON mUrusan.btKodeKategori = tAnggaranRekening_A.btKodeKategoriPelaksana ";
                SSQL = SSQL + " AND tAnggaranRekening_A.btKodeKategoriPelaksana = mUrusan.btKodeUrusan  ";
                SSQL = SSQL + " INNER JOIN mSKPD ON tAnggaranRekening_A.IDDinas = mSKPD.ID  ";
                SSQL = SSQL + " INNER JOIN mRekening on (tAnggaranRekening_A.IIDRekening/1000000) * 1000000= mRekening.IIDRekening   ";
                SSQL = SSQL + " INNER JOIN vwRekening2 ON vwRekening2.IIDRekening =(tAnggaranRekening_A.IIDRekening/100000) * 100000 ";
                SSQL = SSQL + " INNER JOIN vwRekening3 ON vwRekening3.IIDRekening =(tAnggaranRekening_A.IIDRekening/10000) * 10000 ";
                SSQL = SSQL + " INNER JOIN vwRekening4 ON vwRekening4.IIDRekening =(tAnggaranRekening_A.IIDRekening/100) * 100 ";
                SSQL = SSQL + " INNER JOIN vwRekening5 ON vwRekening5.IIDRekening =tAnggaranRekening_A.IIDRekening ";
                SSQL = SSQL + " INNER JOIN tPrograms_A ON tAnggaranRekening_A.IDProgram = tPrograms_A.IDProgram  and  ";
                SSQL = SSQL + " tAnggaranRekening_A.iTahun  = tPrograms_A.iTahun AND  tAnggaranRekening_A.IDDInas  = tPrograms_A.IDDinas  ";
                SSQL = SSQL + " and tAnggaranRekening_A.IDUrusan  = tPrograms_A.IDUrusan  ";
                SSQL = SSQL + " INNER JOIN tKegiatan_A ON tAnggaranRekening_A.iTahun  = tKegiatan_A.iTahun AND  tAnggaranRekening_A.IDDInas  = tKegiatan_A.IDDinas  ";
                SSQL = SSQL + " and tAnggaranRekening_A.IDUrusan  = tKegiatan_A.IDUrusan and tKegiatan_A.IDProgram = tAnggaranRekening_A.IDProgram ";
                SSQL = SSQL + " AND tKegiatan_A.IDkegiatan = tAnggaranRekening_A.IDKegiatan  ";
                SSQL = SSQL + " ORDER BY mFUngsi.btKodeFungsi, mUrusan.btKodeKategori,mUrusan.btKodeUrusan  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PMK04_01()
                                {
                                    Tahun = _Tahun,
                                    JenisLaporan = "Anggaran",
                                    TypeDaerah = "Kabupaten",
                                    NamaProvinsi= "Kalimantan Barat",
                                    NamaDaerah = "Kabupaten Ketapang",
                                    KodeFungsi = DataFormat.GetString(dr["btKodeFungsi"]),
                                    NamaFungsi = DataFormat.GetString(dr["NamaFungsi"]),
                                    TypeUrusan = DataFormat.GetString(dr["TypeUrusan"]),
                                    KodeUrusan = DataFormat.GetString(dr["KodeUrusan"]),
                                    NamaUrusan = DataFormat.GetString(dr["NamaUrusan"]),
                                    SKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                                    Program = DataFormat.GetString(dr["NamaProgram"]),
                                    Kegiatan= DataFormat.GetString(dr["NamaKegiatan"]),
                                    KodeAkun= DataFormat.GetString(dr["KodeAkun"]),
                                    NamaAkun= DataFormat.GetString(dr["NamaAkun"]),
                                    KodeKelompok= DataFormat.GetString(dr["KodeKelompok"]),
                                    NamaKelompok= DataFormat.GetString(dr["NamaKelompok"]),
                                    KodeJenis= DataFormat.GetString(dr["KodeJenis"]),
                                    NamaJenis = DataFormat.GetString(dr["NamaJenis"]),
                                    KodeObyek= DataFormat.GetString(dr["KodeObyek"]),
                                    NamaObyek= DataFormat.GetString(dr["NamaObyek"]),
                                    KodeRincian= DataFormat.GetString(dr["KodeRincian"]),
                                    RincianObjek = DataFormat.GetString(dr["NamaRincian"]),
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]).ToRupiahInReport(),
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

        public List<PMK04_II> GetLampII(int _iTahun)
        {
            List<PMK04_II> _lst = new List<PMK04_II>();
            try
            {
                SSQL = "select Left(tAnggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") as Rek , mRekening.IIDRekening, mRekening.sNamaRekening, sum(tAnggaranRekening_A.cJumlah) as Jumlah from tAnggaranRekening_A INNER JOIN mRekening " +
                         " ON LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ") = LEFT(mRekening.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")  WHERE mRekening.btROot = 1 " +
                         " AND tAnggaranRekening_A.btJenis = 1  and tAnggaranRekening_A.IIDRekening > 4000000 " +
                         " GROUP BY LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN1.ToString() + ")  , mRekening.IIDRekening, mRekening.sNamaRekening";

                SSQL = SSQL + " UNION ALL select Left(tAnggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") as Rek , mRekening.IIDRekening, mRekening.sNamaRekening, sum(tAnggaranRekening_A.cJumlah)  as Jumlah from tAnggaranRekening_A INNER JOIN mRekening " +
                         " ON LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ") = LEFT(mRekening.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")  WHERE mRekening.btROot = 2 " +
                         " AND tAnggaranRekening_A.btJenis = 1  and tAnggaranRekening_A.IIDRekening > 4000000 " +
                         " GROUP BY LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN2.ToString() + ")  , mRekening.IIDRekening, mRekening.sNamaRekening";

                SSQL = SSQL + " UNION ALL select Left(tAnggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ") as Rek , mRekening.IIDRekening, mRekening.sNamaRekening, sum(tAnggaranRekening_A.cJumlah)  as Jumlah from tAnggaranRekening_A INNER JOIN mRekening " +
                         " ON LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  = LEFT(mRekening.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  WHERE mRekening.btROot = 3 " +
                         " AND tAnggaranRekening_A.btJenis = 1  and tAnggaranRekening_A.IIDRekening > 4000000 " +
                         " GROUP BY LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN3.ToString() + ")  , mRekening.IIDRekening, mRekening.sNamaRekening";
                SSQL = SSQL + " UNION ALL select Left(tAnggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ") as Rek , mRekening.IIDRekening, mRekening.sNamaRekening, sum(tAnggaranRekening_A.cJumlah)  as Jumlah from tAnggaranRekening_A INNER JOIN mRekening " +
                         " ON LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")  = LEFT(mRekening.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")  WHERE mRekening.btROot = 4 " +
                         " AND tAnggaranRekening_A.btJenis = 1  and tAnggaranRekening_A.IIDRekening > 4000000 " +
                         " GROUP BY LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN4.ToString() + ")  , mRekening.IIDRekening, mRekening.sNamaRekening";
                SSQL = SSQL + " UNION ALL select Left(tAnggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ") as Rek , mRekening.IIDRekening, mRekening.sNamaRekening, sum(tAnggaranRekening_A.cJumlah)  as Jumlah from tAnggaranRekening_A INNER JOIN mRekening " +
                         " ON LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  = LEFT(mRekening.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  WHERE mRekening.btROot = 5 " +
                         " AND tAnggaranRekening_A.btJenis = 1  and tAnggaranRekening_A.IIDRekening > 4000000 " +
                         " GROUP BY LEFT (tANggaranRekening_A.IIDRekening," + m_ProfileRekening.LEN5.ToString() + ")  , mRekening.IIDRekening, mRekening.sNamaRekening";

                SSQL = SSQL + " ORDER BY IIDRekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PMK04_II()
                                {
                                    KodeRekening = DataFormat.GetLong(dr["IIDRekening"]).ToKodeRekening(m_ProfileRekening),
                                    NamaRekening = DataFormat.GetString(dr["sNamaRekening"]),
                                    Jumlah = DataFormat.GetDecimal (dr["Jumlah"]).ToRupiahInReport(),
                                    Perda=""
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

        public List<PMK04_IIPerJenisBelanja> GetLampIIPerJenisBelanja(int _iTahun)
        {
            List<PMK04_IIPerJenisBelanja> _lst = new List<PMK04_IIPerJenisBelanja>();
            try
            {
                SSQL = "select mFUngsi.btKodeFungsi, mFUngsi.sNamaFungsi as NamaFungsi,";
                SSQL = SSQL + " mUrusan.btKodeUrusan  as KodeUrusan, mUrusan.sNamaUrusan as NamaUrusan, mSKPD.sNamaSKPD, ";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '511%' THEN cJumlah ELSE 0 END) AS BelanjaPegawai ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '512%' THEN cJumlah ELSE 0 END) AS BelanjaBunga ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '513%' THEN cJumlah ELSE 0 END) AS BelanjaSubsidi ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '514%' THEN cJumlah ELSE 0 END) AS BelanjaHibah ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '515%' THEN cJumlah ELSE 0 END) AS BelanjaBansos ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '516%' THEN cJumlah ELSE 0 END) AS BelanjaBagiHasil ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '517%' THEN cJumlah ELSE 0 END) AS BelanjaBantuan ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '515%' THEN cJumlah ELSE 0 END) AS BelanjaTakTerduga ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '521%' THEN cJumlah ELSE 0 END) AS BelanjaLangsungPegawai ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '522%' THEN cJumlah ELSE 0 END) AS BelanjaBarangJasa ,";
SSQL = SSQL + " SUM(Case when tAnggaranRekening_A.IIDRekening like '523%' THEN cJumlah ELSE 0 END) AS BelanjaModal";
SSQL = SSQL + " from mFungsi INNER JOIN mUrusan on mFUngsi.btKodeFungsi = mUrusan.btKodeFungsi ";
SSQL = SSQL + " inner join tAnggaranRekening_A ON mUrusan.btKodeKategori = tAnggaranRekening_A.btKodeKategoriPelaksana";
SSQL = SSQL + " AND tAnggaranRekening_A.btKodeKategoriPelaksana = mUrusan.btKodeUrusan ";
SSQL = SSQL + " INNER JOIN mSKPD ON tAnggaranRekening_A.IDDinas = mSKPD.ID ";
SSQL = SSQL + " group BY mFUngsi.btKodeFungsi, mFUngsi.sNamaFungsi ,";
SSQL = SSQL + " mUrusan.btKodeUrusan  , mUrusan.sNamaUrusan , ";
SSQL = SSQL + " mSKPD.sNamaSKPD ";
SSQL = SSQL + " ORDER BY mFUngsi.btKodeFungsi, mUrusan.btKodeUrusan  ";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PMK04_IIPerJenisBelanja()
                                {
                                    Fungsi = DataFormat.GetString(dr["NamaFungsi"]),
                                    Urusan = DataFormat.GetString(dr["NamaUrusan"]),
                                    SKPD = DataFormat.GetString(dr["sNamaSKPD"]),
                                    BelanjaPegawai = DataFormat.GetDecimal(dr["BelanjaPegawai"]).ToRupiahInReport(),
                                    BelanjaBunga = DataFormat.GetDecimal(dr["BelanjaBunga"]).ToRupiahInReport(),
                                    BelanjaSubsidi = DataFormat.GetDecimal(dr["BelanjaSubsidi"]).ToRupiahInReport(),
                                    BelanjaHibah = DataFormat.GetDecimal(dr["BelanjaHibah"]).ToRupiahInReport(),
                                    BelanjaBansos = DataFormat.GetDecimal(dr["BelanjaBansos"]).ToRupiahInReport(),
                                    BelanjaBagiHasil = DataFormat.GetDecimal(dr["BelanjaBagiHasil"]).ToRupiahInReport(),
                                    BelanjaBantuan = DataFormat.GetDecimal(dr["BelanjaBantuan"]).ToRupiahInReport(),
                                    BelanjaTakTerduga = DataFormat.GetDecimal(dr["BelanjaTakTerduga"]).ToRupiahInReport(),
                                    BelanjaLangsungPegawai = DataFormat.GetDecimal(dr["BelanjaLangsungPegawai"]).ToRupiahInReport(),
                                    BelanjaBarangJasa = DataFormat.GetDecimal(dr["BelanjaBarangJasa"]).ToRupiahInReport(),
                                    BelanjaModal = DataFormat.GetDecimal(dr["BelanjaModal"]).ToRupiahInReport()
        
                                    
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
