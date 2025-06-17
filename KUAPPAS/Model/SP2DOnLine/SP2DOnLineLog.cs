using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SP2DOnLine
{
    public class SP2DOnLineLog
    {
        //create table SP2dOnlineLog (inourut bigint,no int,  waktu varchar(20), resposekode char(2), pesan varchar(100))

        public long NoUrut { set; get; }
        public int  No { set; get; }
        public DateTime  Waktu { set; get; }
        public string otp { set; get; }
        public string  responseKode { set; get; }

        public string pesan { set; get; }

    }
}
