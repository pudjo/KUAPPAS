using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KnightsWarriorAutoupdater;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace KUAPPAS
{
    static class Program
    {
        //public static string[] Args { get; set; } 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
               
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cWWFCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9cdnRSQ2JfU0RxV0M=");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

               Application.Run(new frmMainBaru());

               //Application.Run(new frmMainSide());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                File.WriteAllText("kesalahan.txt",ex.Message);
                // Read a file
  
            }
            
           }
        public static bool CheckForInternetConnection()
        {
            try
            {

                bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                return bb;

//D:\Development\PERMEN90Maret\KUAPPAS\Program.cs
            }
            catch
            {
                return false;
            }
        }
        public static void KillPriorprocess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            string me = curr.MainModule.FileName;
            string bak = me + ".bak";

            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) && (
                    (p.MainModule.FileName == curr.MainModule.FileName) ||
                    (p.MainModule.FileName.Contains(curr.MainModule.FileName))))
                {
                    //   p.Close();
                    p.Close();
                    p.Dispose();
                }


            }
            Process[] procs2 = Process.GetProcessesByName(bak);
            foreach (Process p in procs2)
            {

                p.CloseMainWindow();
                p.Close();
                p.Dispose();



            }

            return;
        }


    }
}
