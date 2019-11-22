using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Helper_Classes
{
    public class Logfile
    {

        public static void WriteLogFile(Exception e)
        {
            string path;
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LogFile");
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            using (StreamWriter logwriter = new StreamWriter(path, true))
            {
                logwriter.WriteLine("Date/Time: " + DateTime.Now.ToString());
                logwriter.WriteLine();

                while (e != null)
                {
                    logwriter.WriteLine(e.GetType().FullName);
                    logwriter.WriteLine("Message : " + e.Message);
                    logwriter.WriteLine("StackTrace : " + e.StackTrace);
                    e = e.InnerException;
                    logwriter.WriteLine("------------------------------------------");
                }
            }
        }
    }
}