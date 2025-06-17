using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS.DataAccess9.DTO.SP2DOnLine
{
    public class KesalahanSP2DOnline
    {
        private string m_sKodeError;
        private string m_PesanKesalahan;

        public KesalahanSP2DOnline(string _kodeKesalahan)
        {
            m_sKodeError = _kodeKesalahan;
        }
        public string m_sKodeKesalahan
        {
            set { m_sKodeError = value; }
            get { return m_sKodeError; }
        }
        public string PesanKesalahan
        {
            get
            {
                switch (m_sKodeError)
                {
                    case "00" :
                        m_PesanKesalahan = "Transaksi Berhasil";
                        break;
    
                    case "01":
                        m_PesanKesalahan = "Data Tidak Tersedia / Tagihan Tidak Tersedia / Data Tidak Ditemukan";
                            break;
                    case  "02" :
                            m_PesanKesalahan = "Tagihan sudah Kadaluarsa/expired";
                            break;
                    case "03" : 
                        m_PesanKesalahan = "Data Pembayaran Tidak Sesuai";
                            break;
                    case "11":
                        m_PesanKesalahan = "Transaksi Pending";
                            break; 

                    case "27":
                            m_PesanKesalahan = "Tagihan Sudah Terbayar Melalui Bank Lain";
                             break;
                    case "31":
                            m_PesanKesalahan = "Transaksi di Tolak - PIN tidak valid";
                            break;
                    case "32":
                            m_PesanKesalahan = "Transaksi di Tolak - Permintaan tidak valid";
                            break;
                    case "33":
                            m_PesanKesalahan = "Transaksi di Tolak - ID Reference telah diproses";
                            break;
                    case "88":
                            m_PesanKesalahan = "Tagihan Sudah Terbayar";
                            break;
                    case "90":
                            m_PesanKesalahan = "Error - Timeout";
                            break;
                    case "92":
                            m_PesanKesalahan = "Error - Exception error (service Inquiry Payment MPN dan Payment MPN)";
                            break;
                    case "97":
                            m_PesanKesalahan = "Error - Invalid Payload Request";
                            break;
                    case "98":
                            m_PesanKesalahan = "Error - Invalid Signature / client_id / Panjang Length Message tidak sama";
                            break;
                    case "99":
                            m_PesanKesalahan = "Error - Exception error";
                            break;
                       
                }
                return m_PesanKesalahan;
            }// end of get
        }

    }
}
