
using System;
using System.Collections.Generic;


namespace KUAPPAS
{
    public static class ExceptionReport
    {
        /////////////////////////////////////////////////////////////////////
        // Get exception message and exception stack
        /////////////////////////////////////////////////////////////////////

        public static string[] GetMessageAndStack
                (
                Exception Ex
                )
        {
            // get system stack at the time of exception
            string StackTraceStr = Ex.StackTrace;

            // break it into individual lines
            string[] StackTraceLines = StackTraceStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // create a new array of trace lines
            List<string> StackTrace = new List<string>();

            // exception error message
            StackTrace.Add(Ex.Message);
#if DEBUG
           // Trace.Write(Ex.Message);
#endif

            // add trace lines
            foreach (string Line in StackTraceLines) if (Line.Contains("PdfFileWriter"))
                {
                    StackTrace.Add(Line);
#if DEBUG
                   // Trace.Write(Line);
#endif
                }

            // error exit
            return StackTrace.ToArray();
        }
    }
}
