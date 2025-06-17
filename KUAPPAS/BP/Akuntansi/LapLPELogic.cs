using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Akuntansi;
using BP;
using DataAccess;
using System.Data;
using Formatting;
using NPOI.SS.Formula.Functions;
using static System.ComponentModel.Design.ObjectSelectorEditor;
namespace BP.Akuntansi
{
    public class LapLPELogic : BP 
    {
        private const string m_IdSurplusDefisit = "310102010001";
        private const string m_strRKPPKD = "310301010001"
;
        public LapLPELogic(int tahun)
            : base(tahun)
        {

        }
    public decimal GetSaldoAwalLPELalu( bool semuaDinas, int dinas) {
        try
        {
            decimal saldoawal = 0;
            // GetSaldoAwalLPELalu = 0
            string codeRekening = "";
            if (Tahun <= 2020)
            {

                codeRekening = "3110101";
            }
            else
            {
                codeRekening = "310101010001";
            }
            DateTime tanggalTerakhirTahunLalu = new DateTime(Tahun, 1, 1);
            //If chkSemuaDinas.Value = vbChecked Then
            if (semuaDinas)
            {
                SSQL = "SELECT  sum(mRekening.iDebet * tSaldoAwalRek.iDebet * tSaldoAwalRek.cJumlah)  " +
                         "as jumlah FROM tSaldoAwalRek inner join mRekening on tSaldoAwalRek.IIDRekening = mRekening.IIDRekening  WHERE  dtSaldo = '12/31/" + (Tahun - 1).ToString() + "' AND tSaldoAwalRek.IIDRekening = " + codeRekening;
            }
            else
            {
                SSQL = "SELECT  sum(mRekening.iDebet * tSaldoAwalRek.iDebet * tSaldoAwalRek.cJumlah)  as jumlah  FROM tSaldoAwalRek inner join mRekening on tSaldoAwalRek.IIDRekening = mRekening.IIDRekening  WHERE  " +
                    " dtSaldo = '12/31/" + (Tahun - 1).ToString() + "' AND tSaldoAwalRek.IIDRekening = " + codeRekening + " AND IDDINAS =" + dinas.ToString() + " AND bPPKD = 0";
            }

            DataTable dt = new DataTable();
            dt = _dbHelper.ExecuteDataTable(SSQL);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    saldoawal = DataFormat.GetDecimal(dr["jumlah"]);
                }
            }



