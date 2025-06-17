using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO;
using DataAccess;
using Formatting;
namespace BP
{
    public class Permen93PerkiraanLogic:BP
    {
        public Permen93PerkiraanLogic(int _pTahun)
            : base(_pTahun)
        {

        }
    //    public List<Permen93Perkiraan> Get(int iTahun)
    //    {
    //        SSQL ="select 'A' as Label,1 as No,'Belanja Pegawai' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening like '511%' ";
    //SSQL = SSQL + " UNION select 'A' as Label,2 as No,'Belanja Barang ' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening like '52%' ";
    //SSQL = SSQL + " UNION select 'A' as Label,3 as No,'Belanja Bunga ' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening like '512%' ";
    //SSQL = SSQL +  " UNION select 'A' as Label,4 as No,'Belanja Subsidi ' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening like '513%' ";
    //SSQL = SSQL + " UNION select 'A' as Label,5 as No,'Belanja Hibah ' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening like '514%' ";
    //SSQL = SSQL + " UNION select 'A' as Label,6 as No,'Belanja Bantuan Sosial' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening like '515%' ";

    //SSQL = SSQL + " UNION select 'B' as Label,1 as No,'Belanja Tanah' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening between 5230100 and 5230199 ";
    //SSQL = SSQL + " UNION select 'B' as Label,2 as No,'Belanja Peralatan dan Mesin ' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening between 5230200 and 5232099 ";
    //SSQL = SSQL + " UNION select 'B' as Label,3 as No,'Belanja Gedung dan Bangunan' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening between 5230200 and 5232099 ";
    //SSQL = SSQL + " UNION select 'B' as Label,4 as No,'Belanja Jalan, Irigasi dan Jaringan' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening between 5230200 and 5232099 ";
    //SSQL = SSQL + " UNION select 'B' as Label,5 as No,'Belanja Aset Tetap Lainnya' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening between 5230200 and 5232099 ";
    //SSQL = SSQL + " UNION select 'B' as Label,6 as No,'Belanja Aset Lainnya' as Nama, SUM(tAnggaranKas.cBulan1) as Januari,SUM(tAnggaranKas.cBulan2) as Februari,SUM(tAnggaranKas.cBulan3) as Maret,SUM(tAnggaranKas.cBulan4) as April,SUM(tAnggaranKas.cBulan5) as Mei,SUM(tAnggaranKas.cBulan6) as Juni,SUM(tAnggaranKas.cBulan7) as Juli,SUM(tAnggaranKas.cBulan8) as Agustus,SUM(tAnggaranKas.cBulan8) as September,SUM(tAnggaranKas.cBulan8) as Oktober,SUM(tAnggaranKas.cBulan8) as November FROM tAnggaranKas WHERE iTahun =2017 AND IIDRekening between 5230200 and 5232099 ";


    //    }


    }
}
