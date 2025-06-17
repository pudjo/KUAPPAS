using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

using DataAccess;
using DTO.Bendahara;
using Formatting;
using BP;
namespace BP.Bendahara
{
    public class SKRSKPDLogic:BP 
    {

        
         public SKRSKPDLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "tSKRSKPD";
            
        }
       
        public long Simpan(SKRSKPD skrskpd)
        {
            long _lNoUrut=0L;
            try {
            DBParameterCollection paramCollection = new DBParameterCollection();


            if (skrskpd.NoUrut== 0)
            {

                _lNoUrut = ReadNo(E_KOLOM_NOURUT.CON_URUT_SKRSKPD, skrskpd.IDDInas);

                SSQL = " INSERT INTO tSKRSKPD (iNoUrut,IDDInas, iTahun , btJenis , btKodeKategori , btKodeUrusan ," +
                 "btKodeSKPD , btKodeUK , dtSKRSKPD, sNoBukti , iMasa , sKeterangan ,iStatus , cJumlah,bPPKD,sNama,sAlamat,sNoNPWD ) values ( " +
                 " @piNoUrut, @pIDDInas,@piTahun , @pbtJenis , @pbtKodeKategori , @pbtKodeUrusan ," +
                 "@pbtKodeSKPD , @pbtKodeUK , @pdtSKRSKPD, @psNoBukti , @piMasa , @psKeterangan ,@piStatus , @pcJumlah,@pbPPKD,@psNama,@psAlamat,@psNoNPWD)";



                paramCollection.Add(new DBParameter("@piNoUrut", _lNoUrut)); 
                paramCollection.Add(new DBParameter("@pIDDInas",skrskpd.IDDInas));
                paramCollection.Add(new DBParameter("@piTahun",skrskpd.Tahun));
                paramCollection.Add(new DBParameter("@pbtJenis ",skrskpd.Jenis));
                paramCollection.Add(new DBParameter("@pbtKodeKategori",skrskpd.KodeKategori));
                paramCollection.Add(new DBParameter("@pbtKodeUrusan",skrskpd.KodeUrusan));
                 paramCollection.Add(new DBParameter("@pbtKodeSKPD",skrskpd.KodeSKPD));
                paramCollection.Add(new DBParameter("@pbtKodeUK",skrskpd.KodeUK));
                paramCollection.Add(new DBParameter("@pdtSKRSKPD",skrskpd.TanggalSKRSKPD));
                paramCollection.Add(new DBParameter("@psNoBukti",skrskpd.NoBukti));
                paramCollection.Add(new DBParameter("@piMasa",skrskpd.Masa));
                paramCollection.Add(new DBParameter("@psKeterangan",skrskpd.Keterangan));
                paramCollection.Add(new DBParameter("@piStatus",skrskpd.Status));
                paramCollection.Add(new DBParameter("@pcJumlah",skrskpd.Jumlah));
                paramCollection.Add(new DBParameter("@pbPPKD",skrskpd.PPKD));
                paramCollection.Add(new DBParameter("@psNama",skrskpd.Nama));
                paramCollection.Add(new DBParameter("@psAlamat",skrskpd.Alamat));
                paramCollection.Add(new DBParameter("@psNoNPWD", skrskpd.NoNPWD));


            } else {
                _lNoUrut = skrskpd.NoUrut;


                SSQL = " UPDATE  tSKRSKPD SET sNoBukti =@psNoBukti, iMasa=@piMasa , sKeterangan=@psKeterangan,iStatus=@piStatus" +
                 ", cJumlah=@pcJumlah,bPPKD=@pbPPKD,sNama=@psNama,sAlamat=@psAlamat,sNoNPWD=@psNoNPWD WHERE iNourut =@piNoUrut";


              
         
                paramCollection.Add(new DBParameter("@psNoBukti", skrskpd.NoBukti));
                paramCollection.Add(new DBParameter("@piMasa", skrskpd.Masa));
                paramCollection.Add(new DBParameter("@psKeterangan", skrskpd.Keterangan));
                paramCollection.Add(new DBParameter("@piStatus", skrskpd.Status));
                paramCollection.Add(new DBParameter("@pcJumlah", skrskpd.Jumlah));
                paramCollection.Add(new DBParameter("@pbPPKD", skrskpd.PPKD));
                paramCollection.Add(new DBParameter("@psNama", skrskpd.Nama));
                paramCollection.Add(new DBParameter("@psAlamat", skrskpd.Alamat));
                paramCollection.Add(new DBParameter("@psNoNPWD", skrskpd.NoNPWD));
                 paramCollection.Add(new DBParameter("@piNoUrut", _lNoUrut));

            }
            _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
            
            return _lNoUrut;

            } catch (Exception ex){
                _isError = true;
                _lastError = ex.Message;

                return 0;

            }
            
        }
          public List<SKRSKPD> GetByDinas( int idDinas, DateTime tanggalAwal, DateTime tanggalakhir)
          {
              List<SKRSKPD> _lst = new List<SKRSKPD>();
              try
              {
                  

                  SSQL = "SELECT tSKRSKPD.* FROM tSKRSKPD WHERE IDDInas =@Dinas " +
                      " and dtSKRSKPD between @TanggalAwal and @TanggalAkhir Order by inourut";//
                 
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("@Dinas", idDinas));
                paramCollection.Add(new DBParameter("@TanggalAwal",tanggalAwal));
                paramCollection.Add(new DBParameter("@TanggalAkhir", tanggalakhir));


                  
                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL, paramCollection);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new SKRSKPD()
                                  {

                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      Tahun = DataFormat.GetInteger(dr["iTahun"]),
                                      KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                                      KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                                      KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                                      KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),
                                   