            return saldoawal;
        }
        catch (Exception ex)
        {
            _isError = true;
            _lastError = ex.Message;
            return 0;
        }
    
    


       }
        public decimal GetFromNeracaAwal(string sIIDRekening , bool semuadinas, int dinas) {
           try
            {

                decimal e = 0;

                 //If chkSemuaDinas.Value = vbChecked Then
                     if (semuadinas){
                            SSQL = "SELECT  mRekening.iDebet * sum(TsALDOaWALRek.iDebet * TsALDOaWALRek.cJumlah)  as jumlah FROM TsALDOaWALRek inner join mRekening ON mRekening.IIDRekening = TsALDOaWALRek.IIDRekening "+
                                " WHERE dtsaldo = '12/31/" + (Tahun - 1).ToString() + "'  AND TsALDOaWALRek.IIDRekening =  " + sIIDRekening + " GROUP BY mRekening.iDebet ";
                     } else{
            
                SSQL = "SELECT  mRekening.iDebet * (TsALDOaWALRek.iDebet * TsALDOaWALRek.cJumlah) as Jumlah FROM TsALDOaWALRek  inner join mRekening ON mRekening.IIDRekening = TsALDOaWALRek.IIDRekening "+
                    "WHERE dtsaldo  = '12/31/" + (Tahun - 1).ToString() + "' AND iddinas  =" + dinas.ToString()+
                    " and bPPKD=0 AND TsALDOaWALRek.IIDRekening =  " + sIIDRekening;
                     }
                     DataTable dt = new DataTable();
                     dt = _dbHelper.ExecuteDataTable(SSQL);
                     if (dt != null)
                     {
                         if (dt.Rows.Count > 0)
                         {
                             DataRow dr = dt.Rows[0];
                             e = DataFormat.GetDecimal(dr["jumlah"]);
                         }
                     }


                return e;
            }
            catch (Exception ex)
            {
               _isError = true;
            _lastError = ex.Message;
            return 0;
              
            }
        }
        public  decimal GetValue(string sIIDRekening , bool semuadinas, int dinas) {
           try
            {
                decimal e = 0;
                  SSQL ="";
               
    
    SSQL = "SELECT SUM(iDebet * cJumlah) as Jumlah FROM tBukubesar where year(dtTransaksi)=" + Tahun.ToString()  + 
               " AND IIDRekening = " + m_IdSurplusDefisit;

    DataTable dt = new DataTable();
    dt = _dbHelper.ExecuteDataTable(SSQL);
    if (dt != null)
    {
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            e = DataFormat.GetDecimal(dr["jumlah"]);
        }
    }

                return e;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }

        public decimal GetEkuitasEx(int tahun, int ppkd , bool semuadinas, int dinas, DateTime tanggalawal, DateTime tanggalakhir)
        {
            try
            {

                decimal e = 0;
                SSQL="";
                string kode =m_IdSurplusDefisit;
                string waktu ="";

                if (tahun==Tahun-1){
                    kode="310101010001";
                    waktu ="'12/31/" + (Tahun ).ToString()  + "'";
                } else {
                    waktu = " dttransaksi between "+ tanggalawal.ToSQLFormat() +" and "+ tanggalakhir.ToSQLFormat();
                }

                if (semuadinas == false)
                {

                    if (ppkd == 1)
                    {
                        SSQL = "Select  SUM(cJumlah) as X  from tSaldoAwalRek WHERE IIDRekening = " + kode + "and  dtSaldo  = '12/31/" + (Tahun-1).ToString() + "' AND bPPKD=1";
         
                    }
                    else
                    {
                        SSQL = "Select  SUM(cJumlah) as X  from tSaldoAwalRek WHERE IIDRekening = " + kode + "and  dtSaldo  = '12/31/" + (Tahun-1).ToString() + "' AND bPPKD=" + ppkd.ToString();
                    }

                }
                else
                {
                   /* if (ppkd == 1)
                    {
                        SSQL = "Select  SUM(cJumlah) as X  from tSaldoAwalRek WHERE IIDRekening = " + kode + " and dtSaldo  = '12/31/" + (Tahun - 1).ToString() + "' AND " +
                         " iddinas = " + dinas.ToString() + " AND bPPKD=1 ";
                    }
                    else
                    {
                        SSQL = "Select  SUM(cJumlah) as X  from tSaldoAwalRek WHERE IIDRekening = " + kode + " and dtSaldo  = '12/31/" + (Tahun - 1).ToString() + "' AND " +
                             " iddinas = " + dinas.ToString() + " AND bPPKD=" + ppkd.ToString();
                    }*/
                     if (tahun == Tahun-1)
                    {
                        SSQL = "Select  SUM( cJumlah) as X  from tBukubesar WHERE IIDRekening =  " + kode + 
                          " AND dttransaksi between '1/1/" + Tahun.ToString() + "' and '12/31/" + Tahun.ToString() +  "'";//
                         
                
                    }
                    else
                    {
                        SSQL = "Select  SUM( cJumlah) as X  from tBukubesar WHERE IIDRekening = " + kode + 
                         " AND dttransaksi between '1/1/" + Tahun.ToString() + "' and "+ tanggalakhir.ToSQLFormat();
                          
                    }
                   // If iTahun = g_nTahun - 1 Then
           // SSQL = "Select  SUM( cJumlah) as X  from tBukubesar WHERE IIDRekening = 310101010001  " & _
           //" AND dttransaksi between '1/1/" & CStr(iTahun) & "' and '12/31/" & CStr(iTahun) & "'"
        //Else
        //SSQL = "Select  SUM( cJumlah) as X  from tBukubesar WHERE IIDRekening = 310101010001  and  iTahun = " & iTahun & _
         //  " AND dttransaksi between " & ctrlPilihanwaktu1.GetSQLAwal & _
         //  " AND " & ctrlPilihanwaktu1.GetSQLAkhir
         //End If
    
                }
          
        
 
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        e = DataFormat.GetDecimal(dr["X"]);
                    }
                }

                return e;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
        public  decimal GetSurplusDefisit(int tahun, bool semuadinas, int dinas , int ppkd ,
            DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            try
            {
                decimal e = 0;
                SSQL ="";
                
                decimal cJumlahBeban =0;
                decimal cJumlahPendapatan =0;
    
                string  strBeban ="";
                string strpendapatan ="";
                            string sTahun= tahun.ToString();
                if(tahun <= 2020){
    
                    strBeban = "9";
    
                   strpendapatan = "8";
                } else{
    
                    strBeban = "8";
                    strpendapatan = "7";
                }
    
    
                //If chkSemuaDinas.Value = vbUnchecked Then
                if (semuadinas== false){
                    if (ppkd==1){
                        SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE IIDRekening like '" + strBeban + "%' and  itahun = " + sTahun + " AND dttransaksi between " + tanggalAwal.ToSQLFormat() + 
                                    " AND " + tanggalAkhir.ToSQLFormat() + " AND bPPKD=1 and btJenisJurnal<10 ";
   
                    } else{
                        if (tahun ==Tahun-1){
                             SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND  IIDRekening like '" + strBeban + "%' AND iddinas = "+ dinas.ToString() + 
                                 " AND dttransaksi between '1/1/" + sTahun + "' and '12/31/" + sTahun + "'";

             
                        } else{
                                SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE   btJenisJurnal<10  AND   IIDRekening like '" + strBeban + "%' and  itahun = " + sTahun + " AND "+
                                    " iddinas ="+ dinas.ToString() + " AND dttransaksi between " + tanggalAwal.ToSQLFormat() + 
                             " AND " + tanggalAkhir.ToSQLFormat() + " AND bPPKD=" + ppkd.ToString();
                        }

                    }

                } else {
                        if (tahun== Tahun-1){
                            SSQL = "Select  SUM( iDebet* cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND   IIDRekening like '" + strBeban + "%' " + 
                           " AND dttransaksi between '1/1/" + sTahun + "' and '12/31/" + sTahun + "'";
                        } else {
                            SSQL = "Select  SUM( iDebet* cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND   IIDRekening like '" + strBeban + "%' and  itahun = " + sTahun + 
                       " AND dttransaksi between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                        }
                }
                 DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        cJumlahBeban = DataFormat.GetDecimal(dr["X"]);
                    }
                }

                // penerimaan
                if (semuadinas == false)
                {
                    if (ppkd == 1)
                    {
                        SSQL = "Select  SUM( -1 *Idebet * cJumlah) as X  from tBukubesar WHERE IIDRekening like '" + strpendapatan + "%' and  itahun.ToString() = " + sTahun + " AND dttransaksi between " + tanggalAwal.ToSQLFormat() +
                                  " AND " + tanggalAkhir.ToSQLFormat() + " AND bPPKD=1 and btJenisJurnal<10 ";

                    }
                    else
                    {
                        if (tahun == Tahun - 1)
                        {
                            SSQL = "Select  SUM( -1 *Idebet * cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND  IIDRekening like '" + strpendapatan + "%' AND iddinas = " + dinas.ToString() +
                                " AND dttransaksi between '1/1/" + sTahun + "' and '12/31/" + sTahun + "'";


                        }
                        else
                        {
                            SSQL = "Select  SUM( -1 *Idebet * cJumlah) as X  from tBukubesar WHERE   btJenisJurnal<10  AND   IIDRekening like '" + strpendapatan + "%' and  itahun= " + sTahun + " AND " +
                                " iddinas =" + dinas.ToString() + " AND dttransaksi between " + tanggalAwal.ToSQLFormat() +
                         " AND " + tanggalAkhir.ToSQLFormat() + " AND bPPKD=" + ppkd.ToString();
                        }

                    }

                }
                else
                {
                    if (tahun == Tahun - 1)
                    {
                        SSQL = "Select  SUM(  -1 *iDebet* cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND   IIDRekening like '" + strpendapatan + "%' " +
                       " AND dttransaksi between '1/1/" + sTahun + "' and '12/31/" + sTahun + "'";
                    }
                    else
                    {
                        SSQL = "Select  SUM( -1 * iDebet* cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND   IIDRekening like '" + strpendapatan + "%' and  itahun = " + sTahun +
                   " AND dttransaksi between " + tanggalAwal.ToSQLFormat() + " AND " + tanggalAkhir.ToSQLFormat();
                    }
                }
                DataTable dtTerima = new DataTable();
                dtTerima = _dbHelper.ExecuteDataTable(SSQL);
                if (dtTerima != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow drTerima = dtTerima.Rows[0];
                        cJumlahPendapatan = DataFormat.GetDecimal(drTerima["X"]);
                    }
                }

                e = cJumlahPendapatan - cJumlahBeban;
    
                return e;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
       
            }
        }

        public decimal GetRKPPKD(bool semuadinas , int dinas,int tahun, DateTime tanggalawal, DateTime tanggalakhir)
        {
            try
            {

                 decimal cRKPPKD=0;
                SSQL ="";
    
            if ( semuadinas== false) {       
                if (cRKPPKD==1){
                    SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE IIDRekening = " + m_strRKPPKD + " and  itahun = " + tahun.ToString() + " AND dttransaksi between " + tanggalawal.ToSQLFormat() + 
                            " AND " + tanggalakhir.ToSQLFormat();
                } else {
                    if (tahun==Tahun-1){
                         SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE IIDRekening = " + m_strRKPPKD + " AND iddinas ="+ dinas.ToString() + " AND dttransaksi between '1/1/" + tahun.ToString() + "' and '12/31/" + tahun.ToString() + "'";
                  
                    } else{
                         SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE IIDRekening = " + m_strRKPPKD + "  and  itahun = " + tahun.ToString() + "  AND iddinas= "+ dinas.ToString() + " AND dttransaksi between " + tanggalawal.ToSQLFormat() + 
                        " AND " + tanggalakhir.ToSQLFormat() + " AND bPPKD=" + cRKPPKD.ToString();
                    }
                } 
        
            } else{
                  if (tahun == Tahun-1){
                        SSQL = "Select  SUM( iDebet* cJumlah) as X  from tBukubesar WHERE IIDRekening = " + m_strRKPPKD + "  " + 
                       " AND dttransaksi between '1/1/" + tahun.ToString() + "' and '12/31/" + tahun.ToString() + "'";

                    } else {
                            SSQL = "Select  SUM( iDebet* cJumlah) as X  from tBukubesar WHERE IIDRekening = " + m_strRKPPKD + " and  itahun = " + tahun.ToString() + 
                                " AND dttransaksi between " + tanggalawal.ToSQLFormat() + " AND " + tanggalakhir.ToSQLFormat();
        
                     }
            }
             DataTable dt = new DataTable();
                dt= _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow drTerima = dt.Rows[0];
                        cRKPPKD = DataFormat.GetDecimal(drTerima["X"]);
                    }
                }
  
    
            if (cRKPPKD!=1){
                cRKPPKD = -1 * cRKPPKD;
            }
                return cRKPPKD ;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
      
            }
        }
        public decimal GetLainLain(int tahun, bool semuadinas, int dinas, 
            DateTime tanggalawal, DateTime tanggalakhir, int ppkd)
        {
            try
            {

                decimal e = 0;

                 SSQL ="";
    decimal cJumlahBeban =0;
    decimal cJumlahPendapatan =0;
    string strBeban ="";
    string strpendapatan ="";
        if (tahun<=2020){
            strBeban = "9";
             strpendapatan = "8";
        } else {
            strBeban = "8";
            strpendapatan = "7";
        }

                //310101010002 
                //310101010002
      if (semuadinas== false){
        if (ppkd ==1) {//If m_iPPKD = 1 Then
            SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE IIDRekening =310101010002  and  iTahun = " + tahun.ToString() + " AND dttransaksi between " + tanggalawal.ToSQLFormat() + 
               " AND " + tanggalakhir.ToSQLFormat() + " AND bPPKD=1 " ;//'and btJenisJurnal<10 ";
          
            
        } else{
            if (tahun == Tahun - 1) {
                SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND  IIDRekening =310101010002 AND iddinas = " +  dinas.ToString ()  + " AND dttransaksi between '1/1/" + tahun.ToString() + "' and '12/31/" + tahun.ToString() + "'";
            
            } else {
                    SSQL = "Select  SUM(Idebet * cJumlah) as X  from tBukubesar WHERE   btJenisJurnal<10  AND   IIDRekening =310101010002 and  iTahun = " + tahun.ToString() + " AND iddinas =" + dinas.ToString() + " AND dttransaksi between " + tanggalawal.ToSQLFormat() + 
                          " AND " + tanggalakhir.ToSQLFormat() + " AND bPPKD=" + ppkd.ToString();
        
        
            }
         
            
        } 
    } else {

        if (tahun==Tahun-1){
            SSQL = "Select  SUM( iDebet* cJumlah) as X  from tBukubesar WHERE  btJenisJurnal<10  AND   IIDRekening = 310101010002 " + 
           " AND dttransaksi between '1/1/" + tahun.ToString() + "' and '12/31/" + tahun.ToString() + "'";
                    } else{
                        if (dinas == 0)
                    {
                            SSQL = " Select SUM(iDebet* cJumlah) as X  from tBukubesar "+
                            " WHERE  btJenisJurnal < 10  AND "+
                            " IIDRekening = 310101010002 AND dttransaksi between " + tanggalawal.ToSQLFormat() + " AND " + tanggalakhir.ToSQLFormat();



                        }
                        else
                        {
                            SSQL = "Select  SUM( mRekening.iDebet* tBukubesar.iDebet* cJumlah) as X  from tBukubesar inner join mRekening on mRekening.IIDRekening= tBukuBesar.iIDRekening " +
                                            " WHERE  tBukubesar.btJenisJurnal<10  AND   tBukubesar.IIDRekening =310101010002 and  tBukubesar.iTahun = " + tahun.ToString() +
                                  " AND dttransaksi between " + tanggalawal.ToSQLFormat() + " AND " + tanggalakhir.ToSQLFormat();

                        }

                    }

    }
      
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow drTerima = dt.Rows[0];
                        e= DataFormat.GetDecimal(drTerima["X"]);
                    }
                }

                return -1* e;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return 0;
            }
        }
    }
}
