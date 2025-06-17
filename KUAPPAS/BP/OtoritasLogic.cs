using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BP
{
    public class OtoritasLogic
    {
        public List<cOtoritas> GetListOtoritas( int kelompokPengguna)
        {
            List<cOtoritas> lst = new List<cOtoritas>();

            if (kelompokPengguna == (int)Otoritas.CON_OTORITAS_ADMIN)
            {

                cOtoritas oAdmin = new cOtoritas((int)Otoritas.CON_OTORITAS_ADMIN, "Admin");
                lst.Add(oAdmin);
                
            }
                
           

           cOtoritas oAnggaran = new cOtoritas((int)Otoritas.CON_OTORITAS_MANANG,"Anggaran");
           lst.Add(oAnggaran);
             
        

        cOtoritas obendaharPenerimaan = new cOtoritas((int)Otoritas.CON_OTORITAS_BENDAHARAPENERIMAAN_SKPD,"Bendahara Penerimaan");
        lst.Add(obendaharPenerimaan);
            cOtoritas obendaharPenerimaanPPKD = new cOtoritas((int)Otoritas.CON_OTORITAS_BENDAHARAPENERIMAAN_PPKD,"Bendahara Penerimaan PPKD");// = 12,
            lst.Add(obendaharPenerimaanPPKD); 
            cOtoritas obendaharPengeluaran = new cOtoritas((int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_SKPD, "Bendahara Pengeluaran");// = 13,
            lst.Add(obendaharPengeluaran); 
            cOtoritas obendaharPengeluaranPembantu = new cOtoritas((int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_PEMBANTU_SKPD, "Bendahara Pengeluaran Pemantu");// = 14,
        //cOtoritas obendaharPengeluaranPPKD = new cOtoritas((int)Otoritas.CON_OTORITAS_BENDAHARAPENGELUARAN_PPKD = 15,
            lst.Add(obendaharPengeluaranPembantu); 
            cOtoritas oKasda = new cOtoritas((int)Otoritas.CON_OTORITAS_KASDA, "Kasda");//  = 50,
            lst.Add(oKasda); 
            cOtoritas oBUD = new cOtoritas((int)Otoritas.CON_OTORITAS_BUD, "BUD");// = 60,

            lst.Add(oBUD); 
            cOtoritas oTerimaSPM = new cOtoritas((int)Otoritas.CON_OTORITAS_BUDTERIMASPM, "Terima SPM (Pelayanan BPKAD)");//  = 61,
            lst.Add(oTerimaSPM); 
            cOtoritas oVerifikasiSPM = new cOtoritas((int)Otoritas.CON_OTORITAS_BUDVERIFIKASISPM, "Verifikasi SPM (BUD)");// = 62,
            lst.Add(oVerifikasiSPM); 
            cOtoritas oCetakSP2D = new cOtoritas((int)Otoritas.CON_OTORITAS_BUDCETAKSP2D, "Terima SPM (Cetak SP2D)");//= 63,
            lst.Add(oCetakSP2D); 
            cOtoritas oSP2DOnline = new cOtoritas((int)Otoritas.CON_OTORITAS_BUDSP2DONLINE, "SP2D Online");// = 64,
            lst.Add(oSP2DOnline); 
            
            cOtoritas oBLUD = new cOtoritas((int)Otoritas.CON_OTORITAS_BLUD, "Bendahara Pengeluaran/Penerimaan BLUD");// = 65,
            lst.Add(oBLUD); 
            cOtoritas oBOS = new cOtoritas((int)Otoritas.CON_OTORITAS_BOS, "Bendahara BOS");// = 66,
            lst.Add(oBOS);
            cOtoritas oAset = new cOtoritas((int)Otoritas.CON_OTORITAS_ASET, "Aset");// = 66,
            lst.Add(oAset);
            cOtoritas oKt = new cOtoritas((int)Otoritas.CON_OTORITAS_AKUNTANSI,"Akuntansi Pelaporan");// = 70,
            lst.Add(oKt);
            cOtoritas oPPK = new cOtoritas((int)Otoritas.CON_OTORITAS_PPK,"PPK SKPD");// = 16,
            lst.Add(oPPK);
            cOtoritas oAuditor = new cOtoritas((int)Otoritas.CON_OTORITAS_AUDIT, "Auditor");//
            lst.Add(oAuditor);
            cOtoritas oSupport = new cOtoritas((int)Otoritas.CON_OTORITAS_SUPPORT, "Support");//
            lst.Add(oSupport);
            

            return lst;
        }
    }
}
