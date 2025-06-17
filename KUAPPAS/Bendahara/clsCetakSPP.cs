using DTO.Bendahara;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUAPPAS.Bendahara
{
    public class clsCetakSPP
    {
        
        long m_inourut;
        SPP m_oSPP;

        public clsCetakSPP()
        {
        }
        public SPP Data{
        
                set {
                    m_oSPP= value ;
                }
         }
        public clsCetakSPP(long pNoUrut)
        {
            m_inourut = pNoUrut;
        }
        public bool CetakSPP1()
        {

            try
            {


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