                                      TanggalSKRSKPD = DataFormat.GetDateTime(dr["dtSKRSKPD"]),
                                      NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                                      Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      Status = DataFormat.GetSingle(dr["iStatus"]),
                                      Jenis = (E_JENIS_PIUTANG)DataFormat.GetInteger(dr["btJenis"]),
                                      Nama = DataFormat.GetString(dr["snama"]),
                                      NoNPWD =DataFormat.GetString(dr["sNoNPWD"]),
                                      Alamat = DataFormat.GetString(dr["sAlamat"]),
                                      PPKD = DataFormat.GetInteger(dr["bppkd"]),
                                      Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"])),
                                 

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
          public SKRSKPD GetByID(int _inourut)
          {
              SKRSKPD oSKRSKPD = new SKRSKPD();
              try
              {
                  SSQL = "SELECT tSKRSKPD.* FROM " + m_sNamaTabel + " WHERE tSKRSKPD.inourut = " + _inourut.ToString();

                  //

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          DataRow dr = dt.Rows[0];

                          oSKRSKPD = new SKRSKPD()
                          {
                              NoUrut = DataFormat.GetLong(dr["inourut"]),
                              Tahun = DataFormat.GetInteger(dr["iTahun"]),
                              KodeKategori = DataFormat.GetInteger(dr["btKodeKategori"]),
                              KodeUrusan = DataFormat.GetInteger(dr["btKodeUrusan"]),
                              KodeSKPD = DataFormat.GetInteger(dr["btKodeSKPD"]),
                              KodeUK = DataFormat.GetInteger(dr["btKodeUK"]),

                              TanggalSKRSKPD = DataFormat.GetDateTime(dr["dtSKRSKPD"]),
                              NoBukti = DataFormat.GetString(dr["sNoBukti"]),
                              Keterangan = DataFormat.GetString(dr["sKeterangan"]),
                              Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                              Status = DataFormat.GetSingle(dr["iStatus"]),
                              Jenis = (E_JENIS_PIUTANG)DataFormat.GetInteger(dr["btJenis"]),
                              Nama = DataFormat.GetString(dr["snama"]),
                              NoNPWD = DataFormat.GetString(dr["sNoNPWD"]),
                              Alamat = DataFormat.GetString(dr["sAlamat"]),
                              PPKD = DataFormat.GetInteger(dr["bppkd"]),
                              Rekenings = GetDetail(DataFormat.GetLong(dr["inourut"])),
  

                                  };

                          };
                      }
                  
                  return oSKRSKPD;
              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return null;
              }

          }
          public bool Hapus(long  iNourut)
          {

              try
              {
                  SSQL = "DELETE  FROM " + m_sNamaTabel + " WHERE WHERE iNourut = " + iNourut.ToString();
                  _dbHelper.ExecuteNonQuery(SSQL);
                  return true;

              }
              catch (Exception ex)
              {
                  _isError = true;
                  _lastError = ex.Message;
                  return false;
              }

          }
          public List<SKRSKPDRekening> GetDetail(long NoUrut)
          {
              List<SKRSKPDRekening> _lst = new List<SKRSKPDRekening>();
              try
              {
                  SSQL = "SELECT TSKRSKPDRekening.*, mRekening.snamaRekening as Nama FROM tSKRSKPDREkening INNER JOIN mRekening ON tSKRSKPDRekening.IIDRekening = mRekening.IIDRekening WHERE tSKRSKPDRekening.inourut = " + NoUrut.ToString();

                  DataTable dt = new DataTable();
                  dt = _dbHelper.ExecuteDataTable(SSQL);
                  if (dt != null)
                  {
                      if (dt.Rows.Count > 0)
                      {
                          _lst = (from DataRow dr in dt.Rows
                                  select new SKRSKPDRekening()
                                  {
                                      NoUrut = DataFormat.GetLong(dr["inourut"]),
                                      IDRekening = DataFormat.GetLong(dr["IIDRekening"]),
                                      Jumlah = DataFormat.GetDecimal(dr["cJumlah"]),
                                      Nama = DataFormat.GetString(dr["Nama"]),
                                      IDRekening64 = DataFormat.GetLong(dr["IIDRekening64"]),

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
