using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_bib.Configuration.Database.SQLServer
{
    public class Parameters
    {
        public static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["biblioteca"].ConnectionString;
        }
    }
}