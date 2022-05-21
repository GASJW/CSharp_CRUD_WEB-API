using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_bib.Configuration.Logger
{
    public class Parameters
    {
        public static string getPathLog()
        {
            return System.Configuration.ConfigurationManager.AppSettings["PathLog"].ToString();
        }
    }
}