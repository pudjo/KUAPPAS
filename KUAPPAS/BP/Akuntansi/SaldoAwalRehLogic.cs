using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DTO.Akuntansi;
using Formatting;
using BP;
using DataAccess;
using System.Data;

using Formatting;


namespace BP.Akuntansi
{
    public class SaldoAwalRehLogic : BP 
    {
        string NamaDatabaseTahunDepan = "";
        public SaldoAwalRehLogic(int tahun)
            : base(tahun)
        {

        }
        public List<SaldoAwalRek> GetSaldoAwal (int skpd){
            try
            {
                List<SaldoAwalRek> lst = new List<SaldoAwalRek>();
                SSQL = "SELECT tSaldoAwalRek.*,mRekening.sNamaRekening from tSaldoAwalRek INNER JOIN mRekening On mRekening.IIDrekening=tSaldoAwalRek.IIDrekening  where year(dtsaldo) =@Tahun -1 ";

               DBParameterCollection paramCollection = new DBParameterCollection();

               paramCollection.Add(new DBParameter("@Tahun", Tahun));
               if (skpd > 0)
               {
                   SSQL = SSQL + " AND IDDInas= @DINAS";
                   paramCollection.Add(new DBParameter("@DINAS", skpd));
               }
               SSQL = SSQL + " ORDER BY tSaldoAwalRek.IIDrekening";
               DataTable dt = new DataTable();
               dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
               if (dt != null)
               {
                   if (dt.Rows.Count > 0)
                   {
                       lst = (from DataRow dr in dt.Rows
                               select new SaldoAwalRek()
                               {

                                   IDRekening = DataFormat.GetLong(dr["IIDrekening"]),
                                 
                                   Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                   Debet = DataFormat.GetSingle(dr["idebet"]),
                                   IDDinas = DataFormat.GetInteger(dr["IdDInas"]),
                                   Nama = DataFormat.GetString(dr["sNamaRekening"])


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
        public decimal GetSaldoAwal(int skpd, int tahun ,long iidrekening )
        {
            try
            {
                decimal saldoawal=0;
                SSQL = "SELECT tSaldoAwalRek.*,mRekening.iDebet as saldoNormal from tSaldoAwalRek INNER JOIN mRekening On mRekening.IIDrekening=tSaldoAwalRek.IIDrekening  " +
                    " where year(dtsaldo) =" + tahun.ToString() +" and tSaldoAwalRek.iidrekening= "+ iidrekening.ToString();

                
                if (skpd > 0)
                {
                    SSQL = SSQL + " AND IDDInas="+ skpd.ToString();
                    
                }
                SSQL = SSQL + " ORDER BY tSaldoAwalRek.IIDrekening";
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        saldoawal = DataFormat.GetDecimal(dr["cJumlah"]) * DataFormat.GetInteger(dr["SaldoNormal"]);

                        
                    }
                }
                return saldoawal;


            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return 0;
            }

        }
        public bool Simpan(SaldoAwalRek saldoAwal)
        {
            try
            {
                int jumlahSaldoAwaltersimpan = CekSaldoAwal(saldoAwal);
                if (jumlahSaldoAwaltersimpan == 0)
                {
                    SSQL = "INSERT INTO tSaldoAwalRek (dtSaldo,IIDRekening, IDDInas, cJumlah,iDebet) values (" +
                        "@dtSaldo,@IDRekening, @DInas, @Jumlah,@Debet)";
                    DBParameterCollection paramCollection = new DBParameterCollection();
      
                    paramCollection.Add(new DBParameter("@dtSaldo",saldoAwal.Tanggal,DbType.Date));
                    paramCollection.Add(new DBParameter("@IDRekening",saldoAwal.IDRekening)); 
                    paramCollection.Add(new DBParameter("@DInas",saldoAwal.IDDinas));
                    paramCollection.Add(new DBParameter("@Jumlah",saldoAwal.Jumlah));
                    paramCollection.Add(new DBParameter("@Debet", saldoAwal.Debet));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }
                else
                {
                    SSQL = "UPDATE tSaldoAwalRek SET cJumlah=@Jumlah,iDebet=@Debet  where " +
                         " IIDRekening=@IDrekening and IDDInas =@Dinas";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                   paramCollection.Add(new DBParameter("@Jumlah",saldoAwal.Jumlah));
                    paramCollection.Add(new DBParameter("@Debet", saldoAwal.Debet));
                   
                    paramCollection.Add(new DBParameter("@IDRekening",saldoAwal.IDRekening)); 
                    paramCollection.Add(new DBParameter("@DInas",saldoAwal.IDDinas));
                   

                   

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                return true;
            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;
            }
             
        }
        public bool SimpanSebagaiSaldoAwalTahunBerikut(SaldoAwalRek saldoAwal)
        {
            try
            {
                NamaDatabaseTahunDepan = "KTP2024SinergiSemester";
              //  NamaDatabaseTahunDepan = "KTP2024";

                int jumlahSaldoAwaltersimpan = CekSaldoAwalTahunBerikkut(saldoAwal);
               
                if (jumlahSaldoAwaltersimpan == 0)
                {
                    SSQL = "INSERT INTO " + NamaDatabaseTahunDepan + ".dbo.tSaldoAwalRek (dtSaldo,IIDRekening, IDDInas, cJumlah,iDebet) values (" +
                        "@dtSaldo,@IDRekening, @DInas, @Jumlah,@Debet)";
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("@dtSaldo", saldoAwal.Tanggal, DbType.Date));
                    paramCollection.Add(new DBParameter("@IDRekening", saldoAwal.IDRekening));
                    paramCollection.Add(new DBParameter("@DInas", saldoAwal.IDDinas));
                    paramCollection.Add(new DBParameter("@Jumlah", saldoAwal.Jumlah));
                    paramCollection.Add(new DBParameter("@Debet", saldoAwal.Debet));

                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);


                }
                else
                {
                    SSQL = "UPDATE " + NamaDatabaseTahunDepan + ".dbo.tSaldoAwalRek SET cJumlah=@Jumlah,iDebet=@Debet  where " +
                         " IIDRekening=@IDrekening and IDDInas =@Dinas";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@Jumlah", saldoAwal.Jumlah));
                    paramCollection.Add(new DBParameter("@Debet", saldoAwal.Debet));

                    paramCollection.Add(new DBParameter("@IDRekening", saldoAwal.IDRekening));
                    paramCollection.Add(new DBParameter("@DInas", saldoAwal.IDDinas));




                    _dbHelper.ExecuteNonQuery(SSQL, paramCollection);

                }
                return true;
            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;
            }

        }
        //public bool SImpanSaldoAwalBendahara(int iddinas, decimal Jumlah)
        //{
        //    SaldoAwalRek sa = new SaldoAwalRek();
        //    sa.IDDinas = iddinas;
        //    sa.Jumlah = Jumlah;
        //    sa.Debet = 1;
        //    sa.IDRekening=
        //}
        
        public int CekSaldoAwal(SaldoAwalRek saldoAwal)
        {
            try{
                int lRet = 0;
                SSQL = " SELECT tSaldoAwalRek.* from tSaldoAwalRek  " +
                    "  where IDDInas =@DINAS and IIDRekening= @IDRekening";

               DBParameterCollection paramCollection = new DBParameterCollection();

               paramCollection.Add(new DBParameter("@DINAS", saldoAwal.IDDinas));
               paramCollection.Add(new DBParameter("@IDRekening", saldoAwal.IDRekening));

               

               DataTable dt = new DataTable();
               dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
               if (dt != null)
               {
                   return dt.Rows.Count;

               }
               return 0;


            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return 0;
            }


        }
        public int CekSaldoAwalTahunBerikkut(SaldoAwalRek saldoAwal)
        {
            try
            {
                int lRet = 0;
                SSQL = " SELECT tSaldoAwalRek.* from " + NamaDatabaseTahunDepan + ".dbo.tSaldoAwalRek  " +
                    "  where IDDInas =@DINAS and IIDRekening= @IDRekening";

                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@DINAS", saldoAwal.IDDinas));
                paramCollection.Add(new DBParameter("@IDRekening", saldoAwal.IDRekening));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                if (dt != null)
                {
                    return dt.Rows.Count;

                }
                return 0;


            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return 0;
            }


        }
        public bool Hapus (SaldoAwalRek saldoAwal)
        {
            try
            {
                bool lRet =true ;
                SSQL = "DELETE FROM tSaldoAwalRek  " +
                    "  where IDDInas =@DINAS and IIDRekening= @IDRekening";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@DINAS", saldoAwal.IDDinas));
                paramCollection.Add(new DBParameter("@IDRekening", saldoAwal.IDRekening));



                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                
                return true ;


            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false ;
            }


        }
    }
}
