using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.SP2DOnLine
{
    public class InquiryRequest
    {
        private string m_sandiBank;
        private string m_snomorRekening;
        private string m_lastError;

        public string Sandibank{
            set
            {
                if (value.Length > 3)
                {
                    m_sandiBank = "0";
                    m_lastError = "Panjang Sandi Bank melebihi ketentuan (3)";


                }
                else m_sandiBank = value;
            }
            get { return m_sandiBank; }

        }
        public string NomorRekening
        {
            set
            {
                if (value.Length > 20)
                {
                    m_snomorRekening = "0";
                    m_lastError = "Panjang Kode Rekening melebihi ketentuan (20)";


                }
                else m_snomorRekening = value;
            }
            get { return m_snomorRekening; }

        }
    }
}
