using DataAccess;
using DTO.Bendahara;
using Formatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Bendahara
{
        //   public int IDDInas { set; get; }
        //public int Tahun { set; get; }
        //public int Jenis { set; get; }
        //public long IDRekening { set; get; }
        //public decimal Jumlah { set; get; }
    public class SaldoAwalLogic:BP
    {
        public SaldoAwalLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSaldoLain";
        }
        



        public bool Simpan(SaldoAwal sa)
        {
            try
            {

                SSQL = "DELETE  tSaldoLain WHERE iTahun=@Tahun  and IDDInas=@DInas and Jenis =@jenis and IIDRekening=@REKENING";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DInas", sa.IDDInas));
                paramCollection.Add(new DBParameter("@Tahun", sa.Tahun));
                paramCollection.Add(new DBParameter("@jenis", sa.Jenis));
                paramCollection.Add(new DBParameter("@REKENING",sa.IDRekening));

               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);
               SSQL = "DELETE  tBKU WHERE iTahun=@Tahun  and IDDInas=@DInas and JenisSumber=99 ";
               _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                string sNoUrut = Tahun.ToString().Substring(2, 2);
                        sNoUrut = sNoUrut + (sa.IDDInas ).ToString();
                        sNoUrut = sNoUrut + "99";
                       long no = ReadNo(E_KOLOM_NOURUT.CON_URUT_UP , sa.IDDInas);
               
                        long lNoUrut = DataFormat.GetLong(sNoUrut + "000000") +no;


                        SSQL = "INSERT INTO tSaldoLain (NOURUT,iTahun ,IDDInas,IIDRekening, Jenis,cJumlah, btIDBank) values (" +
                     "@NOURUT,@Tahun ,@DInas,@REKENING ,@jenis,@Jumlah, @IDBank)";
                
                    paramCollection.Add(new DBParameter("@NOURUT",lNoUrut));
                    paramCollection.Add(new DBParameter("@IDBank",sa.IDRekening));
                    paramCollection.Add(new DBParameter("@Jumlah",sa.Jumlah,DbType.Decimal));

              
            
     
               if(_dbHelper.ExecuteNonQuery(SSQL, paramCollection)>0){

                    BKULogic oBKULogic = new BKULogic(sa.Tahun);
                    BKU saldoBKU = new BKU();
                    saldoBKU.IDDinas = sa.IDDInas;
                    saldoBKU.Tahun= sa.Tahun;
                    saldoBKU.Jumlah = sa.Jumlah;
                    saldoBKU.TanggalTransaksi = new DateTime(sa.Tahun  - 1, 12, 31);
                    saldoBKU.Kodebank = sa.Bank;
                    saldoBKU.KodeUk =0;
                    saldoBKU.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                    saldoBKU.NoBKUSKPD = 0;
                    saldoBKU.NoBKU = 0;
                    saldoBKU.NoBukti = "";
                    saldoBKU.NoUrutSaja = 0;
                    saldoBKU.NourutSumber=lNoUrut;
                    saldoBKU.Keterangan = "";
                    saldoBKU.JenisBelanja = 0;
                    saldoBKU.JenisSumber = 99;
                    saldoBKU.Debet = 1;
                    saldoBKU.LevelTampilan = E_LEVLETAMPILANBKU.eBKUHeader;
                    saldoBKU.NoBKU = 0;
                    saldoBKU.NoBKUSKPD = 0;
                    saldoBKU.Position = 2;
                    saldoBKU.JenisBendahara = E_JENISBENDAHARA.BENDAHARA_PENGELUARAN;
                    saldoBKU.PPKD = 0;
                    saldoBKU.UnitAnggaran = 0;
                 
                    bool tersimpan = false;
                    tersimpan = oBKULogic.Simpan(ref saldoBKU);// GetBKUSaldoAwal();
               }
                   return true;


            }
            catch (Exception ex)
            {
                _lastError= ex.Message;
                _isError= true;

                return false;
            }


        }

        public List<SaldoAwal> Get(int _tahun, int dinas)
        {
            List<SaldoAwal> _lst = new List<SaldoAwal>();
            try
            {
                SSQL = "SELECT * from tsaldoLain WHERE IDDInas = @DINAS  and iTahun =@TAHUN ";                
                
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("@TAHUN", _tahun));
               paramCollection.Add(new DBParameter("@DINAS", dinas));
            
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new SaldoAwal()
                                {

                                    Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                        IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                          Jenis = DataFormat.GetInteger(dr["Jenis"]),
                                          IDRekening= DataFormat.GetLong(dr["Jenis"]),
                                    Bank = DataFormat.GetInteger(dr["btIdBank"]),
                                    Jumlah= DataFormat.GetDecimal(dr["cJumlah"]),

              
                                    
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

        public bool Hapus(int IDDInas, int _iTahun, int Jenis, long IIDrekening)
        {

            try
            {

                SSQL = "DELETE  SaldoLain WHERE iTahun=@Tahun  and IDDInas=@DInas and Jenis =@jenis and IIDRekening=@REKENING";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DInas", IDDInas));
                paramCollection.Add(new DBParameter("@Tahun", _iTahun));
                paramCollection.Add(new DBParameter("@jenis", Jenis));
                paramCollection.Add(new DBParameter("@REKENING", IIDrekening));

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
