using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DTO.Bendahara;
using DataAccess;
using System.Data;
using Formatting;

namespace BP
{
    public class TahapanAnggaranLogic:BP
    {
        public TahapanAnggaranLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mTahapanAnggaran";
        }
        private void CekTable()
        {
            try
            {
                SSQL = "SELECT IDDInas from " + m_sNamaTabel;
                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);

                if (dt != null || _isError==true)
                {
                    SSQL = "ALTER Table " + m_sNamaTabel + " ADD IDDinas int, iStatusAK smallint";
                    _dbHelper.ExecuteNonQuery(SSQL);
                    

                }
                return;

            }catch(Exception ex ){
                
                SSQL = "ALTER Table " + m_sNamaTabel + " ADD IDDinas int, iStatusAK smallint";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                

            }
        }


        public List<TahapanAnggaran> Get()
        {
            List<TahapanAnggaran> _lst = new List<TahapanAnggaran>();
            try
            {


                SSQL = "SELECT * FROM mTahapanAnggaran inner join mSKPD On mTahapanAnggaran.IDDInas = mSKPD.ID WHERE mTahapanAnggaran.iTahun =" + Tahun.ToString() + "   and mSKPD.Root = 1 ORDER BY IDDinas";
                DataTable dt = new DataTable();
                dt=_dbHelper.ExecuteDataTable(SSQL);
                if (dt != null){
                   if (dt.Rows.Count > 0)
                    {
                        _lst = (from DataRow dr in dt.Rows
                                select new TahapanAnggaran()
                                {
                                    Tahun= DataFormat.GetInteger(dr["iTahun"]),
                                    Tahap= DataFormat.GetSingle(dr["btTahap"]),
                                    IDDInas = DataFormat.GetInteger(dr["IDDInas"]),
                                    StatusAnggaranKas = DataFormat.GetInteger(dr["iStatusAK"]),
                                    StatusInput = DataFormat.GetInteger(dr["iStatusInput"]),
                                    NamaDinas=DataFormat.GetString(dr["snamaSKPD"]),
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

        public TahapanAnggaran GetByDinas( int _idDinas, int _tahun)
        {
            TahapanAnggaran ta= new TahapanAnggaran();
            try
            {
                SSQL = "SELECT * FROM " + m_sNamaTabel + " where IDDinas= " + _idDinas.ToString() + " AND iTahun =" + _tahun.ToString() ;

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {                        
                        DataRow dr = dt.Rows[0];
                        ta.Tahun = DataFormat.GetInteger(dr["iTahun"]);
                        ta.Tahap = DataFormat.GetSingle(dr["btTahap"]);
                        ta.IDDInas = DataFormat.GetInteger(dr["IDDInas"]);
                        ta.StatusAnggaranKas = DataFormat.GetInteger(dr["iStatusAK"]);
                        ta.StatusInput = DataFormat.GetInteger(dr["iStatusInput"]);
                    }
                    else
                    {
                       


                    }
                }
                return ta;
            }
            catch (Exception ex)
            {
                _isError = true;
                _lastError = ex.Message;
                return ta;
            }
        }

        //public List<TahapanAnggaran> Get(int _tahun)
        //{
        //    List<TahapanAnggaran> lst = new List<TahapanAnggaran>();
        //    try
        //    {
        //        SSQL = "SELECT " + m_sNamaTabel + ".*, mSKPD.sNamaSKPD as Nama FROM " + m_sNamaTabel + " INNER JOIN mSKPD on " + m_sNamaTabel + ".IDDInas = mSKPD.ID where IDDinas= " + _idDinas.ToString() + " AND iTahun =" + _tahun.ToString();

        //        DataTable dt = new DataTable();
        //        dt = _dbHelper.ExecuteDataTable(SSQL);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];
        //                ta.Tahun = DataFormat.GetInteger(dr["iTahun"]);
        //                ta.Tahap = DataFormat.GetSingle(dr["btTahap"]);
        //                ta.IDDInas = DataFormat.GetInteger(dr["IDDInas"]);
        //                ta.NamaDinas = DataFormat.GetString(dr["Nama"]);

        //                ta.StatusAnggaranKas = DataFormat.GetInteger(dr["iStatusAK"]);
        //            }
        //        }
        //        return ta;
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message;
        //        return ta;
        //    }
        //}
        
        public bool Simpan(ref TahapanAnggaran ta)
        {              
            try
            {
                
                   //ID = DataFormat.GetInteger(dr[(ID,btJenis,IDDInas,sJabatan,sNama,sNIP,bActive
               if (Cek(ta)==false){
                   SSQL = "INSERT INTO " + m_sNamaTabel + " (iTahun,IDDinas,btTahap,iStatusAK) values " +
                        "(@piTahun,@pIDDinas,@pbtTahap,@piStatusAK)";

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@piTahun",ta.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDinas",ta.IDDInas));
                    paramCollection.Add(new DBParameter("@pbtTahap",ta.Tahap));
                    paramCollection.Add(new DBParameter("@piStatusAK",ta.StatusAnggaranKas));
                    _dbHelper.ExecuteNonQuery(SSQL,paramCollection);
                }
                else
                {
                    SSQL = "UPDATE " + m_sNamaTabel + " SET btTahap=@btTahap,iStatusAk=@piStatusAk where iTahun = @pTahun and IDDInas=@pIDDInas";
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("@btTahap", ta.Tahap));
                    paramCollection.Add(new DBParameter("@piStatusAk", ta.StatusAnggaranKas));
                    paramCollection.Add(new DBParameter("@pTahun", ta.Tahun));
                    paramCollection.Add(new DBParameter("@pIDDInas", ta.IDDInas));                    
                    _dbHelper.ExecuteNonQuery(SSQL,paramCollection) ;
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

        public bool SetTahapRKA(int idDinas, Single pTahun, Single pTahap)
        {
            try
            {

        
                

                TahapanAnggaran ta = new TahapanAnggaran();
                if (idDinas == 0)
                {

                    List<SKPD> lSKPD = new List<SKPD > ();
                    SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
                    lSKPD = oSKPDLogic.Get(Tahun);
                    foreach (SKPD s in lSKPD)
                    {
                        RubahStatus( s.ID, (int)Tahun, (int)pTahap);

                    }
                    
                } else {

                    RubahStatus(idDinas, (int)Tahun, (int)pTahap);

                }

                return true;

            }catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        public bool SetTahap(int idDinas, Single pTahun, Single pTahap)
        {
            try
            {

                TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(Tahun);
                TahapanAnggaran ta = new TahapanAnggaran();

                ta = GetByDinas(idDinas, (int)pTahun);
                if (ta == null || ta.IDDInas == 0)
                {
                    SSQL = "INSERT into mTahapanAnggaran (iTahun, IDDinas, btTahap,iStatusAK,IsTATUSiNPUT) values (" +
                        pTahun.ToString() + ", " + idDinas.ToString() + "," + pTahap.ToString() + ",0,1)";
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
                else
                {
                    SSQL = "update mTahapanAnggaran SET btTahap = " + pTahap.ToString() + " WHERE IDDInas =" + idDinas.ToString() + " AND iTahun =" + pTahun.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);

                }
                return true;

            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }
        //public bool Kunci (int idDinas, Single pTahun, Single pTahap)
        //{
        //    try
        //    {

        //        TAnggaranRekeningLogic oLogic = new TAnggaranRekeningLogic(Tahun);
        //        switch ((int)pTahap)
        //        {
        //            case 2:
        //                oLogic.SamakanDPAdenganRKA((int)pTahun, idDinas);
        //                break;
        //            case 4:
        //                oLogic.PersiapkanRKAP((int)pTahun, idDinas);
        //                break;

        //            case 5:
        //                oLogic.SamakanABTdenganRKAP((int)pTahun, idDinas);
        //                break;
        //        }


        //        TahapanAnggaran ta = new TahapanAnggaran();
        //        if (idDinas == 0)
        //        {

        //            List<SKPD> lSKPD = new List<SKPD>();
        //            SKPDLogic oSKPDLogic = new SKPDLogic(Tahun);
        //            lSKPD = oSKPDLogic.Get(Tahun);
        //            foreach (SKPD s in lSKPD)
        //            {
        //                RubahStatus(s.ID, (int)Tahun, (int)pTahap);

        //            }


        //        }
        //        else
        //        {


        //            RubahStatus(idDinas, (int)Tahun, (int)pTahap);

        //        }

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        _lastError = ex.Message;
        //        return false;
        //    }
        //}

        private bool RubahStatus(int SKPDID, int tahun, int pTahap)
        {
            try{
            
            TahapanAnggaran ta = new TahapanAnggaran();
            

                    ta = GetByDinas(SKPDID, tahun);
                    if (ta == null || ta.IDDInas == 0)
                    {
                        SSQL = "INSERT into mTahapanAnggaran (iTahun, IDDinas, btTahap,iStatusAK) values (" +
                            tahun.ToString() + ", " + SKPDID.ToString() + "," + pTahap.ToString() + ",0)";
                        _dbHelper.ExecuteNonQuery(SSQL);

                    }
                    else
                    {
                        SSQL = "update mTahapanAnggaran SET btTahap = " + pTahap.ToString() + " WHERE IDDInas =" + SKPDID.ToString() + " AND iTahun =" + tahun.ToString();
                        _dbHelper.ExecuteNonQuery(SSQL);

                    }
                    return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }

        public  bool KunciInput(int SKPDID, int tahun)
        {
            try
            {

                TahapanAnggaran ta = new TahapanAnggaran();
                if (SKPDID > 0)
                {

                    SSQL = "update mTahapanAnggaran SET iStatusInput = 9 WHERE IDDInas =" + SKPDID.ToString() + " AND iTahun =" + tahun.ToString();
                }  else
                    SSQL = "update mTahapanAnggaran SET iStatusInput = 9 WHERE iTahun =" + tahun.ToString();

                    _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }

        public bool BukaKunciInput(int SKPDID, int tahun)
        {
            try
            {

                TahapanAnggaran ta = new TahapanAnggaran();
                if (SKPDID == 0)
                {
                    SSQL = "update mTahapanAnggaran SET iStatusInput = 1 WHERE iTahun =" + tahun.ToString();
                
                }
                else
                {

                    SSQL = "update mTahapanAnggaran SET iStatusInput = 1 WHERE IDDInas =" + SKPDID.ToString() + " AND iTahun =" + tahun.ToString();
                
                }
                
                _dbHelper.ExecuteNonQuery(SSQL);

                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }

        public bool SetStatusAK(int idDinas, Single pTahun, Single pStatus)
        {
            try
            {
                TahapanAnggaran ta = new TahapanAnggaran();
                ta = GetByDinas(idDinas, (int)pTahun);
                if (ta == null || ta.IDDInas == 0)
                {
                    SSQL = "INSERT into mTahapanAnggaran (iTahun, IDDinas, btTahap,iStatusAK,iStatusInput) values (" +
                        pTahun.ToString() + ", " + idDinas.ToString() + ",0," + pStatus.ToString() + ",1)";
                    _dbHelper.ExecuteNonQuery(SSQL);
                    // SET btTahap=@btTahap,iStatusAk=@piStatusAk where iTahun = @pTahun and IDDInas=@pIDDInas";

                }
                else
                {
                    SSQL = "update mTahapanAnggaran SET iStatusAK = " + pStatus.ToString() + " WHERE IDDInas =" + idDinas.ToString() + " AND iTahun =" + pTahun.ToString();
                    _dbHelper.ExecuteNonQuery(SSQL);
                }
                return true;
            }
            catch (Exception ex)
            {

                _lastError = ex.Message;
                return false;
            }
        }
        private bool Cek(TahapanAnggaran ta)
        {
            if (GetByDinas(ta.IDDInas, ta.Tahun) == null)
            {
                return false;
            }
            else return true;
        }

        public bool PersiapanPenyempurnaan(int _Tahun, int _idDinas)
        {

            //CekTable();
            try
            {

                SSQL = "UPDATE tAnggaranRekening_A SET cJumlahMurni =cDPA, cJumlahGeser=cDPA WHERE iTahun = " + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() ;

                //SSQL = "ALTER TABLE tAnggaranUraian_A ADD JumlahMurni decimal(20,5), sUraianMurni varchar(1000),sLabelMurni varchar(10)," +
                 //   "JumlahGeser decimal(20,5), sUraianGeser varchar(500),sLabelGeser varchar(10)";


                _dbHelper.ExecuteNonQuery(SSQL);

                //'"btUrut=@pbtUrut,VolOlah=@pVolOlah,,,,IDstandardHarga=@pIDstandardHarga,JumlahOlah =@pJumlahOlah,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD WHERE " +
                SSQL = "UPDATE tAnggaranUraian_A SET  cHargaMurni= cHarga, VolMurni=VolOlah,JumlahMurni= Jumlah, sUraianMurni= sUraianAPBD,sLabelMurni= sLabelDPA " +
                    ", cHargaGeser= cHarga, VolGeser=VolOlah,JumlahGeser= Jumlah, sUraianGeser= sUraianAPBD,sLabelGeser= sLabelDPA  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString() ;

                _dbHelper.ExecuteNonQuery(SSQL);


                //SSQL = "ALTER TABLE tAnggaranUraian_A ADD JumlahMurni decimal(20,5), sUraianMurni varchar(1000),sLabelMurni varchar(10)," +
                //  "JumlahGeser decimal(20,5), sUraianGeser varchar(500),sLabelGeser varchar(10)";
                //_dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " UPDATE tIndikator SET sIndikatorMurni=sIndikator ,sTargetMurni=sTarget, sIndikatorGeser=sIndikator,sTargetGeser=sTarget  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString(); 
                _dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;

            }

        }
        public bool PersiapanInputRKAP(int _Tahun, int _idDinas)
        {

            //CekTable();
            try
            {

                SSQL = "UPDATE tAnggaranRekening_A SET cJumlahRKAP =cDPA  WHERE iTahun = " + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE tAnggaranUraian_A SET  cHargaRKAP= cHargaGeser, VolRKAP=VolGESER,JumlahRKAP= JumlahGeser , sUraianRKAP= sUraianGeser " +
                    " WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);
                //SSQL = " UPDATE tIndikator SET sIndikatorMurni=sIndikator ,sTargetMurni=sTarget, sIndikatorGeser=sIndikator,sTargetGeser=sTarget  WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                //_dbHelper.ExecuteNonQuery(SSQL);
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;

            }

        }
        public bool SetAnggaranKAsMurniUtkPenyempurnaan(int _Tahun, int _idDinas)
        {
            //CekTable();
            try
            {


                //'"btUrut=@pbtUrut,VolOlah=@pVolOlah,,,,IDstandardHarga=@pIDstandardHarga,JumlahOlah =@pJumlahOlah,showinreport=@pshowinreport,sLabel=@psLabel,cJumlahYAD=@pcJumlahYAD WHERE " +
                SSQL = "UPDATE tANggaranKas SET  cBulan1Murni= cBulan1, cBulan2Murni= cBulan2,cBulan3Murni= cBulan3,cBulan4Murni= cBulan4 ,cBulan5Murni= cBulan5 ,cBulan6Murni= cBulan6 ,cBulan7Murni= cBulan7,cBulan8Murni= cBulan8 ,cBulan9Murni= cBulan9 ,cBulan10Murni= cBulan10,cBulan11Murni= cBulan11 ,cBulan12Murni= cBulan12 WHERE iTahun =" + _Tahun.ToString() + " AND IDDInas = " + _idDinas.ToString();
                _dbHelper.ExecuteNonQuery(SSQL);


         
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;

            }

        }
        
        //public bool Hapus(int _pIDUnit)
        //{
        //    try
        //    {
                
        //        SSQL = "DELETE FROM " +m_sNamaTabel +"  WHERE ID=@pID";                
        //        DBParameterCollection paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("@pID", _pIDUnit));
        //        if (_dbHelper.ExecuteNonQuery(SSQL) > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _isError = true;
        //        _lastError = ex.Message + " " + SSQL;
        //        return false;

        //    }

        //}
    }
}
