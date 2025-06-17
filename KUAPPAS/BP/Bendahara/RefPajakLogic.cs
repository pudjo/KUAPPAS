using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DTO;
using DTO.Bendahara;
using BP;
using Formatting;
using System.Data;
namespace BP.Bendahara
{
    public class RefPajakLogic:BP
    {
        public RefPajakLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mBatasUP";
        }

        public bool Simpan(RefPajak rp)
        {
            try
            {

                SSQL = "DELETE  RefPajak WHERE kd_map=@KODEMAP and Kd_setor=@KODESETOR";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KODEMAP", rp.kd_map));
                paramCollection.Add(new DBParameter("@KODESETOR", rp.kd_setor));
       

                _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                SSQL = "INSERT INTO RefPajak (kd_map,desc_map,kd_setor,desc_setor,masa_bulan,masa_tahun,mata_uang,wp_badan,wp_pemungut,wp_op,npwp_nol,npwp_lain,butuh_nop,butuh_nosk,npwp_rekanan,nik_rekanan,nomor_faktur,no_skpd,no_spm,keterangan) values ( "+
                    "@kd_map,@desc_map,@kd_setor,@desc_setor,@masa_bulan,@masa_tahun,@mata_uang,@wp_badan,@wp_pemungut,@wp_op,@npwp_nol,@npwp_lain,@butuh_nop,@butuh_nosk,@npwp_rekanan,@nik_rekanan,@nomor_faktur,@no_skpd,@no_spm,@keterangan)";

                paramCollection.Add(new DBParameter("@kd_map",rp.kd_setor));
                paramCollection.Add(new DBParameter("@desc_map",rp.desc_setor));
                paramCollection.Add(new DBParameter("@kd_setor",rp.kd_setor));
                paramCollection.Add(new DBParameter("@desc_setor",rp.desc_setor));
                paramCollection.Add(new DBParameter("@masa_bulan",rp.masa_bulan));
                paramCollection.Add(new DBParameter("@masa_tahun",rp.masa_tahun));
                paramCollection.Add(new DBParameter("@mata_uang",rp.mata_uang));
                paramCollection.Add(new DBParameter("@wp_badan",rp.wp_badan));
                paramCollection.Add(new DBParameter("@wp_pemungut",rp.wp_pemungut));
                paramCollection.Add(new DBParameter("@wp_op",rp.wp_op));
                paramCollection.Add(new DBParameter("@npwp_nol",rp.npwp_nol));
                paramCollection.Add(new DBParameter("@npwp_lain",rp.npwp_lain));
                paramCollection.Add(new DBParameter("@butuh_nop",rp.butuh_nop));
                paramCollection.Add(new DBParameter("@butuh_nosk",rp.butuh_nosk));
                paramCollection.Add(new DBParameter("@npwp_rekanan",rp.npwp_rekanan));
                paramCollection.Add(new DBParameter("@nik_rekanan",rp.nik_rekanan));
                paramCollection.Add(new DBParameter("@nomor_faktur",rp.nomor_faktur));
                paramCollection.Add(new DBParameter("@no_skpd",rp.no_skpd));
                paramCollection.Add(new DBParameter("@no_spm",rp.no_spm));
                paramCollection.Add(new DBParameter("@keterangan", rp.keterangan));
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

        public List<RefPajak> Get()
        {
            List<RefPajak> _lst = new List<RefPajak>();
            try
            {
                SSQL = "select * from ref_pAJAK order by kd_map,Kd_setor";

                DBParameterCollection paramCollection = new DBParameterCollection();
      
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new RefPajak()
                                {
                                    kd_map = DataFormat.GetInteger(dr["kd_map"]),
                                    desc_map = DataFormat.GetString(dr["desc_map"]),
                                    kd_setor= DataFormat.GetInteger(dr["kd_setor"]),
                                    desc_setor = DataFormat.GetString(dr["desc_setor"]),
                                    masa_bulan= DataFormat.GetInteger(dr["masa_bulan"]),
                                    masa_tahun= DataFormat.GetInteger(dr["masa_tahun"]),
                                    mata_uang= DataFormat.GetInteger(dr["mata_uang"]),
                                    wp_badan= DataFormat.GetInteger(dr["wp_badan"]),
                                    wp_pemungut= DataFormat.GetInteger(dr["wp_pemungut"]),
                                    wp_op= DataFormat.GetInteger(dr["wp_op"]),
                                    npwp_nol= DataFormat.GetInteger(dr["npwp_nol"]),
                                    npwp_lain= DataFormat.GetInteger(dr["npwp_lain"]),
                                    butuh_nop= DataFormat.GetInteger(dr["butuh_nop"]),
                                    butuh_nosk= DataFormat.GetInteger(dr["butuh_nosk"]),
                                    npwp_rekanan= DataFormat.GetInteger(dr["npwp_rekanan"]),
                                    nik_rekanan= DataFormat.GetInteger(dr["nik_rekanan"]),
                                    nomor_faktur= DataFormat.GetInteger(dr["nomor_faktur"]),
                                    no_skpd= DataFormat.GetInteger(dr["no_skpd"]),
                                    no_spm= DataFormat.GetInteger(dr["no_spm"]),
              
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


        public bool Hapus(int KdMap, int KdSetor)
        {

            try
            {

                SSQL = "DELETE  RefPajak WHERE kd_map=@KODEMAP and Kd_setor=@KODESETOR";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@KODEMAP", KdMap));
                paramCollection.Add(new DBParameter("@KODESETOR", KdSetor));

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
