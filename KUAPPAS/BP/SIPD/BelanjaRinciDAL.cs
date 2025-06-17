using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP;
using Formatting;
using DataAccess;
using System.Data;

namespace BP.SIPD
{
    public class BelanjaRinciDAL:BP  
    {
        public BelanjaRinciDAL(int _pTahun, int profile)
            : base(_pTahun, 0, profile)
        {

        }
        //public List<BelanjaRinci> Get()
        //{

        //    string SSQL = "";

        //    SSQL = "SELECT * from BelanjaRinci";

        //}
    }
}
