using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUAPPAS
{
    public class EventResponseMessage
    {
        public EventResponseMessage()
        {
            ResponseStatus = true;
        }

        public bool ResponseStatus { set; get; }
        public string Message { set; get; }
        
    }
}
