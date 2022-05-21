using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Utils
{
    public class Logger
    {
        public static string PathLog { get; set; }

        public static void writer(Exception ex)
        {
            string fullPathLog = Path.Combine(PathLog, getFileName());

            using (StreamWriter sw = new StreamWriter(fullPathLog, true))
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                log.Append("\n------------------");
                log.Append("\nData:");
                log.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                log.Append("\nMensagem:");
                log.Append(ex.Message);
                log.Append("\nStackTrace:");
                log.Append(ex.StackTrace);
                log.Append("\nInnerException:");
                log.Append(ex.InnerException);
                log.Append("\nTargetSite:");
                log.Append(ex.TargetSite);
                sw.Write(log);
            }
        }

        private static string getFileName()
        {
            string name = DateTime.Now.ToString("yyyy-MM-dd");
            return $"{name}.txt";
        }
    }
}