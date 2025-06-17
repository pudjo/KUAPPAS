using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP;
using DTO;
using System.Data;
using DataAccess;
using Formatting;


namespace BP
{
    public class SubKegiatanBidangLogic : BP
    {
         public SubKegiatanBidangLogic(int _pTahun)
            : base(_pTahun)
        {

        }
         public List<SubKegiatanBidang> GetByDInas(int dinas, int kodeUK, int UnitAnggaran)
         {
             List<SubKegiatanBidang> _lst = new List<SubKegiatanBidang>();
             try
             {
                 //SubKegiatanBidang
                 SSQL = "SELECT t.Nama, SKB.* FROM SubKegiatanBidang SKB INNER JOIN tSubKegiatan t ON SKB.iTahun = SKB.iTahun " +
                        " AND t.IDDInas= SKB.IDDInas and t.btKodeUK = SKB.UnitAnggaran  and and t.IDSUBKegiatan = SKB.IDSUBKegiatan " +
                        " WHERE SKB.IDDInas = @DINAS  and SKB.btKodeUK =@KODEUK  ORDER BY ID";

                 DBParameterCollection paramCollection = new DBParameterCollection();
                 paramCollection.Add(new DBParameter("@DINAS", dinas));
                 paramCollection.Add(new DBParameter("KODEUK", kodeUK));


                 
                 DataTable dt = new DataTable();


                 dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         _lst = (from DataRow dr in dt.Rows
                                 select new SubKegiatanBidang()
                                 {
                                     
                                     IDSUBKegiatan= DataFormat.GetLong(dr["IDSubKegiatan"]),
                                     Nama = DataFormat.GetString(dr["NamaK"])
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
         public bool Simpan(SubKegiatanBidang skb)
         {
             try
             {
                 if (Cek(skb) == 0)
                 {

                     SSQL = "INSERT INTO SubKegiatanBidang(iTahun,IDDInas,btKodeUK,UnitAnggaran,IDSubkegiatan) values (" +
                         "@Tahun,@DInas,@KodeUK,@UnitAnggaran,@IDSubkegiatan)";

                     DBParameterCollection paramCollection = new DBParameterCollection();

                     paramCollection.Add(new DBParameter("@Tahun", skb.iTahun));
                     paramCollection.Add(new DBParameter("@DInas", skb.IDDInas));
                     paramCollection.Add(new DBParameter("@KodeUK", skb.btKodeUK));
                     paramCollection.Add(new DBParameter("@UnitAnggaran", skb.UnitAnggaran));
                     paramCollection.Add(new DBParameter("@IDSubkegiatan", skb.IDSUBKegiatan));

                     _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
                 }

                 return true;


             }
             catch (Exception ex)
             {
                 _isError = true;
                 _lastError = ex.Message + " " + SSQL;
                 return false;

             }



         }
         private int  Cek(SubKegiatanBidang skb)
         {
             try
             {
                 SSQL = "SELECT * FROM SubKegiatanBidang WHERE iTahun=@TAHUN AND IDDInas=@DINAS AND btKodeUK =@KodeUK " +
                        " AND UnitAnggaran =@UNITANGGARAN AND IdSubKegiatan =@IDSUBKEGATAN";

                 DBParameterCollection paramCollection = new DBParameterCollection();

                 paramCollection.Add(new DBParameter("@TAHUN", skb.iTahun));
                 paramCollection.Add(new DBParameter("@DINAS", skb.IDDInas));
                 paramCollection.Add(new DBParameter("@KodeUK", skb.btKodeUK));
                 paramCollection.Add(new DBParameter("@UNITANGGARAN", skb.UnitAnggaran));
                 paramCollection.Add(new DBParameter("=@IDSUBKEGATAN", skb.IDSUBKegiatan));


                 DataTable dt = new DataTable();
                 dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         return dt.Rows.Count;

                     }
                 }
                 return 0;
             }
             catch (Exception ex)
             {
                 _isError = true;
                 _lastError = ex.Message;
                 return 0;
             }
         }
         private bool Hapus(SubKegiatanBidang skb)
         {
             try
             {
                 SSQL = "DELETE FROM SubKegiatanBidang WHERE iTahun=@TAHUN AND IDDInas=@DINAS AND btKodeUK =@KodeUK " +
                        " AND UnitAnggaran =@UNITANGGARAN AND IdSubKegiatan =@IDSUBKEGATAN";

                 DBParameterCollection paramCollection = new DBParameterCollection();

                 paramCollection.Add(new DBParameter("@TAHUN", skb.iTahun));
                 paramCollection.Add(new DBParameter("@DINAS", skb.IDDInas));
                 paramCollection.Add(new DBParameter("@KodeUK", skb.btKodeUK));
                 paramCollection.Add(new DBParameter("@UNITANGGARAN", skb.UnitAnggaran));
                 paramCollection.Add(new DBParameter("=@IDSUBKEGATAN", skb.IDSUBKegiatan));


                 DataTable dt = new DataTable();
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
