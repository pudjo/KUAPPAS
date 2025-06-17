using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.Bendahara;
using System.Data;
using System.Data.SqlClient;
using Formatting;
using DataAccess;
namespace BP.Bendahara
{
    public class PotonganSPPLogic:BP
    {

        public PotonganSPPLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSPPPotongan";
        }
        
        public bool Simpan(PotonganSPP dh)
        {
            try
            {

                SSQL = "DELETE " + m_sNamaTabel + " WHERE inourut = @NOURUT AND IIDRekeningPotongan=@REKENINGPOTONGAN ";

                DBParameterCollection deleteparameter = new DBParameterCollection();
                deleteparameter.Add(new DBParameter("@NOURUT", dh.NoUrut, DbType.Int64));
                deleteparameter.Add(new DBParameter("@REKENINGPOTONGAN", dh.IIDRekening, DbType.Int64));

                _dbHelper.ExecuteNonQuery(SSQL, deleteparameter);

        //        public string NPWPPenyetor { set; get; }
        //public string NoFaktur { set; get; }
        //public string NIKRekeninan { set; get; }


                 SSQL = "INSERT INTO " + m_sNamaTabel + " (inourut,iidRekeningPotongan, cJumlah, bInformasi,iNo,KodeBilling, KodeSetor, KodeMap,NPWPPenyetor) values ( " +
                          " @pinourut,@piidRekeningPotongan, @pcJumlah, @pbtInformasi,@piNo,@KodeBilling, @KodeSetor, @KodeMap,@NPWPPenyetor) ";//, @piNo, @pOnPerda)";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pinourut", dh.NoUrut, DbType.Int64));
                paramCollection.Add(new DBParameter("@piidRekeningPotongan", dh.IIDRekening, DbType.Int64));
                paramCollection.Add(new DBParameter("@pcJumlah", dh.Jumlah, DbType.Decimal));
                paramCollection.Add(new DBParameter("@pbtInformasi", dh.Informasi, DbType.Single));
                paramCollection.Add(new DBParameter("@piNo", dh.No, DbType.Single));
                paramCollection.Add(new DBParameter("@KodeBilling",dh.IDBilling, DbType.String )); 
                paramCollection.Add(new DBParameter("@KodeSetor",dh.KodeSetor, DbType.String));
                paramCollection.Add(new DBParameter("@KodeMap", dh.KodeMap, DbType.String));
                paramCollection.Add(new DBParameter("@NPWPPenyetor",dh.NPWPPenyetor));

                //paramCollection.Add(new DBParameter("@NoFaktur",dh.NoFaktur));
                //paramCollection.Add(new DBParameter("@NIKRekeninan", dh.NIKRekeninan));
                 //  paramCollection.Add(new DBParameter("@masa_bulan", dh.masa_bulan ));
                //paramCollection.Add(new DBParameter("@masa_tahun", dh.masa_tahun));
                //   paramCollection.Add(new DBParameter("@mata_uang", dh.mata_uang));
                //   paramCollection.Add(new DBParameter("@wp_badan", dh.wp_badan));
                //   paramCollection.Add(new DBParameter("@wp_pemungut", dh.wp_pemungut));
                //   paramCollection.Add(new DBParameter("@wp_op", dh.wp_op));
                //   paramCollection.Add(new DBParameter("@npwp_nol", dh.npwp_nol));
                //   paramCollection.Add(new DBParameter("@npwp_lain", dh.npwp_lain));
                //   paramCollection.Add(new DBParameter("@butuh_nop", dh.butuh_nop));
                //   paramCollection.Add(new DBParameter("@butuh_nosk", dh.butuh_nosk));
                //   paramCollection.Add(new DBParameter("@npwp_rekanan", dh.npwp_rekanan));
                //   paramCollection.Add(new DBParameter("@nik_rekanan", dh.nik_rekanan));
                //   paramCollection.Add(new DBParameter("@nomor_faktur", dh.nomor_faktur));
                //   paramCollection.Add(new DBParameter("@no_skpd", dh.no_skpd));
                //   paramCollection.Add(new DBParameter("@no_spm", dh.no_spm));//






                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                    
                
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;

                return false;
            }
            

        }
        public bool UpdateIDBilling(PotonganSPP p)
        {
            try
            {

                SSQL = "UPDATE tSPPPotongan set KodeBilling= @KODEBILLING  where KodeMap=@KODEMAP AND " +
                    " KodeSetor=@KODESETOR AND inourut=@NOURUT ";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@KODEBILLING", p.IDBilling));
                paramCollection.Add(new DBParameter("@KODEMAP", p.KodeMap));
                paramCollection.Add(new DBParameter("@KODESETOR", p.KodeSetor));
                paramCollection.Add(new DBParameter("@NOURUT", p.NoUrut));

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }
        public List<PotonganSPP> Get( List<long>lstNoUrut)
        {
            List<PotonganSPP> _lst = new List<PotonganSPP>();
            try
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                SSQL = "SELECT * FROM tSPPPotongan inner join mPotongan  on tSPPPotongan.iIDRekeningPotongan = mPotongan.iIDRekeningPotongan ";
                SSQL = SSQL + " WHERE 1>0 ";
                if (lstNoUrut != null )
                {
                    if (lstNoUrut.Count > 0)
                    {
                        int id = 0;
                        string sNamaParameter = "";

                        SSQL = SSQL + " AND  tSPPPotongan.inourut  in ( ";
                        foreach (long nu in lstNoUrut)
                        {
                            sNamaParameter = "@NoUrut" + id.ToString();
                            SSQL = SSQL + sNamaParameter + ",";
                            paramCollection.Add(new DBParameter(sNamaParameter, nu, DbType.Int64));
                            id++;

                        }
                        sNamaParameter = "@NoUrut" + id.ToString();
                        SSQL = SSQL + sNamaParameter + ")";
                        SSQL = SSQL + " AND tSPPPotongan.inourut <> 99"; 
                        paramCollection.Add(new DBParameter(sNamaParameter, 99, DbType.Int64));
                    }
                }
                    
                SSQL =SSQL + " Order by tSPPPotongan.IIDRekeningPotongan ";//

               


                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new PotonganSPP()
                                {
                                    
                                    IIDRekening= DataFormat.GetLong(dr["IIDRekeningPotongan"]),
                                    NoUrut = DataFormat.GetLong(dr["Inourut"]),
                                    Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                    No= DataFormat.GetSingle(dr["ino"]),
                                    Informasi= DataFormat.GetSingle(dr["bInformasi"]),
                                    KodeMap = DataFormat.GetString (dr["KodeMap"]),
                                    KodeSetor = DataFormat.GetString(dr["KodeSetor"]),
                                    IDBilling = DataFormat.GetString(dr["kodebilling"]),
                                    NTPN = DataFormat.GetString(dr["NTPN"]),
                                    Nama = DataFormat.GetString(dr["sNamaPotongan"]),
                                    NPWPPenyetor= DataFormat.GetString(dr["NPWPPenyetor"]),
                                    NoFaktur= DataFormat.GetString(dr["NoFaktur"]),
                                    NIKRekeninan= DataFormat.GetString(dr["NIKRekeninan"]),



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
        public PotonganSPP Get(long nourut ,int _idRekeningPotongan)
        {
            PotonganSPP oPotongan = new PotonganSPP();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " WHERE Inourut =" + nourut.ToString() + " AND IIDRekeningPotongan = " + _idRekeningPotongan.ToString();
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        oPotongan = new PotonganSPP()
                        {

                            IIDRekening = DataFormat.GetLong(dr["IIDRekeningPotongan"]),
                            NoUrut = DataFormat.GetLong(dr["Inourut"]),
                            Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                            No = DataFormat.GetSingle(dr["ino"]),
                            Informasi = DataFormat.GetSingle(dr["bInformasi"])
                               
                        };
                    }
                }
                return oPotongan;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return oPotongan;
            }

        }
        public bool Hapus(long  inourut, int idrekpotongan=0)
        {
            
            try
            {
                SSQL = "DELETE  FROM tSPPPotongan WHERE  inourut = @NOURUT ";

               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@NOURUT", inourut));

                if (idrekpotongan > 0)
                {
                    SSQL = SSQL + " AND iidrekeningPotongan=@REKPOTONGAN";
                    paramCollection.Add(new DBParameter("@REKPOTONGAN", idrekpotongan));
                }
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

    }
}
