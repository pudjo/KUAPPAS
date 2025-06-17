using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using DataAccess;
using System.Data;

namespace BP
{
    public class RevisiLogic : BP
    {

        public RevisiLogic(int _pTahun): base (_pTahun)
        {
            Tahun = _pTahun;
        }
        public void CekVolMurnidiUraian()
        {
            
        }
        public void CekShowInReportdiUraian()
        {
            
        }
        public void CekNamaAPBDKegiatan()
        {
            


        }
        public bool CreateViewRealisasiPerRekening()
        {
            HapusView("viewTrx");


            SSQL = " CREATE VIEW [dbo].[viewTrx] AS   select 1 as TABEL, A.inourut, A.btJenis, " +
             " A.btKodeKategori * 1000000 + A.btKodeUrusan * 10000 + A.btKodeSKPD * 100 as IDDInas, " +
             " (B.btKodeKategoriPelaksana * 10000 + B.btKodeUrusanPelaksana * 100 + B.btIDProgram )  AS idpROGRAM," +
             " (B.btKodeKategoriPelaksana * 100 + B.btKodeUrusanPelaksana )  AS iduRUSAN, " +
             " B.btKodeKategoriPelaksana * 10000000 + B.btKodeUrusanPelaksana * 100000 + B.btIDProgram * 1000 + " +
             " B.btIDKegiatan as IDKegiatan, " +
               " btJenis as JenisSP2d, A.iTahun,A.btKodekategori,  A.dtSPP as dtDocument," +
                 "        A.dtBukukas,A.btKodeUrusan,A.btKodeSKPD,B.btKodeUK,B.btKodeKategoriPelaksana,  " +
               " B.btKodeURusanPelaksana, B.btIDProgram,B.btIDKegiatan,A.btIDSUBKegiatan,B.IIDRekening,  " +
               " 1 as DEBET, b.cJumlah, A.bPPKD FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut " +
               " Where a.btJenis =3 ";
                        SSQL = SSQL + " UNION ALL select 1 as TABEL, A.inourut, A.btJenis, " +
             " A.btKodeKategori * 1000000 + A.btKodeUrusan * 10000 + A.btKodeSKPD * 100 as IDDInas, " +
             " 0 AS idpROGRAM," +
             " (A.btKodeKategori * 100 + A.btKodeUrusan )  AS iduRUSAN, " +
             " 0 as IDKegiatan, " +
               " btJenis as JenisSP2d, A.iTahun,A.btKodekategori,  A.dtSPP as dtDocument," +
                 "        A.dtBukukas,A.btKodeUrusan,A.btKodeSKPD,B.btKodeUK,B.btKodeKategoriPelaksana,  " +
               " B.btKodeURusanPelaksana, B.btIDProgram,B.btIDKegiatan,A.btIDSUBKegiatan,B.IIDRekening,  " +
               " 1 as DEBET, b.cJumlah, A.bPPKD FROM tSPP A INNER join tSPPRekening B ON a.inourut=B.inourut " +
               " Where a.btJenis  in (4,5)  And a.iStatus = 4   " +

               " Union ALL select 2 as TABEL, A.inourut,A.btJenis," +
               " btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 as IDDInas, " +
             " case when bppkd=1 then 0 else (btKodeKategoriPelaksana * 10000 + btKodeUrusanPelaksana * 100 + btIDProgram ) end AS idpROGRAM," +
            "  (btKodeKategoriPelaksana * 100 + btKodeUrusanPelaksana )  AS iduRUSAN," +
             " case when bppkd=1 then 0 else ( btKodeKategoriPelaksana * 10000000 + btKodeUrusanPelaksana * 100000 + btIDProgram * 1000 + btIDKegiatan) End as IDKegiatan," +
             " btJenisBelanja as JenisSP2D,   A.iTahun,A.btKodekategori, A.dtBukukas as dtDocument, A.dtBukukas,A.btKodeUrusan,A.btKodeSKPD,   " +
             " A.btKodeUK,A.btKodeKategoriPelaksana,A.btKodeURusanPelaksana, A.btIDProgram,A.btIDKegiatan,   " +
             " A.btIDSUBKegiatan,B.IIDRekening,1 as DEBET, b.cJumlah ,bPPKD FROM tPanjar A INNER join   " +
             " tPanjarRekening B ON a.inourut=B.inourut Where btJenisBelanja in (1,2) " +
             "  Union ALL select 2 as TABEL,A.inourut,a.iJenisSumber  as btJenis," +
             " A.btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 as IDDInas, " +
             " (B.btKodeKategoriPelaksana1 * 10000 + B.btKodeUrusanPelaksana1 * 100 + B.btIDProgram1 )  AS idpROGRAM," +
             " (B.btKodeKategoriPelaksana1 * 100 + B.btKodeUrusanPelaksana1 )  AS iduRUSAN," +
             " btKodeKategoriPelaksana1 * 10000000 + btKodeUrusanPelaksana1 * 100000 + btIDProgram1 * 1000 + btIDKegiatan1 as IDKegiatan,1 as JenisSP2D, A.iTahun,A.btKodekategori, " +
             " A.dtKoreksi as dtDocument,A.dtKoreksi as dtBukukas,A.btKodeUrusan,A.btKodeSKPD,B.btKodeUK1,B.btKodeKategoriPelaksana1,B.btKodeURusanPelaksana1," +
             " B.btIDProgram1,B.btIDKegiatan1, 0 as btIDSUBKegiatan,B.IIDRekening1, -1* b.iDebet1 as DEBET, " +
             " b.cJumlah1 ,0 as bPPKD  FROM tKoreksi A INNER join tKoreksiDetail B ON a.inourut=B.inourut " +
             " where A.iJenisSumber <3  " +
              " Union all  select 5 as TABEL, A.inourut, A.btJenis,btKodeKategori * 1000000 + btKodeUrusan * 10000 + btKodeSKPD * 100 as IDDInas, " +
             "  case when btIDProgram=0 then 0 else  (A.btKodeKategoriPelaksana * 10000 + a.btKodeUrusanPelaksana * 100 + a.btIDProgram ) END  AS idpROGRAM," +
             " (A.btKodeKategoriPelaksana * 100 + A.btKodeUrusanPelaksana )  AS iduRUSAN," +
            "   case when btIDKegiatan =0 then 0 else  btKodeKategoriPelaksana * 10000000 + btKodeUrusanPelaksana * 100000 + btIDProgram * 1000 + btIDKegiatan END  as IDKegiatan,btJenisSP2d as JenisSP2D, " +
             " A.iTahun,A.btKodekategori, A.dtBukukas as dtDocument, A.dtBukukas,A.btKodeUrusan, A.btKodeSKPD,A.btKodeUK," +
             " A.btKodeKategoriPelaksana,A.btKodeURusanPelaksana,A.btIDProgram, A.btIDKegiatan,A.btIDSUBKegiatan, B.IIDRekening,-1 " +
             " as DEBET, b.cJumlah,bPPKD as bPPKD  FROM tsetor A INNER join tsetorRekening B ON a.inourut=B.inourut " +
             " WHERE A.btJenis=3  ";


            _dbHelper.ExecuteNonQuery(SSQL);
            return true;

        }
        public void TambahTahunDanUrusanPokoSKPD()
        {
            try
            {
                SSQL = "SELECT iTahun from mSKPD ";
                _dbHelper.ExecuteDataTable(SSQL);
                return;
            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE mSKPD ADD iTahun smallint, IDUrusan int";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE mSKPD SET iTahun = 2017, IDurusan=ID/100";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;


            }


        }
        public void CekStatusSPD()
        {
            try
            {
                SSQL = "SELECT iStatus from tSPD";
                _dbHelper.ExecuteDataTable(SSQL);
            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tSPD ADD iStatus smallint";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void CekProgramAPBDKegiatan()
        {
            try
            {
                SSQL = "SELECT sNama2 from tPrograms_A ";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tPrograms_A ADD sNama2 varchar(1500)";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "ALTER TABLE mRekening ADD sNama2 varchar(1500)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tAnggaranRekening_A ADD cJumlahYAD decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tAnggaranUraian_A ADD cJumlahYAD decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void AddIDDInasDiSPD()
        {
            try
            {
                SSQL = "SELECT IDDinas from tSPD  ";
                _dbHelper.ExecuteDataTable(SSQL);
                
                return;



            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tSPD  ADD IDDinas int,btBulan2 smallint";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }

        public void AddIDDInasDiSPDKegiatan()
        {
            try
            {
                SSQL = "SELECT IDDinas from tSPDKegiatan  ";
                _dbHelper.ExecuteDataTable(SSQL);                
                return;

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tSPDKegiatan  ADD iddinas int , idUrusan int , idProgram int ,idkegiatan int ";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }


        }


        

        public void CekKolomPlafonDiANggaranRekening()
        {
            try
            {
                SSQL = "SELECT cPlafon from tAnggaranRekening_A ";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranRekening_A ADD cPlafon decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tAnggaranUraian_A ADD cPlafon decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                //SSQL = "Update tAnggaranUraian_A set showinreport =0";
                //_dbHelper.ExecuteNonQuery(SSQL);


            }
        }
        public void CekKolomFlagImportDiANggaranRekening()
        {
            try
            {
                SSQL = "SELECT isImport from tAnggaranRekening_A ";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranRekening_A ADD isImport smallint ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranRekening_A SET isImport=0";
                _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = "Update tAnggaranUraian_A set showinreport =0";
                //_dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void CekKolomFlagImportDiProgramKegiatan()
        {
            try
            {
                SSQL = "SELECT isImport from tPrograms_A ";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tPrograms_A ADD isImport smallint ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tKegiatan_A ADD isImport smallint ";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "UPDATE tPrograms_A SET isImport=0";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "UPDATE tKegiatan_A SET isImport=0";
                _dbHelper.ExecuteNonQuery(SSQL);

                //SSQL = "Update tAnggaranUraian_A set showinreport =0";
                //_dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;


            }
        }
        public void CekLabeldiUraian()
        {
            try
            {
                SSQL = "SELECT sLabel from tAnggaranUraian_A ";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranUraian_A ADD sLabel varchar(5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void CekNoDasarHukum()
        {
            try
            {
                SSQL = "SELECT iNo from mDasarHukum";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE mDasarHukum ADD iNo smallint";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "Update mDasarHukum SET ino=1";
                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;

            }
        }
        public  void CekSumberTabelDana()
        {

            try
            {
                SSQL = "SELECT * from mSumberDana";
                _dbHelper.ExecuteDataTable(SSQL);
                return;

            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE tSumberDanaRekening(ID bigint ,iTahun int ,IDUrusan int ,IDDinas int ,IDProgram int ,IDkegiatan int ,IDRekening bigint NULL,btKatagori smallint ,btKodeUrusan smallint ,btKatagoriDinas smallint ,btKodeUrusanDinas smallint ,btKodeSKPD int ,btKodeUK smallint ,btIDProgram smallint ,btIDKegiatan smallint ,iSumberDana int ,iIDRekening bigint ,cJumlahMurni decimal(20,5),cJumlah  decimal(20,5),btTahapInput smallint) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "CREATE TABLE mSumberDana(ID int,IIDRekening bigint ,sNama varchar(100)) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO mSumberDana values (1,0,'APBD')";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO mSumberDana values (2,0,'DAU')";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO mSumberDana values (3,0,'DAK')";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO mSumberDana values (4,0,'Bantuan Provinsi')";
                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;

            }
        }
        public void TamhahFisikAdmAnggaranRekening()
        {
            
                try
            {
                SSQL = "SELECT cAdministrasi,cFisik from tAnggaranRekening_A";
                _dbHelper.ExecuteDataTable(SSQL);
                return;

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranRekening_A ADD cFisik decimal (18,5),cAdministrasi decimal (18,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "Update tAnggaranRekening_A SET cFisdik =0,cAdministrasi =0";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void CekTablePrioritasNasional()
        {
            try
            {
                SSQL = "SELECT * FORM mPrioritasNasional";
                _dbHelper.ExecuteDataTable(SSQL);


                

            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE mPrioritasNasional ( iTahun smallint,iInduk int,Nomor int,Kode char(5),Nama varchar(200), Leaf smallint)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "CREATE TABLE tPrioritasNasional ( iTahun smallint,IDUrusan int,IDDInas int, IDProgram int ,IDKEgiatan int , IDRekening bigint, NomorPrioritas int )";
                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;


            }
        }
        public void AddTahapOnRekeningAndUraian()
        {
            try
            {
                SSQL = "SELECT iTahap FROM tAnggaranRekening_A";
                _dbHelper.ExecuteDataTable(SSQL);
             }
            catch (Exception ex)
            {
                SSQL = "ALTER  TABLE tAnggaranRekening_A add iTahap smallint";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranRekening_A SET iTahap =0";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "ALTER  TABLE tAnggaranUraian_A add iTahap smallint";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranUraian_A SET iTahap =0";
                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;

            }
        }
        public void AddJumlahOnRekeningAndUraian()
        {
            try
            {
                SSQL = "SELECT Jumlah FROM tAnggaranUraian_A";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {


                SSQL = "ALTER  TABLE tAnggaranUraian_A add Jumlah decimal (20,5), sUraianAPBD varchar(500)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranUraian_A SET Jumlah =JumlahOlah";
                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;

            }
        }
        public void AddJumlahYADAPBDAndUraian()
        {
            try
            {
                SSQL = "SELECT JumlahYADAPBD FROM tAnggaranUraian_A";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {


                SSQL = "ALTER  TABLE tAnggaranUraian_A add JumlahYADAPBD decimal (20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranUraian_A SET JumlahYADAPBD =cJumlahYAD";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER  TABLE tAnggaranRekening_A add JumlahYADAPBD decimal (20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranRekening_A SET JumlahYADAPBD =cJumlahYAD";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void AddJumlahDPARekening()
        {
            try
            {
                SSQL = "SELECT cDPA FROM tAnggaranRekening_A";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER  TABLE tAnggaranRekening_A add cDPA decimal (20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranRekening_A SET cDPA =cJumlahOlah";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void AddUrutLevelDPAUraian()
        {
            try
            {
                SSQL = "SELECT btUrutDPA FROM tAnggaranUraian_A";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER  TABLE tAnggaranUraian_A add btUrutDPA int , iLevelDPA smallint, sSatuanDPA varchar(100),sLabelDPA varchar(10)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranUraian_A SET btUrutDPA = btUrut, iLevelDPA =Level, sSatuanDPA =sSatuan,sLabelDPA =sLabel,cDPA =cJumlahOlah";

                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;
            }
        }

        public void TamhahKeteranganSPD()
        {

            try
            {
                SSQL = "SELECT sKeterangan from tSPD";
                _dbHelper.ExecuteDataTable(SSQL);
                return;

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tSPD ADD sKeterangan varchar(1200), IDBendahara int";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void TamhahNodanPrefixSPD()
        {

            try
            {
                SSQL = "SELECT iNoSPD from tSPD";
                _dbHelper.ExecuteDataTable(SSQL);
                return;

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tSPD ADD iNoSPD  int ,sPrefix varchar(20)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }

        public void CekVolTriwulanOnKegiatan()
        {
            try
            {
                SSQL = "select  programKegiatan.NamaKegiatan, programKegiatan.NamaSUbKegiatan ,b.sUraian ,t.IIDrekening," +
" t.IDKegiatan, t.IDSUBkegiatan,b.dtBukti, t.cJumlah from tBKURekening t " +
"inner join tbKU b on t.inourut= b.inourut " +
" inner join programKegiatan  on programKegiatan.idsubKegiatan = t.idsubkegiatan " +
" and b.iddinas= programKegiatan.iddinas and " +
" b.UnitANggaran = programKegiatan.KodeUK " +
" where t.IIDrekening= 510204010001 and b.iDebet=-1 and b.IDDInas = 1020100"+
" order by b.dtBukti";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tKegiatan_A ADD T1 decimal(20,5), T2 decimal(20,5),T3 decimal(20,5),T4 decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }

        public void GetRekapPerRekening()
        {
            try
            {
                SSQL = "SELECT T1 from tKegiatan_A ";
                _dbHelper.ExecuteDataTable(SSQL);

            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tKegiatan_A ADD T1 decimal(20,5), T2 decimal(20,5),T3 decimal(20,5),T4 decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void CekKolomMUrni()
        {
            try
            {
                SSQL = "SELECT JumlahMurni, sUraianMurni,sLabelMurni  from tAnggaranUraian_A";
                _dbHelper.ExecuteDataTable(SSQL);
            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranUraian_A ADD JumlahMurni decimal(20,5), sUraianMurni varchar(1000),sLabelMurni varchar(10),"+
                    "JumlahGeser decimal(20,5), sUraianGeser varchar(500),sLabelGeser varchar(10)";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = " ALTER TABLE tIndikator ADD sIndikatorMurni varchar(1000),sTargetMurni varchar(100), sIndikatorGeser varchar(1000),sTargetGeser varchar(100)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void CekKolomAnggaranKas()
        {
            try
            {
                SSQL = "SELECT cBulan1Murni from tAnggaranKas";
                _dbHelper.ExecuteDataTable(SSQL);
            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranKas ADD cBulan1Murni decimal(20,5),cBulan2Murni decimal(20,5),cBulan3Murni decimal(20,5),cBulan4Murni decimal(20,5),cBulan5Murni decimal(20,5),cBulan6Murni decimal(20,5),cBulan7Murni decimal(20,5),cBulan8Murni decimal(20,5),cBulan9Murni decimal(20,5),cBulan10Murni decimal(20,5),cBulan11Murni decimal(20,5),cBulan12Murni decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void AddUrusanBaruInSKPD()
        {
            try
            {
                SSQL = "SELECT IDURusanBaru from mSKPD";
                _dbHelper.ExecuteDataTable(SSQL);
            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE mSKPD ADD IDURusanBaru int";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            } 
        }
        public void MapUrusanBaru()
        {
            try
            {
                SSQL = "Select * from MapUrusanBaru";
                _dbHelper.ExecuteDataTable(SSQL);
            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE MapUrusanBaru (idUrusan int, idUrusanBaru int)";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "ALTER TABLE mSKPD add idUrusanBaru int";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tPrograms_A add idUrusanBaru int";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tKegiatan_A add idUrusanBaru int";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void UKDISKPD()
        {
            try
            {
                SSQL = "Select Parent from mSKPD";
                _dbHelper.ExecuteNonQuery(SSQL);

                
            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE mSKPD ADD Parent int, Root smallint,Level smallint";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE mSKPD SET Parent=0, Root =1, Level=1";
                // untukyang uk silakan update manual 
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void CekPlafonABTKegiatan()
        {
            try
            {
                SSQL = "Select cPlafonABT from tKegiatan_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tKegiatan_A ADD cPlafonABT decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tKegiatan_A SET cPlafonABT = cPlafon";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tKUA SET JumlahPerubahan= JumlahOlah";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;


            }
        }
        public void TAMBAHKolomIIAnggaranRekening()
        {
            try
            {
                SSQL = "Select cJumlahGeserP from tAnggaranRekening_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {
                //SSQL = "ALTER TABLE mSKPD ADD Parent int, Root smallint,Level smallint";
                //_dbHelper.ExecuteNonQuery(SSQL);
                //SSQL = "UPDATE mSKPD SET IDParent=0, Root =1, Level=1";
                //// untukyang uk silakan update manual 
                //_dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }

        public void TAMBAHKolomIIIAnggaranRekening()
        {
            try
            {
                // tANggaraRekening tambah cJumlahRKA

                //sUraianRKA,VolRKA,cHargaRKA,JUmlahRKA
                //"sUraianRKAP,VolRKAP,cHargaRKAP,JumlahRKAP";  /// +



                SSQL = "Select cJumlahRKA from tAnggaranRekening_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {
                SSQL = "ALTER TABLE tAnggaranRekening_A ADD cJumlahRKA decimal (20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE tAnggaranRekening_A SET cJumlahRKA= cJumlahMurni ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "ALTER TABLE tAnggaranUraian_A ADD sUraianRKA varchar(1000),VolRKA decimal (15,5),cHargaRKA decimal (20,5), JUmlahRKA decimal (20,5),sUraianRKAP varchar(1000),VolRKAP decimal (15,5),cHargaRKAP decimal (20,5), JUmlahRKAP decimal (20,5)  ";
                _dbHelper.ExecuteNonQuery(SSQL);


                SSQL = "UPDATE tAnggaranUraian_A SET  sUraianRKA= sUraianMurni,volRka= volMurni, cHargaRKA= cHargaMurni, JumlahRKA= JumlahMurni, " +
                    " sUraianRKAP =sUraianGeser,VolRKAP =VolGeser,cHargaRKAP=cHargaGeser, JUmlahRKAP=JUmlahGEser";

                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE tAnggaranRekening_A SET  btTahapInput=1 where btTahapInput=0";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDATE tAnggaranUraian_A SET btTahapInput =1 where btTahapInput=0";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                
            }
        }
        public void TAMBAHKolomIVAnggaranRekening()
        {
            try
            {
                
                SSQL = "Select JumlahABT from tAnggaranUraian_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {

                SSQL = "ALTER TABLE tAnggaranUraian_A ADD JumlahABT decimal (20,5)  ";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                
            }
        }

        public void TAMBAHKolomUraianABTRekening()
        {
            try
            {
                // tANggaraRekening tambah cJumlahRKA

                //sUraianRKA,VolRKA,cHargaRKA,JUmlahRKA
                //"sUraianRKAP,VolRKAP,cHargaRKAP,JumlahRKAP";  /// +



                //SSQL = "Select sUraianABT from tAnggaranUraian_A";
                //_dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {

                //SSQL = "ALTER TABLE tAnggaranUraian_A ADD sUraianABT varchar(1000) ";
                //_dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                
                
            }
        }

        public void TAMBAHKolomUraianOlahRekening()
        {
            try
            {


                SSQL = "Select sUraianOlah from tAnggaranUraian_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {

                SSQL = "ALTER TABLE tAnggaranUraian_A ADD sUraianOlah varchar(1000) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDATE tANggaranUraian_A SET sUraianOlah=sUraian";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
                
                
            }
        }
        public void TAMBAHKolomIDBapeda()
        {
            try
            {


                SSQL = "Select IDRincianBapeda from tAnggaranRekening_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {

                SSQL = "ALTER TABLE tAnggaranRekening_A ADD IDRincianBapeda int,cJumlahRKABapeda decimal (20,5)";

                _dbHelper.ExecuteNonQuery(SSQL);


                _lastError = ex.Message;

            }
        }
        public void TAMBAHKolomBolehInput()
        {
            try
            {


                SSQL = "Select isTatusInput from mTahapanAnggaran";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "UPDate mTahapanAnggaran set isTatusInput =1";

                _dbHelper.ExecuteNonQuery(SSQL);

            }
            catch (Exception ex)
            {

                SSQL = "ALTER TABLE mTahapanAnggaran ADD isTatusInput int";

                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "UPDate mTahapanAnggaran set isTatusInput =1";

                _dbHelper.ExecuteNonQuery(SSQL);

                _lastError = ex.Message;

            }
        }
        public void TAMBAHKolomIDBapedaDiUraian()
        {
            try
            {


                SSQL = "Select IDRincianBapeda from tAnggaranUraian_A";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {

            

                SSQL = "ALTER TABLE tAnggaranUraian_A ADD IDRincianBapeda int, IDUraianBapeda int, volBapeda decimal (10,5), sUraianBapeda varchar(200), satuanBapeda varchar(50), cHargaBapeda decimal(20,5), cJumlahBapeda decimal(20,5)";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;

            }
        }
        public void TambahTabelJenisAnggaran()
        {
            try
            {
                SSQL = "SELECT * FROM JenisAnggaran";
                _dbHelper.ExecuteDataTable(SSQL);
                return;
            }
            catch (Exception ex)
            {
                SSQL = "CREATE TABLE JenisAnggaran ( iTahun int,Jenis smallint,Tahap smallint, Nama varchar(60),Status smallint)";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",1,1,'RKA Pendapatan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",2,1,'RKA Belanja Tidak Langsung/Gaji Tunjangan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",3,1,'RKABalanja Langsung',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",4,1,'RKAPenerimaan Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",5,1,'RKAPengeluaran Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",1,2,'DPA Pendapatan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",2,2,'DPA Belanja Tidak Langsung/Gaji Tunjangan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",3,2,'DPA Balanja Langsung',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",4,2,'DPA Penerimaan Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",5,2,'DPA Pengeluaran Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",1,3,'RKA Pergeseran Pendapatan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",2,3,'RKA Pergeseran Belanja Tidak Langsung/Gaji Tunjangan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",3,3,'RKA Pergeseran  Balanja Langsung',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",4,3,'RKA Pergeseran Penerimaan Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",5,3,'RKA Pergeseran Pengeluaran Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",1,4,'RKA Perubahan Pendapatan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",2,4,'RKA Perubahan Belanja Tidak Langsung/Gaji Tunjangan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",3,4,'RKA Perubahan  Balanja Langsung',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",4,4,'RKA Perubahan Penerimaan Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",5,4,'RKA Perubahan Pengeluaran Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",1,5,'DPA Perubahan Pendapatan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",2,5,'DPA Perubahan Belanja Tidak Langsung/Gaji Tunjangan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",3,5,'DPA Perubahan Balanja Langsung',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",4,5,'DPA Perubahan Penerimaan Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                SSQL = "INSERT INTO JenisAnggaran ( iTahun ,Jenis ,Tahap , Nama ,Status ) values (" + Tahun.ToString() + ",5,5,'DPA Perubahan Pengeluaran Pembiayaan',1) ";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }
        }
        public void CekTabkleJenisJabatan()
        {


            try
            {


                SSQL = "Select *  from JenisJabatan";
                _dbHelper.ExecuteNonQuery(SSQL);


            }
            catch (Exception ex)
            {



                SSQL = "create table JenisJabatan (ID int, Nama varchar(100))";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO  JenisJabatan (ID , Nama ) values (1, 'Bupati Kepala Daerah' )";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (2, 'Sekretaris Daerah' )";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (3, 'KEPALA BADAN PENGELOLA KEUANGAN DAN ASET DAERAH')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (4, 'KUASA BENDAHARA UMUM DAERAH')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (5, 'KASDA')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (6, 'Kepala Dinas')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (7, 'Bendahara Pengeluaran')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (8, 'Bendahara Penerimaan')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (9, 'PPK SKPD')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (10, 'Kasubbid Penatausahaan Penerimaan, Pembiayaan dan Kas Daerah')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (11, 'Koordinator Pelaksana Pada Kas Daerah')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (12, 'Bendahara Pengeluaran PPKD')";
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO JenisJabatan (ID , Nama ) values (13, 'Bendahara Penerimaan PPKD')";
                _dbHelper.ExecuteNonQuery(SSQL);
                _lastError = ex.Message;
            }

        }

    }
    
}
