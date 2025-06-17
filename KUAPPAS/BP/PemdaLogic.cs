using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Formatting;
using DataAccess;
using System.Data;
namespace BP
{
    public class PemdaLogic:BP 
    {
        public PemdaLogic(int _pTahun)
            : base(_pTahun)
        {
            Tahun = _pTahun;
            m_sNamaTabel = "mPemda";
            CekTabel();
        }
        public bool Simpan(Pemda oPemda)
        {
            try
            {
                int lID = 1;
                SSQL = "DELETE from " + m_sNamaTabel;
                _dbHelper.ExecuteNonQuery(SSQL);

                SSQL = "INSERT INTO mPemda (ID,ProvinsiID,KotaID,Nama,Alamat,Jenis,IbuKota," +
                    " sNamaKaDaerah,sNIPKaDaerah,sNamaSekda,sNIPSekda,sJabatanSekda,sNamaAsSekda ,sJabatanAsSekda ,sNIPAsSekda ,sNamaKaKeu ,sNIPKaKeu ,sJabatanKaKeu,sJabatanKaDa ) values ( " +
                    " @pID,@pProvinsiID,@pKotaID,@pNama,@pAlamat,@pJenis,@pIbuKota," +
                    " @psNamaKaDaerah,@psNIPKaDaerah,@psNamaSekda,@psNIPSekda,@psJabatanSekda,@psNamaAsSekda ,@psJabatanAsSekda ,@psNIPAsSekda ,@psNamaKaKeu ,@psNIPKaKeu ,@psJabatanKaKeu,@psJabatanKaDa )";

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@pID", lID, DbType.Int32));
                paramCollection.Add(new DBParameter("@pProvinsiID", oPemda.ProvinsiID));
                paramCollection.Add(new DBParameter("@pKotaID", oPemda.KotaID));
                paramCollection.Add(new DBParameter("@pNama", oPemda.Nama));
                paramCollection.Add(new DBParameter("@pAlamat", oPemda.Alamat, DbType.String));
                paramCollection.Add(new DBParameter("@pJenis", oPemda.Jenis, DbType.Int16));
                paramCollection.Add(new DBParameter("@pIbuKota", oPemda.Ibukota, DbType.String));                
                paramCollection.Add(new DBParameter("@psNamaKaDaerah", oPemda.NamaKaDaerah, DbType.String));
                paramCollection.Add(new DBParameter("@psNIPKaDaerah", oPemda.NIPKaDaerah, DbType.String));
                paramCollection.Add(new DBParameter("@psNamaSekda", oPemda.NamaSekda, DbType.String));
                paramCollection.Add(new DBParameter("@psNIPSekda", oPemda.NIPSekda, DbType.String));
                paramCollection.Add(new DBParameter("@psJabatanSekda", oPemda.JabatanSekda, DbType.String));
                paramCollection.Add(new DBParameter("@psNamaAsSekda", oPemda.NamaAsSekda, DbType.String));
                paramCollection.Add(new DBParameter("@psJabatanAsSekda", oPemda.JabatanAsSekda, DbType.String));
                paramCollection.Add(new DBParameter("@psNIPAsSekda", oPemda.NIPAsSekda, DbType.String));
                paramCollection.Add(new DBParameter("@psNamaKaKeu", oPemda.NamaKaKeu, DbType.String));
                paramCollection.Add(new DBParameter("@psNIPKaKeu", oPemda.NIPKaKeu, DbType.String));
                paramCollection.Add(new DBParameter("@psJabatanKaKeu", oPemda.JabatanKaKeu, DbType.String));
                paramCollection.Add(new DBParameter("@psJabatanKaDa", oPemda.JabatanKaDaerah, DbType.String));
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

        public Pemda Get()
        {
            Pemda oPemda =new Pemda();
            try
            {

                
                SSQL = "SELECT * from " + m_sNamaTabel;
                _dbHelper.ExecuteNonQuery(SSQL);

                

                DataTable dt = new DataTable();
                dt = _dbHelper.ExecuteDataTable(SSQL);
                if (dt != null)
                {
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        oPemda = new Pemda()
                         {
                            // ID = DataFormat.GetInteger(dr["ID"]),
                             //ProvinsiID = DataFormat.GetInteger(dr["ProvinsiID"]),
                             KotaID = DataFormat.GetInteger(dr["KotaID"]),
                             Nama = DataFormat.GetString(dr["Nama"]),
                             Alamat = DataFormat.GetString(dr["Alamat"]),
                             Jenis = DataFormat.GetSingle(dr["Jenis"]),
                             Ibukota = DataFormat.GetString(dr["Ibukota"]),
                             NamaPanjang = GetNamaJenis(DataFormat.GetSingle(dr["Jenis"])).ToUpper() + " " + DataFormat.GetString(dr["Nama"]).ToUpper(),
                             KodeLokasi = DataFormat.GetString(dr["KodeLokasi"]),
                             NamaKaDaerah = DataFormat.GetString(dr["sNamaKaDaerah"]),
                             NIPKaDaerah = DataFormat.GetString(dr["sNIPKaDaerah"]),
                             JabatanKaDaerah=DataFormat.GetString(dr["sJabatanKaDa"]),
                             NamaSekda = DataFormat.GetString(dr["sNamaSekda"]),
                             NIPSekda = DataFormat.GetString(dr["sNIPSekda"]),
                             JabatanSekda = DataFormat.GetString(dr["sJabatanSekda"]),
                             NamaAsSekda = DataFormat.GetString(dr["sNamaAsSekda"]),
                             JabatanAsSekda = DataFormat.GetString(dr["sJabatanAsSekda"]),
                             NIPAsSekda = DataFormat.GetString(dr["sNIPAsSekda"]),
                             NamaKaKeu = DataFormat.GetString(dr["sNamaKaKeu"]),
                             NIPKaKeu = DataFormat.GetString(dr["sNIPKaKeu"]),
                             JabatanKaKeu = DataFormat.GetString(dr["sJabatanKaKeu"]),
                             NamaJenis = GetNamaJenis(DataFormat.GetSingle(dr["Jenis"]))

                         };
                    }
                    
                }
                return oPemda;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                _isError = true;
                return null;

            }


        }
        private string GetNamaJenis(Single _iJenis)
        {
            string sNama = "";
            switch ((int)_iJenis)
            {
                case 0:
                    sNama = "Provinsi";
                    break;
                case 1:
                    sNama = "Kota";
                    break;
                case 2:
                    sNama = "Kabupaten";
                    break;

            }
            return sNama;

        }
        private void CekTabel()
        {
            //try
            //{
            //    SSQL = "SELECT Ibukota from  " + m_sNamaTabel;
            //    _dbHelper.ExecuteDataTable(SSQL);
            //}
            //catch (Exception ex)
            //{
                
            //    SSQL = "ALTER table " + m_sNamaTabel + " ADD  ID int ,ProvinsiID int,KotaID int,Nama varchar(50) ,Alamat varchar(150) ,Jenis smallint,Ibukota varchar(100)," +
            //                    "NamaPanjang varchar(150) ,sJabatanKaDa varchar(150),KodeLokasi char(6)";
            //    _dbHelper.ExecuteNonQuery(SSQL);


            //}
        }


    }
}
